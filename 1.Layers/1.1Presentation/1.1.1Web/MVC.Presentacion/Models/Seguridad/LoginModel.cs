using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Seguridad
{
    public class LoginModel : AutenticacionDTO
    {
        public List<EmpresaDTO> Empresas { get; set; }
        public RespuestaAutenticacionDto Respuesta { get; set; }
    }
}