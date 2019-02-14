using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Ventas
{
    public class CalcularPreciosVentaServicio
    {
        public static decimal ObtenerKilogramosRemanentes(decimal p5000Anterior, decimal p5000Actual, decimal magnatelIni, decimal magnatelFin)
        {
            var p5000 = p5000Actual - p5000Anterior;
            var Magnatel = magnatelFin - magnatelIni;

            if (p5000 < Magnatel)
                return Magnatel - p5000;
            else
                return p5000 - Magnatel;
        }
        public static decimal ObtenerPrecioSalidaKg(decimal precioPemex, decimal utilidadEsperada, decimal precioFlete)
        {
            return precioPemex + utilidadEsperada + precioFlete;
        }
        public static decimal ObtenerPrecioSalidaLt(decimal precioSalida, decimal factorLitrosaKilos)
        {
            return precioSalida - factorLitrosaKilos;
        }

        public static decimal ObtenerPrecioPemexKilosaLt(decimal precioPemex, decimal factorLitrosaKilos)
        {
            return precioPemex * factorLitrosaKilos;
        }
        public static decimal ObtenerUtilidadEsperadaLt(decimal factorLitrosaKilos, decimal utilidadEsperada)
        {
            return factorLitrosaKilos * factorLitrosaKilos;
        }
        public static byte GetEstatusPrecioVenta(string status)
        {
            if (status == "Programada")
                return EstatusPrecioVentaEnum.Programado;

            if (status == "Vigente")
                return EstatusPrecioVentaEnum.Vigente;

            if (status == "Vencido")
                return EstatusPrecioVentaEnum.Vencido;

            return EstatusPrecioVentaEnum.Programado;
        }
        public static decimal ObtenerSumaSaldoVenta(decimal Ingreso, decimal Saldo)
        {
            return Ingreso + Saldo;
        }

        public static decimal ObtenerSaldoVentaEgreso(decimal Egreso, decimal Saldo)
        {
            return Saldo - Egreso;
        }

        public static decimal ObtenerLtVenta(short empresa, short anio, byte mes, byte dia, short orden, string type)//VentaPuntoDeVentaDetalle
        {
            List<VentaPuntoDeVentaDetalle> getTot = new CajaGeneralDataAccess().BuscarDetalleVenta(empresa, anio, mes, dia, orden);
            decimal total = 0;
            if (type == "Kg")
            {
                foreach (var it in getTot)
                {
                    decimal cant = it.CantidadKg ?? 0;
                    total += cant;
                }
            }
            else if (type == "Lt")
            {
                foreach (var it in getTot)
                {
                    decimal cant = it.CantidadLt ?? 0;
                    total += cant;
                }
            }
            return total;
        }

        public static decimal ObtenerSaldoActual(int puntoventa)//Movimientos
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).OrderByDescending(x => x.Orden).FirstOrDefault().Saldo;

        }
        public static decimal ObtenerSaldoActual(int puntoventa, short orden, int position, DateTime fecha)//Movimientos-Saldos
        {
            decimal value = 0;
            if (position > 1)
            {
                var lst = new CajaGeneralDataAccess().BuscarUltimoOrden(puntoventa, fecha).OrderByDescending(x => x.Orden).Where(x => x.Orden < orden).FirstOrDefault();
                value = lst != null ? lst.Saldo : 0;

            }
            else
            {
                value = 0;//new CajaGeneralDataAccess().Buscar(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().Saldo;
            }

            return value;

        }
        public static decimal ObtenerSaldoActual(int puntoventa, short orden, int position)//Movimientos
        {
            decimal value = 0;
            if (position > 1)
            {
                value = new CajaGeneralDataAccess().Buscar(puntoventa).Where(x => x.Orden == (orden - 1)).FirstOrDefault().Saldo;//.OrderBy(x => x.Orden).FirstOrDefault().Saldo;
            }
            else
            {
                value = new CajaGeneralDataAccess().Buscar(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().Saldo;
            }

            return value;//Descendingreturn new CajaGeneralDataAccess().Buscar(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().Saldo;

        }
        public static decimal ObtenerUltimoSaldoActual(int puntoventa, DateTime fecha)//Movimientos
        {
            if (new CajaGeneralDataAccess().BuscarUltimoMovSaldo(puntoventa, fecha) != null)
                return new CajaGeneralDataAccess().BuscarUltimoMovSaldo(puntoventa, fecha).Saldo;
            else
                return 0;
        }

        public static VentaPuntoDeVenta ObtenerUltimoSaldoEfectivo(short empresa, int puntoventa, string Tipo, DateTime fecha)//VentaPuntoDeVenta-Efectivo
        {
            return new CajaGeneralDataAccess().BuscarUltimoMovimiento(empresa, puntoventa, Tipo, fecha);
        }

        public static decimal ObtenerSaldoActual(short empresa, int puntoventa, string Tipo, DateTime fecha)//puntos de venta
        {
            decimal value = 0;
            VentaPuntoDeVenta _Obj = ObtenerUltimoSaldoEfectivo(empresa, puntoventa, Tipo, fecha);

            if (Tipo == "TotalDia")
            {
                if (_Obj != null)
                    value = _Obj.TotalDia;
            }

            if (Tipo == "TotalMes")
            {
                if (_Obj != null)
                    value = _Obj.TotalMes;
            }
            if (Tipo == "TotalAnio")
            {
                if (_Obj != null)
                    value = _Obj.TotalAnio;
            }

            if (Tipo == "TotalAcumDia")
            {
                if (_Obj != null)
                    value = _Obj.TotalAcumDia;
            }
            if (Tipo == "TotalAcumMes")
            {
                if (_Obj != null)
                    value = _Obj.TotalAcumMes;
            }

            if (Tipo == "TotalAcumAnio")
            {
                if (_Obj != null)
                    value = _Obj.TotalAcumAnio;
            }

            if (Tipo == "IvaDia")
            {
                if (_Obj != null)
                    value = _Obj.IvaDia;
            }

            if (Tipo == "IvaMes")
            {
                if (_Obj != null)
                    value = _Obj.IvaMes;
            }
            if (Tipo == "IvaAnio")
            {
                if (_Obj != null)
                    value = _Obj.IvaAnio;
            }

            if (Tipo == "SubtotalDia")
            {
                if (_Obj != null)
                    value = _Obj.SubtotalDia;
            }

            if (Tipo == "SubtotalMes")
            {
                if (_Obj != null)
                    value = _Obj.SubtotalMes;
            }

            if (Tipo == "SubtotalAnio")
            {
                if (_Obj != null)
                    value = _Obj.SubtotalAnio;
            }

            if (Tipo == "DescuentoDia")
            {
                if (_Obj != null)
                    value = _Obj.DescuentoDia;
            }

            if (Tipo == "DescuentoMes")
            {
                if (_Obj != null)
                    value = _Obj.DescuentoMes;
            }

            if (Tipo == "DescuentoAnio")
            {
                if (_Obj != null)
                    value = _Obj.DescuentoAnio;
            }

            return value;
        }
        public static decimal ObtenerSaldoActual(int puntoventa, int position, string Tipo, int p, short anio, byte mes, byte dia)//puntos de venta
        {
            decimal value = 0;//.OrderByDescending(x => x.FechaAplicacion).FirstOrDefault().TotalDia;

            if (Tipo == "TotalDia")
            {
                if (position > 1)
                {
                    List<VentaPuntoDeVenta> pv = new CajaGeneralDataAccess().BuscarPorPV(puntoventa);
                    //value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalDia;
                    VentaPuntoDeVenta v = pv.Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault();
                    if (v != null)
                        value = v.TotalDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalDia;
                }
            }
            if (Tipo == "TotalMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalMes;
                }
            }
            if (Tipo == "TotalAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalAnio;
                }
            }

            if (Tipo == "TotalAcumDia")
            {
                if (p > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalAcumDia;
                }
            }
            if (Tipo == "TotalAcumMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalAcumMes;
                }
            }
            if (Tipo == "TotalAcumAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().TotalAcumAnio;
                }
            }

            if (Tipo == "IvaDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault() != null ? new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().IvaDia : 0;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().IvaDia;
                }
            }

            if (Tipo == "IvaMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault() != null ? new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().IvaMes : 0;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().IvaMes;
                }
            }
            if (Tipo == "IvaAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().IvaAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().IvaAnio;
                }
            }

            if (Tipo == "SubtotalDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault() != null ? new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalDia : 0;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().SubtotalDia;
                }
            }

            if (Tipo == "SubtotalMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().SubtotalMes;
                }
            }

            if (Tipo == "SubtotalAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().SubtotalAnio;
                }
            }

            if (Tipo == "DescuentoDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault() != null ? new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoDia : 0;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().DescuentoDia;
                }
            }

            if (Tipo == "DescuentoMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().DescuentoMes;
                }
            }

            if (Tipo == "DescuentoAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true && x.Year == anio && x.Mes == mes && x.Dia == dia).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Year == anio && x.Mes == mes && x.Dia == dia).OrderBy(x => x.Orden).FirstOrDefault().DescuentoAnio;
                }
            }


            return value;
        }

        public static decimal ObtenerUltimoSaldoDia(int puntoventa, int position, string Tipo, int p)//puntos de venta
        {
            // return new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Orden == (orden - 1)).FirstOrDefault().TotalDia;//.OrderByDescending(x => x.Orden).FirstOrDefault().TotalDia;
            decimal value = 0;//.OrderByDescending(x => x.FechaAplicacion).FirstOrDefault().TotalDia;
            if (Tipo == "TotalDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().TotalDia;
                }
            }
            if (Tipo == "TotalMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().TotalMes;
                }
            }
            if (Tipo == "TotalAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().TotalAnio;
                }
            }
            if (Tipo == "TotalAcumDia")
            {
                if (p > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().OrderBy(x => x.Orden).FirstOrDefault().TotalAcumDia;
                }
            }
            if (Tipo == "TotalAcumMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().OrderBy(x => x.Orden).FirstOrDefault().TotalAcumMes;
                }
            }
            if (Tipo == "TotalAcumAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().TotalAcumAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarTodosPV().OrderBy(x => x.Orden).FirstOrDefault().TotalAcumAnio;
                }
            }
            if (Tipo == "IvaDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().IvaDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().IvaDia;
                }
            }
            if (Tipo == "IvaMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().IvaMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().IvaMes;
                }
            }
            if (Tipo == "IvaAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().IvaAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().IvaAnio;
                }
            }
            if (Tipo == "SubtotalDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().SubtotalDia;
                }
            }
            if (Tipo == "SubtotalMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().SubtotalMes;
                }
            }
            if (Tipo == "SubtotalAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().SubtotalAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().SubtotalAnio;
                }
            }
            if (Tipo == "DescuentoDia")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoDia;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().DescuentoDia;
                }
            }
            if (Tipo == "DescuentoMes")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoMes;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().DescuentoMes;
                }
            }

            if (Tipo == "DescuentoAnio")
            {
                if (position > 1)
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.DatosProcesados == true).OrderByDescending(x => x.Orden).FirstOrDefault().DescuentoAnio;
                }
                else
                {
                    value = new CajaGeneralDataAccess().BuscarPorPV(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().DescuentoAnio;
                }
            }
            return value;
        }
        public static decimal ObtenerSumaTotalVenta(decimal Total, decimal TotalDia)
        {
            return Total + TotalDia;
        }

        public static decimal ObtenerKilosVenta(decimal capacidad, decimal cantidad)
        {
            return capacidad * cantidad;
        }

        public static short ObtenerConsecutivoOrden()
        {
            return new CajaGeneralDataAccess().BuscarTodos().OrderByDescending(x => x.Orden).FirstOrDefault().Orden;
        }
        public static decimal ObtenerKilosCamioneta(decimal TotalKilosInical, decimal TotalKilosVendidos)
        {
           return TotalKilosInical - TotalKilosVendidos;
        }
    }
}
