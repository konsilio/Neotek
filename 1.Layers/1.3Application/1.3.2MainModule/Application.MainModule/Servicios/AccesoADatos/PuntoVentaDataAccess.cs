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
    public class PuntoVentaDataAccess
    {
        private SagasDataUow uow;

        public PuntoVentaDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(PuntoVenta _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<PuntoVenta>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdPuntoVenta;
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
        public RespuestaDto Actualizar(PuntoVenta _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.PuntoVenta>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdPuntoVenta;
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
        public List<PuntoVenta> BuscarTodos()
        {
            return uow.Repository<PuntoVenta>().Get(x => x.Activo).ToList();
        }
        public List<PuntoVenta> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<PuntoVenta>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
        public PuntoVenta Buscar(int idPuntoVenta)
        {
            return uow.Repository<PuntoVenta>().GetSingle(x => x.IdPuntoVenta.Equals(idPuntoVenta)
                                                         && x.Activo);
        }
        public OperadorChofer BuscarOperador(int idOperador)
        {
            return uow.Repository<OperadorChofer>().GetSingle(x => x.IdOperadorChofer.Equals(idOperador)
                                                         && x.Activo);
        }
        public OperadorChofer BuscarPorUsuario(int idUsuario)
        {
            return uow.Repository<OperadorChofer>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }

        public List<PuntoVenta> BuscarPorOperadorChofer(int OperadorChofer)
        {
            return uow.Repository<PuntoVenta>().Get(x => x.IdOperadorChofer.Equals(OperadorChofer)
                                                         && x.Activo).ToList();
        }

        public RespuestaDto Eliminar(PuntoVenta cteL)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<PuntoVenta>().Delete(cteL);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Eliminar el punto de venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public VentaCorteAnticipoEC BuscarAnticipo(string claveOperacion)
        {
            return uow.Repository<VentaCorteAnticipoEC>().GetSingle(
                x=>x.FolioOperacion.Equals(claveOperacion)
                && x.IdTipoOperacion.Equals(1)
               );
        }

        public List<VentaCorteAnticipoEC> Anticipos(short idEmpresa)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                    x=>
                    x.IdEmpresa.Equals(idEmpresa)
                    && x.IdTipoOperacion.Equals(1)
                ).ToList();
        }

        public RespuestaDto InsertarCorte(VentaCorteAnticipoEC anticipo)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<VentaCorteAnticipoEC>().Insert(anticipo);
                    uow.SaveChanges();
                    _respuesta.EsInsercion = true;
                    _respuesta.Id = anticipo.IdPuntoVenta;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "registrar el anticipo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public VentaCorteAnticipoEC BuscarCorte(string claveOperacion)
        {
            return uow.Repository<VentaCorteAnticipoEC>().GetSingle(
                x=>x.FolioOperacion.Equals(claveOperacion)
                && x.IdTipoOperacion.Equals(2)
                );
        }

        public List<VentaCorteAnticipoEC> Cortes(short idEmpresa)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                x=>x.IdEmpresa.Equals(idEmpresa)
                ).ToList();
        }
    }
}
