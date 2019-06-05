using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepRequisicionDTO
    {
        public string NumRequisicon { get; set; }
        public string Departamento { get; set; }
        public string Estatus { get; set; }
        public string Requisicion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
