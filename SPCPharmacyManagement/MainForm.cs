using System;
using System.Drawing;
using System.Windows.Forms;
using SPCPharmacyManagement.Services;

// Assuming your other forms are in the root namespace. 
// If they are in a subfolder like 'Forms', you would use:
// using SPCPharmacyManagement.Forms; 

namespace SPCPharmacyManagement
{
    public partial class MainForm : Form
    {
        private string currentUser;
        private string userRole;

        public MainForm(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            userRole = role;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckDatabaseConnection();
            UpdateStatusBar();
            UpdateDateTime();
            SetRoleBasedAccess(); // New method to control menu visibility

            lblWelcome.Text = $"Welcome to SPC Management System, {currentUser}!";
            lblUser.Text = $"User: {currentUser} ({userRole})";
        }

        private void SetRoleBasedAccess()
        {
            bool isAdmin = (userRole == "Administrator");

            // Enable/disable management menu items based on role
            supplierToolStripMenuItem.Enabled = isAdmin;
            inventoryToolStripMenuItem.Enabled = isAdmin;
            pharmacyToolStripMenuItem.Enabled = isAdmin;
            tenderToolStripMenuItem.Enabled = isAdmin;

            // Enable/disable quick action buttons based on role
            btnQuickSupplier.Enabled = isAdmin;
            btnQuickInventory.Enabled = isAdmin;
            btnQuickPharmacy.Enabled = isAdmin;
            btnQuickTender.Enabled = isAdmin;

            // Allow order management for both roles for now, or refine as needed
            // For example, non-admins can only view their own orders
            // ordersToolStripMenuItem.Enabled = true; 
            // btnQuickOrders.Enabled = true;
        }

        private void CheckDatabaseConnection()
        {
            if (!DatabaseConnection.TestConnection())
            {
                lblStatus.Text = "Database Disconnected";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Database connection failed. Please check your MySQL server.\n\nSome features may not work properly.",
                    "Database Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblStatus.Text = "Database Connected";
                lblStatus.ForeColor = Color.Green;
            }
        }

        private void UpdateStatusBar()
        {
            lblStatus.Text = "Ready";
            lblUser.Text = $"User: {currentUser} ({userRole})";
        }

        private void UpdateDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        // --- Menu Event Handlers ---
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => Application.Exit();
                loginForm.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SupplierManagementForm());
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new InventoryManagementForm());
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new OrderManagementForm());
        }

        private void pharmacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new PharmacyManagementForm());
        }

        private void tenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new TenderManagementForm(this.currentUser));
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports module will be available in a future version.",
                "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutText = "SPC Pharmacy Management System\n" +
                              "Version 1.0.0\n\n" +
                              "State Pharmaceutical Cooperation\n" +
                              "Management System for Pharmaceutical\n" +
                              "Supply Chain Operations\n\n" +
                              "© 2024 SPC. All rights reserved.";

            MessageBox.Show(aboutText, "About SPC Management System",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Generic method to open forms to reduce code duplication
        private void OpenForm(Form formToOpen)
        {
            try
            {
                // Check if the user has permission before opening
                if (!formToOpen.IsDisposed && !formToOpen.Disposing)
                {
                    bool canOpen = true;
                    // Example of role check before opening
                    if ((formToOpen is SupplierManagementForm || formToOpen is InventoryManagementForm || formToOpen is PharmacyManagementForm || formToOpen is TenderManagementForm) && userRole != "Administrator")
                    {
                        canOpen = false;
                    }

                    if (canOpen)
                    {
                        lblStatus.Text = $"Opening {formToOpen.Text}...";
                        formToOpen.ShowDialog(this); // Show as a modal dialog
                        lblStatus.Text = "Ready";
                        CheckDatabaseConnection(); // Re-check connection after dialog closes
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening {formToOpen.Text}: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error";
            }
            finally
            {
                // Dispose the form object if it's not already disposed
                if (formToOpen != null && !formToOpen.IsDisposed)
                {
                    formToOpen.Dispose();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the application?",
                "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the close event
            }
        }

        // Corrected signature for the Paint event
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (pic != null)
            {
                using (Font font = new Font("Arial", 16, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString("SPC", font, brush, pic.ClientRectangle, sf);
                }
            }
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }
    }
}

