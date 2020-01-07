﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class RendimientoCamionetaDTO
    {

        public string Unidad { get; set; }
        public int DiasTrabajados { get; set; }
        public decimal MantenimientoMensual { get; set; }
        public decimal CarburacionMensualKg { get; set; }
        public decimal CombustibleDiarioKg { get; set; }
        public decimal MantenimientoDiario { get; set; }
        public decimal CombustibleDiario { get; set; }
        public decimal PtoEquilibrioDiario { get; set; }
        public decimal UtilidadDiaria { get; set; }
        public decimal Sueldo { get; set; }
        public decimal Celular { get; set; }
        public decimal Comisiones { get; set; }
        public decimal GastosDiarios { get; set; }
        //  public decimal PuntoEquilibrioKg { get; set; }
        public decimal VentaDiariaKg { get; set; }
        // public decimal UtilidadDiariaKg { get; set; }
        public decimal KgVendidos { get; set; }

    }
}