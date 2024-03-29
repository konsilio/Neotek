﻿using System;

namespace Application.MainModule.DTOs.Mobile
{
    public class AnticipoDto
    {
        public short IdAnticipo { get; set; }
        public string Tiket { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string ClaveOperacion { get; set;}
        public short IdCAlmacenGas { get; set; }
        public decimal Total { get; set; }
        public string Recibe { get; set; }
        public DateTime FechaAnticipo { get; set; }
    }
}
