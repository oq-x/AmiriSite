using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication6
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                switch (Request.Form["email"])
                {
                    case "amiri@gmail.com":
                        if (Request.Form["password"] == "amiripass")
                        {
                            Session["username"] = "amiri";
                            Session["login"] = true;

                            Session["error"] = null;

                            Response.Redirect("/");
                        } else
                        {
                            Session["error"] = "Wrong password!";
                        }
                            break;
                    default:
                        Session["error"] = "Unknown user!";
                        break;
                }
            }
        }
    }
}