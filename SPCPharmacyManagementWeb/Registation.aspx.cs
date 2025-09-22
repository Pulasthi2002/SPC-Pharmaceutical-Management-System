using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;

namespace Spc_web
{
    public partial class Registation : Page
    {
        // Manual declaration if designer file doesn't have it
        protected global::System.Web.UI.WebControls.TextBox txtID;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowMessage("Passwords do not match!");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if email or ID already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @email OR id_number = @id";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@id", txtID.Text.Trim());

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        ShowMessage("Email or ID number already registered!");
                        return;
                    }

                    // Hash password
                    string hashedPassword = HashPassword(txtPassword.Text);

                    // Insert new user
                    string insertQuery = @"INSERT INTO users 
                                        (full_name, id_number, email, password_hash, role) 
                                        VALUES (@name, @id, @email, @password, @role);
                                        SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", ddlRole.SelectedValue);

                    int userId = Convert.ToInt32(cmd.ExecuteScalar());

                    if (userId > 0)
                    {
                        // Store user ID in session
                        Session["NewUserId"] = userId;
                        Session["NewUserRole"] = ddlRole.SelectedValue;

                        // Redirect based on role
                        switch (ddlRole.SelectedValue)
                        {
                            case "SUPPLIER":
                                Response.Redirect("Supplierdetails.aspx");
                                break;
                            case "PHARMACY":
                                Response.Redirect("pharmaciesdetails.aspx");
                                break;
                            default:
                                Response.Redirect("Login.aspx?registered=true");
                                break;
                        }
                    }
                    else
                    {
                        ShowMessage("Registration failed. Please try again.");
                    }
                }
                catch (MySqlException ex)
                {
                    ShowMessage("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    ShowMessage("Error: " + ex.Message);
                }
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
