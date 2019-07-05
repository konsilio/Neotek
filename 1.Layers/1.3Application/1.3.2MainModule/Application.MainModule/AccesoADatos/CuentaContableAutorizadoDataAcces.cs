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

namespace Application.MainModule.AccesoADatos
{
    public class CuentaContableAutorizadoDataAcces
    {
        private SagasDataUow uow;

        public CuentaContableAutorizadoDataAcces()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(CuentaContableAutorizado entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CuentaContableAutorizado>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdCuentaContable;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del centro de costo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(CuentaContableAutorizado entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CuentaContableAutorizado>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdCuentaContable;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del centro de costo"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<CuentaContableAutorizado> BuscarTodos()
        {
            return uow.Repository<CuentaContableAutorizado>().Get(x => x.CCuentaContable.Activo).ToList();
        }
        public List<CuentaContableAutorizado> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<CuentaContableAutorizado>().Get(x => x.CCuentaContable.IdEmpresa.Equals(idEmpresa))
                                                         .ToList();
        }
        public CuentaContableAutorizado Buscar(int id)
        {
            return uow.Repository<CuentaContableAutorizado>().GetSingle(x => x.IdCuentaContable.Equals(id));
        }
        public CuentaContableAutorizado Buscar(int id, DateTime date)
        {
            return uow.Repository<CuentaContableAutorizado>().GetSingle(x => x.IdCuentaContable.Equals(id) 
                                                                            && x.Fecha.Year.Equals(date.Year)
                                                                            && x.Fecha.Month.Equals(date.Month));
        }
    }
}
