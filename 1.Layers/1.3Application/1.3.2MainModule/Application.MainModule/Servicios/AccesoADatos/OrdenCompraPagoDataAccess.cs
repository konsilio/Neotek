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
    public class OrdenCompraPagoDataAccess
    {
        private SagasDataUow uow;

        public OrdenCompraPagoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(OrdenCompraPago oc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompraPago>().Insert(oc);
                    uow.SaveChanges();
                    _respuesta.Id = oc.IdOrdenCompra;
                    _respuesta.Mensaje = Exito.OK;
                    _respuesta.Exito = true;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la solicitud de pago");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public OrdenCompraPago Buscar(int idoc, short orden)
        {
            return uow.Repository<OrdenCompraPago>().GetAll().FirstOrDefault(x => x.IdOrdenCompra.Equals(idoc) && x.Orden.Equals(orden));
        }
        public List<OrdenCompraPago> Buscar(int  idoc)
        {
            return uow.Repository<OrdenCompraPago>().Get(x => x.IdOrdenCompra.Equals(idoc)).ToList();
        }
        public List<OrdenCompraPago> BuscarTodo()
        {
            return uow.Repository<OrdenCompraPago>().GetAll().ToList();
        }
        public RespuestaDto Actualizar(OrdenCompraPago ocp)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompraPago>().Update(ocp);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la confirmación de pago");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
