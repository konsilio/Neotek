using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.ObjetosValor.Enum;
using Utilities.MainModule;

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
            if (!litros.Equals(0) && !capacidadTanqueLt.Equals(0))
                return (litros / capacidadTanqueLt) * 100;
            else
                return 0;
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
        public static decimal ObtenerLitrosFinalesAlmacenPrinAlt(decimal litrosInicialesEnAlmacen, decimal litrosDescargados, decimal litrosRecargados, decimal litrosCarburados)
        {
            //Fórmula: Litros Iniciales + entradas de gas - salidas de gas
            //Litros Iniciales -> litrosInicialesEnAlmacen
            //Entradas de gas -> litrosDescargados
            //Salidas de gas -> litrosRecargados y litrosCarburados
            return litrosInicialesEnAlmacen + litrosDescargados - litrosRecargados - litrosCarburados;
        }
        public static decimal ObtenerPorcentajeRemanentePtoVenta(UnidadAlmacenGas almacen, decimal ventas, DateTime fecha)
        {
            //var capacidad = AlmacenGasServicio.ObtenerUnidadAlamcenGas(idAlmacenGas).CapacidadTanqueKg;
            decimal diferencia = 0;
            decimal porcentajefinal = 0;
            var lecturas = almacen.TomasLectura.Where(x => x.FechaRegistro.Month.Equals(fecha.Month) && x.FechaRegistro.Year.Equals(fecha.Year) && x.FechaRegistro.Day.Equals(fecha.Day)).ToList();
            //var lecturas = AlmacenGasServicio.ObtenerLecturas(almacen.IdAlmacenGas ?? 0, fecha);
            foreach (var item in lecturas)
            {
                var lecturaInicial = 0;
                if (item.IdTipoEvento.Equals(TipoEventoEnum.Inicial) && item.FechaRegistro.ToShortDateString().Equals(fecha.ToShortDateString()))
                    lecturaInicial = Convert.ToInt32(item.P5000 ?? 0);

                if (lecturas.Exists(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final) && x.FechaRegistro.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())))
                {
                    porcentajefinal = item.Porcentaje ?? 0;
                    diferencia = CalculosGenerales.DiferenciaEntreDosNumero(lecturaInicial, item.P5000 ?? 0);
                }
            }
            var libro = diferencia - ventas;
            var real = porcentajefinal * almacen.AlmacenGas.CapacidadTotalKg;
            if (libro != 0 && real != 0)
                return (decimal)((diferencia / real) * 100);
            else
                return 0;
        }
        //public static decimal ObtenerPorcentajeRemanentePtoVenta(short idAlmacenGas, decimal diferencia, decimal porcentajefinal, DateTime periodo)
        //{
        //    var ventas = CajaGeneralServicio.ObtenerVentasPorCAlmacenGas(idAlmacenGas, periodo).Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.CantidadKg));
        //    var capacidad = AlmacenGasServicio.ObtenerUnidadAlamcenGas(idAlmacenGas).CapacidadTanqueKg;
        //    var recargas = AlmacenGasServicio.ObtenerRecargasNoProcesadas();
        //    var libro = diferencia - ventas;
        //    var real = porcentajefinal * capacidad;

        //    return (decimal)((real / libro) * 100);
        //}
    }
}
