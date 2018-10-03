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
    }
}
