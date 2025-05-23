using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class SiteMaster : MasterPage
    {
        public DataManager DataManager => new DataManager("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\oq\\source\\repos\\WebApplication6\\WebApplication6\\App_Data\\Database1.mdf;Integrated Security=True");
        public string CurrentToken()
        {
            if (Session["login"] == null || !(bool)Session["login"]) return null;
            return (string)Session["token"];
        }
        public User CurrentUser()
        {
            string token = CurrentToken();
            if (token == null) return null;
            return DataManager.Authenticate(token);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}