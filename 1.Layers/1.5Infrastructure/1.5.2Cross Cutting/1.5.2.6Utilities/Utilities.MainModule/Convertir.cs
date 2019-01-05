using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities.MainModule
{
    public static class Convertir
    {
        public static string GetPhysicalPath(string path)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(path);
            else
                return string.Concat(HttpRuntime.AppDomainAppPath, path.Replace("~/", string.Empty).Replace("/", "\\"));
        }

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

            string basePartUrl = GetUrlBasePath();
            string lastPartUrl = string.Empty;
            string appPath = string.Empty;

            if (HttpContext.Current != null)
                appPath = HttpContext.Current.Request.PhysicalApplicationPath;
            else
                appPath = HttpRuntime.AppDomainAppPath;

            if (!physicalPath.StartsWith(appPath))
            {
                throw new InvalidOperationException("La dirección capturada no es parte de la ruta de la aplicación");
            }
            lastPartUrl = physicalPath.Replace(appPath, string.Empty)
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

        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}