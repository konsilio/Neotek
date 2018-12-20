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
    
    public partial class Cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cargo()
        {
            this.Abono = new HashSet<Abono>();
        }
    
        public int IdCargo { get; set; }
        public int IdCliente { get; set; }
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal TotalAbonos { get; set; }
        public bool VentaExtraordinaria { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public bool Saldada { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Abono> Abono { get; set; }
        public virtual Cliente CCliente { get; set; }
    }
}
