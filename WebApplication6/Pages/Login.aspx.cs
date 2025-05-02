using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication6
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                SiteMaster m = (SiteMaster)Master;
                User user = m.dataManager.GetUser(Request.Form["email"]);
                if (user == null)
                {
                    Session["error"] = "Unknown user!";
                    return;
                }
                if (!user.IsPasswordEqual(Request.Form["password"]))
                {
                    Session["error"] = "Wrong password!";
                    return;
                }
                Session["username"] = user.Username();
                Session["login"] = true;
                Response.Redirect("/");
            }
        }
    }
}