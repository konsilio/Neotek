//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sagas.MainModule.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdenCompraPago
    {
        public int IdOrdenCompra { get; set; }
        public short Orden { get; set; }
        public Nullable<byte> IdFormaPago { get; set; }
        public short IdBanco { get; set; }
        public string CuentaBancaria { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal TotalImporte { get; set; }
        public decimal SaldoInsoluto { get; set; }
        public string UrlPathCapturaPantalla { get; set; }
        public string PhysicalPathCapturaPantalla { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaConfirmacion { get; set; }
    
        public virtual OrdenCompra OrdenCompra { get; set; }
    }
}
