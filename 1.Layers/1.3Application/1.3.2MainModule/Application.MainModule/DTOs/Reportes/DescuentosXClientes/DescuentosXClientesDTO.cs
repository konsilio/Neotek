using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class DescuentosXClientesDTO
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public decimal DescuentoLt { get; set; }
        public decimal DescuentoTotal { get; set; } 
        public decimal Diferencia { get; set; }
        public decimal Total { get; set; }




    }
}
