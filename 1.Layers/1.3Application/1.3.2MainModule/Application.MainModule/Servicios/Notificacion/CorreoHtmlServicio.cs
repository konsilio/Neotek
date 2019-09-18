using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
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
            html = html.Replace("[[SUBTITULO]]", "Se requiere de su revisión para la siguiente requisición");
            html = html.Replace("[[FECHA]]", DateTime.Now.ToShortDateString());
            html = html.Replace("[[IR_A_SAGAS]]", Convertir.GetUrlBasePath());
            html = html.Replace("[[NUM_REQ]]", req.NumeroRequisicion);
            html = html.Replace("[[FECHA_REQUISICION]]", req.FechaRequerida.ToShortDateString());
            html = html.Replace("[[SOLICITANTE]]", UsuarioServicio.ObtenerNombreCompleto(req.Solicitante));
            html = html.Replace("[[MOTIVO]]", req.MotivoRequisicion);
            html = html.Replace("[[WebAppURL]]", ConfigurationManager.AppSettings["RutaPlantillasHtml"]);

            return html;
        }
        public static string ProductoAutorizadoEntrega(Sagas.MainModule.Entidades.Requisicion req)
        {
            string ruta = Convertir.GetPhysicalPath(string.Concat(_rutaPlantillas, "RequisicionNueva.html"));
            string html = FileUtilities.ObtenerContenido(ruta);

            html = html.Replace("[[TITULO]]", "PRODUCTO REQUERIDO AUTORIZADO PARA ENTREGA");
            html = html.Replace("[[SUBTITULO]]", String.Format("Se ha autorizado la entrega de producto de la requisición: {0}.", req.IdRequisicion));
            html = html.Replace("[[FECHA]]", DateTime.Now.ToShortDateString());
            html = html.Replace("[[IR_A_SAGAS]]", Convertir.GetUrlBasePath());
            html = html.Replace("[[NUM_REQ]]", req.NumeroRequisicion);
            html = html.Replace("[[FECHA_REQUISICION]]", req.FechaRequerida.ToShortDateString());
            html = html.Replace("[[SOLICITANTE]]", UsuarioServicio.ObtenerNombreCompleto(req.Solicitante));
            html = html.Replace("[[MOTIVO]]", req.MotivoRequisicion);
            html = html.Replace("[[WebAppURL]]", ConfigurationManager.AppSettings["RutaPlantillasHtml"]);

            return html;
        }
        public static string OrdenCompraNueva(Sagas.MainModule.Entidades.OrdenCompra oc)
        {
            string ruta = Convertir.GetPhysicalPath(string.Concat(_rutaPlantillas, "OrdenCompraNueva.html"));
            string html = FileUtilities.ObtenerContenido(ruta);

            html = html.Replace("[[TITULO]]", "NUEVA ORDEN DE COMPRA");
            html = html.Replace("[[SUBTITULO]]", "Se requiere de su atencion para la siguiente orden de compra");
            html = html.Replace("[[FECHA]]", DateTime.Now.ToShortDateString());
            html = html.Replace("[[IR_A_SAGAS]]", Convertir.GetUrlBasePath());
            html = html.Replace("[[NUM_REQ]]", oc.NumOrdenCompra);
            html = html.Replace("[[FECHA_REQUISICION]]", oc.FechaRegistro.ToShortDateString());
            html = html.Replace("[[SOLICITANTE]]", UsuarioServicio.ObtenerNombreCompleto(oc.Requisicion.Solicitante));
            html = html.Replace("[[MOTIVO]]", oc.Requisicion.MotivoRequisicion);
            html = html.Replace("[[WebAppURL]]", ConfigurationManager.AppSettings["RutaPlantillasHtml"]);

            return html;
        }
        public static string SolicitudPago(Sagas.MainModule.Entidades.OrdenCompra oc)
        {
            string ruta = Convertir.GetPhysicalPath(string.Concat(_rutaPlantillas, "OrdenCompraNueva.html"));
            string html = FileUtilities.ObtenerContenido(ruta);

            html = html.Replace("[[TITULO]]", "SOLICITUD DE PAOG");
            html = html.Replace("[[SUBTITULO]]", "Se requiere de su atencion para el siguente pago de la orden de compra");
            html = html.Replace("[[FECHA]]", DateTime.Now.ToShortDateString());
            html = html.Replace("[[IR_A_SAGAS]]", Convertir.GetUrlBasePath());
            html = html.Replace("[[NUM_OC]]", oc.NumOrdenCompra);
            html = html.Replace("[[NUM_REQ]]", oc.Requisicion.NumeroRequisicion);
            html = html.Replace("[[FECHA_REQUISICION]]", oc.FechaRegistro.ToShortDateString());
            html = html.Replace("[[SOLICITANTE]]", UsuarioServicio.ObtenerNombreCompleto(oc.Requisicion.Solicitante));
            html = html.Replace("[[MONTO]]", oc.Total.ToString());
            html = html.Replace("[[WebAppURL]]", ConfigurationManager.AppSettings["RutaPlantillasHtml"]);

            return html;
        }
    }
}
