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
    public class AlmacenDataAccess
    {
        private SagasDataUow uow;

        public AlmacenDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<Almacen> ListaProductosAlmacen(short idEmpresa)
        {
            return uow.Repository<Almacen>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<Almacen> ListaProductosAlmacenTodos()
        {
            return uow.Repository<Almacen>().GetAll().ToList();
        }
        public Almacen ProductoAlmacen(int idProducto, short idEmpresa)
        {
            return uow.Repository<Almacen>().GetSingle(x => x.IdProduto.Equals(idProducto) && x.IdEmpresa.Equals(idEmpresa));
        }
        public RespuestaDto ActualizarAlmacenSalida(Almacen _alm, AlmacenSalidaProducto _salida)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Almacen>().Update(_alm);
                    uow.Repository<AlmacenSalidaProducto>().Insert(_salida);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdAlmacen;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la salida de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto ActualizarAlmacenEntradas(Almacen _alm, AlmacenEntradaProducto _entrada)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Almacen>().Update(_alm);
                    uow.Repository<AlmacenEntradaProducto>().Insert(_entrada);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdAlmacen;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto ActualizarAlmacenEntradas(List<Almacen> _alm, List<Almacen> _almCrear, List<AlmacenEntradaProducto> _entrada, OrdenCompra oc, List<OrdenCompraProducto> ocp)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    //Almacen
                    foreach (var alm in _almCrear)
                        uow.Repository<Almacen>().Insert(alm);
                    foreach (var alm in _alm)
                        uow.Repository<Almacen>().Update(alm);
                    foreach (var entrada in _entrada)
                        uow.Repository<AlmacenEntradaProducto>().Insert(entrada);

                    //Orden de compra
                    foreach (var p in ocp)
                        uow.Repository<OrdenCompraProducto>().Update(p);
                    uow.Repository<OrdenCompra>().Update(oc);    

                    uow.SaveChanges();
                    _respuesta.Id = 0;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.EsInsercion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la Salida de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto ActualizarAlmacenSalidas(List<Almacen> _alm, List<AlmacenSalidaProducto> _entrada, Requisicion _requisicion, List<RequisicionProducto> _productos)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {                  
                    //Almacen
                    foreach (var alm in _alm)
                        uow.Repository<Almacen>().Update(alm);
                    foreach (var entrada in _entrada)
                        uow.Repository<AlmacenSalidaProducto>().Insert(entrada);
                    //Requisicion
                    uow.Repository<Requisicion>().Update(_requisicion);
                    foreach (var producto in _productos)
                        uow.Repository<RequisicionProducto>().Update(producto);

                    uow.SaveChanges();
                    _respuesta.Id = 0;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.EsInsercion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Id = _requisicion.IdRequisicion;
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto InsertarAlmacenEntradas(List<Almacen> _alm, List<AlmacenEntradaProducto> _entrada)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var alm in _alm)
                        uow.Repository<Almacen>().Insert(alm);
                    foreach (var entrada in _entrada)
                        uow.Repository<AlmacenEntradaProducto>().Insert(entrada);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Almacen>().Insert(_alm);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsInsercion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Almacen>().Update(_alm);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<EstacionCarburacion> ObtenerEstaciones(short idEmpresa)
        {
            return uow.Repository<EstacionCarburacion>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
    }
}
