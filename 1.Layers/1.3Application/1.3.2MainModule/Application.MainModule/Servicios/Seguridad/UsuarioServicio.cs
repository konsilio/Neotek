using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.DTOs.Seguridad;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class UsuarioServicio
    {      
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            List<UsuarioDTO> lUsuarios = AdaptadoresDTO.Seguridad.UsuarioAdapter.ToDTO(new UsuarioDataAccess().Buscar(idEmpresa));
            return lUsuarios;
        }
        public static List<UsuarioDTO> ListaUsuarios()
        {
            List<UsuarioDTO> lUsuarios = AdaptadoresDTO.Seguridad.UsuarioAdapter.ToDTO(new UsuarioDataAccess().BuscarTodos());
            return lUsuarios;
        }
        public static List<UsuariosModel> ListaAllUsuarios()
        {
            List<UsuariosModel> lUsuarios = AdaptadoresDTO.Seguridad.UsuarioAdapter.ToDTOEmpresa(new UsuarioDataAccess().BuscarTodos());
            return lUsuarios;
        }
        public static Usuario Obtener(int idUsuario)
        {
            return new UsuarioDataAccess().Buscar(idUsuario);
        }

        public static string ObtenerNombreCompleto(Usuario usuario)
        {
            return string.Concat(usuario.Nombre, " ", usuario.Apellido1, " ", usuario.Apellido2);
        }

        public static string ObtenerNombreCompleto(OperadorChofer operador)
        {
            if (operador.Usuario != null)
                return ObtenerNombreCompleto(operador.Usuario);

            return ObtenerNombreCompleto(Obtener(operador.IdUsuario));
        }

        public static RespuestaDto Actualizar(Usuario usuario)
        {
            return new UsuarioDataAccess().Actualizar(usuario);
        }

        //ActualizarUsuarioRol
        public static RespuestaDto Insertar(UsuarioRol usRol)
        {
            return new UsuarioDataAccess().Insertar(usRol);
        }

        public static RespuestaDto AltaUsuario(Usuario user)
        {
            return new UsuarioDataAccess().Insertar(user);
        }

        //EliminarUsuarioRol
        public static RespuestaDto Eliminar(UsuarioRol usRol)
        {
            return new UsuarioDataAccess().Eliminar(usRol);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Usuario");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }


        public static RespuestaDto BorrarSuperAdmin()
        {
            string mensaje = string.Format(Error.P0003, "El SuperAdmin");

            return new RespuestaDto()
            {
                Exito = false,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
