﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraComplementoDTO : OrdenCompraDTO
    {
        public decimal MontoAPagar { get; set; }
        public List<ProductoOCDTO> ProductoOCDTO { get; set; }
    }
}