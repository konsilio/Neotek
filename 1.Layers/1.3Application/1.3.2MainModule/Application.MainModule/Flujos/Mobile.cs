using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Mobile
    {

        public List<RespuestaOrdenesCompraDTO> ConsultarOrdenesCompra(short IdEmpresa)
        {
           return OrdenesCompraServicio.Consultar(IdEmpresa);

        }
    }
}
