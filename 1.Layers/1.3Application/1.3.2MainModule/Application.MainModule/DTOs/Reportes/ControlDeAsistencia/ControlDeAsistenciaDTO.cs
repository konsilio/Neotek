﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class ControlDeAsistenciaDTO
    {
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string PtoVenta { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }


        public List<VentasXPuntoVenta> ListPtoVenta { get; set; }
    }
}
