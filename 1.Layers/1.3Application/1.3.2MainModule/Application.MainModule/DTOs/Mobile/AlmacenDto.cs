using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class AlmacenDto
    {
        public short IdAlmacenGas { get; set; }
        public string NombreAlmacen { get; set; }
        public Nullable<decimal> PorcentajeMedidor { get; set; }
        public Nullable<decimal> CantidadP5000 { get; set; }
        public Nullable<short> IdTipoMedidor { get; set; }
        public List<CilindroDto> Cilindros { get; set; }
    }
}
