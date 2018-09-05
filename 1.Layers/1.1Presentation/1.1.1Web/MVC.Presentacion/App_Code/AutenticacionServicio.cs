﻿using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{ 
    public static class AutenticacionServicio
    {
        public static RespuestaAutenticacionDto Autenticar(short _rs, string _us, string _pwr)
        {
            var agente = new AgenteServicio();
            agente.Acceder(new AutenticacionDTO() { IdEmpresa = _rs, Password = _pwr, Usuario = _us });
            return agente._respuestaAutenticacion;
        }
        public static List<EmpresaDTO> EmpresasLogin()
        {
            var agente = new AgenteServicio();
            agente.ListaEmpresasLogin();
            return agente._listaEmpresas;
        }
    }
}