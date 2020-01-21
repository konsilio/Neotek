using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class ControlAsistenciaDTO
    {
        public int IdControlAsistencia { get; set; }
        public int IdUsuario { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Coordenadas { get; set; }
        public short IdEmpresa { get; set; }
    }
}
