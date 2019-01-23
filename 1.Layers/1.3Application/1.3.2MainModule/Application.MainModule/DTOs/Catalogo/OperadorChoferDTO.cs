using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class OperadorChoferDTO
    {
        public int IdOperadorChofer { get; set; }
        public byte IdTipoOperadorChofer { get; set; }
        public short IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }
        //public virtual TipoOperadorChofer TipoOperadorChofer { get; set; }
        //public virtual Usuario Usuario { get; set; }
        // public virtual ICollection<PuntoVenta> PuntosVenta { get; set; }
        //public virtual Empresa Empresa { get; set; }
    }
}
