using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public static class RequisicionEstatusEnum
    {
        public static byte Creada =  (byte)Estatus.Creada;
        public static byte En_revision = (byte)Estatus.En_revision;
        public static byte Revision_exitosa =  (byte)Estatus.Revision_exitosa;
        public static byte Revision_parcial =  (byte)Estatus.Revision_parcial;
        public static byte Revision_no_exitosa =  (byte)Estatus.Revision_no_exitosa;
        public static byte Autoriza_entrega = (byte)Estatus.Autoriza_entrega;
        public static byte Cerrada = (byte)Estatus.Cerrada;
        public static byte Orden_de_compra_generada =  (byte)Estatus.Orden_de_compra_generada;
        public static byte Solicitante_Notificado = (byte)Estatus.Solicitante_Notificado;
        public static byte Autorizacion_finalizada = (byte)Estatus.Autorizacion_finalizada;
    }
    public enum Estatus : byte
    {
        Creada = 1,
        En_revision = 2,
        Revision_exitosa = 3,
        Revision_parcial = 4,
        Revision_no_exitosa = 5,
        Autoriza_entrega = 6,
        Cerrada = 7,
        Orden_de_compra_generada = 8,
        Solicitante_Notificado = 9,
        Autorizacion_finalizada = 10
    }
}