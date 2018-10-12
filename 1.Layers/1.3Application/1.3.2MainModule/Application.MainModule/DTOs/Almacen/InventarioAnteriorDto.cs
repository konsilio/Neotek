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
        //RemanenteAcumuladoDiaKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.RemanenteAcumuladoDiaKg, kilogramosRemanentes),
        //RemanenteAcumuladoDiaLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.RemanenteAcumuladoDiaLt, litrosRemanentes),
        //RemanenteAcumuladoMesKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.RemanenteAcumuladoMesKg, kilogramosRemanentes),
        //RemanenteAcumuladoMesLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.RemanenteAcumuladoMesLt, litrosRemanentes),
        //RemanenteAcumuladoAnioKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.RemanenteAcumuladoAnioKg, kilogramosRemanentes),
        //RemanenteAcumuladoAnioLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.RemanenteAcumuladoAnioLt, litrosRemanentes),
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
        //CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaDiaKg.Value, kilogramosRealesTractor),
        //CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaDiaLt.Value, litrosRealesTractor),
        //CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaMesKg.Value, kilogramosRealesTractor),
        //CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaMesLt.Value, litrosRealesTractor),
        //CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaAnioKg.Value, kilogramosRealesTractor),
        //CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaAnioLt.Value, litrosRealesTractor),
    }
}
