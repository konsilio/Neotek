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
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs;

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
                    nOC.EsGas = _prod.EsGas;
                    nOC.EsTransporteGas = _prod.EsTransporte;
                    nOC.EsActivoVenta = _prod.EsActivoVenta;
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
        public static ComplementoGasDTO BuscarComplementoGas(OrdenCompra oc)
        {
            var descarga = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(oc.IdOrdenCompra);
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
            OCExpedidor.MontBelvieuDlls = OCExpUltima != null ? OCExpUltima.MontBelvieuDlls : 0;
            OCExpedidor.TarifaServicioPorGalonDlls = OCExpUltima != null ? OCExpUltima.TarifaServicioPorGalonDlls : 0;
            OCExpedidor.TipoDeCambioDOF = OCExpUltima != null ? OCExpUltima.TipoDeCambioDOF : 0;
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
            OCPorteador.Casetas = OCPorUltima != null ? OCPorUltima.Casetas : 0;
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
            var datosreq = Requisicion.RequisicionServicio.BuscarRequisicionPorId(dto.IdRequisicion);
            dto.MotivoRequisicion = datosreq.MotivoRequisicion;
            dto.RequeridoEn = datosreq.RequeridoEn;
            return dto;
        }             
    }
}
