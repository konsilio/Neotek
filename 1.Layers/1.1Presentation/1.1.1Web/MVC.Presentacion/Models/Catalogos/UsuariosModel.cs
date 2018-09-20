using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class UsuariosModel : UsuarioDTO
    {
        public string Empresa { get; set; }
        public List<RolDto> Roles { get; set; }
    }
}