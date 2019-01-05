using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.MainModule.DTOs.Cobranza
{
    public class CargosDTO
    {
        public int IdCargo { get; set; }
        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string Cliente { get; set; }
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public System.DateTime FechaRango1 { get; set; }
        public System.DateTime FechaRango2 { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal TotalAbonos { get; set; }
        public bool VentaExtraordinaria { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public bool Saldada { get; set; }
        public AbonosDTO Abonos { get; set; }
        public decimal Total { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalCheques { get; set; }
        public decimal TotalTransferencia { get; set; }
        public List<AbonosDTO> lstCreditoR { get; set; }
        //cartera vencida
        public string Nombre { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoCorriente { get; set; }
        public decimal SaldoVencido { get; set; }
        public string Serie { get; set; }
        public decimal MontoCargo { get; set; }
        public decimal Dias1a7 { get; set; }
        public decimal Dias8a16 { get; set; }
        public decimal Dias17a31 { get; set; }
        public decimal Dias32a61 { get; set; }
        public decimal Dias62a91 { get; set; }
        public decimal Mas91 { get; set; }
    }
}
