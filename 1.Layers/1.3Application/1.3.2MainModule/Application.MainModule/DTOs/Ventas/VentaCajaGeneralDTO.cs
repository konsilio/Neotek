using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class VentaCajaGeneralDTO
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public int IdPuntoVenta { get; set; }
        public short IdCAlmacenGas { get; set; }
        public Nullable<int> IdOperadorChofer { get; set; }
        public Nullable<int> IdUsuarioEntrega { get; set; }
        public int IdUsuarioRecibe { get; set; }
        public string FolioOperacionDia { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal VentaTotalCredito { get; set; }
        public decimal VentaTotalContado { get; set; }
        public decimal OtrasVentas { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal DescuentoCredito { get; set; }
        public decimal DescuentoContado { get; set; }
        public decimal DescuentoOtrasVentas { get; set; }
        public bool TodoCorrecto { get; set; }
        public string PuntoVenta { get; set; }
        public string OperadorChofer { get; set; }
        public string UsuarioEntrega { get; set; }
        public string UsuarioRecibe { get; set; }   
    }
}
