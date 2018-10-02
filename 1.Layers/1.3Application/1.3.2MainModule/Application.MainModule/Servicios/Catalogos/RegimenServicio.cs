using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System.Collections.Generic;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class RegimenServicio
    {
        public static List<RegimenDTO> ListaRegimen()
        {
            List<RegimenDTO> lregimen = RegimenAdapter.ToDTO(new RegimenDataAccess().ListaRegimen());
            return lregimen;
        }
    }
}
