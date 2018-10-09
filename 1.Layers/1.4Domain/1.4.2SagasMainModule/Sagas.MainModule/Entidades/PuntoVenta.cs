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
    
    public partial class PuntoVenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PuntoVenta()
        {
            this.ReporteDelDia = new HashSet<ReporteDelDia>();
            this.VentaCajaGeneral = new HashSet<VentaCajaGeneral>();
            this.VentaCorteAnticipoEC = new HashSet<VentaCorteAnticipoEC>();
            this.VentaMovimiento = new HashSet<VentaMovimiento>();
            this.VentaPuntoDeVenta = new HashSet<VentaPuntoDeVenta>();
        }
    
        public int IdPuntoVenta { get; set; }
        public short IdEmpresa { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int IdOperadorChofer { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual UnidadAlmacenGas UnidadesAlmacen { get; set; }
        public virtual OperadorChofer OperadorChofer { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReporteDelDia> ReporteDelDia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaCajaGeneral> VentaCajaGeneral { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaCorteAnticipoEC> VentaCorteAnticipoEC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaMovimiento> VentaMovimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaPuntoDeVenta> VentaPuntoDeVenta { get; set; }
    }
}
