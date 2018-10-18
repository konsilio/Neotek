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
    
    public partial class VentaPuntoDeVenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VentaPuntoDeVenta()
        {
            this.VentaPuntoDeVentaDetalle = new HashSet<VentaPuntoDeVentaDetalle>();
        }
    
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public int IdPuntoVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdOperadorChofer { get; set; }
        public Nullable<byte> IdTipoVenta { get; set; }
        public Nullable<int> IdFactura { get; set; }
        public string FolioOperacionDia { get; set; }
        public string FolioVenta { get; set; }
        public bool RequiereFactura { get; set; }
        public bool VentaACredito { get; set; }
        public decimal PorcentajeIva { get; set; }
        public Nullable<decimal> CambioRegresado { get; set; }
        public string PuntoVenta { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public bool ClienteConCredito { get; set; }
        public string OperadorChofer { get; set; }
        public bool DatosProcesados { get; set; }
        public Nullable<System.DateTime> FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal Subtotal { get; set; }
        public decimal SubtotalDia { get; set; }
        public decimal SubtotalMes { get; set; }
        public decimal SubtotalAnio { get; set; }
        public decimal SubtotalAcumDia { get; set; }
        public decimal SubtotalAcumMes { get; set; }
        public decimal SubtotalAcumAnio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoDia { get; set; }
        public decimal DescuentoMes { get; set; }
        public decimal DescuentoAnio { get; set; }
        public decimal DescuentoAcumDia { get; set; }
        public decimal DescuentoAcumMes { get; set; }
        public decimal DescuentoAcumAnio { get; set; }
        public decimal Iva { get; set; }
        public decimal IvaDia { get; set; }
        public decimal IvaMes { get; set; }
        public decimal IvaAnio { get; set; }
        public decimal IvaAcumDia { get; set; }
        public decimal IvaAcumMes { get; set; }
        public decimal IvaAcumAnio { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDia { get; set; }
        public decimal TotalMes { get; set; }
        public decimal TotalAnio { get; set; }
        public decimal TotalAcumDia { get; set; }
        public decimal TotalAcumMes { get; set; }
        public decimal TotalAcumAnio { get; set; }
        public Nullable<decimal> EfectivoRecibido { get; set; }
        public decimal EfectivoRecibidoDia { get; set; }
        public decimal EfectivoRecibidoMes { get; set; }
        public decimal EfectivoRecibidoAnio { get; set; }
        public decimal EfectivoRecibidoAcumDia { get; set; }
        public decimal EfectivoRecibidoAcumMes { get; set; }
        public decimal EfectivoRecibidoAcumAnio { get; set; }
    
        public virtual Cliente CCliente { get; set; }
        public virtual OperadorChofer COperadorChofer { get; set; }
        public virtual PuntoVenta CPuntoVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaPuntoDeVentaDetalle> VentaPuntoDeVentaDetalle { get; set; }
    }
}
