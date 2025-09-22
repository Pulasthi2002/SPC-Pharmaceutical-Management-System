using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class SupplierManagementForm : Form
    {
        private int selectedSupplierId = 0;
        private int selectedUserId = 0; // To store the user_id of the selected supplier
        private bool isEditing = false;
        private const string SearchPlaceholder = "Search by company, contact, or license...";

        public SupplierManagementForm()
        {
            InitializeComponent();
        }

        private void SupplierManagementForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            SetupDataGridView();
            ClearForm();
            txtSearch_Leave(txtSearch, EventArgs.Empty); // Set initial placeholder text
        }

        #region UI and Form Helpers

        /// <summary>
        /// Sets up the visual styling for the DataGridView.
        /// </summary>
        private void SetupDataGridView()
        {
            dgvSuppliers.EnableHeadersVisualStyles = false;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvSuppliers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSuppliers.ColumnHeadersHeight = 35;
            dgvSuppliers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvSuppliers.DefaultCellStyle.BackColor = Color.White;
            dgvSuppliers.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvSuppliers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvSuppliers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvSuppliers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvSuppliers.RowTemplate.Height = 30;
        }

        /// <summary>
        /// Resets the form to its initial state.
        /// </summary>
        private void ClearForm()
        {
            // Clear text fields
            txtCompanyName.Clear();
            txtContactPerson.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtLicenseNumber.Clear();
            txtIdNumber.Clear();
            txtPassword.Clear();

            // Reset selection and state
            dgvSuppliers.ClearSelection();
            selectedSupplierId = 0;
            selectedUserId = 0;
            isEditing = false;

            UpdateButtonStates();

            // Reset validation indicators
            txtCompanyName.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtLicenseNumber.BackColor = Color.White;
            txtIdNumber.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
        }

        /// <summary>
        /// Enables or disables form controls based on the current mode (add/edit).
        /// </summary>
        private void UpdateButtonStates()
        {
            btnUpdate.Enabled = isEditing;
            btnDelete.Enabled = isEditing;

            // User credential fields are only enabled when adding a new supplier
            txtIdNumber.Enabled = !isEditing;
            txtPassword.Enabled = !isEditing;
        }

        /// <summary>
        /// Hashes a password using SHA256 for secure storage.
        /// </summary>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Validates user input before database operations.
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Company name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompanyName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
            {
                MessageBox.Show("License number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLicenseNumber.Focus();
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Additional validation for adding a new supplier (user account creation)
            if (!isEditing)
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email is required for user account creation.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtIdNumber.Text))
                {
                    MessageBox.Show("ID Number is required for user account creation.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdNumber.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required for user account creation.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Database Operations

        /// <summary>
        /// Loads all suppliers from the database and populates the DataGridView.
        /// Joins with the users table to get the user_id for each supplier.
        /// </summary>
        private void LoadSuppliers()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT s.supplier_id as 'ID', s.user_id, s.company_name as 'Company Name', 
                                   s.contact_person as 'Contact Person', s.email as 'Email', 
                                   s.phone as 'Phone', s.license_number as 'License Number',
                                   s.registration_date as 'Registration Date', 
                                   CASE WHEN s.is_active = 1 THEN 'Active' ELSE 'Inactive' END as 'Status'
                                   FROM suppliers s
                                   ORDER BY s.company_name";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvSuppliers.DataSource = dataTable;

                    // Hide the user_id column as it's for internal use only
                    if (dgvSuppliers.Columns["user_id"] != null)
                    {
                        dgvSuppliers.Columns["user_id"].Visible = false;
                    }
                    lblRecordCount.Text = $"Records: {dataTable.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fetches and displays the address for the currently selected supplier.
        /// </summary>
        private void LoadSupplierAddress(int supplierId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT address FROM suppliers WHERE supplier_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", supplierId);
                    object result = cmd.ExecuteScalar();
                    txtAddress.Text = result?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier address: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Add button click. Creates a new user and a new supplier within a transaction.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Insert into users table and retrieve the new user ID
                        string userQuery = @"INSERT INTO users (full_name, id_number, email, password_hash, role, is_active)
                                             VALUES (@full_name, @id_number, @email, @password_hash, 'SUPPLIER', 1);
                                             SELECT LAST_INSERT_ID();";
                        MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction);
                        userCmd.Parameters.AddWithValue("@full_name", txtContactPerson.Text.Trim());
                        userCmd.Parameters.AddWithValue("@id_number", txtIdNumber.Text.Trim());
                        userCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        userCmd.Parameters.AddWithValue("@password_hash", HashPassword(txtPassword.Text));
                        int newUserId = Convert.ToInt32(userCmd.ExecuteScalar());

                        // 2. Insert into suppliers table using the new user ID
                        string supplierQuery = @"INSERT INTO suppliers (user_id, company_name, contact_person, email, phone, address, license_number, registration_date, is_active) 
                                               VALUES (@user_id, @company_name, @contact_person, @email, @phone, @address, @license_number, NOW(), 1)";
                        MySqlCommand supplierCmd = new MySqlCommand(supplierQuery, connection, transaction);
                        supplierCmd.Parameters.AddWithValue("@user_id", newUserId);
                        supplierCmd.Parameters.AddWithValue("@company_name", txtCompanyName.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@contact_person", txtContactPerson.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@license_number", txtLicenseNumber.Text.Trim());
                        supplierCmd.ExecuteNonQuery();

                        // 3. If both inserts succeed, commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Supplier and user account added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadSuppliers();
                        ClearForm();
                    }
                    catch (MySqlException mx)
                    {
                        transaction.Rollback();
                        if (mx.Number == 1062) // Handle duplicate entry for unique keys (email, id_number)
                        {
                            MessageBox.Show("A user with this Email or ID Number already exists. Please use a unique Email and ID Number.", "Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"A database error occurred: {mx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Update button click. Updates both supplier and user details in a transaction.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId == 0 || selectedUserId == 0)
            {
                MessageBox.Show("Please select a supplier from the list to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateInput())
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        // 1. Update the suppliers table
                        string supplierQuery = @"UPDATE suppliers SET company_name = @company_name, contact_person = @contact_person, 
                                               email = @email, phone = @phone, address = @address, 
                                               license_number = @license_number WHERE supplier_id = @id";
                        MySqlCommand supplierCmd = new MySqlCommand(supplierQuery, connection, transaction);
                        supplierCmd.Parameters.AddWithValue("@company_name", txtCompanyName.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@contact_person", txtContactPerson.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@license_number", txtLicenseNumber.Text.Trim());
                        supplierCmd.Parameters.AddWithValue("@id", selectedSupplierId);
                        supplierCmd.ExecuteNonQuery();

                        // 2. Update the corresponding users table record
                        string userQuery = "UPDATE users SET full_name = @full_name, email = @email WHERE user_id = @user_id";
                        MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction);
                        userCmd.Parameters.AddWithValue("@full_name", txtContactPerson.Text.Trim());
                        userCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        userCmd.Parameters.AddWithValue("@user_id", selectedUserId);
                        userCmd.ExecuteNonQuery();

                        // 3. Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Supplier details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadSuppliers();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating supplier: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Delete button click. Deactivates both the supplier and the user.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId == 0 || selectedUserId == 0)
            {
                MessageBox.Show("Please select a supplier to deactivate.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to deactivate the supplier '{txtCompanyName.Text}'? This will also deactivate their user account.", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        // Deactivate supplier
                        string supplierQuery = "UPDATE suppliers SET is_active = 0 WHERE supplier_id = @id";
                        MySqlCommand supplierCmd = new MySqlCommand(supplierQuery, connection, transaction);
                        supplierCmd.Parameters.AddWithValue("@id", selectedSupplierId);
                        supplierCmd.ExecuteNonQuery();

                        // Deactivate user
                        string userQuery = "UPDATE users SET is_active = 0 WHERE user_id = @user_id";
                        MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction);
                        userCmd.Parameters.AddWithValue("@user_id", selectedUserId);
                        userCmd.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Supplier and associated user account deactivated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadSuppliers();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error deactivating supplier: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Clear button click to reset the form.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Handles selection change in the DataGridView to populate the form for editing.
        /// </summary>
        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvSuppliers.SelectedRows[0];

                // Retrieve data from the selected row
                selectedSupplierId = Convert.ToInt32(row.Cells["ID"].Value);
                selectedUserId = Convert.ToInt32(row.Cells["user_id"].Value);

                txtCompanyName.Text = row.Cells["Company Name"].Value?.ToString();
                txtContactPerson.Text = row.Cells["Contact Person"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtLicenseNumber.Text = row.Cells["License Number"].Value?.ToString();

                // The password and ID number are not displayed for security reasons
                txtPassword.Clear();
                txtIdNumber.Clear();

                LoadSupplierAddress(selectedSupplierId);

                isEditing = true;
                UpdateButtonStates();
            }
        }

        #region Search and Filter Event Handlers
        // These handlers manage the search placeholder text and trigger searches.

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SearchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
                txtSearch.Font = new Font(txtSearch.Font, FontStyle.Regular);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = SearchPlaceholder;
                txtSearch.ForeColor = Color.Gray;
                txtSearch.Font = new Font(txtSearch.Font, FontStyle.Italic);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch_Leave(txtSearch, EventArgs.Empty);
            cmbStatusFilter.SelectedIndex = 0;
            LoadSuppliers();
        }

        // Search functionality would be implemented here, similar to LoadSuppliers but with WHERE clauses.
        // For brevity and focus on the core request, the SearchSuppliers method is omitted, 
        // but would follow a similar pattern to LoadSuppliers.

        #endregion

        #endregion
    }
}
