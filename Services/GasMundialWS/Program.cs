using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GasMundialWS
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                var start = DateTime.Now;
                ActualizarSistema service1 = new ActualizarSistema();
                service1.TestStartupAndStop(args);
                var stop = DateTime.Now;
                System.TimeSpan diff = start - stop;

            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ActualizarSistema()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
