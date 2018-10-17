using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exceptions.MainModule.Validaciones;

namespace MVC.Presentacion.App_Code
{ 
    public static class AutenticacionServicio
    {
        public static RespuestaAutenticacionDto Autenticar(short _rs, string _us, string _pwr)
        {
            if(_rs < 1 || string.IsNullOrEmpty(_us) || string.IsNullOrEmpty(_pwr))
                return new RespuestaAutenticacionDto()
                    {
                        Exito = false,
                        Mensaje = Error.S0003,
                        token = string.Empty
                    };

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
        public static LoginModel InitIndex(LoginModel model)
        {            
            if (model == null)
                model = new LoginModel
                {
                    Empresas = EmpresasLogin(),
                };
            else
                model.Empresas = EmpresasLogin();
            return model;
        }
        public static LoginModel InitIndex(RespuestaAutenticacionDto respuesta)
        {
            var model = new LoginModel();
            model = InitIndex(model);
            model.Respuesta = respuesta;
            return model;
        }
    }
}