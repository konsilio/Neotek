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
    public class AlmacenSalidaProductoDataAccess
    {
        private SagasDataUow uow;
        public AlmacenSalidaProductoDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(AlmacenSalidaProducto _salida, Sagas.MainModule.Entidades.Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenSalidaProducto>().Insert(_salida);
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(_alm);
                    uow.SaveChanges();
                  
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del AlmacenSalidaProducto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(AlmacenSalidaProducto _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenSalidaProducto>().Update(_pro);
                    uow.SaveChanges();

                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del AlmacenSalidaProducto"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<AlmacenSalidaProducto> BuscarTodos()
        {
            return uow.Repository<AlmacenSalidaProducto>().GetAll().ToList();
        }
        public List<AlmacenSalidaProducto> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<AlmacenSalidaProducto>().Get(x => x.Almacen.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public AlmacenSalidaProducto Buscar(int IdRequisicion, short Orden)
        {
            return uow.Repository<AlmacenSalidaProducto>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion)
                                                         && x.Orden.Equals(Orden));
        }

    }
}
