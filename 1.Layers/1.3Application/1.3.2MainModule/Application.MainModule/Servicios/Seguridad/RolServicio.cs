using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class RolServicio
    {
        public static List<Rol> ObtenerRoles(short idEmpresa)
        {
            return new RolDataAccess().Buscar(idEmpresa);
        }

        public static List<Rol> ObtenerRoles(Empresa empresa)
        {
            if (empresa != null)
                if (empresa.Roles != null && empresa.Roles.Count > 0)
                    return empresa.Roles.ToList();

            return ObtenerRoles(empresa.IdEmpresa);
        }

        public static Rol Obtener(UsuarioRol usrRol)
        {
            if (usrRol != null)
                if (usrRol.Role != null)
                    return usrRol.Role;

            return Obtener(usrRol.IdRol);
        }

        public static List<RolDto> ListaAllRoles()
        {
            List<RolDto> lRoles = AdaptadoresDTO.Seguridad.RolAdapter.ToDTORoles(new RolDataAccess().BuscarTodosRoles());
            return lRoles;
        }

        public static RespuestaDto AltaRol(Rol rol)
        {
            return new RolDataAccess().Insertar(rol);
        }

        public static Rol Obtener(short idRol)
        {
            return new RolDataAccess().BuscarIdRol(idRol);
        }

        public static RespuestaDto ExisteRol(Usuario usuario, Rol rol)
        {
            if (usuario.Roles.Contains(rol))
            {
                string mensaje = string.Format(Error.ContieneRol, "El usuario", rol.NombreRol);

                return new RespuestaDto()
                {
                    Exito = true,
                    Mensaje = mensaje,
                    MensajesError = new List<string>() { mensaje },
                };
            }

            return new RespuestaDto();
        }

        public static RespuestaDto Actualizar(Rol rol)
        {
            return new RolDataAccess().Actualizar(rol);
        }

        public static RespuestaDto Actualizar(List<Rol> rol)
        {
            return new RolDataAccess().Actualizar(rol);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Rol");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
