using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Transporte
{
    public static class TipoUnidadEqTransporteEnum
    {
        public static byte Camioneta = (byte)TipoUnidadEqTransporte.Camioneta;
        public static byte Pipa = (byte)TipoUnidadEqTransporte.Pipa;
        public static byte Utilitario = (byte)TipoUnidadEqTransporte.Utilitario;
    }
    public enum TipoUnidadEqTransporte
    {
        Camioneta = 1,
        Pipa = 2,
        Utilitario = 3,
    }
}