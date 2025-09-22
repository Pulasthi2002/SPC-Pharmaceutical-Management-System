<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stinfo.aspx.cs" 
    Inherits="Spc_web.stinfo" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header { animation: fadeInDown 0.6s ease-out; }
        .content-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            height: 100%;
        }
        .content-card .card-header {
            background-color: transparent;
            border-bottom: 1px solid #e9ecef;
            padding: 1.25rem 1.5rem;
            font-weight: 600;
        }

        /* --- Change Summary Panel Styling (Left Side) --- */
        .change-summary .summary-item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 1rem;
        }
        .change-summary .value-box {
            background-color: #f8f9fa;
            border: 1px solid #e9ecef;
            padding: 0.75rem 1rem;
            border-radius: 0.5rem;
            text-align: center;
            flex-grow: 1;
        }
        .change-summary .value-box .label {
            font-size: 0.8rem;
            color: #6c757d;
            text-transform: uppercase;
        }
        .change-summary .value-box .value {
            font-size: 1.5rem;
            font-weight: 700;
            color: var(--dark-color);
        }
        .change-summary .arrow-icon {
            font-size: 1.5rem;
            color: #ced4da;
        }
        .change-summary .change-amount {
            font-weight: 600;
            margin-top: 0.5rem;
            display: block;
            text-align: center;
        }

        /* --- Form Styling (Right Side) --- */
        .input-group-text {
            background-color: #f0f2f5;
            border-right: none;
            color: #999;
            width: 42px;
            justify-content: center;
        }
        .form-control, .form-select {
            border-left: none;
            padding-left: 0.25rem;
        }
        .form-label { font-weight: 600; }
        .btn-submit { padding: 12px 30px; font-weight: 600; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Confirm Stock Update</h1>
            <p class="lead text-muted">Review the changes and provide a reason for the inventory update.</p>
        </div>

        <div class="row g-4 g-lg-5">
            <!-- Left Panel: Change Summary -->
            <div class="col-lg-6">
                <div class="content-card change-summary">
                    <div class="card-header">
                        <h5><i class="fas fa-exchange-alt me-2 text-primary"></i>Change Summary</h5>
                    </div>
                    <div class="card-body p-4">
                        <h4 class="fw-bold mb-4">
                            <asp:Literal ID="litDrugName" runat="server"></asp:Literal>
                        </h4>
                        
                        <!-- Price Change -->
                        <div class="mb-4">
                            <p class="mb-2"><strong>Price Change:</strong></p>
                            <div class="summary-item">
                                <div class="value-box">
                                    <div class="label">Old Price</div>
                                    <div class="value"><asp:Literal ID="litOldPrice" runat="server" /></div>
                                </div>
                                <i class="fas fa-long-arrow-alt-right arrow-icon"></i>
                                <div class="value-box">
                                    <div class="label">New Price</div>
                                    <div class="value"><asp:Literal ID="litNewPrice" runat="server" /></div>
                                </div>
                            </div>
                            <p class="change-amount <%# (decimal)Session["NewPrice"] - (decimal)Session["OldPrice"] >= 0 ? "text-success" : "text-danger" %>">
                                Change: <%# ((decimal)Session["NewPrice"] - (decimal)Session["OldPrice"]).ToString("C") %>
                            </p>
                        </div>
                        
                        <hr />

                        <!-- Quantity Change -->
                        <div class="mt-4">
                             <p class="mb-2"><strong>Quantity Change:</strong></p>
                            <div class="summary-item">
                                <div class="value-box">
                                    <div class="label">Old Quantity</div>
                                    <div class="value"><asp:Literal ID="litOldQuantity" runat="server" /></div>
                                </div>
                                <i class="fas fa-long-arrow-alt-right arrow-icon"></i>
                                <div class="value-box">
                                    <div class="label">New Quantity</div>
                                    <div class="value"><asp:Literal ID="litNewQuantity" runat="server" /></div>
                                </div>
                            </div>
                            <p class="change-amount <%# (int)Session["NewQuantity"] - (int)Session["OldQuantity"] >= 0 ? "text-success" : "text-danger" %>">
                                Change: <%# ((int)Session["NewQuantity"] - (int)Session["OldQuantity"]).ToString("+#;-#;0") %> units
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Right Panel: Justification Form -->
            <div class="col-lg-6">
                <div class="content-card">
                    <div class="card-header">
                        <h5><i class="fas fa-clipboard-check me-2 text-primary"></i>Update Justification</h5>
                    </div>
                    <div class="card-body p-4">
                        <div class="mb-4">
                            <label class="form-label">Update Type <span class="text-danger">*</span></label>
                            <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-list-ul fa-fw"></i></span>
                                <asp:DropDownList ID="ddlUpdateType" runat="server" CssClass="form-select" required="true">
                                    <asp:ListItem Value="">-- Select Type --</asp:ListItem>
                                    <asp:ListItem Value="ADD">Add Stock (New Arrival)</asp:ListItem>
                                    <asp:ListItem Value="REMOVE">Remove Stock (Expired/Damaged)</asp:ListItem>
                                    <asp:ListItem Value="ADJUSTMENT">Manual Adjustment</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="mb-4">
                            <label class="form-label">Reason for Update <span class="text-danger">*</span></label>
                            <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-pencil-alt fa-fw"></i></span>
                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" 
                                    Rows="3" CssClass="form-control" required="true" placeholder="e.g., Annual stock count adjustment."></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="mb-4">
                            <label class="form-label">Reference Number (Optional)</label>
                            <div class="input-group">
                                 <span class="input-group-text"><i class="fas fa-hashtag fa-fw"></i></span>
                                <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" placeholder="e.g., Invoice #, PO #"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="d-grid mt-5">
                            <asp:Button ID="btnSaveUpdate" runat="server" Text="Confirm & Log Update" 
                                CssClass="btn btn-primary btn-lg btn-submit" OnClick="btnSaveUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
