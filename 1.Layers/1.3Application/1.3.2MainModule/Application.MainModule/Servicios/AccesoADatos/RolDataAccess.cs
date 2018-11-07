using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class RolDataAccess
    {
        private SagasDataUow uow;

        public RolDataAccess()
        {
            uow = new SagasDataUow();
        }

        public Rol Buscar(short idEmpresa, short idRol)
        {
            return uow.Repository<Rol>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdRol.Equals(idRol)
                                                         && x.Activo);
        }
        public List<Rol> Buscar(short idEmpresa)
        {
            return uow.Repository<Rol>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo).ToList();
        }

        public Rol BuscarIdRol(short idRol)
        {
            return uow.Repository<Rol>().GetSingle(x => x.IdRol.Equals(idRol)
                                                         && x.Activo);
        }

        public UsuarioRol Buscar(short idRol, int usuario)
        {
            return uow.Repository<UsuarioRol>().GetSingle(x => x.IdRol.Equals(idRol)
                                                         && x.IdUsuario.Equals(usuario));
        }

        public List<Rol> BuscarTodosRoles()
        {
            return uow.Repository<Rol>().Get(x => x.Activo).ToList();
        }

        public RespuestaDto Insertar(Rol user)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Rol>().Insert(user);
                    uow.SaveChanges();
                    _respuesta.Id = user.IdRol;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Rol");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(Rol rol)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Rol>().Update(rol);
                    uow.SaveChanges();
                    _respuesta.Id = rol.IdRol;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Rol"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(List<Rol> rol)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _rol in rol)
                    {
                        uow.Repository<Sagas.MainModule.Entidades.Rol>().Update(_rol);
                    }
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Rol"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
