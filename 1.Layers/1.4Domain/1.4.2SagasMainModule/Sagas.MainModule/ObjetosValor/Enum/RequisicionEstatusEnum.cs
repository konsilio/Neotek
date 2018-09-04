using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class RequisicionEstatusEnum
    {

        public static byte Creada = (byte)ReqEstatusEnum.Creada;
        public static byte En_revision = (byte)ReqEstatusEnum.En_revision;
        public static byte Revision_exitosa = (byte)ReqEstatusEnum.Revision_exitosa;
        public static byte Revision_parcial = (byte)ReqEstatusEnum.Revision_parcial;
        public static byte Revision_no_exitosa = (byte)ReqEstatusEnum.Revision_no_exitosa;
        public static byte Autoriza_entrega = (byte)ReqEstatusEnum.Autoriza_entrega;
      public static byte Cerrada = (byte)ReqEstatusEnum.Cerrada;
        public static byte Orden_de_compra_generada = (byte)ReqEstatusEnum.Orden_de_compra_generada;
        public static byte Solicitante_Notificado = (byte)ReqEstatusEnum.Solicitante_Notificado;
        public static byte Autorizacion_finalizada = (byte)ReqEstatusEnum.Autoriza_entrega;
    }

    enum ReqEstatusEnum
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
