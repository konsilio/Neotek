using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;

namespace Utilities.MainModule
{
    public class PlantillasNotificacionesHTML
    {
        private string _rutaPlantillas;

        public PlantillasNotificacionesHTML()
        {
            _rutaPlantillas = ConfigurationManager.AppSettings["RutaPlantillasHtml"];
            // Obtiene la dirección completa del servidor web
            if (HttpContext.Current != null)
                _rutaPlantillas = HttpContext.Current.Server.MapPath(_rutaPlantillas);
            else
                _rutaPlantillas = string.Concat(HttpRuntime.AppDomainAppPath, _rutaPlantillas.Replace("~/", string.Empty).Replace("/", "\\"));
            
        }

        public string PlantillaMensajeSencillo(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaMensajeSencillo.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaComision(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaComision.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaPago(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaPago.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSolicitud(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSolicitud.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionAprobadaPago(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionAprobadaParaPago.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionSolicitarPago(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionSolicitarPago.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }        

        public string PlantillaNotificacionPagoRealizado(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionPagoRealizado.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaPagoConfirmado(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaPagoConfirmado.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaPagoNoConfirmado(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaPagoNoConfirmado.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionPagoError(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionPagoError.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionSuficiencia(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionSuficiencia.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSuficienciaAprobado(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSuficienciaAprobado.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSuficienciaOtorgadaAPoPRESU(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSuficienciaOtorgadaAPoPRESU.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSuficienciaCanceladaAPoPRESU(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSuficienciaCanceladaAPoPRES.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSolicitudCancelada(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaCancelacionSolicitud.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSuficienciaNoOtorgadaAPoPRESU(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSuficienciaNoOtorgadaAPoPRESU.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaFaltaPresupuesto(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaFaltaPresupuesto.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSolicitudVuelo(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSolicitudVuelo.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaRespuestaSolicitudVuelo(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaRespuestaSolicitudVuelo.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaSolicitudTrasladosInternacionales(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSolicitudTrasladosInternacionales.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }


        public string PlantillaSolicitudPresupuestoAP(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaSolicitudPresupuestoAP.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionAutorizadores(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionAutorizadores.html");

            // Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionAdjuntarFacturas(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionAdjuntarFacturas.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionEntregaComprobacion(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionEntregaComprobacion.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionConclusionComision(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionConclusionComision.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionReembolso(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionReembolso.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionReembolsoPagado(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionReembolsoPagado.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionComprobacionCancelada(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaCancelacionComprobacion.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }

        public string PlantillaNotificacionAutorizaComprobacion(DatosPlantilla datos)
        {
            string ruta = string.Concat(_rutaPlantillas, "PlantillaNotificacionAutorizadoresComprobacion.html");

            //Regresamos la plantilla con los valores cargados
            return CargarDatosPlantilla(datos, ruta);
        }
        

        public string CargarDatosPlantilla(DatosPlantilla datos, string ruta)
        {
            string plantillaGenerica = string.Empty;

            StringBuilder s = new StringBuilder();

            // Leemos el contenido de la plantilla
            try
            {
                s = new StringBuilder(File.ReadAllText(ruta));
            }
            catch (Exception ex)
            {
                // Si no se encuentra la plantilla, se debe cargar una por default.
                
            }


            // Reemplazamos los datos
            foreach (var etiqueta in datos.ListaReemplazo)
            {
                s.Replace(etiqueta.Key, etiqueta.Value);
            }


            return s.ToString();
        }
    }
}
