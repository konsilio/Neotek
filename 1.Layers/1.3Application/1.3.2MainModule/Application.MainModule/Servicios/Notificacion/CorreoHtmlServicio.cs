using Application.MainModule.Servicios.Catalogos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Servicios.Notificacion
{
    public static class CorreoHtmlServicio
    {
        private static string _rutaPlantillas = ConfigurationManager.AppSettings["RutaPlantillasHtml"];

        public static string RequisicionNueva(Sagas.MainModule.Entidades.Requisicion req)
        {
            string ruta = Convertir.GetPhysicalPath(string.Concat(_rutaPlantillas, "RequisicionNueva.html"));
            string html = FileUtilities.ObtenerContenido(ruta);

            html = html.Replace("[[TITULO]]", "NUEVA REQUISICIÓN");
            html = html.Replace("[[SUBTITULO]]", "Se requiere de s revisión para la siguiente requisición");
            html = html.Replace("[[FECHA]]", DateTime.Now.ToShortDateString());
            html = html.Replace("[[IR_A_SAGAS]]", Convertir.GetUrlBasePath());
            html = html.Replace("[[NUM_REQ]]", req.NumeroRequisicion);
            html = html.Replace("[[FECHA_REQUISICION]]", req.FechaRequerida.ToShortDateString());
            html = html.Replace("[[SOLICITANTE]]", UsuarioServicio.ObtenerNombreCompleto(req.Solicitante));
            html = html.Replace("[[MOTIVO]]", req.MotivoRequisicion);
            html = html.Replace("[[WebAppURL]]", ConfigurationManager.AppSettings["RutaPlantillasHtml"])

            return html;
        }
    }
}
