using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
{
    public static class RegimenAdapter
    {
        public static RegimenDTO ToDTO(RegimenFiscal _reg)
        {
            RegimenDTO regDTO = new RegimenDTO();
            regDTO.IdRegimenFiscal = _reg.IdRegimenFiscal;
            regDTO.Descripcion = _reg.Descripcion;

            return regDTO;
        }
        public static List<RegimenDTO> ToDTO(List<RegimenFiscal> lreg)
        {
            List<RegimenDTO> lprodDTO = lreg.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
    }
}
