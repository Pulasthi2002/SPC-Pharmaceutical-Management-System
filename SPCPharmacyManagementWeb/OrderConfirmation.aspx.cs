using System;
using System.Web.UI;

namespace Spc_web
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["order_id"] != null)
            {
                litOrderId.Text = Request.QueryString["order_id"];
            }
        }

        // FIX: Add this method to handle the button click
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pharmacy.aspx");
        }
    }
}
