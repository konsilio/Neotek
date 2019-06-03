
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class YearsVentasTotalesDTO
    {
        public List<YearsDTO> Years { get; set; }
        public int IdTipoReporte { get; set; }
    }
}