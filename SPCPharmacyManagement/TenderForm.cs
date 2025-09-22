using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SPCPharmacyManagement.Models;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class CreateTenderForm : Form
    {
        private List<TenderItem> tenderItems = new List<TenderItem>();
        private string adminUsername;

        public CreateTenderForm(string username)
        {
            InitializeComponent();
            this.adminUsername = username;
            dgvTenderItems.DataSource = tenderItems;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDrugName.Text) || nudQuantity.Value <= 0)
            {
                MessageBox.Show("Please enter a valid drug name and quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tenderItems.Add(new TenderItem { DrugName = txtDrugName.Text, RequiredQuantity = (int)nudQuantity.Value });
            RefreshGrid();
            txtDrugName.Clear();
            nudQuantity.Value = 1;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvTenderItems.SelectedRows.Count > 0)
            {
                var selectedItem = dgvTenderItems.SelectedRows[0].DataBoundItem as TenderItem;
                if (selectedItem != null)
                {
                    tenderItems.Remove(selectedItem);
                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            dgvTenderItems.DataSource = null;
            dgvTenderItems.DataSource = tenderItems;
            dgvTenderItems.Columns["TenderItemId"].Visible = false;
            dgvTenderItems.Columns["TenderId"].Visible = false;
        }

        private void btnSaveTender_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || dtpDeadline.Value <= DateTime.Now || tenderItems.Count == 0)
            {
                MessageBox.Show("Title, a future deadline, and at least one item are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Insert tender
                        string tenderQuery = "INSERT INTO tenders (title, description, deadline_date, created_by) VALUES (@title, @desc, @deadline, @user); SELECT LAST_INSERT_ID();";
                        MySqlCommand tenderCmd = new MySqlCommand(tenderQuery, connection, transaction);
                        tenderCmd.Parameters.AddWithValue("@title", txtTitle.Text);
                        tenderCmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                        tenderCmd.Parameters.AddWithValue("@deadline", dtpDeadline.Value);
                        tenderCmd.Parameters.AddWithValue("@user", this.adminUsername);
                        int newTenderId = Convert.ToInt32(tenderCmd.ExecuteScalar());

                        // Insert tender items
                        foreach (var item in tenderItems)
                        {
                            string itemQuery = "INSERT INTO tender_items (tender_id, drug_name, required_quantity) VALUES (@tender_id, @drug, @qty)";
                            MySqlCommand itemCmd = new MySqlCommand(itemQuery, connection, transaction);
                            itemCmd.Parameters.AddWithValue("@tender_id", newTenderId);
                            itemCmd.Parameters.AddWithValue("@drug", item.DrugName);
                            itemCmd.Parameters.AddWithValue("@qty", item.RequiredQuantity);
                            itemCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Tender created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save tender: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
