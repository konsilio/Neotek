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
    }
}