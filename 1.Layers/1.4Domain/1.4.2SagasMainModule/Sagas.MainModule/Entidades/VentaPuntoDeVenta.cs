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
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal PorcentajeIva { get; set; }
        public Nullable<decimal> EfectivoRecibido { get; set; }
        public Nullable<decimal> CambioRegresado { get; set; }
        public string PuntoVenta { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public bool ClienteConCredito { get; set; }
        public string OperadorChofer { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual Cliente CCliente { get; set; }
        public virtual OperadorChofer COperadorChofer { get; set; }
        public virtual PuntoVenta CPuntoVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaPuntoDeVentaDetalle> VentaPuntoDeVentaDetalle { get; set; }
    }
}
