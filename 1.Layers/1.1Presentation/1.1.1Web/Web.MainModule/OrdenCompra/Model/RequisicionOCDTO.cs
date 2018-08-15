using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.MainModule.Requisicion.Model;

namespace Web.MainModule.OrdenCompra.Model 
{
    [Serializable]
    public class RequisicionOCDTO : RequisicionDTO
    {
        List<ProdcutoOC> Productos { get; set; }
    }
}