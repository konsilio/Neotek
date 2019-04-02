﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class FacturacionDTO
    {
        public int IdCliente { get; set; }
        public string RFC { get; set; }
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaVenta { get; set; }
    }
}