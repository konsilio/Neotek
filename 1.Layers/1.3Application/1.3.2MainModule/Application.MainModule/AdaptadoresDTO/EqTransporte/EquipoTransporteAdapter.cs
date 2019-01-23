using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.EqTransporte
{
    class EquipoTransporteAdapter
    {
        public static EquipoTransporteDTO ToDTO(EquipoTransporte _dto)
        {
            EquipoTransporteDTO dto = new EquipoTransporteDTO();
         
            return dto;
        }
        public static List<EquipoTransporteDTO> ToDTO(List<EquipoTransporte> lCargo)
        {
            List<EquipoTransporteDTO> lprodDTO = lCargo.ToList().Select(x => ToDTO(x)).ToList();

            return lprodDTO;
        }
    }
}
