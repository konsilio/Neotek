using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Security.MainModule.Token_Service;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.AccesoADatos;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class UsuarioAplicacionServicio
    {
        public static Usuario Obtener()
        {
            return new UsuarioDataAccess().Buscar(TokenServicio.ObtenerIdUsuario());
        }
    }
}
