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
    public class FormaPagoDataAccess
    {
        private SagasDataUow uow;

        public FormaPagoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(FormaPago _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<FormaPago>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdFormaPago;
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
        public RespuestaDto Actualizar(FormaPago _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.FormaPago>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdFormaPago;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del centro de costo"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<FormaPago> BuscarTodos()
        {
            return uow.Repository<FormaPago>().Get(x => x.Activo).ToList();
        }
        public FormaPago Buscar(byte idFormaPago)
        {
            return uow.Repository<FormaPago>().GetSingle(x => x.IdFormaPago.Equals(idFormaPago)
                                                         && x.Activo);
        }
        public FormaPago BuscarDescripcion(short idEmpresa, string Desc)
        {
            return uow.Repository<FormaPago>().GetSingle(x => x.Descripcion.Equals(Desc)
                                                             && x.Activo);
        }
    }
}
