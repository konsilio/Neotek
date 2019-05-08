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
    public class EgresoDataAccess
    {
        private SagasDataUow uow;
        public EgresoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Egreso entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Egreso>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdEgreso;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Egreso");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Egreso entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Egreso>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdEgreso;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Egreso"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public Egreso Buscar(int id)
        {
            return uow.Repository<Egreso>().GetSingle(x => x.IdEgreso.Equals(id));
        }
        public List<Egreso> BuscarTodos()
        {
            return uow.Repository<Egreso>().GetAll().ToList();
        }
        public List<Egreso> BuscarTodos(short id)
        {
            return uow.Repository<Egreso>().Get(x => x.IdEmpresa.Equals(id)).ToList();
        }
        public List<Egreso> BuscarPorCentroCosto(int id)
        {
            return uow.Repository<Egreso>().Get(x => x.IdCentroCosto.Equals(id)).ToList();
        }
        public List<Egreso> BuscarPoCuentaContable(int id)
        {
            return uow.Repository<Egreso>().Get(x => x.IdCuentaContable.Equals(id)).ToList();
        }
        public Egreso BuscarPorExterno(bool esExterno)
        {
            return uow.Repository<Egreso>().GetSingle(x => x.EsExterno.Equals(esExterno));
        }
        public List<Egreso> BuscarTodos(DateTime fi, DateTime ff)
        {
            return uow.Repository<Egreso>().Get(x => x.FechaRegistro > fi && x.FechaRegistro < ff).ToList();
        }
        public List<Egreso> BuscarTodos(DateTime periodo)
        {
            return uow.Repository<Egreso>().Get(x => x.FechaRegistro.Month.Equals(periodo.Month) && x.FechaRegistro.Year.Equals(periodo.Year)).ToList();
        }
    }
}
