using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class PermisosServicio
    {
        public static RespuestaDto PuedeRegistrarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarProveedor);

            return EvaluarPermiso(rol, Error.P0001, "Proveedores");
        }

        public static RespuestaDto PuedeModificarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarProveedor);

            return EvaluarPermiso(rol, Error.P0002, "Proveedores");
        }

        public static RespuestaDto PuedeEliminarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarProveedor);

            return EvaluarPermiso(rol, Error.P0003, "Proveedores");
        }

        public static RespuestaDto PuedeConsultarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatConsultarProveedor);

            return EvaluarPermiso(rol, Error.P0004, "Proveedores");
        }

        private static RespuestaDto EvaluarPermiso(Rol rol, string error, string format = "")
        {
            if (rol == null)
            {
                string mensaje = string.Format(error, format);
                return new RespuestaDto()
                {
                    Mensaje = mensaje,
                    MensajesError = new List<string>() { mensaje }
                };
            }

            return new RespuestaDto() {Exito = true };
        }
    }
}
