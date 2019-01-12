using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class PipaModel
    {
        public int IdPipa { get; set; }
        public short IdEmpresa { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}