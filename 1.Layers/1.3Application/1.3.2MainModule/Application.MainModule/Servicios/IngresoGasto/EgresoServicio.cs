using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.IngresoGasto
{
    public static class EgresoServicio
    {
        public static RespuestaDto RegistrarEgreso(Egreso entidad)
        {
            return new EgresoDataAccess().Insertar(entidad);
        }
        public static RespuestaDto ActualizarEgreso(Egreso entidad)
        {
            return new EgresoDataAccess().Actualizar(entidad);
        }
        public static List<Egreso> BuscarTodos()
        {
            return new EgresoDataAccess().BuscarTodos();
        }
        public static List<Egreso> BuscarTodos(short IdEmpresa)
        {
            return new EgresoDataAccess().BuscarTodos(IdEmpresa);
        }
        public static List<Egreso> BuscarTodos(DateTime FechaInicio, DateTime FechaFin)
        {
            return new EgresoDataAccess().BuscarTodos(FechaInicio, FechaFin);
        }
        public static List<Egreso> BuscarPorCentroCosto(int IdCentroCosto)
        {
            return new EgresoDataAccess().BuscarPorCentroCosto(IdCentroCosto);
        }
        public static List<Egreso> BuscarPorCuentaContable(int IdCuentaContable)
        {
            return new EgresoDataAccess().BuscarPoCuentaContable(IdCuentaContable);
        }
        public static Egreso CrearIngresoPorOrdenCompra(OrdenCompra Compra)
        {
            return new Egreso()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdCentroCosto = Compra.IdCentroCosto,
                IdCuentaContable = Compra.IdCuentaContable,
                IdTipoEgreso = 1,
                //IdOrdenCompra = Compra.IdOrdenCompra,
                //Ticket = Compra.NumOrdenCompra,
                Descripcion = IngresoGastoConst.EgresoCompra,
                FechaRegistro = DateTime.Now,
                EsExterno = true,
            };
        }
    }
}
