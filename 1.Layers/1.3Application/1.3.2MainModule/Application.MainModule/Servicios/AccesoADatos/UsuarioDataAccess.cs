using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using Exceptions.MainModule.Validaciones;
using Exceptions.MainModule;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class UsuarioDataAccess
    {
        private SagasDataUow uow;

        public UsuarioDataAccess()
        {
            uow = new SagasDataUow();
        }

        public Usuario Buscar(int idUsuario)
        {
            return IntegrarRoles(uow.Repository<Usuario>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo));
        }

        public Usuario Buscar(short idEmpresa, int idUsuario)
        {
            return IntegrarRoles(uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo));
        }
        public Usuario Buscar(short idEmpresa, string NombreUsuario, string password)
        {
            return IntegrarRoles(uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.NombreUsuario.Equals(NombreUsuario)
                                                         && x.Password.Equals(password)
                                                         && x.Activo));
        }
        public List<Usuario> Buscar(short idEmpresa)
        {
            return IntegrarRoles(uow.Repository<Usuario>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.EsAdministracionCentral.Equals(false)
                                                         && x.Activo));
        }
        public List<Usuario> BuscarTodos()
        {
            return IntegrarRoles(uow.Repository<Usuario>().Get(x => x.Activo));
        }
        public RespuestaDto Actualizar(Usuario usuario)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Usuario>().Update(usuario);
                    uow.SaveChanges();
                    _respuesta.Id = usuario.IdUsuario;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Usuario"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(UsuarioRol usuarioRol)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UsuarioRol>().Insert(usuarioRol);
                    uow.SaveChanges();
                    _respuesta.Id = usuarioRol.IdUsuario;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Usuario");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Eliminar(UsuarioRol usuarioRol)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UsuarioRol>().Delete(usuarioRol);
                    uow.SaveChanges();                                    
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Desasignar el Rol al Usuario");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(Usuario user)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Usuario>().Insert(user);
                    uow.SaveChanges();
                    _respuesta.Id = user.IdUsuario;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Usuario");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        private Usuario IntegrarRoles(Usuario usuario)
        {
            if(usuario != null)
                if(usuario.UsuarioRoles != null && usuario.UsuarioRoles.Count > 0)
                    usuario.Roles = usuario.UsuarioRoles.Select(x => x.Role).ToList();

            return usuario;
        }
        private List<Usuario> IntegrarRoles(IEnumerable<Usuario> usuarios)
        {
            return usuarios.Select(x => IntegrarRoles(x)).ToList();
        }
    }
}
