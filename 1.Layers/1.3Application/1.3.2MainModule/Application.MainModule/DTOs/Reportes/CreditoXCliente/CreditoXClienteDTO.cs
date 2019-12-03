using Application.MainModule.DTOs.Cobranza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class CreditoXClienteDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoCorriente { get; set; }
        public decimal Vencido { get; set; }
        public decimal Dias1a7 { get; set; }
        public decimal Dias8a16 { get; set; }
        public decimal Dias17a31 { get; set; }
        public decimal Dias32a61 { get; set; }
        public decimal Dias62a91 { get; set; }
        public decimal Mas91 { get; set; }
        public List<CargosDTO> CargosDetallados { get; set; }





    }
}
