using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class VentaReImpresion
    {
        public string Folio { get; set; }
        public string NombreCliente { get; set; }
        public decimal TotalVenta { get; set; }
        public string FechaHora { get; set; }
        public string TipoVenta { get; set; }
    }
}
