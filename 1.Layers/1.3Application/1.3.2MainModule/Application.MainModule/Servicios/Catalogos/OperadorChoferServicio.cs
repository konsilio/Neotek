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

        public static OperadorChofer ObtenerPorUsuarioAplicacion()
        {
            return new OperadorChoferDataAccess().BuscarPorUsuario(TokenServicio.ObtenerIdUsuario());
        }
    }
}
