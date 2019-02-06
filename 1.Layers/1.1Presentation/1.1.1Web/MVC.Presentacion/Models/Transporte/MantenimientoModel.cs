using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Presentacion.Models
{
    public class MantenimientoModel
    {
        public int Id_Mantenimiento { get; set; }
        public string Mantenimiento { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    }
}
