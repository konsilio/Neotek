using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class YearsDTO
    {
        public int Year { get; set; }
        public bool Seleccionar { get; set; }
        public List<MesVentaDto> MesesVenta { get; set; }
    }
}