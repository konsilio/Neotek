using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class EstadoRepAdapter
    {
        public static EstadosRepDTO ToDTO(EstadosRepublica _edo)
        {
            EstadosRepDTO estadoDTO = new EstadosRepDTO();
            estadoDTO.IdEstadoRep = _edo.IdEstadoRep;
            estadoDTO.Estado = _edo.Estado;

            return estadoDTO;
        }
        public static List<EstadosRepDTO> ToDTO(List<EstadosRepublica> lEstado)
        {
            List<EstadosRepDTO> ledoDTO = lEstado.ToList().Select(x => ToDTO(x)).ToList();
            return ledoDTO;
        }
    }
}
