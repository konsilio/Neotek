using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class UsuariosModel : UsuarioDTO
    {
        public string Empresa { get; set; }      
        public List<RolDto> Roles { get; set; }

    }
}
