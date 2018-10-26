using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class VentaDTO
    {
        public string FolioVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public bool Factura { get; set; }
        public bool Credito { get; set; }
        public decimal Efectivo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Cambio { get; set; }
        public bool SinNumero { get; set; }
        public List<ConceptoDTO> Concepto { get; set; }
    }
}
