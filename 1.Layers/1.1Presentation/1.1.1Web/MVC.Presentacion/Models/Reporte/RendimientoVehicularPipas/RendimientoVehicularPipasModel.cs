using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class RendimientoVehicularPipasModel
    {
        public string Unidad { get; set; }
        public int DiasTrabajados { get; set; }
        public decimal MantenimientoMensual { get; set; }
        public decimal CarburacionMensualLt { get; set; }
        public decimal CombustibleDiarioLt { get; set; }
        public decimal MantenimientoDiario { get; set; }
        public decimal CombustibleDiario { get; set; }
        public decimal PtoEquilibrioDiario { get; set; }
        public decimal UtilidadDiaria { get; set; }
        public decimal Sueldo { get; set; }
        public decimal Celular { get; set; }
        public decimal Comisiones { get; set; }
        public decimal GastosDiarios { get; set; }
        public decimal VentaDiariaKg { get; set; }
        public decimal KgVendidos { get; set; }
    }
}