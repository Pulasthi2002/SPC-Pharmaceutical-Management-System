<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToOrder.aspx.cs" 
    Inherits="Spc_web.AddToOrder" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-container {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 70vh; /* Vertically center the card */
        }
        
        .order-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            animation: fadeInUp 0.6s ease-out;
            max-width: 550px;
            width: 100%;
        }

        .order-card .card-header {
            background-color: var(--primary-color);
            color: #fff;
            padding: 1.5rem;
            border-radius: var(--border-radius) var(--border-radius) 0 0;
            text-align: center;
        }

        /* We will hide the original TextBoxes and display their values in this styled header */
        .order-card .drug-name {
            font-weight: 700;
            font-size: 1.75rem;
        }

        .order-card .unit-price {
            font-size: 1.1rem;
            opacity: 0.9;
        }

        /* --- Modern Quantity Stepper --- */
        .quantity-stepper {
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .quantity-stepper .btn {
            width: 50px;
            height: 50px;
            font-size: 1.5rem;
            font-weight: 300;
            border-radius: 50%; /* Circular buttons */
            border-color: #dee2e6;
        }
        .quantity-stepper .form-control {
            width: 100px;
            height: 60px;
            text-align: center;
            font-size: 2rem;
            font-weight: 600;
            border-left: none;
            border-right: none;
            box-shadow: none !important;
        }
        /* Hides the default up/down arrows in number inputs */
        .quantity-stepper input[type=number]::-webkit-inner-spin-button, 
        .quantity-stepper input[type=number]::-webkit-outer-spin-button { 
            -webkit-appearance: none;
            margin: 0;
        }
        .quantity-stepper input[type=number] {
            -moz-appearance: textfield; /* Firefox */
        }

        .btn-submit {
            padding: 12px 30px;
            font-weight: 600;
            font-size: 1.1rem;
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container page-container">
        <div class="order-card">
            <div class="card-header">
                <%-- We use spans to display the values from the hidden TextBoxes for a better look --%>
                <p class="drug-name mb-1"><span id="displayDrugName"></span></p>
                <p class="unit-price mb-0"><span id="displayUnitPrice"></span> per unit</p>
            </div>
            <div class="card-body p-4 p-md-5 text-center">
                <asp:HiddenField ID="hdnDrugId" runat="server" />
                
                <%-- The original TextBoxes are now hidden but still function for the backend --%>
                <div style="display: none;">
                    <asp:TextBox ID="txtDrugName" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtUnitPrice" runat="server" ReadOnly="true"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <label class="form-label h5 fw-normal text-muted">Select Quantity</label>
                    <div class="quantity-stepper mt-2">
                        <button type="button" class="btn btn-outline-secondary" onclick="step(-1)">-</button>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" 
                            TextMode="Number" min="1" Text="1" required="true"></asp:TextBox>
                        <button type="button" class="btn btn-outline-secondary" onclick="step(1)">+</button>
                    </div>
                </div>

                <div class="d-grid gap-3">
                    <asp:Button ID="btnAddToOrder" runat="server" Text="Confirm & Add to Order" 
                        CssClass="btn btn-primary btn-lg btn-submit" OnClick="btnAddToOrder_Click" />
                    
                    <asp:HyperLink ID="lnkCancel" runat="server" NavigateUrl="~/Pharmacy.aspx" 
                        CssClass="btn btn-link text-secondary">Back to Ordering</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>

    <script>
        // This function syncs the hidden TextBox values to the display spans on page load
        document.addEventListener('DOMContentLoaded', function() {
            const drugNameInput = document.getElementById('<%= txtDrugName.ClientID %>');
            const unitPriceInput = document.getElementById('<%= txtUnitPrice.ClientID %>');
            
            document.getElementById('displayDrugName').textContent = drugNameInput.value;
            document.getElementById('displayUnitPrice').textContent = unitPriceInput.value;
        });

        // Simple JavaScript for the +/- stepper buttons
        function step(amount) {
            const qtyInput = document.getElementById('<%= txtQuantity.ClientID %>');
            let currentValue = parseInt(qtyInput.value, 10);
            
            if (isNaN(currentValue)) {
                currentValue = 1;
            }

            let newValue = currentValue + amount;

            if (newValue < 1) {
                newValue = 1;
            }
            
            qtyInput.value = newValue;
        }
    </script>
</asp:Content>
