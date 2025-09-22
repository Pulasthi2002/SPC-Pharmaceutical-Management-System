<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Spc_web.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - SPC Pharmacy</title>
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
        /* CSS remains the same */
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

        .login-container {
            display: grid;
            grid-template-columns: 55% 45%; /* Swapped order for visual variety */
            max-width: 1000px;
            width: 100%;
            background-color: #fff;
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
            overflow: hidden;
            animation: fadeIn 0.8s ease-out;
        }

        /* --- Branding Panel (Right Side) --- */
        .branding-panel {
            padding: 50px;
            background: linear-gradient(160deg, var(--secondary-color), var(--primary-color));
            color: #fff;
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
            position: relative;
        }
        
        .branding-panel .fa-pills {
            font-size: 4rem;
            margin-bottom: 1.5rem;
            opacity: 0.8;
            transform: rotate(-10deg);
        }

        .branding-panel h1 {
            font-weight: 700;
            font-size: 2.2rem;
        }
        .branding-panel p {
            font-weight: 300;
            font-size: 1.1rem;
            opacity: 0.9;
        }
        
        /* --- Form Panel (Left Side) --- */
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

        /* Modern Segmented Control for Role Selection */
        .role-selector .btn-group {
            width: 100%;
        }
        .role-selector .btn {
            border: 1px solid #dee2e6;
            color: #6c757d;
            background-color: #fff;
            padding: 10px;
            transition: all 0.2s ease-in-out;
        }
        .role-selector .btn.active {
            background-color: var(--primary-color);
            color: #fff;
            border-color: var(--primary-color);
        }
        .role-selector .btn:hover:not(.active) {
            background-color: #e9ecef;
        }
        
        .input-group-text {
            background-color: #f0f2f5;
            border-right: none;
            color: #999;
        }
        .form-control {
            border-left: none;
            padding-left: 0;
        }
        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(0, 123, 255, 0.15);
            border-left: 1px solid #dee2e6;
            padding-left: .75rem;
        }

        .password-toggle { cursor: pointer; }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            padding: 12px;
            font-weight: 600;
        }
        
        /* --- Responsive Design --- */
        @media (max-width: 991.98px) {
            .login-container {
                grid-template-columns: 1fr;
            }
            .branding-panel {
                display: none;
            }
            .form-panel {
                padding: 30px;
            }
        }
        @keyframes fadeIn { from { opacity: 0; transform: scale(0.98); } to { opacity: 1; transform: scale(1); } }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <!-- Login Form Panel -->
            <div class="form-panel">
                <h2>Member Login</h2>
                <p class="sub-heading">Secure access to your pharmaceutical portal.</p>

                <!-- Role Selection -->
                <div class="mb-4 role-selector">
                    <label class="form-label fw-bold">Select your role:</label>
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="btnSupplier" runat="server" CssClass="btn" CommandArgument="SUPPLIER" OnClick="RoleButton_Click">Supplier</asp:LinkButton>
                        <asp:LinkButton ID="btnStaff" runat="server" CssClass="btn" CommandArgument="SPC_STAFF" OnClick="RoleButton_Click">SPC Staff</asp:LinkButton>
                        <asp:LinkButton ID="btnPharmacy" runat="server" CssClass="btn" CommandArgument="PHARMACY" OnClick="RoleButton_Click">Pharmacy</asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="hfSelectedRole" runat="server" />
                    <asp:Label ID="lblRoleError" runat="server" CssClass="text-danger small mt-1 d-block" Visible="false"></asp:Label>
                </div>
                
                <!-- Login Form -->
                <div class="mb-3">
                    <label class="form-label">Email Address</label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="name@example.com" required="true"></asp:TextBox>
                    </div>
                </div>
                
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-lock"></i></span>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" required="true"></asp:TextBox>
                        <span class="input-group-text password-toggle" onclick="togglePasswordVisibility('ContentPlaceHolder1_txtPassword', this)"><i class="fas fa-eye"></i></span>
                    </div>
                </div>
                
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div class="form-check">
                        <asp:CheckBox ID="chkRemember" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="<%= chkRemember.ClientID %>">Remember me</label>
                    </div>
                    <div>
                        <a href="ForgotPassword.aspx" class="text-decoration-none small">Forgot Password?</a>
                    </div>
                </div>
                
                <div class="d-grid mb-3">
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                </div>
                
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3" Visible="false"></asp:Label>

                <div class="text-center">
                    <p class="mb-0 text-muted">Don't have an account? <a href="Registation.aspx" class="fw-bold text-decoration-none">Register Now</a></p>
                </div>
            </div>

            <!-- Branding Panel -->
            <div class="branding-panel">
                <i class="fas fa-pills"></i>
                <h1>SPC Pharmacy Portal</h1>
                <p>Your secure gateway to comprehensive pharmaceutical management and services.</p>
            </div>
        </div>
    </form>

    <script>
        // Set active class on role button click for immediate visual feedback
        const roleButtons = document.querySelectorAll('.role-selector .btn');
        roleButtons.forEach(button => {
            button.addEventListener('click', function (e) {
                // This script provides client-side feedback. The actual value is set on the server postback.
                roleButtons.forEach(btn => btn.classList.remove('active'));
                this.classList.add('active');
            });
        });

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
