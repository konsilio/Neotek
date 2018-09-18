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
            return agente._RespuestaDTO;
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
        public static CentroCostoModel InitCentroCosto(string Tkn)
        {
            return new CentroCostoModel()
            {
                CentrosCostos = BuscarCentrosCosto(Tkn)
            };
        }
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentroCosto;
        }
        public static List<TipoCentroCostoDTO> BuscarTipoCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaTipoCentroCosto(Tkn);
            return agente._listaTipoCentroCosto;
        }
        public static RespuestaDTO ModificarCentroCosto(CentroCostoModificarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminarCentroCosto(CentroCostoEliminarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO NuevoCentroCosto(CentroCostoCrearDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarCentroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
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
        public static RespuestaDTO GuardarCuentaContable(CuentaContableModel model, string tkn)
        {
            CuentaContableCrearDTO ccC = new CuentaContableCrearDTO()
            {
                Numero = model.Numero,
                Descripcion = model.Descripcion,
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn),
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString())
            };
            return GuardarCtaCtble(ccC, tkn);
        }
        private static RespuestaDTO GuardarCtaCtble(CuentaContableCrearDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarCuentaContable(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static List<CuentaContableDTO> ListaCtaCtble(short idEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCuentasContables(idEmpresa, tkn);
            return agente._listaCuentasContables;
        }
        private static RespuestaDTO EliminarCtaContable(CuentaContableEliminarDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCtaCtble(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO BorrarCuentaContable(int idCC, string tkn)
        {
            return EliminarCtaContable(new CuentaContableEliminarDTO { IdCuenta = idCC }, tkn);
        }
        private static RespuestaDTO ModificarCtaContable(CuentaContableModificarDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCtaCtble(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EditarCuentaContable(CuentaContableModel model, string tkn)
        {
            CuentaContableModificarDTO ccm = new CuentaContableModificarDTO()
            {
                IdCuentaContable = model.IdCuentaContable,
                IdEmpresa = model.IdEmpresa,
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                Activo = true
            };
            return ModificarCtaContable(ccm, tkn);            
        }         
        public static CuentaContableModel InitCtaContable(string tkn)
        {
            return new CuentaContableModel()
            { CuentasContables = ListaCtaCtble(TokenServicio.ObtenerIdEmpresa(tkn), tkn) };
        }
        public static CuentaContableModel ActivarModifiarCuentaContable(int idcc, CuentaContableModel model, string tkn)
        {
            if (model.CuentasContables == null)
                model = InitCtaContable(tkn);
            var cc = model.CuentasContables.SingleOrDefault(x => x.IdCuentaContable.Equals(idcc));
            model.IdCuentaContable = cc.IdCuentaContable;
            model.Numero = cc.Numero;
            model.Descripcion = cc.Descripcion;
            return model;
        }
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
        #region Estación de Carburación

        public static List<EstacionCarburacionDTO> GetListaEstacionCarburacion(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEstacionCarburacion(tkn);
            return agente._listaEstacionCarburacion;
        }
        #endregion
        #region Unidad Almacen Gas

        public static List<UnidadAlmacenGasDTO> GetListaUnidadAlmcenGas(short IdEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaUnidadAlmacenGas(IdEmpresa, tkn);
            return agente._listaUnidadAlmacenGas;
        }
        #endregion
        #region Equipo de transporte

        public static List<EquipoTransporteDTO> GetListaEquiposTransporte(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEquipoTransporte(tkn);
            return agente._listaEquipoTransporte;
        }
        #endregion
    }
}