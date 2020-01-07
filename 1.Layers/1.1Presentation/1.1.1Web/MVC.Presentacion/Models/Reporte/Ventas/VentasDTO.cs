using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class VentasDTO
    {
       
      
        public string FolioVenta { get; set; }
        public string RFC { get; set; }
        public string OperadorChofer { get; set; }
        public string PuntoVenta { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> EfectivoRecibido { get; set; }
        public Nullable<decimal> CambioRegresado { get; set; }
        public Nullable<decimal> Bonificacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}