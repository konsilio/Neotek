using Application.MainModule.DTOs;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.EqTransporte
{
    public class EquipoTransporteServicio
    {
        public static List<EquipoTransporteDTO> Obtener(short idempresa)
        {
            List<EquipoTransporteDTO> lPedidos = AdaptadoresDTO.EqTransporte.EquipoTransporteAdapter.ToDTO(new EqTransporteDataAccess().BuscarTodos(idempresa));
            return lPedidos;
        }
    }
}
