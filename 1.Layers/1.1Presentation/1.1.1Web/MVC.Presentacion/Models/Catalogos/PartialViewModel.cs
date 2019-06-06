using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class PartialViewModel

    {    
        public List<RolCompras> ListaRolesCom { get; set; }
        public List<RolDto> ListaRoles { get; set; }
        public List<RolRequsicion> ListaRequsicion { get; set; }
        public List<RolMovilCompra> ListaMovilCompra { get; set; }
        public List<RolMovilVenta> ListaMovilVenta { get; set; }
        public List<RolSistemaVenta> ListaSistemaVenta { get; set; }
        public List<RolTransporte> ListaTransporte{ get; set; }

    }
}