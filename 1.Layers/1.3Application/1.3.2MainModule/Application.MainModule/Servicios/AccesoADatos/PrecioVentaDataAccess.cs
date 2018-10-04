﻿using Application.MainModule.DTOs.Respuesta;
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
    public class PrecioVentaDataAccess
    {
        private SagasDataUow uow;
        public PrecioVentaDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<PrecioVenta> BuscarTodos()
        {
            return uow.Repository<PrecioVenta>().Get(x => x.Activo).ToList();
        }
        public List<PrecioVenta> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<PrecioVenta>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
        public PrecioVenta BuscarIdPV(short IdPrecioVenta)
        {
            return uow.Repository<PrecioVenta>().GetSingle(x => x.IdPrecioVenta.Equals(IdPrecioVenta)
                                                         && x.Activo);
        }
        public PrecioVentaEstatus Buscar(byte IdPrecioVentaEstatus)
        {
            return uow.Repository<PrecioVentaEstatus>().GetSingle(x => x.IdPrecioVentaEstatus.Equals(IdPrecioVentaEstatus)
                                                         && x.Activo);
        }

        public List<PrecioVentaEstatus> Buscar()
        {
            return uow.Repository<PrecioVentaEstatus>().Get(x => x.Activo).ToList();
        }
        public RespuestaDto Insertar(List<PrecioVenta> cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var item in cte)
                    {
                        uow.Repository<PrecioVenta>().Insert(item);

                        uow.SaveChanges();
                        _respuesta.Id = item.IdPrecioVenta;
                        _respuesta.EsInsercion = true;
                        _respuesta.Exito = true;
                        _respuesta.ModeloValido = true;
                        _respuesta.Mensaje = Exito.OK;
                    }
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Precio de Venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(PrecioVenta _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.PrecioVenta>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdPrecioVenta;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del precio de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Eliminar(PrecioVenta cteL)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<PrecioVenta>().Delete(cteL);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Eliminar el precio de venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}