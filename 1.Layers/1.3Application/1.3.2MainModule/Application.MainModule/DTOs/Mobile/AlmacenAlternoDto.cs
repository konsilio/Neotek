using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class AlmacenAlternoDto
    {
        public short IdCAlmacen { get; set; }
        public short IdAlmacenGas { get; set; }
        public MedidorDto Medidor { get; set; }
        public string NombreAlmacen { get; set; }
        public decimal P5000Actual { get; set; }
        public decimal PorcentajeActual { get; set; }
        public decimal CapacidadTanqueLt { get; set; }
        public decimal CantidadActualLt { get; set; }
        public decimal CapacidadTanqueKg { get; set; }
        public decimal CantidadActualKg { get; set; }
    }
}
