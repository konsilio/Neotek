using Application.MainModule.DTOs.Almacen;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
{
    public static class RemanenteServicio
    {
        public static RemanenteDto ObtenerRemanente(AlmacenGasDescarga descarga, short idAlmacenGas, short idEmpresa)
        {
            var ulMovInventario = AlmacenGasServicio.ObtenerUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
            if(ulMovInventario == null) return ObtenerRemanente();

            var ulmMovDescarga = AlmacenGasServicio.ObtenerUltimosMovimientosDeDescargas(descarga , idEmpresa);

            if (ulmMovDescarga.ElementAt(2) == null) return ObtenerRemanente();

            if (ulmMovDescarga.ElementAt(1) == null) return ObtenerRemanente(ulmMovDescarga.ElementAt(2));

            if (ulmMovDescarga.ElementAt(0) == null) return ObtenerRemanente(ulmMovDescarga.ElementAt(2), ulmMovDescarga.ElementAt(1));

            return ObtenerRemanente(ulmMovDescarga.ElementAt(2), ulmMovDescarga.ElementAt(1), ulmMovDescarga.ElementAt(0));
        }

        private static RemanenteDto ObtenerRemanente(AlmacenGasMovimiento ulMovAnio)
        {
            return new RemanenteDto
            {
                RemanenteKg = ulMovAnio.RemanenteKg.Value,
                RemanenteLt = ulMovAnio.RemanenteLt.Value,
                RemanenteAcumuladoDiaKg = 0,
                RemanenteAcumuladoDiaLt = 0,
                RemanenteAcumuladoMesKg = 0,
                RemanenteAcumuladoMesLt = 0,
                RemanenteAcumuladoAnioKg = ulMovAnio.RemanenteAcumuladoAnioKg,
                RemanenteAcumuladoAnioLt = ulMovAnio.RemanenteAcumuladoAnioLt,
            };
        }

        private static RemanenteDto ObtenerRemanente(AlmacenGasMovimiento ulMovAnio, AlmacenGasMovimiento ulMovMes)
        {
            return new RemanenteDto
            {
                RemanenteKg = ulMovMes.RemanenteKg.Value,
                RemanenteLt = ulMovMes.RemanenteLt.Value,
                RemanenteAcumuladoDiaKg = 0,
                RemanenteAcumuladoDiaLt = 0,
                RemanenteAcumuladoMesKg = ulMovMes.RemanenteAcumuladoMesKg,
                RemanenteAcumuladoMesLt = ulMovMes.RemanenteAcumuladoMesLt,
                RemanenteAcumuladoAnioKg = ulMovMes.RemanenteAcumuladoAnioKg,
                RemanenteAcumuladoAnioLt = ulMovMes.RemanenteAcumuladoAnioLt,
            };
        }

        private static RemanenteDto ObtenerRemanente(AlmacenGasMovimiento ulMovAnio, AlmacenGasMovimiento ulMovMes, AlmacenGasMovimiento ulMovDia)
        {
            return new RemanenteDto
            {
                RemanenteKg = ulMovDia.RemanenteKg.Value,
                RemanenteLt = ulMovDia.RemanenteLt.Value,
                RemanenteAcumuladoDiaKg = ulMovDia.RemanenteAcumuladoDiaKg,
                RemanenteAcumuladoDiaLt = ulMovDia.RemanenteAcumuladoDiaLt,
                RemanenteAcumuladoMesKg = ulMovDia.RemanenteAcumuladoMesKg,
                RemanenteAcumuladoMesLt = ulMovDia.RemanenteAcumuladoMesLt,
                RemanenteAcumuladoAnioKg = ulMovDia.RemanenteAcumuladoAnioKg,
                RemanenteAcumuladoAnioLt = ulMovDia.RemanenteAcumuladoAnioLt,
            };
        }

        private static RemanenteDto ObtenerRemanente()
        {
            return new RemanenteDto
            {
                RemanenteKg = 0,
                RemanenteLt = 0,
                RemanenteAcumuladoDiaKg = 0,
                RemanenteAcumuladoDiaLt = 0,
                RemanenteAcumuladoMesKg = 0,
                RemanenteAcumuladoMesLt = 0,
                RemanenteAcumuladoAnioKg = 0,
                RemanenteAcumuladoAnioLt = 0,
            };
        }
    }
}
