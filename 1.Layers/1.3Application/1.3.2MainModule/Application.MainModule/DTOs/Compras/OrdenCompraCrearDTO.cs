using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    public class OrdenCompraCrearDTO : OrdenCompraDTO
    {
        public List<OrdenCompraProductoCrearDTO> Productos { get; set; }
    }
}
