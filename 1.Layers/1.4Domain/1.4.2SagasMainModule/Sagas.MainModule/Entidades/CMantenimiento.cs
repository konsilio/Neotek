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
    
    public partial class CMantenimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMantenimiento()
        {
            this.DetalleMantenimiento = new HashSet<DetalleMantenimiento>();
        }
    
        public int Id_Mantenimiento { get; set; }
        public string Mantenimiento { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleMantenimiento> DetalleMantenimiento { get; set; }
    }
}
