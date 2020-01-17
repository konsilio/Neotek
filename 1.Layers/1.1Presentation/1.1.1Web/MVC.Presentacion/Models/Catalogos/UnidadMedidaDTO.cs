using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class UnidadMedidaDTO
    {
        public short IdUnidadMedida { get; set; }
        public short IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Acronimo { get; set; }
        public string Descripcion { get; set; }
    }
}