﻿using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Cobranza
{
    public class AbonosModel
    {
        public int IdAbono { get; set; }
        public int IdCargo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaAbono { get; set; }
        public decimal MontoAbono { get; set; }
        public byte IdFormaPago { get; set; }
        public string FolioBancario { get; set; }
        public string FormaPago { get; set; }
        public string URLPdf { get; set; }
        public string URLXml { get; set; }
    }
}