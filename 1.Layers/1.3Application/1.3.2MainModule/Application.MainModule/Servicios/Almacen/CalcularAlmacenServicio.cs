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
    }
}
