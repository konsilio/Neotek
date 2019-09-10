using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.DTOs
{
    public class CuentaContableAutorizadoDTO
    {
       
        public int IdCuentaContableAutorizado { get; set; }
        public int IdCuentaContable { get; set; }
        public decimal Autorizado { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}
