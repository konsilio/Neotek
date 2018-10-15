using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class InventarioAnteriorDto
    {
        public decimal RemanenteKg { get; set; }
        public decimal RemanenteLt { get; set; }
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
        public decimal PorcentajeAnterior { get; set; }
        public decimal? P5000Anterior { get; set; }
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
    }
}
