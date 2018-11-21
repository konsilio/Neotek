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
    
    public partial class AlmacenGasDescarga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlmacenGasDescarga()
        {
            this.Fotos = new HashSet<AlmacenGasDescargaFoto>();
            this.Movimientos = new HashSet<AlmacenGasMovimiento>();
        }
    
        public int IdAlmacenEntradaGasDescarga { get; set; }
        public Nullable<short> IdAlmacenGas { get; set; }
        public Nullable<int> IdRequisicion { get; set; }
        public Nullable<int> IdOrdenCompraExpedidor { get; set; }
        public Nullable<int> IdOrdenCompraPorteador { get; set; }
        public Nullable<int> IdProveedorExpedidor { get; set; }
        public Nullable<int> IdProveedorPorteador { get; set; }
        public Nullable<short> IdCAlmacenGas { get; set; }
        public Nullable<short> IdTipoMedidorTractor { get; set; }
        public Nullable<short> IdTipoMedidorAlmacen { get; set; }
        public Nullable<bool> DatosProcesados { get; set; }
        public string ClaveOperacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<decimal> PorcenMagnatelOcularTractorFIN { get; set; }
        public Nullable<decimal> PorcenMagnatelOcularAlmacenFIN { get; set; }
        public Nullable<System.DateTime> FechaFinDescarga { get; set; }
        public Nullable<bool> TanquePrestado { get; set; }
        public Nullable<decimal> PorcenMagnatelOcularTractorINI { get; set; }
        public Nullable<decimal> PorcenMagnatelOcularAlmacenINI { get; set; }
        public Nullable<System.DateTime> FechaInicioDescarga { get; set; }
        public Nullable<System.DateTime> FechaPapeleta { get; set; }
        public Nullable<System.DateTime> FechaEmbarque { get; set; }
        public string NumeroEmbarque { get; set; }
        public string NombreOperador { get; set; }
        public string PlacasTractor { get; set; }
        public string NumTanquePG { get; set; }
        public Nullable<decimal> CapacidadTanqueLt { get; set; }
        public Nullable<decimal> CapacidadTanqueKg { get; set; }
        public Nullable<decimal> PorcenMagnatelPapeleta { get; set; }
        public Nullable<decimal> MasaKg { get; set; }
        public Nullable<decimal> PresionTanque { get; set; }
        public string Sello { get; set; }
        public Nullable<decimal> ValorCarga { get; set; }
        public string NombreResponsable { get; set; }
        public Nullable<decimal> PorcenMagnatelOcular { get; set; }
        public Nullable<System.DateTime> FechaEntraGas { get; set; }
            
        public virtual AlmacenGas AlmacenGasTotal { get; set; }
        public virtual UnidadAlmacenGas UnidadAlmacen { get; set; }
        public virtual TipoMedidorUnidadAlmacenGas MedidorAlamcen { get; set; }
        public virtual TipoMedidorUnidadAlmacenGas MedidorTractor { get; set; }
        public virtual OrdenCompra OCompraExpedidor { get; set; }
        public virtual OrdenCompra OCompraPorteador { get; set; }
        public virtual Requisicion Requisicion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescargaFoto> Fotos { get; set; }
        public virtual Proveedor Expedidor { get; set; }
        public virtual Proveedor Porteador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasMovimiento> Movimientos { get; set; }
    }
}
