using System.Drawing;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem supplierToolStripMenuItem;
        private ToolStripMenuItem inventoryToolStripMenuItem;
        private ToolStripMenuItem ordersToolStripMenuItem;
        private ToolStripMenuItem pharmacyToolStripMenuItem;
        private ToolStripMenuItem tenderToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblUser;
        private ToolStripStatusLabel lblDateTime;
        private Panel mainPanel;
        private Label lblWelcome;
        private PictureBox pictureBox1;
        private Panel pnlQuickActions;
        private Button btnQuickSupplier;
        private Button btnQuickInventory;
        private Button btnQuickOrders;
        private Button btnQuickPharmacy; // New
        private Button btnQuickTender;   // New
        private Button btnQuickReports;
        private Timer timerDateTime;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pharmacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.pnlQuickActions = new System.Windows.Forms.Panel();
            this.btnQuickReports = new System.Windows.Forms.Button();
            this.btnQuickTender = new System.Windows.Forms.Button();
            this.btnQuickPharmacy = new System.Windows.Forms.Button();
            this.btnQuickOrders = new System.Windows.Forms.Button();
            this.btnQuickInventory = new System.Windows.Forms.Button();
            this.btnQuickSupplier = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.pnlQuickActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.supplierToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.pharmacyToolStripMenuItem,
            this.tenderToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "&System";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logoutToolStripMenuItem.Text = "&Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // supplierToolStripMenuItem
            // 
            this.supplierToolStripMenuItem.Name = "supplierToolStripMenuItem";
            this.supplierToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.supplierToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.supplierToolStripMenuItem.Text = "&Supplier Management";
            this.supplierToolStripMenuItem.Click += new System.EventHandler(this.supplierToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.inventoryToolStripMenuItem.Text = "&Inventory Management";
            this.inventoryToolStripMenuItem.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.ordersToolStripMenuItem.Text = "&Order Management";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // pharmacyToolStripMenuItem
            // 
            this.pharmacyToolStripMenuItem.Name = "pharmacyToolStripMenuItem";
            this.pharmacyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pharmacyToolStripMenuItem.Size = new System.Drawing.Size(146, 20);
            this.pharmacyToolStripMenuItem.Text = "&Pharmacy Management";
            this.pharmacyToolStripMenuItem.Click += new System.EventHandler(this.pharmacyToolStripMenuItem_Click);
            // 
            // tenderToolStripMenuItem
            // 
            this.tenderToolStripMenuItem.Name = "tenderToolStripMenuItem";
            this.tenderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tenderToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.tenderToolStripMenuItem.Text = "&Tender Management";
            this.tenderToolStripMenuItem.Click += new System.EventHandler(this.tenderToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "&Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblUser,
            this.lblDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 547);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(761, 17);
            this.lblUser.Spring = true;
            this.lblUser.Text = "User: ";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(58, 17);
            this.lblDateTime.Text = "DateTime";
            this.lblDateTime.Click += new System.EventHandler(this.lblDateTime_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.mainPanel.Controls.Add(this.pnlQuickActions);
            this.mainPanel.Controls.Add(this.lblWelcome);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(900, 523);
            this.mainPanel.TabIndex = 1;
            // 
            // pnlQuickActions
            // 
            this.pnlQuickActions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlQuickActions.BackColor = System.Drawing.Color.White;
            this.pnlQuickActions.Controls.Add(this.btnQuickReports);
            this.pnlQuickActions.Controls.Add(this.btnQuickTender);
            this.pnlQuickActions.Controls.Add(this.btnQuickPharmacy);
            this.pnlQuickActions.Controls.Add(this.btnQuickOrders);
            this.pnlQuickActions.Controls.Add(this.btnQuickInventory);
            this.pnlQuickActions.Controls.Add(this.btnQuickSupplier);
            this.pnlQuickActions.Location = new System.Drawing.Point(147, 201);
            this.pnlQuickActions.Name = "pnlQuickActions";
            this.pnlQuickActions.Size = new System.Drawing.Size(600, 162);
            this.pnlQuickActions.TabIndex = 2;
            // 
            // btnQuickReports
            // 
            this.btnQuickReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnQuickReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickReports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickReports.ForeColor = System.Drawing.Color.White;
            this.btnQuickReports.Location = new System.Drawing.Point(412, 89);
            this.btnQuickReports.Name = "btnQuickReports";
            this.btnQuickReports.Size = new System.Drawing.Size(150, 49);
            this.btnQuickReports.TabIndex = 5;
            this.btnQuickReports.Text = "Reports";
            this.btnQuickReports.UseVisualStyleBackColor = false;
            this.btnQuickReports.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // btnQuickTender
            // 
            this.btnQuickTender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnQuickTender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickTender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickTender.ForeColor = System.Drawing.Color.White;
            this.btnQuickTender.Location = new System.Drawing.Point(38, 89);
            this.btnQuickTender.Name = "btnQuickTender";
            this.btnQuickTender.Size = new System.Drawing.Size(150, 49);
            this.btnQuickTender.TabIndex = 4;
            this.btnQuickTender.Text = "Tender Mgt";
            this.btnQuickTender.UseVisualStyleBackColor = false;
            this.btnQuickTender.Click += new System.EventHandler(this.tenderToolStripMenuItem_Click);
            // 
            // btnQuickPharmacy
            // 
            this.btnQuickPharmacy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnQuickPharmacy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickPharmacy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickPharmacy.ForeColor = System.Drawing.Color.White;
            this.btnQuickPharmacy.Location = new System.Drawing.Point(225, 89);
            this.btnQuickPharmacy.Name = "btnQuickPharmacy";
            this.btnQuickPharmacy.Size = new System.Drawing.Size(150, 49);
            this.btnQuickPharmacy.TabIndex = 3;
            this.btnQuickPharmacy.Text = "Pharmacy Mgt";
            this.btnQuickPharmacy.UseVisualStyleBackColor = false;
            this.btnQuickPharmacy.Click += new System.EventHandler(this.pharmacyToolStripMenuItem_Click);
            // 
            // btnQuickOrders
            // 
            this.btnQuickOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnQuickOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickOrders.ForeColor = System.Drawing.Color.White;
            this.btnQuickOrders.Location = new System.Drawing.Point(412, 24);
            this.btnQuickOrders.Name = "btnQuickOrders";
            this.btnQuickOrders.Size = new System.Drawing.Size(150, 49);
            this.btnQuickOrders.TabIndex = 2;
            this.btnQuickOrders.Text = "Order Mgt";
            this.btnQuickOrders.UseVisualStyleBackColor = false;
            this.btnQuickOrders.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // btnQuickInventory
            // 
            this.btnQuickInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnQuickInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickInventory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickInventory.ForeColor = System.Drawing.Color.White;
            this.btnQuickInventory.Location = new System.Drawing.Point(225, 24);
            this.btnQuickInventory.Name = "btnQuickInventory";
            this.btnQuickInventory.Size = new System.Drawing.Size(150, 49);
            this.btnQuickInventory.TabIndex = 1;
            this.btnQuickInventory.Text = "Inventory Mgt";
            this.btnQuickInventory.UseVisualStyleBackColor = false;
            this.btnQuickInventory.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // btnQuickSupplier
            // 
            this.btnQuickSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnQuickSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickSupplier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickSupplier.ForeColor = System.Drawing.Color.White;
            this.btnQuickSupplier.Location = new System.Drawing.Point(38, 24);
            this.btnQuickSupplier.Name = "btnQuickSupplier";
            this.btnQuickSupplier.Size = new System.Drawing.Size(150, 49);
            this.btnQuickSupplier.TabIndex = 0;
            this.btnQuickSupplier.Text = "Supplier Mgt";
            this.btnQuickSupplier.UseVisualStyleBackColor = false;
            this.btnQuickSupplier.Click += new System.EventHandler(this.supplierToolStripMenuItem_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblWelcome.Location = new System.Drawing.Point(92, 120);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(716, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome to State Pharmaceutical Cooperation";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pictureBox1.Location = new System.Drawing.Point(412, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 65);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 569);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPC Pharmacy Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.pnlQuickActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}