﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class PrecioVentaEstatusDTO
    {
        public byte IdPrecioVentaEstatus { get; set; }
        public string Descripción { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegsitro { get; set; }
    }
}

