using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Security.MainModule.Token_Service;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class UsuarioAplicacionServicio
    {
        public static string Obtener()
        {
            var token = HttpContext.Current.Request.Headers.GetValues("Authorization");
            var claims = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(token[0]);

            return string.Empty;
        }

        public static int ObtenerIdUsuarioFromToken()
        {
            var token = HttpContext.Current.Request.Headers.GetValues("Authorization");
            var claims = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(token[0]);

            var idUsuario = claims.FirstOrDefault(x => x.Type.Equals("IdUsuario"));

            return idUsuario != null ? Convert.ToInt32(idUsuario.Value) : 0;
        }
    }
}
