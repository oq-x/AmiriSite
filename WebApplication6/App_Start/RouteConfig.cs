using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebApplication6
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.Add(new Route("", new RedirectRoute("/Home.aspx")));
            routes.Add("AspxRedirect", new Route("{page}.aspx", new RedirectAspxRouteHandler()));
        }
    }

    public class RedirectRoute : IRouteHandler
    {
        private readonly string _redirectUrl;

        public RedirectRoute(string redirectUrl)
        {
            _redirectUrl = redirectUrl;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            HttpContext.Current.Response.Redirect(_redirectUrl, true);
            return null;
        }
    }

    public class RedirectAspxRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string page = requestContext.RouteData.Values["page"] as string;
            string query = requestContext.HttpContext.Request.Url.Query;
            string redirectUrl = "/Pages/" + page + ".aspx" + query;
            requestContext.HttpContext.Response.RedirectPermanent(redirectUrl);
            return null;
        }
    }
}
