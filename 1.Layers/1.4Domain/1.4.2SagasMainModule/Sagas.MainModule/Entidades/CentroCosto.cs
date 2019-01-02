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
    
    public partial class CentroCosto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CentroCosto()
        {
            this.ReqProductos = new HashSet<RequisicionProducto>();
            this.OCompraProductos = new HashSet<OrdenCompraProducto>();
            this.Egreso = new HashSet<Egreso>();
            this.Ingreso = new HashSet<Ingreso>();
        }
    
        public int IdCentroCosto { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdTipoCentroCosto { get; set; }
        public Nullable<int> IdEquipoTransporte { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdVehiculoUtilitario { get; set; }
        public Nullable<short> IdCAlmacenGas { get; set; }
        public Nullable<int> IdEstacionCarburacion { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public Nullable<int> IdCilindro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisicionProducto> ReqProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompraProducto> OCompraProductos { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual UnidadAlmacenGas UnidadAlmacenGas { get; set; }
        public virtual UnidadAlmacenGasCilindro UnidadAlmacenGasCilindro { get; set; }
        public virtual Camioneta Camioneta { get; set; }
        public virtual EstacionCarburacion EstacionCarburacion { get; set; }
        public virtual Pipa Pipa { get; set; }
        public virtual TipoCentroCosto TipoCentroCosto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Egreso> Egreso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingreso> Ingreso { get; set; }
    }
}
