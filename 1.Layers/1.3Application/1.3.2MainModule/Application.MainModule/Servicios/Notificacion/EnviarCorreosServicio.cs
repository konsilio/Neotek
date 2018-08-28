using Application.MainModule.AdaptadoresDTO;
using Application.MainModule.DTOs;
using Mail.MainModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Notificacion
{
    public static class EnviarCorreosServicio
    {        
        public static void Enviar(CorreoDto datos)
        {
            var _enviarCorreos = CorreoAdapter.FromDto(datos);
            _enviarCorreos.EnviarCorreo(true, true);
        }
    }
}
