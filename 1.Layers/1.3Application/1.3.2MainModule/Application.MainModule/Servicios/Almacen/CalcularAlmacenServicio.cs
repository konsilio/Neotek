using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacenes
{
    public static class CalcularAlmacenServicio
    {
        public static decimal ObtenerSumaEntradaAlmacen(decimal TotalAlmacenActual, decimal TotalEntrada)
        {
            return TotalAlmacenActual + TotalEntrada;
        }
        public static decimal ObtenerRestaSalidaAlmacen(decimal TotalAlmacenActual, decimal TotalEntrada)
        {
            return TotalAlmacenActual - TotalEntrada;
        }
        public static decimal ObtenerDiferneciaMovimiento(decimal cantidadMayor, decimal cantidadMenor)
        {
            if (cantidadMayor < cantidadMenor)
                return cantidadMenor - cantidadMayor;
            return cantidadMayor - cantidadMenor;
        }
        public static decimal ObtenerKgLectura(decimal capacidadTanqueLt, decimal porcentajeMedidor, decimal factorConvercionLtKg)
        {
            if (capacidadTanqueLt != 0)
                return ((capacidadTanqueLt / 100) * porcentajeMedidor) * factorConvercionLtKg;
            else
                return 0;
        }
        public static decimal ObtenerKgLecturaCilindro(decimal cantidad, decimal capacidadCilindro)
        {
            return cantidad * capacidadCilindro;
        }
        public static decimal ObtenerGasSobrante(decimal invetarioLibro, decimal inventarioFisico)
        {
            return invetarioLibro - inventarioFisico;
        }
        public static decimal ObtenerRema(decimal kilosVenta, decimal kilosSobrante)
        {
            return kilosVenta / kilosSobrante;
        }
        public static decimal ObtenerRemaPorcentaje(decimal gasSobrante, decimal gasVentas)
        {
            return gasSobrante / gasVentas;
        }
    }
}
