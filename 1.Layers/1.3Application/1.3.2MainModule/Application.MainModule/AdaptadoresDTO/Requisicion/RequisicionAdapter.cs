using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Requisicion;

namespace Application.MainModule.AdaptadoresDTO.Requisicion
{
    public static class RequisicionAdapter
    {
        public static RequisicionDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionDTO requiscionDTO = new RequisicionDTO()
            {
                IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro,
                IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                OpinionAlmacen = _requisicion.OpinionAlmacen,
                FechaRevision = _requisicion.FechaRevision,
                MotivoCancelacion = _requisicion.MotivoCancelacion,
                IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                FechaAutorizacion = _requisicion.FechaAutorizacion
            };
            return requiscionDTO;
        }
        public static List<RequisicionDTO> ToDTO(List<Sagas.MainModule.Entidades.Requisicion> _requisiciones)
        {
            List<RequisicionDTO> requisicionesDTO = _requisiciones.ToList().Select(x => ToDTO(x)).ToList();
            return requisicionesDTO;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionDTO _requisicion)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {
                IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro,
                IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                OpinionAlmacen = _requisicion.OpinionAlmacen,
                FechaRevision = _requisicion.FechaRevision,
                MotivoCancelacion = _requisicion.MotivoCancelacion,
                IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                FechaAutorizacion = _requisicion.FechaAutorizacion
            };
            return requiscionDTO;
        }
        public static List<Sagas.MainModule.Entidades.Requisicion> FromDTO(List<RequisicionDTO> _requisiciones)
        {
            List<Sagas.MainModule.Entidades.Requisicion> requisicionesDTO = _requisiciones.ToList().Select(x => FromDTO(x)).ToList();
            return requisicionesDTO;
        }

    }
}
