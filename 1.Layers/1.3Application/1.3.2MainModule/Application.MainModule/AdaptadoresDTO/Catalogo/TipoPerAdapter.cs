using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
   public static class TipoPerAdapter
    {
        public static TipoPersonaDTO ToDTO(TipoPersona _pais)
        {
            TipoPersonaDTO paisDTO = new TipoPersonaDTO();
            paisDTO.IdTipoPersona = _pais.IdTipoPersona;
            paisDTO.Descripcion = _pais.Descripcion;

            return paisDTO;
        }
        public static List<TipoPersonaDTO> ToDTO(List<TipoPersona> lPais)
        {
            List<TipoPersonaDTO> lprodDTO = lPais.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
    }
}
