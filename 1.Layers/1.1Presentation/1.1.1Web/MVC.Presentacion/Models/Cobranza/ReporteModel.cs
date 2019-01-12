using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Cobranza
{
    public class ReporteModel
    {
        public List<CarteraVencidaModel> global { get; set; }
        public List<CargosModel> reportedet { get; set; }
    }
}