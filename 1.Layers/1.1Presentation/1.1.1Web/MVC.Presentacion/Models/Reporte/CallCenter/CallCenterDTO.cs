using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CallCenterDTO
    {
        public int IdPedido { get; set; }
        public string RFC { get; set; }
        public string Estatus { get; set; }
        public string AtendidoPor { get; set; }
        public string Pedido { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}