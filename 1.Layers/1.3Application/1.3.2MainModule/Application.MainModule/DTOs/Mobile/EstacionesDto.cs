using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class EstacionesDto : AlmacenDto
    {
        public MedidorDto Medidor { get; set; }

        public AnticiposEstacionDTO AnticiposEstacion { get; set; }

        public decimal P5000Final { get; set; }

        public decimal P5000Inicial { get; set; }

        public string NombrePipa { get; set; }

        
    }
}
