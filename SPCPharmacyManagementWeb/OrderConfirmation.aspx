<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" 
    Inherits="Spc_web.OrderConfirmation" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        /* --- Checkout Stepper (Final State) --- */
        .checkout-stepper {
            display: flex; justify-content: space-between; position: relative; margin-bottom: 4rem; max-width: 600px; width:100%;
        }
        .checkout-stepper::before {
            content: ''; position: absolute; top: 50%; left: 0; right: 0;
            height: 2px; background-color: var(--secondary-color); /* Green line for completion */
             transform: translateY(-50%); z-index: 1;
        }
        .step-item {
            display: flex; flex-direction: column; align-items: center; text-align: center;
            z-index: 2; background-color: var(--light-color); padding: 0 1rem;
        }
        .step-number {
            width: 40px; height: 40px; border-radius: 50%;
            display: flex; align-items: center; justify-content: center; font-weight: 700; margin-bottom: 0.5rem; transition: all 0.3s ease;
        }
        .step-item.completed .step-number { background-color: var(--secondary-color); color: #fff; }
        .step-title { font-weight: 600; color: var(--dark-color); }

        /* --- Confirmation Panel --- */
        .confirmation-panel {
            max-width: 650px;
            width: 100%;
            background-color: #fff;
            padding: 3rem 2rem;
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
            text-align: center;
            animation: fadeInUp 0.8s ease-out;
        }

        /* --- Animated Checkmark --- */
        .checkmark-container {
            width: 100px;
            height: 100px;
            margin: 0 auto 2rem;
            position: relative;
        }
        .checkmark-circle {
            stroke-dasharray: 264;
            stroke-dashoffset: 264;
            stroke-width: 4;
            stroke: var(--secondary-color);
            fill: none;
            animation: draw-circle 0.6s cubic-bezier(0.65, 0, 0.45, 1) forwards;
        }
        .checkmark-check {
            stroke-dasharray: 48;
            stroke-dashoffset: 48;
            stroke-width: 5;
            stroke: var(--secondary-color);
            fill: none;
            animation: draw-check 0.4s cubic-bezier(0.65, 0, 0.45, 1) 0.5s forwards;
        }
        @keyframes draw-circle { to { stroke-dashoffset: 0; } }
        @keyframes draw-check { to { stroke-dashoffset: 0; } }

        /* --- Order Details Box --- */
        .order-details-box {
            background-color: #f8f9fa;
            border: 1px solid #e9ecef;
            border-radius: 0.5rem;
            padding: 1.5rem;
            margin-top: 2rem;
            margin-bottom: 2rem;
        }
        .order-id {
            font-family: 'Courier New', Courier, monospace;
            font-weight: 700;
            background-color: #e9ecef;
            padding: 0.25rem 0.75rem;
            border-radius: 5px;
        }
        
        .btn-return {
            padding: 12px 30px;
            font-weight: 600;
            font-size: 1.1rem;
        }

        @keyframes fadeInUp { from { opacity: 0; transform: translateY(30px); } to { opacity: 1; transform: translateY(0); } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container page-container">

        <!-- Checkout Stepper (Final State) -->
        <div class="checkout-stepper">
            <div class="step-item completed">
                <div class="step-number"><i class="fas fa-check"></i></div>
                <div class="step-title">Review & Ship</div>
            </div>
            <div class="step-item completed">
                <div class="step-number"><i class="fas fa-check"></i></div>
                <div class="step-title">Payment</div>
            </div>
            <div class="step-item completed">
                <div class="step-number"><i class="fas fa-check"></i></div>
                <div class="step-title">Confirmation</div>
            </div>
        </div>

        <!-- Confirmation Panel -->
        <div class="confirmation-panel">
            <!-- Animated Checkmark -->
            <div class="checkmark-container">
                <svg width="100" height="100" viewBox="0 0 88 88">
                    <circle class="checkmark-circle" cx="44" cy="44" r="42"/>
                    <path class="checkmark-check" d="M25,45 l16,16 l25,-25"/>
                </svg>
            </div>

            <h1 class="display-4 fw-bold text-success">Order Confirmed!</h1>
            <p class="lead text-muted">Thank you for your purchase. A confirmation email has been sent to you.</p>

            <div class="order-details-box">
                <p class="mb-0">Your Order ID is:</p>
                <p class="h4 mt-2 order-id">
                    <asp:Literal ID="litOrderId" runat="server" />
                </p>
            </div>
            
            <asp:Button ID="btnReturn" runat="server" Text="Continue Shopping" 
                CssClass="btn btn-primary btn-return" OnClick="btnReturn_Click" />
        </div>
    </div>
</asp:Content>
