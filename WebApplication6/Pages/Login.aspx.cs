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
                master.SetCurrentUser(user);

                Application["numUsersTotal"] = (int)Application["numUsersTotal"] + 1;
                if (Request.QueryString.Get("t") != null)
                {
                    Response.Redirect("/Pages/Tablature?t=" + Request.QueryString.Get("t"));
                }
                else if (Request.QueryString.Get("p") != null)
                {
                    Response.Redirect("/Pages/Post?p=" + Request.QueryString.Get("p"));
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
    }
}