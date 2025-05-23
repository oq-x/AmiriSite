﻿using System;
using System.Web.UI;

namespace WebApplication6
{
    public partial class UserPage : Page
    {
        protected User user;
        protected Tablature[] tablatures;
        protected SiteMaster master => (SiteMaster)Master;
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
                Page.Title = user.Username;
            }
        }
    }
}