using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Seguridad;
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
    public class CuentaContableDataAccess
    {
        private SagasDataUow uow;
        public CuentaContableDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<CuentaContable> BuscarCuentasContables(int idEmpresa)
        {
            return uow.Repository<CuentaContable>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();            
        }
       
        public RespuestaDto InsertarCuentaContable(CuentaContable cc)
        {
            RespuestaDto respuesta = new RespuestaDto();
            try
            {
                uow.Repository<CuentaContable>().Insert(cc);
                uow.SaveChanges();
                respuesta.Exito = true;
                respuesta.EsInsercion = true;
                respuesta.Id = cc.IdCuentaContable;
                respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.MensajesError.Add(ex.Message);
                if (ex.InnerException != null)
                    respuesta.MensajesError.Add(ex.InnerException.Message);
            }
            return respuesta;
        }
        public RespuestaDto ActualizarCuentaContable(CuentaContable cc)
        {
            RespuestaDto respuesta = new RespuestaDto();
            try
            {
                uow.Repository<CuentaContable>().Update(cc);
                uow.SaveChanges();
                respuesta.Exito = true;
                respuesta.Id = cc.IdCuentaContable;
                respuesta.EsActulizacion = true;
                respuesta.ModeloValido = true;
                respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.MensajesError.Add(ex.Message);
                if (ex.InnerException != null)
                    respuesta.MensajesError.Add(ex.InnerException.Message);
            }
           return respuesta;
        }

        public RespuestaDto Insertar(CuentaContable _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CuentaContable>().Insert(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCuentaContable;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la CuentaContable");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(CuentaContable _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.CuentaContable>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCuentaContable;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la CuentaContable"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<CuentaContable> BuscarTodos()
        {
            return uow.Repository<CuentaContable>().Get(x => x.Activo).ToList();
        }

        public List<CuentaContable> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<CuentaContable>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }

        public CuentaContable Buscar(int idCuentaContable)
        {
            return uow.Repository<CuentaContable>().GetSingle(x => x.IdCuentaContable.Equals(idCuentaContable)
                                                         && x.Activo);
        }
        //public CuentaContable Buscar(int idCuentaContable)
        //{
        //    return uow.Repository<CuentaContable>().GetSingle(x => x.IdCuentaContable.Equals(idCuentaContable));
        //}
    }
}
