using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Spc_web
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDrugs();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDrugs(txtSearch.Text.Trim());
        }

        private void LoadDrugs(string search = "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["PharmacyDB"].ConnectionString;
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string query = "SELECT * FROM drugs";
                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE drug_name LIKE @search OR generic_name LIKE @search";
                }
                query += " ORDER BY drug_name";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    }

                    conn.Open();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                rptDrugs.DataSource = dt;
                rptDrugs.DataBind();
                lblNoDrugs.Visible = false;
            }
            else
            {
                rptDrugs.DataSource = null;
                rptDrugs.DataBind();
                lblNoDrugs.Text = "No medications found matching your search.";
                lblNoDrugs.Visible = true;
            }
        }
    }
}
