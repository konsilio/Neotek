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
    public class RecargaCombustibleDataAccess
    {

        private SagasDataUow uow;

        public RecargaCombustibleDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(DetalleRecargaCombustible entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleRecargaCombustible>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleRecargaComb;
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
        public RespuestaDto Actualizar(DetalleRecargaCombustible entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleRecargaCombustible>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleRecargaComb;
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

        public RespuestaDto Borrar(DetalleRecargaCombustible entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<DetalleRecargaCombustible>().Delete(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_DetalleRecargaComb;
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
        public DetalleRecargaCombustible Obtener(int id)
        {
            return uow.Repository<DetalleRecargaCombustible>().GetSingle(x => x.Id_DetalleRecargaComb.Equals(id));
        }
        public List<DetalleRecargaCombustible> ObtenerPorVehiculo(int id)
        {
            return uow.Repository<DetalleRecargaCombustible>().Get(x => x.Id_Vehiculo.Equals(id)).ToList();
        }
        public List<DetalleRecargaCombustible> Obtener(DateTime fi, DateTime ff)
        {
            return uow.Repository<DetalleRecargaCombustible>().Get(x => x.FechaRecarga > fi && x.FechaRecarga < ff).ToList();
        }
        public List<DetalleRecargaCombustible> Obtener()
        {
            return uow.Repository<DetalleRecargaCombustible>().GetAll().ToList();
        }
    }
}