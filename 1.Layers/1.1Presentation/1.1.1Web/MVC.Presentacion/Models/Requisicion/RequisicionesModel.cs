using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionesModel
    {
        public string FechaRequeridaDe { get; set; }
        public string FechaRequeridaA { get; set; }
        public string FechaCreacionDe { get; set; }
        public string FechaCreacionA { get; set; }
        public int IdEstatus { get; set; }
        public List<RequisicionDTO> Requisiciones { get; set; }
        public List<EmpresaDTO> Empresas { get; set; }
        public List<RequisicionEstatusDTO> Estatus { get; set; }
    }
}