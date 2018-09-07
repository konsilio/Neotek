using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                RequisicionProductos = new List<RequisicionProductoNuevoDTO>(),
                CentrosCostos = CatalogoServicio.BuscarCentrosCosto(_tkn),
                Productos = CatalogoServicio.ListaProductos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn),
                Empresas = CatalogoServicio.Empresas(_tkn),
                Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn),_tkn)
            };
        }
        public static RequisicionModel AgregarProducto(RequisicionProductoNuevoDTO prod, string _tkn)
        {
            var model = InitRequisicion(_tkn);
            if (model.RequisicionProductos == null)
                model.RequisicionProductos = new List<RequisicionProductoNuevoDTO>();
            model.RequisicionProductos.Add(prod);
          
            return model;
        }
    }
}