using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Transporte
{
    public class AsignacionModel
    {
        public short IdEmpresa { get; set; }
        public int IdChofer { get; set; }
        public string Chofer { get; set; }
        public int IdVehiculo { get; set; }
        public string Vehiculo { get; set; }
    }
}