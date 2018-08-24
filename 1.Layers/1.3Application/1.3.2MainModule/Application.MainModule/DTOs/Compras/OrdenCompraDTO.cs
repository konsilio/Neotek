using Application.MainModule.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class OrdenCompraDTO
    {
        public int IdOrdenCompra { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdOrdenCompraEstatus { get; set; }
        public int IdRequisicion { get; set; }
        public int IdProveedor { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCuentaContable { get; set; }
        public string NumOrdenCompra { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal SubtotalSinIva { get; set; }
        public decimal SubtotalSinIeps { get; set; }
        public decimal Iva { get; set; }
        public decimal Ieps { get; set; }
        public decimal Total { get; set; }
        public bool EsTransporteGas { get; set; }      
    }
}
