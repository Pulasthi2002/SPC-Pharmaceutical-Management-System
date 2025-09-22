<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs"
    Inherits="Spc_web.Customers" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header {
            animation: fadeInDown 0.6s ease-out;
        }

        /* --- Modern Search Box --- */
        .search-wrapper {
            max-width: 600px;
            margin: 0 auto;
        }
        .search-box {
            position: relative;
        }
        .search-box .form-control {
            border-radius: 50px; /* Pill shape */
            padding-left: 50px;
            padding-top: 12px;
            padding-bottom: 12px;
            border: 1px solid #dee2e6;
            box-shadow: 0 5px 15px rgba(0,0,0,0.05);
            transition: all 0.3s ease;
        }
        .search-box .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 5px 20px rgba(0, 123, 255, 0.15);
        }
        .search-box .search-icon {
            position: absolute;
            top: 50%;
            left: 20px;
            transform: translateY(-50%);
            color: #aaa;
        }

        /* --- Modern Medication Card --- */
        .med-card {
            border: 1px solid #e9ecef;
            border-radius: var(--border-radius);
            transition: all 0.3s ease;
            background-color: #fff;
            display: flex;
            flex-direction: column; /* Ensures footer is always at the bottom */
        }
        .med-card:hover {
            transform: translateY(-8px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
            border-color: var(--primary-color);
        }
        
        .med-card .card-body {
            flex-grow: 1; /* Allows body to take up available space */
        }
        
        .med-card .drug-icon {
            font-size: 1.5rem;
            color: var(--primary-color);
        }

        .info-badge {
            background-color: #f0f2f5;
            color: #555;
            padding: 0.3em 0.7em;
            border-radius: 5px;
            font-size: 0.8rem;
            font-weight: 500;
        }
        
        .med-card .card-footer {
            background-color: #f8f9fa;
            border-top: 1px solid #e9ecef;
        }

        .price-tag {
            font-size: 1.5rem;
            font-weight: 700;
            color: var(--secondary-color);
        }

        /* --- Dynamic Stock Level Indicator --- */
        .stock-level {
            font-weight: 600;
        }
        .stock-ok { color: var(--secondary-color); }
        .stock-low { color: #ffc107; } /* Warning yellow */
        .stock-critical { color: #dc3545; } /* Danger red */

        .fade-in-up {
            opacity: 0;
            transform: translateY(30px);
            transition: opacity 0.6s ease-out, transform 0.6s ease-out;
            animation: fadeInUp 0.8s ease-out forwards;
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header & Search -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Medication Catalog</h1>
            <p class="lead text-muted">Browse and search our comprehensive list of available medications.</p>
            <div class="search-wrapper mt-4">
                <div class="search-box">
                    <i class="fas fa-search search-icon"></i>
                    <asp:TextBox ID="txtSearch" runat="server"
                        CssClass="form-control"
                        placeholder="Search by drug name, generic name, or manufacturer..."
                        AutoPostBack="true"
                        OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>

        <asp:Label ID="lblNoDrugs" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>

        <div class="row g-4">
            <asp:Repeater ID="rptDrugs" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 col-lg-4 col-xl-3 d-flex align-items-stretch fade-in-up">
                        <div class="card med-card">
                            <div class="card-body p-4">
                                <div class="d-flex align-items-start mb-2">
                                    <i class="fas fa-pills drug-icon me-3 mt-1"></i>
                                    <div>
                                        <h4 class="card-title fw-bold mb-0"><%# Eval("drug_name") %></h4>
                                        <h6 class="card-subtitle text-muted"><%# Eval("generic_name") %></h6>
                                    </div>
                                </div>
                                
                                <div class="my-3 d-flex flex-wrap gap-2">
                                    <span class="info-badge">
                                        <i class="fas fa-industry me-1 opacity-75"></i><%# Eval("manufacturer") %>
                                    </span>
                                    <span class="info-badge">
                                        <i class="fas fa-hashtag me-1 opacity-75"></i>Batch: <%# Eval("batch_number") %>
                                    </span>
                                </div>
                                
                                <p class="mb-0 text-muted small">
                                    <strong>Expires:</strong> <%# Convert.ToDateTime(Eval("expiry_date")).ToString("MMM yyyy") %>
                                </p>
                            </div>

                            <div class="card-footer p-3">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>
                                        <%-- This code dynamically changes the color based on stock quantity --%>
                                        <span class='stock-level <%# (int)Eval("quantity_in_stock") == 0 ? "stock-critical" : ((int)Eval("quantity_in_stock") <= 50 ? "stock-low" : "stock-ok") %>'>
                                            <i class="fas fa-boxes-stacked me-1"></i>
                                            <%# (int)Eval("quantity_in_stock") == 0 ? "Out of Stock" : Eval("quantity_in_stock") + " in Stock" %>
                                        </span>
                                    </div>
                                    <div class="price-tag">
                                        <%# Eval("unit_price", "{0:C}") %> <%-- Using {0:C} for currency format --%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
