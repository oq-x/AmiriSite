using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WebApplication6
{
    public partial class PostPage : Page
    {
        protected User creator;
        protected Post post;
        protected SiteMaster master => (SiteMaster)Master;
        protected List<Comment> comments;
        protected User user => master.CurrentUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["p"];
            post = master.DataManager.GetPost(id);
            if (post == null)
            {
                Response.Redirect("/");
                return;
            }
            creator = master.DataManager.GetUser(post.PosterUUID);
            Page.Title = post.Title ;
            comments = master.DataManager.GetComments(post).ToList();
        }

        protected void CommentSubmit(object sender, EventArgs e)
        {
            if (commentInput.Text == "") return;
            if (user == null) return;
            if (post == null) return;
            Comment comment = new Comment(user, commentInput.Text, post);
            master.DataManager.InsertComment(comment);

            Response.Redirect(Request.RawUrl);
        }
    }
}