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
   public static class EstadosrepServicio
    {
        public static List<EstadosRepDTO> ListaEstadosR()
        {
            List<EstadosRepDTO> ledos = AdaptadoresDTO.Catalogo.EstadoRepAdapter.ToDTO(new EstadoRDataAccess().ListaEstadosR());
            return ledos;
        }
    }
}
