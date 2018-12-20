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
    public static class IngresoServicio
    {
        public static RespuestaDto RegistrarEgreso(Ingreso entidad)
        {
            return new IngresoDataAccess().Insertar(entidad);
        }
        public static RespuestaDto ActualizarEgreso(Ingreso entidad)
        {
            return new IngresoDataAccess().Actualizar(entidad);
        }
        public static List<Ingreso> BuscarTodos()
        {
            return new IngresoDataAccess().BuscarTodos();
        }
        public static List<Ingreso> BuscarTodos(short IdEmpresa)
        {
            return new IngresoDataAccess().BuscarTodos(IdEmpresa);
        }
        public static List<Ingreso> BuscarTodos(DateTime FechaInicio, DateTime FechaFin)
        {
            return new IngresoDataAccess().BuscarTodos(FechaInicio, FechaFin);
        }
        public static List<Ingreso> BuscarPorCentroCosto(int IdCentroCosto)
        {
            return new IngresoDataAccess().BuscarPorCentroCosto(IdCentroCosto);
        }
        public static List<Ingreso> BuscarPorCuentaContable(int IdCuentaContable)
        {
            return new IngresoDataAccess().BuscarPoCuentaContable(IdCuentaContable);
        }
        public static Ingreso CrearIngresoPorVenta(VentaPuntoDeVenta Venta)
        {
            return new Ingreso()
            {
                Ticket = Venta.FolioVenta,
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                Monto = Venta.Total,
                Descripcion = IngresoGastoConst.IngresoVenta,
                FechaRegistro = DateTime.Now,
                EsRema = false,
            };
        }
        public static Ingreso CrearIngresoPorOrdenCompra(OrdenCompra Compra)
        {
            return new Ingreso()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdCentroCosto = Compra.IdCentroCosto,
                IdCuentaContable = Compra.IdCuentaContable,
                IdOrdenCompra = Compra.IdOrdenCompra,
                Ticket = Compra.NumOrdenCompra, 
                Descripcion = IngresoGastoConst.IngresoRema,
                FechaRegistro = DateTime.Now,
                EsRema = false,
            };
        }
    }
}
