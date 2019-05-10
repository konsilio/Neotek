using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class HistoricoPrecioVentaDTO
    {
        public int ID { get; set; }
        public string Producto { get; set; }
        public double Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}