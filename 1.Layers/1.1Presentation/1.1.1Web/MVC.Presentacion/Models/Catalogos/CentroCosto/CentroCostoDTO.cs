using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    [Serializable]
    public class CentroCostoDTO
    {
      
        public byte IdCentroCosto { get; set; }
        
        public short IdEmpresa { get; set; }

        public string Empresa { get; set; }

        public byte IdTipoCentroCosto { get; set; }

        public string TipoCentroCosto { get; set; }

        public int IdEquipoTransporte { get; set; }

        public int IdVehiculoUtilitario { get; set; }

        public short IdCAlmacenGas { get; set; }

        public int IdEstacionCarburacion { get; set; }

        public int IdCamioneta { get; set; }

        public int IdPipa { get; set; }

        public int IdCilindro { get; set; }

        public string Numero { get; set; }

        public string Descripcion { get; set; }

        
        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}