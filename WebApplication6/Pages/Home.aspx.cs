using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class Home : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected User user => master.CurrentUser();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}