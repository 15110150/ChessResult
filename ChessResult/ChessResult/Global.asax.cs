using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Data;

namespace ChessResult
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DbSettingProviderManager.StartNewSession()
              .UseDefaultConnectionStringProvider()
              .UseDefaultDbSettingProvider(Server.MapPath("~/App_Data"), ignoreRedundantParameters: false, enableWatching: true)
              .Run();
        }
    }
}