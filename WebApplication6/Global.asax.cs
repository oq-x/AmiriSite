using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication6
{
    public class Global : HttpApplication
    {
        public const string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        public DataManager DataManager => new DataManager(conString);
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["dataManager"] = DataManager;
            Application["numVisitorsOnline"] = 0;
            Application["numUsersTotal"] = 0;
            Application["numVisitorsTotal"] = 0;
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application["numVisitorsOnline"] = (int)Application["numVisitorsOnline"] + 1;
            Application["numVisitorsTotal"] = (int)Application["numVisitorsTotal"] + 1;
        }

        void Session_End(object sender, EventArgs e)
        {
            Application["numVisitorsOnline"] = (int)Application["numVisitorsOnline"] - 1;
        }
    }
}