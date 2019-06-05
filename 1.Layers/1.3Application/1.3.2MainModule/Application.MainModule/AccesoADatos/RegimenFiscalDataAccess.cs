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
    public class RegimenFiscalDataAccess
    {
        private SagasDataUow uow;

        public RegimenFiscalDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(RegimenFiscal _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<RegimenFiscal>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdRegimenFiscal;
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
        public RespuestaDto Actualizar(RegimenFiscal _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.RegimenFiscal>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdRegimenFiscal;
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
        public List<RegimenFiscal> BuscarTodos()
        {
            return uow.Repository<RegimenFiscal>().GetAll().ToList();
        }
        public RegimenFiscal Buscar(int idRegimenFiscal)
        {
            return uow.Repository<RegimenFiscal>().GetSingle(x => x.IdRegimenFiscal.Equals(idRegimenFiscal));
        }
        public RegimenFiscal BuscarDescripcion(string desc)
        {
            return uow.Repository<RegimenFiscal>().GetSingle(x => x.Descripcion.Equals(desc));
        }
    }
}
