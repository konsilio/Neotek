using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.UnitOfWork;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class RequisicionDataAccess
    {
        private SagasDataUow uow;

        public RequisicionDataAccess()
        {
            uow = new SagasDataUow();
        }
        public Sagas.MainModule.Entidades.Requisicion Buscar(int IdRequisicion)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion) && x.IdRequisicionEstatus.Equals(0));
        }
        public int BuscarUltimaRequi()
        {
            if (uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList().Count.Equals(0))
                return 0;
            else
                return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList().Last().IdRequisicion;
        }
        public Sagas.MainModule.Entidades.Requisicion BuscarPorNumeroRequisicion(string NumRequisicion)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetSingle(x => x.NumeroRequisicion.Equals(NumRequisicion));
        }
        public Sagas.MainModule.Entidades.Requisicion BuscarPorIdRequisicion(int IdRequisicion)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion));
        }
        public List<Sagas.MainModule.Entidades.Requisicion> BuscarTodas()
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList();
        }
        public RespuestaDto InsertarNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Insert(_req);
                    uow.SaveChanges();
                    _respuesta.Id = _req.IdRequisicion;
                    _respuesta.Mensaje = _req.NumeroRequisicion;
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.MensajesError = new List<string>();
                    _respuesta.MensajesError.Add(ex.Message);
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Update(_req);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.MensajesError.Add(string.Concat(Error.R0009, " | ", ex.Message));
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);

                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Sagas.MainModule.Entidades.Requisicion _req, List<RequisicionProducto> _reqProd)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Update(_req);
                    foreach (RequisicionProducto _prod in _reqProd)
                        uow.Repository<RequisicionProducto>().Update(_prod);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.MensajesError.Add(string.Concat(Error.R0009, " | ", ex.Message));
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }     
        public RequisicionProducto BuscarProducto (int idProd, int idRequ)
        {
            return uow.Repository<RequisicionProducto>().GetSingle(x => x.IdRequisicion.Equals(idRequ) && x.IdProducto.Equals(idProd));
        }
        public List<RequisicionProducto> BuscarProductoRequisicion(int idRequ)
        {
            return uow.Repository<RequisicionProducto>().Get(x => x.IdRequisicion.Equals(idRequ)).ToList();
        }
        public List<RequisicionEstatus> Estatus()
        {
            return uow.Repository<RequisicionEstatus>().GetAll().ToList();
        }
    }
}
