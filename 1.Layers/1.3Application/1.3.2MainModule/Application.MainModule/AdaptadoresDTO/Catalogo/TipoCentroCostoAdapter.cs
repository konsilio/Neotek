using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class TipoCentroCostoAdapter
    {
        public static TipoCentroCostoDto ToDTO(TipoCentroCosto tcc)
        {
            return new TipoCentroCostoDto()
            {
                IdTipoCentroCosto = tcc.IdTipoCentroCosto,
                Descripcion = tcc.Descripcion
            };
        }
        public static List<TipoCentroCostoDto> ToDTO(List<TipoCentroCosto> tccs)
        {
            return tccs.Select(x => ToDTO(x)).ToList();
        }
        public static TipoCentroCosto FromDTO(TipoCentroCostoDto tcc)
        {
            return new TipoCentroCosto()
            {
                IdTipoCentroCosto = tcc.IdTipoCentroCosto,
                Descripcion = tcc.Descripcion
            };
        }
        public static List<TipoCentroCosto> FromDTO(List<TipoCentroCostoDto> tccs)
        {
            return tccs.Select(x => FromDTO(x)).ToList();
        }
    }
}
