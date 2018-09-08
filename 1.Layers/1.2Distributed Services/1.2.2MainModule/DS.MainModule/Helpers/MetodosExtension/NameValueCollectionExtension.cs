using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace DS.MainModule.Helpers.MetodosExtension
{
    public static class NameValueCollectionExtension
    {
        public static T AsObject<T>(this NameValueCollection source, string prefix)
        where T : class, new()
        {
            var result = new T();
            string fullPrefix = string.IsNullOrEmpty(prefix) ? prefix : prefix + ".";
            foreach (var key in source.AllKeys.Where(k => k.StartsWith(fullPrefix)).
                Select(kwp => kwp.Substring(fullPrefix.Length)))
            {
                var prop = typeof(T).GetProperty(key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(result, Convert.ChangeType(source[fullPrefix + key], prop.PropertyType));
                }
            }

            return result;
        }

        public static T AsObject<T>(this NameValueCollection source)
        where T : class, new()
        {
            var result = new T();
            string fullPrefix = string.Empty;
            foreach (var key in source.AllKeys.Where(k => k.StartsWith(fullPrefix)).
                Select(kwp => kwp.Substring(fullPrefix.Length)))
            {
                var prop = typeof(T).GetProperty(key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(result, Convert.ChangeType(source[fullPrefix + key], prop.PropertyType));
                }
            }

            return result;
        }

    }
}