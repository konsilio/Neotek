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

        public Usuario Buscar(short idEmpresa, int idUsuario)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }
        public List<Usuario> Buscar(short idEmpresa)
        {
            return uow.Repository<Usuario>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.EsAdministracionCentral.Equals(false)
                                                         && x.Activo).ToList();
        }
        public List<Usuario> BuscarTodos()
        {
            return uow.Repository<Usuario>().GetAll().ToList();
        }

        public Usuario Buscar(short idEmpresa, string NombreUsuario, string password)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.NombreUsuario.Equals(NombreUsuario)
                                                         && x.Password.Equals(password)
                                                         && x.Activo);
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

        public Usuario Buscar(Empresa _empresa, int idUsuario)
        {
            if (_empresa.Usuario != null)
            {
                return _empresa.Usuario.SingleOrDefault(x => x.IdUsuario.Equals(idUsuario) && x.Activo);
            }
            else
            {
                return Buscar(_empresa.IdEmpresa ,idUsuario);
            }
        }
        public Usuario Buscar(int idUsuario)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }
    }
}
