using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Pedidos
{
   public class EncuestaDto
    {
        public int IdRespuesta { get; set; }
        public int IdPedido { get; set; }
        public int IdPregunta { get; set; }
        public string Respuesta { get; set; }
        public bool Activo { get; set; }
    }
}
