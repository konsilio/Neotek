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
    
    public partial class RespuestaSatisfaccionPedido
    {
        public int IdRespuesta { get; set; }
        public int IdPedido { get; set; }
        public int IdPregunta { get; set; }
        public byte Respuesta { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Pedido Pedido { get; set; }
    }
}
