using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    partial class InventoryManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvInventory;
        private GroupBox gbDrugDetails;
        private GroupBox gbStockUpdate;
        private GroupBox gbSearchFilter;
        private TextBox txtDrugName;
        private TextBox txtGenericName;
        private TextBox txtManufacturer;
        private TextBox txtBatchNumber;
        private DateTimePicker dtpExpiryDate;
        private NumericUpDown nudUnitPrice;
        private NumericUpDown nudQuantityInStock;
        private TextBox txtDescription;
        private ComboBox cmbUpdateType;
        private NumericUpDown nudUpdateQuantity;
        private TextBox txtUpdateReason;
        private TextBox txtUpdatedBy;
        private Button btnAddDrug;
        private Button btnUpdateDrug;
        private Button btnDeleteDrug;
        private Button btnUpdateStock;
        private Button btnClearDrug;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label lblDrugName;
        private Label lblGenericName;
        private Label lblManufacturer;
        private Label lblBatchNumber;
        private Label lblExpiryDate;
        private Label lblUnitPrice;
        private Label lblQuantity;
        private Label lblDescription;
        private Label lblUpdateType;
        private Label lblUpdateQuantity;
        private Label lblUpdateReason;
        private Label lblUpdatedBy;
        private Label lblSearch;
        private Label lblRecordCount;

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
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.gbDrugDetails = new System.Windows.Forms.GroupBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.txtDrugName = new System.Windows.Forms.TextBox();
            this.lblGenericName = new System.Windows.Forms.Label();
            this.txtGenericName = new System.Windows.Forms.TextBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.lblBatchNumber = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.nudUnitPrice = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantityInStock = new System.Windows.Forms.NumericUpDown();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAddDrug = new System.Windows.Forms.Button();
            this.btnUpdateDrug = new System.Windows.Forms.Button();
            this.btnDeleteDrug = new System.Windows.Forms.Button();
            this.btnClearDrug = new System.Windows.Forms.Button();
            this.gbStockUpdate = new System.Windows.Forms.GroupBox();
            this.lblUpdateType = new System.Windows.Forms.Label();
            this.cmbUpdateType = new System.Windows.Forms.ComboBox();
            this.lblUpdateQuantity = new System.Windows.Forms.Label();
            this.nudUpdateQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblUpdateReason = new System.Windows.Forms.Label();
            this.txtUpdateReason = new System.Windows.Forms.TextBox();
            this.lblUpdatedBy = new System.Windows.Forms.Label();
            this.txtUpdatedBy = new System.Windows.Forms.TextBox();
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.gbSearchFilter = new System.Windows.Forms.GroupBox();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.gbDrugDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantityInStock)).BeginInit();
            this.gbStockUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateQuantity)).BeginInit();
            this.gbSearchFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Location = new System.Drawing.Point(12, 100);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersWidth = 51;
            this.dgvInventory.RowTemplate.Height = 24;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1000, 300);
            this.dgvInventory.TabIndex = 1;
            this.dgvInventory.SelectionChanged += new System.EventHandler(this.dgvInventory_SelectionChanged);
            // 
            // gbDrugDetails
            // 
            this.gbDrugDetails.Controls.Add(this.lblDrugName);
            this.gbDrugDetails.Controls.Add(this.txtDrugName);
            this.gbDrugDetails.Controls.Add(this.lblGenericName);
            this.gbDrugDetails.Controls.Add(this.txtGenericName);
            this.gbDrugDetails.Controls.Add(this.lblManufacturer);
            this.gbDrugDetails.Controls.Add(this.txtManufacturer);
            this.gbDrugDetails.Controls.Add(this.lblBatchNumber);
            this.gbDrugDetails.Controls.Add(this.txtBatchNumber);
            this.gbDrugDetails.Controls.Add(this.lblExpiryDate);
            this.gbDrugDetails.Controls.Add(this.dtpExpiryDate);
            this.gbDrugDetails.Controls.Add(this.lblUnitPrice);
            this.gbDrugDetails.Controls.Add(this.nudUnitPrice);
            this.gbDrugDetails.Controls.Add(this.lblQuantity);
            this.gbDrugDetails.Controls.Add(this.nudQuantityInStock);
            this.gbDrugDetails.Controls.Add(this.lblDescription);
            this.gbDrugDetails.Controls.Add(this.txtDescription);
            this.gbDrugDetails.Controls.Add(this.btnAddDrug);
            this.gbDrugDetails.Controls.Add(this.btnUpdateDrug);
            this.gbDrugDetails.Controls.Add(this.btnDeleteDrug);
            this.gbDrugDetails.Controls.Add(this.btnClearDrug);
            this.gbDrugDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDrugDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.gbDrugDetails.Location = new System.Drawing.Point(12, 420);
            this.gbDrugDetails.Name = "gbDrugDetails";
            this.gbDrugDetails.Size = new System.Drawing.Size(600, 280);
            this.gbDrugDetails.TabIndex = 2;
            this.gbDrugDetails.TabStop = false;
            this.gbDrugDetails.Text = "Drug Details";
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDrugName.Location = new System.Drawing.Point(20, 30);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(71, 15);
            this.lblDrugName.TabIndex = 0;
            this.lblDrugName.Text = "Drug Name:";
            // 
            // txtDrugName
            // 
            this.txtDrugName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDrugName.Location = new System.Drawing.Point(120, 27);
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Size = new System.Drawing.Size(180, 23);
            this.txtDrugName.TabIndex = 1;
            // 
            // lblGenericName
            // 
            this.lblGenericName.AutoSize = true;
            this.lblGenericName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGenericName.Location = new System.Drawing.Point(320, 30);
            this.lblGenericName.Name = "lblGenericName";
            this.lblGenericName.Size = new System.Drawing.Size(85, 15);
            this.lblGenericName.TabIndex = 2;
            this.lblGenericName.Text = "Generic Name:";
            // 
            // txtGenericName
            // 
            this.txtGenericName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGenericName.Location = new System.Drawing.Point(410, 27);
            this.txtGenericName.Name = "txtGenericName";
            this.txtGenericName.Size = new System.Drawing.Size(180, 23);
            this.txtGenericName.TabIndex = 3;
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblManufacturer.Location = new System.Drawing.Point(20, 65);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(82, 15);
            this.lblManufacturer.TabIndex = 4;
            this.lblManufacturer.Text = "Manufacturer:";
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtManufacturer.Location = new System.Drawing.Point(120, 62);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(180, 23);
            this.txtManufacturer.TabIndex = 5;
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.AutoSize = true;
            this.lblBatchNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBatchNumber.Location = new System.Drawing.Point(320, 65);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(87, 15);
            this.lblBatchNumber.TabIndex = 6;
            this.lblBatchNumber.Text = "Batch Number:";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBatchNumber.Location = new System.Drawing.Point(410, 62);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(180, 23);
            this.txtBatchNumber.TabIndex = 7;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExpiryDate.Location = new System.Drawing.Point(20, 100);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(68, 15);
            this.lblExpiryDate.TabIndex = 8;
            this.lblExpiryDate.Text = "Expiry Date:";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpExpiryDate.Location = new System.Drawing.Point(120, 97);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(180, 23);
            this.dtpExpiryDate.TabIndex = 9;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUnitPrice.Location = new System.Drawing.Point(320, 100);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(61, 15);
            this.lblUnitPrice.TabIndex = 10;
            this.lblUnitPrice.Text = "Unit Price:";
            // 
            // nudUnitPrice
            // 
            this.nudUnitPrice.DecimalPlaces = 2;
            this.nudUnitPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudUnitPrice.Location = new System.Drawing.Point(410, 97);
            this.nudUnitPrice.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudUnitPrice.Name = "nudUnitPrice";
            this.nudUnitPrice.Size = new System.Drawing.Size(180, 23);
            this.nudUnitPrice.TabIndex = 11;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuantity.Location = new System.Drawing.Point(20, 135);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblQuantity.TabIndex = 12;
            this.lblQuantity.Text = "Quantity:";
            // 
            // nudQuantityInStock
            // 
            this.nudQuantityInStock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudQuantityInStock.Location = new System.Drawing.Point(120, 132);
            this.nudQuantityInStock.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudQuantityInStock.Name = "nudQuantityInStock";
            this.nudQuantityInStock.Size = new System.Drawing.Size(180, 23);
            this.nudQuantityInStock.TabIndex = 13;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.Location = new System.Drawing.Point(20, 170);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 15);
            this.lblDescription.TabIndex = 14;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescription.Location = new System.Drawing.Point(120, 167);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(470, 60);
            this.txtDescription.TabIndex = 15;
            // 
            // btnAddDrug
            // 
            this.btnAddDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDrug.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddDrug.ForeColor = System.Drawing.Color.White;
            this.btnAddDrug.Location = new System.Drawing.Point(120, 240);
            this.btnAddDrug.Name = "btnAddDrug";
            this.btnAddDrug.Size = new System.Drawing.Size(100, 35);
            this.btnAddDrug.TabIndex = 16;
            this.btnAddDrug.Text = "Add";
            this.btnAddDrug.UseVisualStyleBackColor = false;
            this.btnAddDrug.Click += new System.EventHandler(this.btnAddDrug_Click);
            // 
            // btnUpdateDrug
            // 
            this.btnUpdateDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUpdateDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDrug.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateDrug.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDrug.Location = new System.Drawing.Point(240, 240);
            this.btnUpdateDrug.Name = "btnUpdateDrug";
            this.btnUpdateDrug.Size = new System.Drawing.Size(100, 35);
            this.btnUpdateDrug.TabIndex = 17;
            this.btnUpdateDrug.Text = "Update";
            this.btnUpdateDrug.UseVisualStyleBackColor = false;
            this.btnUpdateDrug.Click += new System.EventHandler(this.btnUpdateDrug_Click);
            // 
            // btnDeleteDrug
            // 
            this.btnDeleteDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDrug.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteDrug.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDrug.Location = new System.Drawing.Point(360, 240);
            this.btnDeleteDrug.Name = "btnDeleteDrug";
            this.btnDeleteDrug.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteDrug.TabIndex = 18;
            this.btnDeleteDrug.Text = "Delete";
            this.btnDeleteDrug.UseVisualStyleBackColor = false;
            this.btnDeleteDrug.Click += new System.EventHandler(this.btnDeleteDrug_Click);
            // 
            // btnClearDrug
            // 
            this.btnClearDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClearDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDrug.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearDrug.ForeColor = System.Drawing.Color.White;
            this.btnClearDrug.Location = new System.Drawing.Point(480, 240);
            this.btnClearDrug.Name = "btnClearDrug";
            this.btnClearDrug.Size = new System.Drawing.Size(100, 35);
            this.btnClearDrug.TabIndex = 19;
            this.btnClearDrug.Text = "Clear";
            this.btnClearDrug.UseVisualStyleBackColor = false;
            this.btnClearDrug.Click += new System.EventHandler(this.btnClearDrug_Click);
            // 
            // gbStockUpdate
            // 
            this.gbStockUpdate.Controls.Add(this.lblUpdateType);
            this.gbStockUpdate.Controls.Add(this.cmbUpdateType);
            this.gbStockUpdate.Controls.Add(this.lblUpdateQuantity);
            this.gbStockUpdate.Controls.Add(this.nudUpdateQuantity);
            this.gbStockUpdate.Controls.Add(this.lblUpdateReason);
            this.gbStockUpdate.Controls.Add(this.txtUpdateReason);
            this.gbStockUpdate.Controls.Add(this.lblUpdatedBy);
            this.gbStockUpdate.Controls.Add(this.txtUpdatedBy);
            this.gbStockUpdate.Controls.Add(this.btnUpdateStock);
            this.gbStockUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbStockUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.gbStockUpdate.Location = new System.Drawing.Point(630, 420);
            this.gbStockUpdate.Name = "gbStockUpdate";
            this.gbStockUpdate.Size = new System.Drawing.Size(380, 280);
            this.gbStockUpdate.TabIndex = 3;
            this.gbStockUpdate.TabStop = false;
            this.gbStockUpdate.Text = "Stock Update (Manufacturing/Purchase)";
            // 
            // lblUpdateType
            // 
            this.lblUpdateType.AutoSize = true;
            this.lblUpdateType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdateType.Location = new System.Drawing.Point(20, 30);
            this.lblUpdateType.Name = "lblUpdateType";
            this.lblUpdateType.Size = new System.Drawing.Size(76, 15);
            this.lblUpdateType.TabIndex = 0;
            this.lblUpdateType.Text = "Update Type:";
            // 
            // cmbUpdateType
            // 
            this.cmbUpdateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpdateType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbUpdateType.Items.AddRange(new object[] {
            "ADD",
            "REMOVE",
            "ADJUSTMENT"});
            this.cmbUpdateType.Location = new System.Drawing.Point(120, 27);
            this.cmbUpdateType.Name = "cmbUpdateType";
            this.cmbUpdateType.Size = new System.Drawing.Size(120, 23);
            this.cmbUpdateType.TabIndex = 1;
            // 
            // lblUpdateQuantity
            // 
            this.lblUpdateQuantity.AutoSize = true;
            this.lblUpdateQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdateQuantity.Location = new System.Drawing.Point(20, 65);
            this.lblUpdateQuantity.Name = "lblUpdateQuantity";
            this.lblUpdateQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblUpdateQuantity.TabIndex = 2;
            this.lblUpdateQuantity.Text = "Quantity:";
            // 
            // nudUpdateQuantity
            // 
            this.nudUpdateQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudUpdateQuantity.Location = new System.Drawing.Point(120, 62);
            this.nudUpdateQuantity.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudUpdateQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudUpdateQuantity.Name = "nudUpdateQuantity";
            this.nudUpdateQuantity.Size = new System.Drawing.Size(120, 23);
            this.nudUpdateQuantity.TabIndex = 3;
            this.nudUpdateQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblUpdateReason
            // 
            this.lblUpdateReason.AutoSize = true;
            this.lblUpdateReason.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdateReason.Location = new System.Drawing.Point(20, 100);
            this.lblUpdateReason.Name = "lblUpdateReason";
            this.lblUpdateReason.Size = new System.Drawing.Size(48, 15);
            this.lblUpdateReason.TabIndex = 4;
            this.lblUpdateReason.Text = "Reason:";
            // 
            // txtUpdateReason
            // 
            this.txtUpdateReason.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUpdateReason.Location = new System.Drawing.Point(120, 97);
            this.txtUpdateReason.Multiline = true;
            this.txtUpdateReason.Name = "txtUpdateReason";
            this.txtUpdateReason.Size = new System.Drawing.Size(240, 60);
            this.txtUpdateReason.TabIndex = 5;
            // 
            // lblUpdatedBy
            // 
            this.lblUpdatedBy.AutoSize = true;
            this.lblUpdatedBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdatedBy.Location = new System.Drawing.Point(20, 175);
            this.lblUpdatedBy.Name = "lblUpdatedBy";
            this.lblUpdatedBy.Size = new System.Drawing.Size(71, 15);
            this.lblUpdatedBy.TabIndex = 6;
            this.lblUpdatedBy.Text = "Updated By:";
            // 
            // txtUpdatedBy
            // 
            this.txtUpdatedBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUpdatedBy.Location = new System.Drawing.Point(120, 172);
            this.txtUpdatedBy.Name = "txtUpdatedBy";
            this.txtUpdatedBy.Size = new System.Drawing.Size(240, 23);
            this.txtUpdatedBy.TabIndex = 7;
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnUpdateStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStock.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStock.Location = new System.Drawing.Point(120, 210);
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateStock.TabIndex = 8;
            this.btnUpdateStock.Text = "Update Stock";
            this.btnUpdateStock.UseVisualStyleBackColor = false;
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);
            // 
            // gbSearchFilter
            // 
            this.gbSearchFilter.Controls.Add(this.lblRecordCount);
            this.gbSearchFilter.Controls.Add(this.btnSearch);
            this.gbSearchFilter.Controls.Add(this.txtSearch);
            this.gbSearchFilter.Controls.Add(this.lblSearch);
            this.gbSearchFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbSearchFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.gbSearchFilter.Location = new System.Drawing.Point(12, 12);
            this.gbSearchFilter.Name = "gbSearchFilter";
            this.gbSearchFilter.Size = new System.Drawing.Size(1000, 80);
            this.gbSearchFilter.TabIndex = 0;
            this.gbSearchFilter.TabStop = false;
            this.gbSearchFilter.Text = "Search Inventory";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblRecordCount.Location = new System.Drawing.Point(880, 30);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(59, 15);
            this.lblRecordCount.TabIndex = 0;
            this.lblRecordCount.Text = "Records: 0";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(553, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(244, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSearch.Location = new System.Drawing.Point(20, 30);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(218, 15);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Search by Name/Generic/Manufacturer:";
            // 
            // InventoryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Controls.Add(this.gbSearchFilter);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.gbDrugDetails);
            this.Controls.Add(this.gbStockUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InventoryManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory Management - SPC Pharmacy System";
            this.Load += new System.EventHandler(this.InventoryManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.gbDrugDetails.ResumeLayout(false);
            this.gbDrugDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantityInStock)).EndInit();
            this.gbStockUpdate.ResumeLayout(false);
            this.gbStockUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateQuantity)).EndInit();
            this.gbSearchFilter.ResumeLayout(false);
            this.gbSearchFilter.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
