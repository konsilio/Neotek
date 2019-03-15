﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class AsignacionModel
    {
        public int IdAsignacion { get; set; }
        public short IdEmpresa { get; set; }
        public int IdChofer { get; set; }
        public string Chofer { get; set; }
        public short IdVehiculo { get; set; }
        public string Vehiculo { get; set; }
        public short TipoVehiculo { get; set; }
    }
}