using MVC.Presentacion.Agente;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class OrdenCompraServicio
    {
        public static OrdenesCompraModel InitOrdenesCompra(string tkn)
        {
            return new OrdenesCompraModel()
            {
                FechaRequeridaDe = DateTime.Now,
                FechaRequeridaA = DateTime.Now,
                FechaRegistroDe = DateTime.Now,
                FechaRegistroA = DateTime.Now,
                Requisiciones = RequisicionServicio.BuscarRequisiciones(TokenServicio.ObtenerIdEmpresa(tkn), tkn)
                    .Where(y => y.IdRequisicionEstatus.Equals(RequisicionEstatusEnum.Autorizacion_finalizada))
                    .OrderByDescending(x => x.IdRequisicion).ToList(),
                OrdenesCompra = ObtenerOrdenesCompra(TokenServicio.ObtenerIdEmpresa(tkn), tkn)
            };
        }
        public static List<OrdenCompraDTO> ObtenerOrdenesCompra(short idEmpresa, string Tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarOrdenesCompra(idEmpresa, Tkn);
            return agente._listaOrdenCompra;
        }
        public static OrdenCompraModel InitOrdenCompra(int id, string _tkn)
        {
            OrdenCompraModel model = new OrdenCompraModel();
            var datos = DatosRequisicion(id, _tkn);
            if (datos != null)
            {
                model.IdRequisicion = datos.IdRequisicion;
                model.NumRequisicion = datos.NumeroRequisicion;
                model.IdSolicitante = datos.IdUsuarioSolicitante;
                model.Solicitante = datos.UsuarioSolicitante;
                model.RequeridoEn = datos.RequeridoEn;
                model.MotivoCompra = datos.MotivoRequisicion;
                model.IdEmpresa = datos.IdEmpresa;
                model.NombreEmpresa = datos.NombreComercial;
                model.FechaRequisicion = datos.FechaRequerida;
                model.OrdenCompraProductos = datos.Productos;
            }
            return model;
        }
        public static OrdenCompraDTO BuscarOrdenCompra(int id, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarOrdenCompra(id, tkn);
            return agente._ordeCompraDTO;
        }
        private static bool EsGasTransp(List<ProductoOCDTO> lprod)
        {
            return lprod.Select(x => x.EsGas || x.EsTransporteGas).SingleOrDefault();
        }
        public static List<OrdenCompraEstatusDTO> ListaEstatus(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarOrdenCompraEstatus(tkn);
            return agente._listaOrdenCompraEstatus;
        }
        public static RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarRequisicionOC(idReq, Tkn);
            return agente._requisicionOrdenCompra;
        }
        private static RespuestaDTO GenerarOrdenesCompra(OrdenCompraCrearDTO ocDTO, string Tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.GuardarOrdenesCompra(ocDTO, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO GenerarOrdenCompra(OrdenCompraModel model, string Tkn)
        {
            OrdenCompraCrearDTO oc = new OrdenCompraCrearDTO();
            oc.IdRequisicion = model.IdRequisicion;
            oc.Productos = ObtenerProductosGrid(model.OrdenCompraProductos);
            oc.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Espera_autorizacion;
            return GenerarOrdenesCompra(oc, Tkn);
        }
        private static List<OrdenCompraProductoCrearDTO> ObtenerProductosGrid(List<ProductoOCDTO> Prods)
        {
            List<OrdenCompraProductoCrearDTO> lp = new List<OrdenCompraProductoCrearDTO>();
            foreach (var _prd in Prods)
            {
                OrdenCompraProductoCrearDTO p = new OrdenCompraProductoCrearDTO();
                p.IdProducto = _prd.IdProducto;
                p.IdCentroCosto = _prd.IdCentroCosto;
                p.IdCuentaContable = _prd.IdCuentaContable;
                p.IdProveedor = _prd.IdProdveedor;
                p.Precio = _prd.Precio;
                p.Descuento = _prd.Descuento;
                p.IVA = _prd.IVA;
                p.IEPS = _prd.IEPS;
                p.Cantidad = _prd.CantidadAComprar;
                decimal _descuento = ((p.Precio * p.Cantidad) * (p.Descuento / 100));
                decimal subtotal = (p.Precio * p.Cantidad) - (_descuento);
                decimal iva = ((subtotal) * (p.IVA / 100));
                decimal ieps = ((subtotal) * (p.IEPS / 100));
                p.Importe = subtotal + iva + ieps;
                lp.Add(p);
                p.EsGas = _prd.EsGas;
                p.EsTransporte = _prd.EsTransporteGas;
                p.EsActivoVenta = _prd.EsActivoVenta;
            }
            return lp;
        }
        public static RespuestaDTO AutorizarOrdenCompra(OrdenCompraDTO dto, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.AutorizarOrdenCompra(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static EntradaMercanciaModel EntradaMercancialModel(int idOC, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarOrdenesCompraEntrada(idOC, tkn);
            return agente._entradaMercancia;
        }
        public static RespuestaDTO RegistrarEntrada(EntradaMercanciaModel model, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.RegistrarEntrada(model, tkn);
            return agente._RespuestaDTO;
        }
        //public static OrdenCompraComplementoDTO InitComplemento(string tkn)
        //{

        //}
        public static OrdenCompraPagoDTO InitOrdenCompraPago(int idOC, string tkn)
        {
            var oc = BuscarOrdenCompra(idOC, tkn);
            var prov = CatalogoServicio.ListaProveedores(tkn).FirstOrDefault(x => x.IdProveedor.Equals(oc.IdProveedor));
            var banco = CatalogoServicio.ListaBanco(tkn).FirstOrDefault(b => b.IdBanco.Equals(prov.IdBanco));
            return new OrdenCompraPagoDTO()
            {
                IdOrdenCompra = oc.IdOrdenCompra,
                NumOrdenCompra = oc.NumOrdenCompra,
                IdProveedor = oc.IdProveedor,
                Proveedor = oc.Proveedor,
                IdBanco = banco.IdBanco,
                Banco = banco.NombreCorto,
                CuentaBancaria = prov.Cuenta,
                Empresa = oc.Empresa,
                MontoPagado = oc.Total.Value
            };
        }
        public static RespuestaDTO ConfirmarPago(OrdenCompraPagoDTO dto, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.EnviarConfirmarPago(dto, tkn);
            return agente._RespuestaDTO;
        }
    }
}