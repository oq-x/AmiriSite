using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class UserList : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*User me = master.CurrentUser();
            if (me == null || !me.Admin)
            {
                Response.Redirect("/");
                return;
            }*/
            table.InnerHtml = master.DataManager.BuildUsersTable();
        }
    }
}