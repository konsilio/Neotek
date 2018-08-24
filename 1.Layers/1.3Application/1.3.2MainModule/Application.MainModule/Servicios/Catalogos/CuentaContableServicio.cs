using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
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
        public static RespuestaDto ModificarCuentaContable(CuentaContable cc)
        {
            return new CuentaContableDataAccess().ActualizarCuentaContable(cc);
        }
    }
}
