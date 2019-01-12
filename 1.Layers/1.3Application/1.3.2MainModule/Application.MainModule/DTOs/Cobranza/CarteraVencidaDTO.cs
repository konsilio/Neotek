using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Cobranza
{
    public class CarteraVencidaDTO
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Ticket { get; set; }
        public string Serie { get; set; }
        public decimal MontoCargo { get; set; }
        public decimal SaldoActual { get; set; }
        public System.DateTime FechaReg { get; set; }
        public System.DateTime FechaVen { get; set; }
        public decimal SaldoActualTotal { get; set; }
        public decimal SaldoCorrienteTotal { get; set; }
        public decimal SaldoVencidoTotal { get; set; }
        public decimal Dias1_7Total { get; set; }
        public decimal Dias8_16Total { get; set; }
        public decimal Dias17_31Total { get; set; }
        public decimal Dias32_61Total { get; set; }
        public decimal Dias62_91Total { get; set; }
        public decimal Mas91Total { get; set; }
        public decimal Dias1_7 { get; set; }
        public decimal Dias8_16 { get; set; }
        public decimal Dias17_31 { get; set; }
        public decimal Dias32_61 { get; set; }
        public decimal Dias62_91 { get; set; }
        public decimal Mas91 { get; set; }
        public decimal SaldoCorriente { get; set; }
        public decimal SaldoVencido { get; set; }
    }
}
