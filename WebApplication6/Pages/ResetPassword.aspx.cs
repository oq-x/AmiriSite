using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class ResetPassword : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected User user
        {
            get
            {
                return Session["resetPasswordUserEmail"] is string email ? master.DataManager.GetUser(email) : null;
            }
            set
            {
                Session["resetPasswordUserEmail"] = value?.Email;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TryResetPassword(object sender, EventArgs e)
        {
            if (user == null)
            {
                error.InnerText = "Not ready!";
                return;
            }
            if (string.IsNullOrEmpty(securityAnswer.Text) || string.IsNullOrEmpty(newPassword.Text))
            {
                error.InnerText = "Please fill in all fields!";
                return;
            }
            if (newPassword.Text.Length < 8)
            {
                error.InnerText = "Password must be at least 8 characters long!";
                return;
            }

            string errorResult = master.DataManager.TryResetPassword(user, newPassword.Text, securityQuestion.InnerText, securityAnswer.Text);

            if (errorResult != "")
            {
                error.InnerText = errorResult;
                return;
            }
            
            master.SetCurrentUser(user);
            user = null;
            Response.Redirect("/");
        }

        protected void LookupUser(object sender, EventArgs e)
        {
            user = master.DataManager.GetUser(email.Text);
            if (user == null)
            {
                error.InnerText = "User not found!";
                return;
            }
            error.InnerText = "";
            email.Enabled = false;
            securityQuestion.InnerText = user.SecurityQuestion;

            if (user.SecurityQuestion == "")
            {
                error.InnerText = "This user has no security question and cannot have their password reset!";
                submit.Enabled = false;
                return;
            }

            secql.Style["display"] = "block";
            securityAnswer.Style["display"] = "block";

            npassl.Style["display"] = "block";
            newPassword.Style["display"] = "block";

            submit.Style["display"] = "none";
            resetPassword.Style["display"] = "block";
        }
    }
}