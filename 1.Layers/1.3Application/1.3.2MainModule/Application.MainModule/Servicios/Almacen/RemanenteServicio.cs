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
        public static RemanenteDto ObtenerRemanenteDelDia(AlmacenGasMovimiento ultimoMovimiento, short idAlmacenGas, DateTime fechaAplicacion)
        {
            if (ultimoMovimiento == null) return ObtenerRemanente(ultimoMovimiento);


        }

        public static RemanenteDto ObtenerRemanenteDelAnio(AlmacenGasMovimiento ultimoMovimiento, short idAlmacenGas, DateTime fechaAplicacion)
        {
            if (ultimoMovimiento == null) return ObtenerRemanente(ultimoMovimiento);


        }

        private static RemanenteDto ObtenerRemanente(AlmacenGasMovimiento ultimoMovimiento)
        {
            return new RemanenteDto
            {
                //RemanenteKg = ultimoMovimiento.,
                //RemanenteLt = ,
                RemanenteAcumuladoDiaKg = ultimoMovimiento.RemanenteAcumuladoDiaKg,
                RemanenteAcumuladoDiaLt = ultimoMovimiento.RemanenteAcumuladoDiaLt,
                RemanenteAcumuladoMesKg = ultimoMovimiento.RemanenteAcumuladoMesKg,
                RemanenteAcumuladoMesLt = ultimoMovimiento.RemanenteAcumuladoMesLt,
                RemanenteAcumuladoAnioKg = ultimoMovimiento.RemanenteAcumuladoAnioKg,
                RemanenteAcumuladoAnioLt = ultimoMovimiento.RemanenteAcumuladoAnioLt,
            };
        }
    }
}
