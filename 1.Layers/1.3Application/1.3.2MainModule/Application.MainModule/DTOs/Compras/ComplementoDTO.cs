using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
   public class ComplementoDTO : OrdenCompraDTO
    {
        public decimal MontoAPagar { get; set; }
        public List<ProductoOCDTO> ProductoOCDTO { get; set; }
        public List<OrdenCompraPagoDTO> Pagos { get; set; }
    }
}
