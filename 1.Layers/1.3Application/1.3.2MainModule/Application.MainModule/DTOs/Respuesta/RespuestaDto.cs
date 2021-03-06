﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Respuesta
{
    public class RespuestaDto
    {
        public bool Exito { get; set; }
        public bool EsInsercion { get; set; }
        public bool EsActulizacion { get; set; }
        public string Mensaje { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool ModeloValido { get; set; }
        public List<string> MensajesError { get; set; }
        public string RedirigirUrl { get; set; }
    }
}
