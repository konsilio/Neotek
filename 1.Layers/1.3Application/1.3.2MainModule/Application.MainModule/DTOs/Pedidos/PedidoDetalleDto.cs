using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Pedidos
{
   public class PedidoDetalleDto
    {
        public int IdPedidoDetalle { get; set; }
        public Nullable<int> IdPedido { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<bool> Cilindro20 { get; set; }
        public Nullable<bool> Cilindro30 { get; set; }
        public Nullable<bool> Cilindro45 { get; set; }
        public Nullable<decimal> TotalKilos { get; set; }
        public Nullable<decimal> TotalLitros { get; set; }
    }
}
