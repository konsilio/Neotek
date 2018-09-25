using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class PuntoVentaModel
    {
        public int IdPuntoVenta { get; set; }
        public short IdEmpresa { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int IdOperadorChofer { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string UnidadesAlmacen { get; set; }
        public string OperadorChofer { get; set; }
        public string Empresa { get; set; }
    }
}
