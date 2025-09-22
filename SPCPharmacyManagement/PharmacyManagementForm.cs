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
    public partial class PharmacyManagementForm : Form
    {
        private int selectedPharmacyId = 0;
        private int selectedUserId = 0; // To store the user_id of the selected pharmacy
        private bool isEditing = false;

        public PharmacyManagementForm()
        {
            InitializeComponent();
        }

        private void PharmacyManagementForm_Load(object sender, EventArgs e)
        {
            LoadPharmacies();
            ClearForm();
        }

        #region UI and Form Helper Methods

        /// <summary>
        /// Hashes a password using SHA256 for secure storage in the database.
        /// </summary>
        /// <param name="password">The plain-text password to hash.</param>
        /// <returns>The SHA256 hashed password as a hex string.</returns>
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
        /// Enables or disables form controls based on whether the user is adding or editing.
        /// </summary>
        private void UpdateButtonStates()
        {
            btnUpdate.Enabled = isEditing;
            // User credential fields are only for creating new pharmacies/users.
            txtIdNumber.Enabled = !isEditing;
            txtPassword.Enabled = !isEditing;
        }

        /// <summary>
        /// Resets the form to its default state for a new entry.
        /// </summary>
        private void ClearForm()
        {
            txtPharmacyName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtLicenseNumber.Clear();
            txtIdNumber.Clear();
            txtPassword.Clear();

            cmbPharmacyType.SelectedIndex = 0; // Default to 'SPC_OWNED' or first item
            chkIsActive.Checked = true;

            dgvPharmacies.ClearSelection();
            selectedPharmacyId = 0;
            selectedUserId = 0;
            isEditing = false;

            UpdateButtonStates();
        }

        /// <summary>
        /// Validates user input before any database operation.
        /// </summary>
        /// <returns>True if all inputs are valid, otherwise false.</returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPharmacyName.Text))
            {
                MessageBox.Show("Pharmacy name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
            {
                MessageBox.Show("License number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbPharmacyType.SelectedItem == null)
            {
                MessageBox.Show("Please select a pharmacy type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Stricter validation when creating a new pharmacy, as a user account must be created.
            if (!isEditing)
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(txtEmail.Text))
                {
                    MessageBox.Show("A valid email is required to create a user account.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtIdNumber.Text))
                {
                    MessageBox.Show("ID Number is required to create a user account.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required to create a user account.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Database Operations

        /// <summary>
        /// Loads all pharmacy records from the database into the DataGridView.
        /// </summary>
        private void LoadPharmacies()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    // The query now includes user_id, which is essential for updates.
                    string query = @"SELECT p.pharmacy_id, p.user_id, p.pharmacy_name, p.contact_person, 
                                     p.phone, p.email, p.license_number, p.pharmacy_type, p.is_active 
                                     FROM pharmacies p 
                                     ORDER BY p.pharmacy_name";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvPharmacies.DataSource = dataTable;

                    // Hide user_id from the user interface as it's for internal use.
                    if (dgvPharmacies.Columns["user_id"] != null)
                        dgvPharmacies.Columns["user_id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pharmacies: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Add' button. Creates a new user and pharmacy in a single transaction.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Step 1: Create the user account with the 'PHARMACY' role.
                    string userQuery = @"INSERT INTO users (full_name, id_number, email, password_hash, role, is_active) 
                                         VALUES (@FullName, @IdNumber, @Email, @Password, 'PHARMACY', @IsActive);
                                         SELECT LAST_INSERT_ID();";
                    MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction);
                    userCmd.Parameters.AddWithValue("@FullName", txtContactPerson.Text.Trim());
                    userCmd.Parameters.AddWithValue("@IdNumber", txtIdNumber.Text.Trim());
                    userCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    userCmd.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text));
                    userCmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                    int newUserId = Convert.ToInt32(userCmd.ExecuteScalar());

                    // Step 2: Create the pharmacy record and link it to the new user ID.
                    string pharmacyQuery = @"INSERT INTO pharmacies (user_id, pharmacy_name, contact_person, phone, email, address, license_number, pharmacy_type, is_active) 
                                             VALUES (@UserId, @Name, @Contact, @Phone, @Email, @Address, @License, @Type, @IsActive)";
                    MySqlCommand pharmacyCmd = new MySqlCommand(pharmacyQuery, connection, transaction);
                    pharmacyCmd.Parameters.AddWithValue("@UserId", newUserId);
                    pharmacyCmd.Parameters.AddWithValue("@Name", txtPharmacyName.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@License", txtLicenseNumber.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Type", cmbPharmacyType.SelectedItem.ToString());
                    pharmacyCmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                    pharmacyCmd.ExecuteNonQuery();

                    // Step 3: If both commands succeed, commit the transaction.
                    transaction.Commit();
                    MessageBox.Show("Pharmacy and user account created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPharmacies();
                    ClearForm();
                }
                catch (MySqlException mx)
                {
                    transaction.Rollback();
                    if (mx.Number == 1062) // Handle unique key violation (duplicate ID number or email)
                        MessageBox.Show("A user with this ID Number or Email already exists.", "Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Database error: " + mx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("An error occurred: " + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the click event for the 'Update' button. Updates both the pharmacy and user records in a transaction.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedPharmacyId == 0)
            {
                MessageBox.Show("Please select a pharmacy to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Step 1: Update the pharmacy details.
                    string pharmacyQuery = @"UPDATE pharmacies SET pharmacy_name=@Name, contact_person=@Contact, phone=@Phone, email=@Email, address=@Address, 
                                             license_number=@License, pharmacy_type=@Type, is_active=@IsActive WHERE pharmacy_id=@Id";
                    MySqlCommand pharmacyCmd = new MySqlCommand(pharmacyQuery, connection, transaction);
                    pharmacyCmd.Parameters.AddWithValue("@Name", txtPharmacyName.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@License", txtLicenseNumber.Text.Trim());
                    pharmacyCmd.Parameters.AddWithValue("@Type", cmbPharmacyType.SelectedItem.ToString());
                    pharmacyCmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                    pharmacyCmd.Parameters.AddWithValue("@Id", selectedPharmacyId);
                    pharmacyCmd.ExecuteNonQuery();

                    // Step 2: Update the corresponding user's details.
                    string userQuery = "UPDATE users SET full_name = @FullName, email = @Email, is_active = @IsActive WHERE user_id = @UserId";
                    MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction);
                    userCmd.Parameters.AddWithValue("@FullName", txtContactPerson.Text.Trim());
                    userCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    userCmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                    userCmd.Parameters.AddWithValue("@UserId", selectedUserId);
                    userCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Pharmacy updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPharmacies();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("An error occurred during update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region UI Event Handlers
        /// <summary>
        /// Populates the form fields when a pharmacy is selected in the grid.
        /// </summary>
        private void dgvPharmacies_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPharmacies.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPharmacies.SelectedRows[0];
                selectedPharmacyId = Convert.ToInt32(row.Cells["pharmacy_id"].Value);
                selectedUserId = Convert.ToInt32(row.Cells["user_id"].Value); // Capture the user_id

                txtPharmacyName.Text = row.Cells["pharmacy_name"].Value.ToString();
                txtContactPerson.Text = row.Cells["contact_person"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtLicenseNumber.Text = row.Cells["license_number"].Value.ToString();
                cmbPharmacyType.SelectedItem = row.Cells["pharmacy_type"].Value.ToString();
                chkIsActive.Checked = Convert.ToBoolean(row.Cells["is_active"].Value);

                txtAddress.Text = GetAddress(selectedPharmacyId);

                isEditing = true;
                UpdateButtonStates();
            }
        }

        /// <summary>
        /// Fetches the full address for a given pharmacy ID.
        /// </summary>
        private string GetAddress(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT address FROM pharmacies WHERE pharmacy_id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteScalar()?.ToString() ?? "";
                }
            }
            catch { return ""; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        #endregion
    }
}
