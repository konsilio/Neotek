using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
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
            List<EstadosRepDTO> ledos = AdaptadoresDTO.Seguridad.EstadoRepAdapter.ToDTO(new EstadoRDataAccess().ListaEstadosR());
            return ledos;
        }

        public static EstadosRepublica Obtener(int idEdo)
        {
            return new EstadoRDataAccess().BuscarIdEdo(idEdo);
        }
    }
}
