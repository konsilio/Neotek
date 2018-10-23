using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class TraspasoDto
    {
        public short IdCAlmacenGasSalida { get; set; }
        public short IdCAlmacenGasEntrada { get; set; }
        public short IdTipoMedidorSalida { get; set; }
        public decimal P5000Salida { get; set; }
        public decimal P5000Entrada { get; set; }
        public decimal PorcentajeSalida { get; set; }
        public string ClaveOperacion { get; set; }
        public List<String> Imagenes { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
