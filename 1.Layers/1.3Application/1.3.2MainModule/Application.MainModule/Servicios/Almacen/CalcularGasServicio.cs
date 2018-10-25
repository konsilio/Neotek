using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.Almacenes
{
    public static class CalcularGasServicio
    {
        public static decimal ObtenerLitrosEnElTanque(decimal capacidadTanqueLt, decimal porcentaje)
        {
            return capacidadTanqueLt * (porcentaje / 100);
        }

        public static decimal ObtenerLitrosDesdePorcentaje(decimal capacidadTanqueLt, decimal porcentaje)
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

        public static decimal ObtenerDiferenciaLecturaP5000(decimal cantidadMayor, decimal cantidadMenor)
        {
            return ObtenerDiferenciaKilogramos(cantidadMayor, cantidadMenor);
        }

        public static decimal ObtenerPorcentajeDesdeLitros(decimal capacidadTanqueLt, decimal litros)
        {
            return (litros / capacidadTanqueLt) * 100;
        }

        public static decimal SumarKilogramos(decimal cantidadActualKg, decimal ingresoKg)
        {
            return cantidadActualKg + ingresoKg;   
        }

        public static decimal SumarLitros(decimal cantidadActualLt, decimal ingresoLt)
        {
            return cantidadActualLt + ingresoLt;
        }
        public static decimal SumarLitrosDescuentaPorcentajeRema(decimal cantidadActualLt, decimal ingresoLt, decimal porcentajeCalibracionPlaneada)
        {
            return cantidadActualLt + (ingresoLt - ingresoLt * (porcentajeCalibracionPlaneada / 100));
        }

        public static decimal RestarKilogramos(decimal cantidadActualKg, decimal salioKg)
        {
            return cantidadActualKg - salioKg;
        }

        public static decimal RestarLitros(decimal cantidadActualLt, decimal salioLt)
        {
            return cantidadActualLt - salioLt;
        }

        public static decimal RestarLitrosDesdePorcentaje(decimal litrosRecargadosP5000, decimal porcentajeCalibracionPlaneada)
        {
            return litrosRecargadosP5000 - (litrosRecargadosP5000 * (porcentajeCalibracionPlaneada / 100));
        }

        public static decimal ObtenerKilogramosEnCamioneta(Dictionary<decimal, decimal> cilindros)
        {
            //Key: Capacidad cilindro Value: Cantidad de cilindros con esa capacidad
            return cilindros.Select(x => x.Key * x.Value).Sum();
        }

        public static decimal SumarKilogramos(decimal cantidadActualKg, decimal capacidadKg, decimal cantidad)
        {
            return cantidadActualKg + (capacidadKg * cantidad);
        }

        public static decimal ObtenerLitrosFinalesAlmacenPrinAlt(decimal litrosInicialesEnAlmacen, decimal litrosDescargados, decimal litrosRecargados)
        {
            //Fórmula: Litros Iniciales del día + entradas de gas en el día - salidas de gas en el día
            //Litros Iniciales -> litrosInicialesEnAlmacen
            //Entradas de gas -> litrosDescargados
            //Salidas de gas -> litrosRecargados
            return litrosInicialesEnAlmacen + litrosDescargados - litrosRecargados;
        }
    }
}
