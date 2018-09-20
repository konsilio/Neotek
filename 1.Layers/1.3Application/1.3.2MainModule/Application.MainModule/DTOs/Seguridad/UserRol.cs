using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
   public class UserRol
    {

        public int IdUsuario { get; set; }
        public short IdRol { get; set; }
        public string Descripcion { get; set; }

        public virtual UsuariosModel Usuarios { get; set; }
        public virtual RolDto Roles { get; set; }
    }
}
