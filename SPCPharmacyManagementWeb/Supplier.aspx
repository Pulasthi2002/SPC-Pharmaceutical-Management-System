<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" 
    Inherits="Spc_web.Supplier" MasterPageFile="~/Spc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* --- General Page Styling --- */
        .dashboard-header {
            animation: fadeInDown 0.6s ease-out;
        }

        .content-card {
            border: none;
            border-radius: var(--border-radius);
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
            animation: fadeInUp 0.8s ease-out;
            animation-fill-mode: both;
        }

        .content-card .card-header {
            background-color: transparent;
            border-bottom: 1px solid #e9ecef;
            padding: 1.25rem 1.5rem;
            font-weight: 600;
            font-size: 1.15rem;
            color: var(--dark-color);
        }

        /* --- Tender Card Styling --- */
        .tender-card {
            border: 1px solid #e9ecef;
            border-radius: var(--border-radius);
            transition: all 0.3s ease;
            background-color: #fff;
        }

        .tender-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
            border-color: var(--primary-color);
        }

        .tender-card .card-body {
            display: flex;
            flex-direction: column;
            height: 100%;
        }
        
        .tender-title {
            color: var(--dark-color);
            font-weight: 600;
        }

        .tender-meta {
            font-size: 0.9rem;
            color: #6c757d; /* Bootstrap's text-muted color */
        }

        .tender-items-list {
            margin-top: 1rem;
            margin-bottom: 1.5rem;
        }
        
        .tender-card .card-footer {
            background-color: transparent;
            border-top: 1px solid #e9ecef;
            padding: 1rem 1.25rem;
        }

        /* --- Modern Status Badge Styling (for Proposals Table) --- */
        .badge-status {
            padding: 0.4em 0.8em;
            font-weight: 600;
            font-size: 0.8rem;
            border-radius: 50px; /* Pill shape */
        }
        /* These classes are designed to work with your existing GetStatusClass() backend function */
        .bg-success-subtle { background-color: #d1e7dd !important; }
        .text-success-emphasis { color: #0f5132 !important; }
        .bg-warning-subtle { background-color: #fff3cd !important; }
        .text-warning-emphasis { color: #664d03 !important; }
        .bg-danger-subtle { background-color: #f8d7da !important; }
        .text-danger-emphasis { color: #58151c !important; }
        .bg-info-subtle { background-color: #cff4fc !important; }
        .text-info-emphasis { color: #055160 !important; }

        /* --- Proposals GridView Styling --- */
        .table {
            border-collapse: separate;
            border-spacing: 0;
        }
        .table thead th {
            background-color: #f8f9fa;
            color: #343a40;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.85rem;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #dee2e6;
        }
        .table tbody td {
            vertical-align: middle;
        }
        .table-hover tbody tr:hover {
            background-color: rgba(0, 123, 255, 0.05);
        }

        .empty-data {
            text-align: center;
            padding: 4rem;
        }
        .empty-data .fa-info-circle {
            font-size: 3rem;
            color: var(--primary-color);
            opacity: 0.6;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <!-- Page Header -->
        <div class="text-center mb-5 dashboard-header">
            <h1 class="display-5 fw-bold">Supplier Dashboard</h1>
            <p class="lead text-muted">Browse available tenders and manage your proposals.</p>
        </div>

        <!-- Active Tenders Section -->
        <div class="mb-5">
            <h2 class="mb-4"><i class="fas fa-bullhorn me-2 text-primary"></i>Active Tenders</h2>
            <div class="row g-4">
                <asp:Repeater ID="rptTenders" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 col-lg-4 d-flex align-items-stretch">
                            <div class="card tender-card w-100">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between align-items-start mb-2">
                                        <h4 class="card-title tender-title mb-0"><%# Eval("title") %></h4>
                                        <span class="badge rounded-pill text-bg-primary"><%# Eval("status") %></span>
                                    </div>
                                    <div class="tender-meta mb-3">
                                        <span><i class="fas fa-calendar-alt me-1"></i> Deadline: <%# Convert.ToDateTime(Eval("deadline_date")).ToString("MMM dd, yyyy") %></span>
                                    </div>
                                    <p class="card-text text-muted"><%# Eval("description") %></p>
                                    
                                    <div class="mt-auto"> <%-- Pushes content below it to the bottom --%>
                                        <h6 class="fw-bold">Items Required:</h6>
                                        <ul class="list-group list-group-flush tender-items-list">
                                            <asp:Repeater ID="rptTenderItems" runat="server" DataSource='<%# Eval("Items") %>'>
                                                <ItemTemplate>
                                                    <li class="list-group-item d-flex justify-content-between align-items-center px-0">
                                                        <%# Eval("drug_name") %>
                                                        <span class="badge text-bg-secondary rounded-pill"><%# Eval("required_quantity") %></span>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <asp:HyperLink runat="server"
                                        NavigateUrl='<%# "ApplyTender.aspx?tender_id=" + Eval("tender_id") %>'
                                        CssClass="btn btn-primary w-100 fw-bold">
                                        <i class="fas fa-edit me-2"></i>View & Apply
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Proposals History Section -->
        <div class="content-card">
            <div class="card-header">
                <h5><i class="fas fa-history me-2"></i>My Proposal History</h5>
            </div>
            <div class="card-body p-0 p-md-3">
                 <div class="table-responsive">
                    <asp:GridView ID="gvProposals" runat="server" 
                        CssClass="table table-hover align-middle mb-0" 
                        AutoGenerateColumns="false" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="tender_id" HeaderText="Tender ID" />
                            <asp:BoundField DataField="title" HeaderText="Tender Title" />
                            <asp:BoundField DataField="proposal_date" HeaderText="Proposal Date" DataFormatString="{0:MMM dd, yyyy}" />
                            <asp:BoundField DataField="total_proposed_amount" HeaderText="Proposed Amount" DataFormatString="{0:C}" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span class='badge-status <%# GetStatusClass(Eval("status").ToString()) %>'>
                                        <i class='fas <%# GetStatusIcon(Eval("status").ToString()) %> me-1'></i>
                                        <%# Eval("status") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="empty-data">
                                <i class="fas fa-info-circle mb-3"></i>
                                <h4 class="text-muted">No proposals found.</h4>
                                <p>You have not submitted any proposals yet.</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
