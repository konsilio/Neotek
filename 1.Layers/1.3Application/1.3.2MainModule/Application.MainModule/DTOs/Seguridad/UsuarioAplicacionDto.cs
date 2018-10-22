using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
    public class UsuarioAplicacionDto
    {
        public bool autenticado { get; set; }
        public short IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public bool SuperUsuario { get; set; }
        public bool AdminCentral { get; set; }
        public string UrlImg { get; set; }
    }
}
