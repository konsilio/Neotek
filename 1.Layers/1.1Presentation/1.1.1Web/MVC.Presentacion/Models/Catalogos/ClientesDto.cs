using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class ClientesDto : ClientesModel
    {
        public string Empresa { get; set; }
        public string TipoPersonaFiscal { get; set; }
        public string RegimenFiscal { get; set; }
    }
}