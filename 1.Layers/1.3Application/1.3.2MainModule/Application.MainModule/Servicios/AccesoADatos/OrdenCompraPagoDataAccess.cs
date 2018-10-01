using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
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
                    _respuesta.MensajesError.Add(ex.Message);
                    if(ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }
    }
}
