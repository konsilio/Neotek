using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class PaisServicio
    {
        public static List<PaisDTO> ListaPaises()
        {
            return PaisAdapter.ToDTO(new PaisDataAccess().ListaPaises());
        }
    }
}
