using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace Spc_web
{
    public partial class AddToOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["drug_id"] == null)
                {
                    Response.Redirect("Pharmacy.aspx");
                }

                int drugId = Convert.ToInt32(Request.QueryString["drug_id"]);
                hdnDrugId.Value = drugId.ToString();
                LoadDrugDetails(drugId);
            }
        }

        private void LoadDrugDetails(int drugId)
        {
            string query = "SELECT * FROM drugs WHERE drug_id = @drugId";
            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@drugId", drugId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtDrugName.Text = reader["drug_name"].ToString();
                        txtUnitPrice.Text = reader["unit_price"].ToString();
                    }
                }
            }
        }

        protected void btnAddToOrder_Click(object sender, EventArgs e)
        {
            int drugId = Convert.ToInt32(hdnDrugId.Value);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            decimal totalPrice = unitPrice * quantity;

            DataTable dtOrderItems = (DataTable)Session["OrderItems"];
            bool exists = false;
            foreach (DataRow row in dtOrderItems.Rows)
            {
                if ((int)row["drug_id"] == drugId)
                {
                    row["quantity"] = (int)row["quantity"] + quantity;
                    row["total_price"] = (decimal)row["unit_price"] * (int)row["quantity"];
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                DataRow newRow = dtOrderItems.NewRow();
                newRow["drug_id"] = drugId;
                newRow["drug_name"] = txtDrugName.Text;
                newRow["quantity"] = quantity;
                newRow["unit_price"] = unitPrice;
                newRow["total_price"] = totalPrice;
                dtOrderItems.Rows.Add(newRow);
            }
            Session["OrderItems"] = dtOrderItems;
            Response.Redirect("Pharmacy.aspx");
        }
    }
}
