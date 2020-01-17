using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class ControlAsistenciaModel
    {
      
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string PtoVenta { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }

   
        public List<VentasXPuntoVentaModel> ListPtoVenta { get; set; }


    }
}