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
                    uow.Repository<PuntoVenta>().Update(_pro);
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
        public List<VentaCajaGeneral> ObtenerVentasCajaGral()
        {
            return uow.Repository<VentaCajaGeneral>().GetAll().OrderBy(x => x.Orden).ToList();
        }
        public List<PuntoVenta> BuscarPorOperadorChofer(int OperadorChofer)
        {
            return uow.Repository<PuntoVenta>().Get(x => x.IdOperadorChofer.Equals(OperadorChofer)
                                                         && x.Activo).ToList();
        }
        public PuntoVenta BuscarPorUnidadAlmacenGas(short idCAlmacenGas)
        {
            return uow.Repository<PuntoVenta>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                      && x.Activo).FirstOrDefault();
        }
        public RespuestaDto InesertarVentaGeneral(VentaCajaGeneral corteCajaGeneral)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<VentaCajaGeneral>().Insert(corteCajaGeneral);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.EsInsercion = true;
                    _respuesta.Mensaje = Exito.OK;
                    _respuesta.Id = corteCajaGeneral.IdPuntoVenta;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "No se ha registrado la venta en general");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }

            }
            return _respuesta;
        }
        public List<VentaCorteAnticipoEC> Anticipos(UnidadAlmacenGas unidadEstacion, DateTime fecha)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                x => x.IdCAlmacenGas.Equals(unidadEstacion.IdCAlmacenGas)
                && x.FechaCorteAnticipo.Day.Equals(fecha.Day)
                && x.FechaCorteAnticipo.Month.Equals(fecha.Month)
                && x.FechaCorteAnticipo.Year.Equals(fecha.Year)
           ).ToList();
        }
        public List<VentaCorteAnticipoEC> Anticipos(UnidadAlmacenGas unidadEstacion)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                x => x.IdCAlmacenGas.Equals(unidadEstacion.IdCAlmacenGas)
            ).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarVentasTipoPago(int idPuntoVenta, DateTime fecha, bool esCredito = false)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(
                x => x.IdPuntoVenta.Equals(idPuntoVenta) &&
                x.FechaRegistro.Day.Equals(fecha.Day) &&
                x.FechaRegistro.Month.Equals(fecha.Month) &&
                x.FechaRegistro.Year.Equals(fecha.Year) &&
                x.VentaACredito.Equals(esCredito)
           ).ToList();
        }
        public VentaCorteAnticipoEC BuscarCorte(DateTime fecha, short idCAlmacenGas)
        {
            return uow.Repository<VentaCorteAnticipoEC>().GetSingle(
                    x => x.IdCAlmacenGas.Equals(idCAlmacenGas) &&
                    x.Dia.Equals((byte)fecha.Day) &&
                    x.Mes.Equals((byte)fecha.Month) &&
                    x.Year.Equals((short)fecha.Year) &&
                    x.IdTipoOperacion.Equals(2)
                );
        }
        public List<VentaCorteAnticipoEC> BuscarAnticipos(DateTime fecha, short idCAlmacenGas)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas) &&
                x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                x.FechaCorteAnticipo.Year.Equals(fecha.Year) &&
                x.TipoOperacion.Equals(1)
             ).ToList();
        }
        public RespuestaDto RegistrarReporteDia(ReporteDelDia reporteEntity)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<ReporteDelDia>().Insert(reporteEntity);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, " registrar el reporte del dìa ");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
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
                x => x.FolioOperacion.Equals(claveOperacion)
                && x.IdTipoOperacion.Equals(1)
               );
        }
        public List<VentaCorteAnticipoEC> Anticipos(short idEmpresa)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                    x =>
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
                x => x.FolioOperacion.Equals(claveOperacion)
                && x.IdTipoOperacion.Equals(2)
                );
        }
        public List<VentaCorteAnticipoEC> Cortes(short idEmpresa)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(
                x => x.IdEmpresa.Equals(idEmpresa)
                ).ToList();
        }
        public VentaPuntoDeVenta EvaluarFolio(string folioVenta)
        {
            return uow.Repository<VentaPuntoDeVenta>().GetSingle(x => x.FolioVenta.Equals(folioVenta));
        }
        public RespuestaDto InsertarMobile(VentaPuntoDeVenta venta)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<VentaPuntoDeVenta>().Insert(venta);
                    //uow.Repository<AlmacenGas>().Update(_alm);

                    uow.SaveChanges();
                    _respuesta.EsInsercion = true;
                    _respuesta.Id = venta.IdPuntoVenta;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "registrar la venta.");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<PrecioVenta> PreciosVenta(short idEmpresa)
        {
            return uow.Repository<PrecioVenta>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
        public List<VentaCorteAnticipoEC> ObtenerCortesAnticipos()
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get().ToList();
        }
        public object ActualizarVentas(VentaPuntoDeVenta item)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<VentaPuntoDeVenta>().Update(item);
                    //uow.Repository<AlmacenGas>().Update(_alm);

                    uow.SaveChanges();
                    _respuesta.EsInsercion = true;
                    _respuesta.Id = item.IdPuntoVenta;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "actualizar la venta.");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public VentaPuntoDeVenta ObtenerVenta(string tiketVenta)
        {
            return uow.Repository<VentaPuntoDeVenta>().GetSingle(x => x.FolioVenta.Equals(tiketVenta));
        }
        public RespuestaDto insertCargoMobile(Cargo cargo)
        {
            RespuestaDto _Respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Cargo>().Insert(cargo);
                    uow.SaveChanges();
                    _Respuesta.Exito = true;
                    _Respuesta.EsInsercion = true;
                    _Respuesta.Id = cargo.IdCargo;
                    _Respuesta.ModeloValido = true;
                    _Respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _Respuesta.Exito = false;
                    _Respuesta.Mensaje = string.Format(Error.S0004, "registar el cargo");
                    _Respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _Respuesta;
        }
        public List<VentaPuntoDeVenta> ObtenerVentas(int idPuntoVenta, DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.FechaRegistro.Day.Equals(fecha.Day) && x.FechaRegistro.Month.Equals(fecha.Month) && x.FechaRegistro.Year.Equals(fecha.Year)
            && x.IdPuntoVenta.Equals(idPuntoVenta)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarVentasPorRFC(string rfc)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.RFC.Equals(rfc)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarVentasPorCliente(int id)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdCliente.Equals(id)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarVentasPorEmpresa(short IdEmpresa)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdEmpresa.Equals(IdEmpresa)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarVentasPorPeriodo(short IdEmpresa, DateTime p)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdEmpresa.Equals(IdEmpresa) 
                                                            && x.FechaRegistro.Month.Equals(p.Month)
                                                            && x.FechaRegistro.Year.Equals(p.Year)).ToList();
        }
    }
}
