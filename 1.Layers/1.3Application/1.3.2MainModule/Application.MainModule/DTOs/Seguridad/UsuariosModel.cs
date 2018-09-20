using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
   public class UsuariosModel : UsuarioDTO
    {
        public string Empresa { get; set; }
        public List<RolDto> Roles { get; set; }
        public List<UserRol> UsuarioRoles { get; set; }

    }
}
