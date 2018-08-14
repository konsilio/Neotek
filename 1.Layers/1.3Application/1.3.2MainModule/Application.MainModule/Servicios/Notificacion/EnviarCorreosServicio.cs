using Mail.MainModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Notificacion
{
    public class EnviarCorreosServicio
    {
        EnviarCorreos _enviarCorreos = new EnviarCorreos();
        public void EnviarCorreo(MensajeCorreo datos)
        {
            //_enviarCorreos.De = datos.De;
            //if (!string.IsNullOrEmpty(datos.Para))
            //    _enviarCorreos.Para = datos.Para;
            //_enviarCorreos.Mensaje = datos.Mensaje;
            //_enviarCorreos.Asunto = datos.Asunto;

            //if (datos.ParaLista != null && datos.ParaLista.Count > 0)
            //    foreach (string para in datos.ParaLista)
            //        _enviarCorreos.Para = para;

            //if (datos.ConCopia != null && datos.ConCopia.Count > 0)
            //{
            //    for (int i = 0; i < datos.ConCopia.Count; i++)
            //    {
            //        _enviarCorreos.ConCopia = datos.ConCopia[i].ToString();
            //    }
            //}

            //if (datos.ConCopiaOculta != null && datos.ConCopiaOculta.Count > 0)
            //{
            //    for (int i = 0; i < datos.ConCopiaOculta.Count; i++)
            //    {
            //        _enviarCorreos.ConCopiaOculta = datos.ConCopiaOculta[i].ToString();
            //    }
            //}

            //_enviarCorreos.RutaArchivoAdjunto = datos.RutaArchivo;
            _enviarCorreos.EnviarCorreo();
        }
    }
}
