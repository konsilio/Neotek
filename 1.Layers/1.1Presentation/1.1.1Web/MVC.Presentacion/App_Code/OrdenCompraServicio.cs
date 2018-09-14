using MVC.Presentacion.Agente;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class OrdenCompraServicio
    {
        public static OrdenesCompraModel InitOrdenesCompra(string tkn)
        {
            return new OrdenesCompraModel()
            {
                FechaRequeridaDe = DateTime.Now,
                FechaRequeridaA = DateTime.Now,
                FechaRegistroDe = DateTime.Now,
                FechaRegistroA = DateTime.Now,
                Requisiciones = RequisicionServicio.BuscarRequisiciones(TokenServicio.ObtenerIdEmpresa(tkn), tkn),
                OrdenesCompra = ObtenerOrdenesCompra(TokenServicio.ObtenerIdEmpresa(tkn), tkn)
            };
        }
        public static List<OrdenCompraDTO> ObtenerOrdenesCompra(short idEmpresa, string Tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarOrdenesCompra(idEmpresa, Tkn);
            return agente._listaOrdenCompra;
        }
        public static OrdenCompraModel InitOrdenCompra(int id, string _tkn)
        {
            OrdenCompraModel model = new OrdenCompraModel();
            var datos = DatosRequisicion(id, _tkn);
            if (datos != null)
            {
                model.IdRequisicion = datos.IdRequisicion;
                model.NumRequisicion = datos.NumeroRequisicion;
                model.IdSolicitante = datos.IdUsuarioSolicitante;
                model.Solicitante = datos.UsuarioSolicitante;
                model.RequeridoEn = datos.RequeridoEn;
                model.MotivoCompra = datos.MotivoRequisicion;
                model.IdEmpresa = datos.IdEmpresa;
                model.NombreEmpresa = datos.NombreComercial;
                model.FechaRequisicion = datos.FechaRequerida;
                model.OrdenCompraProductos = datos.Productos;
            }
            return model;
        }
        public static List<OrdenCompraEstatusDTO> ListaEstatus(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarOrdenCompraEstatus(tkn);
            return agente._listaOrdenCompraEstatus;
        }
        public static RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarRequisicionOC(idReq, Tkn);
            return agente._requisicionOrdenCompra;
        }
        #region Adaptadores
        
        #endregion

    }
}