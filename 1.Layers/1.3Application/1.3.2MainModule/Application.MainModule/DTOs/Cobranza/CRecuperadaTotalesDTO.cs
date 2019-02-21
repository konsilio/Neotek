using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Cobranza
{
    public class CRecuperadaTotalesDTO
    {
        public short IdEmpresa { get; set; }
        public decimal Total_Efectivo { get; set; }
        public decimal Total_Cheques { get; set; }
        public decimal Total_Transferencia { get; set; }
        public decimal Total { get; set; }

    }
}
