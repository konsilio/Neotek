using Application.MainModule.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
   public class YearsVentasTotales
    {
        public List<YearDTO> Years { get; set; }
        public int IdTipoReporte { get; set; }
    }
}
