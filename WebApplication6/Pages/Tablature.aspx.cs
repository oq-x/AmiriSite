using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WebApplication6
{
    public partial class TablaturePage : Page
    {
        protected User creator;
        protected Tablature tab;
        protected SiteMaster master => (SiteMaster)Master;
        protected List<Comment> comments;
        protected User user => master.CurrentUser();
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
            comments = master.DataManager.GetComments(tab).ToList();
        }

        protected void CommentSubmit(object sender, EventArgs e)
        {
            if (commentInput.Text == "") return;
            if (user == null) return;
            if (tab == null) return;
            Comment comment = new Comment(user, commentInput.Text, tab);
            master.DataManager.InsertComment(comment);

            Response.Redirect(Request.RawUrl);
        }
    }
}