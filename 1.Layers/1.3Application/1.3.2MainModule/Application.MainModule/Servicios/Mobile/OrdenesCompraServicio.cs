using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
   public static class OrdenesCompraServicio
    {
        public static List<RespuestaOrdenesCompraDTO> Consultar(short idEmpresa)
        {
            var ordenCompraDataAccess = new OrdenCompraDataAccess();
            return OrdenesCompraAdapter.ToDTO(ordenCompraDataAccess.Buscar(idEmpresa, OrdenCompraEstatusEnum.Proceso_compra));
        }
    }
}
