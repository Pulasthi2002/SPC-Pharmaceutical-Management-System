using System;
using System.Web;
using System.Web.Security;

namespace Spc_web
{
    public partial class Spc : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This logic runs on every page that uses the master page.
            // It checks if the user is currently logged in.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // USER IS LOGGED IN
                // Show the Logout button and hide the Sign Up button.
                liSignUp.Visible = false;
                liLogout.Visible = true;
            }
            else
            {
                // USER IS NOT LOGGED IN
                // Show the Sign Up button and hide the Logout button.
                liSignUp.Visible = true;
                liLogout.Visible = false;
            }
        }

        // This method is now connected to the Logout LinkButton.
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Sign out the user using Forms Authentication.
            FormsAuthentication.SignOut();

            // Clear and abandon the session to remove all session data.
            Session.Clear();
            Session.Abandon();

            // Redirect the user to the Login page.
            Response.Redirect("~/Login.aspx", true);
        }
    }
}
