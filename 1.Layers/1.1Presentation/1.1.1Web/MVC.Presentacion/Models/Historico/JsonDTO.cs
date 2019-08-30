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
        public int m { get; set; }
    }

}