using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class OperadorChoferServicio
    {
        public static RespuestaDto Insertar(OperadorChofer oc)
        {
            return new OperadorChoferDataAccess().Insertar(oc);
        }
        public static OperadorChofer CrearParaPipa()
        {
            return new OperadorChofer()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdTipoOperadorChofer = TipoOperadorChoferEnum.Chofer,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
        }
        public static OperadorChofer CrearParaCamioneta()
        {
            return new OperadorChofer()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdTipoOperadorChofer = TipoOperadorChoferEnum.Chofer,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
        }
        public static OperadorChofer CrearParaUtilitario()
        {
            return new OperadorChofer()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdTipoOperadorChofer = TipoOperadorChoferEnum.Operador,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
        }

        public static OperadorChofer Obtener(int idOperadorChofer)
        {
            return new OperadorChoferDataAccess().Buscar(idOperadorChofer);
        }
        public static OperadorChofer ObtenerPorUsuario(int idUsuario)
        {
            return new OperadorChoferDataAccess().BuscarPorUsuario(idUsuario);
        }
        public static List<OperadorChofer> ObtenerPorEmpresa(short idEmpresa)
        {
            return new OperadorChoferDataAccess().BuscarTodos(idEmpresa);
        }
        public static OperadorChofer ObtenerPorUsuarioAplicacion()
        {
            return new OperadorChoferDataAccess().BuscarPorUsuario(TokenServicio.ObtenerIdUsuario());
        }
        public static OperadorChofer Obtener(UnidadAlmacenGas unidad)
        {
            PuntoVenta puntoVenta;
            if (unidad.PuntosVenta != null && unidad.PuntosVenta.Count > 0)
                puntoVenta = unidad.PuntosVenta.FirstOrDefault();
            else
                puntoVenta = PuntoVentaServicio.Obtener(unidad);
            if (puntoVenta != null)
            {
                if (puntoVenta.OperadorChofer != null)
                    return puntoVenta.OperadorChofer;
                else
                    return Obtener(puntoVenta.IdOperadorChofer);
            }
            return null;
        }
        public static string ObtenerNombreCompleto(UnidadAlmacenGas unidad)
        {
            var operador = Obtener(unidad);
            return operador != null ? UsuarioServicio.ObtenerNombreCompleto(operador) : "Nombre no disponible";
        }
        public static string ObtenerNombreCompleto(DetalleRecargaCombustible dto)
        {

            UnidadAlmacenGas unidad = new UnidadAlmacenGas();
            if (dto.EsCamioneta)
                unidad = AlmacenGasServicio.ObtenerPorCamioneta(dto.Id_Vehiculo);
            if (dto.EsPipa)
                unidad = AlmacenGasServicio.ObtenerPorPipa(dto.Id_Vehiculo);
            if (!dto.EsUtilitario)
            {
                if (unidad != null)
                {
                    var operador = Obtener(unidad);
                    return operador != null ? UsuarioServicio.ObtenerNombreCompleto(operador) : "Nombre no disponible";
                }
                else
                    return "Vehiculo no asignado";
            }
            else
            {
                var operador = Obtener(AsignacionUtilitarioServicio.BuscarPorUtilitario(dto.Id_Vehiculo).IdChoferOperador);
                return operador != null ? UsuarioServicio.ObtenerNombreCompleto(operador) : "Nombre no disponible";
            }
        }
    }
}
