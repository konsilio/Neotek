using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Transporte;
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
    public class AsigancionUtilitariosDataAccess
    {
        private SagasDataUow uow;

        public AsigancionUtilitariosDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(AsignacionUtilitarios entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AsignacionUtilitarios>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdAsignacionUtilitario;
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
        public RespuestaDto Actualizar(AsignacionUtilitarios entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();

            using (uow)
            {
                try
                {
                    uow.Repository<AsignacionUtilitarios>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdAsignacionUtilitario;
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
        public AsignacionUtilitarios Obtener(int IdP)
        {
            return uow.Repository<AsignacionUtilitarios>().GetSingle(
                x => x.IdAsignacionUtilitario.Equals(IdP) && x.Activo);
        }
        public AsignacionUtilitarios ObtenerPorUtilitario(int id)
        {
            return uow.Repository<AsignacionUtilitarios>().GetSingle(
                x => x.IdUtilitario.Equals(id) && x.Activo);
        }
        public AsignacionUtilitarios Obtener(TransporteDTO dto)
        {
            return uow.Repository<AsignacionUtilitarios>().GetSingle(
                x => //x.IdChoferOperador.Equals(dto.IdChofer) 
                 x.IdUtilitario.Equals(dto.IdVehiculo)
                && x.IdEmpresa.Equals(dto.IdEmpresa)
                && x.Activo
                );
        }
        public PuntoVenta ObtenerOcupada(int idOperador, short idCAlmacen, short idEmpresa)
        {
            return uow.Repository<PuntoVenta>().GetSingle(
                x => x.IdOperadorChofer.Equals(idOperador) ||
                x.IdCAlmacenGas.Equals(idCAlmacen)
                && x.IdEmpresa.Equals(idEmpresa)
                && x.Activo
                );
        }
        public List<AsignacionUtilitarios> Obtener()
        {
            return uow.Repository<AsignacionUtilitarios>().Get(x => x.Activo).ToList();
        }
        public List<AsignacionUtilitarios> ObtenerAsignacionUtilitarios(short idEmpresa)
        {
            return uow.Repository<AsignacionUtilitarios>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }

    }
}
