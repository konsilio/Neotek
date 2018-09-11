using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
//using System.Web.Script.Serialization;

namespace MVC.Presentacion.App_Code
{
    public static class CatalogoServicio
    {
   
        #region Paises
        public static List<PaisModel> GetPaises(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarPaises(tkn);
            return agente._listaPaises;
        }
        #endregion
        #region Estados
        public static List<EstadosRepModel> GetEstados(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarEstados(tkn);
            return agente._listaEstados;
        }
        #endregion
        #region Empresas
        //public bool create(EmpresaDTO Objemp)
        //{
        //    try
        //    {
        //        //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmpresaDTO));
        //        //MemoryStream mem = new MemoryStream();
        //        //ser.WriteObject(mem, Objemp);
        //        //string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
        //        //WebClient webClient = new WebClient();
        //        //webClient.Headers["Content-type"] = "application/json";
        //        //webClient.Encoding = Encoding.UTF8;
        //        //webClient.UploadString(_URL + "create", "POST", data);

        //        //return true;
        //        var agente = new AgenteServicio();
        //        agente.GuardarEmpresaNueva(Objemp);

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static RespuestaDTO create(EmpresaModel cc, string tkn)
        {            
            var agente = new AgenteServicio();
            agente.GuardarEmpresaNueva(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO ActualizaConfigEmpresa(EmpresaConfiguracion cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEmpresaConfiguracion(cc, tkn);
            return agente._respuestaDTO;
        }
        public static List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaEmpresasLogin(tkn);
            return agente._listaEmpresas;
        }

        //public static List<EmpresaConfiguracion> EmpresasC(string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.ListaEmpresasLogin(tkn);
        //    return agente._listaEmpresas;
        //}

        public static Empresa FiltrarEmpresa(Empresa model, int id, string tkn)
        {
            List<EmpresaDTO> newList = Empresas(tkn).Where(x => x.IdEmpresa == id).ToList();//model.Empresas;
                             

            if (newList.Count != 0)
                model.Empresas = newList;

            return model;
        }
        #endregion
        #region Usuarios

        public static List<UsuarioDTO> ObtenerTodosUsuarios(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(token);
            return agente._listaUsuarios;
        }
        //public static UsuariosModel UsuarioEmpresa(int id, string tkn)
        //{
        //    ////List<UsuarioDTO> newList = ListaUsuarios(tkn).Where(x => x.IdEmpresa == id).ToList();//model.Empresas;


        //    ////if (newList.Count != 0)
        //    ////    model.Empresas = newList;

        //    ////return model;
        //}


        //public static UsuarioDTO FiltrarUsuarios(int id, string tkn)
        //{
        //    List<UsuarioDTO> newList = ListaUsuarios(tkn).Where(x => x.IdEmpresa == id).ToList();//model.Empresas;


        //    if (newList.Count != 0)
        //        model.Empresas = newList;

        //    return model;
        //}
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
        public static List<ProductoDTO> ListaProductos(string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductos(Token);
            return agente._listProductos;
        }
        #endregion
    }
}