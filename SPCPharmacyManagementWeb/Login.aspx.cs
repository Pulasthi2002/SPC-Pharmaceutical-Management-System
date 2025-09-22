using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Configuration; // For ConfigurationManager
using System.Text.RegularExpressions; // Added for email validation

namespace Spc_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check for registration success message
                if (Request.QueryString["registered"] == "true")
                {
                    lblMessage.Text = "Registration successful! Please log in.";
                    lblMessage.Visible = true;
                    lblMessage.CssClass = "text-success";
                }
            }
        }

        protected void RoleButton_Click(object sender, EventArgs e)
        {
            var button = (System.Web.UI.WebControls.LinkButton)sender;
            hfSelectedRole.Value = button.CommandArgument;
            lblRoleError.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string role = hfSelectedRole.Value;

            // --- Server-Side Validation Added ---
            if (string.IsNullOrEmpty(role))
            {
                ShowMessage("Please select your role.");
                return;
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ShowMessage("Please enter a valid email address.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("Please enter your password.");
                return;
            }
            // --- End of Validation ---


            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Select user_id and password_hash
                    string query = "SELECT user_id, password_hash FROM users WHERE email = @email AND role = @role";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@role", role);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("user_id");
                            string storedHash = reader.GetString("password_hash");

                            if (VerifyPassword(password, storedHash))
                            {
                                // Update last login
                                UpdateLastLogin(userId);

                                // Create authentication ticket with user_id and role
                                // The UserData field can store additional comma-separated values if needed.
                                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                    1, // Version
                                    email, // User name for the ticket, can be email or user_id
                                    DateTime.Now, // Issue Date
                                    DateTime.Now.AddMinutes(30), // Expiration
                                    chkRemember.Checked, // Persistent cookie
                                    $"{role}|{userId}" // UserData: Store role and userId
                                );

                                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                                // Create auth cookie
                                HttpCookie authCookie = new HttpCookie(
                                    FormsAuthentication.FormsCookieName,
                                    encryptedTicket
                                );

                                // Add the cookie to the response
                                Response.Cookies.Add(authCookie);

                                // Redirect based on role
                                RedirectToRolePage(role);
                            }
                            else
                            {
                                ShowMessage("Invalid email or password");
                            }
                        }
                        else
                        {
                            ShowMessage("No account found with these credentials");
                        }
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

        private void UpdateLastLogin(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET last_login = NOW() WHERE user_id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void RedirectToRolePage(string role)
        {
            switch (role)
            {
                case "SUPPLIER":
                    Response.Redirect("Supplier.aspx");
                    break;
                case "SPC_STAFF":
                    Response.Redirect("staff.aspx");
                    break;
                case "PHARMACY":
                    Response.Redirect("Pharmacy.aspx");
                    break;
                case "CUSTOMER":
                    Response.Redirect("Customers.aspx");
                    break;
                default:
                    ShowMessage("Invalid role selected");
                    break;
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from input password
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                // Compare computed hash with stored hash
                return sb.ToString().Equals(storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblMessage.CssClass = "text-danger";
        }
    }
}
