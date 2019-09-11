using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Notificacion;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Compras
    {
        public OrdenCompraRespuestaDTO ComprarGas()
        {
            UsuarioAplicacionServicio.Obtener();

            return new OrdenCompraRespuestaDTO()
            {
                Exito = true
            };
        }
        /// <summary>
        /// Con el id de la Requisicion se genera un DTO con los datos para poder generar ordenes de compras
        /// </summary>
        /// <param name="idRequisicion"></param>
        /// <returns></returns>
        public RequisicionOCDTO BuscarRequisicion(int idRequisicion)
        {
            var Req = OrdenCompraServicio.BuscarRequisicion(idRequisicion);
            Req.ProductosOC = OrdenCompraServicio.DescartarProductosParaOC(Req.ProductosOC);
            Req = RequisicionServicio.DeterminarTipoRequisicion(Req);
            //Retornamos el objeto con los productos filtrados
            return Req;
        }
        /// <summary>
        /// Generamos la(s) ordene(s) de compra segun los provedores de la lista de productos.
        /// </summary>
        /// <param name="oc"></param>
        /// <returns></returns>
        public RespuestaDto GenerarOrdenesCompra(OrdenCompraCrearDTO oc)
        {
            var resp = PermisosServicio.PuedeRegistrarOrdenCompra();
            if (!resp.Exito) return resp;

            var Prods = BuscarRequisicion(oc.IdRequisicion).ProductosOC;
            if (Prods.Count != oc.Productos.Count) return OrdenCompraServicio.NoSeAsignoValorATotosLosProductos();
            //oc.Productos = OrdenCompraServicio.AsignarNuevos(oc)
            List<OrdenCompra> locDTO = OrdenCompraServicio.IdentificarOrdenes(oc);
            locDTO = OrdenCompraServicio.AsignarProductos(oc.Productos, locDTO);
            locDTO = CalcularOrdenCompraServicio.CalcularTotales(locDTO);

            RespuestaDto respuesta = new RespuestaDto();
            respuesta.Exito = true;
            foreach (var ocDTO in locDTO)
            {
                ocDTO.NumOrdenCompra = FolioServicio.GeneraNumerOrdenCompra(ocDTO);
                var orDTO = OrdenCompraServicio.GuardarOrdenCompra(ocDTO);
                respuesta.Mensaje += string.Concat(orDTO.Mensaje, " ,");
                if (orDTO.Exito)
                {
                    respuesta.Id = orDTO.Id;
                    respuesta.EsInsercion = true;
                    RequisicionServicio.UpDateRequisicionEstaus(oc.IdRequisicion, 8);
                    NotificarServicio.OrdenDeCompraNueva(OrdenCompraServicio.Buscar(orDTO.Id));
                }
                else return orDTO;
            }
            return respuesta;
        }
        /// <summary>
        /// Actualiza los datos de la orden de compra de su autorizacion
        /// </summary>
        /// <param name="_oc"></param>
        /// <returns></returns>
        public RespuestaDto AutorizarOrdenCompra(OrdenCompraDTO _oc)
        {
            var resp = PermisosServicio.PuedeAutorizarOrdenCompra();
            if (!resp.Exito) return resp;

            var oc = OrdenCompraServicio.Buscar(_oc.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            if (!oc.IdOrdenCompraEstatus.Equals(OrdenCompraEstatusEnum.Espera_autorizacion))
                return OrdenCompraServicio.EstatusIncorrecto();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdUsuarioAutorizador = TokenServicio.ObtenerIdUsuario();
            entity.FechaAutorizacion = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            entity.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Proceso_compra;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto CancelarOrdenCompra(OrdenCompraDTO dto)
        {
            //Falta rol en la Base de datos
            //var resp = PermisosServicio.PuedeCancelarOrdeCompra();
            //if (!resp.Exito) return resp;

            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdOrdenCompraEstatus = 5;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto FinalizarOrdenCompra(OrdenCompraDTO dto)
        {
            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Compra_exitosa;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto FinalizarEntradaProductoOrdenCompra(OrdenCompraDTO dto)
        {
            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto ActualizarOrdenCompraFactura(OrdenCompraDTO dto)
        {
            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.FolioFactura = dto.FolioFactura;
            entity.FolioFiscalUUID = dto.FolioFiscalUUID;
            entity.FechaResgistroFactura = Convert.ToDateTime(DateTime.Today.ToShortDateString());

            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto ActulizarOrdenCompraProducto(List<OrdenCompraProductoDTO> listDTO)
        {
            if (listDTO.FirstOrDefault().IdOrdenCompra == Convert.ToDecimal((decimal)listDTO.Sum(x => x.IdOrdenCompra) / (decimal)listDTO.Count))
            {
                var OC = BuscarOrdenCompra(listDTO.FirstOrDefault().IdOrdenCompra);
                var prodsEntity = ProductosOCAdapter.FromEntity(OrdenCompraServicio.BuscarProductosPorOrdenCompra(OC.IdOrdenCompra));
                var prodOC = ProductosOCAdapter.FromDTO(listDTO);
                var oc = OrdenCompraServicio.Buscar(OC.IdOrdenCompra);
                oc.Total = prodOC.Sum(x => x.Importe);
                var entity = OrdenComprasAdapter.FromEntity(oc);              
                var lProds = OrdenCompraServicio.AplicarCambiosOrdenCompraProducto(prodOC, prodsEntity);
                return OrdenCompraServicio.ActualzarProductos(lProds, entity);            
            }
            else
            {
                List<OrdenCompra> lOC = new List<OrdenCompra>();
                List<OrdenCompraProducto> lOCP = new List<OrdenCompraProducto>();
                foreach (var p in listDTO)
                {
                    var oc = OrdenCompraServicio.Buscar(p.IdOrdenCompra);
                    oc.IdCuentaContable = p.IdCuentaContable;
                    var entity = OrdenComprasAdapter.FromEntity(oc);
                    lOC.Add(entity);

                    var prodsEntity      = ProductosOCAdapter.FromEntity(OrdenCompraServicio.BuscarProductosPorOrdenCompra(p.IdOrdenCompra));
                    var prodOC = ProductosOCAdapter.FromDTO(listDTO);
                    lOCP = OrdenCompraServicio.AplicarCambiosOrdenCompraProducto(prodOC, prodsEntity);
                }                
                return OrdenCompraServicio.ActualzarProductos(lOCP, lOC);
            }
           
        }
        public RespuestaDto SolicitarPago(OrdenCompraDTO dto)
        {
            var ExistePago = BuscarPagos(dto.IdOrdenCompra);
            short ordenPago = 0;
            if (ExistePago.Count != 0) ordenPago = ExistePago.LastOrDefault().Orden++;

            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            oc.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.SolicitudPago;

            var ocEntity = OrdenComprasAdapter.FromEntity(oc);
            ocEntity.OrdenCompraPago.Add(OrdenCompraServicio.GenerarPago(oc, ordenPago));

            var respActualiza = OrdenCompraServicio.Actualizar(ocEntity);
            if (respActualiza.Exito) NotificarServicio.ConfirmacionPago(oc);

            return respActualiza;
        }
        public List<OrdenCompraDTO> ListaOrdenCompra(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarOrdenCompra();
            if (!resp.Exito) return new List<OrdenCompraDTO>();

            var _locEntity = OrdenCompraServicio.BuscarTodo(IdEmpresa);
            List<OrdenCompraDTO> loc = OrdenComprasAdapter.ToDTO(_locEntity);
            return loc;
        }
        public OrdenCompraDTO BuscarOrdenCompra(int idOrdeCompra)
        {
            //Valida permiso para consultar orden de compra
            var resp = PermisosServicio.PuedeConsultarOrdenCompra();
            if (!resp.Exito) return new OrdenCompraCrearDTO();

            //Se busca el id en la base y se genera DTO para enviar
            var oc = OrdenComprasAdapter.ToDTO(OrdenCompraServicio.Buscar(idOrdeCompra));
            return OrdenCompraServicio.AgregarDatosRequisicion(oc);
        }
        public ComplementoGasDTO BuscarComplementoGas(int idOrdenCompra)
        {
            var _OrdeCompra = OrdenCompraServicio.Buscar(idOrdenCompra);

            var _ComplementoGas = OrdenCompraServicio.BuscarComplementoGas(_OrdeCompra);
            var requisicion = new RequisicionDataAccess().BuscarPorIdRequisicion(_OrdeCompra.IdRequisicion);
            foreach (var orden in requisicion.OrdenesCompra)
            {
                if (orden.EsGas)
                    _ComplementoGas.OrdenCompraExpedidor = OrdenComprasAdapter.ToDTO(orden);
                if (orden.EsTransporteGas)
                    _ComplementoGas.OrdenCompraPorteador = OrdenComprasAdapter.ToDTO(orden);
                _ComplementoGas.Productos.AddRange(ProductosOCAdapter.ToDTO(orden.Productos.ToList()));
            }
            _ComplementoGas = OrdenCompraServicio.CargarDatosRequisicion(_ComplementoGas, requisicion);
            //var alamacen = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(_OrdeCompra.IdOrdenCompra);
            var descarga = AlmacenGasServicio.ObtenerDescargaPorIdrequisicion(_OrdeCompra.IdRequisicion);
            _ComplementoGas.Fotos = ImagenServicio.BuscarImagenes(descarga);
            return _ComplementoGas;
        }
        public List<OrdenCompraEstatusDTO> ListaEstatus()
        {
            return OrdenComprasAdapter.ToDTO(OrdenCompraServicio.ListaEstatus());
        }
        public RespuestaDto ConfirmarPago(OrdenCompraPagoDTO dto)
        {
            var Pago = OrdenCompraPagoServicio.Buscar(dto.IdOrdenCompra, dto.Orden);

            var entity = OrdenCompraPagoAdapter.FromEntity(Pago);
            var oc = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(entity.IdOrdenCompra));

            entity.PhysicalPathCapturaPantalla = dto.PhysicalPathCapturaPantalla;
            entity.UrlPathCapturaPantalla = dto.UrlPathCapturaPantalla;
            entity.FechaConfirmacion = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            entity = ImagenServicio.ObtenerImagen(entity, dto.NumOrdenCompra);
            entity.MontoPagado = dto.MontoPagado;
            entity.TotalImporte = oc.Total.Value;
            entity.SaldoInsoluto = oc.Total.Value - dto.MontoPagado;

            oc.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Compra_exitosa;

            var req = RequisicionAdapter.FromEntity(RequisicionServicio.Buscar(oc.IdRequisicion));
            req.IdRequisicionEstatus = RequisicionEstatusEnum.Autoriza_entrega;
            return OrdenCompraPagoServicio.Actualiza(entity, oc, req);
        }
        public RespuestaDto CrearOrdenCompraPago(OrdenCompraPagoDTO dto)
        {
            var Pago = OrdenCompraPagoAdapter.FromDTO(dto);
            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);

            Pago = CalcularPagoServicio.CalcularPago(Pago, oc);
            return OrdenCompraPagoServicio.Guardar(Pago);
        }
        public List<OrdenCompraPagoDTO> BuscarPagos(int idOc)
        {
            var pagos = OrdenCompraPagoServicio.BuscarPagos(idOc);
            return OrdenCompraPagoAdapter.ToDTO(pagos);
        }
        public RespuestaDto GuardarDatosExipedidor(ComplementoGasDTO dto)
        {
            var ExistePago = BuscarPagos(dto.OrdenCompraExpedidor.IdOrdenCompra);
            if (!ExistePago.Count.Equals(0)) return OrdenCompraServicio.PagoExistenteExpedidor();

            var ocExperidro = OrdenCompraServicio.Buscar(dto.OrdenCompraExpedidor.IdOrdenCompra);
            ocExperidro = OrdenCompraServicio.CompletarDatosExpedidor(dto, ocExperidro);
            var Entity = OrdenComprasAdapter.FromEntity(ocExperidro);
            return OrdenCompraServicio.Actualizar(Entity);
        }
        public RespuestaDto SolicitarPagoExpedidor(ComplementoGasDTO dto)
        {
            //var ExistePago = BuscarPagos(dto.OrdenCompraExpedidor.IdOrdenCompra);
            //if (!ExistePago.Count.Equals(0)) return OrdenCompraServicio.PagoExistenteExpedidor();
            //var ocExpedidor = OrdenCompraServicio.Buscar(dto.OrdenCompraExpedidor.IdOrdenCompra);
            //ocExpedidor.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.SolicitudPago;
            //var respActualiza = OrdenCompraServicio.Actualizar(OrdenComprasAdapter.FromEntity(ocExpedidor));
            //if (respActualiza.Exito) NotificarServicio.ConfirmacionPago(ocExpedidor);
            //return respActualiza;

            var oc = BuscarOrdenCompra(dto.OrdenCompraExpedidor.IdOrdenCompra);

            return SolicitarPago(oc);
        }
        public RespuestaDto GuardarDatosPapeleta(ComplementoGasDTO dto)
        {
            var oce = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(dto.OrdenCompraExpedidor.IdOrdenCompra));

            var papeleta = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(dto.OrdenCompraExpedidor.IdOrdenCompra);

            //var papeleta = AlmacenAdapter.FromDto(papeletaDto);
            papeleta.IdRequisicion = oce.IdRequisicion;
            var almacen = CentroCostoServicio.Obtener(oce.IdCentroCosto).UnidadAlmacenGas;
            papeleta.IdCAlmacenGas = almacen.IdCAlmacenGas;
            papeleta.IdAlmacenGas = almacen.IdAlmacenGas;
            papeleta.IdTipoMedidorAlmacen = almacen.IdTipoMedidor;

            var ocPapeleta =  AlmacenAdapter.FromEntity(papeleta);
            ocPapeleta.FechaPapeleta = dto.Fecha;
            ocPapeleta.FechaEmbarque = dto.FechaEmbarque;
            ocPapeleta.NumeroEmbarque = dto.NumeroEmbarque;
            ocPapeleta.ValorCarga = dto.ValorCarga;
            ocPapeleta.Sello = dto.Sello;
            ocPapeleta.NombreResponsable = dto.NombreResponsable;
            ocPapeleta.PorcenMagnatelPapeleta = dto.PorcentajeTanque;
            ocPapeleta.PorcenMagnatelOcular = dto.PorcentajeMedidor;
            ocPapeleta.PlacasTractor = dto.PlacasTractor;
            ocPapeleta.NombreOperador = dto.NombreOperador;
            ocPapeleta.PresionTanque = dto.PresionTanque;
            ocPapeleta.NumTanquePG = dto.NumeroTanque;
            ocPapeleta.CapacidadTanqueLt = dto.CapacidadTanque;
            //ocPapeleta.PorcenMagnatelOcular = dto.PorcenMagnatelOcularTractorINI;
            ocPapeleta.FechaInicioDescarga = dto.FechaEntraGas;
            ocPapeleta.PorcenMagnatelOcularAlmacenINI = dto.PorcenMagnatelOcularAlmacenINI;
            ocPapeleta.PorcenMagnatelOcularAlmacenFIN = dto.PorcenMagnatelOcularAlmacenFIN;
            ocPapeleta.PorcenMagnatelOcularTractorFIN = dto.PorcenMagnatelOcularTractorFIN;
            ocPapeleta.PorcenMagnatelOcularTractorINI = dto.PorcenMagnatelOcularTractorINI;
            ocPapeleta.MasaKg = dto.KilosPapeleta;

            return OrdenCompraServicio.Actualizar(ocPapeleta);
        }
        public RespuestaDto GuardarDatosPorteador(ComplementoGasDTO dto)
        {
            var ExistePago = BuscarPagos(dto.OrdenCompraPorteador.IdOrdenCompra);
            if (!ExistePago.Count.Equals(0)) return OrdenCompraServicio.PagoExistentePorteador();

            var ocPorteador = OrdenCompraServicio.Buscar(dto.OrdenCompraPorteador.IdOrdenCompra);
            var PT = dto.OrdenCompraPorteador.FactorConvTransporte * dto.KilosPapeleta;
            var ST = PT + dto.OrdenCompraPorteador.Casetas;
            var iva = (dto.OrdenCompraPorteador.Iva / 100);
            var t = (ST * iva) + ST;
            dto.OrdenCompraPorteador.PrecioTransporte = PT;
            dto.OrdenCompraPorteador.SubtotalSinIva = ST;
            dto.OrdenCompraPorteador.Total = t;
           
            ocPorteador = OrdenCompraServicio.CompletarDatosPorteador(dto, ocPorteador);

            return OrdenCompraServicio.Actualizar(OrdenComprasAdapter.FromEntity(ocPorteador));
        }
        public RespuestaDto SolicitarPagoPorteador(ComplementoGasDTO dto)
        {
            var oc = BuscarOrdenCompra(dto.OrdenCompraPorteador.IdOrdenCompra);
            return SolicitarPago(oc);
        }
        public List<OrdenCompra> BuscarCompras(short idEmpresa)
        {
            return OrdenCompraServicio.BuscarTodo(idEmpresa);
        }
    }
}
