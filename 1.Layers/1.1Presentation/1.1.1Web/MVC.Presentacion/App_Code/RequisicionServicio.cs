using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Presentacion.Models.Seguridad;

namespace MVC.Presentacion.App_Code
{
    public static class RequisicionServicio
    {
        public static List<RequisicionDTO> BuscarRequisiciones(short idEmpresa, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisiciones(idEmpresa, token);
            return respuestaReq._listaRequisicion;
        }
        public static List<RequisicionEstatusDTO> BuscarRequisicionEstatus(string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicionEstatus(token);
            return respuestaReq._listaRequisicionEstatus;
        }
        public static RequisicionesModel InitRequisiciones(string _tok)
        {
            return new RequisicionesModel
            {               
                Estatus = BuscarRequisicionEstatus(_tok),
                Requisiciones = BuscarRequisiciones(TokenServicio.ObtenerIdEmpresa(_tok), _tok),
                Empresas = CatalogoServicio.Empresas(_tok)
            };
        }
        public static RequisicionesModel FiltrarRequisicones(RequisicionesModel model)
        {
            List<RequisicionDTO> newList = model.Requisiciones;
            #region Por estatus
            if (model.IdEstatus != 0)
                newList = newList.Where(x => x.IdRequisicionEstatus.ToString().Equals(model.IdEstatus)).ToList();
            #endregion

            #region Por Fecha de registro           
            if (model.FechaCreacionDe != null)
                newList = newList.Where(x => x.FechaRegistro >= Convert.ToDateTime(model.FechaCreacionDe)).ToList();
            if (model.FechaCreacionDe != null)
                newList = newList.Where(x => x.FechaRegistro <= Convert.ToDateTime(model.FechaCreacionA)).ToList();
            #endregion

            #region Por Fecha de sequisicion
            if (model.FechaRequeridaDe != null)
                newList = newList.Where(x => x.FechaRequerida >= Convert.ToDateTime(model.FechaRequeridaDe)).ToList();
            if (model.FechaRequeridaA != null)
                newList = newList.Where(x => x.FechaRequerida <= Convert.ToDateTime(model.FechaRequeridaA)).ToList();
            #endregion

            if (newList.Count.Equals(0))
                model.Requisiciones = newList;

            return model;
        }
        public static RequisicionModel InitRequisicion(string _tkn)
        {
            return new RequisicionModel()
            {
                FechaRequerida = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                Productos = CatalogoServicio.ListaProductos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn),
                CentrosCostos = CatalogoServicio.BuscarCentrosCosto(_tkn),
                RequisicionProductos = new List<RequisicionProductoNuevoDTO>(),
            };
        }
        public static RequisicionModel AgregarProducto(RequisicionModel model, string _tkn)
        {
            model.Productos = CatalogoServicio.ListaProductos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            model.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(_tkn);
            if (model.RequisicionProductos == null)
                model.RequisicionProductos = new List<RequisicionProductoNuevoDTO>();
            if (ValidarProductoRepetido(model))            
                foreach (var item in model.RequisicionProductos.Where((x => x.IdProducto.Equals(model.IdProducto)
                                       && x.IdCentroCosto.Equals(model.IdCentroCosto))))
                {
                    item.Cantidad = item.Cantidad + model.Cantidad;
                    item.Aplicacion = item.Aplicacion + "|" + model.Aplicacion;
                }            
            else            
                model.RequisicionProductos.Add(CrearProductoNuevo(model));
                  
            return model;
        }
        public static  RequisicionProductoNuevoDTO CrearProductoNuevo(RequisicionModel model)
        {
            var prod = model.Productos.FirstOrDefault(x => x.IdProducto.Equals(model.IdProducto));
            var cc = model.CentrosCostos.FirstOrDefault(x => x.IdCentroCosto.Equals(model.IdCentroCosto));
            return new RequisicionProductoNuevoDTO()
            {
                IdTipoProducto = model.IdTipoProducto,
                TipoProducto = model.IdTipoProducto.Equals(2) ? "Producto" : "Servicio",
                IdProducto = model.IdProducto,
                Producto = prod.Descripcion,
                Cantidad = model.Cantidad,
                Unidad = prod.UnidadMedida,
                IdCentroCosto = model.IdCentroCosto,
                CentroCosto = cc.Descripcion,
                Aplicacion = model.Aplicacion                
            };
        }
        private static bool ValidarProductoRepetido(RequisicionModel model)
        {
            bool resp = false;
            if (!model.Productos.Equals(0))
                resp = (model.RequisicionProductos.Exists(x => x.IdProducto.Equals(model.IdProducto) && x.IdCentroCosto.Equals(model.IdCentroCosto)));
            return resp;
        }
        public static RespuestaDTO GuardarRequisicion(RequisicionModel model, string _tkn)
        {
            RequisicionEDTO req = new RequisicionEDTO()
            {
                IdEmpresa = model.IdEmpresa,
                FechaRequerida = Convert.ToDateTime(model.FechaRequerida.ToShortDateString()),
                FechaRegistro = DateTime.Today,
                IdRequisicionEstatus = RequisicionEstatusEnum.Creada,
                IdUsuarioSolicitante = model.IdUsuarioSolicitante,
                MotivoRequisicion = model.MotivoRequisicion,
                RequeridoEn = model.RequeridoEn,
                ListaProductos = ToEDTO(model.RequisicionProductos)
            };
            return RespuestaRequisicionDTO(req, _tkn);
        }
        public static RespuestaDTO RespuestaRequisicionDTO(RequisicionEDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.GuardarRequisicon(Req, tkn);
            return respuestaReq._respuestaDTO;
        }
        public static List<RequisicionProductoEDTO> ToEDTO(List<RequisicionProductoNuevoDTO> modelPord)
        {
            return modelPord.Select(x => ToEDTO(x)).ToList();
        }
        public static RequisicionProductoEDTO ToEDTO(RequisicionProductoNuevoDTO modelPord)
        {
            return new RequisicionProductoEDTO()
            {
                IdProducto = modelPord.IdProducto,
                IdTipoProducto = modelPord.IdTipoProducto,
                IdCentroCosto = modelPord.IdCentroCosto,
                Cantidad = modelPord.Cantidad,
                Aplicacion = modelPord.Aplicacion
            };
        }
    }
}