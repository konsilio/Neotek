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
    
    public partial class PuntoVenta
    {
        public int IdPuntoVenta { get; set; }
        public short IdEmpresa { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int IdOperadorChofer { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual UnidadAlmacenGas UnidadesAlmacen { get; set; }
        public virtual OperadorChofer OperadorChofer { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
