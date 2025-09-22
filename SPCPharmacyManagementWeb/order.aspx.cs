using System;
using System.Configuration;
using System.Data;
using System.Web.Security; // Required for FormsAuthentication
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace Spc_web
{
    public partial class Order : Page
    {
        // Property to hold the pharmacy ID of the currently logged-in user
        protected int CurrentPharmacyId { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. AUTHENTICATE AND GET PHARMACY ID
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            // Retrieve user data stored in the FormsAuthentication ticket during login
            string userData = ((FormsIdentity)User.Identity).Ticket.UserData;
            string[] userParts = userData.Split('|');

            // Ensure the ticket is valid and the user has the 'PHARMACY' role
            if (userParts.Length < 2 || userParts[0] != "PHARMACY")
            {
                // If not a pharmacy user, sign them out and redirect to login
                FormsAuthentication.SignOut();
                Response.Redirect("Login.aspx", false);
                return;
            }

            int userId = int.Parse(userParts[1]);
            // Get the pharmacy_id associated with this user_id
            CurrentPharmacyId = GetPharmacyIdByUserId(userId);

            if (CurrentPharmacyId == 0)
            {
                // This can happen if a user with role 'PHARMACY' doesn't have a record in the pharmacies table
                // It's a data integrity issue, so redirecting to login is a safe fallback.
                FormsAuthentication.SignOut();
                Response.Redirect("Login.aspx", false);
                return;
            }

            // 2. PROCESS PAGE LOGIC
            if (!IsPostBack)
            {
                if (Session["OrderItems"] == null)
                {
                    Response.Redirect("Pharmacy.aspx");
                    return;
                }
                BindOrderItems();
            }
        }

        /// <summary>
        /// Queries the database to find the pharmacy_id for a given user_id.
        /// </summary>
        /// <param name="userId">The user_id of the logged-in user.</param>
        /// <returns>The corresponding pharmacy_id, or 0 if not found.</returns>
        private int GetPharmacyIdByUserId(int userId)
        {
            int pharmacyId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;
            string query = "SELECT pharmacy_id FROM pharmacies WHERE user_id = @userId";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pharmacyId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    // System.Diagnostics.Trace.WriteLine("Error getting pharmacy ID: " + ex.Message);
                }
            }
            return pharmacyId;
        }

        private void BindOrderItems()
        {
            DataTable dtOrderItems = (DataTable)Session["OrderItems"];
            gvOrderItems.DataSource = dtOrderItems;
            gvOrderItems.DataBind();

            decimal total = 0;
            foreach (DataRow row in dtOrderItems.Rows)
            {
                total += Convert.ToDecimal(row["total_price"]);
            }
            litTotalAmount.Text = total.ToString("C"); // Using "C" for currency format
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // 3. USE THE LOGGED-IN PHARMACY'S ID
            // Replace the hardcoded ID with the one retrieved in Page_Load
            int pharmacyId = this.CurrentPharmacyId;

            if (pharmacyId == 0)
            {
                // As a final check, ensure the pharmacy ID is valid before proceeding
                // You can show a message to the user here.
                return;
            }

            DataTable dtOrderItems = (DataTable)Session["OrderItems"];
            decimal totalAmount = decimal.Parse(litTotalAmount.Text, System.Globalization.NumberStyles.Currency);

            string orderQuery = @"INSERT INTO orders 
                                (pharmacy_id, shipping_address, order_notes, total_amount) 
                                VALUES (@pharmacyId, @shipping, @notes, @total);
                                SELECT LAST_INSERT_ID();";

            int orderId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(orderQuery, conn);
                cmd.Parameters.AddWithValue("@pharmacyId", pharmacyId); // Use the correct ID
                cmd.Parameters.AddWithValue("@shipping", txtShippingAddress.Text);
                cmd.Parameters.AddWithValue("@notes", txtOrderNotes.Text);
                cmd.Parameters.AddWithValue("@total", totalAmount);

                conn.Open();
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            if (orderId > 0)
            {
                foreach (DataRow row in dtOrderItems.Rows)
                {
                    string itemQuery = @"INSERT INTO order_items 
                                       (order_id, drug_id, quantity, unit_price, total_price)
                                       VALUES (@orderId, @drugId, @qty, @unitPrice, @totalPrice)";

                    using (MySqlConnection itemConn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand itemCmd = new MySqlCommand(itemQuery, itemConn);
                        itemCmd.Parameters.AddWithValue("@orderId", orderId);
                        itemCmd.Parameters.AddWithValue("@drugId", row["drug_id"]);
                        itemCmd.Parameters.AddWithValue("@qty", row["quantity"]);
                        itemCmd.Parameters.AddWithValue("@unitPrice", row["unit_price"]);
                        itemCmd.Parameters.AddWithValue("@totalPrice", row["total_price"]);
                        itemConn.Open();
                        itemCmd.ExecuteNonQuery();
                    }
                }

                Session.Remove("OrderItems");
                // Using false parameter prevents a ThreadAbortException
                Response.Redirect($"Pay.aspx?order_id={orderId}", false);
            }
        }
    }
}
