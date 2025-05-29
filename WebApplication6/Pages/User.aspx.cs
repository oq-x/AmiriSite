using System;
using System.Net.Mail;
using System.Web.UI;

namespace WebApplication6
{
    public partial class UserPage : Page
    {
        protected User user;
        protected Tablature[] tablatures;
        protected Post[] posts;
        protected SiteMaster master => (SiteMaster)Master;
        protected bool canEdit;
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["n"];
            if (name != null) {
                user = master.DataManager.GetUserByUsername(name);
            } else
            {
                try
                {
                    string uuid = Request.QueryString["u"] ?? "";
                    user = master.DataManager.GetUser(Guid.Parse(uuid));
                } catch { }
            }
            if (user == null)
            {
                User me = master.CurrentUser();
                if (me != null)
                {
                    Response.Redirect("User?n=" + me.Username);
                    return;
                }
                else
                {
                    Response.Redirect("/");
                    return;
                }
            }
            else
            {
                tablatures = master.DataManager.GetTablatures(user);
                posts = master.DataManager.GetPosts(user);

                canEdit = (master.CurrentUser()?.Is(user) ?? false) || (master.CurrentUser()?.Admin ?? false);
                Page.Title = user.Username;

                if (canEdit && !IsPostBack)
                {
                    emailInput.Text = user.Email;
                    bioInput.Text = user.Bio();
                }
            }
        }

        protected void SubmitUserEdit(object sender, EventArgs e)
        {
            if (!IsEmailValid())
            {
                error.InnerText = "Invalid email address!";
                error.Style["display"] = "block";
                return;
            }

            string email = emailInput.Text;
            string bio = bioInput.Text;

            user.Email = email;
            user.SetBio(bio);

            master.DataManager.UpdateUser(user);

            Response.Redirect(Request.RawUrl);
        }
        protected bool IsEmailValid()
        {
            string email = emailInput.Text;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}