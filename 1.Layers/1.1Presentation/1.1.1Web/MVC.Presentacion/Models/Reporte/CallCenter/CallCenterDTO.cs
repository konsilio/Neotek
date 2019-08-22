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
        public string Observaciones { get; set; }
        public string AtendidoPor { get; set; }
        public string Pedido { get; set; }
        public string Litros { get; set; }
        public string kg20 { get; set; }
        public string kg30 { get; set; }
        public string kg45 { get; set; }
        public int AtendidoEn { get; set; }
        public DateTime Fecha { get; set; }
    }
}