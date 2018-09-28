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
        public static decimal ObtenerPrecioSalidaLt(decimal precioPemex, decimal factorLitrosaKilos)
        {
            return precioPemex + factorLitrosaKilos;
        }
    }
}
