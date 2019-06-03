using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class HistoricoVentaModel
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public double MontoVenta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsLocal { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
        public string Url { get; set; }
    }
}