using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.UnitOfWork;
using Application.MainModule.AdaptadoresDTO.Catalogo;
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
            return uow.Repository<Empresa>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa));
        }

        public Empresa Buscar(int idEmpresa)
        {
            return uow.Repository<Empresa>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa));
        }

        public List<Empresa> BuscarTodos()
        {
            return uow.Repository<Empresa>().Get(x=> x.Activo).ToList();
        }

        public List<Empresa> BuscarTodos(bool conAC)
        {
            return uow.Repository<Empresa>().Get(x => x.EsAdministracionCentral.Equals(conAC)).ToList();
        }

        public RespuestaDto Insertar(Empresa _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Empresa>().Insert(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEmpresa;
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
    }
}
