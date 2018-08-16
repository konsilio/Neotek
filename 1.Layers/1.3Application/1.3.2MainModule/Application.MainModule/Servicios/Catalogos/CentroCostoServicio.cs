using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class CentroCostoServicio
    {
        public static List<CentroCostoDTO> ObtenerCentrosCostos()
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return CentroCostoAdapter.ToDTO(new CentroCostoDataAccess().BuscarCentroCosto());
            else
                return CentroCostoAdapter.ToDTO(new CentroCostoDataAccess().BuscarCentroCosto().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList());
        }
    }
}
