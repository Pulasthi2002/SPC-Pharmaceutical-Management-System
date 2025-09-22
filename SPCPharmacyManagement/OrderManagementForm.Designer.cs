using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    partial class OrderManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvOrders;
        private DataGridView dgvOrderItems;
        private GroupBox gbOrderDetails;
        private GroupBox gbNewOrder;
        private ComboBox cmbPharmacy;
        private ComboBox cmbDrug;
        private NumericUpDown nudQuantity;
        private TextBox txtOrderNotes;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnCreateOrder;
        private Button btnUpdateOrder;
        private Button btnClearOrder;
        private Label lblTotalAmount;
        private TextBox txtSearch;
        private Button btnSearch;
        private ComboBox cmbStatusFilter;
        private Label lblPharmacy;
        private Label lblDrug;
        private Label lblQuantity;
        private Label lblOrderNotes;
        private Label lblSearch;
        private Label lblStatusFilter;
        private Label lblRecordCount;
        private ToolTip toolTip1;
        private Button btnUpdateStatus;

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
            this.components = new System.ComponentModel.Container();
            this.dgvOrders = new DataGridView();
            this.dgvOrderItems = new DataGridView();
            this.gbOrderDetails = new GroupBox();
            this.gbNewOrder = new GroupBox();
            this.cmbPharmacy = new ComboBox();
            this.cmbDrug = new ComboBox();
            this.nudQuantity = new NumericUpDown();
            this.txtOrderNotes = new TextBox();
            this.btnAddItem = new Button();
            this.btnRemoveItem = new Button();
            this.btnCreateOrder = new Button();
            this.btnUpdateOrder = new Button();
            this.btnClearOrder = new Button();
            this.lblTotalAmount = new Label();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.cmbStatusFilter = new ComboBox();
            this.lblPharmacy = new Label();
            this.lblDrug = new Label();
            this.lblQuantity = new Label();
            this.lblOrderNotes = new Label();
            this.lblSearch = new Label();
            this.lblStatusFilter = new Label();
            this.lblRecordCount = new Label();
            this.toolTip1 = new ToolTip(this.components);
            this.btnUpdateStatus = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.gbOrderDetails.SuspendLayout();
            this.gbNewOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();

            // Search controls
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new Font("Segoe UI", 9F);
            this.lblSearch.Location = new Point(12, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(130, 15);
            this.lblSearch.Text = "Search by Pharmacy/ID:";

            this.txtSearch.Font = new Font("Segoe UI", 9F);
            this.txtSearch.Location = new Point(150, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(200, 23);

            this.btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(360, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(75, 27);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            // Status filter
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new Font("Segoe UI", 9F);
            this.lblStatusFilter.Location = new Point(450, 15);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new Size(42, 15);
            this.lblStatusFilter.Text = "Status:";

            this.cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new Font("Segoe UI", 9F);
            this.cmbStatusFilter.Items.AddRange(new string[] { "All", "PENDING", "PROCESSING", "SHIPPED", "DELIVERED", "CANCELLED" });
            this.cmbStatusFilter.Location = new Point(500, 12);
            this.cmbStatusFilter.Size = new Size(120, 23);
            this.cmbStatusFilter.SelectedIndex = 0;
            this.cmbStatusFilter.SelectedIndexChanged += new EventHandler(this.cmbStatusFilter_SelectedIndexChanged);

            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblRecordCount.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblRecordCount.Location = new Point(780, 15);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new Size(100, 15);
            this.lblRecordCount.Text = "Records: 0";

            // dgvOrders
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.BackgroundColor = Color.White;
            this.dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new Point(12, 50);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersWidth = 51;
            this.dgvOrders.RowTemplate.Height = 24;
            this.dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new Size(800, 250);
            this.dgvOrders.TabIndex = 4;
            this.dgvOrders.SelectionChanged += new EventHandler(this.dgvOrders_SelectionChanged);

            // gbOrderDetails
            this.gbOrderDetails.Controls.Add(this.dgvOrderItems);
            this.gbOrderDetails.Controls.Add(this.lblTotalAmount);
            this.gbOrderDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbOrderDetails.ForeColor = Color.FromArgb(44, 62, 80);
            this.gbOrderDetails.Location = new Point(830, 50);
            this.gbOrderDetails.Name = "gbOrderDetails";
            this.gbOrderDetails.Size = new Size(400, 250);
            this.gbOrderDetails.TabStop = false;
            this.gbOrderDetails.Text = "Order Items";

            // dgvOrderItems
            this.dgvOrderItems.AllowUserToAddRows = false;
            this.dgvOrderItems.AllowUserToDeleteRows = false;
            this.dgvOrderItems.BackgroundColor = Color.White;
            this.dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderItems.Location = new Point(10, 25);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.ReadOnly = true;
            this.dgvOrderItems.RowHeadersWidth = 51;
            this.dgvOrderItems.RowTemplate.Height = 24;
            this.dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderItems.Size = new Size(380, 180);

            // lblTotalAmount
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalAmount.Location = new Point(10, 215);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new Size(120, 19);
            this.lblTotalAmount.Text = "Total Amount: LKR 0.00";

            // gbNewOrder
            this.gbNewOrder.Controls.Add(this.lblPharmacy);
            this.gbNewOrder.Controls.Add(this.cmbPharmacy);
            this.gbNewOrder.Controls.Add(this.lblDrug);
            this.gbNewOrder.Controls.Add(this.cmbDrug);
            this.gbNewOrder.Controls.Add(this.lblQuantity);
            this.gbNewOrder.Controls.Add(this.nudQuantity);
            this.gbNewOrder.Controls.Add(this.btnAddItem);
            this.gbNewOrder.Controls.Add(this.btnRemoveItem);
            this.gbNewOrder.Controls.Add(this.lblOrderNotes);
            this.gbNewOrder.Controls.Add(this.txtOrderNotes);
            this.gbNewOrder.Controls.Add(this.btnCreateOrder);
            this.gbNewOrder.Controls.Add(this.btnUpdateOrder);
            this.gbNewOrder.Controls.Add(this.btnClearOrder);
            this.gbNewOrder.Controls.Add(this.btnUpdateStatus);
            this.gbNewOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbNewOrder.ForeColor = Color.FromArgb(44, 62, 80);
            this.gbNewOrder.Location = new Point(12, 320);
            this.gbNewOrder.Name = "gbNewOrder";
            this.gbNewOrder.Size = new Size(1218, 200);
            this.gbNewOrder.TabStop = false;
            this.gbNewOrder.Text = "Create / Edit Order";

            // Controls inside gbNewOrder
            this.lblPharmacy.AutoSize = true;
            this.lblPharmacy.Font = new Font("Segoe UI", 9F);
            this.lblPharmacy.Location = new Point(20, 30);
            this.lblPharmacy.Text = "Pharmacy:";

            this.cmbPharmacy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbPharmacy.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbPharmacy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPharmacy.Font = new Font("Segoe UI", 9F);
            this.cmbPharmacy.Location = new Point(100, 27);
            this.cmbPharmacy.Size = new Size(200, 23);

            this.lblDrug.AutoSize = true;
            this.lblDrug.Font = new Font("Segoe UI", 9F);
            this.lblDrug.Location = new Point(320, 30);
            this.lblDrug.Text = "Drug:";

            this.cmbDrug.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cmbDrug.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cmbDrug.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDrug.Font = new Font("Segoe UI", 9F);
            this.cmbDrug.Location = new Point(370, 27);
            this.cmbDrug.Size = new Size(200, 23);

            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new Font("Segoe UI", 9F);
            this.lblQuantity.Location = new Point(590, 30);
            this.lblQuantity.Text = "Quantity:";

            this.nudQuantity.Font = new Font("Segoe UI", 9F);
            this.nudQuantity.Location = new Point(660, 27);
            this.nudQuantity.Minimum = 1;
            this.nudQuantity.Maximum = 9999;
            this.nudQuantity.Value = 1;
            this.nudQuantity.Size = new Size(80, 23);

            this.btnAddItem.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAddItem.FlatStyle = FlatStyle.Flat;
            this.btnAddItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAddItem.ForeColor = Color.White;
            this.btnAddItem.Location = new Point(760, 25);
            this.btnAddItem.Size = new Size(80, 27);
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Click += new EventHandler(this.btnAddItem_Click);

            this.btnRemoveItem.BackColor = Color.FromArgb(231, 76, 60);
            this.btnRemoveItem.FlatStyle = FlatStyle.Flat;
            this.btnRemoveItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRemoveItem.ForeColor = Color.White;
            this.btnRemoveItem.Location = new Point(860, 25);
            this.btnRemoveItem.Size = new Size(100, 27);
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.Click += new EventHandler(this.btnRemoveItem_Click);

            this.lblOrderNotes.AutoSize = true;
            this.lblOrderNotes.Font = new Font("Segoe UI", 9F);
            this.lblOrderNotes.Location = new Point(20, 70);
            this.lblOrderNotes.Text = "Order Notes:";

            this.txtOrderNotes.Font = new Font("Segoe UI", 9F);
            this.txtOrderNotes.Location = new Point(110, 67);
            this.txtOrderNotes.Multiline = true;
            this.txtOrderNotes.ScrollBars = ScrollBars.Vertical;
            this.txtOrderNotes.Size = new Size(400, 60);

            // Action buttons
            this.btnCreateOrder.BackColor = Color.FromArgb(46, 204, 113);
            this.btnCreateOrder.FlatStyle = FlatStyle.Flat;
            this.btnCreateOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCreateOrder.ForeColor = Color.White;
            this.btnCreateOrder.Location = new Point(530, 100);
            this.btnCreateOrder.Size = new Size(120, 35);
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.Click += new EventHandler(this.btnCreateOrder_Click);

            this.btnUpdateOrder.BackColor = Color.FromArgb(52, 152, 219);
            this.btnUpdateOrder.FlatStyle = FlatStyle.Flat;
            this.btnUpdateOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnUpdateOrder.ForeColor = Color.White;
            this.btnUpdateOrder.Location = new Point(670, 100);
            this.btnUpdateOrder.Size = new Size(120, 35);
            this.btnUpdateOrder.Text = "Update Order";
            this.btnUpdateOrder.Click += new EventHandler(this.btnUpdateOrder_Click);

            this.btnUpdateStatus.BackColor = Color.FromArgb(243, 156, 18);
            this.btnUpdateStatus.FlatStyle = FlatStyle.Flat;
            this.btnUpdateStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnUpdateStatus.ForeColor = Color.White;
            this.btnUpdateStatus.Location = new Point(810, 100);
            this.btnUpdateStatus.Size = new Size(120, 35);
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.Click += new EventHandler(this.btnUpdateStatus_Click);

            this.btnClearOrder.BackColor = Color.FromArgb(149, 165, 166);
            this.btnClearOrder.FlatStyle = FlatStyle.Flat;
            this.btnClearOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClearOrder.ForeColor = Color.White;
            this.btnClearOrder.Location = new Point(950, 100);
            this.btnClearOrder.Size = new Size(120, 35);
            this.btnClearOrder.Text = "Clear / New";
            this.btnClearOrder.Click += new EventHandler(this.btnClearOrder_Click);

            // OrderManagementForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(1240, 540);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblStatusFilter);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.gbOrderDetails);
            this.Controls.Add(this.gbNewOrder);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OrderManagementForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Order Management - SPC Pharmacy System";
            this.Load += new EventHandler(this.OrderManagementForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.gbOrderDetails.ResumeLayout(false);
            this.gbOrderDetails.PerformLayout();
            this.gbNewOrder.ResumeLayout(false);
            this.gbNewOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
