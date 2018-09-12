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
            //DatosRequisicion(id, _tkn);

            //dgListaproductos.DataSource = ViewState["ListaProdcutoOC"] = reqDto.Productos;
            //dgListaproductos.Visible = true;
            //dgListaproductos.DataBind();
            //if (ValidarRequisiconGas())
            //{
            //    dgListaproductos.Columns[8].Visible = false;
            //    dgListaproductos.Columns[9].Visible = false;
            //    dgListaproductos.Columns[10].Visible = false;
            //    dgListaproductos.Columns[11].Visible = false;
            //    dgListaproductos.Columns[12].Visible = false;
            //}
            return model;
        }
        //public RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        //{
        //    AgenteServicio agente = new AgenteServicio();
        //    agente.BuscarRequisicioOC(idReq, Tkn);
        //    return agente._requisicion;
        //}

    }
}