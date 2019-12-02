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
        public const string SiExiste = "{0} ya existe. Revise la información.";
        public const string ContieneRol = "{0} contiene el Rol {1}. Por lo que no puede asignarse de nuevo.";
        public const string EstatusIncorrecto = "El estatus de {0} no corresponde al proceso que desea realizar";
        public const string PagoExistente = "Ya no puedes solicitar un pago para {0}, ya se realizdo uno";
        public const string NoEncontrado = "No se encontro info";

        public const string S0001 = "El campo '{0}' es obligatorio.";
        public const string S0002 = "El campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string S0003 = "El usuario y la contraseña no coinciden";
        public const string S0004 = "No se logró {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string S0005 = "El usuario no tiene acciones permitida en la aplicación";
        public const string S0007 = "No se a realizado lectura Inicial";
        public const string S0006 = "Ya se realizo la lectura final";

        public const string R0001 = "Error al guardar la requisición";
        public const string R0002 = "El campo '{0}' es obligatorio.";
        public const string R0003 = "El campo '{0}' el valor debe ser mayor a 0.";
        public const string R0004 = "El campo '{0}' requiere un mínimo de {2} caracteres y un máximo de {1}.";
        public const string R0005 = "El campo '{0}' debe ser numerico y/o mayor a 0";
        public const string R0006 = "La lista de '{0}' debe contener al menos un elemento";
        public const string R0007 = "Debes agregar al menos un producto";
        public const string R0008 = "No se puede exceder la cantidad de {2}";
        public const string R0009 = "La requisición no se actualizo correctamente";
        public const string R0010 = "La busqueda no arrojo resultados";
        public const string R0011 = "La fecha de requisición no puede ser menor o igual a la fehca actual";
        public const string R0012 = "Verifique que los datos esten completos";
        public const string R0013 = "Debes revisar todos los productos en el almacen";
        public const string R0014 = "Falta la opinion de almacen";


        public const string OC0001 = "La orden de compra no se actualizo correctamente";
        public const string OC0002 = "Debes asignar un Proveedor, una Cuenta contable y un precio como minimo a todos los producto";

        public const string C0001 = "El '{0}' no coincide con los lineamientos establecidos por el Servicio de Administración Tributaria. Corrígelo por favor.";
        public const string C0002 = "No se logró hacer el registro {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string C0003 = "No se logró hacer la modificación {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string C0004 = "El campo {0} no tiene el formato de correo electrónico.";
        public const string C0005 = "{0} que intenta registrar ya existe como {1}. Ingresa otro o modifica el existente.";
        public const string C0006 = "Un centro de costos no puede contener multiples unidades físicas. Unidades seleccionadas {0}. Deselecciones las unidades hasta dejar una o ninguna.";
        public const string C0007 = "{0} no exite. Verifique la información.";
        public const string C0008 = "El campo '{0}' acepta un valor mínimo de {2} y un máximo de {1}.";
        public const string C0009 = "No se logró hacer la eliminación {0}. Si este mensaje persiste comuniquese con el administrador del sistema";

        public const string CP0001 = "El almacén mínimo ('{0}') no puede ser mayor al almacén máximo ('{1}').";
        public const string CP0002 = "Si marcas'Activo de venta' o 'Es Gas'; no debe marcarse 'Es Transporte Gas'.";

        public const string M0001 = "No se encontraron {0} para mostrar.";
        public const string M0002 = "No se encontró {0} en el servidor.";

        public const string P0001 = "No cuentas con los permisos necesario para insertar {0}.";
        public const string P0002 = "No cuentas con los permisos necesario para modificar {0}.";
        public const string P0003 = "No cuentas con los permisos necesario para eliminar {0}.";
        public const string P0004 = "No cuentas con los permisos necesario para consultar {0}.";
        public const string P0005 = "No cuentas con los permisos necesario para generar {0}.";

        public const string A0001 = "No se logró hacer el registro {0}. Si este mensaje persiste comuniquese con el administrador del sistema";
        public const string A0002 = "La cantidad a entregar supera las existencias en almacén";

        public const string F0001 = "La fecha inicio es mayor que la fecha final";
        public const string F0002 = "Las fecha deben estar dentro del mes en curso";
        public const string F0003 = "Lagunos registros tuvieron algun error, no se puede completar la tarea";
    }
}
