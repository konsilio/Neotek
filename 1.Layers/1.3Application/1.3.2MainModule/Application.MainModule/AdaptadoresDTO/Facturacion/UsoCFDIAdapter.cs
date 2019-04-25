using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    class UsoCFDIAdapter
    {
        public static UsoCFDIDTO ToDTO(UsoCFDI entidad)
        {
            return new UsoCFDIDTO()
            {
                Id_UsoCFDI = entidad.Id_UsoCFDI,
                UsoCFDISAT = entidad.UsoCFDISAT,
                Descripcion = entidad.Descripcion,
                PersonaFisica = entidad.PersonaFisica,
                FechaIniVigencia = entidad.FechaIniVigencia,
                FechaFinVigencia = entidad.FechaFinVigencia,
            };
        }
        public static List<UsoCFDIDTO> ToDTO(List<UsoCFDI> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
    }
}
