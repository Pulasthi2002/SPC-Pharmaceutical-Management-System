using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace Spc_web
{
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["order_id"] == null)
                {
                    Response.Redirect("Pharmacy.aspx");
                }

                int orderId = Convert.ToInt32(Request.QueryString["order_id"]);
                litOrderId.Text = orderId.ToString();

                // Load order details
                string query = "SELECT total_amount FROM orders WHERE order_id = @orderId";
                using (MySqlConnection conn = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        litTotalAmount.Text = Convert.ToDecimal(result).ToString("0.00");
                    }
                }
            }
        }

        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(Request.QueryString["order_id"]);

            string updateQuery = @"UPDATE orders 
                                SET payment_status = 'PAID'
                                WHERE order_id = @orderId";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Show success message
            Response.Redirect("OrderConfirmation.aspx?order_id=" + orderId);
        }
    }
}
