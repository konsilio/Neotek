using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class CuentaContableServicio
    {
        public static List<CuentaContable> buscarCuentasContables(int idEmpresa)
        {
            return new CuentaContableDataAccess().BuscarCuentasContables(idEmpresa);
        }
        public static CuentaContable ObtenerCuentaContable(int idCuentaContable)
        {
            return new CuentaContableDataAccess().Buscar(idCuentaContable);
        }       
        public static RespuestaDto ModificarCuentaContable(CuentaContable cc )
        {
            return new CuentaContableDataAccess().Actualizar(cc);
        }
        public static RespuestaDto RegistrarCuentaContable(CuentaContable cuenta)
        {
            return new CuentaContableDataAccess().Insertar(cuenta);
        }       
        public static List<CuentaContable> Obtener()
        {
            var empresa = new EmpresaDataAccess().Buscar(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CuentaContableDataAccess().BuscarTodos();
            else
                return new CuentaContableDataAccess().BuscarTodos(empresa.IdEmpresa);
        }
        public static CuentaContable Obtener(int IdCuentaContable)
        {
            return new CuentaContableDataAccess().Buscar(IdCuentaContable);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La Cuenta contable");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
