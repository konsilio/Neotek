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
    public class CombustibleDataAccess
    {
        private SagasDataUow uow;
        public CombustibleDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(CCombustible _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CCombustible>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.Id_Combustible;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del ccombustible");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(CCombustible _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CCombustible>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.Id_Combustible;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del ccombustible"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Eliminar(CCombustible cteL)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CCombustible>().Delete(cteL);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Eliminar ccombustible");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public CCombustible ObtenerCombustible(int IdP)
        {
            return uow.Repository<CCombustible>().GetSingle(
                x => x.Id_Combustible.Equals(IdP) && x.Activo
                );
        }
        public List<CCombustible> ObtenerCombustible(short idEmpresa)
        {
            return uow.Repository<CCombustible>().Get(
                x => x.Id_Empresa.Equals(idEmpresa) && x.Activo
                ).ToList();
        }
        public List<CCombustible> ObtenerCombustible(short idEmpresa, string descripcion)
        {
            return uow.Repository<CCombustible>().Get(x => x.Id_Empresa.Equals(idEmpresa)&& x.Descripcion.Contains(descripcion) && x.Activo).ToList();
        }       
        public List<CCombustible> ObtenerCombustible()
        {
            return uow.Repository<CCombustible>().Get(x => x.Activo).ToList();
        }
    }
}
