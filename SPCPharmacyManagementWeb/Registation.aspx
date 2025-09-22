<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registation.aspx.cs" Inherits="Spc_web.Registation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - SPC Pharmacy</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Google Fonts: Poppins -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    
    <!-- Bootstrap 5.3 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    
    <!-- Font Awesome 6.4 -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style>
        /* CSS Variables for theme consistency */
        :root {
            --primary-color: #007bff;
            --secondary-color: #4CAF50;
            --dark-color: #2c3e50;
            --light-color: #f8f9fa;
            --font-family: 'Poppins', sans-serif;
            --border-radius: 0.8rem;
            --box-shadow: 0 10px 35px rgba(0, 0, 0, 0.08);
        }

        body {
            font-family: var(--font-family);
            background-color: var(--light-color);
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            padding: 20px;
        }

        .register-container {
            display: grid;
            grid-template-columns: 45% 55%;
            max-width: 1100px;
            width: 100%;
            background-color: #fff;
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
            overflow: hidden;
            animation: fadeIn 0.8s ease-out;
        }

        /* --- Branding Panel (Left Side) --- */
        .branding-panel {
            padding: 50px;
            background: linear-gradient(160deg, var(--primary-color), var(--dark-color));
            color: #fff;
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
            position: relative;
        }

        .branding-panel::before {
             content: '';
            position: absolute;
            top: -50px;
            left: -80px;
            width: 200px;
            height: 200px;
            background: rgba(255,255,255,0.08);
            border-radius: 50%;
        }

        .branding-panel h1 {
            font-weight: 700;
            font-size: 2.2rem;
            z-index: 1;
        }
        .branding-panel p {
            font-weight: 300;
            font-size: 1.1rem;
            opacity: 0.9;
            z-index: 1;
        }
        .branding-panel .fa-clinic-medical {
            font-size: 4rem;
            margin-bottom: 1.5rem;
            opacity: 0.8;
            z-index: 1;
        }

        /* --- Form Panel (Right Side) --- */
        .form-panel {
            padding: 40px 50px;
        }

        .form-panel h2 {
            font-weight: 700;
            color: #333;
            margin-bottom: 10px;
        }

        .form-panel .sub-heading {
            color: #777;
            margin-bottom: 30px;
        }

        .input-group-text {
            background-color: #f0f2f5;
            border-right: none;
            color: #999;
        }

        .form-control, .form-select {
            border-left: none;
            padding-left: 0;
            transition: border-color 0.2s, box-shadow 0.2s;
        }
        .form-control:focus, .form-select:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(0, 123, 255, 0.15);
            border-left: 1px solid #dee2e6; /* Add back border on focus */
            padding-left: .75rem;
        }
        .form-control:focus + .input-group-text, .form-select:focus + .input-group-text {
            border-color: var(--primary-color);
        }

        .password-toggle {
            cursor: pointer;
        }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            padding: 12px;
            font-weight: 600;
            transition: background-color 0.3s, transform 0.2s;
        }
        .btn-primary:hover {
            background-color: #0069d9;
            transform: translateY(-2px);
        }

        /* --- Responsive Design --- */
        @media (max-width: 991.98px) {
            .register-container {
                grid-template-columns: 1fr;
            }
            .branding-panel {
                display: none; /* Hide branding panel on smaller screens for a focused form experience */
            }
            .form-panel {
                padding: 30px;
            }
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: scale(0.98); }
            to { opacity: 1; transform: scale(1); }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <!-- Branding Panel -->
            <div class="branding-panel">
                <i class="fas fa-clinic-medical"></i>
                <h1>Welcome to the Network</h1>
                <p>Join a community dedicated to advancing healthcare through technology and collaboration.</p>
            </div>

            <!-- Registration Form Panel -->
            <div class="form-panel">
                <h2>Create an Account</h2>
                <p class="sub-heading">Get started with SPC Pharmacy today.</p>

                <div class="mb-3">
                    <label class="form-label">Full Name</label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-user"></i></span>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="John Doe" required="true"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">ID Number</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-id-card"></i></span>
                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control" placeholder="Your ID" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Role</label>
                         <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user-tag"></i></span>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select" required="true">
                                <asp:ListItem Value="" Selected="True" Disabled="True">-- Select Role --</asp:ListItem>
                                <asp:ListItem Value="SUPPLIER">Supplier</asp:ListItem>
                                <asp:ListItem Value="SPC_STAFF">SPC Staff</asp:ListItem>
                                <asp:ListItem Value="PHARMACY">Pharmacy</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Email Address</label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="name@example.com" required="true"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Password</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create password" required="true"></asp:TextBox>
                             <span class="input-group-text password-toggle" onclick="togglePasswordVisibility('ContentPlaceHolder1_txtPassword', this)"><i class="fas fa-eye"></i></span>
                        </div>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Confirm Password</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password" required="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="mb-3 form-check">
                    <asp:CheckBox ID="chkTerms" runat="server" CssClass="form-check-input" />
                    <label class="form-check-label" for="<%= chkTerms.ClientID %>">I agree to the <a href="#" class="text-decoration-none">Terms & Conditions</a></label>
                </div>

                <div class="d-grid my-4">
                    <asp:Button ID="btnRegister" runat="server" Text="Create Account" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
                </div>
                
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3" Visible="false"></asp:Label>

                <div class="text-center">
                    <p class="mb-0">Already have an account? <a href="Login.aspx" class="fw-bold text-decoration-none">Sign In</a></p>
                </div>
            </div>
        </div>
    </form>

    <script>
        // Modern password visibility toggle function
        function togglePasswordVisibility(fieldId, iconElement) {
            const passwordField = document.getElementById(fieldId);
            const icon = iconElement.querySelector('i');
            
            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                passwordField.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        }
    </script>
</body>
</html>
