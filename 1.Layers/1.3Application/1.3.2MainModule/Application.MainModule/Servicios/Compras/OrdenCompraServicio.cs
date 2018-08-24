using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Compras
{
    public class OrdenCompraServicio
    {
        public static RequisicionOCDTO BuscarRequisicion(int _idrequi)
        {
            return OrdenComprasAdapter.ToDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
        public static OrdenCompraRespuestaDTO GuardarOrdenCompra(OrdenCompra oc)
        {
            return new OrdenCompraDataAccess().InsertarNuevo(oc);
        }
        public static List<OrdenCompra> IdentificarOrdenes(OrdenCompraCrearDTO ocInicial)
        {
            List<OrdenCompra> nlist = new List<OrdenCompra>();
            foreach (var _prod in ocInicial.Productos)
            {
                if (!nlist.Exists(x => x.IdProveedor.Equals(_prod.IdProveedor)))
                {
                    OrdenCompra nOC = new OrdenCompra();
                    nOC.IdProveedor = _prod.IdProveedor;
                    nOC.IdEmpresa = TokenServicio.ObtenerEsAdministracionCentral() == true ? ocInicial.IdEmpresa : TokenServicio.ObtenerIdEmpresa();
                    nOC.IdRequisicion = ocInicial.IdRequisicion;
                    nOC.IdCentroCosto = _prod.IdCentroCosto;
                    nOC.IdCuentaContable = _prod.IdCuentaContable;
                    nOC.IdOrdenCompraEstatus = ocInicial.IdOrdenCompraEstatus;
                    nOC.FechaRegistro = DateTime.Today;
                    nlist.Add(nOC);
                }
            }
            return nlist;
        }
        public static List<OrdenCompra> AsignarProductos(List<OrdenCompraProductoCrearDTO> _prods, List<OrdenCompra> _ocs)
        {
            foreach (var _prod in _prods)
            {
                foreach (var _oc in _ocs)
                {
                    if (_prod.IdProveedor.Equals(_oc.IdProveedor))
                        _oc.Productos.Add(ProductosOCAdapter.FromDTO(_prod));                    
                }
            }
            return _ocs;
        }
        public static List<OrdenCompra> CalcularTotales(List<OrdenCompra> ocs)
        {
            foreach (var oc in ocs)
            {
                foreach (var prod in oc.Productos)
                {
                    oc.Iva += (prod.Precio * (prod.IVA / 100));
                    oc.Ieps += (prod.Precio * (prod.IEPS / 100));
                    oc.Total += prod.Importe;
                    if (prod.EsGas) oc.EsGas = true;
                    if (prod.EsActivoVenta) oc.EsActivoVenta = true;
                }
            }
            return ocs;
        }
    }
}
