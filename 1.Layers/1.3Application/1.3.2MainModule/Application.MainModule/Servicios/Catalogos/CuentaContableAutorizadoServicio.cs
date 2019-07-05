using Application.MainModule.AccesoADatos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
   public static class CuentaContableAutorizadoServicio
    {
        public static RespuestaDto RegistrarCuentaContableAutorizado(CuentaContableAutorizado provee)
        {
            return new CuentaContableAutorizadoDataAcces().Insertar(provee);
        }
        public static RespuestaDto ModificarCuentaContableAutorizado(CuentaContableAutorizado provee)
        {
            return new CuentaContableAutorizadoDataAcces().Actualizar(provee);
        }
        public static List<CuentaContableAutorizado> Obtener()
        {
            var empresa = new EmpresaDataAccess().Buscar(Seguridad.TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CuentaContableAutorizadoDataAcces().BuscarTodos();
            else
                return new CuentaContableAutorizadoDataAcces().BuscarTodos(empresa.IdEmpresa);
        }
        public static CuentaContableAutorizado Obtener(int id)
        {
            return new CuentaContableAutorizadoDataAcces().Buscar(id);
        }
        public static CuentaContableAutorizado Obtener(int id, DateTime fecha)
        {
            return new CuentaContableAutorizadoDataAcces().Buscar(id, fecha);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La Cuenta Contable");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
