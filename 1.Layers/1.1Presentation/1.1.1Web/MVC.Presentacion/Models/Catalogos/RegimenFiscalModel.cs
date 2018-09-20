using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class RegimenFiscalModel
    {
        public short IdRegimenFiscal { get; set; }
        public byte IdTipoPersona { get; set; }
        public string c_RegimenFiscal { get; set; }
        public string Descripcion { get; set; }
        public bool AplicaPersonaFisica { get; set; }
        public bool AplicaPersonaMoral { get; set; }
        public System.DateTime FechaInicioVigencia { get; set; }
        public Nullable<System.DateTime> FechaFinVigencia { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}