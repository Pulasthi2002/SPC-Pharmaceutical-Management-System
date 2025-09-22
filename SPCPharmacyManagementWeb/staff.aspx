<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staff.aspx.cs" Inherits="Spc_web.staff" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header { animation: fadeInDown 0.6s ease-out; }
        .content-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
        }
        .content-card .card-header {
            background-color: transparent;
            border-bottom: 1px solid #e9ecef;
            padding: 1.25rem 1.5rem;
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
        .table tbody td { vertical-align: middle; }
        .table-hover tbody tr:hover { background-color: rgba(0, 123, 255, 0.05); }

        /* --- Styling for In-line Editing Controls --- */
        .edit-mode-input {
            padding: 0.5rem 0.75rem;
            font-size: 1rem;
            border: 1px solid var(--primary-color);
            border-radius: 0.375rem;
            width: 100%;
            min-width: 120px; /* Ensure it doesn't get too small */
            box-shadow: 0 0 0 0.25rem rgba(0, 123, 255, 0.15);
        }

        /* --- Custom Action Buttons for TemplateField --- */
        .btn-action {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 35px;
            height: 35px;
            border-radius: 50%;
            border: none;
            transition: all 0.2s ease;
            text-decoration: none !important;
        }
        .btn-edit { background-color: #cfe2ff; color: #0d6efd; }
        .btn-edit:hover { background-color: #0d6efd; color: #fff; }
        
        .btn-save { background-color: #d1e7dd; color: #198754; }
        .btn-save:hover { background-color: #198754; color: #fff; }
        
        .btn-cancel { background-color: #e2e3e5; color: #6c757d; }
        .btn-cancel:hover { background-color: #6c757d; color: #fff; }
        
        .empty-state { text-align: center; padding: 4rem; }
        .empty-state .fa-icon { font-size: 3rem; color: #ced4da; margin-bottom: 1rem; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="page-header text-center mb-5">
            <h1 class="display-5 fw-bold">Staff Dashboard</h1>
            <p class="lead text-muted">Manage the pharmaceutical drug inventory.</p>
        </div>
        
        <div class="content-card">
            <div class="card-header">
                <h5 class="mb-0"><i class="fas fa-edit me-2 text-primary"></i>Drug Inventory Management</h5>
            </div>
            <div class="card-body p-0 p-md-3">
                 <div class="table-responsive">
                    <asp:GridView ID="gvDrugs" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-hover align-middle mb-0" 
                        DataKeyNames="drug_id"
                        OnRowEditing="gvDrugs_RowEditing" OnRowCancelingEdit="gvDrugs_RowCancelingEdit"
                        OnRowUpdating="gvDrugs_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="drug_id" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField DataField="drug_name" HeaderText="Drug Name" ReadOnly="true" />
                            <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate><%# Eval("unit_price", "{0:C}") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control edit-mode-input" Text='<%# Bind("unit_price", "{0:F2}") %>' TextMode="Number" step="0.01"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Stock">
                                <ItemTemplate><%# Eval("quantity_in_stock") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control edit-mode-input" Text='<%# Bind("quantity_in_stock") %>' TextMode="Number"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <%-- ** THE FIX IS HERE ** --%>
                            <%-- We replace the broken CommandField with a more flexible TemplateField --%>
                            <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-action btn-edit" CommandName="Edit" ToolTip="Edit">
                                        <i class="fas fa-pencil-alt"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="d-flex justify-content-center gap-2">
                                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn-action btn-save" CommandName="Update" ToolTip="Save">
                                            <i class="fas fa-check"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn-action btn-cancel" CommandName="Cancel" ToolTip="Cancel" CausesValidation="false">
                                            <i class="fas fa-times"></i>
                                        </asp:LinkButton>
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="empty-state">
                                <i class="fas fa-pills fa-icon"></i>
                                <h5 class="text-muted">No drugs found in inventory.</h5>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
