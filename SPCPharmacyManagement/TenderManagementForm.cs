using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SPCPharmacyManagement.Models;
using SPCPharmacyManagement.Services;

namespace SPCPharmacyManagement
{
    public partial class TenderManagementForm : Form
    {
        private string adminUsername;

        public TenderManagementForm(string username)
        {
            InitializeComponent();
            this.adminUsername = username;
        }

        private void TenderManagementForm_Load(object sender, EventArgs e)
        {
            LoadTenders();
        }

        private void LoadTenders()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT tender_id, title, deadline_date, status FROM tenders ORDER BY created_date DESC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTenders.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tenders: " + ex.Message, "Error");
            }
        }

        private void dgvTenders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTenders.SelectedRows.Count > 0)
            {
                int tenderId = Convert.ToInt32(dgvTenders.SelectedRows[0].Cells["tender_id"].Value);
                LoadTenderDetails(tenderId);
                LoadProposals(tenderId);
            }
        }

        private void LoadTenderDetails(int tenderId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    // Get tender info
                    MySqlCommand cmd = new MySqlCommand("SELECT title, status FROM tenders WHERE tender_id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", tenderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTenderTitle.Text = reader["title"].ToString();
                            lblTenderStatus.Text = "Status: " + reader["status"].ToString();
                        }
                    }

                    // Get tender items
                    string query = "SELECT drug_name, required_quantity FROM tender_items WHERE tender_id = @id";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", tenderId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTenderItems.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tender details: " + ex.Message, "Error");
            }
        }

        private void LoadProposals(int tenderId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT p.proposal_id, s.company_name, p.proposal_date, p.status, p.total_proposed_amount 
                                     FROM tender_proposals p
                                     JOIN suppliers s ON p.supplier_id = s.supplier_id
                                     WHERE p.tender_id = @id
                                     ORDER BY p.proposal_date DESC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", tenderId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvProposals.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading proposals: " + ex.Message, "Error");
            }
        }

        private void btnCreateTender_Click(object sender, EventArgs e)
        {
            using (var form = new CreateTenderForm(this.adminUsername))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTenders(); // Refresh list after a new tender is created
                }
            }
        }

        private void btnAcceptProposal_Click(object sender, EventArgs e)
        {
            if (dgvProposals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a proposal to accept.", "Selection Error");
                return;
            }

            int proposalId = Convert.ToInt32(dgvProposals.SelectedRows[0].Cells["proposal_id"].Value);
            int tenderId = Convert.ToInt32(dgvTenders.SelectedRows[0].Cells["tender_id"].Value);

            DialogResult confirm = MessageBox.Show("Accepting this proposal will automatically reject all others for this tender and close the tender. Continue?", "Confirm Acceptance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // 1. Accept the selected proposal
                        MySqlCommand cmdAccept = new MySqlCommand("UPDATE tender_proposals SET status = 'ACCEPTED' WHERE proposal_id = @id", connection, transaction);
                        cmdAccept.Parameters.AddWithValue("@id", proposalId);
                        cmdAccept.ExecuteNonQuery();

                        // 2. Reject all other proposals for this tender
                        MySqlCommand cmdReject = new MySqlCommand("UPDATE tender_proposals SET status = 'REJECTED' WHERE tender_id = @tender_id AND proposal_id != @proposal_id", connection, transaction);
                        cmdReject.Parameters.AddWithValue("@tender_id", tenderId);
                        cmdReject.Parameters.AddWithValue("@proposal_id", proposalId);
                        cmdReject.ExecuteNonQuery();

                        // 3. Close the tender
                        MySqlCommand cmdCloseTender = new MySqlCommand("UPDATE tenders SET status = 'CLOSED' WHERE tender_id = @id", connection, transaction);
                        cmdCloseTender.Parameters.AddWithValue("@id", tenderId);
                        cmdCloseTender.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Proposal accepted successfully.", "Success");
                        LoadTenders(); // Refresh everything
                        LoadProposals(tenderId);
                        lblTenderStatus.Text = "Status: CLOSED";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accepting proposal: " + ex.Message, "Error");
            }
        }

        private void btnRejectProposal_Click(object sender, EventArgs e)
        {
            if (dgvProposals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a proposal to reject.", "Selection Error");
                return;
            }

            int proposalId = Convert.ToInt32(dgvProposals.SelectedRows[0].Cells["proposal_id"].Value);
            int tenderId = Convert.ToInt32(dgvTenders.SelectedRows[0].Cells["tender_id"].Value);

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE tender_proposals SET status = 'REJECTED' WHERE proposal_id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", proposalId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Proposal rejected.", "Success");
                    LoadProposals(tenderId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting proposal: " + ex.Message, "Error");
            }
        }
    }
}
