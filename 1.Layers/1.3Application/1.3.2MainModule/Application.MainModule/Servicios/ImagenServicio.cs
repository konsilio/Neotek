using Application.MainModule.Servicios.Almacen;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Servicios
{
    public static class ImagenServicio
    {
        private static string rutaImagenes = ConfigurationManager.AppSettings["RutaImagenesInventario"];
        private static string rutaImagenesPagos = ConfigurationManager.AppSettings["RutaImagenesPagos"];

        public static AlmacenGasDescargaFoto ObtenerImagen(AlmacenGasDescargaFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //string a = "CadenaBase64|IdUA|Magnatel|Inicial|.jpeg";

            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", foto.Orden, "_", campos.ElementAt(2), "_", campos.ElementAt(3));
            string extension = campos.ElementAt(4);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);            

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static OrdenCompraPago ObtenerImagen(OrdenCompraPago foto)
        {
            //La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //string a = "CadenaBase64|NumeroOrdenCompra|.jpeg";
            var b64 = foto.PhysicalPathCapturaPantalla;
            List<string> campos = FilterFunciones.ObtenerFields(b64);
            string nombre = string.Concat(campos.ElementAt(1), "_", foto.Orden, "_", ".png");
            string extension = campos.ElementAt(2);           

            foto.PhysicalPathCapturaPantalla = Convertir.GetPhysicalPath(rutaImagenesPagos);
            foto.PhysicalPathCapturaPantalla = GenerarNombre(nombre, extension, b64);
            foto.UrlPathCapturaPantalla = Convertir.PhysicalPathToUrlPath(b64);

            FileUtilities.GuardarImagen(b64, foto.PhysicalPathCapturaPantalla);            
            campos.Clear();
            return foto;
        }

        public static void LimpiarImagenes()
        {
            double diasVigencia = Convert.ToDouble(ConfigurationManager.AppSettings["ImagenesDiasVigencia"]) * -1;
            DateTime fechaVigencia = DateTime.Now.AddDays(diasVigencia);

            List<string> rutas = AlmacenGasServicio.ObtenerRutaImagenesSinVigencia(fechaVigencia);
            rutas.ForEach(x => FileUtilities.EliminarArchivo(x));
        }

        public static string EstructurarNombreImagen(string cadenaBase64, int idUA, string ObjetoFoto, bool inicial, string extension)
        {
            string deli = "|";
            string inicialFinal = inicial ? "Inicial" : "Final";
            return cadenaBase64 + deli + idUA.ToString() + deli + ObjetoFoto + deli + inicialFinal + deli + extension;
        }

        public static Image ObtenerImagenDeBase64(string base64)
        {
            var byteArray = Convert.FromBase64String(base64);
            var imagen = FileUtilities.GuardarImagen(byteArray);

            return imagen;
        }

        public static string GenerarNombre(string nombre, string extension)
        {
            return string.Concat("Imagen_", nombre, extension.Contains(".") ? extension: "." + extension);
        }

        public static string GenerarNombre(string nombre, string extension, string ruta)
        {
            return string.Concat(ruta, "\\", GenerarNombre(nombre, extension));
        }
    }
}
