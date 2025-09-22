using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Ensure this is present
using MySql.Data.MySqlClient;
using SPCPharmacyManagement.Models;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class OrderManagementForm : Form
    {
        private int selectedOrderId = 0;
        private List<OrderItem> currentOrderItems = new List<OrderItem>();
        private bool isEditing = false;

        public OrderManagementForm()
        {
            InitializeComponent();
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
            LoadPharmacies();
            LoadDrugs();
            ClearOrderForm();
        }

        private void LoadOrders()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT o.order_id as 'Order ID', p.pharmacy_name as 'Pharmacy', 
                                   o.order_date as 'Order Date', o.status as 'Status', 
                                   o.total_amount as 'Total Amount', o.order_notes as 'Notes'
                                   FROM orders o 
                                   JOIN pharmacies p ON o.pharmacy_id = p.pharmacy_id 
                                   ORDER BY o.order_date DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvOrders.DataSource = dataTable;
                    dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Format columns
                    if (dgvOrders.Columns["Order Date"] != null) dgvOrders.Columns["Order Date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                    if (dgvOrders.Columns["Total Amount"] != null) dgvOrders.Columns["Total Amount"].DefaultCellStyle.Format = "C2";

                    lblRecordCount.Text = $"Orders: {dataTable.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPharmacies()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT pharmacy_id, pharmacy_name FROM pharmacies WHERE is_active = 1 ORDER BY pharmacy_name";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    cmbPharmacy.Items.Clear();
                    cmbPharmacy.DisplayMember = "Text";
                    cmbPharmacy.ValueMember = "Value";

                    while (reader.Read())
                    {
                        cmbPharmacy.Items.Add(new { Text = reader["pharmacy_name"].ToString(), Value = Convert.ToInt32(reader["pharmacy_id"]) });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pharmacies: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDrugs()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT drug_id, CONCAT(drug_name, ' (Stock: ', quantity_in_stock, ')') as display_text, 
                                   drug_name, unit_price, quantity_in_stock 
                                   FROM drugs WHERE quantity_in_stock > 0 ORDER BY drug_name";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    cmbDrug.Items.Clear();
                    cmbDrug.DisplayMember = "Text";
                    cmbDrug.ValueMember = "Value";

                    while (reader.Read())
                    {
                        cmbDrug.Items.Add(new
                        {
                            Text = reader["display_text"].ToString(),
                            Value = new
                            {
                                DrugId = Convert.ToInt32(reader["drug_id"]),
                                DrugName = reader["drug_name"].ToString(),
                                UnitPrice = Convert.ToDecimal(reader["unit_price"]),
                                Stock = Convert.ToInt32(reader["quantity_in_stock"])
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading drugs: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvOrders.SelectedRows[0];
                selectedOrderId = Convert.ToInt32(row.Cells["Order ID"].Value);

                LoadOrderItems(selectedOrderId);
                LoadOrderForEditing(selectedOrderId);

                isEditing = true;
                UpdateButtonStates();
            }
        }

        private void LoadOrderItems(int orderId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT oi.drug_id as 'Drug ID', d.drug_name as 'Drug Name', 
                                   oi.quantity as 'Quantity', oi.unit_price as 'Unit Price', 
                                   oi.total_price as 'Total Price'
                                   FROM order_items oi 
                                   JOIN drugs d ON oi.drug_id = d.drug_id 
                                   WHERE oi.order_id = @order_id";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@order_id", orderId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvOrderItems.DataSource = dataTable;
                    dgvOrderItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dgvOrderItems.Columns["Drug ID"] != null) dgvOrderItems.Columns["Drug ID"].Visible = false;
                    if (dgvOrderItems.Columns["Unit Price"] != null) dgvOrderItems.Columns["Unit Price"].DefaultCellStyle.Format = "C2";
                    if (dgvOrderItems.Columns["Total Price"] != null) dgvOrderItems.Columns["Total Price"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order items: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderForEditing(int orderId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    // Load main order details
                    string orderQuery = "SELECT pharmacy_id, order_notes FROM orders WHERE order_id = @order_id";
                    MySqlCommand orderCmd = new MySqlCommand(orderQuery, connection);
                    orderCmd.Parameters.AddWithValue("@order_id", orderId);

                    using (MySqlDataReader reader = orderCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int pharmacyId = Convert.ToInt32(reader["pharmacy_id"]);
                            txtOrderNotes.Text = reader["order_notes"]?.ToString();

                            // Select pharmacy in ComboBox
                            for (int i = 0; i < cmbPharmacy.Items.Count; i++)
                            {
                                if (Convert.ToInt32(((dynamic)cmbPharmacy.Items[i]).Value) == pharmacyId)
                                {
                                    cmbPharmacy.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }

                    // Load order items into the temporary list
                    string itemsQuery = "SELECT drug_id, quantity, unit_price FROM order_items WHERE order_id = @order_id";
                    MySqlCommand itemsCmd = new MySqlCommand(itemsQuery, connection);
                    itemsCmd.Parameters.AddWithValue("@order_id", orderId);

                    currentOrderItems.Clear();
                    using (MySqlDataReader reader = itemsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new OrderItem
                            {
                                DrugId = Convert.ToInt32(reader["drug_id"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["unit_price"])
                            };
                            item.CalculateTotalPrice();
                            currentOrderItems.Add(item);
                        }
                    }

                    RefreshOrderItemsDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order for editing: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbDrug.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a drug to add.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dynamic selectedDrug = ((dynamic)cmbDrug.SelectedItem).Value;
            int drugId = selectedDrug.DrugId;
            string drugName = selectedDrug.DrugName;
            decimal unitPrice = selectedDrug.UnitPrice;
            int stock = selectedDrug.Stock;
            int quantity = (int)nudQuantity.Value;

            if (currentOrderItems.Any(x => x.DrugId == drugId))
            {
                MessageBox.Show("This drug is already in the order. Please remove it first to change the quantity.", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantity > stock)
            {
                MessageBox.Show($"Insufficient stock. Available: {stock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newItem = new OrderItem { DrugId = drugId, DrugName = drugName, Quantity = quantity, UnitPrice = unitPrice };
            newItem.CalculateTotalPrice();
            currentOrderItems.Add(newItem);

            RefreshOrderItemsDisplay();
            cmbDrug.SelectedIndex = -1;
            nudQuantity.Value = 1;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count == 0 && currentOrderItems.Any())
            {
                MessageBox.Show("Please select an item from the list below to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                int drugIdToRemove = Convert.ToInt32(dgvOrderItems.SelectedRows[0].Cells["Drug ID"].Value);
                var itemToRemove = currentOrderItems.FirstOrDefault(x => x.DrugId == drugIdToRemove);

                if (itemToRemove != null)
                {
                    currentOrderItems.Remove(itemToRemove);
                    RefreshOrderItemsDisplay();
                }
            }
        }

        private void RefreshOrderItemsDisplay()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Drug ID", typeof(int));
            dt.Columns.Add("Drug Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Unit Price", typeof(decimal));
            dt.Columns.Add("Total Price", typeof(decimal));

            decimal totalAmount = 0;
            foreach (var item in currentOrderItems)
            {
                dt.Rows.Add(item.DrugId, item.DrugName, item.Quantity, item.UnitPrice, item.TotalPrice);
                totalAmount += item.TotalPrice;
            }

            dgvOrderItems.DataSource = dt;
            if (dgvOrderItems.Columns["Drug ID"] != null) dgvOrderItems.Columns["Drug ID"].Visible = false;

            lblTotalAmount.Text = $"Total Amount: LKR {totalAmount:N2}";
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateOrderInput()) return;

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string orderQuery = @"INSERT INTO orders (pharmacy_id, order_date, status, total_amount, order_notes) 
                                                VALUES (@pharmacy_id, @order_date, @status, @total_amount, @order_notes);
                                                SELECT LAST_INSERT_ID();";
                            MySqlCommand orderCmd = new MySqlCommand(orderQuery, connection, transaction);
                            orderCmd.Parameters.AddWithValue("@pharmacy_id", ((dynamic)cmbPharmacy.SelectedItem).Value);
                            orderCmd.Parameters.AddWithValue("@order_date", DateTime.Now);
                            orderCmd.Parameters.AddWithValue("@status", "PENDING");
                            orderCmd.Parameters.AddWithValue("@total_amount", currentOrderItems.Sum(x => x.TotalPrice));
                            orderCmd.Parameters.AddWithValue("@order_notes", txtOrderNotes.Text.Trim());

                            int newOrderId = Convert.ToInt32(orderCmd.ExecuteScalar());

                            // Process order items
                            ProcessOrderItems(newOrderId, connection, transaction);

                            transaction.Commit();
                            MessageBox.Show("Order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadOrders();
                            LoadDrugs();
                            ClearOrderForm();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating order: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            if (selectedOrderId == 0)
            {
                MessageBox.Show("Please select an order to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateOrderInput()) return;

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Restore stock for all old items
                            RestoreStockForOrder(selectedOrderId, connection, transaction);

                            // Delete old items
                            MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM order_items WHERE order_id = @order_id", connection, transaction);
                            deleteCmd.Parameters.AddWithValue("@order_id", selectedOrderId);
                            deleteCmd.ExecuteNonQuery();

                            // Update the main order
                            string updateOrderQuery = @"UPDATE orders SET pharmacy_id = @pharmacy_id, total_amount = @total_amount, 
                                                      order_notes = @order_notes WHERE order_id = @order_id";
                            MySqlCommand updateCmd = new MySqlCommand(updateOrderQuery, connection, transaction);
                            updateCmd.Parameters.AddWithValue("@pharmacy_id", ((dynamic)cmbPharmacy.SelectedItem).Value);
                            updateCmd.Parameters.AddWithValue("@total_amount", currentOrderItems.Sum(x => x.TotalPrice));
                            updateCmd.Parameters.AddWithValue("@order_notes", txtOrderNotes.Text.Trim());
                            updateCmd.Parameters.AddWithValue("@order_id", selectedOrderId);
                            updateCmd.ExecuteNonQuery();

                            // Insert new items and deduct stock
                            ProcessOrderItems(selectedOrderId, connection, transaction);

                            transaction.Commit();
                            MessageBox.Show("Order updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadOrders();
                            LoadDrugs();
                            ClearOrderForm();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (selectedOrderId == 0)
            {
                MessageBox.Show("Please select an order to update its status.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] statuses = { "PENDING", "PROCESSING", "SHIPPED", "DELIVERED", "CANCELLED" };
            // Original line causing error: string newStatus = Interaction.InputBox("Enter new status (PENDING, PROCESSING, SHIPPED, DELIVERED, CANCELLED):", "Update Order Status", "");
            string newStatus = ShowInputDialog("Enter new status (PENDING, PROCESSING, SHIPPED, DELIVERED, CANCELLED):", "Update Order Status", "");

            if (string.IsNullOrEmpty(newStatus)) return;

            if (!statuses.Contains(newStatus.ToUpper()))
            {
                MessageBox.Show("Invalid status entered.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE orders SET status = @status WHERE order_id = @order_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@status", newStatus.ToUpper());
                    cmd.Parameters.AddWithValue("@order_id", selectedOrderId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Order status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order status: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            ClearOrderForm();
        }

        // Helper methods for transactions
        private void ProcessOrderItems(int orderId, MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (var item in currentOrderItems)
            {
                // Insert item
                string itemQuery = @"INSERT INTO order_items (order_id, drug_id, quantity, unit_price, total_price) 
                                   VALUES (@order_id, @drug_id, @quantity, @unit_price, @total_price)";
                MySqlCommand itemCmd = new MySqlCommand(itemQuery, connection, transaction);
                itemCmd.Parameters.AddWithValue("@order_id", orderId);
                itemCmd.Parameters.AddWithValue("@drug_id", item.DrugId);
                itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                itemCmd.Parameters.AddWithValue("@unit_price", item.UnitPrice);
                itemCmd.Parameters.AddWithValue("@total_price", item.TotalPrice);
                itemCmd.ExecuteNonQuery();

                // Deduct stock
                string stockQuery = "UPDATE drugs SET quantity_in_stock = quantity_in_stock - @quantity WHERE drug_id = @drug_id";
                MySqlCommand stockCmd = new MySqlCommand(stockQuery, connection, transaction);
                stockCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                stockCmd.Parameters.AddWithValue("@drug_id", item.DrugId);
                stockCmd.ExecuteNonQuery();
            }
        }

        private void RestoreStockForOrder(int orderId, MySqlConnection connection, MySqlTransaction transaction)
        {
            string restoreQuery = @"UPDATE drugs d JOIN order_items oi ON d.drug_id = oi.drug_id 
                                  SET d.quantity_in_stock = d.quantity_in_stock + oi.quantity 
                                  WHERE oi.order_id = @order_id";
            MySqlCommand restoreCmd = new MySqlCommand(restoreQuery, connection, transaction);
            restoreCmd.Parameters.AddWithValue("@order_id", orderId);
            restoreCmd.ExecuteNonQuery();
        }

        // UI and Validation
        private void ClearOrderForm()
        {
            cmbPharmacy.SelectedIndex = -1;
            cmbDrug.SelectedIndex = -1;
            nudQuantity.Value = 1;
            txtOrderNotes.Clear();
            currentOrderItems.Clear();
            selectedOrderId = 0;
            isEditing = false;

            RefreshOrderItemsDisplay();
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            btnUpdateOrder.Enabled = isEditing;
            btnUpdateStatus.Enabled = isEditing;
        }

        private bool ValidateOrderInput()
        {
            if (cmbPharmacy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a pharmacy for the order.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPharmacy.Focus();
                return false;
            }

            if (currentOrderItems.Count == 0)
            {
                MessageBox.Show("Please add at least one drug to the order.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOrders();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchOrders();
        }

        private void SearchOrders()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string searchTerm = txtSearch.Text.Trim();
                    string statusFilter = cmbStatusFilter.SelectedItem.ToString();

                    string query = @"SELECT o.order_id as 'Order ID', p.pharmacy_name as 'Pharmacy', 
                                   o.order_date as 'Order Date', o.status as 'Status', 
                                   o.total_amount as 'Total Amount', o.order_notes as 'Notes'
                                   FROM orders o 
                                   JOIN pharmacies p ON o.pharmacy_id = p.pharmacy_id 
                                   WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND (p.pharmacy_name LIKE @search OR o.order_id LIKE @search)";
                    }

                    if (statusFilter != "All")
                    {
                        query += " AND o.status = @status";
                    }

                    query += " ORDER BY o.order_date DESC";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    if (!string.IsNullOrEmpty(searchTerm))
                        cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                    if (statusFilter != "All")
                        cmd.Parameters.AddWithValue("@status", statusFilter);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvOrders.DataSource = dataTable;
                    lblRecordCount.Text = $"Orders: {dataTable.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching orders: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Custom input dialog to replace Interaction.InputBox for C# compatibility.
        /// </summary>
        /// <param name="prompt">The text to display as a prompt.</param>
        /// <param name="title">The title of the dialog box.</param>
        /// <param name="defaultValue">The default value of the text box.</param>
        /// <returns>The text entered by the user, or an empty string if the user clicked Cancel.</returns>
        private static string ShowInputDialog(string prompt, string title, string defaultValue)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = prompt;
            textBox.Text = defaultValue;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(10, 20, 370, 15);
            textBox.SetBounds(10, 45, 370, 25);
            buttonOk.SetBounds(220, 80, 75, 25);
            buttonCancel.SetBounds(305, 80, 75, 25);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(395, 115);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult result = form.ShowDialog();
            return result == DialogResult.OK ? textBox.Text : "";
        }
    }
}
