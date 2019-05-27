using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class DashBoard
    {
        public static string DashBoardRemanente(string token)
        {
            var agente = new AgenteServicio();
            agente.DashBoardRemanenteJson(token);
            return agente._Json;
        }
    }
}