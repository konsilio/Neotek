using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class CatalogoServicio
    {
        #region Empresas
        public static List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaEmpresasLogin(tkn);
            return agente._listaEmpresas;
        }
        #endregion
        #region Usuarios
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaUsuarios(idEmpresa, token);
            return agente._listaUsuarios;
        }
        #endregion
        #region Centro de Costo
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentroCosto;
        }
        //public static RespuestaDTO ModificarCentroCosto(CentroCostoModificarDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.ModificarCtroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO EliminarCentroCosto(CentroCostoEliminarDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.EliminarCtroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO NuevoCentroCosto(CentroCostoCrearDto dto, string Tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.GuardarCentroCosto(dto, Tkn);
        //    return agente._respuestaDTO;
        //}
        #endregion
        #region Porductos
        public static List<ProductoDTO> ListaProductos(short idEmpresa, string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductos(idEmpresa, Token);
            return agente._listProductos;
        }
        #endregion
    }
}