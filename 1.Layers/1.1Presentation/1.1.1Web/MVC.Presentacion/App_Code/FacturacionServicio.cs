using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Facturacion;
using System.Collections.Generic;

namespace MVC.Presentacion.App_Code
{
    public class FacturacionServicio
    {
        public static List<FacturacionModel> ObtenerInfoTicket(FacturacionModel _mod, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCargosFilter(_mod, tkn);
            return agente._ListainfoTicket;
        }
    }
}