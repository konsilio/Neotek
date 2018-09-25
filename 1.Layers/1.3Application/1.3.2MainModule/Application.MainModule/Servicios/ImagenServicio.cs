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
        private static string rutaImagenes = ConfigurationManager.AppSettings["RutaImagenes"];

        public static AlmacenGasDescargaFoto ObtenerImagen(AlmacenGasDescargaFoto foto)
        {
            var imagen = ObtenerImagenDeBase64(foto.CadenaBase64);
            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(foto.Orden.ToString(), foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);
            imagen.Save(foto.PathImagen);
            return foto;
        }

        public static Image ObtenerImagenDeBase64(string base64)
        {
            var byteArray = Convert.FromBase64String(base64);
            var imagen = FileUtilities.ObtenerImagen(byteArray);

            return imagen;
        }

        public static string GenerarNombre(string orden)
        {
            return string.Concat("Imagen_", orden);
        }

        public static string GenerarNombre(string orden, string ruta)
        {
            return string.Concat(ruta, "//Imagen_", orden);
        }
    }
}
