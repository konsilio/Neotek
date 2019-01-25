﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.EquipoTransporte
{
    public class MantenimientoDetalleDTO
    {
        public int Id_DetalleMtto { get; set; }
        public System.DateTime FechaMtto { get; set; }
        public int id_vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }
        public int Id_tipomtto { get; set; }
        public string DescripcionMtto { get; set; }
        public int Kilometraje_Actual { get; set; }
        public string NumeroOC { get; set; }
    }
}
