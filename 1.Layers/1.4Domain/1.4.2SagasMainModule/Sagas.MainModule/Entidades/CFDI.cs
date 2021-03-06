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
    
    public partial class CFDI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CFDI()
        {
            this.Abono = new HashSet<Abono>();
        }
    
        public int Id_RelTF { get; set; }
        public string Id_FolioVenta { get; set; }
        public byte Id_FormaPago { get; set; }
        public int Id_MetodoPago { get; set; }
        public string VersionCFDI { get; set; }
        public System.DateTime FechaTimbrado { get; set; }
        public string UUID { get; set; }
        public int Folio { get; set; }
        public string Serie { get; set; }
        public string URLPdf { get; set; }
        public string URLXml { get; set; }
    
        public virtual FormaPago CFormaPago { get; set; }
        public virtual MetodoPago CMetodoPago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Abono> Abono { get; set; }
    }
}
