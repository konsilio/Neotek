using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Seguridad
    {
        public RespuestaAutenticacionDto Autenticacion(AutenticacionDto autenticacionDto)
        {
            return AutenticarServicio.AutenticarUsuario(autenticacionDto);
        }
        public RespuestaAutenticacionMobileDto AutenticacionMobile(AutenticacionDto autenticacionDto)
        {
            return AutenticarServicio.AutenticarUsuarioMobile(autenticacionDto);
          

        }
    }
}
