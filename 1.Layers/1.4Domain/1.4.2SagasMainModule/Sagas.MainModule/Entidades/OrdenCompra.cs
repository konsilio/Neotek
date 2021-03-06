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
    
    public partial class OrdenCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdenCompra()
        {
            this.Productos = new HashSet<OrdenCompraProducto>();
            this.AlmacenGasDescarga = new HashSet<AlmacenGasDescarga>();
            this.AlmacenGasDescarga1 = new HashSet<AlmacenGasDescarga>();
            this.EntradasAAlmacenMercancias = new HashSet<AlmacenEntradaProducto>();
            this.OrdenCompraPago = new HashSet<OrdenCompraPago>();
            this.Ingreso = new HashSet<Ingreso>();
        }
    
        public int IdOrdenCompra { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdOrdenCompraEstatus { get; set; }
        public int IdRequisicion { get; set; }
        public int IdProveedor { get; set; }
        public Nullable<int> IdUsuarioGenerador { get; set; }
        public Nullable<int> IdUsuarioAutorizador { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCuentaContable { get; set; }
        public string NumOrdenCompra { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
        public bool EsTransporteGas { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaAutorizacion { get; set; }
        public Nullable<System.DateTime> FechaComplemento { get; set; }
        public Nullable<decimal> SubtotalSinIva { get; set; }
        public Nullable<decimal> SubtotalSinIeps { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Ieps { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> MontBelvieuDlls { get; set; }
        public Nullable<decimal> TarifaServicioPorGalonDlls { get; set; }
        public Nullable<decimal> TipoDeCambioDOF { get; set; }
        public Nullable<decimal> PrecioPorGalon { get; set; }
        public Nullable<decimal> FactorGalonALitros { get; set; }
        public Nullable<decimal> ImporteEnLitros { get; set; }
        public Nullable<decimal> FactorCompraLitrosAKilos { get; set; }
        public Nullable<decimal> PVPM { get; set; }
        public string FolioFiscalUUID { get; set; }
        public string FolioFactura { get; set; }
        public Nullable<System.DateTime> FechaResgistroFactura { get; set; }
        public Nullable<decimal> FactorConvTransporte { get; set; }
        public Nullable<decimal> PrecioTransporte { get; set; }
        public Nullable<decimal> Casetas { get; set; }
    
        public virtual Proveedor Proveedor { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual OrdenCompraEstatus OrdenCompraEstatus { get; set; }
        public virtual Requisicion Requisicion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompraProducto> Productos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> AlmacenGasDescarga { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> AlmacenGasDescarga1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenEntradaProducto> EntradasAAlmacenMercancias { get; set; }
        public virtual Usuario UsuarioAutorizador { get; set; }
        public virtual Usuario UsuarioGenerador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompraPago> OrdenCompraPago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingreso> Ingreso { get; set; }
    }
}
