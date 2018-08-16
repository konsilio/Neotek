using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Requisicion;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;

namespace Application.MainModule.AdaptadoresDTO.Requisicion
{
    public static class RequisicionAdapter
    {
        #region ToDTO
        public static RequisicionDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionDTO requiscionDTO = new RequisicionDTO()
            {
                IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                UsuarioSolicitante = UsuarioServicio.ObtenerNombreCompleto(_requisicion.Solicitante),
                IdEmpresa = _requisicion.IdEmpresa,
                NombreComercial = _requisicion.Empresa.NombreComercial,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                RequisicionEstatus = _requisicion.RequisicionEstatus.Estatus,
                FechaRequerida = _requisicion.FechaRequerida,
                FechaRegistro = _requisicion.FechaRegistro
            };
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTORevision(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            requiscionDTO.IdRequisicion = _requisicion.IdRequisicion;
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision.Value;
            requiscionDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionDTO.FechaRevision = _requisicion.FechaRevision.Value;
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTOAutorizacion(RequisicionDTO requiscionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionDTO.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion.Value;
            requiscionDTO.FechaAutorizacion = _requisicion.FechaAutorizacion.Value;
            return requiscionDTO;
        }
        public static List<RequisicionDTO> ToDTO(List<Sagas.MainModule.Entidades.Requisicion> _requisiciones)
        {
            List<RequisicionDTO> requisicionesDTO = _requisiciones.ToList().Select(x => ToDTO(x)).ToList();
            return requisicionesDTO;
        }
        public static RequisicionAutorizacionDTO ToAutDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionAutorizacionDTO _requisicionDTO = new RequisicionAutorizacionDTO();
            _requisicionDTO.IdRequisicion = _requisicion.IdRequisicion;
            _requisicionDTO.NumeroRequisicion = _requisicion.NumeroRequisicion;
            _requisicionDTO.IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante;
            _requisicionDTO.IdEmpresa = _requisicion.IdEmpresa;
            _requisicionDTO.MotivoRequisicion = _requisicion.MotivoRequisicion;
            _requisicionDTO.RequeridoEn = _requisicion.RequeridoEn;
            _requisicionDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            _requisicionDTO.FechaRequerida = _requisicion.FechaRequerida;
            _requisicionDTO.ListaProductos = RequisicionProductoAdapter.ToAutDTO(_requisicion.Productos.ToList());
            return _requisicionDTO;
        }
        #endregion
        #region FromDTO
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionCancelaDTO _requisicionDTO, Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            _requisicion.IdRequisicion = _requisicionDTO.IdRequisicion;
            _requisicion.IdRequisicionEstatus = _requisicionDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = _requisicionDTO.IdUsuarioRevision;           
            _requisicion.MotivoCancelacion = _requisicionDTO.MotivoCancelacion;
            if (_requisicionDTO.IdUsuarioRevision != 0)
            {
                _requisicion.IdUsuarioRevision = _requisicionDTO.IdUsuarioRevision;
                _requisicion.FechaRevision = _requisicionDTO.FechaAutorizacion;
            }
            else
            {
                _requisicion.IdUsuarioAutorizacion = _requisicionDTO.IdUsuarioAutorizacion;
                _requisicion.FechaAutorizacion = _requisicionDTO.FechaAutorizacion;
            }
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionCancelaDTO _requisicion)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {               
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoCancelacion = _requisicion.MotivoCancelacion,               
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,             
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
        //public static List<Sagas.MainModule.Entidades.Requisicion> FromDTO(List<RequisicionDTO> _requisiciones)
        //{
        //    List<Sagas.MainModule.Entidades.Requisicion> requisicionesDTO = _requisiciones.ToList().Select(x => FromDTO(x)).ToList();
        //    return requisicionesDTO;
        //}
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
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionRevisionDTO _requisicion)
        {
            Sagas.MainModule.Entidades.Requisicion requiscionDTO = new Sagas.MainModule.Entidades.Requisicion()
            {
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoCancelacion = _requisicion.MotivoCancelacion,
                OpinionAlmacen = _requisicion.OpinionAlmacen,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRevision = new System.DateTime(),
                Productos = RequisicionProductoAdapter.FromDTO(_requisicion.ListaProductos)
            };
            return requiscionDTO;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionRevPutDTO _requisicionDTO, Sagas.MainModule.Entidades.Requisicion entidadAnterior)
        {//Revision
            Sagas.MainModule.Entidades.Requisicion _requisicion = FromEntity(entidadAnterior);
            //_requisicion.NumeroRequisicion = _requisicionDTO.NumeroRequisicion;
            _requisicion.IdRequisicionEstatus = _requisicionDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = _requisicionDTO.IdUsuarioRevision;
            _requisicion.OpinionAlmacen = _requisicionDTO.OpinionAlmacen;
            _requisicion.FechaRevision = _requisicionDTO.FechaRevision;
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromDTO(RequisicionAutPutDTO _requisicionDTO, Sagas.MainModule.Entidades.Requisicion entidadAnterior)
        {//Autorizacion
            Sagas.MainModule.Entidades.Requisicion _requisicion = FromEntity(entidadAnterior);
            //_requisicion.NumeroRequisicion = _requisicionDTO.NumeroRequisicion;
            _requisicionDTO.IdUsuarioAutorizacion = _requisicionDTO.IdUsuarioAutorizacion;
            _requisicion.FechaAutorizacion = _requisicionDTO.FechaAutorizacion;
            _requisicion.IdRequisicionEstatus = _requisicionDTO.IdRequisicionEstatus;
            return _requisicion;
        }
        public static Sagas.MainModule.Entidades.Requisicion FromEntity(Sagas.MainModule.Entidades.Requisicion _entidadAnterior)
        {
            Sagas.MainModule.Entidades.Requisicion _endidad = new Sagas.MainModule.Entidades.Requisicion()
            {
                IdRequisicion = _entidadAnterior.IdRequisicion,
                IdUsuarioSolicitante = _entidadAnterior.IdUsuarioSolicitante,
                IdEmpresa = _entidadAnterior.IdEmpresa,
                NumeroRequisicion = _entidadAnterior.NumeroRequisicion,
                MotivoRequisicion = _entidadAnterior.MotivoRequisicion,
                RequeridoEn = _entidadAnterior.RequeridoEn,
                IdRequisicionEstatus = _entidadAnterior.IdRequisicionEstatus,
                FechaRequerida = _entidadAnterior.FechaRequerida,
                FechaRegistro = _entidadAnterior.FechaRegistro,
                IdUsuarioRevision = _entidadAnterior.IdUsuarioRevision,
                OpinionAlmacen = _entidadAnterior.OpinionAlmacen,
                FechaRevision = _entidadAnterior.FechaRevision,
                MotivoCancelacion = _entidadAnterior.MotivoCancelacion,
                IdUsuarioAutorizacion = _entidadAnterior.IdUsuarioAutorizacion,
                FechaAutorizacion = _entidadAnterior.FechaAutorizacion,
            };
            return _endidad;
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
        public static RequisicionRevisionDTO ToRevDTO(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionRevisionDTO requiscionEDTO = new RequisicionRevisionDTO
            {
                IdRequisicion = _requisicion.IdRequisicion,
                IdUsuarioSolicitante = _requisicion.IdUsuarioSolicitante,
                IdEmpresa = _requisicion.IdEmpresa,
                NumeroRequisicion = _requisicion.NumeroRequisicion,
                MotivoRequisicion = _requisicion.MotivoRequisicion,
                RequeridoEn = _requisicion.RequeridoEn,
                IdRequisicionEstatus = _requisicion.IdRequisicionEstatus,
                FechaRequerida = _requisicion.FechaRequerida,
                ListaProductos = RequisicionProductoAdapter.ToRevDTO(_requisicion.Productos.ToList())
            };
            return requiscionEDTO;
        }
        public static RequisicionEDTO ToEDTORevision(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionEDTO requiscionEDTO = new RequisicionEDTO();
            requiscionEDTO.IdRequisicion = _requisicion.IdRequisicion;
            requiscionEDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionEDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision.Value;
            requiscionEDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionEDTO.FechaRevision = _requisicion.FechaRevision.Value;
            requiscionEDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionEDTO.ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList());
            return requiscionEDTO;
        }
        public static RequisicionEDTO ToEDTOAutorizacion(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            RequisicionEDTO requiscionEDTO = new RequisicionEDTO();
            requiscionEDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionEDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionEDTO.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion.Value;
            requiscionEDTO.FechaAutorizacion = _requisicion.FechaAutorizacion.Value;
            requiscionEDTO.ListaProductos = RequisicionProductoAdapter.ToDTO(_requisicion.Productos.ToList());
            return requiscionEDTO;
        }
        #endregion
        #region FromEDTO
        public static Sagas.MainModule.Entidades.Requisicion FromEDTO(RequisicionEDTO requiscionEDTO)
        {
            Sagas.MainModule.Entidades.Requisicion _requisicion = new Sagas.MainModule.Entidades.Requisicion();
            _requisicion.IdUsuarioSolicitante = requiscionEDTO.IdUsuarioSolicitante;
            _requisicion.IdEmpresa = requiscionEDTO.IdEmpresa;
            _requisicion.NumeroRequisicion = Servicios.FolioServicio.GenerarNumeroRequisicion(requiscionEDTO);
            _requisicion.MotivoRequisicion = requiscionEDTO.MotivoRequisicion;
            _requisicion.RequeridoEn = requiscionEDTO.RequeridoEn;
            _requisicion.IdRequisicionEstatus = requiscionEDTO.IdRequisicionEstatus;
            _requisicion.FechaRequerida = requiscionEDTO.FechaRequerida;
            _requisicion.FechaRegistro = requiscionEDTO.FechaRegistro;
            _requisicion.Productos = RequisicionProductoAdapter.FromDTO(requiscionEDTO.ListaProductos);
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
