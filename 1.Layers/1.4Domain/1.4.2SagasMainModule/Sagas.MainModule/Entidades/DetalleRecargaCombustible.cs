//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sagas.MainModule.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleRecargaCombustible
    {
        public int Id_DetalleRecargaComb { get; set; }
        public int Id_Vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }
        public int KilometrajeActual { get; set; }
        public decimal KilometrajeRecorrido { get; set; }
        public decimal LitrosRecargados { get; set; }
        public int IdTipoCombustible { get; set; }
        public System.DateTime FechaRecarga { get; set; }
    
        public virtual Camioneta CCamioneta { get; set; }
        public virtual CCombustible CCombustible { get; set; }
        public virtual Pipa CPipa { get; set; }
    }
}
