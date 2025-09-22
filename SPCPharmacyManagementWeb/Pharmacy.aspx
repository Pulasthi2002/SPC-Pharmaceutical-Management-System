<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pharmacy.aspx.cs" 
    Inherits="Spc_web.Pharmacy" MasterPageFile="~/Spc.master" %>

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
            color: var(--dark-color);
        }

        /* --- Modern Table Styling --- */
        .table thead th {
            background-color: #f8f9fa;
            color: #343a40;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.8rem;
            letter-spacing: 0.5px;
            vertical-align: middle;
        }
        .table tbody td {
            vertical-align: middle;
        }
        .table-hover tbody tr:hover {
            background-color: rgba(0, 123, 255, 0.05);
        }

        /* --- Order Cart Styling (Right Panel) --- */
        #order-summary {
            background-color: var(--light-color);
            position: sticky; /* Makes the cart follow the user on scroll */
            top: 100px; /* Adjust based on navbar height */
        }
        .order-total-section {
            padding: 1.5rem;
            border-top: 1px solid #e9ecef;
        }
        .order-total-section .total-label {
            font-size: 1.1rem;
            font-weight: 500;
            color: #555;
        }
        .order-total-section .total-value {
            font-size: 2rem;
            font-weight: 700;
            color: var(--primary-color);
        }
        .btn-checkout {
            padding: 12px;
            font-size: 1.1rem;
            font-weight: 600;
        }
        .btn-remove {
            width: 35px;
            height: 35px;
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            border: none;
            background-color: #f8d7da;
            color: #842029;
            transition: all 0.2s ease;
            text-decoration: none; /* Add this for LinkButton */
        }
        .btn-remove:hover {
            background-color: #dc3545;
            color: #fff;
            text-decoration: none; /* Add this for LinkButton */
        }

        /* --- Dynamic Stock Level Indicator --- */
        .stock-level {
            font-weight: 600;
        }
        .stock-ok { color: var(--secondary-color); }
        .stock-low { color: #ffc107; }
        .stock-critical { color: #dc3545; }
        
        .empty-state {
            text-align: center;
            padding: 3rem 1rem;
        }
        .empty-state .fa-icon {
            font-size: 3rem;
            color: #ced4da;
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Pharmaceutical Ordering</h1>
            <p class="lead text-muted">Select from available medications to build your order.</p>
        </div>
        
        <div class="row g-4">
            <!-- Left Panel: Drug Inventory -->
            <div class="col-lg-8">
                <div class="content-card">
                    <div class="card-header">
                        <h5><i class="fas fa-capsules me-2 text-primary"></i>Available Medications</h5>
                    </div>
                    <div class="card-body p-0 p-md-3">
                         <div class="table-responsive">
                            <asp:GridView ID="gvDrugs" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-hover align-middle mb-0" DataKeyNames="drug_id"
                                OnRowCommand="gvDrugs_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="drug_name" HeaderText="Drug Name" />
                                    <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" />
                                    <asp:BoundField DataField="unit_price" HeaderText="Price" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Stock">
                                        <ItemTemplate>
                                            <span class='stock-level <%# (int)Eval("quantity_in_stock") == 0 ? "stock-critical" : ((int)Eval("quantity_in_stock") <= 50 ? "stock-low" : "stock-ok") %>'>
                                                <%# Eval("quantity_in_stock") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="120px" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnOrder" runat="server" Text="Add to Order" 
                                                CommandName="OrderDrug" 
                                                CommandArgument='<%# Eval("drug_id") %>'
                                                CssClass="btn btn-sm btn-outline-primary" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="empty-state">
                                        <i class="fas fa-pills fa-icon"></i>
                                        <h5 class="text-muted">No drugs available for ordering.</h5>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Right Panel: Current Order (Shopping Cart) -->
            <div class="col-lg-4">
                <div id="order-summary" class="content-card">
                    <div class="card-header">
                        <h5><i class="fas fa-shopping-cart me-2 text-primary"></i>Current Order</h5>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="False" 
                                CssClass="table align-middle mb-0" OnRowDeleting="gvOrderItems_RowDeleting"
                                DataKeyNames="drug_id">
                                <Columns>
                                    <asp:BoundField DataField="drug_name" HeaderText="Drug" />
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" ItemStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="total_price" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-CssClass="text-end" />
                                    <%-- ** THE FIX IS HERE ** --%>
                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnRemove" runat="server" 
                                                CssClass="btn-remove"
                                                CommandName="Delete" 
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                ToolTip="Remove Item"
                                                CausesValidation="false">
                                                <i class='fas fa-times'></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="empty-state">
                                        <i class="fas fa-shopping-cart fa-icon"></i>
                                        <p class="text-muted">Your order is empty.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="order-total-section text-center">
                        <p class="total-label mb-1">Order Total</p>
                        <p class="total-value mb-4">
                            <asp:Literal ID="litTotalAmount" runat="server" Text="$0.00" />
                        </p>
                        <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" 
                            CssClass="btn btn-primary w-100 btn-checkout" OnClick="btnCheckout_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
