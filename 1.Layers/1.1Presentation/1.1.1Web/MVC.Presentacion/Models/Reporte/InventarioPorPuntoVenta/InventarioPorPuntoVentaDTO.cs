using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class InventarioPorPuntoVentaDTO
    {
        public int ID { get; set; }
        public string NombreVehiculo { get; set; }
        public string LecturaInicial { get; set; }
        public string LecturaFinal { get; set; }
        public string ImagenLI { get; set; }
        public string ImagenLF { get; set; }
        public string Diferencia { get; set; }
        public DateTime Fecha { get; set; }
    }
}