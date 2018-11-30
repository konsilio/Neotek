using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class CamionetaDto : AlmacenDto
    {
        public short IdCAlmacen { get; set; }
        public string Numero { get; set; }
        public decimal PorcentajeActual { get; set; }
        public decimal CantidadActualLt { get; set; }
        public decimal CantidadActualKg { get; set; }
        public MedidorDto Medidor { get; set; }
        //public List<CilindroDto> Cilindros { get; set; }
    }
}
