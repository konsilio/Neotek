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
     public class VehiculoUtilitarioDataAccess
    {
        private SagasDataUow uow;

        public VehiculoUtilitarioDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(CUtilitario _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CUtilitario>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdUtilitario;
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
        public RespuestaDto Actualizar(CUtilitario _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CUtilitario>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdUtilitario;
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
        public RespuestaDto Borrar(CUtilitario _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CUtilitario>().Delete(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdUtilitario;
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
        public CUtilitario Obtener(int IdP)
        {
            return uow.Repository<CUtilitario>().GetSingle(
                x => x.IdUtilitario.Equals(IdP) && x.Activo
                );
        }
        public CUtilitario Obtener(int IdP, bool Activo)
        {
            return uow.Repository<CUtilitario>().GetSingle(
                x => x.IdUtilitario.Equals(IdP)
                );
        }
        public List<CUtilitario> Obtener(short idEmpresa)
        {
            return uow.Repository<CUtilitario>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
        public List<CUtilitario> Obtener()
        {
            return uow.Repository<CUtilitario>().Get(x => x.Activo).ToList();
        }
        public CUtilitario Obtener(short idEmpresa, string nom, string num)
        {
            return uow.Repository<CUtilitario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo && x.Nombre.Equals(nom.Trim()) && x.Numero.Equals(num.Trim()));
        }
    }
}
