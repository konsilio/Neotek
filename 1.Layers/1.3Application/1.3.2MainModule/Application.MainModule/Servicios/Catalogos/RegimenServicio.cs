using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class RegimenServicio
    {
        public static List<RegimenDTO> ListaRegimen()
        {
            List<RegimenDTO> lregimen = AdaptadoresDTO.Seguridad.RegimenAdapter.ToDTO(new RegimenDataAccess().ListaRegimen());
            return lregimen;
        }
    }
}
