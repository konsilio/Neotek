using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.Servicios.Almacenes;
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
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Catalogos;

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
        public static RespuestaDto GuardarOrdenCompra(OrdenCompra oc)
        {
            return new OrdenCompraDataAccess().InsertarNuevo(oc);
        }
        public static List<OrdenCompra> IdentificarOrdenes(OrdenCompraCrearDTO ocInicial)
        {
            List<OrdenCompra> nlist = new List<OrdenCompra>();
            foreach (var _prod in ocInicial.Productos)
            {
                var p = Catalogos.ProductoServicio.ObtenerProducto(_prod.IdProducto);
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
                    nOC.EsGas = p.EsGas;
                    nOC.EsTransporteGas = p.EsTransporteGas;
                    nOC.EsActivoVenta = p.EsActivoVenta;
                    nOC.Activo = true;
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
        public static List<OrdenCompra> BuscarTodo(short idEmpresa, DateTime fi, DateTime ff)
        {
            return new OrdenCompraDataAccess().BuscarTodos(idEmpresa, fi, ff);
        }
        public static OrdenCompra Buscar(int idOrdenCompra)
        {
            return new OrdenCompraDataAccess().Buscar(idOrdenCompra);
        }
        public static OrdenCompra BuscarOCExpedidor(AlmacenGasDescarga descarga)
        {
            if (descarga.OCompraExpedidor != null)
                return descarga.OCompraExpedidor;

            return Buscar(descarga.IdOrdenCompraExpedidor.Value);
        }
        public static OrdenCompra BuscarOCPorteador(AlmacenGasDescarga descarga)
        {
            if (descarga.OCompraPorteador != null)
                return descarga.OCompraPorteador;

            return Buscar(descarga.IdOrdenCompraPorteador.Value);
        }
        public static RespuestaDto Actualizar(OrdenCompra oc)
        {
            return new OrdenCompraDataAccess().Actualizar(oc);
        }
        public static RespuestaDto Actualizar(AlmacenGasDescarga oc)
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
        public static RespuestaDto EstatusIncorrecto()
        {
            string mensaje = string.Format(Error.EstatusIncorrecto, "la Orden de Compra");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
                Exito = false,
            };
        }
        public static RespuestaDto PagoExistenteExpedidor()
        {
            string mensaje = string.Format(Error.PagoExistente, "el expedidor");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
                Exito = false,
            };
        }
        public static RespuestaDto NoSeAsignoValorATotosLosProductos()
        {
            string mensaje = Error.OC0002;

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
                Exito = false,
            };
        }
        public static RespuestaDto PagoExistentePorteador()
        {
            string mensaje = string.Format(Error.PagoExistente, "el porteador");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
                Exito = false,
            };
        }
        public static ComplementoGasDTO BuscarComplementoGas(OrdenCompra oc)
        {
            //var descarga = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(oc.IdOrdenCompra);
            var descarga = AlmacenGasServicio.ObtenerDescargaPorIdrequisicion(oc.IdRequisicion);
            return ComplementoGasAdapter.ToDTO(descarga);
        }
        public static List<OrdenCompraEstatus> ListaEstatus()
        {
            return new OrdenCompraDataAccess().Estatus();
        }
        public static AplicaDescargaDto AplicarDescarga(AplicaDescargaDto apDesDto, AlmacenGasDescarga descarga, Empresa empresa)
        {
            OrdenCompra OCExpedidor = OrdenComprasAdapter.FromEntity(BuscarOCExpedidor(descarga));
            OrdenCompra OCPorteador = OrdenComprasAdapter.FromEntity(BuscarOCPorteador(descarga));
            OrdenCompra OCExpUltima = BuscarUltimaOCGas(empresa);
            OrdenCompra OCPorUltima = BuscarUltimaOCTransporte(empresa);

            OCExpedidor.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            OCExpedidor.MontBelvieuDlls = OCExpUltima != null && OCExpUltima.MontBelvieuDlls > 0 ? OCExpUltima.MontBelvieuDlls : 0;
            OCExpedidor.TarifaServicioPorGalonDlls = OCExpUltima != null && OCExpUltima.TarifaServicioPorGalonDlls > 0 ? OCExpUltima.TarifaServicioPorGalonDlls : 0;
            OCExpedidor.TipoDeCambioDOF = OCExpUltima != null && OCExpUltima.TipoDeCambioDOF > 0 ? OCExpUltima.TipoDeCambioDOF : 0;
            OCExpedidor.PrecioPorGalon = CalcularOrdenCompraServicio.ComplementoPrecioPorGalon(OCExpedidor.MontBelvieuDlls.Value, OCExpedidor.TarifaServicioPorGalonDlls.Value, OCExpedidor.TipoDeCambioDOF.Value);
            OCExpedidor.FactorGalonALitros = empresa.FactorGalonALitros;
            OCExpedidor.ImporteEnLitros = CalcularOrdenCompraServicio.ComplementoImporteEnLitros(OCExpedidor.PrecioPorGalon.Value, OCExpedidor.FactorGalonALitros.Value);
            OCExpedidor.FactorCompraLitrosAKilos = empresa.FactorCompraLitroAKilos;
            OCExpedidor.PVPM = CalcularOrdenCompraServicio.ComplementoPVPMKg(OCExpedidor.ImporteEnLitros.Value, OCExpedidor.FactorCompraLitrosAKilos.Value);
            OCExpedidor.SubtotalSinIva = CalcularOrdenCompraServicio.Subtotal(OCExpedidor.PVPM.Value, descarga.MasaKg.Value);
            OCExpedidor.Iva = CalcularOrdenCompraServicio.Iva(OCExpedidor.SubtotalSinIva.Value, IvaEnum.p16);
            OCExpedidor.Total = CalcularOrdenCompraServicio.Total(OCExpedidor.SubtotalSinIva.Value, OCExpedidor.Iva.Value, 0);

            OCPorteador.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            OCPorteador.FactorConvTransporte = empresa.FactorFleteGas;
            OCPorteador.PrecioTransporte = CalcularOrdenCompraServicio.ComplementoPrecioTransporteDeGas(descarga.MasaKg.Value, OCPorteador.FactorConvTransporte.Value);
            OCPorteador.Casetas = OCPorUltima != null && OCPorUltima.Casetas > 0 ? OCPorUltima.Casetas : 0;
            OCPorteador.SubtotalSinIva = CalcularOrdenCompraServicio.Subtotal(new List<decimal>() { OCPorteador.PrecioTransporte.Value, OCPorteador.Casetas.Value });
            OCPorteador.Iva = CalcularOrdenCompraServicio.Iva(OCPorteador.SubtotalSinIva.Value, IvaEnum.p16);
            OCPorteador.Total = CalcularOrdenCompraServicio.Total(OCPorteador.SubtotalSinIva.Value, OCPorteador.Iva.Value, 0);

            apDesDto.OCExpedidor = OCExpedidor;
            apDesDto.OCPorteador = OCPorteador;

            apDesDto.DescargaSinNavigationProperties.IdRequisicion = OCExpedidor.IdRequisicion;
            apDesDto.DescargaSinNavigationProperties.DatosProcesados = true;

            return apDesDto;
        }
        public static OrdenCompra BuscarUltimaOCTransporte(Empresa empresa)
        {
            if (empresa.OrdenesCompra != null && empresa.OrdenesCompra.Count > 0)
                return empresa.OrdenesCompra.LastOrDefault(x => x.EsTransporteGas
                                                             && x.IdOrdenCompraEstatus.Equals(OrdenCompraEstatusEnum.Compra_exitosa));

            var listaOCs = new OrdenCompraDataAccess().Buscar(empresa.IdEmpresa, OrdenCompraEstatusEnum.Compra_exitosa, false, false, true);

            if (listaOCs == null || listaOCs.Count <= 0)
                return null;

            return listaOCs.LastOrDefault();
        }
        public static OrdenCompra BuscarUltimaOCGas(Empresa empresa)
        {
            if (empresa.OrdenesCompra != null && empresa.OrdenesCompra.Count > 0)
                return empresa.OrdenesCompra.LastOrDefault(x => x.EsActivoVenta
                                                             && x.EsGas
                                                             && x.IdOrdenCompraEstatus.Equals(OrdenCompraEstatusEnum.Compra_exitosa));

            var listaOCs = new OrdenCompraDataAccess().Buscar(empresa.IdEmpresa, OrdenCompraEstatusEnum.Compra_exitosa, true, true, false);

            if (listaOCs == null || listaOCs.Count <= 0)
                return null;

            return listaOCs.LastOrDefault();
        }
        public static OrdenCompraDTO AgregarDatosRequisicion(OrdenCompraDTO dto)
        {
            var datosreq = Requisiciones.RequisicionServicio.BuscarRequisicionPorId(dto.IdRequisicion);
            dto.MotivoRequisicion = datosreq.MotivoRequisicion;
            dto.RequeridoEn = datosreq.RequeridoEn;
            return dto;
        }
        public static ComplementoGasDTO CargarDatosRequisicion(ComplementoGasDTO dto, Sagas.MainModule.Entidades.Requisicion req)
        {
            return ComplementoGasAdapter.ToRequisicion(dto, req);
        }
        public static ComplementoGasDTO CargarDatosPapeleta(ComplementoGasDTO dto)
        {
            return ComplementoGasAdapter.ToPapeleta(dto);
        }
        public static OrdenCompra CompletarDatosExpedidor(ComplementoGasDTO dto, OrdenCompra oc)
        {
            //var productoComplemento = oc.Productos.SingleOrDefault(x => x.EsGas);       

            oc.IdCentroCosto = oc.IdCentroCosto;
            oc.IdCuentaContable = oc.IdCuentaContable;
            oc.IdProveedor = oc.IdProveedor;

            var RPMMNTPG = dto.OrdenCompraExpedidor.MontBelvieuDlls;
            var TSG = dto.OrdenCompraExpedidor.TarifaServicioPorGalonDlls;
            var TC = dto.OrdenCompraExpedidor.TipoDeCambioDOF;
            var FactorCGalLtr = dto.OrdenCompraExpedidor.FactorGalonALitros;
            var FactorCaKg = dto.OrdenCompraExpedidor.FactorCompraLitrosAKilos;
            var kilogramosPapeleta = dto.KilosPapeleta;
            var Iva = dto.OrdenCompraExpedidor.Iva;

            var PrecioXGalon = CalcularOrdenCompraServicio.ComplementoPrecioPorGalon(RPMMNTPG ?? 0, TSG ?? 0, TC ?? 0);
            var ImporteLitros = CalcularOrdenCompraServicio.ComplementoImporteEnLitros(PrecioXGalon, FactorCGalLtr ?? 0);
            var PVPM = CalcularOrdenCompraServicio.ComplementoPVPMKg(ImporteLitros, FactorCaKg ?? 0);
            var PVIva = decimal.Round(CalcularOrdenCompraServicio.ComplementoPVIVA(PVPM, Iva ?? 0), 5);
            var ImportePagar = CalcularOrdenCompraServicio.ComplementoImporte(kilogramosPapeleta, PVIva);

            CargarDatosPapeleta(dto);

            oc.MontBelvieuDlls = dto.OrdenCompraExpedidor.MontBelvieuDlls;
            oc.TarifaServicioPorGalonDlls = dto.OrdenCompraExpedidor.TarifaServicioPorGalonDlls;
            oc.TipoDeCambioDOF = dto.OrdenCompraExpedidor.TipoDeCambioDOF;
            oc.PrecioPorGalon = PrecioXGalon;
            oc.FactorGalonALitros = dto.OrdenCompraExpedidor.FactorGalonALitros;
            oc.ImporteEnLitros = ImporteLitros;
            oc.FactorCompraLitrosAKilos = dto.OrdenCompraExpedidor.FactorCompraLitrosAKilos;
            oc.PVPM = PVPM;
            oc.Iva = dto.OrdenCompraExpedidor.Iva;
            oc.Total = ImportePagar;
            oc.FolioFactura = dto.OrdenCompraExpedidor.FolioFactura;
            oc.FolioFiscalUUID = dto.OrdenCompraExpedidor.FolioFiscalUUID;

            return oc;
        }
        public static OrdenCompra CompletarDatosPorteador(ComplementoGasDTO dto, OrdenCompra oc)
        {           
            oc.IdCentroCosto = oc.IdCentroCosto;
            oc.IdCuentaContable = oc.IdCuentaContable;
            oc.IdProveedor = oc.IdProveedor;
            oc.FactorConvTransporte = dto.OrdenCompraPorteador.FactorConvTransporte;
            oc.PrecioTransporte = dto.OrdenCompraPorteador.PrecioTransporte;
            oc.Casetas = dto.OrdenCompraPorteador.Casetas;
            oc.SubtotalSinIva = dto.OrdenCompraPorteador.SubtotalSinIva;
            oc.Iva = dto.OrdenCompraPorteador.Iva;
            oc.Total = dto.OrdenCompraPorteador.Total;
            oc.FolioFactura = dto.OrdenCompraPorteador.FolioFactura;
            oc.FolioFiscalUUID = dto.OrdenCompraPorteador.FolioFiscalUUID;

            return oc;
        }
        public static OrdenCompra DeterminarEstatusPorEntradas(OrdenCompra oc, List<OrdenCompraProducto> productos)
        {
            if (productos.Where(x => x.CantidadEntregada.Equals(x.Cantidad)).Count().Equals(productos.Count))
                oc.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            else
                oc.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Proceso_compra;
            return oc;
        }
        public static List<OrdenCompraProducto> BuscarProductosPorOrdenCompra(int idOrdeCompra)
        {
            return new OrdenCompraProductoDataAccess().Buscar(idOrdeCompra);
        }
        public static RespuestaDto ActualzarProductos(List<OrdenCompraProducto> productos, OrdenCompra oc)
        {
            return new OrdenCompraProductoDataAccess().Actualizar(productos, oc);
        }
        public static RespuestaDto ActualzarProductos(List<OrdenCompraProducto> productos, List<OrdenCompra> ocs)
        {
            return new OrdenCompraProductoDataAccess().Actualizar(productos, ocs);
        }
        public static List<OrdenCompraProducto> AplicarCambiosOrdenCompraProducto(List<OrdenCompraProducto> Prods, List<OrdenCompraProducto> Entitys)
        {
            foreach (var Entity in Entitys)
            {
                var prod = Prods.FirstOrDefault(x => x.IdProducto.Equals(Entity.IdProducto));
                Entity.IdCentroCosto = prod.IdCentroCosto;            
                Entity.Precio = prod.Precio;
                Entity.Descuento = prod.Descuento;
                Entity.IVA = prod.IVA;
                Entity.IEPS = prod.IEPS;
                Entity.Importe = prod.Importe;
            }
            return Entitys;
        }
        public static OrdenCompraPago GenerarPago(OrdenCompra oc, short orden)
        {
            var proveedor = ProveedorServicio.Obtener(oc.IdProveedor);
            return new OrdenCompraPago()
            {
                IdOrdenCompra = oc.IdOrdenCompra,
                Orden = orden,
                IdBanco = proveedor.IdBanco,
                IdFormaPago = 99,
                CuentaBancaria = proveedor.Cuenta,
                TotalImporte = oc.Total ?? 0,
                MontoPagado = 0,
                SaldoInsoluto = oc.Total ?? 0,
                FechaRegistro = Convert.ToDateTime(DateTime.Now.ToShortDateString())
            };
        }
        public static string ListaProductos(List<OrdenCompraProducto> Lista)
        {
            string respuesta = string.Empty;
            foreach (var item in Lista)
            {
                if (respuesta.Equals(string.Empty))
                    respuesta = item.Descripcion;
                else
                    respuesta += string.Concat(", ", item.Descripcion);
            }
            return respuesta;
        }
        public static string DeterminarEstatusPago(List<OrdenCompraPago> pagos = null)
        {
            if (pagos == null && pagos.Count == 0 && pagos.Exists(x => x.SaldoInsoluto != 0))
                return "Por Pagar";
            else
                return "Pagado";
        }
    }
}
