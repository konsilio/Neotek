﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionAutorizacionModel
    {
        public int IdRequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public short IdEmpresa { get; set; }
        public DateTime FechaRequerida { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public byte RequisicionEstatus { get; set; }
        public string OpinionAlmacen { get; set; }
        public string MotivoCancelacion { get; set; }
        public List<RequisicionProductoAutorizacionDTO> Productos { get; set; }
    }
}