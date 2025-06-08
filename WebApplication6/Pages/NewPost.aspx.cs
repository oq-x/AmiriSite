using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class NewPostPage : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected User user => master.CurrentUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (user == null)
            {
                Response.Redirect("Login");
                return;
            }
        }

        protected void Publish(object sender, EventArgs e)
        {
            if (user == null)
            {
                Response.Redirect("Login");
                return;
            }
            if (title.Text.Length < 10 || title.Text.Length > 100)
            {
                errorMessage.InnerText = "Title must be between 10 and 100 characters";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (content.Text.Length < 10)
            {
                errorMessage.InnerText = "Post content must be at least 10 characters long";
                errorMessage.Style["display"] = "block";
                return;
            }

            Post post = new Post(user, content.Text, title.Text);
            master.DataManager.InsertPost(post);

            Response.Redirect($"Post?p={post.UUID}");
        }
    }
}