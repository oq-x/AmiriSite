using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class UserPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster master = (SiteMaster)Master;
            string name = Request.QueryString["n"];
            User me = master.CurrentUser();

            if (name == null || !master.DataManager.UserExistByUsername(name))
            {
                if (me != null)
                {
                    Response.Redirect("User?n=" + me.Username);
                    return;
                }
                else
                {
                    Response.Redirect("/");
                    return;
                }
            }
        }
    }
}