using System;
using System.Configuration;
using System.Web.UI;

namespace WebApplication6
{
    public partial class TablaturesPage : Page
    {
        protected Tablature[] tablatures;
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["q"] ?? "";
            tablatures = master.DataManager.GetTablatures(query);

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

        public static string FormatCapoFret(int capoFret)
        {
            if (capoFret == 0)
            {
                return "No";
            }
            int ten = capoFret / 10;
            int one = capoFret % 10;

            if (one == 1 && ten != 1)
            {
                return $"{capoFret}st fret";
            }
            else if (one == 2 && ten != 1)
            {
                return $"{capoFret}nd fret";
            }
            else if (one == 3 && ten != 1)
            {
                return $"{capoFret}rd fret";
            }
            else
            {
                return $"{capoFret}th fret";
            }
        }
    }
}