using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public static class TipoMedidorAdapter
    {
        public static MedidorDto ToDto(TipoMedidorUnidadAlmacenGas med)
        {
            try
            {
                return new MedidorDto()
                {
                    IdTipoMedidor = med.IdTipoMedidor,
                    NombreTipoMedidor = med.Medidor,
                    CantidadFotografias = med.NumeroFotografias
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<MedidorDto> ToDto(List<TipoMedidorUnidadAlmacenGas> med)
        {
            return med.ToList().Select(x => ToDto(x)).ToList();
        }
    }
}
