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
    
    public partial class MetodoPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetodoPago()
        {
            this.RelTicketCFDI = new HashSet<CFDI>();
        }
    
        public int Id_MetodoPago { get; set; }
        public string MetodoPagoSAT { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaIniVigencia { get; set; }
        public Nullable<System.DateTime> FechaFinVigencia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CFDI> RelTicketCFDI { get; set; }
    }
}
