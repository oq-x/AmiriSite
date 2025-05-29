using System;
using System.Web;
using System.Web.UI;

namespace WebApplication6
{
    public partial class UserList : Page
    {
        protected SiteMaster master => (SiteMaster)Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["q"] ?? "";
            string orderByColumn = Request.QueryString["c"] ?? "";
            string order = Request.QueryString["o"];
            if (order != "ASC" && order != "DESC")
            {
                order = "ASC";
            }
            User me = master.CurrentUser();
            if (me == null || !me.Admin)
            {
                Response.Redirect("/");
                return;
            }
            table.InnerHtml = master.DataManager.BuildUsersTable(query, orderByColumn, order);

            if (!IsPostBack)
            {
                input.Text = query;
                column.Text = orderByColumn;
                asc.Checked = order == "ASC";
                desc.Checked = order == "DESC";
            }
        }
        protected void Input(object sender, EventArgs e)
        {
            string query = input.Text.Trim();
            var uriBuilder = new UriBuilder(Request.Url);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryParams["q"] = query;

            uriBuilder.Query = queryParams.ToString();

            Response.Redirect(uriBuilder.ToString());
        }

        protected void InputColumn(object sender, EventArgs e)
        {
            string query = column.Text.Trim();
            var uriBuilder = new UriBuilder(Request.Url);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryParams["c"] = query;

            uriBuilder.Query = queryParams.ToString();

            Response.Redirect(uriBuilder.ToString());
        }

        protected void InputOrder(object sender, EventArgs e)
        {
            bool isAsc = asc.Checked;
            bool isDesc = desc.Checked;

            var uriBuilder = new UriBuilder(Request.Url);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryParams["o"] = isAsc ? "ASC" : "DESC";

            uriBuilder.Query = queryParams.ToString();

            Response.Redirect(uriBuilder.ToString());
        }
    }
}