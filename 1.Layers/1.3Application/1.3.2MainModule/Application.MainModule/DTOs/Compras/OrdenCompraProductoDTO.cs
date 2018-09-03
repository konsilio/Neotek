using Application.MainModule.DTOs.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class OrdenCompraProductoDTo : OrdenCompraProductoCrearDTO
    {
        public int IdOrdenCompra { get; set; }     
    }
}
