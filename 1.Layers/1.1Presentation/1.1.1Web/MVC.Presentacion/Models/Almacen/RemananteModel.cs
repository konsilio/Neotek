using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Almacen
{
    public class RemanenteModel
    {
        public short IdEmpresa { get; set; }
        public int IdTipo { get; set; }
        public int IdPuntoVenta { get; set; }
        public DateTime Fecha { get; set; }
    }
}