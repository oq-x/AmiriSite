using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class TablaturePage : Page
    {
        protected User creator;
        protected Tablature tab;
        protected SiteMaster master => (SiteMaster)Master;
        protected Comment[] comments;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["t"];
            tab = master.DataManager.GetTablature(id);
            if (tab == null)
            {
                Response.Redirect("/");
                return;
            }
            creator = master.DataManager.GetUser(tab.PosterUUID);
            Page.Title = $"{tab.ArtistName} - {tab.SongName}";
            comments = master.DataManager.GetComments(tab);
        }
    }
}