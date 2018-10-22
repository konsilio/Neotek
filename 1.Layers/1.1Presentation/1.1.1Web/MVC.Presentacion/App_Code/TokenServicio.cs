using Security.MainModule.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MVC.Presentacion.App_Code
{
    public class TokenServicio
    {
        public static List<Claim> ObtenerClaims(string token)
        {
            return TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(token);
        }
        public static short ObtenerIdEmpresa(string token)
        {
            var claims = ObtenerClaims(token);
            var Empresa = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.IdEmpresa));

            int idEmpresa = Empresa != null ? Convert.ToInt32(Empresa.Value) : 0;
            return (short)idEmpresa;
        }
        public static int ObtenerIdUsuario(string token)
        {
            var claims = ObtenerClaims(token);
            var Usuario = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.IdUsuario));

            return Usuario != null ? Convert.ToInt32(Usuario.Value) : 0;
        }
        public static bool ObtenerEsAdministracionCentral(string token)
        {
            var claims = ObtenerClaims(token);
            var EsAdminCentral = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.EsAdminCentral));

            return EsAdminCentral != null ? Convert.ToBoolean(EsAdminCentral.Value) : false;
        }
        public static bool ObtenerAutenticado(string token)
        {
            var claims = ObtenerClaims(token);
            var EsAutenticado = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.Autenticado));

            return EsAutenticado != null ? Convert.ToBoolean(EsAutenticado.Value) : false;
        }
        public static bool ObtenerEsSuperUsuario(string token)
        {
            var claims = ObtenerClaims(token);
            var EsSuperUsuario = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.EsSuperUsuario));

            return EsSuperUsuario != null ? Convert.ToBoolean(EsSuperUsuario.Value) : false;
        }
        public static string ObtenerNombre(string token)
        {
            var claims = ObtenerClaims(token);
            var Nombre = claims.FirstOrDefault(x => x.Type.Equals(TokenEtiquetasEnum.NombreUsuario));

            return  Nombre.Value;
        }
    }
}