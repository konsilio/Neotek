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
        public static decimal ObtenerPrecioSalidaKg(decimal precioPemex, decimal utilidadEsperada)
        {
            return precioPemex + utilidadEsperada;
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

            if (value == 0)
            { }
            return value;//Descendingreturn new CajaGeneralDataAccess().Buscar(puntoventa).OrderBy(x => x.Orden).FirstOrDefault().Saldo;

        }
        public static decimal ObtenerSaldoActual(int puntoventa, short orden, int position, string Tipo)//puntps de venta
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

        public static short ObtenerConsecutivoOrden()
        {
            return new CajaGeneralDataAccess().BuscarTodos().OrderByDescending(x => x.Orden).FirstOrDefault().Orden;
        }
    }
}
