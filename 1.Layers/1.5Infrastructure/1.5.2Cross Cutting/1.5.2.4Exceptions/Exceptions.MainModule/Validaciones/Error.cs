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

        public const string R0001 = "Error al guardar la requisicion";
        public const string R0002 = "El campo '{0}' es obligatorio.";
        public const string R0003 = "En campo '{0}' el valor debe ser mayor a 0.";
        public const string R0004 = "En campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string R0005 = "En campo '{0}' debe ser numerico y/o mayor a 0";
        public const string R0006 = "La lista de '{0}' debe contener al menos un elemento";
        public const string R0007 = "Debes agregar al menos un producto";
        public const string R0008 = "No se puede exceder la cantidad de {2}";
        public const string R0009 = "La requiscion no se actualizo correctamente";

        public const string M0001 = "No se encontraron {0} para mostrar.";
    }
}
