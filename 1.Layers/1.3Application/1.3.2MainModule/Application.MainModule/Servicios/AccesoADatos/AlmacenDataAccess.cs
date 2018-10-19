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
    public class AlmacenDataAccess
    {
        private SagasDataUow uow;

        public AlmacenDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<Sagas.MainModule.Entidades.Almacen> ListaProductosAlmacen(short idEmpresa)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Almacen>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<Sagas.MainModule.Entidades.Almacen> ListaProductosAlmacenTodos()
        {
            return uow.Repository<Sagas.MainModule.Entidades.Almacen>().GetAll().ToList();
        }
        public Sagas.MainModule.Entidades.Almacen ProductoAlmacen(int idProducto, short idEmpresa)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Almacen>().GetSingle(x => x.IdProduto.Equals(idProducto) && x.IdEmpresa.Equals(idEmpresa));
        }
        public RespuestaDto ActualizarAlmacenSalida(Sagas.MainModule.Entidades.Almacen _alm, AlmacenSalidaProducto _salida)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(_alm);
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
        public RespuestaDto ActualizarAlmacenEntradas(Sagas.MainModule.Entidades.Almacen _alm, AlmacenEntradaProducto _entrada)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(_alm);
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
        public RespuestaDto ActualizarAlmacenEntradas(List<Sagas.MainModule.Entidades.Almacen> _alm, List<Sagas.MainModule.Entidades.Almacen> _almCrear, List<AlmacenEntradaProducto> _entrada)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var alm in _almCrear)
                        uow.Repository<Sagas.MainModule.Entidades.Almacen>().Insert(alm);
                    foreach (var alm in _alm)
                        uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(alm);
                    foreach (var entrada in _entrada)
                        uow.Repository<AlmacenEntradaProducto>().Insert(entrada);
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
        public RespuestaDto ActualizarAlmacenSalidas(List<Sagas.MainModule.Entidades.Almacen> _alm, List<AlmacenSalidaProducto> _entrada, Sagas.MainModule.Entidades.Requisicion _requisicion, List<RequisicionProducto> _productos)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {                  
                    //Almacen
                    foreach (var alm in _alm)
                        uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(alm);
                    foreach (var entrada in _entrada)
                        uow.Repository<AlmacenSalidaProducto>().Insert(entrada);
                    //Requisicion
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Update(_requisicion);
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
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.A0001, "de la entrada de producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto InsertarAlmacenEntradas(List<Sagas.MainModule.Entidades.Almacen> _alm, List<AlmacenEntradaProducto> _entrada)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var alm in _alm)
                        uow.Repository<Sagas.MainModule.Entidades.Almacen>().Insert(alm);
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
        public RespuestaDto Insertar(Sagas.MainModule.Entidades.Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Insert(_alm);
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
        public RespuestaDto Actualizar(Sagas.MainModule.Entidades.Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(_alm);
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
    }
}
