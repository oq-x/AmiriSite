using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class Register : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                string email = Request.Form["email"];
                string username = Request.Form["username"];
                string password = Request.Form["password"];

                string firstName = Request.Form["fname"];
                string lastName = Request.Form["lname"];
                string phone = Request.Form["phone"];
                string birthday = Request.Form["birthday"];

                string securityQuestion = Request.Form["securityQuestion"];
                string securityAnswer = Request.Form["securityAnswer"];

                if (username.Length < 3 || username.Length > 15)
                {
                    Session["error"] = "Username must be 3 to 15 characters long!";
                    return;
                }

                if (password.Length < 8)
                {
                    Session["error"] = "Password must be at least 8 characters long!";
                    return;
                }

                if (firstName.Length < 2 || firstName.Length > 15)
                {
                    Session["error"] = "First name must be 3 to 15 characters long!";
                    return;
                }

                if (lastName.Length < 2 || lastName.Length > 15)
                {
                    Session["error"] = "Last name must be 3 to 15 characters long!";
                    return;
                }

                bool existM = master.DataManager.UserExistByEmail(email);
                if (existM)
                {
                    Session["error"] = "This email is already taken!";
                    return;
                }

                bool existU = master.DataManager.UserExistByUsername(username);
                if (existU)
                {
                    Session["error"] = "This username is already taken!";
                    return;
                }

                User user = new User(email, username, password, firstName, lastName, phone, birthday, gender.Value, securityQuestion, securityAnswer);

                master.DataManager.CreateUser(user);

                Session["token"] = user.Token();
                Session["login"] = true;
                Application["numUsersTotal"] = (int)Application["numUsersTotal"] + 1;
                if (Request.QueryString.Get("t") != null)
                {
                    Response.Redirect("/Pages/Tablature?t=" + Request.QueryString.Get("t"));
                }
                else if (Request.QueryString.Get("p") != null)
                {
                    Response.Redirect("/Pages/Post?p=" + Request.QueryString.Get("p"));
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
    }
}