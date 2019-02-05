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
    
    public partial class CUtilitario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUtilitario()
        {
            this.EquipoTransporte = new HashSet<EquipoTransporte>();
            this.AsignacionUtilitarios = new HashSet<AsignacionUtilitarios>();
        }
    
        public int IdUtilitario { get; set; }
        public short IdEmpresa { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipoTransporte> EquipoTransporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionUtilitarios> AsignacionUtilitarios { get; set; }
    }
}