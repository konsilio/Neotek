using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    public class OrdenCompraRespuestaDTO
    {
        public int IdOrdenCompra { get; set; }
        public string NumOrdenCompra { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
