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
        public static decimal ObtenerPorcentajeRemanentePtoVenta(UnidadAlmacenGas almacen, List<VentaPuntoDeVentaDetalle> ventas, DateTime fecha)
        {
            // Obtenemos las lecturas del dia
            var lecturas = almacen.TomasLectura.Where(x => x.FechaRegistro.ToShortDateString().Equals(fecha.ToShortDateString())).ToList();
            // Identifica lectura inicial y final
            var lecturaInicial = new AlmacenGasTomaLectura();
            var lecturaFinal = new AlmacenGasTomaLectura();
            if (lecturas.Exists(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)))
                lecturaInicial = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
            if (lecturas.Exists(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)))
                lecturaFinal = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));
            //Calcula Inventarios
            decimal InvInicial = 0;
            decimal InvFinal = 0;
            decimal kilosVenta = 0;

            if (almacen.IdCamioneta == null)
            {
                kilosVenta = ventas.Sum(x => x.CantidadProducto ?? 0);
                InvInicial = ((lecturaInicial.Porcentaje ?? 0) * almacen.CapacidadTanqueLt ?? 0) / 100;
                InvFinal = ((lecturaFinal.Porcentaje ?? 0) * almacen.CapacidadTanqueLt ?? 0) / 100;
            }
            else
            {
                return (decimal)10.00;
                //foreach (var v in ventas)
                //{
                //    if (v.ProductoDescripcion.Contains("20"))
                //        kilosVenta += v.CantidadProducto ?? 0 * 20;
                //    if (v.ProductoDescripcion.Contains("30"))
                //        kilosVenta += v.CantidadProducto ?? 0 * 30;
                //    if (v.ProductoDescripcion.Contains("45"))
                //        kilosVenta += v.CantidadProducto ?? 0 * 45;
                //}
            }
            //Calcula difernecia entre inventarios desde lecturas
            var diferencia = CalculosGenerales.DiferenciaEntreDosNumero(InvInicial, InvFinal);
            //Determina remanente entre la cantidad vendida y la difernecia entre las lecturas
            var rema = CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(diferencia, kilosVenta), 2);
            //Determina porcentaje que representa el remamente
            return (rema * 100) / almacen.CapacidadTanqueLt ?? 0;



            //var libro = CalculosGenerales.DiferenciaEntreDosNumero(kilosVenta, diferencia);
            //var real = (((lecturaFinal.Porcentaje ?? 0) / 100) * almacen.CapacidadTanqueLt ?? 0);
            //if (libro != 0 && real != 0)
            //    return (decimal)((diferencia / real) * 100);
            //else
            //    return 0;
        }
        public static string ObteneremanentePtoVenta(UnidadAlmacenGas almacen, List<VentaPuntoDeVentaDetalle> ventas, DateTime fecha)
        {
            //var capacidad = AlmacenGasServicio.ObtenerUnidadAlamcenGas(idAlmacenGas).CapacidadTanqueKg;
            // Obtenemos las lecturas del dia
            //Comentario para publicar
            var lecturas = almacen.TomasLectura.Where(x => x.FechaRegistro.ToShortDateString().Equals(fecha.ToShortDateString())).ToList();
            // Identifica lectura inicial y final
            var lecturaInicial = new AlmacenGasTomaLectura();
            var lecturaFinal = new AlmacenGasTomaLectura();
            if (lecturas.Exists(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)))
                lecturaInicial = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
            if (lecturas.Exists(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)))
                lecturaFinal = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));
            
            //Calcula Inventario inicial
            decimal InvInicial = 0;
            decimal InvFinal = 0;
            decimal kilosVenta = 0;
            if (almacen.IdCamioneta == null)
            {
                //Determina cantidad
                kilosVenta = ventas.Sum(x => x.CantidadProducto ?? 0);
                //Determina inventarios en base a los porcentaje del medidor
                InvInicial = ((lecturaInicial.Porcentaje ?? 0) * almacen.CapacidadTanqueLt ?? 0) / 100;
                InvFinal = ((lecturaFinal.Porcentaje ?? 0) * almacen.CapacidadTanqueLt ?? 0) / 100;
                //Calcula difernecia entre inventarios desde lecturas
                var diferencia = CalculosGenerales.DiferenciaEntreDosNumero(InvInicial, InvFinal);
                //Determina cantidad remanente entre la cantidad vendida y la difernecia entre las lecturas
                return string.Concat(CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(diferencia, kilosVenta), 2).ToString(), " Lts.");
            }
            else
            {
                foreach (var v in ventas)
                {
                    if (v.ProductoDescripcion.Contains("20"))
                        kilosVenta += v.CantidadProducto ?? 0 * 20;
                    if (v.ProductoDescripcion.Contains("30"))
                        kilosVenta += v.CantidadProducto ?? 0 * 30;
                    if (v.ProductoDescripcion.Contains("45"))
                        kilosVenta += v.CantidadProducto ?? 0 * 45;
                }
                //Determina cantidad remanente entre la cantidad vendida y la difernecia entre las lecturas
                return string.Concat(CalculosGenerales.Truncar((kilosVenta * (decimal)0.1), 2).ToString(), " Kg.");
            }
        }
    }
}
