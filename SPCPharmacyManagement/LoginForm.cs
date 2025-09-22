using System;
using System.Drawing;
using System.Windows.Forms;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class LoginForm : Form
    {
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123";
        private const string MANAGER_USERNAME = "manager";
        private const string MANAGER_PASSWORD = "manager123";

        private int loginAttempts = 0;
        private const int MAX_LOGIN_ATTEMPTS = 3;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Test database connection on form load
            if (!DatabaseConnection.TestConnection())
            {
                MessageBox.Show("Warning: Database connection failed. Please check your MySQL server.\n\nThe application will continue but database operations may not work.",
                    "Database Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Validate credentials
            bool isValidUser = false;
            string userRole = "";

            if (username.Equals(ADMIN_USERNAME, StringComparison.OrdinalIgnoreCase) && password == ADMIN_PASSWORD)
            {
                isValidUser = true;
                userRole = "Administrator";
            }
            else if (username.Equals(MANAGER_USERNAME, StringComparison.OrdinalIgnoreCase) && password == MANAGER_PASSWORD)
            {
                isValidUser = true;
                userRole = "Manager";
            }

            if (isValidUser)
            {
                MessageBox.Show($"Login successful! Welcome {userRole}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                MainForm mainForm = new MainForm(username, userRole);
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                loginAttempts++;

                if (loginAttempts >= MAX_LOGIN_ATTEMPTS)
                {
                    MessageBox.Show("Maximum login attempts exceeded. Application will close.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    int remainingAttempts = MAX_LOGIN_ATTEMPTS - loginAttempts;
                    MessageBox.Show($"Invalid credentials. {remainingAttempts} attempt(s) remaining.\n\nValid credentials:\nAdmin: admin/admin123\nManager: manager/manager123",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?",
                "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(sender, e);
            }
        }

        // Visual effects
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(39, 174, 96);
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(46, 204, 113);
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.FromArgb(192, 57, 43);
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.FromArgb(231, 76, 60);
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {
            // Add shadow effect to login panel
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(50, 0, 0, 0), 3))
            {
                e.Graphics.DrawRectangle(pen, 2, 2, panel.Width - 4, panel.Height - 4);
            }
        }

        private void picLogo_Paint(object sender, PaintEventArgs e)
        {
            // Draw SPC logo
            PictureBox pic = sender as PictureBox;
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("SPC", font, brush, pic.ClientRectangle, sf);
            }
        }
    }
}
