using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Application.MainModule.Servicios.AccesoADatos
{
    public class CFDIDataAccess
    {
        private SagasDataUow uow;

        public CFDIDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(CFDI entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CFDI>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_RelTF;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "registro del CFDI");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(CFDI entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CFDI>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_RelTF;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del punto de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(List<CFDI> entidades)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var entidad in entidades)
                    {
                        uow.Repository<CFDI>().Update(entidad);
                    }
                    uow.SaveChanges();
                    _respuesta.Id = entidades[0].Id_RelTF;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del punto de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Borrar(CFDI entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CFDI>().Delete(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_RelTF;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del punto de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public CFDI Obtener(int id)
        {
            return uow.Repository<CFDI>().GetSingle(x => x.Id_RelTF.Equals(id) && x.UUID.Trim() != (string.Empty));
        }
        public CFDI Obtener(string Id_FolioVenta)
        {
            return uow.Repository<CFDI>().GetSingle(x => x.Id_FolioVenta.Equals(Id_FolioVenta));
        }
        public List<CFDI> Obtener()
        {
            return uow.Repository<CFDI>().Get(x => x.UUID.Trim() != (string.Empty)).ToList();
        }
    }
}
