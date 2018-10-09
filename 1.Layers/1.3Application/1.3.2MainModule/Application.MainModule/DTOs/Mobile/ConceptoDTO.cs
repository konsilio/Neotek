using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class ConceptoDTO
    {
        public short IdTipoGas { get; set; }
        public int Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal PUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public short IdCategoria { get; set; }
        public short IdLinea { get; set; }
        public short IdProducto { get; set; }
    }
}
