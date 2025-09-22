<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pharmaciesdetails.aspx.cs" 
    Inherits="Spc_web.pharmaciesdetails" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Page-specific styles for a modern form layout --%>
    <style>
        .page-header { animation: fadeInDown 0.6s ease-out; }
        .form-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            animation: fadeInUp 0.8s ease-out;
            animation-fill-mode: both;
        }

        .form-card .card-header {
            background-color: transparent;
            border-bottom: 1px solid #e9ecef;
            padding: 1.5rem 2rem;
            font-weight: 600;
            font-size: 1.25rem;
            color: var(--dark-color);
        }

        .form-label {
            font-weight: 500;
            margin-bottom: 0.5rem;
        }

        /* Consistent modern input group styling */
        .input-group-text {
            background-color: #f0f2f5;
            border-right: none;
            color: #999;
            width: 42px; /* Fixed width for perfect icon alignment */
            justify-content: center;
        }
        .form-control, .form-select {
            border-left: none;
            padding-left: 0.25rem;
        }
        .form-control:focus, .form-select:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(0, 123, 255, 0.15);
            border-left: 1px solid #dee2e6;
            padding-left: .75rem;
        }
        
        .btn-submit {
            padding: 12px 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
        .btn-submit:hover {
            transform: translateY(-3px);
            box-shadow: 0 4px 15px rgba(0, 123, 255, 0.3);
        }
        
        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">

        <!-- Page Header -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Complete Your Pharmacy Profile</h1>
            <p class="lead text-muted">Provide your details to join the SPC Pharmacy network.</p>
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>

        <!-- Main Form Card -->
        <div class="card form-card">
            <div class="card-header">
                <h4><i class="fas fa-prescription-bottle-alt me-2 text-primary"></i>Pharmacy Information</h4>
            </div>
            <div class="card-body p-4 p-md-5">
                <div class="mb-4">
                    <label class="form-label">Pharmacy Name <span class="text-danger">*</span></label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-clinic-medical fa-fw"></i></span>
                        <asp:TextBox ID="txtPharmacyName" runat="server" CssClass="form-control" placeholder="e.g., City Central Pharmacy" required="true"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Contact Person</label>
                         <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user-tie fa-fw"></i></span>
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" placeholder="e.g., John Smith"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6 mb-4">
                        <label class="form-label">License Number <span class="text-danger">*</span></label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-id-badge fa-fw"></i></span>
                            <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-control" placeholder="Your official license number" required="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="mb-4">
                    <label class="form-label">Email <span class="text-danger">*</span></label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-envelope fa-fw"></i></span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="contact@pharmacy.com" required="true"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Phone</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-phone fa-fw"></i></span>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="(+1) 234-567-890"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Pharmacy Type</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-tags fa-fw"></i></span>
                            <asp:DropDownList ID="ddlPharmacyType" runat="server" CssClass="form-select">
                                <asp:ListItem Value="LINKED_DEALER">Linked Dealer</asp:ListItem>
                                <asp:ListItem Value="SPC_OWNED">SPC Owned</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="mb-4">
                    <label class="form-label">Address</label>
                     <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-map-marker-alt fa-fw"></i></span>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="123 Health St, Wellness City"></asp:TextBox>
                    </div>
                </div>
                
                <hr class="my-4" />

                <div class="text-end">
                    <asp:Button ID="btnSave" runat="server" Text="Complete Registration" CssClass="btn btn-primary btn-lg btn-submit" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
