using System.Collections.Generic;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class TipoRegimenAdapter
    {
        public static List<TipoRegimenFiscalDto> ToDto(List<RegimenDTO> tregimenes)
        {
            var list = new List<TipoRegimenFiscalDto>();
            foreach (var tregimen in tregimenes)
            {
                list.Add(ToDto(tregimen));
            }
            return list;
        }

        private static TipoRegimenFiscalDto ToDto(RegimenDTO tregimen)
        {
            return new TipoRegimenFiscalDto()
            {
                IdTipoPersona = tregimen.IdTipoPersona,
                Descripcion = tregimen.Descripcion,
                AplicaPersonaFisica = tregimen.AplicaPersonaFisica,
                AplicaPersonaMoral = tregimen.AplicaPersonaMoral,
                c_RegimenFiscal = tregimen.c_RegimenFiscal,
                IdRegimenFiscal = tregimen.IdRegimenFiscal
            };
        }
    }
}
