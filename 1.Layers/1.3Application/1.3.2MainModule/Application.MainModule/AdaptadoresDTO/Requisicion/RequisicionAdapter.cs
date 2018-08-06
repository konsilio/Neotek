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
                FechaRegistro = _requisicion.FechaRegistro
                //IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                //OpinionAlmacen = _requisicion.OpinionAlmacen,
                //FechaRevision = _requisicion.FechaRevision,
                //MotivoCancelacion = _requisicion.MotivoCancelacion,
                //IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                //FechaAutorizacion = _requisicion.FechaAutorizacion
            };
            if (_requisicion.IdRequisicionEstatus.Equals(2))
            {
                requiscionDTO = DatosRevision(requiscionDTO, _requisicion);
            }
            if (_requisicion.IdRequisicionEstatus.Equals(3))
            {
                requiscionDTO = DatosRevision(requiscionDTO, _requisicion);
                _requisicion.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion;
                _requisicion.FechaAutorizacion = _requisicion.FechaAutorizacion;
            }
            return requiscionDTO;
        }
        private static RequisicionDTO DatosRevision(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            requiscionDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision;
            requiscionDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionDTO.FechaRevision = _requisicion.FechaRevision;
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
                //IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro
                //,IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                //OpinionAlmacen = _requisicion.OpinionAlmacen,
                //FechaRevision = _requisicion.FechaRevision,
                //MotivoCancelacion = _requisicion.MotivoCancelacion,
                //IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                //FechaAutorizacion = _requisicion.FechaAutorizacion
            };
            return requiscionDTO;
        }
        public static List<Sagas.MainModule.Entidades.Requisicion> FromDTO(List<RequisicionDTO> _requisiciones)
        {
            List<Sagas.MainModule.Entidades.Requisicion> requisicionesDTO = _requisiciones.ToList().Select(x => FromDTO(x)).ToList();
            return requisicionesDTO;
        }
        public static Sagas.MainModule.Entidades.Requisicion UnirFromDTO(RequisicionDTO _requisicion, List<RequisicionProductoDTO> _prod)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {
                //IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro,
                //IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                //OpinionAlmacen = _requisicion.OpinionAlmacen,
                //FechaRevision = _requisicion.FechaRevision,
                //MotivoCancelacion = _requisicion.MotivoCancelacion,
                //IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                //FechaAutorizacion = _requisicion.FechaAutorizacion,
                Productos = RequisicionProductoAdapter.FromDTO(_prod)
            };
            return requiscionDTO;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromEDTO(RequisicionEDTO _requisicion)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {
                //IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = Servicios.FolioServicio.GenerarNumeroRequisicion(_requisicion),
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro,
                //IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                //OpinionAlmacen = _requisicion.OpinionAlmacen,
                //FechaRevision = _requisicion.FechaRevision,
                //MotivoCancelacion = _requisicion.MotivoCancelacion,
                //IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                //FechaAutorizacion = _requisicion.FechaAutorizacion,
                Productos = RequisicionProductoAdapter.FromDTO(_requisicion.ListaProductos)
            };
            return requiscionDTO;
        }
        public static RequisicionEDTO ToEDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionEDTO requiscionEDTO = new RequisicionEDTO
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
                //IdUsuarioRevision = _requisicion.IdUsuarioRevision,
                //OpinionAlmacen = _requisicion.OpinionAlmacen,
                //FechaRevision = _requisicion.FechaRevision,
                //MotivoCancelacion = _requisicion.MotivoCancelacion,
                //IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion,
                //FechaAutorizacion = _requisicion.FechaAutorizacion,
                ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList())
            };
            return requiscionEDTO;
        }
    }
}
