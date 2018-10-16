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
   public class CajaGeneralDataAccess
    {
        private SagasDataUow uow;

        public CajaGeneralDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<VentaMovimiento> BuscarTodos()
        {
            return uow.Repository<VentaMovimiento>().Get().ToList();
        }
      
        public List<VentaMovimiento> Buscar(short idEmpresa)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         ).ToList();
        }

        public VentaCajaGeneral BuscarGralPorCve(string cve)
        {
            return uow.Repository<VentaCajaGeneral>().GetSingle(x => x.FolioOperacionDia.Equals(cve));
        }

        public List<VentaPuntoDeVenta> BuscarPorCve(string cve)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.FolioOperacionDia.Equals(cve)).ToList();           
        }

        public List<VentaCorteAnticipoEC> BuscarPorCveEC(string cve)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(x => x.FolioOperacionDia.Equals(cve)).ToList();
        }

        public RespuestaDto Actualizar(List<VentaPuntoDeVenta> pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _pv in pv)
                    {
                        uow.Repository<Sagas.MainModule.Entidades.VentaPuntoDeVenta>().Update(_pv);
                    }
                    uow.SaveChanges();                   
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la liquidación"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(List<VentaCorteAnticipoEC> pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _pv in pv)
                    {
                        uow.Repository<Sagas.MainModule.Entidades.VentaCorteAnticipoEC>().Update(_pv);
                    }
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la liquidación"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
