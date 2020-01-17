using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public static class OrdenCompraEstatusEnum
    {
        public static byte Espera_autorizacion = (byte)Estatus.Espera_autorizacion;
        public static byte Proceso_compra = (byte)Estatus.Proceso_compra;
        public static byte Compra_exitosa = (byte)Estatus.Compra_exitosa;
        public static byte Compra_cancelada = (byte)Estatus.Compra_cancelada;
        public static byte EnComplementoCompra = (byte)Estatus.EnComplementoCompra;
        public static byte SolicitudPago = (byte)Estatus.SolicitudPago;
        public static byte PagoConfirmado = (byte)Estatus.PagoConfirmado;
    }
    enum Estatus : byte
    {
        Espera_autorizacion = 2,
        Proceso_compra = 1,
        Compra_exitosa = 4,
        Compra_cancelada = 5,
        EnComplementoCompra = 6,
        SolicitudPago = 7,
        PagoConfirmado = 8
    }
}