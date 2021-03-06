﻿using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using PagedList;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenesCompraModel
    {
        public string Numrequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public string NumeroOrdenCompra { get; set; }
        public int IdProveedor { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaRequeridaDe { get; set; }
        public DateTime FechaRequeridaA { get; set; }
        public DateTime FechaRegistroDe { get; set; }
        public DateTime FechaRegistroA { get; set; }
        public List<OrdenCompraDTO> OrdenesCompra { get; set; }
        public List<RequisicionDTO> Requisiciones { get; set; }
    }
}