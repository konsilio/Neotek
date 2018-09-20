using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{

    public class Empresa 
    {
        public List<EmpresaDTO> Empresas { get; set; }
        //public string NombreComercial { get; set; }
    }
}