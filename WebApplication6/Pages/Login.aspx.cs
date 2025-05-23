using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class Login : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                User user = master.DataManager.GetUser(Request.Form["email"]);
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
                Session["token"] = user.Token();
                Session["login"] = true;
                if (Request.QueryString.Get("t") != null)
                {
                    Response.Redirect("/Pages/Tablature?t=" + Request.QueryString.Get("t"));
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
    }
}