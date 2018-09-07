﻿using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionModel : RequisicionProductoNuevoDTO
    {
        public short IdEmpresa { get; set; }
        public DateTime FechaRequerida { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public RequisicionRevisionDTO RequisicionRevicion {get; set;}
        public List<RequisicionProductoNuevoDTO> RequisicionProductos { get; set; }       
        public List<CentroCostoDTO> CentrosCostos { get; set; }
        public List<ProductoDTO> Productos { get; set; }
    }
}