using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class CombustibleModel
    {
        public int Id_Combustible { get; set; }
        public string TipoCombustible { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionBusqueda { get; set; }        
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    }
}