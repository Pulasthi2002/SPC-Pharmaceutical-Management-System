<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" 
    Inherits="Spc_web.Pay" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-container {
            display: flex;
            align-items: center;
            justify-content: center;
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        .payment-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            animation: fadeInUp 0.6s ease-out;
            max-width: 550px;
            width: 100%;
            overflow: hidden; /* Important for the card visual */
        }
        
        /* --- Checkout Stepper (from previous page for consistency) --- */
        .checkout-stepper {
            display: flex; justify-content: space-between; position: relative; margin-bottom: 4rem;
        }
        .checkout-stepper::before {
            content: ''; position: absolute; top: 50%; left: 0; right: 0;
            height: 2px; background-color: #e9ecef; transform: translateY(-50%); z-index: 1;
        }
        .step-item {
            display: flex; flex-direction: column; align-items: center; text-align: center;
            z-index: 2; background-color: var(--light-color); padding: 0 1rem;
        }
        .step-number {
            width: 40px; height: 40px; border-radius: 50%; background-color: #e9ecef; color: #6c757d;
            display: flex; align-items: center; justify-content: center; font-weight: 700; margin-bottom: 0.5rem; transition: all 0.3s ease;
        }
        .step-item.active .step-number { background-color: var(--primary-color); color: #fff; }
        .step-item.completed .step-number { background-color: var(--secondary-color); color: #fff; }
        .step-title { font-weight: 600; color: #6c757d; }
        .step-item.active .step-title, .step-item.completed .step-title { color: var(--dark-color); }

        /* --- Interactive Credit Card Visual --- */
        .credit-card-flipper {
            perspective: 1000px;
            margin-bottom: 2rem;
        }
        .credit-card-visual {
            width: 100%;
            aspect-ratio: 1.586 / 1; /* Standard credit card ratio */
            max-width: 400px;
            margin: 0 auto;
            position: relative;
            transition: transform 0.6s;
            transform-style: preserve-3d;
        }
        .credit-card-flipper.is-flipped .credit-card-visual {
            transform: rotateY(180deg);
        }
        .card-face {
            position: absolute; width: 100%; height: 100%;
            backface-visibility: hidden; -webkit-backface-visibility: hidden;
            border-radius: 15px;
            background: linear-gradient(135deg, #495057, #212529);
            color: #fff;
            padding: 20px;
            box-shadow: 0 15px 30px rgba(0,0,0,0.2);
            display: flex; flex-direction: column; justify-content: space-between;
        }
        .card-back {
            transform: rotateY(180deg);
        }
        .card-back .black-strip { height: 50px; background: #000; margin: 10px -20px; }
        .card-back .cvv-box {
            background: #fff; color: #000; text-align: right;
            padding: 10px; border-radius: 4px; height: 40px;
            font-style: italic; font-family: 'Courier New', Courier, monospace;
        }
        #card-cvv-display { padding-right: 10px; }

        #card-number-display {
            font-family: 'Courier New', Courier, monospace; font-size: clamp(1rem, 5vw, 1.5rem);
            letter-spacing: 2px; word-spacing: 10px;
        }
        #card-expiry-display { font-family: 'Courier New', Courier, monospace; font-size: 1rem; }
        .card-chip { width: 50px; height: 40px; background: #ffdf9a; border-radius: 5px; }
        
        @keyframes fadeInUp { from { opacity: 0; transform: translateY(30px); } to { opacity: 1; transform: translateY(0); } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header & Stepper -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Payment Details</h1>
            <p class="lead text-muted">Please provide your payment information to complete the purchase.</p>
        </div>
        <div class="checkout-stepper">
            <div class="step-item completed">
                <div class="step-number"><i class="fas fa-check"></i></div>
                <div class="step-title">Review & Ship</div>
            </div>
            <div class="step-item active">
                <div class="step-number">2</div>
                <div class="step-title">Payment</div>
            </div>
            <div class="step-item">
                <div class="step-number">3</div>
                <div class="step-title">Confirmation</div>
            </div>
        </div>

        <div class="page-container">
            <div class="payment-card">
                <div class="p-4 p-md-5">
                    <!-- Interactive Card Visual -->
                    <div class="credit-card-flipper mb-4">
                        <div class="credit-card-visual">
                            <div class="card-face card-front">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="card-chip"></div>
                                    <i class="fab fa-cc-visa fa-3x"></i>
                                </div>
                                <div id="card-number-display" class="text-center">•••• •••• •••• ••••</div>
                                <div class="d-flex justify-content-between">
                                    <span>Card Holder</span>
                                    <span>Expires</span>
                                </div>
                                <div class="d-flex justify-content-between">
                                    <span id="card-name-display">SPC Pharmacy</span>
                                    <span id="card-expiry-display">MM/YY</span>
                                </div>
                            </div>
                            <div class="card-face card-back">
                                <div class="black-strip"></div>
                                <div class="text-end">
                                    <small>CVV</small>
                                    <div class="cvv-box" id="card-cvv-display"></div>
                                </div>
                                <div class="text-center mt-auto">
                                    <i class="fab fa-cc-visa fa-2x"></i>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Payment Form -->
                    <div class="mb-3">
                        <label class="form-label">Card Number</label>
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control form-control-lg" placeholder="1234 5678 9012 3456" required="true"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col-md-7 mb-3">
                            <label class="form-label">Expiry Date</label>
                            <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control form-control-lg" placeholder="MM / YY" required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-5 mb-3">
                            <label class="form-label">CVV</label>
                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="•••" required="true"></asp:TextBox>
                        </div>
                    </div>

                    <hr class="my-4"/>

                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <span class="h5 mb-0 text-muted">Order Total:</span>
                        <span class="h4 mb-0 fw-bold text-primary"><asp:Literal ID="litTotalAmount" runat="server" /></span>
                    </div>

                    <div class="d-grid">
                        <asp:Button ID="btnSubmitPayment" runat="server" Text="Pay Now" 
                            CssClass="btn btn-primary btn-lg" OnClick="btnSubmitPayment_Click" />
                    </div>
                    <div class="text-center mt-3">
                        <small class="text-muted"><i class="fas fa-lock me-1"></i> Secure payment powered by SPC Pharmacy</small>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Hidden Literal for Order ID --%>
    <div style="display:none;"><asp:Literal ID="litOrderId" runat="server" /></div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const cardNumberInput = document.getElementById('<%= txtCardNumber.ClientID %>');
            const expiryInput = document.getElementById('<%= txtExpiry.ClientID %>');
            const cvvInput = document.getElementById('<%= txtCVV.ClientID %>');
            const cardFlipper = document.querySelector('.credit-card-flipper');

            const cardNumberDisplay = document.getElementById('card-number-display');
            const cardExpiryDisplay = document.getElementById('card-expiry-display');
            
            // Format card number with spaces
            cardNumberInput.addEventListener('input', function (e) {
                e.target.value = e.target.value.replace(/[^\d]/g, '').replace(/(.{4})/g, '$1 ').trim();
                cardNumberDisplay.textContent = e.target.value || '•••• •••• •••• ••••';
            });

            // Format expiry date with a slash
            expiryInput.addEventListener('input', function (e) {
                let value = e.target.value.replace(/[^\d]/g, '');
                if (value.length > 2) {
                    value = value.substring(0, 2) + ' / ' + value.substring(2, 4);
                }
                e.target.value = value;
                cardExpiryDisplay.textContent = e.target.value || 'MM/YY';
            });

            // Flip card for CVV input
            cvvInput.addEventListener('focus', () => cardFlipper.classList.add('is-flipped'));
            cvvInput.addEventListener('blur', () => cardFlipper.classList.remove('is-flipped'));
        });
    </script>
</asp:Content>
