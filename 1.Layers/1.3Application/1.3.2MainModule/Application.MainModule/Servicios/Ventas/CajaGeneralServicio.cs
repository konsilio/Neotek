using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Equipo;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Servicios.Ventas
{
    public class CajaGeneralServicio
    {
        public static List<CajaGeneralDTO> Obtener()
        {
            List<CajaGeneralDTO> lPventas = CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().BuscarTodos());
            return lPventas;
        }
        public static List<VentaCajaGeneral> Obtener(DateTime fecha)
        {
            return new CajaGeneralDataAccess().ObtenerLiquidaciones((short)fecha.Year, Convert.ToByte(fecha.Month), Convert.ToByte(fecha.Day));
        }
        public static List<VPuntoVentaDetalleDTO> ObtenerVentas(short empresa, short year, byte month, byte dia, short? orden)
        {
            List<VentaPuntoDeVentaDetalle> _lst = BuscarPuntoVentaDetalle(empresa, year, month, dia, orden.Value).ToList();

            List<VPuntoVentaDetalleDTO> lventafinal = new List<VPuntoVentaDetalleDTO>();

            var resultantList = _lst
                    .GroupBy(s => s.CantidadKg)
                    .Select(grp => grp.ToList())
                    .ToList();
            foreach (var list in resultantList)
            {
                List<VPuntoVentaDetalleDTO> lPventas = CajaGeneralAdapter.ToDTOVC(list);
                lventafinal.AddRange(lPventas);
            }
            return lventafinal;
        }
        public static ReporteDelDia ObtenerReporteDia(string ClaveRporte)
        {
            return new CajaGeneralDataAccess().BuscarPorClaveReporte(ClaveRporte);
        }
        //Camioneta Reporte del dia
        public static List<ReporteDiaDTO> ObtenerRepCamionetas(short unidad, DateTime fecha)
        {
            var puntoVenta = PuntoVentaServicio.Obtener(unidad).IdPuntoVenta;

            //Obtener Venta Detalle por PV, Fecha
            List<VentaPuntoDeVenta> vm = new CajaGeneralDataAccess().BuscarPorPuntoVenta(puntoVenta, fecha);//.Where(x => x.FechaAplicacion.Value.ToShortDateString().Equals(fecha.ToShortDateString())).ToList();
            var FolioOperacion = vm.Where(x => x.FechaAplicacion.Value.ToShortDateString() == fecha.ToShortDateString()).ToList().FirstOrDefault().FolioOperacionDia;

            //Obtener Lt vendidos- AlmacenGasMovimiento
            var Ltvendidos = AlmacenGasServicio.ObtenerMovimientos(FolioOperacion, fecha).FirstOrDefault().SalidaLt;

            //Obtener Preciokg
            List<VentaPuntoDeVentaDetalle> _lst = BuscarPuntoVentaDetalle(null, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day, null).ToList();
            List<VPuntoVentaDetalleDTO> lstDto = CajaGeneralAdapter.ToDTO(_lst);
            var PrecioKg = _lst.FirstOrDefault().PrecioUnitarioKg ?? 0;

            //Obtener importe contado - imp credito
            var importes = ObtenerCG(FolioOperacion);

            //obtener unidad almacen
            var almacen = AlmacenGasServicio.ObtenerAlmacen(unidad);

            //obtener tanques,  NombreTanque=Cantidad Clindros, Normal= cantidad cilindros venta, Venta = Clindros vendidos completos (cilindro y gas)
            List<TanquesDto> tanques = new List<TanquesDto>();
            var resultantList = _lst
                   .GroupBy(s => s.CantidadKg)
                   .Select(grp => grp.ToList())
                   .ToList();
            foreach (var list in resultantList)
            {
                List<TanquesDto> lventas = CajaGeneralAdapter.ToDTOT(list);
                tanques.AddRange(lventas);
            }

            List<ReporteDiaDTO> lRventas = CajaGeneralAdapter.ToDtoC(lstDto, importes, almacen, tanques);
            return lRventas;
        }
        //Pipa Reporte del dia
        public static VPuntoVentaDetalleDTO ObtenerRepPipas(short? unidad, DateTime fecha)
        {

            AlmacenGasTomaLectura LecturasInicial = AlmacenGasServicio.BuscarLecturaPorFecha(unidad.Value, TipoEventoEnum.Inicial, fecha);
            AlmacenGasTomaLectura LecturasFinal = AlmacenGasServicio.BuscarLecturaPorFecha(unidad.Value, TipoEventoEnum.Final, fecha);
            //Obtener Punto de Venta
            var puntoVenta = PuntoVentaServicio.Obtener(unidad.Value).IdPuntoVenta;

            //Obtener Venta Detalle por PV, Fecha
            var FolioOperacion = new CajaGeneralDataAccess().BuscarPorPV(puntoVenta).Where(x => x.FechaAplicacion.Equals(fecha)).FirstOrDefault().FolioOperacionDia;

            //Obtener Lt vendidos- AlmacenGasMovimiento
            var Ltvendidos = AlmacenGasServicio.ObtenerMovimientos(FolioOperacion, fecha).FirstOrDefault().SalidaLt;

            //Obtener PrecioLt
            List<VentaPuntoDeVentaDetalle> _lst = BuscarPuntoVentaDetalle(null, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day, null).ToList();
            var PrecioLt = _lst.FirstOrDefault().PrecioUnitarioLt;
            var date = _lst.FirstOrDefault().FechaRegistro;

            //Obtener importe contado - imp credito
            var importes = ObtenerCG(FolioOperacion);
            VPuntoVentaDetalleDTO ent = CajaGeneralAdapter.ToDto(importes, FolioOperacion, Ltvendidos, PrecioLt.Value, date, LecturasInicial.P5000.Value, LecturasFinal.P5000.Value, LecturasInicial.Porcentaje.Value, LecturasFinal.Porcentaje.Value);

            return ent;
        }
        /*OBTENER  Lt y Kg vendidos del punto de venta por la cve del reporte tbl VentaPuntoDeVentaDetalle*/
        public static List<AlmacenGasMovimientoDto> ObtenerPVDetalle(short unidad, short empresa, short year, byte month, byte dia, short orden, string Folio)
        {
            List<AlmacenGasMovimientoDto> ldetalles = CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().Buscar(empresa, year, month, dia, orden, Folio));
            return ldetalles;
        }
        public static int ObtenerCorteUltimo(short unidad, short empresa, short year, byte month, byte dia)
        {
            try
            {
                return new CajaGeneralDataAccess().ObtenerCorteUltimo(unidad, empresa, year, month, dia).LastOrDefault().Orden;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int ObtenerCorteUltimo(short empresa, short year, byte month, byte dia)
        {
            try
            {
                return new CajaGeneralDataAccess().ObtenerCorteUltimo(empresa, year, month, dia).LastOrDefault().Orden;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static bool ExisteCorteUltimo(short unidad, short empresa, short year, byte month, byte dia)
        {
            try
            {
                return new CajaGeneralDataAccess().ObtenerCorteUltimo(unidad, empresa, year, month, dia).Count().Equals(0) ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool ExisteCorteUltimo(short empresa, short year, byte month, byte dia)
        {
            try
            {
                return new CajaGeneralDataAccess().ObtenerCorteUltimo(empresa, year, month, dia).Count().Equals(0) ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<VentasPipaDto> ObtenerVentasPipas(short unidad, short empresa, short year, byte month, byte dia, short orden, DateTime fecha, string FolioOperacion)
        {
            List<VentasPipaDto> lst = new List<VentasPipaDto>();
            AlmacenGasTomaLectura LecturasInicial = AlmacenGasServicio.BuscarLecturaPorFecha(unidad, TipoEventoEnum.Inicial, fecha);
            AlmacenGasTomaLectura LecturasFinal = AlmacenGasServicio.BuscarLecturaPorFecha(unidad, TipoEventoEnum.Final, fecha);
            //Obtener Lt vendidos- AlmacenGasMovimiento
            var Ltvendidos = AlmacenGasServicio.ObtenerMovimientos(FolioOperacion, fecha).FirstOrDefault().SalidaLt;
            var PrecioLt = PrecioVentaGasServicio.ObtenerPreciosVentaIdEmp(empresa).Where(x => x.IdPrecioVentaEstatus.Equals(EstatusPrecioVentaEnum.Vigente) && x.Activo).FirstOrDefault();
            List<VentasPipaDto> ldetalles = CajaGeneralAdapter.ToDTO(lst, LecturasInicial.P5000.Value, LecturasFinal.P5000.Value, Ltvendidos, PrecioLt);//AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().Buscar(empresa, year, month, dia, orden));
            return ldetalles;
        }
        public static List<CajaGeneralDTO> ObtenerIdEmp(short IdEmpresa)
        {
            List<CajaGeneralDTO> lPventas = CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().Buscar(IdEmpresa));
            return lPventas;
        }
        public static VentaCajaGeneral ObtenerCG(string cve)
        {
            return new CajaGeneralDataAccess().BuscarGralPorCve(cve);
        }
        public static List<VentaPuntoVentaDTO> ObtenerPV(string cve)
        {
            List<VentaPuntoVentaDTO> lPventas = CajaGeneralAdapter.ToDTOC(new CajaGeneralDataAccess().BuscarPorCve(cve));
            return lPventas;
        }
        public static List<VentaPuntoDeVenta> ObtenerVPV(ReporteDelDia cve)
        {
            return new CajaGeneralDataAccess().BuscarPorCve(cve);

        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasCamioneta(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasCamionetas(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasCamionetaMes(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasCamionetasMes(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasPipas(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasPipas(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasPipasMes(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasPipasMes(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalBonificaciones(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalBonificaciones(f);
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalDescuentos(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalDescuentos(f);
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasACredito(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasACredito(f);
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasEstaciones(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasEstaciones(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasEstacionesMes(DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasEstacionesMes(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaPuntoDeVenta> ObtenerTotalVentasEstaciones(EstacionCarburacion entidad, DateTime f)
        {
            return new CajaGeneralDataAccess().BuscarTotalVentasEstaciones(f, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<VentaCorteAnticipoDTO> ObtenerCE(string cve)
        {
            List<VentaCorteAnticipoDTO> lPventas = CajaGeneralAdapter.ToDTOCE(new CajaGeneralDataAccess().BuscarPorCveEC(cve));
            return lPventas;
        }
        public static RespuestaDto Actualizar(VentaCajaGeneral pv)
        {
            return new CajaGeneralDataAccess().Actualizar(pv);
        }
        public static RespuestaDto Insertar(VentaCajaGeneral pv)
        {
            return new CajaGeneralDataAccess().Insertar(pv);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentas()
        {
            return new CajaGeneralDataAccess().BuscarTodosPV();
        }
        public static RespuestaDto Actualizar(List<VentaCorteAnticipoEC> pv)
        {
            return new CajaGeneralDataAccess().Actualizar(pv);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Reporte del dia");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static void ProcesarVentasPuntosDeVenta()
        {
            ProcesarMovimientoVentas();
        }
        public static void ProcesarMovimientoVentas()
        {
            List<VentaPuntoDeVenta> ventaspv = ObtenerVentasPuntosVentaNoProc();//obtenerVentasPuntoVentaNoProc no procesadas
            if (ventaspv != null && ventaspv.Count > 0)
            {
                ActualizarTotalesVentas(ventaspv); //se actualizan totales de VentasPuntoVenta - en tabla  VentaPuntoDeVentaEfectivo
                CargarAVentasMovimientos(ventaspv); //guardar Ventas (de VentaPuntoDeVenta) a Tabla VentasMovimiento
                CargarEnAlmacenGasMov(ventaspv); //guardar registro en Almacen gas movimiento 
            }
            List<VentaCorteAnticipoEC> CortesAnticipos = ObtenerVentasCorteAnticipoNoProc();//Obtener existencia de anticipos no procesados
            if (CortesAnticipos != null && CortesAnticipos.Count() > 0)
            {
                CargarAnticiposAMovimientos(CortesAnticipos);//Guardar Anticipos en Tabla VentasMovimiento
            }
        }
        public static VentaMovimiento ObtenerVentaMovimiento(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static decimal ObtenerMovimientosJE(int jefeEstacion)
        {
            return new CajaGeneralDataAccess().BuscarTodos().Where(x => x.IdOperadorChofer.Equals(jefeEstacion)).OrderByDescending(w => w.Orden).FirstOrDefault().Saldo;
        }
        public static List<VentaMovimiento> ObtenerListaMovimientos(int puntoventa)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa);
        }
        public static VentaMovimiento ObtenerVentasMovimientos(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static VentaPuntoDeVenta ObtenerPuntoVenta(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static VentaPuntoDeVenta ObtenerPuntoVenta(int puntoventa, byte dia, byte mes, short y, short orden)
        {
            return new CajaGeneralDataAccess().BuscarPorPV(puntoventa, dia, mes, y).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static List<VentaPuntoDeVenta> ObtenerPuntosVenta()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
        public static void ActualizarTotalesVentas(List<VentaPuntoDeVenta> vm)
        {

            //List<VentaMovimiento> VtasMov = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDtoVtaM(_lst);
            //new CajaGeneralDataAccess().Insertar(VtasMov);

            //var listaMov = VtasMov.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados - por punto de venta

            //foreach (var _lMov in listaMov)
            //{
            //    var _lMovFecha = _lMov.GroupBy(x => x.FechaAplicacion.ToShortDateString()).ToList();
            //    foreach (var x in _lMovFecha)
            //    {
            //        ActualizarSaldos(x.ToList(), "PuntosVenta", 0);
            //    }
            //}
            VentaPuntoDeVenta Updt = new VentaPuntoDeVenta();
            // List<VentaPuntoDeVenta> lst = vm.GroupBy(x => x.IdPuntoVenta).SelectMany(gr => gr).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            var lst1 = vm.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            foreach (var y in lst1)
            {
                var lst = y.GroupBy(x => x.FechaAplicacion.Value.ToShortDateString()).ToList(); //VentaspuntosDeVentaAgrupados- por fecha
                                                                                                // var _lMovFecha = _lMov.GroupBy(x => x.FechaAplicacion.ToShortDateString()).ToList();
                                                                                                //Actualizar Total Dia   
                int posList = 0;
                foreach (var _lst in lst)
                {
                    int position = 0;
                    posList++;

                    foreach (var item in _lst)
                    {

                        decimal TotalAcumD = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalAcumDia", item.FechaAplicacion.Value);//CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumDia", posList + position, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalDia
                        decimal TotalAcumM = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalAcumMes", item.FechaAplicacion.Value);//CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumMes", posList + position, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalMes
                        decimal TotalAcumA = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalAcumAnio", item.FechaAplicacion.Value);//CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumAnio", posList + position, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalAnio

                        position++;
                        Updt = ObtenerPuntoVenta(item.IdPuntoVenta, item.Dia, item.Mes, item.Year, item.Orden);
                        if (!Updt.DatosProcesados)
                        {
                            if (item.Total > 0)
                            {//ObtenerSaldoActual(item.IdEmpresa,item.IdPuntoVenta, "TotalDia", item.FechaAplicacion.Value);//
                                item.TotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalDia
                                //item.TotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalMes", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - TotalMes
                                item.TotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalMes", item.FechaAplicacion.Value);//ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalMes", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalMes
                                item.TotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalAnio", item.FechaAplicacion.Value);//ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalAnio", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalAnio
                                item.TotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "TotalAnio", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - TotalAnio

                                Updt.TotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalDia); //se agrega el Total de venta al TotalDia por punto de venta
                                Updt.TotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalMes); //se agrega el Total de venta al TotalMes por punto de venta
                                Updt.TotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalAnio); //se agrega el Total de venta al TotalAnio por punto de venta
                                Updt.TotalAcumDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumD);
                                Updt.TotalAcumMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumM);
                                Updt.TotalAcumAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumA);
                            }
                            if (item.Iva > 0)
                            {
                                //item.IvaDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "IvaDia",  item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - IvaDia

                                item.IvaDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaDia", posList, item.Year, item.Mes, item.Dia);
                                item.IvaMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "IvaMes", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - IvaMes
                                item.IvaAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "IvaAnio", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - IvaAnio

                                //item.IvaMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaMes", posList, item.Year, item.Mes, item.Dia);
                                //item.IvaAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaAnio", posList, item.Year, item.Mes, item.Dia);

                                Updt.IvaDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaDia); //se agrega el Iva de venta al IvaDia por punto de venta
                                Updt.IvaMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaMes); //se agrega el Iva de venta al IvaMes por punto de venta
                                Updt.IvaAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaAnio); //se agrega el Iva de venta al IvaAnio por punto de venta
                            }
                            if (item.Subtotal > 0)
                            {
                                item.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "SubtotalDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - SubtotalDia
                                item.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "SubtotalMes", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - SubtotalMes
                                item.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "SubtotalAnio", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - SubtotalAnio

                                Updt.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalDia); //se agrega el Subtotal de venta al SubtotalDia por punto de venta
                                Updt.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalMes); //se agrega el Subtotal de venta al SubtotalMes por punto de venta
                                Updt.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalAnio); //se agrega el Subtotal de venta al SubtotalAnio por punto de venta
                            }

                            if (item.Descuento > 0)
                            {
                                item.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "DescuentoDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - DescuentoDia
                                item.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "DescuentoMes", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - DescuentoMes
                                item.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdEmpresa, item.IdPuntoVenta, "DescuentoAnio", item.FechaAplicacion.Value); //Obtener Saldo actual por punto de venta - DescuentoAnio

                                Updt.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoDia); //se agrega el Descuento de venta al DescuentoDia por punto de venta
                                Updt.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoMes); //se agrega el Descuento de venta al DescuentoMes por punto de venta
                                Updt.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoAnio); //se agrega el Descuento de venta al DescuentoAnio por punto de venta
                            }
                            Updt.DatosProcesados = true;
                            if (Updt.EsBonificacion)
                                Updt.EsBonificacion = true;
                            var rep = CajaGeneralAdapter.FromEntity(Updt);
                            new CajaGeneralDataAccess().Actualizar(rep);
                        }
                    }
                }
            }
        }
        public static void ActualizarSaldos(List<VentaMovimiento> vm, string from, decimal CSaldo)//tabla movimientos
        {
            // Updt = new VentaMovimiento();
            int position = 0;

            foreach (var _lst in vm)
            {
                position++;
                VentaMovimiento Updt = ObtenerVentaMovimiento(_lst.IdPuntoVenta, _lst.Orden);
                if (Updt != null)
                {
                    if (from == "PuntosVenta")
                    {
                        _lst.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(_lst.IdPuntoVenta, _lst.Orden, position, _lst.FechaAplicacion);//ObtenerUltimoSaldoActual(_lst.IdPuntoVenta, _lst.FechaAplicacion);//ObtenerSaldoActual(_lst.IdPuntoVenta, _lst.Orden, position);
                        if (_lst.Ingreso > 0)//Actualiza saldo proveniente de Puntos venta
                        {
                            Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSumaSaldoVenta(_lst.Ingreso, _lst.Saldo);
                        }
                        else if (_lst.Egreso > 0)//Actualiza saldo proveniente de Puntos venta
                        {
                            Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(_lst.Egreso, _lst.Saldo);
                        }
                    }
                    else //Movimientos de Cortes y Anticipos
                    {
                        if (position > 1)
                        {
                            CSaldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(_lst.IdPuntoVenta, _lst.Orden, position);
                            Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(_lst.Egreso, CSaldo);
                        }
                        else
                        {
                            Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(_lst.Egreso, CSaldo);
                        }
                    }
                    var rep = CajaGeneralAdapter.FromEntity(Updt);
                    new CajaGeneralDataAccess().Actualizar(rep);
                    if (CSaldo != 0)
                    {
                        CargarAnticiposMovimientos(Updt);//guardar movimiento Anticipo/corte como ingreso del Jefe de Estacion a Tabla VentasMovimiento
                    }
                }
            }

        }
        public static void CargarAnticiposMovimientos(VentaMovimiento entity)
        {
            VentaMovimiento lstFinal = new VentaMovimiento();

            lstFinal.IdEmpresa = entity.IdEmpresa;
            lstFinal.Year = entity.Year;
            lstFinal.Mes = entity.Mes;
            lstFinal.Dia = entity.Dia;
            lstFinal.Orden = (short)(CalcularPreciosVentaServicio.ObtenerConsecutivoOrden() + 1);//entity.Orden,
            lstFinal.IdPuntoVenta = entity.IdPuntoVenta;
            lstFinal.IdCliente = entity.IdCliente;
            lstFinal.IdOperadorChofer = entity.IdOperadorChofer;
            lstFinal.FolioOperacionDia = entity.FolioOperacionDia;
            lstFinal.FolioVenta = entity.FolioVenta;
            lstFinal.Ingreso = entity.Egreso;
            //revisar si tiene mas anticipos o corte para agregarlos al saldo
            lstFinal.Saldo = entity.Egreso + ObtenerMovimientosJE(entity.IdOperadorChofer);
            lstFinal.PuntoVenta = entity.PuntoVenta;
            lstFinal.OperadorChoferNombre = entity.OperadorChoferNombre;
            lstFinal.FechaRegistro = entity.FechaRegistro;
            lstFinal.FechaAplicacion = entity.FechaAplicacion;
            lstFinal.Descripcion = entity.Descripcion;
            lstFinal.IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(entity.IdPuntoVenta).IdCAlmacenGas;

            var rep = CajaGeneralAdapter.FromEntity(lstFinal);
            new CajaGeneralDataAccess().Insertar(rep);
        }
        public static void CargarAVentasMovimientos(List<VentaPuntoDeVenta> lst)
        {
            List<RegistrarVentasMovimientosDTO> _lst = MergedLst(lst, null);
            List<VentaMovimiento> VtasMov = CajaGeneralAdapter.FromDtoVtaM(_lst);
            new CajaGeneralDataAccess().Insertar(VtasMov);

            var listaMov = VtasMov.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados - por punto de venta

            foreach (var _lMov in listaMov)
            {
                var _lMovFecha = _lMov.GroupBy(x => x.FechaAplicacion.ToShortDateString()).ToList();
                foreach (var x in _lMovFecha)
                {
                    ActualizarSaldos(x.ToList(), "PuntosVenta", 0);
                }
            }
        }
        public static void CargarMovimientos(VentaPuntoDeVenta movimiento, List<VentaPuntoDeVentaDetalle> detventas, Decimal LinicialF, Decimal Lfinal)
        {
            var almacenGas = new PuntoVentaDataAccess().Buscar(movimiento.IdPuntoVenta).IdCAlmacenGas;
            Empresa empresa = EmpresaServicio.Obtener(movimiento.IdEmpresa);
            UnidadAlmacenGas unidadSalida = AlmacenGasServicio.ObtenerAlmacen(almacenGas);

            AlmacenGasMovimientoDto salidaGasMov = ToDto(movimiento, detventas);
            AlmacenGasMovimiento apDescDto = CajaGeneralAdapter.FromEntity(unidadSalida, empresa, salidaGasMov, LinicialF, Lfinal);

            new AlmacenGasDescargaDataAccess().Insertar(apDescDto);
        }
        public static void CargarEnAlmacenGasMov(List<VentaPuntoDeVenta> lMov)
        {
            if (lMov != null && lMov.Count > 0)
            {
                foreach (var x in lMov)
                {
                    List<VentaPuntoDeVentaDetalle> detventas = ObtenerDetallesVentasNoProc(x.IdEmpresa, x.Year, x.Mes, x.Dia, x.Orden);
                    AlmacenGasTomaLectura agtl = AlmacenGasServicio.BuscarLecturaPorFecha(new PuntoVentaDataAccess().Buscar(x.IdPuntoVenta).IdCAlmacenGas, 1, x.FechaAplicacion.Value);
                    AlmacenGasTomaLectura agtl2 = AlmacenGasServicio.BuscarLecturaPorFecha(new PuntoVentaDataAccess().Buscar(x.IdPuntoVenta).IdCAlmacenGas, 2, x.FechaAplicacion.Value);
                    CargarMovimientos(x, detventas, (agtl != null && agtl.P5000 != null) ? agtl.P5000.Value : 0, (agtl2 != null && agtl2.P5000 != null) ? agtl2.P5000.Value : 0);
                }
            }
        }
        public static void CargarAnticiposAMovimientos(List<VentaCorteAnticipoEC> lst)
        {
            //insertar movimientos de anticipos en tabla movimientos
            bool Procesados = true;
            List<VentaMovimiento> listMov = new List<VentaMovimiento>();

            var anticipos = lst.GroupBy(x => x.IdPuntoVenta);
            foreach (var x in anticipos)
            {
                var CurrentSaldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(x.Select(w => w.IdPuntoVenta).FirstOrDefault());
                foreach (var li in x)
                {/*revisar flujo*/
                    RegistrarVentasMovimientosDTO _lstmov = CajaGeneralAdapter.ToDTO(li);
                    VentaMovimiento Vtasanticipos = CajaGeneralAdapter.FromDTO(_lstmov);
                    new CajaGeneralDataAccess().Insertar(Vtasanticipos);// hace la insercion a movimientos
                    li.DatosProcesados = Procesados;
                    var rep = CajaGeneralAdapter.FromEntity(li);
                    new CajaGeneralDataAccess().Actualizar(rep); // actualiza cortes anticipos a procesados
                    listMov.Add(Vtasanticipos);
                }
                ActualizarSaldos(listMov, "", CurrentSaldo);
            }
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVentaNoProc()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVenta(int id)
        {
            return new CajaGeneralDataAccess().BuscarPorPV(id);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVenta(int id, DateTime fecha)
        {
            return new PuntoVentaDataAccess().ObtenerVentas(id, fecha);
        }
        public static List<VentaPuntoDeVentaDetalle> ObtenerDetallesVentasNoProc(short empresa, short year, byte month, byte dia, short orden)
        {
            return new CajaGeneralDataAccess().BuscarDetalleVenta(empresa, year, month, dia, orden);
        }
        public static List<VentaCorteAnticipoEC> ObtenerVentasCorteAnticipoNoProc()
        {
            bool noProcesados = false;
            return new CajaGeneralDataAccess().BuscarAnticiposC().Where(x => x.DatosProcesados.Equals(noProcesados)).OrderByDescending(x => x.FechaAplicacion).ToList();
        }
        public static List<RegistrarVentasMovimientosDTO> MergedLst(List<VentaPuntoDeVenta> pv, List<VentaCorteAnticipoEC> vca)
        {
            List<VentaPuntoDeVenta> Ventas = pv.AsEnumerable()
                                   .Select(o => new VentaPuntoDeVenta
                                   {
                                       IdEmpresa = o.IdEmpresa,
                                       Year = o.Year,
                                       Mes = o.Mes,
                                       Dia = o.Dia,
                                       Orden = o.Orden,
                                       IdPuntoVenta = o.IdPuntoVenta,
                                       IdCliente = o.IdCliente,
                                       IdOperadorChofer = o.IdOperadorChofer,
                                       FolioOperacionDia = o.FolioOperacionDia,
                                       FolioVenta = o.FolioVenta,
                                       Total = o.Total,
                                       PuntoVenta = o.PuntoVenta,
                                       OperadorChofer = o.OperadorChofer,
                                       FechaRegistro = o.FechaRegistro,
                                   }).ToList();

            List<RegistrarVentasMovimientosDTO> lstFinal = pv.Select(v => new RegistrarVentasMovimientosDTO()
            {
                IdEmpresa = v.IdEmpresa,
                Year = v.Year,
                Mes = v.Mes,
                Dia = v.Dia,
                Orden = v.Orden,
                IdPuntoVenta = v.IdPuntoVenta,
                IdCliente = v.IdCliente,
                IdOperadorChofer = v.IdOperadorChofer,
                FolioOperacionDia = v.FolioOperacionDia,
                FolioVenta = v.FolioVenta,
                Ingreso = v.Total,
                PuntoVenta = v.PuntoVenta,
                OperadorChoferNombre = v.OperadorChofer,
                FechaRegistro = DateTime.Now,
                FechaAplicacion = v.FechaAplicacion ?? DateTime.Now,
                Descripcion = "Venta",
                IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas,
            }).ToList();

            return lstFinal;
        }
        public static List<AlmacenGasMovimientoDto> ToAlmacenGasMov(List<VentaPuntoDeVenta> ag)
        {
            /***ACTUALIZAR ENTRADAS Y SALIDAS en Almacen gas movimiento***/

            List<AlmacenGasMovimientoDto> lstFinal = ag.Select(v => new AlmacenGasMovimientoDto()
            {
                IdEmpresa = v.IdEmpresa,
                Year = v.Year,
                Mes = v.Mes,
                Dia = v.Dia,
                Orden = (short)(ObtenerUltimosMovimientosDeDescargasPorUnidadAlmacenGas(v.IdEmpresa, new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas, v.FechaAplicacion ?? DateTime.Now).FirstOrDefault().Orden + 1),
                IdTipoMovimiento = 0,
                IdTipoEvento = TipoEventoEnum.Venta,
                IdOrdenVenta = v.Orden,
                IdAlmacenGas = AlmacenGasServicio.ObtenerAlmacen(new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas).IdAlmacenGas ?? 0,
                IdCAlmacenGasPrincipal = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas,//??
                IdCAlmacenGasReferencia = 0,
                IdAlmacenEntradaGasDescarga = 0,
                IdAlmacenGasRecarga = 0,
                FolioOperacionDia = v.FolioOperacionDia,
                CAlmacenPrincipalNombre = "",
                CAlmacenReferenciaNombre = "",
                OperadorChoferNombre = v.OperadorChofer,
                TipoEvento = IdentificarTipoEventoString(TipoEventoEnum.Venta).ToString(),
                TipoMovimiento = "",
                EntradaKg = 0,
                EntradaLt = 0,
                SalidaKg = 0,
                SalidaLt = 0,
                P5000Anterior = 0,
                P5000Actual = 0,
                FechaAplicacion = v.FechaAplicacion ?? DateTime.Now,
                FechaRegistro = DateTime.Now,
            }).ToList();

            return lstFinal;
        }
        public static List<RegistrarVentasMovimientosDTO> MergeLstAnticipos(List<VentaCorteAnticipoEC> mov)
        {
            List<RegistrarVentasMovimientosDTO> lstFinal = mov.Select(v => new RegistrarVentasMovimientosDTO()
            {
                IdEmpresa = v.IdEmpresa,
                Year = v.Year,
                Mes = v.Mes,
                Dia = v.Dia,
                Orden = (short)(CalcularPreciosVentaServicio.ObtenerConsecutivoOrden() + 1),//v.Orden,
                IdPuntoVenta = v.IdPuntoVenta,
                IdCliente = 0,//v.IdCliente,
                IdOperadorChofer = v.IdOperadorChofer,
                FolioOperacionDia = v.FolioOperacionDia,
                FolioVenta = v.FolioOperacion,//v.FolioVenta,
                Egreso = v.TotalAnticipado,
                PuntoVenta = v.PuntoVenta,
                OperadorChoferNombre = v.UsuarioRecibe,
                FechaRegistro = v.FechaRegistro,
                FechaAplicacion = v.FechaAplicacion,
                Descripcion = v.TipoOperacion,
                IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas,
            }).ToList();
            return lstFinal;
        }
        public static AlmacenGasMovimientoDto ToDto(VentaPuntoDeVenta v, List<VentaPuntoDeVentaDetalle> ventasdetalles)
        {
            decimal P5000Ini = 0;//Consultar de tabla AlmacenGasTomaLectura CampoP500 parametros:IdCalmacen,FechaAplicacion,IdTipoEvento.Inicial -0
            decimal P5000Fin = 0;//Consultar de tabla AlmacenGasTomaLectura CampoP500 parametros:IdCalmacen,FechaAplicacion,IdTipoEvento.Final -1
            decimal SalidaLt = 0;
            decimal Salidakg = 0;
            foreach (var val in ventasdetalles)
            { SalidaLt = +(decimal)val.CantidadLt; Salidakg = +(decimal)val.CantidadKg; }
            AlmacenGasMovimientoDto c = new AlmacenGasMovimientoDto();
            c.IdEmpresa = v.IdEmpresa;
            c.Year = v.Year;
            c.Mes = v.Mes;
            c.Dia = v.Dia;
            c.Orden = v.Orden;
            c.IdTipoMovimiento = TipoMovimientoEnum.Salida;
            c.IdTipoEvento = TipoEventoEnum.Venta;
            c.IdOrdenVenta = v.Orden;
            c.IdAlmacenGas = AlmacenGasServicio.ObtenerAlmacen(new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas).IdAlmacenGas ?? 0;
            c.IdCAlmacenGasPrincipal = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas;//??
            c.IdCAlmacenGasReferencia = null;
            c.IdAlmacenEntradaGasDescarga = null;
            c.IdAlmacenGasRecarga = null;
            c.FolioOperacionDia = v.FolioOperacionDia;
            c.CAlmacenPrincipalNombre = "";
            c.CAlmacenReferenciaNombre = "";
            c.OperadorChoferNombre = v.OperadorChofer;
            c.TipoEvento = IdentificarTipoEventoString(TipoEventoEnum.Venta).ToString();
            c.TipoMovimiento = CajaGeneralServicio.IdentificarTipoMovimientoString(TipoMovimientoEnum.Salida).ToString();
            c.EntradaKg = 0;
            c.EntradaLt = 0;
            c.SalidaKg = Salidakg;
            c.SalidaLt = SalidaLt;
            c.P5000Anterior = P5000Ini;
            c.P5000Actual = P5000Fin;
            c.FechaAplicacion = v.FechaAplicacion ?? DateTime.Now;
            c.FechaRegistro = DateTime.Now;
            return c;
        }
        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosDeDescargasPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Venta, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Venta, (short)fecha.Year, (byte)fecha.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Venta, (short)fecha.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }
        public static TipoEventoConst IdentificarTipoEventoString(byte evento)
        {
            if (evento == 9)
                return TipoEventoConst.Venta;

            return TipoEventoConst.Venta;
        }
        public static stringMovimiento IdentificarTipoMovimientoString(byte movimiento)
        {
            if (movimiento == 2)
                return stringMovimiento.Salida;

            return stringMovimiento.Salida;
        }
        public static List<VentaPuntoDeVentaDetalle> BuscarPuntoVentaDetalle(short? empresa, short year, byte month, byte dia, short? orden)
        {
            return new CajaGeneralDataAccess().BuscarDetalleVenta(empresa, year, month, dia, orden.Value).ToList();
        }
        public static decimal ObtenerPrecioLt(short? empresa, short year, byte month, byte dia, short? orden)
        {
            List<VentaPuntoDeVentaDetalle> lst = new CajaGeneralDataAccess().BuscarDetalleVenta(empresa, year, month, dia, orden.Value).ToList();

            if (lst != null)
                return lst[0].PrecioUnitarioProducto.Value; /***Cambiar por "PrecioUnitarioLitro", cuando george actualice Dto***/
            else
                return 0;
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorCAlmacenGas(short idCAlmacen, DateTime fecha)
        {
            return new PuntoVentaDataAccess().ObtenerVentas(idCAlmacen, fecha);
        }
        public static RespuestaDto ActualizarVentas(List<VentaPuntoDeVenta> ventas)
        {
            return new CajaGeneralDataAccess().Actualizar(ventas);
        }
        public static string RepVentasXPuntoVenta(List<PuntoVenta> pventas, VentasXPuntoVenta dto)
        {
            // Diferencia entre dias, horas y minutos
            TimeSpan ts = dto.PeriodoDTO.FechaFin - dto.PeriodoDTO.FechaInicio;
            // Differencia entre dias.
            int differenceInDays = ts.Days + 1;
            string respuesta = "[";
            for (DateTime date = dto.PeriodoDTO.FechaInicio; date <= dto.PeriodoDTO.FechaFin; date = date.AddDays(1.0))
            {
                respuesta += string.Concat("{'Día':'", date.ToString("dd"), "-", date.ToString("MMM"), "',");
                foreach (var pv in pventas)
                {
                    respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                         Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                                .Sum(x => x.VentaPuntoDeVentaDetalle
                                                                .Sum(y => y.CantidadLt ?? 0))).ToString(), "',");
                }
                respuesta += string.Concat("'Litros':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                               .Sum(y => y.VentaPuntoDeVentaDetalle
                                                               .Sum(z => z.CantidadLt ?? 0)))).ToString(), "',");
                respuesta += string.Concat("'Kilos':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                              .Sum(y => y.VentaPuntoDeVentaDetalle
                                                              .Sum(z => z.CantidadKg ?? 0)))).ToString(), "',");
                respuesta += string.Concat("'Total Real':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                              .Sum(y => y.Total)).ToString(), "',");
                respuesta += string.Concat("'Total c/Desc':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                            .Sum(y => y.Total - y.Descuento)).ToString(), "',");
                respuesta += string.Concat("'Crédito':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString())
                                                             && v.VentaACredito).Sum(y => y.Total)).ToString(), "',");
                respuesta += string.Concat("'Bonificación':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString())
                                                            && v.EsBonificacion).Sum(y => y.Bonificacion ?? 0))).ToString(), "',");
                respuesta += string.Concat("'Efectivo en caja':'", ((pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                            .Sum(y => y.Total))) - (pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString())
                                                            && v.VentaACredito).Sum(y => y.Total)))).ToString(), "',");
                respuesta += string.Concat("'Descuentos':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                            .Sum(y => y.Descuento)).ToString(), "'");
                //respuesta = respuesta.TrimEnd(',');
                respuesta += "},";
            }

            respuesta += string.Concat("{'Día':'Sumas',");

            foreach (var pv in pventas)
            {
                respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                     Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                            .Sum(x => x.VentaPuntoDeVentaDetalle
                                                           .Sum(y => y.CantidadLt ?? 0))).ToString(), "',");
            }
            respuesta += string.Concat("'Litros':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                          .Sum(y => y.VentaPuntoDeVentaDetalle
                                                          .Sum(z => z.CantidadLt ?? 0)))).ToString(), "',");
            respuesta += string.Concat("'Kilos':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                             .Sum(y => y.VentaPuntoDeVentaDetalle
                                                             .Sum(z => z.CantidadKg ?? 0)))).ToString(), "',");
            respuesta += string.Concat("'Total Real':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                          .Sum(y => y.Total)).ToString(), "',");
            respuesta += string.Concat("'Total c/Desc':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                        .Sum(y => y.Total - y.Descuento)).ToString(), "',");
            respuesta += string.Concat("'Crédito':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin
                                                         && v.VentaACredito).Sum(y => y.Total)).ToString(), "',");
            respuesta += string.Concat("'Bonificación':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin
                                                        && v.EsBonificacion).Sum(y => y.Bonificacion ?? 0))).ToString(), "',");
            respuesta += string.Concat("'Efectivo en caja':'", ((pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                        .Sum(y => y.Total))) - (pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin
                                                        && v.VentaACredito).Sum(y => y.Total)))).ToString(), "',");
            respuesta += string.Concat("'Descuentos':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                        .Sum(y => y.Descuento)).ToString(), "'},");

            respuesta += string.Concat("{'Día':'KILOS',");

            foreach (var pv in pventas)
            {
                respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                   Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                            .Sum(x => x.VentaPuntoDeVentaDetalle
                                                           .Sum(y => y.CantidadKg ?? 0))).ToString(), "',");
            }
            respuesta = respuesta.TrimEnd(',');
            respuesta += "},";

            respuesta += string.Concat("{'Día':'PROM VTA',");
            foreach (var pv in pventas)
            {
                respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                   Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                            .Sum(x => x.VentaPuntoDeVentaDetalle
                                                           .Sum(y => y.CantidadLt / differenceInDays ?? 0))).ToString(), "',");
            }




            respuesta = respuesta.TrimEnd(',');
            respuesta += "}]";
            return respuesta;
        }

        public static string RepVentasXPuntoVentaCamionetas(List<PuntoVenta> pventas, VentasXPuntoVenta dto)
        {
            // Diferencia entre dias, horas y minutos
            TimeSpan ts = dto.PeriodoDTO.FechaFin - dto.PeriodoDTO.FechaInicio;
            // Differencia entre dias.
            int differenceInDays = ts.Days + 1;

            string respuesta = "[";
            for (DateTime date = dto.PeriodoDTO.FechaInicio; date <= dto.PeriodoDTO.FechaFin; date = date.AddDays(1.0))
            {
                respuesta += string.Concat("{'Día':'", date.ToString("dd"), "-", date.ToString("MMM"), "',");
                foreach (var pv in pventas)
                {
                    respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                       Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                                .Sum(x => x.VentaPuntoDeVentaDetalle
                                                                .Sum(y => y.CantidadKg ?? 0))).ToString(), "',");
                }
                respuesta += string.Concat("'Kilos':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                              .Sum(y => y.VentaPuntoDeVentaDetalle
                                                              .Sum(z => z.CantidadKg ?? 0))).ToString(), "',");
                respuesta += string.Concat("'Total':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                              .Sum(y => y.Total)).ToString(), "',");
                respuesta += string.Concat("'Efectivo en caja':'", ((pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                            .Sum(y => y.Total))) - (pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString())
                                                            && v.VentaACredito).Sum(y => y.Total)))).ToString(), "',");
                respuesta += string.Concat("'Descuento':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                         .Sum(y => y.Descuento)).ToString(), "',");
                respuesta += string.Concat("'Bonificación':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString())
                                                            && v.EsBonificacion).Sum(y => y.Bonificacion)).ToString(), "',");


                respuesta += string.Concat("'20 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                       .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("20"))
                                                       .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "',");
                respuesta += string.Concat("'30 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                       .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("30"))
                                                       .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "',");
                respuesta += string.Concat("'45 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro.ToShortDateString().Equals(date.ToShortDateString()))
                                                       .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("45"))
                                                       .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "'");



                //respuesta = respuesta.TrimEnd(',');
                respuesta += "},";
            }

            respuesta += string.Concat("{'Día':'Sumas',");

            foreach (var pv in pventas)
            {
                respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                     Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                            .Sum(x => x.VentaPuntoDeVentaDetalle
                                                           .Sum(y => y.CantidadKg ?? 0))).ToString(), "',");
            }
            respuesta += string.Concat("'Kilos':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                             .Sum(y => y.VentaPuntoDeVentaDetalle
                                                             .Sum(z => z.CantidadKg ?? 0))).ToString(), "',");
            respuesta += string.Concat("'Total':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                          .Sum(y => y.Total)).ToString(), "',");
            respuesta += string.Concat("'Efectivo en caja':'", ((pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                            .Sum(y => y.Total))) - (pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin
                                            && v.VentaACredito).Sum(y => y.Total)))).ToString(), "',");
            respuesta += string.Concat("'Descuento':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                    .Sum(y => y.Descuento)).ToString(), "',");
            respuesta += string.Concat("'Bonificación':'", pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin
                                                        && v.EsBonificacion).Sum(y => y.Bonificacion)).ToString(), "',");



            respuesta += string.Concat("'20 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                    .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("20"))
                                                    .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "',");
            respuesta += string.Concat("'30 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                   .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("30"))
                                                   .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "',");
            respuesta += string.Concat("'45 KG':'", Math.Truncate(pventas.Sum(x => x.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                   .Sum(y => y.VentaPuntoDeVentaDetalle.Where(vd => vd.ProductoDescripcion.Contains("45"))
                                                   .Sum(z => z.CantidadProducto ?? 0)))).ToString(), "'},");


            respuesta += string.Concat("{'Día':'PROM VTA',");

            foreach (var pv in pventas)
            {
                respuesta += string.Concat("'", pv.UnidadesAlmacen.Numero, "':'",
                   Math.Truncate(pv.VentaPuntoDeVenta.Where(v => v.FechaRegistro >= dto.PeriodoDTO.FechaInicio && v.FechaRegistro <= dto.PeriodoDTO.FechaFin)
                                                            .Sum(x => x.VentaPuntoDeVentaDetalle
                                                           .Sum(y => y.CantidadKg / differenceInDays ?? 0))).ToString(), "',");
            }




            respuesta = respuesta.TrimEnd(',');
            respuesta += "}]";
            return respuesta;

        }
        public static string RepEquipoDeTransporte(List<PuntoVenta> pventas, PeriodoDTO dto)
        {
            // Diferencia entre dias, horas y minutos
            TimeSpan ts = dto.FechaFin - dto.FechaInicio;
            // Differencia entre dias.
            int differenceInDays = ts.Days + 1;

            string respuesta = "[";

            foreach (var Unidad in pventas)
            {

                int idPunt = Unidad.UnidadesAlmacen.Pipa != null ? Unidad.UnidadesAlmacen.Pipa.CDetalleEquipoTransporte.FirstOrDefault().IdEquipoTransporteDetalle : Unidad.UnidadesAlmacen.Camioneta.CDetalleEquipoTransporte.FirstOrDefault().IdEquipoTransporteDetalle;
                var listaRecargas = RecargaCombustibleServicio.Buscar(dto.FechaInicio, dto.FechaFin, idPunt, Unidad.UnidadesAlmacen.IdPipa == null ? false : true, Unidad.UnidadesAlmacen.IdCamioneta == null ? false : true, false);
                var nombre = Unidad.UnidadesAlmacen.Numero;
                respuesta += string.Concat("{'Unidades':'", Unidad.UnidadesAlmacen.Numero, "',");

                for (DateTime date = dto.FechaInicio; date <= dto.FechaFin; date = date.AddDays(1.0))
                {
                    respuesta += string.Concat("'Día ", date.ToString("dd"), "-", date.ToString("MMM"), "':'",
                       Math.Truncate(listaRecargas.Where(y => y.FechaRecarga.ToShortDateString().Equals(date.ToShortDateString())).Sum(x => x.Monto ?? 0)).ToString(), "',");                   
                }
                respuesta += string.Concat("'Total':'", listaRecargas.Sum(y => y.Monto).ToString(), "',");

                respuesta += string.Concat("'Litros':'", Math.Truncate(listaRecargas.Sum(z => z.LitrosRecargados)).ToString(), "'},");
            }
            respuesta = respuesta.TrimEnd(',');
            respuesta += "]";


            return respuesta;

        }
    }
}

