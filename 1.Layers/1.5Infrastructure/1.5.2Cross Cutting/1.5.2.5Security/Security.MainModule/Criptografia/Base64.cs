using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Security.MainModule.Criptografia
{
    public static class Base64
    {
        public static bool EsBase64(string cadena)
        {
            return Regex.IsMatch(cadena, @"[A-Za-z0-9+/=]");
        }

        public static string CodificarUTF8(string cadena)
        {
            byte[] UTF8 = Encoding.UTF8.GetBytes(cadena);
            return Convert.ToBase64String(UTF8);
        }        

        public static string DecodificarUTF8(string cadenaBase64UTF8)
        {
            byte[] base64 = Convert.FromBase64String(cadenaBase64UTF8);
            return Encoding.UTF8.GetString(base64);
        }
        
        public static string CodificarASCII(string cadena)
        {
            byte[] ASCII = Encoding.ASCII.GetBytes(cadena);
            return Convert.ToBase64String(ASCII);
        }

        public static string DecodificarASCII(string cadenaBase64ASCII)
        {
            byte[] base64 = Convert.FromBase64String(cadenaBase64ASCII);
            return Encoding.ASCII.GetString(base64);
        }
    }
}
