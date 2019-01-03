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
    }
}
