using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepHistorioPrecioDTO
    {
        public int ID { get; set; }
        public string Producto { get; set; }
        public double Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
