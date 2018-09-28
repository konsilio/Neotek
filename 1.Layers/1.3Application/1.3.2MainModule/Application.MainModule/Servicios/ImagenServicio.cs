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
            foto.PathImagen = Convertir.GetPhysicalPath(rutaImagenes);
            foto.PathImagen = GenerarNombre(".jpg", foto.Orden.ToString(), foto.PathImagen);
            foto.UrlImagen = Convertir.PhysicalPathToUrlPath(foto.PathImagen);            

            FileUtilities.GuardarImagen(foto.CadenaBase64, foto.PathImagen);
            foto.CadenaBase64 = null;

            //var imagen = ObtenerImagenDeBase64(foto.CadenaBase64);
            //imagen.Save(foto.PathImagen, System.Drawing.Imaging.ImageFormat.Jpeg);
            return foto;
        }

        public static Image ObtenerImagenDeBase64(string base64)
        {
            var byteArray = Convert.FromBase64String(base64);
            var imagen = FileUtilities.GuardarImagen(byteArray);

            return imagen;
        }

        public static string GenerarNombre(string extension, string orden)
        {
            return string.Concat("Imagen_", orden, extension.Contains(".") ? extension: "." + extension);
        }

        public static string GenerarNombre(string extension, string orden, string ruta)
        {
            return string.Concat(ruta, "\\", GenerarNombre(extension, orden));
        }
    }
}
