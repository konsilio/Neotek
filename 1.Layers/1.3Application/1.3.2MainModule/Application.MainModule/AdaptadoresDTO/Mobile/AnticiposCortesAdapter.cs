using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AnticiposCortesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones)
        {
            return new DatosAnticiposCorteDto()
            {
                estaciones = estaciones.Select(x=>ToDTO(x)).ToList()
            };
        }

        private static EstacionesDto ToDTO(EstacionCarburacion estacion)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre
            };
        }
    }
}
