using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class EquipoTransporteDTO
    {
        /*Parametros Búsqueda*/
        public string PlacasBusq { get; set; }
        public string AliasUnidadBusq { get; set; }
        /*Parametros Búsqueda*/
        public int IdEquipoTransporte { get; set; }
        public short IdEmpresa { get; set; }
        public Nullable<int> IdVehiculoUtilitario { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public Nullable<int> IdEstacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }

        //Info Vehicular
        public string NumIdVehicular { get; set; }//NumeroIdentificacion
        public string Placas { get; set; }
        public string NumMotor { get; set; }
        public string DescVehiculo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public short Cilindros { get; set; }
        public int IdTipoCombustible { get; set; }
        public int IdTipoUnidad { get; set; }
        public bool Activo { get; set; }
        public bool EsForaneo { get; set; }
        public string AliasUnidad { get; set; }

        public decimal CapacidadLts { get; set; }
        public decimal CapacidadKg { get; set; }
        public short IdTipoMedidor { get; set; }

        public int IdEquipoTransporteDetalle { get; set; }
        public Nullable<bool> EsCamioneta { get; set; }
        public Nullable<bool> EsPipa { get; set; }
        public Nullable<bool> EsUtilitario { get; set; }
        
    }
}