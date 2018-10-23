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
    public class VentasEstacionesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones, List<PuntoVenta> puntosventa)
        {
           
           return new DatosAnticiposCorteDto()
            {
                              
            };
        }

    }
}
