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
        public static decimal ObtenerSaldoActual(int puntoventa)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).OrderByDescending(x => x.FechaAplicacion).FirstOrDefault().Saldo;
        }

        public static void ProcesarSaldos()
        {
            CalcularSaldoMovimientoVentas();
        }

        public static List<VentaMovimiento> CalcularSaldoMovimientoVentas()
        {
            List<VentaMovimiento> movimientos = new List<VentaMovimiento>();
            List<PuntoVenta> punto = ObtenerPuntosVenta();
            foreach (var pvx in punto)
            {
                movimientos = ObtenerVentaMovimiento(pvx.IdPuntoVenta);
                if (movimientos != null && movimientos.Count > 0)
                {
                    movimientos.ForEach(x => movimientos.Add(ActualizarSaldos(x)));
                }
            }
            return movimientos;
        }
        public static List<VentaMovimiento> ObtenerVentaMovimiento(int puntoventa)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa);
        }

        public static List<PuntoVenta> ObtenerPuntosVenta()
        {
            return new PuntoVentaDataAccess().BuscarTodos();
        }

        public static VentaMovimiento ActualizarSaldos(VentaMovimiento vm)
        {
            VentaMovimiento Updt = new VentaMovimiento();
            if (vm.Saldo != 0)
            {
                vm.Saldo = ObtenerSaldoActual(vm.IdPuntoVenta);
                if (vm.Ingreso > 0)
                {
                    Updt.Saldo = ObtenerSumaSaldoVenta(vm.Ingreso, vm.Saldo);
                }
                else if (vm.Egreso > 0)
                {
                    Updt.Saldo = ObtenerSaldoVentaEgreso(vm.Egreso, vm.Saldo);
                }
                new CajaGeneralDataAccess().Actualizar(Updt);
            }

            return Updt;
        }

        //public static List<VentaCorteAnticipoEC> CalcularSaldoVentaCorte()
        //{
        //    List<VentaCorteAnticipoEC> corteanticipos = new List<VentaCorteAnticipoEC>();
        //    List<PuntoVenta> punto = ObtenerPuntosVenta();
        //    foreach (var pvx in punto)
        //    {
        //        corteanticipos = ObtenerCorteAnticipos(pvx.IdPuntoVenta);
        //        if (corteanticipos != null && corteanticipos.Count > 0)
        //        {
        //            corteanticipos.ForEach(x => corteanticipos.Add(ActualizarSaldos(x)));
        //        }
        //    }
        //    return corteanticipos;
        //}

        //public static List<VentaCorteAnticipoEC> ObtenerCorteAnticipos(int puntoventa)
        //{
        //    return new CajaGeneralDataAccess().BuscarPorIdPv(puntoventa);
        //}

        //public static VentaCorteAnticipoEC ActualizarSaldos(VentaCorteAnticipoEC vm)
        //{
        //    VentaCorteAnticipoEC Updt = new VentaCorteAnticipoEC();
        //    if (vm.TotalVenta != 0)
        //    {
        //        //vm.Saldo = ObtenerSaldoActual(vm.IdPuntoVenta);
        //        //if (vm.Ingreso > 0)
        //        //{
        //        //    Updt.Saldo = ObtenerSumaSaldoVenta(vm.Ingreso, vm.Saldo);
        //        //}
        //        //else if (vm.Egreso > 0)
        //        //{
        //        //    Updt.Saldo = ObtenerSaldoVentaEgreso(vm.Egreso, vm.Saldo);
        //        //}
        //        //new CajaGeneralDataAccess().Actualizar(Updt);
        //    }

        //    return Updt;
        //}
    }
}
