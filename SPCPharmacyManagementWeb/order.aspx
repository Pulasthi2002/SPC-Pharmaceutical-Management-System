<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" 
    Inherits="Spc_web.Order" MasterPageFile="~/Spc.master" %>

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

        /* --- Modern Checkout Stepper --- */
        .checkout-stepper {
            display: flex;
            justify-content: space-between;
            position: relative;
            margin-bottom: 4rem;
        }
        .checkout-stepper::before {
            content: '';
            position: absolute;
            top: 50%;
            left: 0;
            right: 0;
            height: 2px;
            background-color: #e9ecef;
            transform: translateY(-50%);
            z-index: 1;
        }
        .step-item {
            display: flex;
            flex-direction: column;
            align-items: center;
            text-align: center;
            z-index: 2;
            background-color: var(--light-color); /* Matches body background */
            padding: 0 1rem;
        }
        .step-number {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            background-color: #e9ecef;
            color: #6c757d;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 700;
            margin-bottom: 0.5rem;
            transition: all 0.3s ease;
        }
        .step-item.active .step-number {
            background-color: var(--primary-color);
            color: #fff;
        }
        .step-title {
            font-weight: 600;
            color: #6c757d;
        }
        .step-item.active .step-title {
            color: var(--primary-color);
        }

        /* --- Order Summary (Right Panel) --- */
        #order-summary-card {
            position: sticky;
            top: 100px;
        }
        .order-summary-table {
            font-size: 0.95rem;
        }
        .order-summary-table th {
            font-weight: 500;
            color: #6c757d;
            border: none;
        }
        .order-summary-table td { border: none; }
        .order-summary-total {
            border-top: 2px dashed #dee2e6;
            padding-top: 1rem;
            margin-top: 1rem;
        }
        .order-summary-total strong {
            font-size: 1.25rem;
            font-weight: 700;
        }
        
        /* --- Form Styling (Left Panel) --- */
        .form-section .form-label {
            font-weight: 600;
        }
        .input-group .form-control {
            border-left: none; padding-left: 0;
        }
        .input-group-text {
            background-color: #f0f2f5; border-right: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Checkout</h1>
            <p class="lead text-muted">Please confirm your order and provide shipping details.</p>
        </div>

        <!-- Checkout Stepper -->
        <div class="checkout-stepper">
            <div class="step-item active">
                <div class="step-number">1</div>
                <div class="step-title">Review & Ship</div>
            </div>
            <div class="step-item">
                <div class="step-number">2</div>
                <div class="step-title">Payment</div>
            </div>
            <div class="step-item">
                <div class="step-number">3</div>
                <div class="step-title">Confirmation</div>
            </div>
        </div>

        <div class="row g-4 g-lg-5">
            <!-- Left Panel: Shipping & Notes -->
            <div class="col-lg-7">
                <div class="content-card form-section">
                    <div class="card-body p-4 p-md-5">
                        <h4 class="mb-4"><i class="fas fa-truck me-2 text-primary"></i>Shipping Information</h4>
                        <div class="mb-4">
                            <label class="form-label">Shipping Address</label>
                            <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-map-marker-alt fa-fw"></i></span>
                                <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3" required="true" placeholder="Enter your full shipping address..."></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="mb-4">
                            <label class="form-label">Order Notes (Optional)</label>
                            <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-pencil-alt fa-fw"></i></span>
                                <asp:TextBox ID="txtOrderNotes" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="2" placeholder="Any special instructions for this order?"></asp:TextBox>
                            </div>
                        </div>

                        <hr class="my-5" />

                        <div class="d-flex justify-content-between align-items-center">
                             <asp:HyperLink runat="server" NavigateUrl="~/Pharmacy.aspx" CssClass="btn btn-link text-secondary">
                                <i class="fas fa-arrow-left me-2"></i>Back to Ordering
                            </asp:HyperLink>
                            <asp:Button ID="btnPlaceOrder" runat="server" Text="Proceed to Payment" 
                                CssClass="btn btn-primary btn-lg" OnClick="btnPlaceOrder_Click" />
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Right Panel: Order Summary -->
            <div class="col-lg-5">
                <div id="order-summary-card" class="content-card">
                     <div class="card-body p-4">
                        <h4 class="mb-4"><i class="fas fa-receipt me-2 text-primary"></i>Order Summary</h4>
                        <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="False" 
                            CssClass="table order-summary-table table-borderless">
                            <Columns>
                                <asp:BoundField DataField="drug_name" HeaderText="Item" />
                                <asp:BoundField DataField="quantity" HeaderText="Qty" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="total_price" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-CssClass="text-end" />
                            </Columns>
                        </asp:GridView>
                        <div class="order-summary-total d-flex justify-content-between">
                            <strong>Total Amount</strong>
                            <strong><asp:Literal ID="litTotalAmount" runat="server" /></strong>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
