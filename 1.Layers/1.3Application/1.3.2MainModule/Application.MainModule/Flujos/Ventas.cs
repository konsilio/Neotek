using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
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
        {
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
            //var reporteDia = CajaGeneralServicio.ObtenerReporteDia(cveReporte);
            //if (reporteDia == null)
            //    return corte;
            var ventas = CajaGeneralServicio.ObtenerVPV(reporteDia).ToList();
            corte.Tickets = CajaGeneralAdapter.ToDTOC(ventas);
            var lecturas = AlmacenGasServicio.ObtenerLecturas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            corte.FolioOperacionDia = cveReporte;
            corte.Fecha = reporteDia.FechaReporte;
            corte.NombreUnidad = reporteDia.CAlmacenGas.Numero;
            corte.IdPuntoVenta = reporteDia.IdPuntoVenta ?? 0;
            corte.OperadorChofer = reporteDia.OperadorChofer;
            corte.TipoUnidad = 1;
            PrecioVenta precio = null;
            if (reporteDia.CAlmacenGas.IdCamioneta != null)
            {
                precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
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
                    lects.Venta = (lects.CantidadLt * cil.Cilindro.CapacidadKg) * precio.PrecioSalidaKg.Value;
                    corte.Lecturas.Add(lects);
                    corte.TotalCantidad = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(vd => vd.CantidadKg.Value));
                }
            }
            if (reporteDia.CAlmacenGas.IdPipa != null || reporteDia.CAlmacenGas.IdEstacionCarburacion != null)
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
                    precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
                    lects.Venta = lects.CantidadLt * precio.PrecioSalidaLt ?? 0;
                }
                else
                {
                    precio = PrecioVentaGasServicio.ObtenerPrecioVigenteEstacion(TokenServicio.ObtenerIdEmpresa(), reporteDia.CAlmacenGas.IdEstacionCarburacion.Value);
                    if (precio == null)
                        precio = PrecioVentaGasServicio.ObtenerPrecioVigenteEstaciones(TokenServicio.ObtenerIdEmpresa());
                    lects.Venta = lects.CantidadLt * precio.PrecioSalidaLt ?? 0;
                }
                corte.Lecturas.Add(lects);
                corte.TotalCantidad = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.CantidadLt ?? 0));
            }
            corte.TotalVenta = ventas.Sum(x => x.Total);
            corte.TotalOtros = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => !y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.CantidadLt ?? 0));
            corte.TotalContado = ventas.Where(x => x.VentaACredito.Equals(false)).Sum(v => v.Total);
            corte.TotalCredito = ventas.Where(x => x.VentaACredito.Equals(true)).Sum(v => v.Total);
            corte.Descuentos = ventas.Sum(x => x.VentaPuntoDeVentaDetalle.Where(y => y.IdProducto.Equals(productoGas.IdProducto)).Sum(vd => vd.DescuentoTotal));
            corte.Bonidificaciones = ventas.Where(v => v.EsBonificacion).Sum(x => x.Bonificacion ?? 0);
            if (reporteDia.CAlmacenGas.IdCamioneta != null)
                corte.TotalEfectio = ((corte.TotalCantidad * precio.PrecioSalidaKg ?? 0) + corte.TotalOtros) - (corte.TotalCredito + corte.Descuentos + corte.Bonidificaciones);
            else
                corte.TotalEfectio = ((corte.TotalCantidad * precio.PrecioSalidaLt ?? 0) + corte.TotalOtros) - (corte.TotalCredito + corte.Descuentos + corte.Bonidificaciones);
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
            var precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
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
            var ticket = PuntoVentaServicio.Obtener(item.FolioVenta);
            var emty = CajaGeneralAdapter.FromEntity(ticket);
            emty.FormaDePago = item.FormaDePago;
            emty.Referencia = string.IsNullOrEmpty(item.FormaDePago) ? string.Empty : item.Referencia;
            PuntoVentaServicio.ActualizarVentasCorte(emty);
            return new RespuestaDto() { Exito = true, Mensaje = Exito.OK };
        }
    }
}
