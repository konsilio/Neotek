using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Web.MainModule
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_AcquireRequestState(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string lcReqPath = Request.Path.ToLower();
        //        System.Web.SessionState.HttpSessionState curSession = HttpContext.Current.Session;
        //        if (lcReqPath != "/login" && (curSession == null || curSession["StringToken"] == null))
        //        {
        //            Context.Server.ClearError();
        //            Context.Response.AddHeader("Location", "Login.aspx");
        //            Context.Response.TrySkipIisCustomErrors = true;
        //            Context.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
        //            Context.Response.Output.Close();
        //            Context.Response.End();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // todo: handle exceptions nicely!
        //    }
        //}
        //protected void Session_End(object sender, EventArgs e)
        //{
        //    Application_AcquireRequestState(sender, e);
        //}
    }
}