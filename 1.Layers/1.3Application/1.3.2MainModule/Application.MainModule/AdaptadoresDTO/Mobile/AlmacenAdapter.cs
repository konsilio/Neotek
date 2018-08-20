using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenAdapter
    {
        public static AlmacenDto ToDto(AlmacenGas alm)
        {
            return new AlmacenDto()
            {
                IdAlmacenGas = alm.IdAlmacenGas,
                NombreAlmacen= "Núm. 1",
            };
        }

        public static List<AlmacenDto> ToDto(List<AlmacenGas> alm)
        {
            return alm.ToList().Select(x => ToDto(x)).ToList();
        }
    }
}
