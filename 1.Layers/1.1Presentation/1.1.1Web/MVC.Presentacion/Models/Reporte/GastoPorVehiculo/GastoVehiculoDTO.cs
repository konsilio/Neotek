using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class GastoVehiculoDTO
    {
        public string Vehiculo { get; set; }
        public double Combustible { get; set; }
        public double Mantenimiento { get; set; }
        public double Otros { get; set; }
        public double Total { get; set; }
    }
}