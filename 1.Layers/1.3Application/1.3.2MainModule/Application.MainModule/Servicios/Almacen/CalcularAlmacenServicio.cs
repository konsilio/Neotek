using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
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
    }
}
