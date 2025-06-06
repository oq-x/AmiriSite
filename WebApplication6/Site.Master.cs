using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class SiteMaster : MasterPage
    {
        public DataManager DataManager => (DataManager)Application["dataManager"];
        public string CurrentToken()
        {
            if (Session["login"] == null || !(bool)Session["login"]) return null;
            return (string)Session["token"];
        }

        public void SetCurrentUser(string token)
        {
            Session["login"] = true;
            Session["token"] = token;
        }
        public void SetCurrentUser(User user)
        {
            SetCurrentUser(user.Token());
        }
        public User CurrentUser()
        {
            string token = CurrentToken();
            if (token == null) return null;
            return DataManager.Authenticate(token);

        }
        public void LogOut(object sender, EventArgs e)
        {
            Session.Abandon();

            Response.Redirect("/");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}