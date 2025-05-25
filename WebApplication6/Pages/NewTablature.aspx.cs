using System;
using System.Linq;
using System.Web.UI;

namespace WebApplication6
{
    public partial class NewTablaturePage : Page
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
            if (songName.Text.Length < 3 || songName.Text.Length > 50)
            {
                errorMessage.InnerText = "Song name must be between 3 and 50 characters";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (artistName.Text.Length < 3 || artistName.Text.Length > 50)
            {
                errorMessage.InnerText = "Artist name must be between 3 and 50 characters";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (albumName.Text.Length < 3 || albumName.Text.Length > 50)
            {
                errorMessage.InnerText = "Album name must be between 3 and 50 characters";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (tuningType.Text.Length > 20)
            {
                errorMessage.InnerText = "Tuning type must not be longer than 50 characters";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (!new string[]{"Super Easy", "Easy", "Medium", "Hard", "Super Hard"}.Contains(difficulty.Value))
            {
                errorMessage.InnerText = "Invalid difficulty value";
                errorMessage.Style["display"] = "block";
                return;
            }
            int capoI;
            if (!int.TryParse(capo.Text, out capoI))
            {
                capoI = 0;
            }
            if (capoI > 15)
            {
                errorMessage.InnerText = "Invalid capo value (how do you install the capo that high?)";
                errorMessage.Style["display"] = "block";
                return;
            }
            int releaseYearI;
            if (!int.TryParse(releaseYear.Text, out releaseYearI) || releaseYearI > DateTime.Now.Year)
            {
                errorMessage.InnerText = "Invalid year";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (content.Text.Length < 200)
            {
                errorMessage.InnerText = "Tablature content must be at least 200 characters long";
                errorMessage.Style["display"] = "block";
                return;
            }
            if (string.IsNullOrWhiteSpace(tuningType.Text))
            {
                tuningType.Text = "Standard";
            }

            Tablature tab = new Tablature(user, songName.Text, artistName.Text, albumName.Text, releaseYearI, content.Text, tuningType.Text, difficulty.Value, capoI);
            master.DataManager.InsertTablature(tab);

            Response.Redirect($"Tablature?t={tab.UUID}");
        }
    }
}