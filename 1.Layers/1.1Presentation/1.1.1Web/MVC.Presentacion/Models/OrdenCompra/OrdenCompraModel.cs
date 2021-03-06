﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraModel
    {
        public int IdRequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public int IdSolicitante{ get; set; }
        public string Solicitante { get; set; }   
        public short IdEmpresa { get; set; } 
        public string Empresa { get; set; }           
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }     
        public bool EsGasTransporte { get; set; }
        public DateTime FechaRequisicion { get; set; }
        public List<ProductoOCDTO> OrdenCompraProductos { get; set; }      
    }
}