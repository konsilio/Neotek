using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class CalibracionDto
    {
        public string ClaveOperacion { get; set; }
        public short IdCAlmacenGas { get; set; }
        public short IdTipoMedidor { get; set; }
        public decimal P5000 { get; set; }
        public decimal PorcentajeMedidor1 { get; set; }
        public decimal PorcentajeMedidor2 { get; set; }
        public List<string> Imagenes { get; set; }
    }
}
