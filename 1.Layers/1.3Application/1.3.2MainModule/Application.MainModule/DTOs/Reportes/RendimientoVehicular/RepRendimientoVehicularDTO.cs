using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepRendimientoVehicularDTO
    {
        public int IdRegistro { get; set; }
        public string Vehiculo { get; set; }
        public decimal KmInicial { get; set; }
        public decimal KmFinal { get; set; }
        public decimal KmRecorridos { get; set; }
        public decimal LitrosCargados { get; set; }
        public decimal Rendimiento { get; set; }
        public DateTime Fecha { get; set; }
    }
}
