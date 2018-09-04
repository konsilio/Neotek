using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
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
        /// <summary>
        /// Busca los datos necesarios para una Orden de Compra de la requisicion Autorizada 
        /// </summary>
        /// <param name="_idrequi"></param>
        /// <returns></returns>
        public static RequisicionOCDTO BuscarRequisicion(int _idrequi)
        {
            return OrdenComprasAdapter.ToDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
        /// <summary>
        /// Descarta los prodcuto que no son aptos para una orden de compra
        /// </summary>
        /// <param name="prods"></param>
        /// <returns></returns>
        public static List<ProductoOCDTO> DescartarProductosParaOC(List<ProductoOCDTO> prods)
        {
            return prods.ToList().Where(x => !x.CantidadAComprar.Equals(0) || x.EsTransporteGas).ToList();
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
                    nOC.IdUsuarioGenerador = TokenServicio.ObtenerIdUsuario();
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
        /// <summary>
        /// Calcula los totales de la orden de compra sumando los importes de los productos
        /// </summary>
        /// <param name="ocs"></param>
        /// <returns></returns>
        public static List<OrdenCompra> CalcularTotales(List<OrdenCompra> ocs)
        {
            foreach (var oc in ocs)
            {
                foreach (var prod in oc.Productos)
                {
                    //Se validan valores nulos para inicialicar
                    if (oc.Iva == null) oc.Iva = 0;
                    if (oc.Ieps == null) oc.Ieps = 0;
                    if (oc.SubtotalSinIeps == null) oc.SubtotalSinIeps = 0;
                    if (oc.SubtotalSinIva == null) oc.SubtotalSinIva = 0;
                    if (oc.Total == null) oc.Total = 0;

                    oc.Iva += (prod.Precio * (prod.IVA / 100));
                    oc.Ieps += (prod.Precio * (prod.IEPS / 100));
                    oc.SubtotalSinIeps = prod.Importe - oc.Ieps;
                    oc.SubtotalSinIva = prod.Importe - oc.Iva;
                    oc.Total += prod.Importe;
                    if (prod.EsGas) oc.EsGas = true;
                    if (prod.EsActivoVenta) oc.EsActivoVenta = true;
                }
            }
            return ocs;
        }
        /// <summary>
        /// Busca todos las ordene de compra por ID de empresa
        /// En caso de no ser administracion central se tomara el ID de la empresa del token para filtrar.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public static List<OrdenCompra> BuscarTodo(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return new OrdenCompraDataAccess().BuscarTodos().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            else
                return new OrdenCompraDataAccess().BuscarTodos().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public static OrdenCompra Buscar(int idOrdenCompra)
        {
            return new OrdenCompraDataAccess().Buscar(idOrdenCompra);
        }
        public static RespuestaDto Actualizar(OrdenCompra oc)
        {
            return new OrdenCompraDataAccess().Actualizar(oc);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La Orden de Compra");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static ComplementoGasDTO BuscarComplemento(OrdenCompra oc)
        {
            var descarga = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(oc.IdOrdenCompra);
            return ComplementoGasAdapter.ToDTO(descarga);
        }
        public static List<OrdenCompraEstatus> ListaEstatus()
        {
            return new OrdenCompraDataAccess().Estatus();
        }
    }
}
