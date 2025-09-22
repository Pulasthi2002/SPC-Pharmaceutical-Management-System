using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMain;
        private Panel pnlLogin;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private CheckBox chkShowPassword;
        private PictureBox picLogo;
        private Label lblVersion;

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
            this.pnlMain = new Panel();
            this.pnlLogin = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.chkShowPassword = new CheckBox();
            this.picLogo = new PictureBox();
            this.lblVersion = new Label();
            this.pnlMain.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.BackColor = Color.FromArgb(41, 128, 185);
            this.pnlMain.Controls.Add(this.pnlLogin);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Location = new Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(900, 600);
            this.pnlMain.TabIndex = 0;

            // pnlLogin
            this.pnlLogin.BackColor = Color.White;
            this.pnlLogin.Controls.Add(this.lblVersion);
            this.pnlLogin.Controls.Add(this.chkShowPassword);
            this.pnlLogin.Controls.Add(this.picLogo);
            this.pnlLogin.Controls.Add(this.btnExit);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.txtUsername);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.lblUsername);
            this.pnlLogin.Controls.Add(this.lblSubtitle);
            this.pnlLogin.Controls.Add(this.lblTitle);
            this.pnlLogin.Location = new Point(225, 100);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new Size(450, 400);
            this.pnlLogin.TabIndex = 0;
            this.pnlLogin.Paint += new PaintEventHandler(this.pnlLogin_Paint);

            // picLogo
            this.picLogo.BackColor = Color.FromArgb(52, 152, 219);
            this.picLogo.Location = new Point(200, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(50, 50);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.Paint += new PaintEventHandler(this.picLogo_Paint);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblTitle.Location = new Point(75, 85);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(300, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "SPC Management System";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblSubtitle.Location = new Point(125, 125);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(200, 19);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "State Pharmaceutical Cooperation";
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblUsername.Location = new Point(75, 170);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(71, 19);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new Point(75, 195);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(300, 27);
            this.txtUsername.TabIndex = 4;
            this.txtUsername.Text = "admin";
            this.txtUsername.KeyPress += new KeyPressEventHandler(this.txtUsername_KeyPress);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblPassword.Location = new Point(75, 235);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(70, 19);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new Point(75, 260);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(300, 27);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "admin123";
            this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtPassword_KeyPress);

            // chkShowPassword
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPassword.ForeColor = Color.FromArgb(127, 140, 141);
            this.chkShowPassword.Location = new Point(75, 295);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new Size(108, 19);
            this.chkShowPassword.TabIndex = 7;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new EventHandler(this.chkShowPassword_CheckedChanged);

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(46, 204, 113);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(225, 330);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(80, 35);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new EventHandler(this.btnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new EventHandler(this.btnLogin_MouseLeave);

            // btnExit
            this.btnExit.BackColor = Color.FromArgb(231, 76, 60);
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = Color.White;
            this.btnExit.Location = new Point(315, 330);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(60, 35);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new EventHandler(this.btnExit_MouseEnter);
            this.btnExit.MouseLeave += new EventHandler(this.btnExit_MouseLeave);

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblVersion.Location = new Point(350, 375);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(85, 13);
            this.lblVersion.TabIndex = 10;
            this.lblVersion.Text = "Version 1.0.0";

            // LoginForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SPC Login";
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.LoginForm_KeyDown);
            this.Load += new EventHandler(this.LoginForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
