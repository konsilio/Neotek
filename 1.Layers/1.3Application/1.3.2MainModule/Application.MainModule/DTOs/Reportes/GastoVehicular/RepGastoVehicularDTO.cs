using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepGastoVehicularDTO
    {
        public string Vehiculo { get; set; }
        public double Combustible { get; set; }
        public double Mantenimiento { get; set; }
        public double Otros { get; set; }
        public double Total { get; set; }
    }
}
