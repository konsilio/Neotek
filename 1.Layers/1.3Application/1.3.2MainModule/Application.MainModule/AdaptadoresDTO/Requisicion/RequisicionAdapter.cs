using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.MainModule.AdaptadoresDTO.Requisiciones
{
    public static class RequisicionAdapter
    {
        #region ToDTO
        public static RequisicionDTO ToDTO(Requisicion _requisicion)
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
                FechaRegistro = _requisicion.FechaRegistro,
                EsExterno =_requisicion.EsExterno
            };
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTORevision(RequisicionDTO requiscionDTO, Requisicion _requisicion)
        {
            requiscionDTO.IdRequisicion = _requisicion.IdRequisicion;
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionDTO.IdUsuarioRevision = _requisicion.IdUsuarioRevision.Value;
            requiscionDTO.OpinionAlmacen = _requisicion.OpinionAlmacen;
            requiscionDTO.FechaRevision = _requisicion.FechaRevision.Value;
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            return requiscionDTO;
        }
        public static RequisicionDTO ToDTOAutorizacion(RequisicionDTO requiscionDTO, Requisicion _requisicion)
        {
            requiscionDTO.IdRequisicionEstatus = _requisicion.IdRequisicionEstatus;
            requiscionDTO.MotivoCancelacion = _requisicion.MotivoCancelacion;
            requiscionDTO.IdUsuarioAutorizacion = _requisicion.IdUsuarioAutorizacion.Value;
            requiscionDTO.FechaAutorizacion = _requisicion.FechaAutorizacion.Value;
            return requiscionDTO;
        }
        public static List<RequisicionDTO> ToDTO(List<Requisicion> _requisiciones)
        {
            return _requisiciones.Select(x => ToDTO(x)).ToList();
        }
        public static RequisicionAutorizacionDTO ToAutDTO(Requisicion _requisicion)
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
        public static RequisicionEstatusDTO ToDTO(RequisicionEstatus estatus)
        {
            return new RequisicionEstatusDTO()
            {
                IdRequisicionEstatus = estatus.IdRequisicionEstatus,
                Estatus = estatus.Estatus
            };
        }
        public static List<RequisicionEstatusDTO> ToDTO(List<RequisicionEstatus> lestatus)
        {
            return lestatus.Select(x => ToDTO(x)).ToList();
        }
        public static RequisicionRevisionDTO ToRevDTO(Requisicion _requisicion)
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
        #endregion
        #region FromDTO
        public static Requisicion FromDTO(RequisicionDTO dto)
        {
            return new Requisicion()
            {
                IdUsuarioSolicitante = dto.IdUsuarioSolicitante,
                IdEmpresa = dto.IdEmpresa,
                NumeroRequisicion = Servicios.FolioServicio.GenerarNumeroRequisicion(dto),
                MotivoRequisicion = dto.MotivoRequisicion,
                RequeridoEn = dto.RequeridoEn,
                IdRequisicionEstatus = dto.IdRequisicionEstatus,
                FechaRequerida = Convert.ToDateTime(dto.FechaRequerida.ToShortDateString()),
                FechaRegistro = dto.FechaRegistro,
                Productos = RequisicionProductoAdapter.FromDTO(dto.Productos),
                EsExterno = dto.EsExterno,
            };
        }        
        public static Requisicion FromDTORevision(RequisicionDTO requiscionDTO, Requisicion _requisicion)
        {
            _requisicion.IdRequisicion = requiscionDTO.IdRequisicion;
            _requisicion.IdRequisicionEstatus = requiscionDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = requiscionDTO.IdUsuarioRevision;
            _requisicion.OpinionAlmacen = requiscionDTO.OpinionAlmacen;
            _requisicion.FechaRevision = requiscionDTO.FechaRevision;
            _requisicion.MotivoCancelacion = requiscionDTO.MotivoCancelacion;
            return _requisicion;
        }
        public static Requisicion FromDTOAutorizacion(RequisicionDTO requiscionDTO, Requisicion _requisicion)
        {
            _requisicion.IdRequisicionEstatus = requiscionDTO.IdRequisicionEstatus;
            _requisicion.MotivoCancelacion = requiscionDTO.MotivoCancelacion;
            _requisicion.IdUsuarioAutorizacion = requiscionDTO.IdUsuarioAutorizacion;
            _requisicion.FechaAutorizacion = requiscionDTO.FechaAutorizacion;
            return _requisicion;
        }
        public static Requisicion UnirFromDTO(RequisicionDTO _requisicion, List<RequisicionProductoDTO> _prod)
        {
            Requisicion requiscionDTO = new Requisicion()
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
        public static Requisicion FromDTO(RequisicionRevisionDTO _requisicion)
        {
            Requisicion requiscionDTO = new Requisicion()
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
        public static Requisicion FromDTO(RequisicionRevPutDTO _requisicionDTO, Requisicion entidadAnterior)
        {//Revision
            Requisicion _requisicion = FromEntity(entidadAnterior);
            _requisicion.IdRequisicionEstatus = _requisicionDTO.IdRequisicionEstatus;
            _requisicion.IdUsuarioRevision = _requisicionDTO.IdUsuarioRevision;
            _requisicion.OpinionAlmacen = _requisicionDTO.OpinionAlmacen;
            _requisicion.FechaRevision = _requisicionDTO.FechaRevision;
            return _requisicion;
        }
        public static Requisicion FromDTO(RequisicionAutPutDTO _requisicionDTO, Requisicion entidadAnterior)
        {//Autorizacion
            Requisicion _requisicion = FromEntity(entidadAnterior);
            _requisicion.IdUsuarioAutorizacion = _requisicionDTO.IdUsuarioAutorizacion;
            _requisicion.FechaAutorizacion = _requisicionDTO.FechaAutorizacion;
            _requisicion.IdRequisicionEstatus = _requisicionDTO.IdRequisicionEstatus;

            return _requisicion;
        }
        public static Requisicion FromEntity(Requisicion _entidadAnterior)
        {
            Requisicion _endidad = new Requisicion()
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
                EsExterno = _entidadAnterior.EsExterno,
            };
            return _endidad;
        }
        public static RequisicionProducto FromEntity(RequisicionProducto _entidadAnterior)
        {
            return new RequisicionProducto()
            {
                IdRequisicion = _entidadAnterior.IdRequisicion,
                IdProducto = _entidadAnterior.IdProducto,
                Orden = _entidadAnterior.Orden,
                IdTipoProducto = _entidadAnterior.IdTipoProducto,
                IdCentroCosto = _entidadAnterior.IdCentroCosto,
                Cantidad = _entidadAnterior.Cantidad,
                Aplicacion = _entidadAnterior.Aplicacion,
                RevisionFisica = _entidadAnterior.RevisionFisica,
                CantidadAlmacenActual = _entidadAnterior.CantidadAlmacenActual,
                CantidadAComprar = _entidadAnterior.CantidadAComprar,
                AutorizaEntrega = _entidadAnterior.AutorizaEntrega,
                AutorizaCompra = _entidadAnterior.AutorizaCompra,
                EsActivoVenta = _entidadAnterior.EsActivoVenta,
                EsGas = _entidadAnterior.EsGas,
                EsTransporteGas = _entidadAnterior.EsTransporteGas,
            };
        }
        public static List<RequisicionProducto> FromEntity(List<RequisicionProducto> lProdDTO)
        {
            return lProdDTO.ToList().Select(x => FromEntity(x)).ToList();
        }
        public static Requisicion FromEntity(Requisicion _entidadAnterior, List<RequisicionProducto> _productos)
        {
            Requisicion _endidad = FromEntity(_entidadAnterior);
            _endidad.Productos = FromEntity(_productos);
            return _endidad;
        }
        public static RepRequisicionDTO ToRepDTO(Requisicion entidad, List<RequisicionProducto> entidadp)
        {
            return new RepRequisicionDTO()
            {
                NumRequisicion = entidad.NumeroRequisicion,
                Departamento = entidadp[0].CentroCosto.Descripcion,
                Requisicion = RequisicionServicio.ListaProductos(entidadp),
                Estatus = entidad.RequisicionEstatus.Estatus,
                Fecha = entidad.FechaRegistro,               
            };
        }

        #endregion
    }
}
