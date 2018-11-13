using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Ventas
{
     public class ReporteDelDiaDTO
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public Nullable<int> IdPuntoVenta { get; set; }
        public Nullable<short> IdCAlmacenGas { get; set; }
        public Nullable<int> IdOperadorChofer { get; set; }
        public Nullable<int> IdUsuarioJEC { get; set; }
        public string FolioOperacionDia { get; set; }
        public Nullable<decimal> LitrosVenta { get; set; }
        public Nullable<decimal> KilosVenta { get; set; }
        public Nullable<decimal> PrecioLt { get; set; }
        public Nullable<decimal> PrecioKg { get; set; }
        public Nullable<decimal> ImporteContado { get; set; }
        public Nullable<decimal> ImporteCredito { get; set; }
        public Nullable<decimal> ImporteAnticipos { get; set; }
        public Nullable<decimal> ImporteCortes { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string PuntoVenta { get; set; }
        public string OperadorChofer { get; set; }
        public string UsuarioJEC { get; set; }
        public System.DateTime FechaReporte { get; set; }
        public System.DateTime FechaRegistro { get; set; }

    }
}
