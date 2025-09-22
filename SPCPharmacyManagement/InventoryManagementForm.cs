using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SPCPharmacyManagement.Models;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class InventoryManagementForm : Form
    {
        private int selectedDrugId = 0;
        private bool isEditing = false;

        public InventoryManagementForm()
        {
            InitializeComponent();
        }

        private void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            LoadInventory();
            SetupDataGridView();
            ClearDrugForm();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView appearance
            dgvInventory.EnableHeadersVisualStyles = false;
            dgvInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvInventory.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvInventory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvInventory.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void LoadInventory()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT drug_id as 'ID', drug_name as 'Drug Name', 
                                   generic_name as 'Generic Name', manufacturer as 'Manufacturer',
                                   batch_number as 'Batch Number', expiry_date as 'Expiry Date',
                                   unit_price as 'Unit Price', quantity_in_stock as 'Stock Quantity'
                                   FROM drugs ORDER BY drug_name";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvInventory.DataSource = dataTable;
                    dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Hide ID column and format others
                    if (dgvInventory.Columns["ID"] != null) dgvInventory.Columns["ID"].Visible = false;
                    if (dgvInventory.Columns["Expiry Date"] != null) dgvInventory.Columns["Expiry Date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                    if (dgvInventory.Columns["Unit Price"] != null) dgvInventory.Columns["Unit Price"].DefaultCellStyle.Format = "C2";

                    lblRecordCount.Text = $"Records: {dataTable.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvInventory.SelectedRows[0];
                selectedDrugId = Convert.ToInt32(row.Cells["ID"].Value);

                txtDrugName.Text = row.Cells["Drug Name"].Value?.ToString();
                txtGenericName.Text = row.Cells["Generic Name"].Value?.ToString();
                txtManufacturer.Text = row.Cells["Manufacturer"].Value?.ToString();
                txtBatchNumber.Text = row.Cells["Batch Number"].Value?.ToString();

                DateTime expiryDate;
                if (DateTime.TryParse(row.Cells["Expiry Date"].Value?.ToString(), out expiryDate))
                    dtpExpiryDate.Value = expiryDate;

                decimal unitPrice;
                if (decimal.TryParse(row.Cells["Unit Price"].Value?.ToString(), out unitPrice))
                    nudUnitPrice.Value = unitPrice;

                int quantity;
                if (int.TryParse(row.Cells["Stock Quantity"].Value?.ToString(), out quantity))
                    nudQuantityInStock.Value = quantity;

                LoadDrugDescription(selectedDrugId);

                isEditing = true;
                UpdateButtonStates();
            }
        }

        private void LoadDrugDescription(int drugId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT description FROM drugs WHERE drug_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", drugId);

                    object result = cmd.ExecuteScalar();
                    txtDescription.Text = result?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading description: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDrug_Click(object sender, EventArgs e)
        {
            if (ValidateDrugInput())
            {
                try
                {
                    using (var connection = DatabaseConnection.GetConnection())
                    {
                        connection.Open();
                        string query = @"INSERT INTO drugs (drug_name, generic_name, manufacturer, batch_number, 
                                       expiry_date, unit_price, quantity_in_stock, description) 
                                       VALUES (@drug_name, @generic_name, @manufacturer, @batch_number, 
                                       @expiry_date, @unit_price, @quantity_in_stock, @description)";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@drug_name", txtDrugName.Text.Trim());
                        cmd.Parameters.AddWithValue("@generic_name", txtGenericName.Text.Trim());
                        cmd.Parameters.AddWithValue("@manufacturer", txtManufacturer.Text.Trim());
                        cmd.Parameters.AddWithValue("@batch_number", txtBatchNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@expiry_date", dtpExpiryDate.Value.Date);
                        cmd.Parameters.AddWithValue("@unit_price", nudUnitPrice.Value);
                        cmd.Parameters.AddWithValue("@quantity_in_stock", nudQuantityInStock.Value);
                        cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Drug added successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInventory();
                        ClearDrugForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding drug: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateDrug_Click(object sender, EventArgs e)
        {
            if (selectedDrugId == 0)
            {
                MessageBox.Show("Please select a drug to update.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateDrugInput())
            {
                try
                {
                    using (var connection = DatabaseConnection.GetConnection())
                    {
                        connection.Open();
                        string query = @"UPDATE drugs SET drug_name = @drug_name, generic_name = @generic_name, 
                                       manufacturer = @manufacturer, batch_number = @batch_number, 
                                       expiry_date = @expiry_date, unit_price = @unit_price, 
                                       quantity_in_stock = @quantity_in_stock, description = @description 
                                       WHERE drug_id = @id";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@drug_name", txtDrugName.Text.Trim());
                        cmd.Parameters.AddWithValue("@generic_name", txtGenericName.Text.Trim());
                        cmd.Parameters.AddWithValue("@manufacturer", txtManufacturer.Text.Trim());
                        cmd.Parameters.AddWithValue("@batch_number", txtBatchNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@expiry_date", dtpExpiryDate.Value.Date);
                        cmd.Parameters.AddWithValue("@unit_price", nudUnitPrice.Value);
                        cmd.Parameters.AddWithValue("@quantity_in_stock", nudQuantityInStock.Value);
                        cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@id", selectedDrugId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Drug updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInventory();
                        ClearDrugForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating drug: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteDrug_Click(object sender, EventArgs e)
        {
            if (selectedDrugId == 0)
            {
                MessageBox.Show("Please select a drug to delete.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the drug '{txtDrugName.Text}'?\n\nThis action cannot be undone and will remove all related records.",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = DatabaseConnection.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM drugs WHERE drug_id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id", selectedDrugId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Drug deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInventory();
                        ClearDrugForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting drug: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (selectedDrugId == 0)
            {
                MessageBox.Show("Please select a drug from the list to update its stock.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateStockUpdate())
            {
                try
                {
                    using (var connection = DatabaseConnection.GetConnection())
                    {
                        connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // 1. Insert a record into the stock_updates table
                                string insertQuery = @"INSERT INTO stock_updates (drug_id, quantity, update_type, reason, updated_by) 
                                                     VALUES (@drug_id, @quantity, @update_type, @reason, @updated_by)";
                                MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection, transaction);
                                insertCmd.Parameters.AddWithValue("@drug_id", selectedDrugId);
                                insertCmd.Parameters.AddWithValue("@quantity", nudUpdateQuantity.Value);
                                insertCmd.Parameters.AddWithValue("@update_type", cmbUpdateType.SelectedItem.ToString());
                                insertCmd.Parameters.AddWithValue("@reason", txtUpdateReason.Text.Trim());
                                insertCmd.Parameters.AddWithValue("@updated_by", txtUpdatedBy.Text.Trim());
                                insertCmd.ExecuteNonQuery();

                                // 2. Update the quantity in the drugs table
                                string updateQuery;
                                if (cmbUpdateType.SelectedItem.ToString() == "ADD")
                                {
                                    updateQuery = "UPDATE drugs SET quantity_in_stock = quantity_in_stock + @quantity WHERE drug_id = @drug_id";
                                }
                                else // REMOVE or ADJUSTMENT (handle adjustment logic if different)
                                {
                                    updateQuery = "UPDATE drugs SET quantity_in_stock = quantity_in_stock - @quantity WHERE drug_id = @drug_id";
                                }

                                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection, transaction);
                                updateCmd.Parameters.AddWithValue("@quantity", nudUpdateQuantity.Value);
                                updateCmd.Parameters.AddWithValue("@drug_id", selectedDrugId);
                                updateCmd.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("Stock updated successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadInventory();
                                ClearStockUpdateForm();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw; // Re-throw the exception to be caught by the outer catch block
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating stock: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearDrug_Click(object sender, EventArgs e)
        {
            ClearDrugForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDrugs();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchDrugs();
                e.Handled = true;
            }
        }

        private void SearchDrugs()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string searchTerm = txtSearch.Text.Trim();

                    string query = @"SELECT drug_id as 'ID', drug_name as 'Drug Name', 
                            generic_name as 'Generic Name', manufacturer as 'Manufacturer',
                            batch_number as 'Batch Number', expiry_date as 'Expiry Date',
                            unit_price as 'Unit Price', quantity_in_stock as 'Stock Quantity'
                            FROM drugs 
                            WHERE drug_name LIKE @search OR generic_name LIKE @search 
                            OR manufacturer LIKE @search OR batch_number LIKE @search
                            ORDER BY drug_name";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvInventory.DataSource = dataTable;
                    lblRecordCount.Text = $"Records: {dataTable.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching drugs: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDrugForm()
        {
            txtDrugName.Clear();
            txtGenericName.Clear();
            txtManufacturer.Clear();
            txtBatchNumber.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            nudUnitPrice.Value = 0;
            nudQuantityInStock.Value = 0;
            txtDescription.Clear();
            selectedDrugId = 0;
            isEditing = false;

            UpdateButtonStates();
        }

        private void ClearStockUpdateForm()
        {
            cmbUpdateType.SelectedIndex = 0;
            nudUpdateQuantity.Value = 1;
            txtUpdateReason.Clear();
            txtUpdatedBy.Clear();
        }

        private void UpdateButtonStates()
        {
            btnUpdateDrug.Enabled = isEditing;
            btnDeleteDrug.Enabled = isEditing;
        }

        private bool ValidateDrugInput()
        {
            if (string.IsNullOrWhiteSpace(txtDrugName.Text))
            {
                MessageBox.Show("Drug name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDrugName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtManufacturer.Text))
            {
                MessageBox.Show("Manufacturer is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManufacturer.Focus();
                return false;
            }

            if (dtpExpiryDate.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Expiry date must be in the future.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpiryDate.Focus();
                return false;
            }

            if (nudUnitPrice.Value <= 0)
            {
                MessageBox.Show("Unit price must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudUnitPrice.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateStockUpdate()
        {
            if (cmbUpdateType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an update type (ADD/REMOVE).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUpdateType.Focus();
                return false;
            }

            if (nudUpdateQuantity.Value <= 0)
            {
                MessageBox.Show("Update quantity must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudUpdateQuantity.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUpdatedBy.Text))
            {
                MessageBox.Show("The 'Updated By' field is required (e.g., Plant ID, Staff Name).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUpdatedBy.Focus();
                return false;
            }

            // Check if removing more stock than available
            if (cmbUpdateType.SelectedItem.ToString() == "REMOVE")
            {
                int currentStock = (int)nudQuantityInStock.Value;
                if (nudUpdateQuantity.Value > currentStock)
                {
                    MessageBox.Show($"Cannot remove {nudUpdateQuantity.Value} units. Only {currentStock} units are available.",
                        "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudUpdateQuantity.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
