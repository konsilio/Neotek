using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Historico
{
    public class HistoricoVentaModel
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal MontoVenta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsLocal { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
    }
}