﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Seguridad.Model
{
    public class RespuestaAutenticacionDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string token { get; set; }
    }
}