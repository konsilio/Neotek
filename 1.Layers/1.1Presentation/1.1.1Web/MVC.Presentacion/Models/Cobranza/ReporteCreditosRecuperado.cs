using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Cobranza
{
    public class ReporteCreditosRecuperado
    {
        public List<CargosModel> reporteCargos { get; set; }
        public List<AbonosModel> reporteAbonos { get; set; }
    }
}