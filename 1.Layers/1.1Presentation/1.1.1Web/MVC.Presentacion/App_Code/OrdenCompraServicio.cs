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
                //  OrdenesCompra = 
            };
        }
        //public static List<OrdenCompraDTO> ObtenerOrdenesCompra(short idEmpresa, string Tkn)
        //{
        //    AgenteServicio agente = new AgenteServicio();
        //    agente.BuscarOrdenesCompra(idEmpresa, Tkn);
        //    return agente._listaOrdenCompraDTO;
        //}
    }
}