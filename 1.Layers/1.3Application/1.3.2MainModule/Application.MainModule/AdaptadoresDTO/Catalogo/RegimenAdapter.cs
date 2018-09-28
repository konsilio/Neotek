using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
{
    public static class RegimenAdapter
    {
        public static RegimenDTO ToDTO(RegimenFiscal _reg)
        {
            RegimenDTO regDTO = new RegimenDTO();
            regDTO.IdRegimenFiscal = _reg.IdRegimenFiscal;
            regDTO.Descripcion = _reg.Descripcion;
            regDTO.c_RegimenFiscal = _reg.c_RegimenFiscal;
            regDTO.AplicaPersonaFisica = _reg.AplicaPersonaFisica;
            regDTO.AplicaPersonaMoral = _reg.AplicaPersonaMoral;
            regDTO.IdTipoPersona = _reg.IdTipoPersona;
            return regDTO;
        }
        public static List<RegimenDTO> ToDTO(List<RegimenFiscal> lreg)
        {
            List<RegimenDTO> lprodDTO = lreg.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
    }
}
