using System.Drawing;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    partial class TenderManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private SplitContainer splitContainer1;
        private DataGridView dgvTenders;
        private DataGridView dgvProposals;
        private GroupBox gbTenderDetails;
        private Label lblTenderTitle;
        private Label lblTenderStatus;
        private DataGridView dgvTenderItems;
        private Button btnCreateTender;
        private Button btnAcceptProposal;
        private Button btnRejectProposal;
        private TableLayoutPanel topButtonsPanel; // To manage button layout

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvTenders = new System.Windows.Forms.DataGridView();
            this.dgvProposals = new System.Windows.Forms.DataGridView();
            this.gbTenderDetails = new System.Windows.Forms.GroupBox();
            this.dgvTenderItems = new System.Windows.Forms.DataGridView();
            this.lblTenderStatus = new System.Windows.Forms.Label();
            this.lblTenderTitle = new System.Windows.Forms.Label();
            this.topButtonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateTender = new System.Windows.Forms.Button();
            this.btnAcceptProposal = new System.Windows.Forms.Button();
            this.btnRejectProposal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProposals)).BeginInit();
            this.gbTenderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenderItems)).BeginInit();
            this.topButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTenders);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvProposals);
            this.splitContainer1.Panel2.Controls.Add(this.gbTenderDetails);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Size = new System.Drawing.Size(984, 543);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvTenders
            // 
            this.dgvTenders.AllowUserToAddRows = false;
            this.dgvTenders.AllowUserToDeleteRows = false;
            this.dgvTenders.AllowUserToResizeRows = false;
            this.dgvTenders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTenders.BackgroundColor = System.Drawing.Color.White;
            this.dgvTenders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTenders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTenders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTenders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTenders.ColumnHeadersHeight = 35;
            this.dgvTenders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTenders.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTenders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTenders.EnableHeadersVisualStyles = false;
            this.dgvTenders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTenders.Location = new System.Drawing.Point(10, 10);
            this.dgvTenders.MultiSelect = false;
            this.dgvTenders.Name = "dgvTenders";
            this.dgvTenders.ReadOnly = true;
            this.dgvTenders.RowHeadersVisible = false;
            this.dgvTenders.RowTemplate.Height = 30;
            this.dgvTenders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTenders.Size = new System.Drawing.Size(380, 523);
            this.dgvTenders.TabIndex = 0;
            this.dgvTenders.SelectionChanged += new System.EventHandler(this.dgvTenders_SelectionChanged);
            // 
            // dgvProposals
            // 
            this.dgvProposals.AllowUserToAddRows = false;
            this.dgvProposals.AllowUserToDeleteRows = false;
            this.dgvProposals.AllowUserToResizeRows = false;
            this.dgvProposals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProposals.BackgroundColor = System.Drawing.Color.White;
            this.dgvProposals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProposals.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProposals.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProposals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProposals.ColumnHeadersHeight = 35;
            this.dgvProposals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProposals.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProposals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProposals.EnableHeadersVisualStyles = false;
            this.dgvProposals.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvProposals.Location = new System.Drawing.Point(10, 260);
            this.dgvProposals.MultiSelect = false;
            this.dgvProposals.Name = "dgvProposals";
            this.dgvProposals.ReadOnly = true;
            this.dgvProposals.RowHeadersVisible = false;
            this.dgvProposals.RowTemplate.Height = 30;
            this.dgvProposals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProposals.Size = new System.Drawing.Size(560, 273);
            this.dgvProposals.TabIndex = 1;
            // 
            // gbTenderDetails
            // 
            this.gbTenderDetails.Controls.Add(this.dgvTenderItems);
            this.gbTenderDetails.Controls.Add(this.lblTenderStatus);
            this.gbTenderDetails.Controls.Add(this.lblTenderTitle);
            this.gbTenderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTenderDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbTenderDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.gbTenderDetails.Location = new System.Drawing.Point(10, 10);
            this.gbTenderDetails.Name = "gbTenderDetails";
            this.gbTenderDetails.Size = new System.Drawing.Size(560, 250);
            this.gbTenderDetails.TabIndex = 0;
            this.gbTenderDetails.TabStop = false;
            this.gbTenderDetails.Text = "Selected Tender Details & Proposals";
            // 
            // dgvTenderItems
            // 
            this.dgvTenderItems.AllowUserToAddRows = false;
            this.dgvTenderItems.AllowUserToDeleteRows = false;
            this.dgvTenderItems.AllowUserToResizeRows = false;
            this.dgvTenderItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTenderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTenderItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.dgvTenderItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTenderItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTenderItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTenderItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTenderItems.ColumnHeadersHeight = 30;
            this.dgvTenderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTenderItems.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTenderItems.EnableHeadersVisualStyles = false;
            this.dgvTenderItems.Location = new System.Drawing.Point(10, 60);
            this.dgvTenderItems.MultiSelect = false;
            this.dgvTenderItems.Name = "dgvTenderItems";
            this.dgvTenderItems.ReadOnly = true;
            this.dgvTenderItems.RowHeadersVisible = false;
            this.dgvTenderItems.RowTemplate.Height = 28;
            this.dgvTenderItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTenderItems.Size = new System.Drawing.Size(544, 184);
            this.dgvTenderItems.TabIndex = 2;
            // 
            // lblTenderStatus
            // 
            this.lblTenderStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenderStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenderStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTenderStatus.Location = new System.Drawing.Point(350, 30);
            this.lblTenderStatus.Name = "lblTenderStatus";
            this.lblTenderStatus.Size = new System.Drawing.Size(200, 23);
            this.lblTenderStatus.TabIndex = 1;
            this.lblTenderStatus.Text = "Status: OPEN";
            this.lblTenderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTenderTitle
            // 
            this.lblTenderTitle.AutoSize = true;
            this.lblTenderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenderTitle.Location = new System.Drawing.Point(10, 25);
            this.lblTenderTitle.Name = "lblTenderTitle";
            this.lblTenderTitle.Size = new System.Drawing.Size(126, 28);
            this.lblTenderTitle.TabIndex = 0;
            this.lblTenderTitle.Text = "Tender Title";
            // 
            // topButtonsPanel
            // 
            this.topButtonsPanel.ColumnCount = 3;
            this.topButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.topButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.topButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.topButtonsPanel.Controls.Add(this.btnCreateTender, 0, 0);
            this.topButtonsPanel.Controls.Add(this.btnAcceptProposal, 1, 0);
            this.topButtonsPanel.Controls.Add(this.btnRejectProposal, 2, 0);
            this.topButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.topButtonsPanel.Name = "topButtonsPanel";
            this.topButtonsPanel.RowCount = 1;
            this.topButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topButtonsPanel.Size = new System.Drawing.Size(984, 50);
            this.topButtonsPanel.TabIndex = 2;
            // 
            // btnCreateTender
            // 
            this.btnCreateTender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCreateTender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreateTender.FlatAppearance.BorderSize = 0;
            this.btnCreateTender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTender.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCreateTender.ForeColor = System.Drawing.Color.White;
            this.btnCreateTender.Location = new System.Drawing.Point(3, 3);
            this.btnCreateTender.Name = "btnCreateTender";
            this.btnCreateTender.Size = new System.Drawing.Size(322, 44);
            this.btnCreateTender.TabIndex = 0;
            this.btnCreateTender.Text = "Advertise New Tender";
            this.btnCreateTender.UseVisualStyleBackColor = false;
            this.btnCreateTender.Click += new System.EventHandler(this.btnCreateTender_Click);
            // 
            // btnAcceptProposal
            // 
            this.btnAcceptProposal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAcceptProposal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAcceptProposal.FlatAppearance.BorderSize = 0;
            this.btnAcceptProposal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcceptProposal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAcceptProposal.ForeColor = System.Drawing.Color.White;
            this.btnAcceptProposal.Location = new System.Drawing.Point(331, 3);
            this.btnAcceptProposal.Name = "btnAcceptProposal";
            this.btnAcceptProposal.Size = new System.Drawing.Size(322, 44);
            this.btnAcceptProposal.TabIndex = 1;
            this.btnAcceptProposal.Text = "Accept Selected Proposal";
            this.btnAcceptProposal.UseVisualStyleBackColor = false;
            this.btnAcceptProposal.Click += new System.EventHandler(this.btnAcceptProposal_Click);
            // 
            // btnRejectProposal
            // 
            this.btnRejectProposal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRejectProposal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRejectProposal.FlatAppearance.BorderSize = 0;
            this.btnRejectProposal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejectProposal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRejectProposal.ForeColor = System.Drawing.Color.White;
            this.btnRejectProposal.Location = new System.Drawing.Point(659, 3);
            this.btnRejectProposal.Name = "btnRejectProposal";
            this.btnRejectProposal.Size = new System.Drawing.Size(322, 44);
            this.btnRejectProposal.TabIndex = 2;
            this.btnRejectProposal.Text = "Reject Selected Proposal";
            this.btnRejectProposal.UseVisualStyleBackColor = false;
            this.btnRejectProposal.Click += new System.EventHandler(this.btnRejectProposal_Click);
            // 
            // TenderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(984, 593);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.topButtonsPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "TenderManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tender & Proposal Management";
            this.Load += new System.EventHandler(this.TenderManagementForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProposals)).EndInit();
            this.gbTenderDetails.ResumeLayout(false);
            this.gbTenderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTenderItems)).EndInit();
            this.topButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
