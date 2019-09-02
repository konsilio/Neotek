using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class JsonDTO
    {
        public List<Data> data { get; set; }
        public List<string> ykeys { get; set; }
        public List<string> barColors { get; set; }
        public List<string> labels { get; set; }
        public List<string> keys { get; set; }
        public string element { get; set; }
        public string xkey { get; set; }
        public string hideHover { get; set; }
        public string gridLineColor { get; set; }
        public bool resize { get; set; }    
    }

    public class Data {
        public string y { get; set; }
        public decimal a { get; set; }
        public decimal b { get; set; }
        public decimal c { get; set; }
        public decimal d { get; set; }
        public decimal e { get; set; }
        public decimal f { get; set; }
        public decimal g { get; set; }
        public decimal h { get; set; }
        public decimal i { get; set; }
        public decimal j { get; set; }
        public decimal k { get; set; }
        public decimal l { get; set; }
        public int m { get; set; }
    }

}