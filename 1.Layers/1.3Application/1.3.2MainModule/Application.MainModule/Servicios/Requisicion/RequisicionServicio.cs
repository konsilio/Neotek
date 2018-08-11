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
        public static RespuestaRequisicionDto GuardarRequisicionNueva(RequisicionEDTO _req)
        {
            var requisicionResp = new RequisicionDataAccess().InsertarNueva(RequisicionAdapter.FromEDTO(_req));
            if (requisicionResp != null)
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = requisicionResp.IdRequisicion,
                    NumRequisicion = requisicionResp.NumRequisicion,
                    Exito = true,
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
        public static RespuestaRequisicionDto UpdateRequisicionRevision(RequisicionRevisionDTO _req)
        {
            var requisicionResp = new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req));
            if (requisicionResp != null && requisicionResp.Exito)
            {
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = requisicionResp.IdRequisicion,
                    NumRequisicion = requisicionResp.NumRequisicion,
                    Exito = true,
                    Mensaje = "Requisicion revisada con exito"
                };
            }
            else
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = 0,
                    NumRequisicion = string.Empty,
                    Exito = false,
                    Mensaje = Error.R0009
                };
        }
        public static RespuestaRequisicionDto UpDateRequisicionAutoriza(RequisicionEDTO _req)
        {
            var requisicionResp = new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromEDTOAutorizacion(_req));
            if (requisicionResp != null)
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = requisicionResp.IdRequisicion,
                    NumRequisicion = requisicionResp.NumRequisicion,
                    Exito = true,
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
        public static RequisicionRevisionDTO BuscarRequisicion(string _nrequi)
        {
            return RequisicionAdapter.ToRevDTO(new RequisicionDataAccess().BuscarPorNumeroRequisicion(_nrequi));
        }
    }
}
