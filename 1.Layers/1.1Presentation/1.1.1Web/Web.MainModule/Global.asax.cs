﻿using System;
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
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                string lcReqPath = Request.Path.ToLower();

                // Session is not stable in AcquireRequestState - Use Current.Session instead.
                System.Web.SessionState.HttpSessionState curSession = HttpContext.Current.Session;
                // If we do not have a OK Logon (remember Session["LogonOK"] = null; on logout, and set to true on logon.)
                //  and we are not already on loginpage, redirect.

                // note: on missing pages curSession is null, Test this without 'curSession == null || ' and catch exception.
                if (lcReqPath != "/login" && (curSession == null || curSession["StringToken"] == null))
                {
                    // Redirect nicely
                    Context.Server.ClearError();
                    Context.Response.AddHeader("Location", "Login.aspx");
                    Context.Response.TrySkipIisCustomErrors = true;
                    Context.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                    // End now end the current request so we dont leak.
                    Context.Response.Output.Close();
                    Context.Response.End();
                    return;
                }
            }
            catch (Exception)
            {

                // todo: handle exceptions nicely!
            }
        }
        protected void Session_End(object sender, EventArgs e)
        {
            Application_AcquireRequestState(sender, e);
        }
    }
}