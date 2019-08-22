using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepCuentaPorPagarDTO
    {
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContable { get; set; }
        public double SaldoPasivo { get; set; }
        public double SaldoPagado { get; set; }
        public double SaldoInsoluto { get; set; }

    }
}
