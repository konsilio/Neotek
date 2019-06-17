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
using Application.MainModule.Servicios;
using System.Globalization;

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
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-MX");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void EjecutaServicios(object source, ElapsedEventArgs e)
        {
            //QUITAR ESTA LINEA EN PRODUCCION/////////////
           myTimer.Stop();
            //QUITAR ESTA LINEA EN PRODUCCION/////////////
            ActualizarSistemaServicio.Actualizar();
        }

        private void Timer()
        {
            myTimer = new Timer()
            {
                // Los milisegundos estan declarados en el web.config 
                Interval = Convert.ToDouble(ConfigurationManager.AppSettings["GlobalTimerTime"]),
                AutoReset = true,
                Enabled = true,
            };

            myTimer.Elapsed += new ElapsedEventHandler(EjecutaServicios);
        }

        private Timer myTimer;
    }
}
