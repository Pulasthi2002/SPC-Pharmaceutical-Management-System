using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Spc_web
{
    public partial class staff : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDrugs();
            }
        }

        private void LoadDrugs()
        {
            string query = "SELECT drug_id, drug_name, generic_name, manufacturer, unit_price, quantity_in_stock FROM drugs";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
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
        }

        protected void gvDrugs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDrugs.EditIndex = e.NewEditIndex;
            LoadDrugs();
        }

        protected void gvDrugs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDrugs.EditIndex = -1;
            LoadDrugs();
        }

        protected void gvDrugs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int drugId = Convert.ToInt32(gvDrugs.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvDrugs.Rows[e.RowIndex];

            // **THE FIX IS HERE**: We now find controls by their specific ID.
            // This is much safer and more reliable than using cell indexes.
            TextBox txtPrice = (TextBox)row.FindControl("txtUnitPrice");
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

            decimal newPrice = Convert.ToDecimal(txtPrice.Text);
            int newQuantity = Convert.ToInt32(txtQuantity.Text);

            // Using e.OldValues is still a good way to get the original data
            decimal oldPrice = Convert.ToDecimal(e.OldValues["unit_price"]);
            int oldQuantity = Convert.ToInt32(e.OldValues["quantity_in_stock"]);

            // Store values in session for the next page
            Session["UpdateData"] = new
            {
                DrugId = drugId,
                OldPrice = oldPrice,
                NewPrice = newPrice,
                OldQuantity = oldQuantity,
                NewQuantity = newQuantity
            };

            // Exit edit mode
            gvDrugs.EditIndex = -1;
            LoadDrugs();

            // Redirect to the details page
            Response.Redirect($"stinfo.aspx?drug_id={drugId}");
        }

        // We no longer need the gvDrugs_RowDataBound event for styling,
        // as the CSS classes are now applied directly in the .aspx markup.
        // You can safely delete the old RowDataBound method.
    }
}
