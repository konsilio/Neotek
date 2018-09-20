using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class CentroCostoModel : CentroCostoDTO
    {
        public List<CentroCostoDTO> CentrosCostos { get; set; }
    }
}