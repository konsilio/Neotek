using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class PipaDto :AlmacenDto
    {
        public MedidorDto Medidor { get; set; }
        public decimal P5000Final { get; set; }
        public AnticiposEstacionDTO AnticiposEstacion { get; set; }
    }
}
