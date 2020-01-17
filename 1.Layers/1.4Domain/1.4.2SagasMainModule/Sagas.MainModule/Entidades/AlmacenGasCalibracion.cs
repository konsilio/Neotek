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
    
    public partial class AlmacenGasCalibracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlmacenGasCalibracion()
        {
            this.Fotografias = new HashSet<AlmacenGasCalibracionFoto>();
        }
    
        public short IdCAlmacenGas { get; set; }
        public int IdOrden { get; set; }
        public Nullable<short> IdTipoMedidor { get; set; }
        public Nullable<byte> IdTipoEvento { get; set; }
        public Nullable<byte> IdDestinoCalibracion { get; set; }
        public Nullable<decimal> PorcentajeCalibracion { get; set; }
        public Nullable<decimal> P5000 { get; set; }
        public decimal Porcentaje { get; set; }
        public string ClaveOperacion { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaAplicacion { get; set; }
    
        public virtual UnidadAlmacenGas UnidadAlmacenGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasCalibracionFoto> Fotografias { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
    }
}
