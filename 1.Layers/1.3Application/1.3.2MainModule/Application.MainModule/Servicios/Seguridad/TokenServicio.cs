using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Security.MainModule.Token_Service;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Catalogos;

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

        public static bool ObtenerEsAdministracionCentral()
        {
            var claims = ObtenerClaims();
            var EsAdminCentral = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.EsAdminCentral));

            return EsAdminCentral != null ? Convert.ToBoolean(EsAdminCentral.Value) : false;
        }
        public static bool ObtenerAutenticado()
        {
            var claims = ObtenerClaims();
            var EsAutenticado = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.Autenticado));

            return EsAutenticado != null ? Convert.ToBoolean(EsAutenticado.Value) : false;
        }

        public static Usuario ObtenerUsuarioAplicacion()
        {
            return UsuarioServicio.Obtener(ObtenerIdUsuario());
        }

        public static bool EsSuperUsuario()
        {
            var claims = ObtenerClaims();
            var EsSuperUsuario = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.EsSuperUsuario));

            return EsSuperUsuario != null ? Convert.ToBoolean(EsSuperUsuario.Value) : false;
        }
    }
}
