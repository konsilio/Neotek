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
    
    public partial class Bitacora
    {
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
