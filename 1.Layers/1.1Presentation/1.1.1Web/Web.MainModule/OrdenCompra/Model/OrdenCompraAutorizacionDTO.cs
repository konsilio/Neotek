﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public class OrdenCompraAutorizacionDTO
    {
        public int IdOrdenCompra { get; set; }
        public int UsuarioAutoriza { get; set; }
    }
}