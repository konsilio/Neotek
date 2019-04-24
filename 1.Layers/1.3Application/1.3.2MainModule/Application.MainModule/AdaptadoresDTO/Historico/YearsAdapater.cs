using System;
using System.Collections.Generic;
using Application.MainModule.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Historico
{
     class YearsAdapater
    {

        public static YearDTO ToDTO(int entidad)
        {
            return new YearDTO()
            {
                Year = entidad
            };            
        }
        public static List<YearDTO> ToDTO(List<int> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }

    }
}
