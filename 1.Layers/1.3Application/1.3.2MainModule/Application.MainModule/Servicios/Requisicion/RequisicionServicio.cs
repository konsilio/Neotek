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

namespace Application.MainModule.Servicios.Requisicion
{
    public static class RequisicionServicio
    {
        public static RespuestaRequisicionDto GuardarRequisicionNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            return new RequisicionDataAccess().InsertarNueva(_req);
        }
        public static RespuestaRequisicionDto UpdateRequisicionRevision(RequisicionRevPutDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad), RequisicionProductoAdapter.FromDTO(_req.ListaProductos, entidad.Productos.ToList()));            
        }
        public static RespuestaRequisicionDto UpDateRequisicionAutoriza(RequisicionAutPutDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            var requisicionResp = new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad));
            if (requisicionResp != null)
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = requisicionResp.IdRequisicion,
                    NumRequisicion = requisicionResp.NumRequisicion,
                    Exito = requisicionResp.Exito,
                    Mensaje = requisicionResp.Mensaje
                };
            else
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = 0,
                    NumRequisicion = string.Empty,
                    Exito = false,
                    Mensaje = "Error" //Agregar mensaje espesifico en Exceptions
                };
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
    }
}
