using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class HistoricoPrecioVentaModel
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool LitroGas { get; set; }
        public bool Clilindro10K { get; set; }
        public bool Clilindro20K { get; set; }
        public bool Clilindro30K { get; set; }
        public bool Clilindro45K { get; set; }
    }
}