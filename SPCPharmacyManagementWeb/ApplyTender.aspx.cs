//ApplyTender.aspx.cs
using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security; // Required for FormsAuthentication

namespace Spc_web
{
    public partial class ApplyTender : System.Web.UI.Page
    {
        protected int CurrentSupplierId { get; private set; } // Property to store the logged-in supplier's ID

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Authenticate and get the supplier ID
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx"); // Redirect to login if not authenticated
                    return;
                }

                // Retrieve user ID from the FormsAuthentication ticket
                string userData = ((FormsIdentity)User.Identity).Ticket.UserData;
                string[] parts = userData.Split('|');
                if (parts.Length < 2 || parts[0] != "SUPPLIER")
                {
                    // Invalid ticket data or not a supplier
                    FormsAuthentication.SignOut();
                    Response.Redirect("Login.aspx");
                    return;
                }

                int userId = int.Parse(parts[1]);
                CurrentSupplierId = GetSupplierIdByUserId(userId);

                if (CurrentSupplierId == 0) // Supplier ID not found for the user ID
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Request.QueryString["tender_id"] == null)
                {
                    Response.Redirect("Supplier.aspx");
                    return;
                }

                int tenderId = Convert.ToInt32(Request.QueryString["tender_id"]);
                hdnTenderId.Value = tenderId.ToString();
                LoadTenderDetails(tenderId);
                LoadTenderItems(tenderId);
            }
        }

        private int GetSupplierIdByUserId(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;
            int supplierId = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT supplier_id FROM suppliers WHERE user_id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    supplierId = Convert.ToInt32(result);
                }
            }
            return supplierId;
        }

        private void LoadTenderDetails(int tenderId)
        {
            string query = "SELECT * FROM tenders WHERE tender_id = @tenderId";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenderId", tenderId);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        litTenderTitle.Text = reader["title"].ToString();
                        litTenderDesc.Text = reader["description"].ToString();
                    }
                }
            }
        }

        private void LoadTenderItems(int tenderId)
        {
            string query = "SELECT * FROM tender_items WHERE tender_id = @tenderId";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenderId", tenderId);
                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptProposalItems.DataSource = dt;
                    rptProposalItems.DataBind();
                }
            }
        }

        protected void btnSubmitProposal_Click(object sender, EventArgs e)
        {
            
            if (CurrentSupplierId == 0)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                string userData = ((FormsIdentity)User.Identity).Ticket.UserData;
                string[] parts = userData.Split('|');
                int userId = int.Parse(parts[1]);
                CurrentSupplierId = GetSupplierIdByUserId(userId);
                if (CurrentSupplierId == 0)
                {
                    // Log error or show message: Supplier ID could not be determined.
                    ShowMessage("Error: Could not determine supplier ID. Please log in again.");
                    return;
                }
            }

            int tenderId = Convert.ToInt32(hdnTenderId.Value);
            int supplierId = CurrentSupplierId; // Use the actual logged-in supplier ID
            string notes = txtNotes.Text;
            decimal totalAmount = Convert.ToDecimal(hdnTotalAmount.Value);

            // Insert proposal
            string query = @"INSERT INTO tender_proposals 
                            (tender_id, supplier_id, notes, total_proposed_amount)
                            VALUES (@tenderId, @supplierId, @notes, @totalAmount);
                            SELECT LAST_INSERT_ID();";

            int proposalId = 0;

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenderId", tenderId);
                cmd.Parameters.AddWithValue("@supplierId", supplierId); // Use the retrieved supplierId
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@totalAmount", totalAmount);

                conn.Open();
                proposalId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Insert proposal items
            foreach (RepeaterItem item in rptProposalItems.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                    HiddenField hdnTenderItemId = (HiddenField)item.FindControl("hdnTenderItemId");

                    decimal unitPrice;
                    if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                    {
                        ShowMessage("Invalid unit price entered. Please enter a valid number.");
                        // Optionally, skip this item or break the loop and rollback.
                        continue;
                    }
                    int tenderItemId = Convert.ToInt32(hdnTenderItemId.Value);

                    query = @"INSERT INTO proposal_items 
                             (proposal_id, tender_item_id, proposed_unit_price)
                             VALUES (@proposalId, @tenderItemId, @unitPrice)";

                    using (MySqlConnection conn = new MySqlConnection(
                        ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@proposalId", proposalId);
                        cmd.Parameters.AddWithValue("@tenderItemId", tenderItemId);
                        cmd.Parameters.AddWithValue("@unitPrice", unitPrice);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Redirect back to supplier page
            Response.Redirect("Supplier.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Supplier.aspx");
        }

        private void ShowMessage(string message)
        {
            // You'll need a Label control on ApplyTender.aspx for this to work
            // Example: <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            // If you don't have one, you might consider using ClientScript.RegisterStartupScript
            // or redirecting with a message in the query string.
            // For now, assuming you have lblMessage on ApplyTender.aspx
            Label lbl = (Label)Master.FindControl("ContentPlaceHolder1").FindControl("lblMessage");
            if (lbl != null)
            {
                lbl.Text = message;
                lbl.Visible = true;
                lbl.CssClass = "text-danger";
            }
            else
            {
                // Fallback if lblMessage is not found, e.g., for debugging
                // Response.Write($"<script>alert('{message}');</script>");
            }
        }
    }
}
