using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using Application.MainModule.Servicios;

namespace GasMundialWS
{
    public partial class ActualizarSistema : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public ActualizarSistema()
        {
            InitializeComponent();
        }

        internal void TestStartupAndStop(string[] args)
        {
            ActualizarSistemaServicio.Actualizar();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            try
            {                
                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                timer.Interval = 1800000; //number in milisecinds 30 minutes  
                timer.Enabled = true;
            }
            catch (Exception ex) {
                WriteToFile("-----Service  failed (at: " + DateTime.Now + "):" + ex.ToString());
            }
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
            try
            {
                ActualizarSistemaServicio.Actualizar();
                WriteToFile("Service completed at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                WriteToFile("-----Service  failed (at: " + DateTime.Now + "):" + ex.ToString());
            }
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\GaseraServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
