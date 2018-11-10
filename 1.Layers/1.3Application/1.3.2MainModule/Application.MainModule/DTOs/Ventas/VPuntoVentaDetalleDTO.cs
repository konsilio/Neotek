using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Ventas
{
     public class VPuntoVentaDetalleDTO
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public short OrdenDetalle { get; set; }
        public int IdProducto { get; set; }
        public short IdProductoLinea { get; set; }
        public short IdCategoria { get; set; }
        public Nullable<decimal> PrecioUnitarioLt { get; set; }
        public Nullable<decimal> PrecioUnitarioKg { get; set; }
        public Nullable<decimal> DescuentoUnitarioLt { get; set; }
        public Nullable<decimal> DescuentoUnitarioKg { get; set; }
        public Nullable<decimal> CantidadLt { get; set; }
        public Nullable<decimal> CantidadKg { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal Subtotal { get; set; }
        public string ProductoDescripcion { get; set; }
        public string ProductoLinea { get; set; }
        public string ProductoCategoria { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public short IdUnidadMedida { get; set; }
        public Nullable<decimal> PrecioUnitarioProducto { get; set; }
        public Nullable<decimal> DescuentoUnitarioProducto { get; set; }
        public Nullable<decimal> CantidadProducto { get; set; }
        public string UnidadMedida { get; set; }
    }
}
