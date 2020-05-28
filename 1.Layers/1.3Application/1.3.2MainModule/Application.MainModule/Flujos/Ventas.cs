using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.AdaptadoresDTO.Cobranza;
using Application.MainModule.AdaptadoresDTO.Mobile.VentaExtraordinaria;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Cobranza;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Flujos
{
    public class Ventas
    {
        public List<CajaGeneralDTO> CajaGeneral()
        {
            //  CajaGeneralServicio.ProcesarMovimientoVentas();
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return CajaGeneralServicio.Obtener();

            else
                return CajaGeneralServicio.Obtener();
        }
        public List<AlmacenGasMovimientoDto> MovimientosGas(short unidad, short empresa, short year, byte month, byte dia, short? orden, string Folio)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerPVDetalle(unidad, empresa, year, month, dia, orden.Value, Folio).ToList();
        }
        public List<VentasPipaDto> MovimientosGas(short unidad, short empresa, short year, byte month, byte dia, short? orden, DateTime fecha, string Folio)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerVentasPipas(unidad, empresa, year, month, dia, orden.Value, fecha, Folio).ToList();
        }
        public List<VPuntoVentaDetalleDTO> MovimientosGasCilindro(short? empresa, short year, byte month, byte dia, short? orden)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerVentas(empresa.Value, year, month, dia, orden.Value).ToList();
        }
        public List<CajaGeneralDTO> CajaGeneralIdEmpresa(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerIdEmp(IdEmpresa).ToList();
        }
        public CorteCajaDTO CajaGeneral(string cveReporte)
        {//Liquidación
            CorteCajaDTO corte = new CorteCajaDTO();
            var reporteDia = CajaGeneralServicio.ObtenerReporteDia(cveReporte);
            if (reporteDia == null)
            {
                reporteDia = CajaGeneralServicio.ObtenerReporteDiaCorteCaja(cveReporte);
                if (reporteDia == null)
                    return corte;
            }

            var productoGas = ProductoServicio.ObtenerProductoGasVenta(TokenServicio.ObtenerIdEmpresa());
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            //var precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
            if (!resp.Exito) return null;

            var ventas = CajaGeneralServicio.ObtenerVPV(reporteDia).ToList();
            corte.Tickets = CajaGeneralAdapter.ToDTOC(ventas);
            var lecturas = AlmacenGasServicio.ObtenerLecturas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            //Se toma en cuenta los movimiento que alteran el P5000
            //var Calibraciones = AlmacenGasServicio.ObtenerCalibraciones(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            //var Autoconsimos = AlmacenGasServicio.ObtenerAutoconsumo(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            //var Traspasos = AlmacenGasServicio.ObtenerTraspasos(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            //var Recargas = AlmacenGasServicio.ObtenerRecargas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            //var Descargas = AlmacenGasServicio.ObtenerDescargasTodas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte); // Solo aplica para compra de gas
            corte.FolioOperacionDia = cveReporte;
            corte.Fecha = reporteDia.FechaReporte;
            corte.NombreUnidad = reporteDia.CAlmacenGas.Numero;
            corte.IdPuntoVenta = reporteDia.IdPuntoVenta ?? 0;
            corte.OperadorChofer = reporteDia.OperadorChofer;
            corte.TipoUnidad = 1;
            //PrecioVenta precio = null;
            if (reporteDia.CAlmacenGas.IdCamioneta != null)
            {
                //precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
                corte.TipoUnidad = 2;
                var li = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                var lf = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));

                foreach (var cil in li.Cilindros)
                {
                    VentasPipaDto lects = new VentasPipaDto();
                    lects.Concepto = Math.Truncate(cil.Cilindro.CapacidadKg).ToString() + "Kg";
                    lects.P5000Inicial = cil.Cantidad;
                    lects.P5000Final = lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(cil.IdCilindro)).Cantidad;
                    lects.CantidadLt = CalculosGenerales.DiferenciaEntreDosNumero(lects.P5000Inicial, lects.P5000Final);
                    lects.Venta = (lects.CantidadLt * cil.Cilindro.CapacidadKg) * reporteDia.PrecioKg.Value;
                    corte.Lecturas.Add(lects);
                    corte.TotalCantidad = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(vd => vd.CantidadKg.Value));
                    corte.TotalVenta = (corte.TotalCantidad * reporteDia.PrecioKg ?? 0);
                }
            }
            if (reporteDia.CAlmacenGas.IdCamioneta == null)
            {
                if (reporteDia.CAlmacenGas.IdPipa != null)
                    corte.TipoUnidad = 3;
                var li = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                var lf = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));

                VentasPipaDto lects = new VentasPipaDto();
                lects.Concepto = "Litros";
                lects.P5000Inicial = li.P5000 ?? 0;
                lects.P5000Final = lf.P5000 ?? 0;
                lects.CantidadLt = CalculosGenerales.DiferenciaEntreDosNumero(lects.P5000Inicial, lects.P5000Final);
                if (reporteDia.CAlmacenGas.IdPipa != null)
                {
                    ///precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
                    lects.Venta = lects.CantidadLt * reporteDia.PrecioLt ?? 0;
                }
                else
                {
                    var precio = PrecioVentaGasServicio.ObtenerPrecioVigenteEstacion(TokenServicio.ObtenerIdEmpresa(), reporteDia.CAlmacenGas.IdEstacionCarburacion.Value);
                    if (precio == null)
                        precio = PrecioVentaGasServicio.ObtenerPrecioVigenteEstaciones(TokenServicio.ObtenerIdEmpresa());
                    lects.Venta = lects.CantidadLt * precio.PrecioSalidaLt ?? 0;
                }
                corte.Lecturas.Add(lects);
                corte.TotalCantidad = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.CantidadLt ?? 0));
                corte.TotalVenta = (lects.CantidadLt * reporteDia.PrecioLt ?? 0);
            }
            corte.TotalOtros = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => !y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => (vd.PrecioUnitarioProducto ?? 0 * vd.CantidadProducto ??0)));
            corte.TotalContado = ventas.Where(x => x.VentaACredito.Equals(false)).Sum(v => v.Total);
            corte.TotalCredito = ventas.Where(x => x.VentaACredito.Equals(true)).Sum(v => v.Total);
            corte.Descuentos = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.DescuentoTotal));
            corte.TotalCheques = ventas.Where(x => x.FormaDePago == "Cheques").Sum(y => y.Total);
            corte.TotalTransferencias = ventas.Where(x => x.FormaDePago == "Transferencias").Sum(y => y.Total);
            corte.Bonidificaciones = ventas.Where(v => v.EsBonificacion).Sum(x => x.Bonificacion ?? 0);

            if (reporteDia.CAlmacenGas.IdCamioneta != null)
                corte.TotalEfectio = ((corte.TotalCantidad * reporteDia.PrecioKg ?? 0) + corte.TotalOtros) - (corte.TotalCredito + corte.Descuentos + corte.Bonidificaciones + corte.TotalCheques + corte.TotalTransferencias);
            else
                corte.TotalEfectio = ((corte.TotalCantidad * reporteDia.PrecioLt ?? 0) + corte.TotalOtros) - (corte.TotalCredito + corte.Descuentos + corte.Bonidificaciones + corte.TotalCheques + corte.TotalTransferencias);
            return corte;
        }
        public RespuestaDto GenerarLiquidacion(string cveReporte)
        {
            VentaCajaGeneral corte = new VentaCajaGeneral();
            var cajaera = UsuarioServicio.Obtener(TokenServicio.ObtenerIdUsuario());
            var reporteDia = CajaGeneralServicio.ObtenerReporteDia(cveReporte);
            if (reporteDia == null)
                return new RespuestaDto() { Exito = false, Mensaje = string.Format(Error.NoExiste, "El reporte") };

            var productoGas = ProductoServicio.ObtenerProductoGasVenta(TokenServicio.ObtenerIdEmpresa());
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            //var precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
            if (!resp.Exito) return null;

            var ventas = CajaGeneralServicio.ObtenerVPV(reporteDia).ToList();
            var lecturas = AlmacenGasServicio.ObtenerLecturas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            corte.FolioOperacionDia = string.Concat("C", FechasFunciones.ObtenerClaveUnica());
            corte.IdCAlmacenGas = reporteDia.IdCAlmacenGas.Value;
            corte.IdOperadorChofer = reporteDia.IdOperadorChofer;
            corte.OperadorChofer = reporteDia.OperadorChofer;
            corte.IdUsuarioEntrega = reporteDia.COperadorChofer.IdUsuario;
            corte.UsuarioEntrega = reporteDia.OperadorChofer;
            corte.UsuarioRecibe = UsuarioServicio.ObtenerNombreCompleto(cajaera);
            corte.IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario();
            corte.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
            corte.Year = (short)reporteDia.FechaReporte.Year;
            corte.Mes = (byte)reporteDia.FechaReporte.Month;
            corte.Dia = (byte)reporteDia.FechaReporte.Day;
            corte.Orden = Convert.ToInt16(CajaGeneralServicio.ObtenerCorteUltimo(corte.IdEmpresa, corte.Year, corte.Mes, corte.Dia) + 1);
            corte.TodoCorrecto = true;
            corte.PuntoVenta = reporteDia.CAlmacenGas.Numero;
            corte.IdPuntoVenta = reporteDia.IdPuntoVenta ?? 0;
            corte.VentaTotal = ventas.Sum(x => x.Total);
            corte.OtrasVentas = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => !y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.CantidadLt.Value));
            corte.VentaTotalContado = ventas.Where(x => x.VentaACredito.Equals(false)).Sum(v => v.Total);
            corte.VentaTotalCredito = ventas.Where(x => x.VentaACredito.Equals(true)).Sum(v => v.Total);
            corte.VentaTotalBonificacion = ventas.Where(v => v.EsBonificacion.Equals(true)).Sum(x => x.Bonificacion ?? 0);
            corte.DescuentoTotal = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.DescuentoTotal));
            corte.DescuentoCredito = ventas.Where(v => v.VentaACredito.Equals(true)).Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.DescuentoTotal));
            corte.DescuentoContado = ventas.Where(v => v.VentaACredito.Equals(false)).Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.DescuentoTotal));
            corte.DescuentoOtrasVentas = 0;
            if (CajaGeneralServicio.ExisteCorteUltimo(corte.IdCAlmacenGas, corte.IdEmpresa, corte.Year, corte.Mes, corte.Dia))
                return new RespuestaDto() { Exito = false, Mensaje = string.Format(Error.SiExiste, "La liquidacion") };
            var respuestaCorte = CajaGeneralServicio.Insertar(corte);
            if (!respuestaCorte.Exito) return respuestaCorte;

            var VentasEntity = CajaGeneralAdapter.FromEmtity(ventas);
            VentasEntity.ForEach(x => { x.FolioOperacionDia = corte.FolioOperacionDia; });

            return CajaGeneralServicio.ActualizarVentas(VentasEntity);
        }
        public List<VentaPuntoDeVenta> CajaGeneralCamioneta(DateTime fecha)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerTotalVentasCamioneta(fecha).ToList();
        }
        public List<VentaPuntoDeVenta> CajaGeneralEstacion(DateTime fecha)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerTotalVentasEstaciones(fecha).ToList();
        }
        public List<VentaCorteAnticipoDTO> CajaGeneralEstacion(string cveReporte)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerCE(cveReporte).ToList();
        }
        public RespuestaDto GuardarReporteLiquidado(VentaPuntoVentaDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerPV(Dto.FolioOperacionDia).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rcg = CajaGeneralServicio.ObtenerCG(Dto.FolioOperacionDia);
            var rep = CajaGeneralAdapter.FromDto(rcg);

            return CajaGeneralServicio.Actualizar(rep);
        }
        public RespuestaDto GuardarReporteLiquidadoEst(VentaCorteAnticipoDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerCE(Dto.FolioOperacion).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rep = CajaGeneralAdapter.FromDtoCE(reporte);
            return CajaGeneralServicio.Actualizar(rep);
        }
        public List<PuntoVentaDTO> ObtenerPuntosVentaLiquidacion()
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            var ptosVenta = PuntoVentaServicio.ObtenerTodosLiquidacion();
            return PuntoVentaAdapter.ToDTO(ptosVenta);
        }
        public List<VentaCajaGeneralDTO> ObtenerLiquidaciones()
        {
            var liquis = CajaGeneralServicio.Obtener(DateTime.Now);
            return CajaGeneralAdapter.ToDTO(liquis);
        }
        public RespuestaDto ActaualizarTickets(VentaPuntoVentaDTO item)
        {
            try
            {
                var ticket = PuntoVentaServicio.Obtener(item.FolioVenta);
                var emty = CajaGeneralAdapter.FromEntity(ticket);
                emty.FormaDePago = item.FormaDePago;
                emty.Referencia = string.IsNullOrEmpty(item.FormaDePago) ? string.Empty : item.Referencia;
                return PuntoVentaServicio.ActualizarVentasCorte(emty);
                //return new RespuestaDto() { Exito = true, Mensaje = Exito.OK };
            }
            catch (Exception ex)
            {
                return new RespuestaDto() { Exito = false, Mensaje = ex.Message };
            }
        }
        public VentaPuntoVentaDTO BuscarTicket(string FolioVenta)
        {
            var venta = PuntoVentaServicio.Obtener(FolioVenta);
            return CajaGeneralAdapter.ToDTOC(venta);
        }
        public RespuestaDto ActaualizarTicket(VentaPuntoVentaDTO item)
        {
            try
            {
                //if (!TokenServicio.EsSuperUsuario())
                //{
                //    return new RespuestaDto()
                //    {
                //        Exito = false,
                //        EsInsercion = false,
                //        EsActulizacion = false,
                //        Mensaje = string.Concat(Error.P0002, "folios de venta"),
                //        Id = 0,
                //        Codigo = null,
                //        ModeloValido = false,
                //    };
                //}
                var ticket = PuntoVentaServicio.Obtener(item.FolioVenta);//Obtiene el ticket completo
                //if (ticket.FechaRegistro.Date.Equals(DateTime.Now.Date))// la edicion solo se permite el dia que se registro la venta
                //{ 
                //    return new RespuestaDto()
                //    {
                //        Exito = false,
                //        EsInsercion = false,
                //        EsActulizacion = false,
                //        Mensaje = Error.CC005,
                //        Id = 0,
                //        Codigo = null,
                //        ModeloValido = false,
                //    };
                //}
                List<Bitacora> listaBitacora = new List<Bitacora>();
                if (!ticket.FolioVenta.Equals(ticket.FolioOperacionDia))// la edicion solo se permite el dia que se registro la venta
                {
                    return new RespuestaDto()
                    {
                        Exito = false,
                        EsInsercion = false,
                        EsActulizacion = false,
                        Mensaje = Error.CC006,
                        Id = 0,
                        Codigo = null,
                        ModeloValido = false,
                    };
                }
                var emty = CajaGeneralAdapter.FromEntity(ticket);//Se prepara para edición
                var cliente = ClienteServicio.Obtener(ticket.IdCliente); //Obtiene al cliente y su info.
                var cargo = CobranzaServicio.ObtenerCargo(emty.FolioVenta);
                if (!item.VentaACredito)//Se requiere de contado
                {
                    //Si la venta no es a Credito, se valida si originalmente se registro a credito
                    //para quitar la cuenta por cobrar a Credito y cobranza
                    if (emty.VentaACredito)
                    {
                        //Se buscar el cargo si la venta ya estaba a credito
                        if (cargo != null)
                            if (cargo.Abono.Count.Equals(0))//Se valida que no existan abonos
                            {
                                var cargoEmty = AbonosAdapter.FromEmty(cargo);
                                cargoEmty.Activo = false;//Se desactiva la cuenta por cobrar
                                var respCobranza = CobranzaServicio.Update(cargoEmty);
                                if (!respCobranza.Exito)
                                    return respCobranza;//No se pudo desactivar la cuenta
                                emty.EfectivoRecibido = item.EfectivoRecibido < ticket.Total ? ticket.Total : emty.EfectivoRecibido;
                                emty.VentaACredito = item.VentaACredito;
                                listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Folio cambia de Crédito a Contado" });
                            }
                            else
                            {
                                //Al tener abonos registrados, se niega la edicion de la venta
                                return new RespuestaDto()
                                {
                                    Exito = false,
                                    EsInsercion = false,
                                    EsActulizacion = false,
                                    Mensaje = Error.CC001,
                                    Id = 0,
                                    Codigo = null,
                                    ModeloValido = false,
                                };
                            }
                    }
                    else// No se edito el tipo de venta de contado
                    {
                        if (item.EsBonificacion)
                        {
                            if (item.EfectivoRecibido < ticket.Total)
                            {
                                emty.EsBonificacion = true;
                                emty.Bonificacion = CalculosGenerales.DiferenciaEntreDosNumero(emty.Total, item.EfectivoRecibido ?? 0);
                                listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Folio cambia a Bonificación" });
                            }
                            else
                            {
                                emty.Bonificacion = 0;
                                emty.EsBonificacion = false;
                            }
                            emty.EfectivoRecibido = item.EfectivoRecibido;
                        }
                        else
                        {
                            emty.Bonificacion = 0;
                            emty.EsBonificacion = false;
                            emty.EfectivoRecibido = item.EfectivoRecibido < ticket.Total ? ticket.Total : item.EfectivoRecibido;
                            listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Folio cambia a Contado" });
                        }
                    }
                }
                else// Se requiere a Credito
                {
                    if (emty.VentaACredito)// La venta originalmente es a credito
                    {
                        //Recalcular cuenta por cobrar
                        if (cargo.Abono.Count.Equals(0))
                        {
                            var cargoEmty = AbonosAdapter.FromEmty(cargo);
                            cargoEmty.TotalCargo = emty.Total;//Se actuliza el total del cargo sin abonos
                            var respCobranza = CobranzaServicio.Update(cargo);
                            if (!respCobranza.Exito)
                                return respCobranza;//No se pudo Actulizar la cuenta
                            else
                            {
                                emty.VentaACredito = true;
                                emty.EfectivoRecibido = 0;
                            }
                        }
                        else
                        {
                            //Al tener abonos registrados, se niega la edicion de la venta
                            return new RespuestaDto()
                            {
                                Exito = false,
                                EsInsercion = false,
                                EsActulizacion = false,
                                Mensaje = Error.CC002,
                                Id = 0,
                                Codigo = null,
                                ModeloValido = false,
                            };
                        }

                    }
                    if (!emty.VentaACredito)
                    {
                        //Crear cuenta por cobrar
                        if (cliente.CreditoDisponibleMonto > 0 && cliente.CreditoDisponibleMonto >= ticket.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadLt))
                        {
                            int dias = Convert.ToInt32(cliente.limiteCreditoDias);
                            var cargoNuevo = CargoAdapter.FromDTO(emty, DateTime.Now.AddDays(dias), TokenServicio.ObtenerIdEmpresa());
                            var insertCargo = PuntoVentaServicio.insertCargoMobile(cargoNuevo);
                            if (insertCargo.Exito)
                            {

                                decimal creditoDisponibleMonto = cliente.CreditoDisponibleMonto - ticket.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadLt ?? 0);
                                cliente.CreditoDisponibleMonto = creditoDisponibleMonto;
                                var actualizaCredito = ClienteServicio.ModificarCredito(cliente);
                                emty.VentaACredito = true;
                                emty.EfectivoRecibido = 0;
                                listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket", FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Folio: " + ticket.FolioVenta + " de Contado a Crédito" });
                            }
                            else
                                return insertCargo;
                        }
                        else
                        {
                            RespuestaDto _res = new RespuestaDto();
                            _res.Exito = false;
                            _res.EsInsercion = false;
                            _res.EsActulizacion = false;
                            _res.Mensaje = "El cliente no cuenta con crédito disponible, favor de comunicarse con el área de crédito y cobranza";
                            _res.Id = 0;
                            _res.Codigo = null;
                            _res.ModeloValido = false;
                            return _res;
                        }
                    }
                }
                var resp = PuntoVentaServicio.ActualizarVentasCorte(emty);
                if (resp.Exito)
                    foreach (var bitacora in listaBitacora)
                        UsuarioServicio.GuardarBitacora(bitacora);
                return resp;
            }
            catch (Exception ex)
            {
                return new RespuestaDto() { Exito = false, Mensaje = ex.Message };
            }
        }
        public RespuestaDto ActaualizarTicketDetalle(VPuntoVentaDetalleDTO item)
        {
            try
            {
                List<Bitacora> listaBitacora = new List<Bitacora>();
                var ticket = PuntoVentaServicio.Obtener(item.FolioOperacion);
                //if (ticket.FechaRegistro.Date.Equals(DateTime.Now.Date))// la edicion solo se permite el dia que se registro la venta
                //{ 
                //    return new RespuestaDto()
                //    {
                //        Exito = false,
                //        EsInsercion = false,
                //        EsActulizacion = false,
                //        Mensaje = Error.CC005,
                //        Id = 0,
                //        Codigo = null,
                //        ModeloValido = false,
                //    };
                //}
                if (!ticket.FolioVenta.Equals(ticket.FolioOperacionDia))// la edicion solo se permite antes de liquidar en caja
                {
                    return new RespuestaDto()
                    {
                        Exito = false,
                        EsInsercion = false,
                        EsActulizacion = false,
                        Mensaje = Error.CC006,
                        Id = 0,
                        Codigo = null,
                        ModeloValido = false,
                    };
                }
                var precioVenta = new Catalogos().ObtenerPrecioVentaVigente(ticket.CPuntoVenta.IdCAlmacenGas);
                var detalle = ticket.VentaPuntoDeVentaDetalle.Where(x => x.OrdenDetalle.Equals(item.OrdenDetalle)).FirstOrDefault();
                var cargo = CobranzaServicio.ObtenerCargo(detalle.VentaPuntoDeVenta.FolioVenta);
                var emty = CajaGeneralAdapter.FromEmtity(detalle);
                //emty.PrecioUnitarioProducto = item.PrecioUnitarioProducto;
                if (detalle.VentaPuntoDeVenta.CPuntoVenta.UnidadesAlmacen.IdCamioneta != null)
                {// Es venta de camioneta
                    RespuestaDto RespCamioneta = new RespuestaDto();
                    //var listaCilindros = detalle.VentaPuntoDeVenta.VentaPuntoDeVentaDetalle;
                    if (precioVenta.PrecioSalidaKg < item.PrecioUnitarioProducto)
                    {
                        return new RespuestaDto()
                        {
                            Exito = false,
                            EsInsercion = false,
                            EsActulizacion = false,
                            Mensaje = string.Format(Error.CC004, precioVenta.PrecioSalidaKg),
                            Id = 0,
                            Codigo = null,
                            ModeloValido = false,
                        };
                    }
                    emty.DescuentoUnitarioKg = CalculosGenerales.DiferenciaEntreDosNumero(precioVenta.PrecioSalidaKg ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.DescuentoUnitarioLt = CalculosGenerales.DiferenciaEntreDosNumero(precioVenta.PrecioSalidaKg ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.DescuentoUnitarioProducto = CalculosGenerales.DiferenciaEntreDosNumero(precioVenta.PrecioSalidaKg ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.PrecioUnitarioKg = item.PrecioUnitarioProducto - emty.DescuentoUnitarioKg;
                    emty.PrecioUnitarioLt = item.PrecioUnitarioProducto - emty.DescuentoUnitarioKg;
                    emty.PrecioUnitarioProducto = precioVenta.PrecioSalidaKg;
                    emty.CantidadProducto = item.CantidadProducto;
                    string kilos = string.Concat(emty.ProductoDescripcion.Where(char.IsNumber));
                    int kilosNum = 0;
                    int.TryParse(kilos, out kilosNum);
                    emty.CantidadKg = (kilosNum * item.CantidadProducto);
                    emty.CantidadLt = (emty.CantidadKg / (decimal)0.54);
                    emty.Subtotal = ((item.PrecioUnitarioProducto.Value * emty.CantidadKg.Value) / (decimal)1.16);
                    emty.DescuentoTotal = (emty.DescuentoUnitarioKg.Value * emty.CantidadKg.Value);
                    if (cargo != null)
                    {
                        if (cargo.Abono.Count.Equals(0))
                        {
                            var respDetalle = PuntoVentaServicio.ActualizarVentaDetalles(emty); // Se guarda la informacion regustrada
                            if (!respDetalle.Exito)
                                return respDetalle;
                            listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Se edito el detalle de la venta: " + emty.OrdenDetalle.ToString() });

                            var venta = PuntoVentaServicio.Obtener(ticket.FolioVenta);
                            var ventaEmty = CajaGeneralAdapter.FromEntity(venta);
                            //Se calculan los nuevos totales con la informacion actualizada
                            ventaEmty.Descuento = venta.VentaPuntoDeVentaDetalle.Sum(s => s.DescuentoTotal);
                            ventaEmty.Total = venta.VentaPuntoDeVentaDetalle.Sum(s => { return (s.PrecioUnitarioKg.Value * s.CantidadKg.Value); });
                            ventaEmty.Subtotal = (ventaEmty.Total / (decimal)1.16);
                            ventaEmty.Iva = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.Subtotal);
                            if (!ventaEmty.EsBonificacion)
                            {
                                if (ventaEmty.Total > venta.EfectivoRecibido)
                                {
                                    ventaEmty.EfectivoRecibido = ventaEmty.Total;
                                    ventaEmty.CambioRegresado = 0;
                                }
                                else
                                    ventaEmty.CambioRegresado = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, venta.EfectivoRecibido.Value);

                            }
                            else
                            {
                                ventaEmty.Bonificacion = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.EfectivoRecibido.Value);
                                ventaEmty.CambioRegresado = 0;
                            }
                            var cargoEmty = AbonosAdapter.FromEmty(cargo);
                            cargoEmty.TotalCargo = ventaEmty.Total;//Se actuliza el total del cargo sin abonos
                            var respCobranza = CobranzaServicio.Update(cargo);
                            if (!respCobranza.Exito)
                                return respCobranza;//No se pudo actualizar la cuenta
                            listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Se actualizo el cargo relacionado a la venta" });
                            RespCamioneta = PuntoVentaServicio.ActualizarVentasCorte(ventaEmty);
                        }
                        else
                        {
                            //Al tener abonos registrados, se niega la edicion de la venta
                            return new RespuestaDto()
                            {
                                Exito = false,
                                EsInsercion = false,
                                EsActulizacion = false,
                                Mensaje = Error.CC002,
                                Id = 0,
                                Codigo = null,
                                ModeloValido = false,
                            };
                        }
                    }
                    else
                    {
                        var respDetalle = PuntoVentaServicio.ActualizarVentaDetalles(emty); // Se guarda la informacion regustrada
                        if (!respDetalle.Exito)
                            return respDetalle;
                        listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Se actualizo el detalle de venta: " + emty.OrdenDetalle.ToString() });
                        var venta = PuntoVentaServicio.Obtener(ticket.FolioVenta);
                        var ventaEmty = CajaGeneralAdapter.FromEntity(venta);
                        //Se calculan los nuevos totales con la informacion actualizada
                        ventaEmty.Descuento = venta.VentaPuntoDeVentaDetalle.Sum(s => s.DescuentoTotal);
                        ventaEmty.Total = venta.VentaPuntoDeVentaDetalle.Sum(s => { return (s.PrecioUnitarioKg.Value * s.CantidadKg.Value); });
                        ventaEmty.Subtotal = (ventaEmty.Total / (decimal)1.16);
                        ventaEmty.Iva = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.Subtotal);
                        if (!ventaEmty.EsBonificacion)
                        {
                            if (ventaEmty.Total > venta.EfectivoRecibido)
                                ventaEmty.EfectivoRecibido = ventaEmty.Total;
                            ventaEmty.CambioRegresado = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.EfectivoRecibido.Value);
                        }
                        else
                            ventaEmty.Bonificacion = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.EfectivoRecibido.Value);
                        RespCamioneta = PuntoVentaServicio.ActualizarVentasCorte(ventaEmty);
                    }
                    foreach (var bitacora in listaBitacora)
                        UsuarioServicio.GuardarBitacora(bitacora);
                    return RespCamioneta;
                }
                else
                {// es venta de Pipa o de Estacion (Litros)
                    if (precioVenta.PrecioSalidaLt < item.PrecioUnitarioProducto)
                    {
                        return new RespuestaDto()
                        {
                            Exito = false,
                            EsInsercion = false,
                            EsActulizacion = false,
                            Mensaje = string.Format(Error.CC004, precioVenta.PrecioSalidaLt),
                            Id = 0,
                            Codigo = null,
                            ModeloValido = false,
                        };
                    }
                    emty.DescuentoUnitarioKg = CalculosGenerales.DiferenciaEntreDosNumero(emty.PrecioUnitarioProducto ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.DescuentoUnitarioLt = CalculosGenerales.DiferenciaEntreDosNumero(emty.PrecioUnitarioProducto ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.DescuentoUnitarioProducto = CalculosGenerales.DiferenciaEntreDosNumero(emty.PrecioUnitarioProducto ?? 0, item.PrecioUnitarioProducto ?? 0);
                    emty.PrecioUnitarioKg = precioVenta.PrecioSalidaKg - emty.DescuentoUnitarioKg;
                    emty.PrecioUnitarioLt = item.PrecioUnitarioProducto;
                    emty.CantidadProducto = item.CantidadProducto;
                    emty.CantidadKg = (item.CantidadProducto * (decimal)0.54);
                    emty.CantidadLt = item.CantidadProducto;
                    emty.Subtotal = ((item.PrecioUnitarioProducto.Value * item.CantidadProducto.Value) / (decimal)0.16);
                    emty.DescuentoTotal = ((emty.DescuentoUnitarioProducto ?? 0) * (item.CantidadProducto ?? 0));
                    var ventaEmty = CajaGeneralAdapter.FromEntity(detalle.VentaPuntoDeVenta);
                    ventaEmty.Descuento = emty.DescuentoTotal;
                    ventaEmty.Total = (item.PrecioUnitarioProducto.Value * item.CantidadProducto.Value);
                    ventaEmty.Iva = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, (ventaEmty.Total / (decimal)1.16));
                    ventaEmty.Subtotal = (ventaEmty.Total / (decimal)1.16);
                    if (!ventaEmty.EsBonificacion)
                        ventaEmty.CambioRegresado = CalculosGenerales.DiferenciaEntreDosNumero(ventaEmty.Total, ventaEmty.EfectivoRecibido ?? 0);
                    if (cargo != null)
                    {
                        if (cargo.Abono.Count.Equals(0))
                        {
                            var cargoEmty = AbonosAdapter.FromEmty(cargo);
                            cargoEmty.TotalCargo = ventaEmty.Total;//Se actuliza el total del cargo sin abonos
                            var respCobranza = CobranzaServicio.Update(cargo);
                            if (!respCobranza.Exito)
                                return respCobranza;//No se pudo desactivar la cuenta
                        }
                        else
                        {
                            //Al tener abonos registrados, se niega la edicion de la venta
                            return new RespuestaDto()
                            {
                                Exito = false,
                                EsInsercion = false,
                                EsActulizacion = false,
                                Mensaje = Error.CC002,
                                Id = 0,
                                Codigo = null,
                                ModeloValido = false,
                            };
                        }
                    }
                    listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Se edito el detalle de la venta: " + emty.OrdenDetalle.ToString() });
                    listaBitacora.Add(new Bitacora { Accion = "Edicion Ticket: " + ticket.FolioVenta + " " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Se actualizaron los totales de la venta por cambio al detalle" });
                    foreach (var bitacora in listaBitacora)
                        UsuarioServicio.GuardarBitacora(bitacora);
                    return PuntoVentaServicio.ActualizarVentaDetalles(emty, ventaEmty);
                }
            }
            catch (Exception ex)
            {
                return new RespuestaDto() { Exito = false, Mensaje = ex.Message };
            }
        }
        public RespuestaDto EliminarTicket(string FolioVenta)
        {
            var ticket = PuntoVentaServicio.Obtener(FolioVenta);//Obtiene el ticket completo            
            var cargo = CobranzaServicio.ObtenerCargo(FolioVenta);
            if (cargo != null)
            {
                if (!cargo.Abono.Count.Equals(0))
                {
                    //Al tener abonos registrados, se niega la eliminacón de la venta
                    return new RespuestaDto()
                    {
                        Exito = false,
                        EsInsercion = false,
                        EsActulizacion = false,
                        Mensaje = Error.CC003,
                        Id = 0,
                        Codigo = null,
                        ModeloValido = false,
                    };
                }
                else
                {
                    var cargoEmty = AbonosAdapter.FromEmty(cargo);
                    cargoEmty.Activo = false;//Se desactiva la cuenta por cobrar
                    var respCobranza = CobranzaServicio.Update(cargoEmty);
                    if (!respCobranza.Exito)
                        return respCobranza;//No se pudo eliminar la cuenta, se detiene proceso
                }
            }
            var detalles = CajaGeneralAdapter.FromEmtity(ticket.VentaPuntoDeVentaDetalle.ToList());//Se prepara para eliminar
            var emty = CajaGeneralAdapter.FromEntity(ticket);//Se prepara para eliminar
            var resp = PuntoVentaServicio.EliminarVentas(detalles, emty);
            if (resp.Exito)
                UsuarioServicio.GuardarBitacora(new Bitacora { Accion = "Eliminación de ticket: " + DateTime.Now.ToShortTimeString(), FechaRegistro = DateTime.Now, IdUsuario = TokenServicio.ObtenerIdUsuario(), Descripcion = "Folio eliminado: " + FolioVenta });
            return resp;
        }
    }
}
