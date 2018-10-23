using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class InventarioAnteriorDto
    {
        public decimal RemaKg { get; set; }
        public decimal RemaLt { get; set; }
        public decimal RemaDiaKg { get; set; }
        public decimal RemaDiaLt { get; set; }
        public decimal RemaMesKg { get; set; }
        public decimal RemaMesLt { get; set; }
        public decimal RemaAnioKg { get; set; }
        public decimal RemaAnioLt { get; set; }
        public decimal RemaAcumDiaKg { get; set; }
        public decimal RemaAcumDiaLt { get; set; }
        public decimal RemaAcumMesKg { get; set; }
        public decimal RemaAcumMesLt { get; set; }
        public decimal RemaAcumAnioKg { get; set; }
        public decimal RemaAcumAnioLt { get; set; }
        public decimal EntradaKg { get; set; }
        public decimal EntradaLt { get; set; }
        public decimal SalidaKg { get; set; }
        public decimal SalidaLt { get; set; }
        public decimal CantidadAnteriorKg { get; set; }
        public decimal CantidadAnteriorLt { get; set; }
        public decimal? PorcentajeAnterior { get; set; }
        public decimal? P5000Anterior { get; set; }
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
        public decimal CantidadAnteriorTotalKg { get; set; }
        public decimal CantidadAnteriorTotalLt { get; set; }
        public decimal PorcentajeAnteriorTotal { get; set; }
        public decimal CantidadAnteriorGeneralKg { get; set; }
        public decimal CantidadAnteriorGeneralLt { get; set; }
        public decimal PorcentajeAnteriorGeneral { get; set; }
        public decimal CantidadAcumuladaDiaKg { get; set; }
        public decimal CantidadAcumuladaDiaLt { get; set; }
        public decimal CantidadAcumuladaMesKg { get; set; }
        public decimal CantidadAcumuladaMesLt { get; set; }
        public decimal CantidadAcumuladaAnioKg { get; set; }
        public decimal CantidadAcumuladaAnioLt { get; set; }

        public decimal DescargaKg { get; set; }
        public decimal DescargaLt { get; set; }
        public decimal DescargaDiaKg { get; set; }
        public decimal DescargaDiaLt { get; set; }
        public decimal DescargaMesKg { get; set; }
        public decimal DescargaMesLt { get; set; }
        public decimal DescargaAnioKg { get; set; }
        public decimal DescargaAnioLt { get; set; }
        public decimal DescargaAcumDiaKg { get; set; }
        public decimal DescargaAcumDiaLt { get; set; }
        public decimal DescargaAcumMesKg { get; set; }
        public decimal DescargaAcumMesLt { get; set; }
        public decimal DescargaAcumAnioKg { get; set; }
        public decimal DescargaAcumAnioLt { get; set; }

        public decimal RecargaKg { get; set; }
        public decimal RecargaLt { get; set; }
        public decimal RecargaDiaKg { get; set; }
        public decimal RecargaDiaLt { get; set; }
        public decimal RecargaMesKg { get; set; }
        public decimal RecargaMesLt { get; set; }
        public decimal RecargaAnioKg { get; set; }
        public decimal RecargaAnioLt { get; set; }
        public decimal RecargaAcumDiaKg { get; set; }
        public decimal RecargaAcumDiaLt { get; set; }
        public decimal RecargaAcumMesKg { get; set; }
        public decimal RecargaAcumMesLt { get; set; }
        public decimal RecargaAcumAnioKg { get; set; }
        public decimal RecargaAcumAnioLt { get; set; }
        public decimal TraspasoKg { get; set; }
        public decimal TraspasoLt { get; set; }
        public decimal TraspasoDiaKg { get; set; }
        public decimal TraspasoDiaLt { get; set; }
        public decimal TraspasoMesKg { get; set; }
        public decimal TraspasoMesLt { get; set; }
        public decimal TraspasoAnioKg { get; set; }
        public decimal TraspasoAnioLt { get; set; }
        public decimal TraspasoAcumDiaKg { get; set; }
        public decimal TraspasoAcumDiaLt { get; set; }
        public decimal TraspasoAcumMesKg { get; set; }
        public decimal TraspasoAcumMesLt { get; set; }
        public decimal TraspasoAcumAnioKg { get; set; }
        public decimal TraspasoAcumAnioLt { get; set; }
        public decimal AutoconsumoKg { get; set; }
        public decimal AutoconsumoLt { get; set; }
        public decimal AutoconsumoDiaKg { get; set; }
        public decimal AutoconsumoDiaLt { get; set; }
        public decimal AutoconsumoMesKg { get; set; }
        public decimal AutoconsumoMesLt { get; set; }
        public decimal AutoconsumoAnioKg { get; set; }
        public decimal AutoconsumoAnioLt { get; set; }
        public decimal AutoconsumoAcumDiaKg { get; set; }
        public decimal AutoconsumoAcumDiaLt { get; set; }
        public decimal AutoconsumoAcumMesKg { get; set; }
        public decimal AutoconsumoAcumMesLt { get; set; }
        public decimal AutoconsumoAcumAnioKg { get; set; }
        public decimal AutoconsumoAcumAnioLt { get; set; }
        public decimal CalibracionKg { get; set; }
        public decimal CalibracionLt { get; set; }
        public decimal CalibracionDiaKg { get; set; }
        public decimal CalibracionDiaLt { get; set; }
        public decimal CalibracionMesKg { get; set; }
        public decimal CalibracionMesLt { get; set; }
        public decimal CalibracionAnioKg { get; set; }
        public decimal CalibracionAnioLt { get; set; }
        public decimal CalibracionAcumDiaKg { get; set; }
        public decimal CalibracionAcumDiaLt { get; set; }
        public decimal CalibracionAcumMesKg { get; set; }
        public decimal CalibracionAcumMesLt { get; set; }
        public decimal CalibracionAcumAnioKg { get; set; }
        public decimal CalibracionAcumAnioLt { get; set; }

        public string NombreOperador { get; set; }
    }
}
