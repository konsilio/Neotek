using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class ProductoDTO
    {
        public int IdOrdenCompra { get; set; }
        public string Producto { get; set; }
        public string UnidadMedida { get; set; }
        public Decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Importe { get; set; }
        public Nullable<int> IdProducto { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdLinea { get; set; }
        public string Nombre { get; set; }
    }
}
