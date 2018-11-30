using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.Almacenes;
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
           
        public static AlmacenGasRecargaFoto ObtenerImagen(AlmacenGasRecargaFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|EstacionNo1|IdUa|Magnatel|Inicial|60-5|.jpg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.IdOrden, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static AlmacenGasTraspasoFoto ObtenerImagen(AlmacenGasTraspasoFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|Tractor|IdUa|Magnatel|Inicial|60-5|.jpeg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.OrdenImagen, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static AlmacenGasAutoConsumoFoto ObtenerImagen(AlmacenGasAutoConsumoFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|Tractor|IdUa|Magnatel|Inicial|60-5|.jpeg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.OrdenImagen, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static AlmacenGasTomaLecturaFoto ObtenerImagen(AlmacenGasTomaLecturaFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|Tractor|IdUa|Magnatel|Inicial|60-5|.jpeg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.IdOrdenFoto, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static AlmacenGasCalibracionFoto ObtenerImagen(AlmacenGasCalibracionFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|Tractor|IdUa|Magnatel|Inicial|60-5|.jpeg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.IdOrdenFoto, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static AlmacenGasDescargaFoto ObtenerImagen(AlmacenGasDescargaFoto foto)
        {
            // La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //                  0         1     2       3       4      5     6
            //string a = "CadenaBase64|Tractor|IdUa|Magnatel|Inicial|60-5|.jpeg"
            List<string> campos = FilterFunciones.ObtenerFields(foto.CadenaBase64);
            string nombre = string.Concat(campos.ElementAt(1), "_", campos.ElementAt(2), "_", foto.Orden, "_", campos.ElementAt(3), "_", campos.ElementAt(4), "_", campos.ElementAt(5));
            string extension = campos.ElementAt(6);
            foto.CadenaBase64 = campos.ElementAt(0);

            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(nombre, extension, foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;
            campos.Clear();
            return foto;
        }

        public static OrdenCompraPago ObtenerImagen(OrdenCompraPago foto, string numOrden)
        {
            //La cadena en el campo foto.CadenaBase64 debe contener el siguiente formato
            //string a = "CadenaBase64|NumeroOrdenCompra|.jpeg";
            var b64 = foto.UrlPathCapturaPantalla;
            List<string> campos = FilterFunciones.ObtenerFields(b64);
            string nombre = string.Concat(numOrden, "_", foto.Orden);
            string extension = ".jpg";//campos.ElementAt(2);

            var ruta = Convertir.GetPhysicalPath(rutaImagenesPagos);
            foto.PhysicalPathCapturaPantalla = GenerarNombre(nombre, extension, ruta);
            foto.UrlPathCapturaPantalla = Convertir.PhysicalPathToUrlPath(foto.PhysicalPathCapturaPantalla);

            FileUtilities.GuardarImagen(b64, foto.PhysicalPathCapturaPantalla);
            campos.Clear();
            return foto;
        }

        public static List<ImagenDTO> BuscarImagenes(AlmacenGasDescarga descarga)
        {
            //Formato del nombre de la imagen Tractor_5_4_Magnatel_Inicial_90.jpeg
            List<ImagenDTO> li = new List<ImagenDTO>();
            var fotosAlacenGasDescarga = AlmacenGasServicio.ObtenerImagenes(descarga);
            foreach (var foto in fotosAlacenGasDescarga)
            {
                ImagenDTO i = new ImagenDTO();
                var t = FilterFunciones.ObtenerFields(foto.UrlImagen, '/').Count;
                i.Nombre = foto.UrlImagen.Split('/')[t - 1];

                var nomPartes = FilterFunciones.ObtenerFields(i.Nombre, '_');

                i.UrlImg = foto.UrlImagen;
                i.Oden = short.Parse(nomPartes.ElementAt(2));
                i.Tipo = nomPartes.ElementAt(3);
                i.Momento = nomPartes.ElementAt(4);
                i.Lectura = string.Concat(nomPartes.ElementAt(5).Split('.')[0].Replace('-', '.'), " %");
                li.Add(i);
            }
            return li;
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
            return string.Concat(nombre, extension.Contains(".") ? extension: "." + extension);
        }

        public static string GenerarNombre(string nombre, string extension, string ruta)
        {
            return string.Concat(ruta, "\\", GenerarNombre(nombre, extension));
        }   
    }
}
