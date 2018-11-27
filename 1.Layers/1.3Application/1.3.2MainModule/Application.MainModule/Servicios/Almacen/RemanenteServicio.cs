using Application.MainModule.DTOs.Almacen;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacenes
{
    public static class RemaServicio
    {
        public static RemaDto ObtenerRema(AlmacenGasDescarga descarga, short idAlmacenGas, short idEmpresa)
        {
            var ulmMovDescarga = AlmacenGasServicio.ObtenerUltimosMovimientosDeDescargas(descarga , idEmpresa);
            if (ulmMovDescarga.ElementAt(2) == null) return ObtenerRema();
            if (ulmMovDescarga.ElementAt(1) == null) return ObtenerRema(ulmMovDescarga.ElementAt(2));
            if (ulmMovDescarga.ElementAt(0) == null) return ObtenerRema(ulmMovDescarga.ElementAt(2), ulmMovDescarga.ElementAt(1));
            return ObtenerRema(ulmMovDescarga.ElementAt(2), ulmMovDescarga.ElementAt(1), ulmMovDescarga.ElementAt(0));
        }    

        private static RemaDto ObtenerRema(AlmacenGasMovimiento ulMovAnio)
        {
            return new RemaDto
            {
                RemaKg = ulMovAnio.RemaKg.Value,
                RemaLt = ulMovAnio.RemaLt.Value,
                RemaAcumDiaKg = 0,
                RemaAcumDiaLt = 0,
                RemaAcumMesKg = 0,
                RemaAcumMesLt = 0,
                RemaAcumAnioKg = ulMovAnio.RemaAcumAnioKg,
                RemaAcumAnioLt = ulMovAnio.RemaAcumAnioLt,
            };
        }

        private static RemaDto ObtenerRema(AlmacenGasMovimiento ulMovAnio, AlmacenGasMovimiento ulMovMes)
        {
            return new RemaDto
            {
                RemaKg = ulMovMes.RemaKg.Value,
                RemaLt = ulMovMes.RemaLt.Value,
                RemaAcumDiaKg = 0,
                RemaAcumDiaLt = 0,
                RemaAcumMesKg = ulMovMes.RemaAcumMesKg,
                RemaAcumMesLt = ulMovMes.RemaAcumMesLt,
                RemaAcumAnioKg = ulMovMes.RemaAcumAnioKg,
                RemaAcumAnioLt = ulMovMes.RemaAcumAnioLt,
            };
        }

        private static RemaDto ObtenerRema(AlmacenGasMovimiento ulMovAnio, AlmacenGasMovimiento ulMovMes, AlmacenGasMovimiento ulMovDia)
        {
            return new RemaDto
            {
                RemaKg = ulMovDia.RemaKg.Value,
                RemaLt = ulMovDia.RemaLt.Value,
                RemaAcumDiaKg = ulMovDia.RemaAcumDiaKg,
                RemaAcumDiaLt = ulMovDia.RemaAcumDiaLt,
                RemaAcumMesKg = ulMovDia.RemaAcumMesKg,
                RemaAcumMesLt = ulMovDia.RemaAcumMesLt,
                RemaAcumAnioKg = ulMovDia.RemaAcumAnioKg,
                RemaAcumAnioLt = ulMovDia.RemaAcumAnioLt,
            };
        }

        private static RemaDto ObtenerRema()
        {
            return new RemaDto
            {
                RemaKg = 0,
                RemaLt = 0,
                RemaAcumDiaKg = 0,
                RemaAcumDiaLt = 0,
                RemaAcumMesKg = 0,
                RemaAcumMesLt = 0,
                RemaAcumAnioKg = 0,
                RemaAcumAnioLt = 0,
            };
        }
    }
}
