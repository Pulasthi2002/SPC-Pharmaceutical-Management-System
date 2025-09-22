using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace Spc_web
{
    public partial class stinfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UpdateData"] == null || Request.QueryString["drug_id"] == null)
                {
                    Response.Redirect("staff.aspx");
                }
            }
        }

        protected void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if (Session["UpdateData"] == null) return;

            dynamic updateData = Session["UpdateData"];
            int drugId = updateData.DrugId;
            int newQuantity = updateData.NewQuantity;
            int oldQuantity = updateData.OldQuantity;
            int quantityChange = newQuantity - oldQuantity;

            string updateType = ddlUpdateType.SelectedValue;
            string reason = txtReason.Text;
            string reference = txtReference.Text;

            // Get staff ID from session (replace with your actual session variable)
            int staffId = 1;

            string query = @"INSERT INTO stock_updates 
                            (drug_id, quantity, update_type, reason, updated_by, reference_number) 
                            VALUES (@drug_id, @quantity, @type, @reason, @staff_id, @ref)";

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@drug_id", drugId);
                cmd.Parameters.AddWithValue("@quantity", quantityChange);
                cmd.Parameters.AddWithValue("@type", updateType);
                cmd.Parameters.AddWithValue("@reason", reason);
                cmd.Parameters.AddWithValue("@staff_id", staffId);
                cmd.Parameters.AddWithValue("@ref", reference);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Update drugs table with new quantity
            string updateQuery = "UPDATE drugs SET quantity_in_stock = @qty WHERE drug_id = @id";
            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@qty", newQuantity);
                cmd.Parameters.AddWithValue("@id", drugId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Clear session and redirect
            Session.Remove("UpdateData");
            Response.Redirect("staff.aspx");
        }
    }
}
