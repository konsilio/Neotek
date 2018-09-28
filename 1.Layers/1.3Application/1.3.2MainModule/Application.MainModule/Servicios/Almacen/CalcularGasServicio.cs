﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
{
    public static class CalcularGasServicio
    {
        public static decimal ObtenerLitrosEnElTanque(decimal capacidadTanque, decimal porcentaje)
        {
            return capacidadTanque * (porcentaje / 100);
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
