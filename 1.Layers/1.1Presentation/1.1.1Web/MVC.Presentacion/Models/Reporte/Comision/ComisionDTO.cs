﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class ComisionDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Puesto { get; set; }
        public string Empleado { get; set; }
        public string PuntoVenta { get; set; }
        public decimal Venta { get; set; }
        public string Unidad { get; set; }
        public decimal Comision { get; set; }
        public decimal Total { get; set; }
    }
}