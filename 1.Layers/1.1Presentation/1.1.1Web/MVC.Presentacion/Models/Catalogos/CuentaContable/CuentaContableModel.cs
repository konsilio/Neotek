using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class CuentaContableModel : CuentaContableDTO
    {
        public List<CuentaContableDTO> CuentasContables { get; set; }
    }
}