using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Compras
{
    public class OrdenCompraServicio
    {
        public static RequisicionOCDTO BuscarRequisicion (int _idrequi)
        {
            return OrdenComprasAdapter.ToDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
    }
}
