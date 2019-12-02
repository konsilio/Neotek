using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepOrdenCompraDTO
    {
        public string NumOrdenCompra { get; set; }
        public string Departamento { get; set; }
        public string NumRequisicion { get; set; }
        public string Requerimiento { get; set; }
        public string Kilos { get; set; }
        public decimal Importe { get; set; }
        public string Estatus { get; set; }
        public string Pagado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
