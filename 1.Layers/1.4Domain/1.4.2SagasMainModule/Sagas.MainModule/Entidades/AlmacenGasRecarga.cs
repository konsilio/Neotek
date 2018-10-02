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
    
    public partial class AlmacenGasRecarga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlmacenGasRecarga()
        {
            this.Cilindros = new HashSet<AlmacenGasRecargaCilindro>();
            this.Fotografias = new HashSet<AlmacenGasRecargaFoto>();
        }
    
        public int IdAlmacenGasRecarga { get; set; }
        public Nullable<short> IdCAlmacenGasSalida { get; set; }
        public short IdCAlmacenGasEntrada { get; set; }
        public Nullable<short> IdTipoMedidorSalida { get; set; }
        public Nullable<short> IdTipoMedidorEntrada { get; set; }
        public byte IdTipoEvento { get; set; }
        public Nullable<decimal> P5000Salida { get; set; }
        public Nullable<decimal> P5000Entrada { get; set; }
        public Nullable<decimal> PorcentajeSalida { get; set; }
        public Nullable<decimal> ProcentajeEntrada { get; set; }
        public string ClaveOperacion { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual UnidadAlmacenGas UnidadAlmacenSalida { get; set; }
        public virtual UnidadAlmacenGas UnidadAlmacenEntrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasRecargaCilindro> Cilindros { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasRecargaFoto> Fotografias { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
    }
}
