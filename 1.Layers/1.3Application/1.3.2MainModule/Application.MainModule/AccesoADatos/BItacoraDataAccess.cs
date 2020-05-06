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

namespace Application.MainModule.Servicios
{
    public class BitacoraDataAcces
    {

        private SagasDataUow uow;

        public BitacoraDataAcces()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Bitacora _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Bitacora>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.Id;
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
        public RespuestaDto Insertar(List<Bitacora> _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var item in _cc)
                    {
                        uow.Repository<Bitacora>().Insert(item);
                    }

                    uow.SaveChanges();
                    _respuesta.Id = _cc[0].Id;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del cargo " + _cc[0].Id);
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Bitacora _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Bitacora>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.Id;
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
        public RespuestaDto Actualizar(Cargo _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Cargo>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCargo;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del cargo"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<Bitacora> BuscarTodos()
        {
            return uow.Repository<Bitacora>().GetAll().ToList();
        }
        public Bitacora BuscarBitacora(int id)
        {
            return uow.Repository<Bitacora>().GetSingle(x => x.Id.Equals(id));
        }       
    }
}
