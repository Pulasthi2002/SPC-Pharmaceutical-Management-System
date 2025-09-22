using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spc_web
{
    public partial class Pharmacy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDrugs();
                InitializeOrder();
            }
        }

        private void LoadDrugs()
        {
            string query = "SELECT * FROM drugs WHERE quantity_in_stock > 0";
            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvDrugs.DataSource = dt;
                    gvDrugs.DataBind();
                }
            }
        }

        private void InitializeOrder()
        {
            if (Session["OrderItems"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("drug_id", typeof(int));
                dt.Columns.Add("drug_name", typeof(string));
                dt.Columns.Add("quantity", typeof(int));
                dt.Columns.Add("unit_price", typeof(decimal));
                dt.Columns.Add("total_price", typeof(decimal));
                Session["OrderItems"] = dt;
            }
            BindOrderItems();
        }

        protected void gvDrugs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OrderDrug")
            {
                int drugId = Convert.ToInt32(e.CommandArgument);
                
                // Redirect to order page with drug ID
                Response.Redirect($"AddToOrder.aspx?drug_id={drugId}");
            }
        }

        private void BindOrderItems()
        {
            DataTable dtOrderItems = (DataTable)Session["OrderItems"];
            gvOrderItems.DataSource = dtOrderItems;
            gvOrderItems.DataBind();

            decimal total = 0;
            foreach (DataRow row in dtOrderItems.Rows)
            {
                total += Convert.ToDecimal(row["total_price"]);
            }
            litTotalAmount.Text = total.ToString("0.00");
        }

        protected void gvOrderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dtOrderItems = (DataTable)Session["OrderItems"];
            dtOrderItems.Rows.RemoveAt(e.RowIndex);
            Session["OrderItems"] = dtOrderItems;
            BindOrderItems();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx");
        }
    }
}
