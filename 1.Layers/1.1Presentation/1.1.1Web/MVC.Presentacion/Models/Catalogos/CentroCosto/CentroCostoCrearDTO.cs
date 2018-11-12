using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Presentacion.Models.Catalogos
{
    public class CentroCostoCrearDTO
    { 
        public short IdEmpresa { get; set; }
        public byte IdTipoCentroCosto { get; set; }
        public Nullable<int> IdEquipoTransporte { get; set; }
        public Nullable<int> IdVehiculoUtilitario { get; set; }       
        public Nullable<short> IdCAlmacenGas { get; set; }        
        public Nullable<int> IdEstacionCarburacion { get; set; }
        public Nullable<int> IdCamioneta { get; set; }     
        public Nullable<int> IdPipa { get; set; }        
        public Nullable<int> IdCilindro { get; set; }      
        public string Numero { get; set; }        
        public string Descripcion { get; set; }
    }
}