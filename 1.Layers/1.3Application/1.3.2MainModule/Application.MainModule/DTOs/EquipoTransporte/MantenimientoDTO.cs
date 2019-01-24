using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.EquipoTransporte
{
    public class MantenimientoDTO
    {
        public int IdMantenimiento { get; set; }
        public string Mantenimiento { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    }
}
