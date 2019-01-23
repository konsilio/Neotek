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
        public bool Pregunta1Val1 { get; set; }
        public bool Pregunta1Val2 { get; set; }
        public bool Pregunta1Val3 { get; set; }
        public bool Pregunta1Val4 { get; set; }
        public bool Pregunta1Val5 { get; set; }
        public bool Pregunta2Val1 { get; set; }
        public bool Pregunta2Val2 { get; set; }
        public bool Pregunta2Val3 { get; set; }
        public bool Pregunta2Val4 { get; set; }
        public bool Pregunta2Val5 { get; set; }
    }
}
