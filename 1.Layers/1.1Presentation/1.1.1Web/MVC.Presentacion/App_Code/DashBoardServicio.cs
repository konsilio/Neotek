using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class DashBoardServicio
    {
        public static AdministracionDTO DashBoardRemanente(string token)
        {
            var agente = new AgenteServicio();
            agente.DashBoardRemanenteJson(token);
            return agente._AdministracionDTO;
        }
        public static string DashBoardCallCenter(string token)
        {
            var agente = new AgenteServicio();
            agente.GetJsonCallCenter(token);
            return agente._Json;
        }
        public static AndenDTO DashBoardAnden(string token)
        {
            var agente = new AgenteServicio();
            agente.DashAnden(token);
            return agente._AndenDTO;
        }
    }
}