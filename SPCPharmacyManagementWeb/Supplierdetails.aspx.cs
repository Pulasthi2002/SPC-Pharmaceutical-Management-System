using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Spc_web
{
    public partial class Supplierdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NewUserId"] == null)
                {
                    Response.Redirect("Registation.aspx");
                }
                txtRegDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["NewUserId"]);

            string query = @"INSERT INTO suppliers 
                            (user_id, company_name, contact_person, email, phone, address, license_number)
                            VALUES (@userId, @company, @contact, @email, @phone, @address, @license)";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@company", txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@contact", txtContactPerson.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@license", txtLicenseNumber.Text);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Clear session and redirect
                        Session.Remove("NewUserId");
                        Response.Redirect("Login.aspx?registered=true");
                    }
                }
                catch (MySqlException ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
