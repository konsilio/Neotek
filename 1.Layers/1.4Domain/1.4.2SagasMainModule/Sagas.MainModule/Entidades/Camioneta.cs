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
    
    public partial class Camioneta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Camioneta()
        {
            this.UnidadAlmacenGas = new HashSet<UnidadAlmacenGas>();
            this.Cilindros = new HashSet<CamionetaCilindro>();
            this.CCentroCosto = new HashSet<CentroCosto>();
            this.EquipoTransporte = new HashSet<EquipoTransporte>();
            this.CDetalleEquipoTransporte = new HashSet<CDetalleEquipoTransporte>();
            this.DetalleMantenimiento = new HashSet<DetalleMantenimiento>();
            this.DetalleRecargaCombustible = new HashSet<DetalleRecargaCombustible>();
        }
    
        public int IdCamioneta { get; set; }
        public short IdEmpresa { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadAlmacenGas> UnidadAlmacenGas { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CamionetaCilindro> Cilindros { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentroCosto> CCentroCosto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipoTransporte> EquipoTransporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CDetalleEquipoTransporte> CDetalleEquipoTransporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleMantenimiento> DetalleMantenimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleRecargaCombustible> DetalleRecargaCombustible { get; set; }
    }
}
