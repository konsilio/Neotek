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
    public class IngresoDataAccess
    {
        private SagasDataUow uow;
        public IngresoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Ingreso entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Ingreso>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdIngreso;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del ingreso");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Ingreso entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Ingreso>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdIngreso;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del ingreso"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<Ingreso> BuscarTodos()
        {
            return uow.Repository<Ingreso>().GetAll().ToList();
        }
        public List<Ingreso> BuscarTodos(short id)
        {
            return uow.Repository<Ingreso>().Get(x => x.IdEmpresa.Equals(id)).ToList();
        }
        public List<Ingreso> BuscarTodos(DateTime fi, DateTime ff)
        {
            return uow.Repository<Ingreso>().Get(x => x.FechaRegistro > fi && x.FechaRegistro < ff).ToList();
        }
        public List<Ingreso> BuscarPorCentroCosto(int id)
        {
            return uow.Repository<Ingreso>().Get(x => x.IdCentroCosto.Equals(id)).ToList();
        }
        public List<Ingreso> BuscarPoCuentaContable(int id)
        {
            return uow.Repository<Ingreso>().Get(x => x.IdCuentaContable.Equals(id)).ToList();
        }
        public Ingreso Buscar(string Ticket)
        {
            return uow.Repository<Ingreso>().GetSingle(x => x.Ticket.Equals(Ticket));
        }
    }
}
