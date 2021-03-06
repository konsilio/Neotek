﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class AutoconsumoDTO
    {
        public short IdCAlmacenGasSalida { get; set; }

        public short IdCAlmacenGasEntrada { get; set; }

        public string ClaveOperacion { get; set; }

        public List<string> Imagenes { get; set; }

        public int CantidadFotos { get; set; }

        public decimal PorcentajeMedidor { get; set; }

        public decimal P5000Salida { get; set; }

        public DateTime FechaRegistro { get; set; }

        public short IdTipoMedidor { get; set; }

        public DateTime FechaAplicacion { get; set; }

    }
}
