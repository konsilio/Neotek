using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CuentaContableAutorizadoDTO
    {
        public int IdCuentaContable { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal Autorizado { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}