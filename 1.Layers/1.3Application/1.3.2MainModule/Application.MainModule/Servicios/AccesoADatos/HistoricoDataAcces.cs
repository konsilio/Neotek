using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;


namespace Application.MainModule.Servicios.AccesoADatos
{
    class HistoricoDataAcces
    {

        private SagasDataUow uow;
        public RespuestaDto _respuesta = new RespuestaDto();

        public HistoricoDataAcces()
        {
            uow = new SagasDataUow();
        }


        public RespuestaDto Insertar(List<HistoricoVentas> Lista)
        {
            try
            {
                foreach (var entidad in Lista)
                {
                    uow.Repository<HistoricoVentas>().Insert(entidad);
                }
                uow.SaveChanges();

                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;

            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = ex.Message;
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
          
        }
        public RespuestaDto Actualizar(HistoricoVentas entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<HistoricoVentas>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id;
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

        public HistoricoVentas Obtener(int id)
        {
            return uow.Repository<HistoricoVentas>().GetSingle(
                x => x.Id.Equals(id)
                );
        }
        public List<HistoricoVentas> Obtener()
        {
            return uow.Repository<HistoricoVentas>().GetAll().ToList();
        }
        public List<int> ObtenerElementosDistintos()
        {
            return uow.Repository<HistoricoVentas>().GetAll().OrderBy(m => m.Anio).Select(m => m.Anio).Distinct().ToList();
        }
        public string Json()
        {
            return "";
        }
        public List<HistoricoVentas> ObtenerPorMes(int Anio, int mes)
        {
            return uow.Repository<HistoricoVentas>().Get(x => x.Anio.Equals(Anio) && x.Mes.Equals(mes)).ToList();
        }
    }
}
