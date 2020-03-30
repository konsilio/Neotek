using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.UnitOfWork;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using Exceptions.MainModule;
using Application.MainModule.DTOs.Catalogo;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class EmpresaDataAccess
    {
        private SagasDataUow uow;

        public EmpresaDataAccess()
        {
            uow = new SagasDataUow();
        }

        public Empresa Buscar(short idEmpresa)
        {
            return uow.Repository<Empresa>().Get(x => x.IdEmpresa.Equals(idEmpresa)).FirstOrDefault();
        }

        public List<Empresa> BuscarTodos()
        {
            return uow.Repository<Empresa>().Get(x=> x.Activo).ToList();
        }

        public List<Empresa> BuscarTodos(bool conAC)
        {
            return uow.Repository<Empresa>().Get(x => x.EsAdministracionCentral.Equals(conAC)).ToList();
        }

        public RespuestaDto Insertar(Empresa _emp)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Empresa>().Insert(_emp);
                    uow.SaveChanges();
                    _respuesta.Id = _emp.IdEmpresa;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la Empresa");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(Empresa _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Empresa>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEmpresa;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la Empresa"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Eliminar(short id)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Empresa>().Delete(id);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0009, "de la Empresa");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        /// <summary>
        /// Retorna el listado de usuario por medio del id de empresa
        /// </summary>
        /// <param name="empresa">Entidad de tipo empresa con los datos de este</param>
        /// <returns>Lista de usuarios que estan en la empresas</returns>
        public List<Usuario> GetUsuariosEmpresa(Empresa empresa)
        {
            if (empresa != null)
                return uow.Repository<Usuario>().Get(
                    x => x.IdEmpresa.Equals(empresa.IdEmpresa)
                    && x.Activo
                    ).ToList();
            else
                return null;
        }
    }
}
