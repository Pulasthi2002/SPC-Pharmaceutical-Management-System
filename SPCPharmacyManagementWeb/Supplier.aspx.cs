//supplier.aspx.cs
using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.Security; // Required for FormsAuthentication
using System.Linq; // Required for LINQ extensions if using them

namespace Spc_web
{
    public partial class Supplier : System.Web.UI.Page
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

                LoadActiveTenders();
                LoadSupplierProposals(CurrentSupplierId); // Pass the supplier ID
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

        private void LoadActiveTenders()
        {
            string query = @"SELECT * FROM tenders 
                             WHERE status = 'OPEN' AND deadline_date > NOW()
                             ORDER BY deadline_date ASC";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Add column for tender items for nested repeater
                    if (!dt.Columns.Contains("Items"))
                        dt.Columns.Add("Items", typeof(DataTable));

                    foreach (DataRow row in dt.Rows)
                    {
                        int tenderId = Convert.ToInt32(row["tender_id"]);
                        row["Items"] = GetTenderItems(tenderId);
                    }

                    rptTenders.DataSource = dt;
                    rptTenders.DataBind();
                }
            }
        }

        private DataTable GetTenderItems(int tenderId)
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
                    return dt;
                }
            }
        }

        // Modified to accept supplierId
        private void LoadSupplierProposals(int supplierId)
        {
            string query = @"SELECT tp.*, t.title 
                             FROM tender_proposals tp
                             JOIN tenders t ON tp.tender_id = t.tender_id
                             WHERE tp.supplier_id = @supplierId -- Filter by logged-in supplier
                             ORDER BY tp.proposal_date DESC";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplierId", supplierId); // Add parameter
                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvProposals.DataSource = dt;
                    gvProposals.DataBind();
                }
            }
        }

        protected string GetStatusIcon(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    return "fa-check-circle";
                case "pending":
                    return "fa-clock";
                case "rejected":
                    return "fa-times-circle";
                default:
                    return "fa-info-circle"; // A default icon
            }
        }

        // For status badge coloring in GridView
        public string GetStatusClass(string status)
        {
            switch (status)
            {
                case "PENDING":
                    return "bg-warning";
                case "ACCEPTED":
                    return "bg-success";
                case "REJECTED":
                    return "bg-danger";
                default:
                    return "bg-secondary";
            }
        }
    }
}
