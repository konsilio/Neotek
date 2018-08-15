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
    public class TipoMedidorGasDataAccess
    {
        private SagasDataUow uow;

        public TipoMedidorGasDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(TipoMedidorUnidadAlmacenGas _med)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<TipoMedidorUnidadAlmacenGas>().Insert(_med);
                    uow.SaveChanges();
                    _respuesta.Id = _med.IdTipoMedidor;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Tipo de Medidor");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(TipoMedidorUnidadAlmacenGas _med)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.TipoMedidorUnidadAlmacenGas>().Update(_med);
                    uow.SaveChanges();
                    _respuesta.Id = _med.IdTipoMedidor;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Tipo de medidor"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<TipoMedidorUnidadAlmacenGas> BuscarTodos()
        {
            return uow.Repository<TipoMedidorUnidadAlmacenGas>().Get(x => x.Activo).ToList();
        }

        public TipoMedidorUnidadAlmacenGas Buscar(short idTipoMedidorUnidadAlmacenGas)
        {
            return uow.Repository<TipoMedidorUnidadAlmacenGas>().GetSingle(x => x.IdTipoMedidor.Equals(idTipoMedidorUnidadAlmacenGas)
                                                         && x.Activo);
        }
    }
}
