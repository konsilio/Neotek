﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class EquipoTransporteDTO
    {
        public int IdEquipoTransporte { get; set; }
        public short IdEmpresa { get; set; }
        public Nullable<int> IdVehiculoUtilitario { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public Nullable<int> IdEstacion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }

        //Info Vehicular
        public string NumeroIdentificacion { get; set; }
        public string NumeroPlacas { get; set; }
        public string NumeroMotor { get; set; }
        public string Vehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int Cilindros { get; set; }
        public int IdTipoCombustible { get; set; }
        public int IdTipoUnidad { get; set; }
        public bool UnidadActiva { get; set; }
        public string AliasUnidad { get; set; }

        public int Id_DetalleEtransporte { get; set; }
        public int Id_Vehiculo { get; set; }
        public Nullable<bool> EsCamioneta { get; set; }
        public Nullable<bool> EsPipa { get; set; }
        public Nullable<bool> EsUtilitario { get; set; }
        public string NumIdVehicular { get; set; }
        public string Placas { get; set; }
        public string NumMotor { get; set; }
        public string DescVehiculo { get; set; }
       
        public short cilindros { get; set; }
       
    }
}
