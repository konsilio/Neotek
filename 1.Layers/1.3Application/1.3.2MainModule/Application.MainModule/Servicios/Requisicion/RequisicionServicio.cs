using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisicion;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.Servicios.Notificacion;

namespace Application.MainModule.Servicios.Requisicion
{
    public static class RequisicionServicio
    {
        public static RespuestaRequisicionDto GuardarRequisicionNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            var respuesta = new RequisicionDataAccess().InsertarNueva(_req);
            if (respuesta.Exito)
                NotificarServicio.RequisicionNueva(_req);

            return respuesta;
        }
        public static RespuestaRequisicionDto UpdateRequisicionRevision(RequisicionRevPutDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad), RequisicionProductoAdapter.FromDTO(_req.ListaProductos, entidad.Productos.ToList()));            
        }
        public static RespuestaRequisicionDto UpDateRequisicionAutoriza(RequisicionAutPutDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad));

        }
        public static List<RequisicionDTO> BuscarRequisicionPorIdEmpresa(Int16 _IdEmpresa)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodas().Where(x => x.IdEmpresa.Equals(_IdEmpresa)).ToList());
        }
        public static RequisicionRevisionDTO BuscarRequisicion(int _idrequi)
        {
            return RequisicionAdapter.ToRevDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
        public static RequisicionAutorizacionDTO BuscarRequisicionAuto(int _idequi)
        {
            return RequisicionAdapter.ToAutDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idequi));
        }
        public static RespuestaRequisicionDto CancelarRequisicion(RequisicionCancelaDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad));

        }
    }
}
