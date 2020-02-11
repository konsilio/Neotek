using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class PrecioVentaDataAccess
    {
        private SagasDataUow uow;
        public PrecioVentaDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<PrecioVenta> BuscarTodos()
        {
            return uow.Repository<PrecioVenta>().Get(x => x.Activo).OrderByDescending(x => x.FechaRegistro).ToList();
        }
        public List<PrecioVenta> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<PrecioVenta>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.CProducto.Activo).ToList();
        }
        public List<PrecioVenta> BuscarTodos(short idEmpresa, DateTime fi, DateTime ff)
        {
            return uow.Repository<PrecioVenta>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && (x.FechaRegistro > fi
                                                         && x.FechaRegistro < ff)
                                                         && x.CProducto.Activo)
                                                         .ToList();
        }
        public PrecioVenta BuscarPrecioVentaVigente(short idEmpresa)
        {
            return uow.Repository<PrecioVenta>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdPrecioVentaEstatus.Equals(EstatusPrecioVentaEnum.Vigente));
        }
        public PrecioVenta BuscarPrecioVentaVigenteEstaciones(short idEmpresa)
        {
            return uow.Repository<PrecioVenta>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.EsEstaciones
                                                            && x.IdPrecioVentaEstatus.Equals(EstatusPrecioVentaEnum.Vigente));
        }
        public PrecioVenta BuscarPrecioVentaVigenteEstaciones(short idEmpresa, int idEstaciones)
        {
            return uow.Repository<PrecioVenta>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.IdEstacion.Equals(idEstaciones)
                                                            && x.IdPrecioVentaEstatus.Equals(EstatusPrecioVentaEnum.Vigente));
        }
        public PrecioVenta BuscarIdPV(short IdPrecioVenta)
        {
            return uow.Repository<PrecioVenta>().GetSingle(x => x.IdPrecioVenta.Equals(IdPrecioVenta)
                                                         && x.Activo);

        }
        public PrecioVentaEstatus Buscar(byte IdPrecioVentaEstatus)
        {
            return uow.Repository<PrecioVentaEstatus>().GetSingle(x => x.IdPrecioVentaEstatus.Equals(IdPrecioVentaEstatus)
                                                         && x.Activo);
        }
        public List<PrecioVentaEstatus> Buscar()
        {
            return uow.Repository<PrecioVentaEstatus>().Get(x => x.Activo).ToList();
        }
        public PrecioVenta BuscarUltimoRegistro()
        {
            return BuscarUltimoRegistroInsertado().LastOrDefault();
        }
        public List<PrecioVenta> BuscarUltimoRegistroInsertado()
        {
            return uow.Repository<PrecioVenta>().Get().ToList();
        }
        public int BuscarUltimoPrecio()
        {
            if (uow.Repository<PrecioVenta>().GetAll().ToList().Count.Equals(0))
                return 0;
            else
                return uow.Repository<PrecioVenta>().GetAll().ToList().Last().IdPrecioVenta;
        }
        public RespuestaDto Insertar(List<PrecioVenta> pvs)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var cte in pvs)
                    {
                        if (cte.IdProducto.Equals(0))
                        {
                            PrecioVenta PrecioActual = new PrecioVenta();
                            if (!cte.EsEstaciones && cte.IdEstacion == null)
                                PrecioActual = BuscarPrecioVentaVigente(cte.IdEmpresa);
                            if (cte.EsEstaciones && cte.IdEstacion == null)
                                PrecioActual = BuscarPrecioVentaVigenteEstaciones(cte.IdEmpresa);
                            if (cte.IdEstacion != null)
                                PrecioActual = BuscarPrecioVentaVigenteEstaciones(cte.IdEmpresa, cte.IdEstacion.Value);
                            if (PrecioActual != null && PrecioActual.IdPrecioVenta > 0)
                                if (cte.FechaProgramada == null)
                                    PrecioActual.IdPrecioVentaEstatus = EstatusPrecioVentaEnum.Vencido;
                        }                        
                        uow.Repository<PrecioVenta>().Insert(cte);
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
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Precio de Venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(PrecioVenta _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<PrecioVenta>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdPrecioVenta;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del precio de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Eliminar(PrecioVenta cteL)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<PrecioVenta>().Delete(cteL);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Eliminar el precio de venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
