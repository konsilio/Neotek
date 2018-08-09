using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public static class RequisiconEstatus
    {
        public enum Estatus 
        {
            Creada = 3,
            En_revision = 4,
            Revision_exitosa = 5,
            Revision_parcial = 6,
            Revision_no_exitosa = 7,
            Autoriza_entrega = 8,
            Cerrada = 9,
            Orden_de_compra_generada = 9,
            Solicitante_Notificado = 10
        }
    }
}