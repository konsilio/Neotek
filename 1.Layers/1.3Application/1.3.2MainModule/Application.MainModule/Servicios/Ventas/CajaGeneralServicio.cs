using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Ventas
{
    public class CajaGeneralServicio
    {
        public static List<CajaGeneralDTO> Obtener()
        {
            List<CajaGeneralDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().BuscarTodos());
            return lPventas;
        }

        public static List<CajaGeneralDTO> ObtenerIdEmp(short IdEmpresa)
        {
            List<CajaGeneralDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().Buscar(IdEmpresa));
            return lPventas;
        }
        public static VentaCajaGeneral ObtenerCG(string cve)
        {
            return new CajaGeneralDataAccess().BuscarGralPorCve(cve);
        }
        public static List<VentaPuntoVentaDTO> ObtenerPV(string cve)
        {
            List<VentaPuntoVentaDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTOC(new CajaGeneralDataAccess().BuscarPorCve(cve));
            return lPventas;
        }

        public static List<VentaCorteAnticipoDTO> ObtenerCE(string cve)
        {
            List<VentaCorteAnticipoDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTOCE(new CajaGeneralDataAccess().BuscarPorCveEC(cve));
            return lPventas;
        }

        public static RespuestaDto Actualizar(List<VentaPuntoDeVenta> pv)
        {
            return new CajaGeneralDataAccess().Actualizar(pv);
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
                ActualizarTotalesVentas(ventaspv); //se actualizan totales de VentasPuntoVenta
                CargarAVentasMovimientos(ventaspv);//guardar Ventas (de VentaPuntoDeVenta) a Tabla VentasMovimiento
                CargarEnAlmacenGasMov(ventaspv);//guardar registro en Almacen gas movimiento y remanentes
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
        public static List<VentaPuntoDeVenta> ObtenerPuntosVenta()
        {
            return new CajaGeneralDataAccess().Buscar();
        }

        public static void ActualizarTotalesVentas(List<VentaPuntoDeVenta> vm)
        {
            VentaPuntoDeVenta Updt = new VentaPuntoDeVenta();
            // List<VentaPuntoDeVenta> lst = vm.GroupBy(x => x.IdPuntoVenta).SelectMany(gr => gr).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            var lst = vm.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            //Actualizar Total Dia   
            int posList = 0;
            foreach (var _lst in lst)
            {
                int position = 0;
                posList++;

                foreach (var item in _lst)
                {
                    decimal TotalAcumD = CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumDia", posList + position,item.Year, item.Mes,item.Dia ); //Obtener Saldo actual por punto de venta - TotalAnio
                    decimal TotalAcumM = CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumMes", posList + position, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalAnio
                    decimal TotalAcumA = CalcularPreciosVentaServicio.ObtenerSaldoActual(0, posList, "TotalAcumAnio", posList + position, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalAnio

                    position++;
                    Updt = ObtenerPuntoVenta(item.IdPuntoVenta, item.Orden);
                    if (item.Total > 0)
                    {
                        item.TotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalDia
                        item.TotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalMes", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalMes
                        item.TotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "TotalAnio", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - TotalAnio

                        Updt.TotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalDia); //se agrega el Total de venta al TotalDia por punto de venta
                        Updt.TotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalMes); //se agrega el Total de venta al TotalMes por punto de venta
                        Updt.TotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalAnio); //se agrega el Total de venta al TotalAnio por punto de venta
                        Updt.TotalAcumDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumD);
                        Updt.TotalAcumMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumM);
                        Updt.TotalAcumAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, TotalAcumA);
                    }
                    if (item.Iva > 0)
                    {
                        item.IvaDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - IvaDia
                        item.IvaMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaMes", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - IvaMes
                        item.IvaAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "IvaAnio", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - IvaAnio

                        Updt.IvaDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaDia); //se agrega el Iva de venta al IvaDia por punto de venta
                        Updt.IvaMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaMes); //se agrega el Iva de venta al IvaMes por punto de venta
                        Updt.IvaAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaAnio); //se agrega el Iva de venta al IvaAnio por punto de venta
                    }
                    if (item.Subtotal > 0)
                    {
                        item.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "SubtotalDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - SubtotalDia
                        item.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "SubtotalMes", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - SubtotalMes
                        item.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "SubtotalAnio", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - SubtotalAnio

                        Updt.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalDia); //se agrega el Subtotal de venta al SubtotalDia por punto de venta
                        Updt.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalMes); //se agrega el Subtotal de venta al SubtotalMes por punto de venta
                        Updt.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalAnio); //se agrega el Subtotal de venta al SubtotalAnio por punto de venta
                    }

                    if (item.Descuento > 0)
                    {
                        item.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "DescuentoDia", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - DescuentoDia
                        item.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "DescuentoMes", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - DescuentoMes
                        item.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, position, "DescuentoAnio", posList, item.Year, item.Mes, item.Dia); //Obtener Saldo actual por punto de venta - DescuentoAnio

                        Updt.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoDia); //se agrega el Descuento de venta al DescuentoDia por punto de venta
                        Updt.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoMes); //se agrega el Descuento de venta al DescuentoMes por punto de venta
                        Updt.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoAnio); //se agrega el Descuento de venta al DescuentoAnio por punto de venta

                    }
                    Updt.DatosProcesados = true;
                    var rep = CajaGeneralAdapter.FromEntity(Updt);
                    new CajaGeneralDataAccess().Actualizar(rep);
                }
            }
        }

        public static void ActualizarSaldos(List<VentaMovimiento> vm, string from, decimal CSaldo)
        {
            VentaMovimiento Updt = new VentaMovimiento();
            int position = 0;
            foreach (var _lst in vm)
            {
                position++;
                Updt = ObtenerVentaMovimiento(_lst.IdPuntoVenta, _lst.Orden);

                if (from == "PuntosVenta")
                {
                    _lst.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(_lst.IdPuntoVenta, _lst.Orden, position);
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
            List<VentaMovimiento> VtasMov = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDtoVtaM(_lst);
            new CajaGeneralDataAccess().Insertar(VtasMov);

            var listaMov = VtasMov.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados - por punto de venta

            foreach (var _lMov in listaMov)
            {
                ActualizarSaldos(_lMov.ToList(), "PuntosVenta", 0);
            }
        }
        public static AlmacenGasMovimiento AplicarVentaMov(UnidadAlmacenGas unidadSalida, Empresa empresa, AlmacenGasMovimientoDto Dto)
        {

            decimal kilogramosPapeletaTractor = 0;//descargasGas[0].MasaKg.Value;
            decimal litrosRealesTractor = CalcularGasServicio.ObtenerLitrosEnElTanque(0, 0);//(descargasGas[0].CapacidadTanqueLt.Value, descargasGas[0].PorcenMagnatelOcularTractorINI.Value);
            decimal kilogramosRealesTractor = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosRealesTractor, empresa.FactorLitrosAKilos);
            decimal kilogramosRemanentes = CalcularGasServicio.ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);
            decimal litrosRemanentes = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosRemanentes, empresa.FactorLitrosAKilos);

            decimal unidadSalidaCantidadKg = unidadSalida.CantidadActualKg;
            decimal unidadSalidaCantidadLt = unidadSalida.CantidadActualLt;
            decimal unidadSalidaPorcentaje = unidadSalida.PorcentajeActual;


            //unidadSalida.CantidadActualLt = CalcularGasServicio.RestarLitros(unidadSalidaCantidadLt, Dto.SalidaLt);
            //unidadSalida.CantidadActualKg = CalcularGasServicio.RestarKilogramos(unidadSalida.CantidadActualKg, Dto.SalidaLt);
            //unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(unidadSalida.CapacidadTanqueLt.Value, unidadSalida.CantidadActualLt);

            AlmacenGasMovimiento ulMov = AlmacenGasServicio.ObtenerUltimoMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas);
            AlmacenGasMovimiento ulMovDescarga = AlmacenGasServicio.ObtenerUltimoMovimientoPorUnidadAlmacenGas(Dto.IdEmpresa, unidadSalida.IdCAlmacenGas, Dto.FechaAplicacion);
//ObtenerUltimoMovimientoDeDescargaPorUnidadAlmacenGas
            return new AlmacenGasMovimiento()
            {
                IdEmpresa = Dto.IdEmpresa,
                Year = Dto.Year,
                Mes = Dto.Mes,
                Dia = Dto.Dia,
                Orden = ulMov != null && ulMov.Orden > 0 ? (short)(ulMov.Orden + 1) : (short)1,
                IdTipoMovimiento = TipoMovimientoEnum.Salida,
                IdTipoEvento = TipoEventoEnum.Venta,
                IdOrdenVenta = Dto.IdOrdenVenta,
                IdAlmacenGas = Dto.IdAlmacenGas,
                IdCAlmacenGasPrincipal = unidadSalida.IdCAlmacenGas,
                IdCAlmacenGasReferencia = Dto.IdCAlmacenGasReferencia,
                IdAlmacenEntradaGasDescarga = Dto.IdAlmacenEntradaGasDescarga,
                IdAlmacenGasRecarga = Dto.IdAlmacenGasRecarga,
                FolioOperacionDia = Dto.FolioOperacionDia,
                CAlmacenPrincipalNombre = unidadSalida.Numero,
                CAlmacenReferenciaNombre = Dto.CAlmacenReferenciaNombre,
                OperadorChoferNombre = Dto.OperadorChoferNombre,
                TipoEvento = CajaGeneralServicio.IdentificarTipoEventoString(TipoEventoEnum.Venta).ToString(),
                TipoMovimiento = CajaGeneralServicio.IdentificarTipoMovimientoString(TipoMovimientoEnum.Salida).ToString(),
                EntradaKg = 0,
                EntradaLt = 0,
                SalidaKg = Dto.SalidaKg,
                SalidaLt = Dto.SalidaLt,
                CantidadAnteriorKg = unidadSalidaCantidadKg,
                CantidadAnteriorLt = unidadSalidaCantidadLt,
                CantidadActualKg = CalcularGasServicio.RestarKilogramos(unidadSalidaCantidadKg, Dto.SalidaKg),
                CantidadActualLt = CalcularGasServicio.RestarKilogramos(unidadSalida.CantidadActualKg, empresa.FactorLitrosAKilos),
                PorcentajeAnterior = unidadSalidaPorcentaje,
                PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(unidadSalida.CapacidadTanqueLt.Value, unidadSalida.CantidadActualLt),
                P5000Anterior = null,
                P5000Actual = null,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.CantidadAcumuladaDiaKg : 0, Dto.SalidaKg),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.CantidadAcumuladaDiaLt : 0, Dto.SalidaLt),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.CantidadAcumuladaMesKg : 0, Dto.SalidaKg),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.CantidadAcumuladaMesLt : 0, Dto.SalidaLt),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.CantidadAcumuladaAnioKg : 0, Dto.SalidaKg),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.CantidadAcumuladaAnioLt : 0, Dto.SalidaLt),

                RemaKg = kilogramosRemanentes,
                RemaLt = litrosRemanentes,
                RemaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaDiaKg != null ? ulMovDescarga.RemaDiaKg.Value : 0, kilogramosRemanentes),
                RemaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaDiaLt != null ? ulMovDescarga.RemaDiaLt.Value : 0, litrosRemanentes),
                RemaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaMesKg != null ? ulMovDescarga.RemaMesKg.Value : 0, kilogramosRemanentes),
                RemaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaMesLt != null ? ulMovDescarga.RemaMesLt.Value : 0, litrosRemanentes),
                RemaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaAnioKg != null ? ulMovDescarga.RemaAnioKg.Value : 0, kilogramosRemanentes),
                RemaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaAnioLt != null ? ulMovDescarga.RemaAnioLt.Value : 0, litrosRemanentes),
                RemaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.RemaAcumDiaKg : 0, kilogramosRemanentes),
                RemaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumDiaLt : 0, litrosRemanentes),
                RemaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.RemaAcumMesKg : 0, kilogramosRemanentes),
                RemaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumMesLt : 0, litrosRemanentes),
                RemaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.RemaAcumAnioKg : 0, kilogramosRemanentes),
                RemaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumAnioLt : 0, litrosRemanentes),
                FechaAplicacion = Dto.FechaAplicacion,
                FechaRegistro = DateTime.Now,

            };

            // Movimiento = AlmacenGasAdapter.FromEntity(Dto, unidadEntrada, ulMov, invAnterior),
        }

        public static AlmacenGasMovimiento CargarMovimientos(VentaPuntoDeVenta movimiento)
        {
            var almacenGas = new PuntoVentaDataAccess().Buscar(movimiento.IdPuntoVenta).IdCAlmacenGas;
            Empresa empresa = EmpresaServicio.Obtener(movimiento.IdEmpresa);
            UnidadAlmacenGas unidadEntrada = AlmacenGasServicio.ObtenerAlmacen(almacenGas);

            AlmacenGasMovimientoDto entGasMov = ToDto(movimiento);
            AlmacenGasMovimiento apDescDto = AplicarVentaMov(unidadEntrada, empresa, entGasMov);

            new AlmacenGasDescargaDataAccess().Insertar(apDescDto);
            return apDescDto;
        }

        public static List<AlmacenGasMovimiento> CargarMovimientos(List<VentaPuntoDeVenta> lMov)
        {
            List<AlmacenGasMovimiento> aplicaciones = new List<AlmacenGasMovimiento>();
            List<VentaPuntoDeVenta> ventaGas = lMov;

            if (ventaGas != null && ventaGas.Count > 0)
            {
                ventaGas.ForEach(x => aplicaciones.Add(CargarMovimientos(x)));
            }

            return aplicaciones;
        }

        public static void CargarEnAlmacenGasMov(List<VentaPuntoDeVenta> lst)
        {
            List<AlmacenGasMovimiento> AppMov = CargarMovimientos(lst);
            //List<AlmacenGasMovimiento> AGasMov = AdaptadoresDTO.Almacenes.AlmacenGasAdapter.FromDto(_lst);
            //new AlmacenGasDataAccess().Insertar(AGasMov);
            // new AlmacenGasDescargaDataAccess().Insertar(AppMov);
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
                    RegistrarVentasMovimientosDTO _lstmov = MergeAnticipos(li);
                    VentaMovimiento Vtasanticipos = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDTO(_lstmov);
                    new CajaGeneralDataAccess().Insertar(Vtasanticipos);// hace la insercion a movimientos
                    li.DatosProcesados = Procesados;
                    var rep = CajaGeneralAdapter.FromEntity(li);
                    new CajaGeneralDataAccess().Actualizar(rep); // actualiza cortes anticipos a procesados
                    listMov.Add(Vtasanticipos);
                }
                ActualizarSaldos(listMov, "", CurrentSaldo);
            }
            //   }
            //actualizar totales de VentasMovimientos
            //     var puntosventa = lst.GroupBy(x => x.IdPuntoVenta);
            //foreach (var x in puntosventa)
            //{/*Seleccionar ultimo registro del movimiento, perteneciente a los insertados de los anticipos*/
            // //ActualizarSaldos(ObtenerListaMovimientos(x.Select(z => z.IdPuntoVenta).FirstOrDefault()),"");
            // //insertar registro en tabla movimientos como ingreso al jefe de estaciones, cada registro o monto
            //    List<RegistrarVentasMovimientosDTO> _lstmov = MergeLstAnticipos(x.ToList());
            //    List<VentaMovimiento> _lstmov2 = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDtoVtaM(_lstmov);
            //    //var CurrentSaldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(x.Select(w => w.IdPuntoVenta).FirstOrDefault());
            //    ActualizarSaldos(_lstmov2, "", CurrentSaldo);
            //}

        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVentaNoProc()
        {
            return new CajaGeneralDataAccess().Buscar();
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
                Descripcion = "",
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
                CantidadAnteriorKg = 0,
                CantidadAnteriorLt = 0,
                CantidadActualKg = 0,
                CantidadActualLt = 0,
                CantidadAcumuladaDiaKg = 0,
                CantidadAcumuladaDiaLt = 0,
                CantidadAcumuladaMesKg = 0,
                CantidadAcumuladaMesLt = 0,
                CantidadAcumuladaAnioKg = 0,
                CantidadAcumuladaAnioLt = 0,
                PorcentajeActual = 0,
                P5000Anterior = 0,
                P5000Actual = 0,
                FechaAplicacion = v.FechaAplicacion ?? DateTime.Now,
                FechaRegistro = DateTime.Now,
            }).ToList();

            return lstFinal;
        }
        public static RegistrarVentasMovimientosDTO MergeAnticipos(VentaCorteAnticipoEC v)
        {
            RegistrarVentasMovimientosDTO lstFinal = new RegistrarVentasMovimientosDTO();

            lstFinal.IdEmpresa = v.IdEmpresa;
            lstFinal.Year = v.Year;
            lstFinal.Mes = v.Mes;
            lstFinal.Dia = v.Dia;
            lstFinal.Orden = (short)(CalcularPreciosVentaServicio.ObtenerConsecutivoOrden() + 1);//v.Orden,
            lstFinal.IdPuntoVenta = v.IdPuntoVenta;
            lstFinal.IdCliente = 0;//v.IdCliente,
            lstFinal.IdOperadorChofer = v.IdOperadorChofer;
            lstFinal.FolioOperacionDia = v.FolioOperacionDia;
            lstFinal.FolioVenta = v.FolioOperacion;//v.FolioVenta,
            lstFinal.Egreso = v.TotalAnticipado;
            lstFinal.PuntoVenta = v.PuntoVenta;
            lstFinal.OperadorChoferNombre = v.OperadorChofer;
            lstFinal.FechaRegistro = DateTime.Now;
            lstFinal.FechaAplicacion = v.FechaAplicacion;
            lstFinal.Descripcion = v.TipoOperacion;
            lstFinal.IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas;

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

        public static AlmacenGasMovimientoDto ToDto(VentaPuntoDeVenta v)
        {

            AlmacenGasMovimientoDto c = new AlmacenGasMovimientoDto();
            c.IdEmpresa = v.IdEmpresa;
            c.Year = v.Year;
            c.Mes = v.Mes;
            c.Dia = v.Dia;
            c.Orden = 0;
            c.IdTipoMovimiento = 0;
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
            c.TipoMovimiento = "";
            c.EntradaKg = 0;
            c.EntradaLt = 0;
            c.SalidaKg = 0;
            c.SalidaLt = 0;
            c.CantidadAnteriorKg = 0;
            c.CantidadAnteriorLt = 0;
            c.CantidadActualKg = 0;
            c.CantidadActualLt = 0;
            c.CantidadAcumuladaDiaKg = 0;
            c.CantidadAcumuladaDiaLt = 0;
            c.CantidadAcumuladaMesKg = 0;
            c.CantidadAcumuladaMesLt = 0;
            c.CantidadAcumuladaAnioKg = 0;
            c.CantidadAcumuladaAnioLt = 0;
            c.PorcentajeActual = 0;
            c.P5000Anterior = 0;
            c.P5000Actual = 0;
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
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var movimientos = ObtenerUltimosMovimientosDeDescargasPorUnidadAlmacenGas(idEmpresa, idCAlmacenGas, fecha);

            if (movimientos.ElementAt(0) != null) return movimientos.ElementAt(0);

            if (movimientos.ElementAt(1) != null) return movimientos.ElementAt(1);

            if (movimientos.ElementAt(2) != null) return movimientos.ElementAt(2);

            return AlmacenGasAdapter.FromInit();
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
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
        }
    }
}
