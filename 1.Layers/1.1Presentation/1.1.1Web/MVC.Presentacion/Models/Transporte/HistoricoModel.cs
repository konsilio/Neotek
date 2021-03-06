﻿using MVC.Presentacion.Models.Catalogos;
using System;

namespace MVC.Presentacion.Models
{
    public class HistoricoModel
    {
        public int Id_DetalleMtto { get; set; }
        public DateTime FechaMtto { get; set; }
        public int id_vehiculo { get; set; }
        public string Vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }
        public int Id_tipomtto { get; set; } 
        public string DescripcionMtto { get; set; }
        public int Kilometraje_Actual { get; set; }
        public string NumeroOC { get; set; }
        public EquipoTransporteDTO _Vehiculo {get; set;} 
    }
}
