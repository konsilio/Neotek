using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    public class OrdenCompraPagoDTO
    {
        public int IdOrdenCompra { get; set; }
        public string NumOrdenCompra { get; set; }
        public short Orden { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public byte IdFormaPago { get; set; }
        public string Empresa { get; set; }
        public short IdBanco { get; set; }
        public string Banco { get; set; }
        public string CuentaBancaria { get; set; }
        public decimal MontoPagado { get; set; }
        public string CadenaBase64 { get; set; }
        public string UrlPathCapturaPantalla { get; set; }
        public string PhysicalPathCapturaPantalla { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
