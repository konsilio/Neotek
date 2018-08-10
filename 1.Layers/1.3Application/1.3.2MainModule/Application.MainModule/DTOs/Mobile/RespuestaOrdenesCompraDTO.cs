using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class RespuestaOrdenesCompraDTO : RespuestaDto
    {
        public List<OrdenCompraDTO> OrdenesCompra { get; set; }
    }
}
