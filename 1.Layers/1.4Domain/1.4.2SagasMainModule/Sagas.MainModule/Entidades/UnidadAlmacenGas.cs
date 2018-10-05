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
    
    public partial class UnidadAlmacenGas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnidadAlmacenGas()
        {
            this.DescargasGas = new HashSet<AlmacenGasDescarga>();
            this.CentroCosto = new HashSet<CentroCosto>();
            this.TomasLectura = new HashSet<AlmacenGasTomaLectura>();
            this.RecargasSalida = new HashSet<AlmacenGasRecarga>();
            this.RecargasEntrada = new HashSet<AlmacenGasRecarga>();
            this.AutoConsumosSalida = new HashSet<AlmacenGasAutoConsumo>();
            this.AutoConsumosEntrada = new HashSet<AlmacenGasAutoConsumo>();
            this.Calibraciones = new HashSet<AlmacenGasCalibracion>();
            this.TraspasosSalida = new HashSet<AlmacenGasTraspaso>();
            this.TraspasosEntrada = new HashSet<AlmacenGasTraspaso>();
            this.PuntosVenta = new HashSet<PuntoVenta>();
            this.MovimientosGas = new HashSet<AlmacenGasMovimiento>();
        }
    
        public short IdCAlmacenGas { get; set; }
        public Nullable<short> IdAlmacenGas { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdTipoAlmacen { get; set; }
        public Nullable<short> IdTipoMedidor { get; set; }
        public Nullable<int> IdEstacionCarburacion { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public bool EsGeneral { get; set; }
        public bool EsAlterno { get; set; }
        public string Numero { get; set; }
        public Nullable<decimal> CapacidadTanqueLt { get; set; }
        public Nullable<decimal> CapacidadTanqueKg { get; set; }
        public decimal CantidadActualLt { get; set; }
        public decimal CantidadActualKg { get; set; }
        public decimal PorcentajeActual { get; set; }
        public Nullable<decimal> P5000Actual { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal PorcentajeCalibracionPlaneada { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> DescargasGas { get; set; }
        public virtual TipoUnidadAlmacenGas TipoUnidadAlmacenGas { get; set; }
        public virtual TipoMedidorUnidadAlmacenGas Medidor { get; set; }
        public virtual Camioneta Camioneta { get; set; }
        public virtual EstacionCarburacion EstacionCarburacion { get; set; }
        public virtual Pipa Pipa { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentroCosto> CentroCosto { get; set; }
        public virtual AlmacenGas AlmacenGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasTomaLectura> TomasLectura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasRecarga> RecargasSalida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasRecarga> RecargasEntrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasAutoConsumo> AutoConsumosSalida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasAutoConsumo> AutoConsumosEntrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasCalibracion> Calibraciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasTraspaso> TraspasosSalida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasTraspaso> TraspasosEntrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntoVenta> PuntosVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasMovimiento> MovimientosGas { get; set; }
    }
}
