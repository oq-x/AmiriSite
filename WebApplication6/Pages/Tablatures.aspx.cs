using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class TablaturesPage : Page
    {
        protected Tablature[] tablatures;
        protected User[] creators;
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["q"] ?? "";
            tablatures = master.DataManager.GetTablatures(query);

            if (!IsPostBack)
            {
                input.Text = query;
            }

            creators = new User[tablatures.Length];
            for (int i = 0; i < tablatures.Length; i++)
            {
                creators[i] = master.DataManager.GetUser(tablatures[i].PosterUUID);
            }
        }

        protected void Input(object sender, EventArgs e)
        {
            string query = input.Text.Trim();

            string url = Request.Url.AbsolutePath + "?q=" + Server.UrlEncode(query);
            Response.Redirect(url);
        }
    }
}