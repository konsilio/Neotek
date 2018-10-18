using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class EmpresaModificaConfig
    {
        public short IdEmpresa { get; set; }
        public decimal FactorLitrosAKilos { get; set; }
        public System.DateTime CierreInventario { get; set; }
        public byte InventarioSano { get; set; }
        public byte InventarioCrítico { get; set; }
        public decimal MaxRemaGaseraMensual { get; set; }
        public decimal FactorGalonALitros { get; set; }
        public decimal FactorCompraLitroAKilos { get; set; }
        public decimal FactorFleteGas { get; set; }
      
    }
}

