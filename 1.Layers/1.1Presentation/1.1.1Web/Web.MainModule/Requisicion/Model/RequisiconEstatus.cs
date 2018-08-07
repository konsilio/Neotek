using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public class RequisiconEstatus
    {
        enum Estatus : short
        {
            Creada = 1,
            En_revision = 2,
            Revision_exitosa = 3,
            Revision_parcial = 4,
            Revision_no_exitosa = 5,
            Autoriza_entrega = 6,
            Cerrada = 7,
            Orden_de_compra_generada = 8,
            Solicitante_Notificado = 9
        }
    }
}