using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class MetodoPagoDTO
    {
        public int Id_MetodoPago { get; set; }
        public string MetodoPagoSAT { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaIniVigencia { get; set; }
        public Nullable<System.DateTime> FechaFinVigencia { get; set; }
    }
}
