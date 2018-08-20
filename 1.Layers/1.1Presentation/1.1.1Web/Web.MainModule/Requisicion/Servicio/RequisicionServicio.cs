﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Web.MainModule.Agente;
using Web.MainModule.Seguridad.Model;
using Web.MainModule.Catalogos.Model;

namespace Web.MainModule.Requisicion.Servicio
{
    public class RequisicionServicio
    {
        #region Operadores
        public Model.RespuestaRequisicionDto GuardarRequisicion(Model.RequisicionEDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.GuardarRequisicon(Req, tkn);
            return respuestaReq._respuestaRequisicion;
        }
        public Model.RespuestaRequisicionDto ActualizarRequisicionRevision(Model.RequisicionRevPutDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.ActualizarRequisicionRevision(Req, tkn);
            return respuestaReq._respuestaRequisicion;
        }
        public Model.RespuestaRequisicionDto ActualizarRequisicionAutorizacion(Model.RequisicionAutPutDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.ActualizarRequisicionAutorizacion(Req, tkn);
            return respuestaReq._respuestaRequisicion;
        }
        public Model.RespuestaRequisicionDto CancalarRequisicion(Model.RequisicionCancelaDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.ActualizarRequisicionCancelar(Req, tkn);
            return respuestaReq._respuestaRequisicion;
        }
        public List<Model.RequisicionProductoDTO> GenerarLista(List<Model.RequisicionProductoDTO> lP, Model.RequisicionProductoDTO p)
        {
            lP.Add(p);
            return lP;
        }
        public Model.RequisicionProductoDTO CrearProductoLocal(int _idProducto, int _idTipoProducto, int _idCentroCosto, decimal _cantidad, string _aplicacion)
        {
            return new Model.RequisicionProductoDTO
            {
                IdProducto = _idProducto,
                IdTipoProducto = _idTipoProducto,
                IdCentroCosto = _idCentroCosto,
                Cantidad = _cantidad,
                Aplicacion = _aplicacion
            };
        }
        public Model.RequisicionProductoGridDTO GenerarProductoGrid(DropDownList _tipoProducto, DropDownList _producto, DropDownList _centroCosto, string _aplicacion, decimal _cantidad, List<Model.ProductoDTO> _prods)
        {
            Model.ProductoDTO _prod = _prods.SingleOrDefault(x => x.IdProducto.Equals(int.Parse(_producto.SelectedItem.Value)));
            return new Model.RequisicionProductoGridDTO
            {
                IdTipoProducto = int.Parse(_tipoProducto.SelectedItem.Value),
                TipoProducto = _tipoProducto.SelectedItem.Text,
                IdProducto = int.Parse(_producto.SelectedItem.Value),
                Producto = _producto.SelectedItem.Text,
                IdCentroCosto = int.Parse(_centroCosto.SelectedItem.Value),
                CentroCosto = _centroCosto.SelectedItem.Text,
                Cantidad = _cantidad,
                Aplicacion = _aplicacion,
                IdUnidad = _prod.IdUnidadMedida,
                Unidad = _prod.UnidadMedida,
            };
        }
        public List<Model.RequisicionProductoGridDTO> GenerarListaGrid(List<Model.RequisicionProductoGridDTO> LProductos, Model.RequisicionProductoGridDTO Producto, string Token)
        {
            LProductos.Add(Producto);
            //var lprodAso = ListaProductosAsociados(Producto.IdProducto, Token);
            //if (!lprodAso.Count.Equals(0))           
            //{
            //    foreach (var prod in lprodAso)
            //    {
            //        LProductos.Add(new Model.RequisicionProductoGridDTO
            //        {
            //            IdProducto = prod.IdProducto,
            //            IdTipoProducto = prod.IdProductoServicioTipo,
            //            TipoProducto = prod.TipoProducto,
            //            Producto = prod.Descripcion,
            //            IdCentroCosto = Producto.IdCentroCosto,
            //            CentroCosto = Producto.CentroCosto, //Falta Campo para definir Centro de costos en productos asociados
            //            Cantidad = 1, // Falta campo para definir el valror en productos asociados
            //            Aplicacion = Producto.Aplicacion,
            //            IdUnidad = prod.IdUnidadMedida,
            //            Unidad = prod.UnidadMedida
            //        });
            //    }               
            //}
            return LProductos;
        }
        public List<Model.RequisicionDTO> BuscarRequisiciones(short idEmpresa, string token)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.BuscarRequisiciones(idEmpresa, token);
            return respuestaReq._listaRequisiciones;
        }
        public Model.RequisicionAutorizacion BuscarRequisicionByNumRequiAuto(int numreq, string token)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.BuscarRequisicioAuto(numreq, token);
            return respuestaReq._requsicionAutorizacion;
        }
        public Model.RespuestaRequisicionDto EnviarNotificacion(int numreq, string token)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.EnviarNotificacion(numreq, token);
            return respuestaReq._respuestaRequisicion;
        }
        public Model.RequisicionRevisionDTO BuscarRequisicionByNumRequiRevi(int numreq, string token)
        {
            var respuestaReq = new AgenteServicios();
            respuestaReq.BuscarRequisicio(numreq, token);
            return respuestaReq._requisicionRevisionDTO;
        }
        public List<Model.UsuarioDTO> ListaUsuarios(short idEmpresa, string token)
        {
            var agente = new AgenteServicios();
            agente.BuscarListaUsuarios(idEmpresa, token);
            return agente._listUsuarios;
        }
        public List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicios();
            agente.ListaEmpresas(tkn);
            return agente._listaEmpresas;
        }
        public List<Model.ProductoDTO> ListaProductos(short idEmpresa, string Token)
        {
            var agente = new AgenteServicios();
            agente.BuscarProductos(idEmpresa, Token);
            return agente._listProductos;
        }
        public List<Model.ProductoDTO> ListaProductosAsociados(int idProdcuto, string Token)
        {
            var agente = new AgenteServicios();
            agente.BuscarProductosAsociados(idProdcuto, Token);
            return agente._listProductos;
        }
        public List<CentroCostoDTO> ListaCentroCostos(string Token)
        {
            var agente = new AgenteServicios();
            agente.BuscarCentrosCostos(Token);
            return agente._listaCentrosCostos;
        }      
        #endregion
        #region Adaptadores
        public Model.RequisicionProductoDTO ToDTO(Model.RequisicionProductoGridDTO _ReqGridDTO)
        {
            Model.RequisicionProductoDTO DTO = new Model.RequisicionProductoDTO()
            {
                IdProducto = _ReqGridDTO.IdProducto,
                IdTipoProducto = _ReqGridDTO.IdTipoProducto,
                IdCentroCosto = _ReqGridDTO.IdCentroCosto,
                Cantidad = _ReqGridDTO.Cantidad,
                Aplicacion = _ReqGridDTO.Aplicacion
            };
            return DTO;
        }
        public Model.RequisicionProductoEDTO ToEDTO(Model.RequisicionProductoDTO _ReqGridDTO)
        {
            Model.RequisicionProductoEDTO EDTO = new Model.RequisicionProductoEDTO()
            {
                IdRequisicion = _ReqGridDTO.IdRequisicion,
                IdProducto = _ReqGridDTO.IdProducto,
                IdTipoProducto = _ReqGridDTO.IdTipoProducto,
                IdCentroCosto = _ReqGridDTO.IdCentroCosto,
                Cantidad = _ReqGridDTO.Cantidad,
                Aplicacion = _ReqGridDTO.Aplicacion,
                RevisionFisica = _ReqGridDTO.RevisionFisica,
                CantidadAlmacenActual = _ReqGridDTO.CantidadAlmacenActual,
                CantidadAComprar = _ReqGridDTO.CantidadAComprar,
                AutorizaEntrega = _ReqGridDTO.AutorizaEntrega,
                AutorizaCompra = _ReqGridDTO.AutorizaCompra
            };
            return EDTO;
        }
        public Model.RequisicionProductoEDTO ToEDTO(Model.RequisicionProductoGridDTO _ReqGridDTO)
        {
            Model.RequisicionProductoEDTO EDTO = new Model.RequisicionProductoEDTO()
            {
                IdRequisicion = 0,
                IdProducto = _ReqGridDTO.IdProducto,
                IdTipoProducto = _ReqGridDTO.IdTipoProducto,
                IdCentroCosto = _ReqGridDTO.IdCentroCosto,
                Cantidad = _ReqGridDTO.Cantidad,
                Aplicacion = _ReqGridDTO.Aplicacion,
                RevisionFisica = false,
                CantidadAlmacenActual = 0,
                CantidadAComprar = 0,
                AutorizaEntrega = false,
                AutorizaCompra = false
            };
            return EDTO;
        }
        public Model.RequisicionEDTO UnirDtos(Model.RequisicionDTO _reqDto, List<Model.RequisicionProductoDTO> _lProdDto)
        {
            Model.RequisicionEDTO EDTO = new Model.RequisicionEDTO()
            {
                IdRequisicion = _reqDto.IdRequisicion,
                IdUsuarioSolicitante = _reqDto.IdUsuarioSolicitante,
                IdEmpresa = _reqDto.IdEmpresa,
                NumeroRequisicion = _reqDto.NumeroRequisicion,
                MotivoRequisicion = _reqDto.MotivoRequisicion,
                RequeridoEn = _reqDto.RequeridoEn,
                IdRequisicionEstatus = _reqDto.IdRequisicionEstatus,
                FechaRequerida = _reqDto.FechaRequerida,
                FechaRegistro = _reqDto.FechaRegistro,
                IdUsuarioRevision = _reqDto.IdUsuarioRevision,
                OpinionAlmacen = _reqDto.OpinionAlmacen,
                FechaRevision = _reqDto.FechaRevision,
                MotivoCancelacion = _reqDto.MotivoCancelacion,
                IdUsuarioAutorizacion = _reqDto.IdUsuarioAutorizacion,
                FechaAutorizacion = _reqDto.FechaAutorizacion,
                ListaProductos = ToEDTO(_lProdDto)
            };
            return EDTO;
        }
        public Model.RequisicionEDTO UnirDtos(Model.RequisicionCrearDTO _reqDto)
        {
            Model.RequisicionEDTO EDTO = new Model.RequisicionEDTO()
            {
                IdUsuarioSolicitante = _reqDto.IdUsuarioSolicitante,
                IdEmpresa = _reqDto.IdEmpresa,
                MotivoRequisicion = _reqDto.MotivoRequisicion,
                RequeridoEn = _reqDto.RequeridoEn,
                IdRequisicionEstatus = _reqDto.IdRequisicionEstatus,
                FechaRequerida = _reqDto.FechaRequerida,
                FechaRegistro = _reqDto.FechaRegistro,
                ListaProductos = ToEDTO(_reqDto.ListaProductos)
            };
            return EDTO;
        }
        public List<Model.RequisicionProductoDTO> ToDTO(List<Model.RequisicionProductoGridDTO> _reqProdDTO)
        {
            List<Model.RequisicionProductoDTO> reqProdDTO = _reqProdDTO.ToList().Select(x => ToDTO(x)).ToList();
            return reqProdDTO;
        }
        public List<Model.RequisicionProductoEDTO> ToEDTO(List<Model.RequisicionProductoDTO> _reqProdDTO)
        {
            List<Model.RequisicionProductoEDTO> reqProdDTO = _reqProdDTO.ToList().Select(x => ToEDTO(x)).ToList();
            return reqProdDTO;
        }
        public List<Model.RequisicionProductoEDTO> ToEDTO(List<Model.RequisicionProductoGridDTO> _reqProdDTO)
        {
            List<Model.RequisicionProductoEDTO> reqProdDTO = _reqProdDTO.ToList().Select(x => ToEDTO(x)).ToList();
            return reqProdDTO;
        }
        public Model.RequisicionProductoGridDTO ToGridDTO(Model.RequisicionProductoEDTO _reqProdEDTO)
        {
            Model.RequisicionProductoGridDTO GridDTO = new Model.RequisicionProductoGridDTO();

            GridDTO.IdProducto = _reqProdEDTO.IdProducto;
            GridDTO.Producto = "Nombre del produto o servicio";
            GridDTO.Unidad = "PZA";
            GridDTO.IdTipoProducto = _reqProdEDTO.IdTipoProducto;
            GridDTO.TipoProducto = _reqProdEDTO.IdTipoProducto.Equals(1) ? "Producto" : "Servicio";
            GridDTO.IdCentroCosto = _reqProdEDTO.IdCentroCosto;
            GridDTO.CentroCosto = "Centro de Costos";
            GridDTO.Cantidad = _reqProdEDTO.Cantidad;
            GridDTO.Aplicacion = _reqProdEDTO.Aplicacion;

            return GridDTO;
        }
        public List<Model.RequisicionProductoGridDTO> ToGridDTO(List<Model.RequisicionProductoEDTO> _reqProdEDTO)
        {
            List<Model.RequisicionProductoGridDTO> reqProdGridDTO = _reqProdEDTO.ToList().Select(x => ToGridDTO(x)).ToList();
            return reqProdGridDTO;
        }
        public List<Model.RequisicionProdReviPutDTO> ToProdPRevPutDTO(List<Model.RequisicionProductoRevisionDTO> LDTO)
        {
            List<Model.RequisicionProdReviPutDTO> LPutDTO = LDTO.ToList().Select(x => ToProdPRevPutDTO(x)).ToList();
            return LPutDTO;
        }
        public Model.RequisicionProdReviPutDTO ToProdPRevPutDTO(Model.RequisicionProductoRevisionDTO DTO)
        {
            Model.RequisicionProdReviPutDTO putDTO = new Model.RequisicionProdReviPutDTO();
            putDTO.IdProducto = DTO.IdProducto;
            putDTO.RevisionFisica = true;
            return putDTO;
        }
        #endregion
    }
}