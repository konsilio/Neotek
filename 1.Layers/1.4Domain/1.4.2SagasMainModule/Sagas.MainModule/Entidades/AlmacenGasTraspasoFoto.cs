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
    
    public partial class AlmacenGasTraspasoFoto
    {
        public short IdCAlmacenGasSalida { get; set; }
        public short IdCAlmacenGasEntrada { get; set; }
        public int IdOrden { get; set; }
        public short IdImagenDe { get; set; }
        public string UrlImagen { get; set; }
        public string PathImagen { get; set; }
        public string CadenaBase64 { get; set; }
    
        public virtual AlmacenGasTraspaso Traspaso { get; set; }
    }
}
