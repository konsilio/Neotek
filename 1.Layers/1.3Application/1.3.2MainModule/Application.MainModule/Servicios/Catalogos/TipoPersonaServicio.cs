using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class TipoPersonaServicio
    {
        public static List<TipoPersonaDTO> ListaTipoPersona()
        {
            List<TipoPersonaDTO> lpaises = AdaptadoresDTO.Seguridad.TipoPerAdapter.ToDTO(new TipoPerDataAccess().ListaTiposPer());
            return lpaises;
        }

        public static TipoPersonaDTO TipoPersona(short IdTipoP)
        {
            TipoPersonaDTO lp = AdaptadoresDTO.Seguridad.TipoPerAdapter.ToDTO(new TipoPerDataAccess().TiposPer(IdTipoP));
            return lp;
        }

    }
}
