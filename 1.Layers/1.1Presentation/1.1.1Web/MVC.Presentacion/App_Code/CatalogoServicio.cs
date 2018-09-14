using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC.Presentacion.App_Code
{
    public static class CatalogoServicio
    {

        #region Empresas    
        public static RespuestaDTO create(EmpresaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEmpresaNueva(cc, tkn);
            return agente._respuestaDTO;
        }

        public static List<PaisModel> GetPaises(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarPaises(tkn);
            return agente._listaPaises;
        }

        public static List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaEmpresasLogin(tkn);
            return agente._listaEmpresas;
        }
        #endregion
        #region Usuarios
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaUsuarios(idEmpresa, token);
            return agente._listaUsuarios;
        }
        #endregion
        #region Centro de Costo
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentroCosto;
        }
        //public static RespuestaDTO ModificarCentroCosto(CentroCostoModificarDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.ModificarCtroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO EliminarCentroCosto(CentroCostoEliminarDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.EliminarCtroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO NuevoCentroCosto(CentroCostoCrearDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.GuardarCentroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        #endregion
        #region Porductos
        public static List<ProductoDTO> ListaProductos(string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductos(Token);
            return agente._listProductos;
        }
        #endregion
        #region Proveedores
        public static List<ProveedorDTO> CargarProveedores(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarProveedores(Tkn);
            return agente._listaProveedores;
        }
        public static List<SelectListItem> ProveedoresSelectList(List<ProveedorDTO> lprov)
        {
            List<SelectListItem> nlprov = new List<SelectListItem>();
            foreach (var prov in lprov)
            {
                nlprov.ToList().Add(new SelectListItem { Value = prov.IdProveedor.ToString(), Text = prov.NombreComercial });
            }
            return nlprov;
        }
        #endregion
        #region Cuentas contables      
        //public static RespuestaDTO GuardarCtaCtble(CuentaContableCrearDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.GuardarCuentaContable(cc, tkn);
        //    return agente._respuestaDTO;
        //}

        public static List<CuentaContableDTO> ListaCtaCtble(short idEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCuentasContables(idEmpresa, tkn);
            return agente._listaCuentasContables;
        }
        //public static RespuestaDTO EliminarCtaContable(CuentaContableEliminarDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.EliminarCtaCtble(cc, tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO ModificarCtaContable(CuentaContableModificarDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.ModificarCtaCtble(cc, tkn);
        //    return agente._respuestaDTO;
        //}
        #endregion
        #region Impuestos
        public static List<SelectListItem> ListaIVA()
        {
            List<SelectListItem> Ivas = new List<SelectListItem>();
            Ivas.Add(new SelectListItem { Value = "0", Text = "0%" });
            Ivas.Add(new SelectListItem { Value = "10", Text = "10%" });
            Ivas.Add(new SelectListItem { Value = "16", Text = "16%" });
            return Ivas;
        }
        public static List<SelectListItem> ListaIEPS()
        {
            List<SelectListItem> Ieps = new List<SelectListItem>();
            Ieps.Add(new SelectListItem { Value = "0", Text = "0%" });
            Ieps.Add(new SelectListItem { Value = "4", Text = "4%" });
            Ieps.Add(new SelectListItem { Value = "11", Text = "11%" });

            return Ieps;
        }
        #endregion
    }
}