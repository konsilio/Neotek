using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;
using System.Configuration;

namespace DS.MainModule
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.Timer();
        }

        public void EjecutaServicios(object source, ElapsedEventArgs e)
        {
            //Notificacion24Hrs.MailDisponibilidad();
            //servTipoCambio.GenerarTipoCambioDelDia();
        }

        private void Timer()
        {
            Timer myTimer = new Timer()
            {
                // Los milisegundos estan declarados en el web.config 
                Interval = Convert.ToDouble(ConfigurationManager.AppSettings["GlobalTimerTime"]),
                AutoReset = true,
                Enabled = true,
            };

            myTimer.Elapsed += new ElapsedEventHandler(EjecutaServicios);
        }
    }
}
