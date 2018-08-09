using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Requisicion;
using Sagas.MainModule.ObjetosValor.Enum;

namespace Application.MainModule.AdaptadoresDTO.Requisicion
{
    public static class RequisicionAdapter
    {
        #region ToDTO
        public static RequisicionDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionDTO requiscionDTO = new RequisicionDTO()
            {                
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro            
            };
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTORevision(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            requiscionDTO.IdRequisicion = _requisicion.IdRequisicion;
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision;
            requiscionDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionDTO.FechaRevision = _requisicion.FechaRevision;
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTOAutorizacion(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {          
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;           
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionDTO.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion;
            requiscionDTO.FechaAutorizacion = _requisicion.FechaAutorizacion;
            return requiscionDTO;
        }
        public static List<RequisicionDTO> ToDTO(List<Sagas.MainModule.Entidades.Requisicion> _requisiciones)
        {
            List<RequisicionDTO> requisicionesDTO = _requisiciones.ToList().Select(x => ToDTO(x)).ToList();
            return requisicionesDTO;
        }
        #endregion
        #region FromDTO
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionDTO _requisicion)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {               
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro                
            };
            return requiscionDTO;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTORevision(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            _requisicion.IdRequisicion = requiscionDTO.IdRequisicion;
            _requisicion.IdRequisicionEstatus = requiscionDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = requiscionDTO.IdUsuarioRevision;
            _requisicion.OpinionAlmacen = requiscionDTO.OpinionAlmacen;
            _requisicion.FechaRevision = requiscionDTO.FechaRevision;
            _requisicion.MotivoCancelacion = requiscionDTO.MotivoCancelacion;
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTOAutorizacion(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            _requisicion.IdRequisicionEstatus = requiscionDTO.IdRequisicionEstatus;
            _requisicion.MotivoCancelacion = requiscionDTO.MotivoCancelacion;
            _requisicion.IdUsuarioAutorizacion = requiscionDTO.IdUsuarioAutorizacion;
            _requisicion.FechaAutorizacion = requiscionDTO.FechaAutorizacion;
            return _requisicion;
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
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro,               
                Productos = RequisicionProductoAdapter.FromDTO(_prod)
            };
            return requiscionDTO;
        }
        #endregion
        #region ToEDTO
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
                ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList())
            };
            return requiscionEDTO;
        }
        public static RequisicionEDTO ToEDTORevision(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionEDTO requiscionEDTO = new RequisicionEDTO();
            requiscionEDTO.IdRequisicion = _requisicion.IdRequisicion;
            requiscionEDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionEDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision;
            requiscionEDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionEDTO.FechaRevision = _requisicion.FechaRevision;
            requiscionEDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionEDTO.ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList());
            return requiscionEDTO;
        }
        public static RequisicionEDTO ToEDTOAutorizacion( Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionEDTO requiscionEDTO = new RequisicionEDTO();
            requiscionEDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionEDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionEDTO.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion;
            requiscionEDTO.FechaAutorizacion = _requisicion.FechaAutorizacion;
            requiscionEDTO.ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList());
            return requiscionEDTO;
        }
        #endregion
        #region FromEDTO
        public static Sagas.MainModule.Entidades.Requisicion FromEDTO(RequisicionCrearDTO requiscionEDTO)
        {
            Sagas.MainModule.Entidades.Requisicion _requisicion = new Sagas.MainModule.Entidades.Requisicion();
            _requisicion.IdUsuarioSolicitante = requiscionEDTO.IdUsuarioSolicitante;
            _requisicion.IdEmpresa = requiscionEDTO.IdEmpresa;
            _requisicion.NumeroRequisicion = Servicios.FolioServicio.GenerarNumeroRequisicion(requiscionEDTO);
            _requisicion.MotivoRequisicion = requiscionEDTO.MotivoRequisicion;
            _requisicion.RequeridoEn = requiscionEDTO.RequeridoEn;
            _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Creado;
            _requisicion.FechaRequerida = requiscionEDTO.FechaRequerida;
            _requisicion.FechaRegistro = requiscionEDTO.FechaRegistro;
            //_requisicion.Productos = RequisicionProductoAdapter.FromDTO(requiscionEDTO.ListaProductos);
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromEDTORevision(RequisicionEDTO requiscionEDTO)
        {
            Sagas.MainModule.Entidades.Requisicion _requisicion = new Sagas.MainModule.Entidades.Requisicion();
            _requisicion.IdRequisicion = requiscionEDTO.IdRequisicion;
            _requisicion.IdRequisicionEstatus = requiscionEDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = requiscionEDTO.IdUsuarioRevision;
            _requisicion.OpinionAlmacen = requiscionEDTO.OpinionAlmacen;
            _requisicion.FechaRevision = requiscionEDTO.FechaRevision;
            _requisicion.MotivoCancelacion = requiscionEDTO.MotivoCancelacion;
            _requisicion.Productos = RequisicionProductoAdapter.FromDTO(requiscionEDTO.ListaProductos);
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromEDTOAutorizacion(RequisicionEDTO requiscionEDTO)
        {
            Sagas.MainModule.Entidades.Requisicion _requisicion = new Sagas.MainModule.Entidades.Requisicion();
            _requisicion.IdRequisicionEstatus = requiscionEDTO.IdRequisicionEstatus;
            _requisicion.MotivoCancelacion = requiscionEDTO.MotivoCancelacion;
            _requisicion.IdUsuarioAutorizacion = requiscionEDTO.IdUsuarioAutorizacion;
            _requisicion.FechaAutorizacion = requiscionEDTO.FechaAutorizacion;
            _requisicion.Productos = RequisicionProductoAdapter.FromDTO(requiscionEDTO.ListaProductos);
            return _requisicion;
        }
        #endregion        
    }
}
