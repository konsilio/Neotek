using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.MainModule
{
    public class UsuarioAD
    {
        public bool Existe { get; set;}
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Puesto { get; set; }
        public string Area { get; set; }
        public string ResponsableInmedieato { get; set; }
        public string Mensaje { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
    }
}
