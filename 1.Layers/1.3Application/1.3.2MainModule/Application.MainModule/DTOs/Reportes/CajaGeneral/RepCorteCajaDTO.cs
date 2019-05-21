using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepCorteCajaDTO
    {
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string Unidad { get; set; }
        public double TotalVenta { get; set; }
    }
}
