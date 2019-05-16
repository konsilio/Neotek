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
    public class TipoCentroCostoDataAccess
    {
        private SagasDataUow uow;
        public TipoCentroCostoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(TipoCentroCosto _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<TipoCentroCosto>().Insert(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdTipoCentroCosto;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del TipoCentroCosto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(TipoCentroCosto _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.TipoCentroCosto>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdTipoCentroCosto;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del TipoCentroCosto"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<TipoCentroCosto> BuscarTodos()
        {
            return uow.Repository<TipoCentroCosto>().GetAll().ToList();
        }

        //public List<TipoCentroCosto> BuscarTodos(short idEmpresa)
        //{
        //    return uow.Repository<TipoCentroCosto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
        //                                                 && x.Activo)
        //                                                 .ToList();
        //}

        public TipoCentroCosto Buscar(int idTipoCentroCosto)
        {
            return uow.Repository<TipoCentroCosto>().GetSingle(x => x.IdTipoCentroCosto.Equals(idTipoCentroCosto));
        }

    }
}
