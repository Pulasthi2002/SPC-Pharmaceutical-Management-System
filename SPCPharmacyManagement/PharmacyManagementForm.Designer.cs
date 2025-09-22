namespace SPCPharmacyManagement
{
    partial class PharmacyManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPharmacies = new System.Windows.Forms.DataGridView();
            this.gbPharmacyDetails = new System.Windows.Forms.GroupBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblIdNumber = new System.Windows.Forms.Label();
            this.txtIdNumber = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.cmbPharmacyType = new System.Windows.Forms.ComboBox();
            this.lblPharmacyType = new System.Windows.Forms.Label();
            this.txtLicenseNumber = new System.Windows.Forms.TextBox();
            this.lblLicenseNumber = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtPharmacyName = new System.Windows.Forms.TextBox();
            this.lblPharmacyName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPharmacies)).BeginInit();
            this.gbPharmacyDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPharmacies
            // 
            this.dgvPharmacies.AllowUserToAddRows = false;
            this.dgvPharmacies.AllowUserToDeleteRows = false;
            this.dgvPharmacies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPharmacies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPharmacies.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPharmacies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPharmacies.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPharmacies.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPharmacies.EnableHeadersVisualStyles = false;
            this.dgvPharmacies.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvPharmacies.Location = new System.Drawing.Point(12, 12);
            this.dgvPharmacies.MultiSelect = false;
            this.dgvPharmacies.Name = "dgvPharmacies";
            this.dgvPharmacies.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPharmacies.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPharmacies.RowTemplate.Height = 30;
            this.dgvPharmacies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPharmacies.Size = new System.Drawing.Size(960, 312);
            this.dgvPharmacies.TabIndex = 0;
            this.dgvPharmacies.SelectionChanged += new System.EventHandler(this.dgvPharmacies_SelectionChanged);
            // 
            // gbPharmacyDetails
            // 
            this.gbPharmacyDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPharmacyDetails.Controls.Add(this.lblPassword);
            this.gbPharmacyDetails.Controls.Add(this.txtPassword);
            this.gbPharmacyDetails.Controls.Add(this.lblIdNumber);
            this.gbPharmacyDetails.Controls.Add(this.txtIdNumber);
            this.gbPharmacyDetails.Controls.Add(this.btnClear);
            this.gbPharmacyDetails.Controls.Add(this.btnUpdate);
            this.gbPharmacyDetails.Controls.Add(this.btnAdd);
            this.gbPharmacyDetails.Controls.Add(this.chkIsActive);
            this.gbPharmacyDetails.Controls.Add(this.cmbPharmacyType);
            this.gbPharmacyDetails.Controls.Add(this.lblPharmacyType);
            this.gbPharmacyDetails.Controls.Add(this.txtLicenseNumber);
            this.gbPharmacyDetails.Controls.Add(this.lblLicenseNumber);
            this.gbPharmacyDetails.Controls.Add(this.txtAddress);
            this.gbPharmacyDetails.Controls.Add(this.lblAddress);
            this.gbPharmacyDetails.Controls.Add(this.txtEmail);
            this.gbPharmacyDetails.Controls.Add(this.lblEmail);
            this.gbPharmacyDetails.Controls.Add(this.txtPhone);
            this.gbPharmacyDetails.Controls.Add(this.lblPhone);
            this.gbPharmacyDetails.Controls.Add(this.txtContactPerson);
            this.gbPharmacyDetails.Controls.Add(this.lblContactPerson);
            this.gbPharmacyDetails.Controls.Add(this.txtPharmacyName);
            this.gbPharmacyDetails.Controls.Add(this.lblPharmacyName);
            this.gbPharmacyDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbPharmacyDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.gbPharmacyDetails.Location = new System.Drawing.Point(12, 330);
            this.gbPharmacyDetails.Name = "gbPharmacyDetails";
            this.gbPharmacyDetails.Size = new System.Drawing.Size(960, 250);
            this.gbPharmacyDetails.TabIndex = 1;
            this.gbPharmacyDetails.TabStop = false;
            this.gbPharmacyDetails.Text = "Pharmacy Details";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPassword.Location = new System.Drawing.Point(680, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(60, 15);
            this.lblPassword.TabIndex = 22;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(780, 107);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(160, 23);
            this.txtPassword.TabIndex = 8;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIdNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblIdNumber.Location = new System.Drawing.Point(680, 75);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(68, 15);
            this.lblIdNumber.TabIndex = 20;
            this.lblIdNumber.Text = "ID Number:";
            // 
            // txtIdNumber
            // 
            this.txtIdNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIdNumber.Location = new System.Drawing.Point(780, 72);
            this.txtIdNumber.Name = "txtIdNumber";
            this.txtIdNumber.Size = new System.Drawing.Size(160, 23);
            this.txtIdNumber.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(370, 190);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(250, 190);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(130, 190);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.chkIsActive.Location = new System.Drawing.Point(460, 145);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(70, 19);
            this.chkIsActive.TabIndex = 9;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // cmbPharmacyType
            // 
            this.cmbPharmacyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPharmacyType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPharmacyType.FormattingEnabled = true;
            this.cmbPharmacyType.Items.AddRange(new object[] {
            "SPC_OWNED",
            "LINKED_DEALER"});
            this.cmbPharmacyType.Location = new System.Drawing.Point(130, 142);
            this.cmbPharmacyType.Name = "cmbPharmacyType";
            this.cmbPharmacyType.Size = new System.Drawing.Size(200, 23);
            this.cmbPharmacyType.TabIndex = 3;
            // 
            // lblPharmacyType
            // 
            this.lblPharmacyType.AutoSize = true;
            this.lblPharmacyType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPharmacyType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPharmacyType.Location = new System.Drawing.Point(20, 145);
            this.lblPharmacyType.Name = "lblPharmacyType";
            this.lblPharmacyType.Size = new System.Drawing.Size(89, 15);
            this.lblPharmacyType.TabIndex = 10;
            this.lblPharmacyType.Text = "Pharmacy Type:";
            // 
            // txtLicenseNumber
            // 
            this.txtLicenseNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLicenseNumber.Location = new System.Drawing.Point(780, 37);
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new System.Drawing.Size(160, 23);
            this.txtLicenseNumber.TabIndex = 6;
            // 
            // lblLicenseNumber
            // 
            this.lblLicenseNumber.AutoSize = true;
            this.lblLicenseNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLicenseNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLicenseNumber.Location = new System.Drawing.Point(680, 40);
            this.lblLicenseNumber.Name = "lblLicenseNumber";
            this.lblLicenseNumber.Size = new System.Drawing.Size(94, 15);
            this.lblLicenseNumber.TabIndex = 8;
            this.lblLicenseNumber.Text = "License Number:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAddress.Location = new System.Drawing.Point(460, 37);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(200, 58);
            this.txtAddress.TabIndex = 4;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAddress.Location = new System.Drawing.Point(350, 40);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(52, 15);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(130, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEmail.Location = new System.Drawing.Point(20, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Location = new System.Drawing.Point(460, 107);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 23);
            this.txtPhone.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPhone.Location = new System.Drawing.Point(350, 110);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(44, 15);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Phone:";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContactPerson.Location = new System.Drawing.Point(130, 72);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(200, 23);
            this.txtContactPerson.TabIndex = 1;
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblContactPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContactPerson.Location = new System.Drawing.Point(20, 75);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(87, 15);
            this.lblContactPerson.TabIndex = 0;
            this.lblContactPerson.Text = "Contact Person:";
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPharmacyName.Location = new System.Drawing.Point(130, 37);
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Size = new System.Drawing.Size(200, 23);
            this.txtPharmacyName.TabIndex = 0;
            // 
            // lblPharmacyName
            // 
            this.lblPharmacyName.AutoSize = true;
            this.lblPharmacyName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPharmacyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPharmacyName.Location = new System.Drawing.Point(20, 40);
            this.lblPharmacyName.Name = "lblPharmacyName";
            this.lblPharmacyName.Size = new System.Drawing.Size(98, 15);
            this.lblPharmacyName.TabIndex = 0;
            this.lblPharmacyName.Text = "Pharmacy Name:";
            // 
            // PharmacyManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(984, 592);
            this.Controls.Add(this.gbPharmacyDetails);
            this.Controls.Add(this.dgvPharmacies);
            this.MaximizeBox = false;
            this.Name = "PharmacyManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pharmacy Management";
            this.Load += new System.EventHandler(this.PharmacyManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPharmacies)).EndInit();
            this.gbPharmacyDetails.ResumeLayout(false);
            this.gbPharmacyDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPharmacies;
        private System.Windows.Forms.GroupBox gbPharmacyDetails;
        private System.Windows.Forms.TextBox txtPharmacyName;
        private System.Windows.Forms.Label lblPharmacyName;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtLicenseNumber;
        private System.Windows.Forms.Label lblLicenseNumber;
        private System.Windows.Forms.ComboBox cmbPharmacyType;
        private System.Windows.Forms.Label lblPharmacyType;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblIdNumber;
        private System.Windows.Forms.TextBox txtIdNumber;
    }
}
