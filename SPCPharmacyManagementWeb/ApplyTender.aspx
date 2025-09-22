<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyTender.aspx.cs" 
    Inherits="Spc_web.ApplyTender" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .content-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            height: 100%; /* Important for column height matching */
        }
        .info-panel {
            background-color: var(--light-color);
        }
        .content-card .card-header {
            background-color: transparent;
            border-bottom: 1px solid #e9ecef;
            padding: 1.25rem 1.5rem;
            font-weight: 600;
            font-size: 1.15rem;
            color: var(--dark-color);
        }

        /* --- Info Panel Styling --- */
        .info-list-item {
            display: flex;
            align-items: flex-start;
            margin-bottom: 1.5rem;
        }
        .info-list-item .icon {
            font-size: 1.25rem;
            color: var(--primary-color);
            margin-right: 1rem;
            width: 25px;
            text-align: center;
        }

        /* --- Interactive Table Styling --- */
        .table thead th {
            background-color: #f8f9fa;
            color: #343a40;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.8rem;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #dee2e6;
            vertical-align: middle;
        }
        .table tbody td {
            vertical-align: middle;
        }

        .input-group .form-control {
            border-right: 1px solid #dee2e6; /* Add back border next to input group text */
            padding-left: .75rem;
        }
        .input-group .form-control:focus {
            border-left: 1px solid #dee2e6;
        }

        /* --- Totals Summary Styling --- */
        .total-summary h3 {
            font-weight: 300;
        }
        .total-summary .total-amount-value {
            font-weight: 700;
            color: var(--primary-color);
        }
        
        .btn-submit {
             padding: 12px 30px;
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="text-center mb-5">
            <h1 class="display-5 fw-bold">Tender Application</h1>
            <p class="lead text-muted">Review the details below and submit your pricing proposal.</p>
        </div>
        
        <asp:HiddenField ID="hdnTenderId" runat="server" />

        <div class="row g-4">
            <!-- Left Panel: Tender Information -->
            <div class="col-lg-5">
                <div class="card content-card info-panel">
                    <div class="card-header">
                        <h4><i class="fas fa-info-circle me-2"></i>Tender Details</h4>
                    </div>
                    <div class="card-body p-4">
                        <ul class="list-unstyled">
                            <li class="info-list-item">
                                <div class="icon"><i class="fas fa-file-signature fa-fw"></i></div>
                                <div>
                                    <strong class="d-block">Tender Title</strong>
                                    <p class="mb-0 text-muted"><asp:Literal ID="litTenderTitle" runat="server" /></p>
                                </div>
                            </li>
                            <li class="info-list-item">
                                <div class="icon"><i class="fas fa-align-left fa-fw"></i></div>
                                <div>
                                    <strong class="d-block">Description</strong>
                                    <p class="mb-0 text-muted"><asp:Literal ID="litTenderDesc" runat="server" /></p>
                                </div>
                            </li>
                             <li class="info-list-item">
                                <div class="icon"><i class="fas fa-calendar-times fa-fw"></i></div>
                                <div>
                                    <strong class="d-block">Deadline</strong>
                                    <p class="mb-0 text-muted"><asp:Literal ID="litDeadline" runat="server" /></p>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Right Panel: Proposal Form -->
            <div class="col-lg-7">
                <div class="card content-card">
                    <div class="card-header">
                        <h4><i class="fas fa-edit me-2"></i>Your Proposal</h4>
                    </div>
                    <div class="card-body p-4">
                        <h5 class="mb-3">Item Pricing</h5>
                        <div class="table-responsive">
                            <table class="table align-middle">
                                <thead>
                                    <tr>
                                        <th>Drug Name</th>
                                        <th class="text-center">Required Qty</th>
                                        <th>Your Price (per unit)</th>
                                        <th class="text-end">Line Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptProposalItems" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("drug_name") %></td>
                                                <td class="text-center"><%# Eval("required_quantity") %></td>
                                                <td>
                                                    <div class="input-group">
                                                        <span class="input-group-text">$</span>
                                                        <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" 
                                                            TextMode="Number" step="0.01" min="0.01" required="true"
                                                            Text='0.00' oninput="calculateTotal(this)" />
                                                    </div>
                                                    <asp:HiddenField ID="hdnTenderItemId" runat="server" 
                                                        Value='<%# Eval("tender_item_id") %>' />
                                                </td>
                                                <td class="text-end fw-bold">
                                                    $<span class="item-total">0.00</span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        
                        <hr class="my-4"/>
                        
                        <div class="mb-4">
                            <label class="form-label fw-bold">Additional Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" 
                                TextMode="MultiLine" Rows="3" placeholder="Provide any additional comments or details here..."></asp:TextBox>
                        </div>
                        
                        <!-- Total Summary -->
                        <div class="card bg-primary-subtle border-primary-subtle total-summary p-4 rounded-3">
                             <div class="d-flex justify-content-between align-items-center">
                                <h3 class="mb-0">Grand Total:</h3>
                                <h3 class="mb-0 total-amount-value">$<span id="totalAmount">0.00</span></h3>
                            </div>
                            <asp:HiddenField ID="hdnTotalAmount" runat="server" Value="0" />
                        </div>
                        
                        <!-- Action Buttons -->
                        <div class="d-flex justify-content-end mt-4">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                CssClass="btn btn-secondary me-3" OnClick="btnCancel_Click" CausesValidation="false" />
                            <asp:Button ID="btnSubmitProposal" runat="server" Text="Submit Proposal" 
                                CssClass="btn btn-primary btn-lg btn-submit" OnClick="btnSubmitProposal_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // No changes needed to this script, it works with the new layout.
        // Using 'oninput' is slightly better than 'onchange' for real-time updates.
        function calculateTotal(input) {
            const row = input.closest('tr');
            const qty = parseFloat(row.cells[1].textContent);
            const price = parseFloat(input.value) || 0;
            const total = qty * price;
            
            // Find the span with class 'item-total' in the last cell
            row.cells[3].querySelector('.item-total').textContent = total.toFixed(2);
            
            // Update grand total
            let grandTotal = 0;
            document.querySelectorAll('.item-total').forEach(el => {
                grandTotal += parseFloat(el.textContent) || 0;
            });
            
            document.getElementById('totalAmount').textContent = grandTotal.toFixed(2);
            // Use the ClientID for the hidden field to ensure it works correctly in ASP.NET
            document.getElementById('<%= hdnTotalAmount.ClientID %>').value = grandTotal;
        }
    </script>
</asp:Content>
