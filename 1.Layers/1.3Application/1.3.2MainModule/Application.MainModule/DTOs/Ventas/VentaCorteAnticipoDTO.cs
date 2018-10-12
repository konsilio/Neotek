using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Ventas
{
   public class VentaCorteAnticipoDTO
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public byte IdTipoOperacion { get; set; }
        public int IdPuntoVenta { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int IdOperadorChofer { get; set; }
        public int IdUsuarioRecibe { get; set; }
        public string FolioOperacionDia { get; set; }
        public string FolioOperacion { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalAnticipado { get; set; }
        public decimal MontoRecortadoAnticipado { get; set; }
        public string TipoOperacion { get; set; }
        public string PuntoVenta { get; set; }
        public string OperadorChofer { get; set; }
        public string UsuarioRecibe { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaCorteAnticipo { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
