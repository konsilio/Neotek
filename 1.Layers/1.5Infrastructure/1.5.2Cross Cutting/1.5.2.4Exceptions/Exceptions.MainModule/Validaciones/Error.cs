using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.MainModule.Validaciones
{
    public static class Error
    {
        public const string NoExiste = "{0} no existe. Revise la información que esta solicitando.";

        public const string S0001 = "El campo '{0}' es obligatorio.";
        public const string S0002 = "En campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string S0003 = "El Usuario y la contraseña no coinciden";

        public const string R0001 = "Error al guardar la requisición";
        public const string R0002 = "El campo '{0}' es obligatorio.";
        public const string R0003 = "En campo '{0}' el valor debe ser mayor a 0.";
        public const string R0004 = "En campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string R0005 = "En campo '{0}' debe ser numerico y/o mayor a 0";
        public const string R0006 = "La lista de '{0}' debe contener al menos un elemento";
        public const string R0007 = "Debes agregar al menos un producto";
        public const string R0008 = "No se puede exceder la cantidad de {2}";
        public const string R0009 = "La requisición no se actualizo correctamente";
        public const string R0010 = "La busqueda no arrojo resultados";
        public const string R0011 = "La fecha de requisición no puede ser menor o igual a la fehca actual";
        public const string R0012 = "Verifique que los datos esten completos";

        public const string C0001 = "El '{0}' no coincide con los lineamientos establecidos por el Servicio de Administración Tributaria. Corrígelo por favor.";
        public const string C0002 = "No se logró hacer el registro {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string C0003 = "No se logró hacer la modificación {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string C0004 = "El campo {0} no tiene el formato de correo electrónico.";

        public const string M0001 = "No se encontraron {0} para mostrar.";
        public const string M0002 = "No se encontró {0} en el servidor.";

        public const string P0001 = "No cuentas con los permisos necesario para insertar {0}.";
        public const string P0002 = "No cuentas con los permisos necesario para modificar {0}.";
        public const string P0003 = "No cuentas con los permisos necesario para eliminar {0}.";
        public const string P0004 = "No cuentas con los permisos necesario para consultar {0}.";
    }
}
