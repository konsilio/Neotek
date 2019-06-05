using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class InventarioPorPuntoVentaModel
    {
        public DateTime Fecha { get; set; }
        public List<PipaModel> Pipas { get; set; }
        public List<EstacionCarburacionDTO> Estaciones { get; set; }
    }
}