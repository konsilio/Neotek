using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class CombustibleDTO
    {

        public int Id_Combustible { get; set; }
        public string TipoCombustible { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    }
}
