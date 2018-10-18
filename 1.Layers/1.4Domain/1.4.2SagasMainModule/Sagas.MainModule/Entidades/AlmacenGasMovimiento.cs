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
    
    public partial class AlmacenGasMovimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlmacenGasMovimiento()
        {
            this.Fotografias = new HashSet<AlmacenGasMovimientoFoto>();
        }
    
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public byte IdTipoMovimiento { get; set; }
        public Nullable<byte> IdTipoEvento { get; set; }
        public Nullable<short> IdOrdenVenta { get; set; }
        public short IdAlmacenGas { get; set; }
        public short IdCAlmacenGasPrincipal { get; set; }
        public Nullable<short> IdCAlmacenGasReferencia { get; set; }
        public Nullable<int> IdAlmacenEntradaGasDescarga { get; set; }
        public Nullable<int> IdAlmacenGasRecarga { get; set; }
        public string FolioOperacionDia { get; set; }
        public string CAlmacenPrincipalNombre { get; set; }
        public string CAlmacenReferenciaNombre { get; set; }
        public string OperadorChoferNombre { get; set; }
        public string TipoEvento { get; set; }
        public string TipoMovimiento { get; set; }
        public Nullable<decimal> RemanenteKg { get; set; }
        public Nullable<decimal> RemanenteLt { get; set; }
        public decimal RemanenteAcumuladoDiaKg { get; set; }
        public decimal RemanenteAcumuladoDiaLt { get; set; }
        public decimal RemanenteAcumuladoMesKg { get; set; }
        public decimal RemanenteAcumuladoMesLt { get; set; }
        public decimal RemanenteAcumuladoAnioKg { get; set; }
        public decimal RemanenteAcumuladoAnioLt { get; set; }
        public decimal EntradaKg { get; set; }
        public decimal EntradaLt { get; set; }
        public decimal SalidaKg { get; set; }
        public decimal SalidaLt { get; set; }
        public decimal CantidadAnteriorKg { get; set; }
        public decimal CantidadAnteriorLt { get; set; }
        public decimal CantidadActualKg { get; set; }
        public decimal CantidadActualLt { get; set; }
        public Nullable<decimal> CantidadAcumuladaDiaKg { get; set; }
        public Nullable<decimal> CantidadAcumuladaDiaLt { get; set; }
        public Nullable<decimal> CantidadAcumuladaMesKg { get; set; }
        public Nullable<decimal> CantidadAcumuladaMesLt { get; set; }
        public Nullable<decimal> CantidadAcumuladaAnioKg { get; set; }
        public Nullable<decimal> CantidadAcumuladaAnioLt { get; set; }
        public Nullable<decimal> PorcentajeAnterior { get; set; }
        public Nullable<decimal> PorcentajeActual { get; set; }
        public Nullable<decimal> P5000Anterior { get; set; }
        public Nullable<decimal> P5000Actual { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal CantidadAnteriorGeneralKg { get; set; }
        public decimal CantidadAnteriorGeneralLt { get; set; }
        public decimal CantidadActualGeneralKg { get; set; }
        public decimal CantidadActualGeneralLt { get; set; }
        public decimal PorcentajeAnteriorGeneral { get; set; }
        public decimal PorcentajeActualGeneral { get; set; }
        public decimal CantidadAnteriorTotalKg { get; set; }
        public decimal CantidadAnteriorTotalLt { get; set; }
        public decimal CantidadActualTotalKg { get; set; }
        public decimal CantidadActualTotalLt { get; set; }
        public decimal PorcentajeAnteriorTotal { get; set; }
        public decimal PorcentajeActualTotal { get; set; }
        public Nullable<decimal> AutoconsumoKg { get; set; }
        public Nullable<decimal> AutoconsumoLt { get; set; }
        public Nullable<decimal> AutoconsumoDiaKg { get; set; }
        public Nullable<decimal> AutoconsumoDiaLt { get; set; }
        public Nullable<decimal> AutoconsumoMesKg { get; set; }
        public Nullable<decimal> AutoconsumoMesLt { get; set; }
        public Nullable<decimal> AutoconsumoAnioKg { get; set; }
        public Nullable<decimal> AutoconsumoAnioLt { get; set; }
        public decimal AutoconsumoAcumDiaKg { get; set; }
        public decimal AutoconsumoAcumDiaLt { get; set; }
        public decimal AutoconsumoAcumMesKg { get; set; }
        public decimal AutoconsumoAcumMesLt { get; set; }
        public decimal AutoconsumoAcumAnioKg { get; set; }
        public decimal AutoconsumoAcumAnioLt { get; set; }
        public Nullable<decimal> CalibracionKg { get; set; }
        public Nullable<decimal> CalibracionLt { get; set; }
        public Nullable<decimal> CalibracionDiaKg { get; set; }
        public Nullable<decimal> CalibracionDiaLt { get; set; }
        public Nullable<decimal> CalibracionMesKg { get; set; }
        public Nullable<decimal> CalibracionMesLt { get; set; }
        public Nullable<decimal> CalibracionAnioKg { get; set; }
        public Nullable<decimal> CalibracionAnioLt { get; set; }
        public decimal CalibracionAcumDiaKg { get; set; }
        public decimal CalibracionAcumDiaLt { get; set; }
        public decimal CalibracionAcumMesKg { get; set; }
        public decimal CalibracionAcumMesLt { get; set; }
        public decimal CalibracionAcumAnioKg { get; set; }
        public decimal CalibracionAcumAnioLt { get; set; }
        public Nullable<decimal> DescargaKg { get; set; }
        public Nullable<decimal> DescargaLt { get; set; }
        public Nullable<decimal> DescargaDiaKg { get; set; }
        public Nullable<decimal> DescargaDiaLt { get; set; }
        public Nullable<decimal> DescargaMesKg { get; set; }
        public Nullable<decimal> DescargaMesLt { get; set; }
        public Nullable<decimal> DescargaAnioKg { get; set; }
        public Nullable<decimal> DescargaAnioLt { get; set; }
        public decimal DescargaAcumDiaKg { get; set; }
        public decimal DescargaAcumDiaLt { get; set; }
        public decimal DescargaAcumMesKg { get; set; }
        public decimal DescargaAcumMesLt { get; set; }
        public decimal DescargaAcumAnioKg { get; set; }
        public decimal DescargaAcumAnioLt { get; set; }
        public decimal CAlmEntradaDiaKg { get; set; }
        public decimal CAlmEntradaDiaLt { get; set; }
        public decimal CAlmSalidaDiaKg { get; set; }
        public decimal CAlmSalidaDiaLt { get; set; }
        public decimal CAlmEntradaMesKg { get; set; }
        public decimal CAlmEntradaMesLt { get; set; }
        public decimal CAlmSalidaMesKg { get; set; }
        public decimal CAlmSalidaMesLt { get; set; }
        public decimal CAlmEntradaAnioKg { get; set; }
        public decimal CAlmEntradaAnioLt { get; set; }
        public decimal CAlmSalidaAnioKg { get; set; }
        public decimal CAlmSalidaAnioLt { get; set; }
        public Nullable<decimal> RecargaKg { get; set; }
        public Nullable<decimal> RecargaLt { get; set; }
        public Nullable<decimal> RecargaDiaKg { get; set; }
        public Nullable<decimal> RecargaDiaLt { get; set; }
        public Nullable<decimal> RecargaMesKg { get; set; }
        public Nullable<decimal> RecargaMesLt { get; set; }
        public Nullable<decimal> RecargaAnioKg { get; set; }
        public Nullable<decimal> RecargaAnioLt { get; set; }
        public decimal RecargaAcumDiaKg { get; set; }
        public decimal RecargaAcumDiaLt { get; set; }
        public decimal RecargaAcumMesKg { get; set; }
        public decimal RecargaAcumMesLt { get; set; }
        public decimal RecargaAcumAnioKg { get; set; }
        public decimal RecargaAcumAnioLt { get; set; }
        public Nullable<decimal> RemaKg { get; set; }
        public Nullable<decimal> RemaLt { get; set; }
        public Nullable<decimal> RemaDiaKg { get; set; }
        public Nullable<decimal> RemaDiaLt { get; set; }
        public Nullable<decimal> RemaMesKg { get; set; }
        public Nullable<decimal> RemaMesLt { get; set; }
        public Nullable<decimal> RemaAnioKg { get; set; }
        public Nullable<decimal> RemaAnioLt { get; set; }
        public decimal RemaAcumDiaKg { get; set; }
        public decimal RemaAcumDiaLt { get; set; }
        public decimal RemaAcumMesKg { get; set; }
        public decimal RemaAcumMesLt { get; set; }
        public decimal RemaAcumAnioKg { get; set; }
        public decimal RemaAcumAnioLt { get; set; }
        public Nullable<decimal> TraspasoKg { get; set; }
        public Nullable<decimal> TraspasoLt { get; set; }
        public Nullable<decimal> TraspasoDiaKg { get; set; }
        public Nullable<decimal> TraspasoDiaLt { get; set; }
        public Nullable<decimal> TraspasoMesKg { get; set; }
        public Nullable<decimal> TraspasoMesLt { get; set; }
        public Nullable<decimal> TraspasoAnioKg { get; set; }
        public Nullable<decimal> TraspasoAnioLt { get; set; }
        public decimal TraspasoAcumDiaKg { get; set; }
        public decimal TraspasoAcumDiaLt { get; set; }
        public decimal TraspasoAcumMesKg { get; set; }
        public decimal TraspasoAcumMesLt { get; set; }
        public decimal TraspasoAcumAnioKg { get; set; }
        public decimal TraspasoAcumAnioLt { get; set; }
        public Nullable<decimal> VentaKg { get; set; }
        public Nullable<decimal> VentaLt { get; set; }
        public Nullable<decimal> VentaDiaKg { get; set; }
        public Nullable<decimal> VentaDiaLt { get; set; }
        public Nullable<decimal> VentaMesKg { get; set; }
        public Nullable<decimal> VentaMesLt { get; set; }
        public Nullable<decimal> VentaAnioKg { get; set; }
        public Nullable<decimal> VentaAnioLt { get; set; }
        public decimal VentaAcumDiaKg { get; set; }
        public decimal VentaAcumDiaLt { get; set; }
        public decimal VentaAcumMesKg { get; set; }
        public decimal VentaAcumMesLt { get; set; }
        public decimal VentaAcumAnioKg { get; set; }
        public decimal VentaAcumAnioLt { get; set; }
    
        public virtual AlmacenGasDescarga Descarga { get; set; }
        public virtual AlmacenGas Inventario { get; set; }
        public virtual AlmacenGasRecarga Recarga { get; set; }
        public virtual UnidadAlmacenGas UnidadAlamcen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasMovimientoFoto> Fotografias { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual CTipoMovimiento CTipoMovimiento { get; set; }
    }
}
