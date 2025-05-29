using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class Statistics : Page
    {
        protected int numVisitorsOnline;
        protected int numVisitorsTotal;
        protected int numUsersTotal;
        protected void Page_Load(object sender, EventArgs e)
        {
            numVisitorsOnline = (int)Application["numVisitorsOnline"];
            numVisitorsTotal = (int)Application["numVisitorsTotal"];
            numUsersTotal = (int)Application["numUsersTotal"];
        }
    }
}