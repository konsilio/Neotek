using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
{
    public static class CalcularGasServicio
    {
        public static decimal ObtenerLitrosEnElTanque(decimal capacidadTanqueLt, decimal porcentaje)
        {
            return capacidadTanqueLt * (porcentaje / 100);
        }

        public static decimal ObtenerLitrosPorPorcentaje(decimal capacidadTanqueLt, decimal porcentaje)
        {
            return capacidadTanqueLt * (porcentaje / 100);
        }

        public static decimal ObtenerKilogramosDesdeLitros(decimal litros, decimal factor)
        {
            return litros * factor;
        }

        public static decimal ObtenerLitrosDesdeKilos(decimal kilogramos, decimal factor)
        {
            return kilogramos / factor;
        }

        public static decimal ObtenerDiferenciaKilogramos(decimal cantidadMayor, decimal cantidadMenor)
        {
            if (cantidadMayor < cantidadMenor)
                return cantidadMenor - cantidadMayor;

            return cantidadMayor - cantidadMenor;
        }

        public static decimal ObtenerDiferenciaLitros(decimal cantidadMayor, decimal cantidadMenor)
        {
            return ObtenerDiferenciaKilogramos(cantidadMayor, cantidadMenor);
        }

        public static decimal ObtenerDiferenciaPorcentaje(decimal cantidadMayor, decimal cantidadMenor)
        {
            return ObtenerDiferenciaKilogramos(cantidadMayor, cantidadMenor);
        }

        public static decimal ObtenerDiferenciaLectura(decimal cantidadMayor, decimal cantidadMenor)
        {
            return ObtenerDiferenciaKilogramos(cantidadMayor, cantidadMenor);
        }

        public static decimal SumarKilogramos(decimal cantidadActualKg, decimal ingresoKg)
        {
            return cantidadActualKg + ingresoKg;   
        }

        internal static decimal SumarLitros(decimal cantidadActualLt, decimal ingresoLt)
        {
            return cantidadActualLt + ingresoLt;
        }
    }
}
