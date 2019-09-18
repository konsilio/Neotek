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
    public class MantenimientoDetDataAccess
    {
        private SagasDataUow uow;

        public MantenimientoDetDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(DetalleMantenimiento entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleMantenimiento>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleMtto;
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
        public RespuestaDto Insertar(DetalleMantenimiento entidad, Egreso egreso)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleMantenimiento>().Insert(entidad);
                    uow.Repository<Egreso>().Insert(egreso);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleMtto;
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
        public RespuestaDto Actualizar(DetalleMantenimiento entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleMantenimiento>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleMtto;
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
        public RespuestaDto Borrar(DetalleMantenimiento entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleMantenimiento>().Delete(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleMtto;
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
        public DetalleMantenimiento Obtener(int id)
        {
            return uow.Repository<DetalleMantenimiento>().GetSingle(x => x.Id_DetalleMtto.Equals(id));
        }
        public List<DetalleMantenimiento> Obtener()
        {
            return uow.Repository<DetalleMantenimiento>().GetAll().ToList();
        }
        public List<DetalleMantenimiento> Obtener(DateTime fi, DateTime ff)
        {
            return uow.Repository<DetalleMantenimiento>().Get(x => x.FechaMtto > fi && x.FechaMtto < ff).ToList();
        }
    }
}