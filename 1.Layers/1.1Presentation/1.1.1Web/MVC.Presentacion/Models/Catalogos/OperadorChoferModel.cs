using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class OperadorChoferModel
    {
        public int IdOperadorChofer { get; set; }
        public byte IdTipoOperadorChofer { get; set; }
        public short IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }

    }
}