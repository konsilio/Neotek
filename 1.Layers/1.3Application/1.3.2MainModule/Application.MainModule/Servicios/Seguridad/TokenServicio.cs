using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Security.MainModule.Token_Service;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class TokenServicio
    {
        public static List<Claim> ObtenerClaims()
        {
            var token = HttpContext.Current.Request.Headers.GetValues(TokenEtiquetasEnum.Authorization);
            return TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(token[0]);
        }

        public static short ObtenerIdEmpresa()
        {
            var claims = ObtenerClaims();
            var Empresa = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.IdEmpresa));

            int idEmpresa = Empresa != null ? Convert.ToInt32(Empresa.Value) : 0;
            return (short)idEmpresa;
        }

        public static int ObtenerIdUsuario()
        {
            var claims = ObtenerClaims();
            var Usuario = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.IdUsuario));

            return Usuario != null ? Convert.ToInt32(Usuario.Value) : 0;
        }
    }
}
