using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class EstatusTipoFechaModel
    {
        public byte IdPrecioVentaEstatus { get; set; }
        public string Descripción { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegsitro { get; set; }
    }
}