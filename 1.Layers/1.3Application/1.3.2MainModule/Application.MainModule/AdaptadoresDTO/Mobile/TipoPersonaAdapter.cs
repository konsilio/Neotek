using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    class TipoPersonaAdapter
    {
        public static DatosTipoPersonaDto ToDto(List<TipoPersonaDTO> tpersonas, List<RegimenDTO> tregimenes)
        {
            return new DatosTipoPersonaDto()
            {
                tipoPersona = OrdenarDto(tpersonas,tregimenes),
                //regimenFiscal = TipoRegimenAdapter.ToDto(tregimenes)
            };
        }

        private static List<TipoPersonaDto> OrdenarDto(List<TipoPersonaDTO> tpersonas,List<RegimenDTO> tregimenes)
        {
            var list = new List<TipoPersonaDto>();
            foreach (var tpersona in tpersonas)
            {
                list.Add(ToDto(tpersona,tregimenes.FindAll(x=>x.IdTipoPersona.Equals(tpersona.IdTipoPersona)).ToList()));
            }
            return list;
        }

        private static TipoPersonaDto ToDto(TipoPersonaDTO tpersona,List<RegimenDTO> regimenes)
        {
            return new TipoPersonaDto()
            {
                IdTipoPersona = tpersona.IdTipoPersona,
                Descripcion = tpersona.Descripcion,
                Regimenes = ToDtoRegiment(regimenes)
            };
        }

        private static List<TipoRegimenFiscalDto> ToDtoRegiment(List<RegimenDTO> regimenes)
        {
            List<TipoRegimenFiscalDto> list = new List<TipoRegimenFiscalDto>();
            foreach (var regimen in regimenes)
            {
                list.Add(new TipoRegimenFiscalDto()
                {
                    c_RegimenFiscal = regimen.c_RegimenFiscal,
                    AplicaPersonaFisica = regimen.AplicaPersonaFisica,
                    AplicaPersonaMoral = regimen.AplicaPersonaMoral,
                    Descripcion = regimen.Descripcion,
                    IdRegimenFiscal = regimen.IdRegimenFiscal,
                    IdTipoPersona = regimen.IdTipoPersona
                });
            }
            return list;
        }
    }
}
