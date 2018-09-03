using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class CuentaContableDto
    {
        public int IdCuentaContable { get; set; }
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
