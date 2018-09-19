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
        private static RespuestaDTO ModificarCentroCosto(CentroCostoModificarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EditarCentroCosto(CentroCostoModel model, string tkn)
        {
            CentroCostoModificarDTO dto = new CentroCostoModificarDTO()
            {
                IdCentroCosto = model.IdCentroCosto,
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                IdTipoCentroCosto = model.IdTipoCentroCosto
            };
            if (!model.IdEquipoTransporte.Equals(0)) dto.IdEquipoTransporte = model.IdEquipoTransporte;
            if (!model.IdEstacionCarburacion.Equals(0)) dto.IdEstacionCarburacion = model.IdEstacionCarburacion;
            if (!model.IdCAlmacenGas.Equals(0)) dto.IdCAlmacenGas = model.IdCAlmacenGas;
            return ModificarCentroCosto(dto, tkn);
        }
        public static CentroCostoModel ActivarModificar(int idcc, CentroCostoModel model, string tkn)
        {
            if (model.CentrosCostos == null)
                model = InitCentroCosto(tkn);
            var cc = model.CentrosCostos.SingleOrDefault(x => x.IdCentroCosto.Equals(idcc));
            model.Numero = cc.Numero;
            model.IdCentroCosto = cc.IdCentroCosto;
            model.Descripcion = cc.Descripcion;
            if (!cc.IdEquipoTransporte.Equals(0)) model.IdEquipoTransporte = cc.IdEquipoTransporte;
            if (!cc.IdEstacionCarburacion.Equals(0)) model.IdEstacionCarburacion = cc.IdEstacionCarburacion;
            if (!cc.IdCAlmacenGas.Equals(0)) model.IdCAlmacenGas = cc.IdCAlmacenGas;
            return model;
        }
        private static RespuestaDTO EliminarCentroCosto(CentroCostoEliminarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO BorrarCentroCosto(int id, string tkn)
        {
            return EliminarCentroCosto(new CentroCostoEliminarDTO { IdCentroCosto = id }, tkn);
        }
        private static RespuestaDTO NuevoCentroCosto(CentroCostoCrearDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarCentroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO CrearCentroCosto(CentroCostoModel model, string tkn)
        {
            CentroCostoCrearDTO dto = new CentroCostoCrearDTO()
            {
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                IdTipoCentroCosto = model.IdTipoCentroCosto
            };
            dto.IdEquipoTransporte = model.IdEquipoTransporte;
            dto.IdEstacionCarburacion = model.IdEstacionCarburacion;
            dto.IdCAlmacenGas = model.IdCAlmacenGas;
            dto.IdCamioneta = 0;
            dto.IdCilindro = 0;
            dto.IdPipa = 0;
            dto.IdVehiculoUtilitario = 0;
            dto.IdTipoCentroCosto = 1;
            return NuevoCentroCosto(dto, tkn);
        }
        #endregion
        #region Porductos
        public static List<ProductoDTO> ListaProductos(string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductos(Token);
            return agente._listaProductos;
        }
        public static RespuestaDTO CrearProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarProducto(dto, tkn);
            return agente._RespuestaDTO;
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
        #region Categoria Producto
        public static RespuestaDTO CrearCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0))
                dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<CategoriaProductoDTO> ListaCategorias(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCategoriasProducto(tkn);
            return agente._listaCategoriasProducto;
        }
        public static CategoriaProductoDTO ActivarEditar(short id, string tkn)
        {
            var cat = ListaCategorias(tkn).SingleOrDefault(x => x.IdCategoria.Equals(id));
            return new CategoriaProductoDTO()
            {
                IdCategoria = cat.IdCategoria,
                Descripcion = cat.Descripcion,
                Nombre = cat.Nombre,
                IdEmpresa = cat.IdEmpresa
            };
        }
        #endregion
        #region Linea Producto
        public static RespuestaDTO CrearLineaProducto(LineaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<LineaProductoDTO> ListaLineasProducto(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCategoriasProducto(tkn);
            return agente._listaLineasProducto;
        }
        #endregion
        #region Unidad de medida
        public static RespuestaDTO CrearUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<UnidadMedidaDTO> ListaUnidadesMedida(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaUnidadesMedida(tkn);
            return agente._listaUnidadesMedida;
        }
        #endregion
    }
}