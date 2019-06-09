using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Cobranza
{
    public class CRecuperadaDTO
    {
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public System.DateTime FechaAbono { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoAbono { get; set; }
        public byte IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public string FolioBancario { get; set; }
        public string Url_PDF { get; set; }
        public string Url_XML { get; set; }
    }
}
