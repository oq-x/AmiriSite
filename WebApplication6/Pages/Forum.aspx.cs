using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class Forum : Page
    {
        protected Post[] posts;
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["q"] ?? "";
            posts = master.DataManager.GetPosts(query);

            if (!IsPostBack)
            {
                input.Text = query;
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