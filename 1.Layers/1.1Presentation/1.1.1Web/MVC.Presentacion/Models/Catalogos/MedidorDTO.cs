using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class MedidorDTO
    {
        public short IdTipoMedidor { get; set; }
        public string NombreTipoMedidor { get; set; }
        public byte CantidadFotografias { get; set; }
    }
}