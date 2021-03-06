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
    
    public partial class Requisicion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requisicion()
        {
            this.Productos = new HashSet<RequisicionProducto>();
            this.OrdenesCompra = new HashSet<OrdenCompra>();
            this.EntradasDeGas = new HashSet<AlmacenGasDescarga>();
            this.EntradasDeProductos = new HashSet<AlmacenEntradaProducto>();
            this.SalidasDeProductos = new HashSet<AlmacenSalidaProducto>();
        }
    
        public int IdRequisicion { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public short IdEmpresa { get; set; }
        public string NumeroRequisicion { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public byte IdRequisicionEstatus { get; set; }
        public System.DateTime FechaRequerida { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioRevision { get; set; }
        public string OpinionAlmacen { get; set; }
        public Nullable<System.DateTime> FechaRevision { get; set; }
        public string MotivoCancelacion { get; set; }
        public Nullable<int> IdUsuarioAutorizacion { get; set; }
        public Nullable<System.DateTime> FechaAutorizacion { get; set; }
        public bool EsExterno { get; set; }
    
        public virtual RequisicionEstatus RequisicionEstatus { get; set; }
        public virtual Usuario Solicitante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisicionProducto> Productos { get; set; }
        public virtual Usuario Almacenista { get; set; }
        public virtual Usuario Autorizador { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompra> OrdenesCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> EntradasDeGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenEntradaProducto> EntradasDeProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenSalidaProducto> SalidasDeProductos { get; set; }
    }
}
