using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class NotificacionDTO
    {
        public List<string> UsuarioKey { get; set;  }
        public string AutorizacionKey { get; set; }
        public string TipoNotificacion { get; set; }
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public string Titulo { get; set; }
        public bool Exito { get; set; }
        public List<string> MensajesError { get; set; }
    }
}
