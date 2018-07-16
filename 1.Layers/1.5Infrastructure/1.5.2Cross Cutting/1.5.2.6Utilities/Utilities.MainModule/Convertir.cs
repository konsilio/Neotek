using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities.MainModule
{
    public static class Convertir
    {
        public static string PhysicalPathToVirtualPath(string physicalPath)
        {
            if (!physicalPath.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("La dirección capturada no es parte de la ruta de la aplicación");
            }

            return "~/" + physicalPath.Substring(HttpContext.Current.Request.PhysicalApplicationPath.Length)
                  .Replace("\\", "/");
        }

        public static string PhysicalPathToUrlPath(string physicalPath)
        {
            if (physicalPath == null)
                return null;

            if (!physicalPath.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("La dirección capturada no es parte de la ruta de la aplicación");
            }

            string basePartUrl = GetUrlBasePath();
            string lastPartUrl = physicalPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, string.Empty)
                                             .Replace("\\", "/");
            
            Uri newUri = new Uri(basePartUrl + lastPartUrl);

            return newUri.AbsoluteUri;
        }

        public static string GetUrlBasePath()
        {
            string basePartUrl;
            if (HttpContext.Current != null)
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.IsSecureConnection)
                    basePartUrl = "https://";
                else
                    basePartUrl = "http://";

                basePartUrl += request["HTTP_HOST"] + "/";

                if (!basePartUrl.Contains("localhost"))
                    basePartUrl += request.Url.Segments[1];
            }
            else
                basePartUrl = ConfigurationManager.AppSettings["WebApi"];

            Uri newUri = new Uri(basePartUrl);

            return newUri.AbsoluteUri;
        }

        /// <summary>
        /// Método que se encarga de construir la URL base de los servicios con sólo el nombre del HOST
        /// </summary>
        /// <returns></returns>
        public static string GetUrlBase()
        {
            HttpRequest request = HttpContext.Current.Request;
            string basePartUrl;

            if (request.IsSecureConnection)
                basePartUrl = "https://";
            else
                basePartUrl = "http://";

            basePartUrl += request["HTTP_HOST"] + "/";
            
            Uri newUri = new Uri(basePartUrl);

            return newUri.AbsoluteUri;
        }
    }
}