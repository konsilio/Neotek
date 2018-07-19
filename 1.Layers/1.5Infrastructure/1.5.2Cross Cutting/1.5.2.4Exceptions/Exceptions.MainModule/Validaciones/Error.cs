using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.MainModule.Validaciones
{
    public static class Error
    {
        public const string S0001 = "El campo '{0}' es obligatorio.";
        public const string S0002 = "En campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string S0003 = "El Usuario y la contraseña no coinciden";
    }
}
