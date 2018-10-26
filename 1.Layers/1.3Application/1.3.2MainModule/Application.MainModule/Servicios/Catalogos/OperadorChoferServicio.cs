using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class OperadorChoferServicio
    {
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
    }
}
