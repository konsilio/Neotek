using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class DescuentosXClientes
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public decimal DescuentoLt { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal Diferencia { get; set; }
        public decimal Total { get; set; }
    }
}
