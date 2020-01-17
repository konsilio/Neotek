using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class EstacionCarburacionDTO
    {
        public int IdEstacionCarburacion { get; set; }
        public short IdEmpresa { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}