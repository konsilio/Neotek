using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using System.Drawing;

namespace Utilities.MainModule
{
    public static class FileUtilities
    {
        private static string zipExtension = ".zip";

        //******** ARCHIVOS EN GENERAL

        public static string ObtenerContenido(string rutaArchivo)
        {
            var contenido = File.Exists(rutaArchivo)
                ? File.ReadAllText(rutaArchivo)
                : "El archivo no Existe";
            
            return contenido;
        }

        public static void EliminarArchivo(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
                File.Delete(rutaArchivo);
        }

        //******** ARCHIVOS EN GENERAL

        //******** ARCHIVOS ZIP

        public static string CrearArchivoZip(string rutaDirectorio, string rutaZip, bool crearDirectorio = false, bool eliminarDirectorio = false)
        {
            if (!rutaDirectorio.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("El directorio a comprimir no es parte de la ruta de la aplicación");
            }

            if (!Directory.Exists(rutaDirectorio) && crearDirectorio)
                Directory.CreateDirectory(rutaDirectorio);

            string zip = CrearArchivoZip(rutaDirectorio, rutaZip);

            if (eliminarDirectorio)
                Directory.Delete(rutaDirectorio);

            return zip;
        }

        /// <summary>
        /// Crea un archivo .zip a partir del directorio que se ha enviado.
        /// </summary>
        /// <param name="rutaDirectorio">Ruta del directorio a comprimir</param>
        /// <param name="rutaZip">Ruta en la que se guardará el archivo .zip. Puede indicar el .zip o omitirlo</param>
        /// <returns>Regresa la ruta física del archivo .zip</returns>
        public static string CrearArchivoZip(string rutaDirectorio, string rutaZip)
        {
            if (!rutaDirectorio.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("El directorio a comprimir no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("La ruta de salida del .zip no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.Contains(zipExtension))
                rutaZip = string.Concat(rutaZip, zipExtension);

            if (File.Exists(rutaZip))
                File.Delete(rutaZip);

            ZipFile.CreateFromDirectory(rutaDirectorio, rutaZip);

            return rutaZip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rutaZip"></param>
        /// <param name="rutaDirectorio"></param>
        /// <returns></returns>
        public static string ExtraerArchivoZip(string rutaZip, string rutaDirectorio)
        {
            if (!rutaDirectorio.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("El directorio de salida no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("La ruta del .zip no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.Contains(zipExtension))
                rutaZip = string.Concat(rutaZip, zipExtension);

            ZipFile.ExtractToDirectory(rutaZip, rutaDirectorio);

            return rutaZip;
        }

        public static string AgregarArchivoAlZip(string rutaZip, string rutaArchivoAAnexar, string nombreArchivoAAnexar)
        {
            if (!rutaArchivoAAnexar.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("El directorio de salida no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("La ruta del .zip no es parte de la ruta de la aplicación");
            }

            if (!rutaZip.Contains(zipExtension))
                rutaZip = string.Concat(rutaZip, zipExtension);

            using (ZipArchive archive = ZipFile.Open(rutaZip, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(rutaArchivoAAnexar, nombreArchivoAAnexar);
            }

            File.Delete(rutaArchivoAAnexar);

            return rutaZip;
        }

        //******** ARCHIVOS ZIP

        //******** ARCHIVOS XML

        public static T DeserealizarXML<T>(string xmlContenido)
        {
            var serializer = new XmlSerializer(typeof(T));
            var buffer = Encoding.UTF8.GetBytes(xmlContenido);
            using (var stream = new MemoryStream(buffer))
            {
                var xmlClass = (T)serializer.Deserialize(stream);
                return xmlClass;
            }
        }

        //******** ARCHIVOS XML

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image image;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                image = Image.FromStream(ms, true);
            }
            catch
            {
                image = null;
            }
            return image;
        }
    }
}
