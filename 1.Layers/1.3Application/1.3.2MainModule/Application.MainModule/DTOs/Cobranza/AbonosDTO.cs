﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Cobranza
{

    public class AbonosDTO
    {
        public int IdAbono { get; set; }
        public int IdCargo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public System.DateTime FechaAbono { get; set; }
        public decimal MontoAbono { get; set; }
        public byte IdFormaPago { get; set; }
        public string FolioBancario { get; set; }
    }
}