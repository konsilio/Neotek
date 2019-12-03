using Security.MainModule.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace MVC.Presentacion.App_Code
{
    public static class TokenServicio
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



        public static void ClearTemp(TempDataDictionary temp)
        {
            if (temp.Keys.Contains("DataSourceAsignaciones"))       temp.Remove("DataSourceAsignaciones");
            if (temp.Keys.Contains("DataSourceCentros"))            temp.Remove("DataSourceCentros");
            if (temp.Keys.Contains("DataSourceEmpresas"))           temp.Remove("DataSourceEmpresas");
            if (temp.Keys.Contains("DataSourceTrasportes"))         temp.Remove("DataSourceTrasportes");
            if (temp.Keys.Contains("DataSourceMantenimientos"))     temp.Remove("DataSourceMantenimientos");
            if (temp.Keys.Contains("DataSourcePrecioVentas"))       temp.Remove("DataSourcePrecioVentas");
            if (temp.Keys.Contains("DataSourcePrecioVentasOtro"))   temp.Remove("DataSourcePrecioVentasOtro");
            if (temp.Keys.Contains("DataSourceProductos"))          temp.Remove("DataSourceProductos");
            if (temp.Keys.Contains("DataSourceLineas"))             temp.Remove("DataSourceLineas");
            if (temp.Keys.Contains("DataSourceCategorias"))         temp.Remove("DataSourceCategorias");
            if (temp.Keys.Contains("DataSourceUnidades"))           temp.Remove("DataSourceUnidades");
            if (temp.Keys.Contains("DataSourceProveedores"))        temp.Remove("DataSourceProveedores");
            if (temp.Keys.Contains("DataSourceRecargas"))           temp.Remove("DataSourceRecargas");
            if (temp.Keys.Contains("DataSourceRequisiciones"))      temp.Remove("DataSourceRequisiciones");
            if (temp.Keys.Contains("DataSourceUsuarios"))           temp.Remove("DataSourceUsuarios");
        }
    }
}