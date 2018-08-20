using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class CentroCostoAdapter
    {
        public static CentroCostoDTO ToDTO(CentroCosto cc)
        {
            CentroCostoDTO ccDTO = new CentroCostoDTO();
            ccDTO.IdCentroCosto = cc.IdCentroCosto;
            ccDTO.IdEmpresa = cc.IdEmpresa;
            ccDTO.IdTipoCentroCosto = cc.IdTipoCentroCosto;
            ccDTO.IdEquipoTransporte = cc.IdEquipoTransporte != null ? cc.IdEquipoTransporte.Value : 0;
            ccDTO.Numero = cc.Numero;
            ccDTO.Descripcion = cc.Descripcion;
            ccDTO.Activo = cc.Activo;
            ccDTO.FechaRegistro = cc.FechaRegistro;
            return ccDTO;
        }
        public static List<CentroCostoDTO> ToDTO(List<CentroCosto> ccs)
        {
            List<CentroCostoDTO> ccsDTO = ccs.Select(x => ToDTO(x)).ToList();
            return ccsDTO;
        }
    }
}
