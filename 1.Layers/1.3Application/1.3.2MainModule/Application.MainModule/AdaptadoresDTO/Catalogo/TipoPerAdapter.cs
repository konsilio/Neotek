using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
{
   public static class TipoPerAdapter
    {
        public static TipoPersonaDTO ToDTO(TipoPersona _tpersona)
        {
            TipoPersonaDTO personaDTO = new TipoPersonaDTO();
            personaDTO.IdTipoPersona = _tpersona.IdTipoPersona;
            personaDTO.Descripcion = _tpersona.Descripcion;

            return personaDTO;
        }
        public static List<TipoPersonaDTO> ToDTO(List<TipoPersona> lPais)
        {
            List<TipoPersonaDTO> lprodDTO = lPais.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
    }
}
