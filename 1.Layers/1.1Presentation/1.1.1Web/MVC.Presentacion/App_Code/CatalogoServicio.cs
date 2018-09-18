using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using Security.MainModule.Criptografia;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Utilities.MainModule;
//using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public static EmpresaModel guardarLogosEmpresa(EmpresaModel Objemp, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
            if (UrlLogotipo180px != null || UrlLogotipo500px != null || UrlLogotipo1000px != null)
            {
                try
                {
                    string destinationFolder = ConfigurationManager.AppSettings["GuardarLogoEmpresa"];
                    string destinationFolderSave =  Convertir.GetPhysicalPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]);

                    //Checking file is available to save.  
                    if (UrlLogotipo180px != null)
                    {

                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo180px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo180px.FileName));
                        UrlLogotipo180px.SaveAs(pathSave);
                        Objemp.UrlLogotipo180px = pathBD;
                    }

                    if (Objemp.UrlLogotipo500px != null)
                    {
                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo500px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo500px.FileName));
                        UrlLogotipo500px.SaveAs(pathSave);
                        Objemp.UrlLogotipo500px = pathBD;
                    }

                    if (Objemp.UrlLogotipo1000px != null)
                    {
                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo1000px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo500px.FileName));
                        UrlLogotipo1000px.SaveAs(pathSave);
                        Objemp.UrlLogotipo1000px = pathBD;
                    }
                }
                catch (Exception ex)
                {
                   ex.ToString();
                }
            }
            return Objemp;
        }

        public static EmpresaDTO guardarLogosEmpresaDto(EmpresaDTO Objemp, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
            if (UrlLogotipo180px != null || UrlLogotipo500px != null || UrlLogotipo1000px != null)
            {
                try
                {
                    string destinationFolder = ConfigurationManager.AppSettings["GuardarLogoEmpresa"];
                    string destinationFolderSave = Convertir.GetPhysicalPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]);

                    //Checking file is available to save.  
                    if (UrlLogotipo180px != null)
                    {

                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo180px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo180px.FileName));
                        UrlLogotipo180px.SaveAs(pathSave);
                        Objemp.UrlLogotipo180px = pathBD;
                    }

                    if (Objemp.UrlLogotipo500px != null)
                    {
                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo500px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo500px.FileName));
                        UrlLogotipo500px.SaveAs(pathSave);
                        Objemp.UrlLogotipo500px = pathBD;
                    }

                    if (Objemp.UrlLogotipo1000px != null)
                    {
                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo1000px.FileName));
                        string pathSave = Path.Combine(destinationFolderSave, Path.GetFileName(UrlLogotipo500px.FileName));
                        UrlLogotipo1000px.SaveAs(pathSave);
                        Objemp.UrlLogotipo1000px = pathBD;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return Objemp;
        }

        public static RespuestaDTO create(EmpresaModel cc, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px, string tkn)
        {
            guardarLogosEmpresa(cc, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px);
        #region Empresas    
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
        public static RespuestaDTO ActualizaEdicionEmpresa(EmpresaDTO cc, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px, string tkn)
        {
            guardarLogosEmpresaDto(cc, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px);
            var agente = new AgenteServicio();
            agente.GuardarEmpresaEdicion(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO EliminaEmpresaSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarEmpresa(id, tkn);
            return agente._respuestaDTO;
        }

        public static List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaEmpresasLogin(tkn);
            return agente._listaEmpresas;
        }
           

        //consulta empresa mediante id
        public static Empresa FiltrarEmpresa(Empresa model, int id, string tkn)
        {
            List<EmpresaDTO> newList = Empresas(tkn).Where(x => x.IdEmpresa == id).ToList();
                       
            if (newList.Count != 0)
                model.Empresas = newList;

            return model;
        }
        #endregion

        #region Usuarios

        public static UsuarioDTO encryptaPWD(UsuarioDTO mod)
        {
            if (!String.IsNullOrEmpty(mod.Password))
            {
                mod.Password = SHA.GenerateSHA256String(mod.Password);
            }
            return mod;
        }
        public static UsuariosModel AgregarIdRolToList(UsuariosModel cc, string token)
        {
           var Nombre = ObtenerRolesId(cc.IdRol, token);
            RolDto rol = new RolDto();
            rol.IdRol = cc.IdRol;
            rol.NombreRol = Nombre[0].NombreRol;
            rol.Rol1 = Nombre[0].NombreRol;
            List<RolDto> Roles = new List<RolDto>();
            Roles.Add(rol);

            cc.Roles = Roles;
            return cc;
        }
        public static List<UsuariosModel> ObtenerTodosUsuarios(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(token);
            return agente._lstUserEmp;
        }

        public static List<UsuariosModel> ObtenerIdUsuario(int id,string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarUsuarioId(id,token);
            return agente._lstUserEmp;
        }

        public static RespuestaDTO CrearUsuario(UsuarioDTO cc, string tkn)
        {
            encryptaPWD(cc);
            var agente = new AgenteServicio();
            agente.GuardarNuevoUsuario(cc, tkn);
            return agente._respuestaDTO;
        }
             
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaUsuarios(idEmpresa, token);
            return agente._listaUsuarios;
        }
        public static RespuestaDTO ActualizaCredencialesUser(UsuarioDTO cc, string tkn)
        {
            encryptaPWD(cc);
            var agente = new AgenteServicio();
            agente.GuardarCredenciales(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO AgregarRolAlUsuario(UsuariosModel cc, string tkn)
        {
            AgregarIdRolToList(cc,tkn);          
            var agente = new AgenteServicio();
            agente.GuardarRolesAsig(cc, tkn);
            return agente._respuestaDTO;
        }

        public static List<UsuariosModel> ObtenerUsuariosRol(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(token);
            return agente._lstUserEmp;
        }
        public static RespuestaDTO ActualizaEdicionUsuario(UsuarioDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarUsuarioEdicion(cc, tkn);
            return agente._respuestaDTO;
        }
        
        public static RespuestaDTO EliminaUsuarioSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarUsuario(id, tkn);
            return agente._respuestaDTO;
        }

        public static List<UsuariosModel> FiltrarBusquedaUsuario(UsuariosModel us, string token)
        {
            var agente = new AgenteServicio();
            agente.FiltrarUsuarios(us.IdEmpresa,us.IdUsuario, us.Email1,token);
            return agente._lstUserEmp;
           
        }
            
        #endregion

        #region Roles
        public static List<RolDto> ObtenerTodosRoles(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosRoles(token);
            return agente._lstaAllRoles;
        }

        public static List<RolDto> ObtenerRolesId(int id,string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolId(id,token);
            return agente._lstaAllRoles;
        }

        public static RespuestaDTO AgregarRoles(RolDto cc, string tkn)
        {            
            var agente = new AgenteServicio();
            agente.GuardarNuevoRol(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO ActualizaNombreRol(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarModificacionRol(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO ActualizaPermisos(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarPermisos(cc, tkn);
            return agente._respuestaDTO;
        }

        public static RespuestaDTO EliminaRolSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarRol(id, tkn);
            return agente._respuestaDTO;
        }
        #endregion

        #region Clientes
        public static List<TipoPersonaModel> ObtenerTiposPersona(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTiposPersona(token);
            return agente._lstaTipoPersona;
        }
        public static List<RegimenFiscalModel> ObtenerRegimenFiscal(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRegimenFiscal(token);
            return agente._lstaRegimenFiscal;
        }

        public static List<ClientesDto> ListaClientes(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaClientes(idEmpresa, token);
            return agente._lstaClientes;
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
        #region Proveedores
        public static List<ProveedorDTO> CargarProveedores(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarProveedores(Tkn);
            return agente._listaProveedores;
        }
        public static List<SelectListItem> ProveedoresSelectList(List<ProveedorDTO> lprov)
        {
            List<SelectListItem> nlprov = new List<SelectListItem>();
            foreach (var prov in lprov)
            {
                nlprov.ToList().Add(new SelectListItem { Value = prov.IdProveedor.ToString(), Text = prov.NombreComercial });
            }
            return nlprov;
        }
        #endregion
        #region Cuentas contables      
        //public static RespuestaDTO GuardarCtaCtble(CuentaContableCrearDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.GuardarCuentaContable(cc, tkn);
        //    return agente._respuestaDTO;
        //}

        public static List<CuentaContableDTO> ListaCtaCtble(short idEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCuentasContables(idEmpresa, tkn);
            return agente._listaCuentasContables;
        }
        //public static RespuestaDTO EliminarCtaContable(CuentaContableEliminarDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.EliminarCtaCtble(cc, tkn);
        //    return agente._respuestaDTO;
        //}
        //public static RespuestaDTO ModificarCtaContable(CuentaContableModificarDto cc, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.ModificarCtaCtble(cc, tkn);
        //    return agente._respuestaDTO;
        //}
        #endregion
        #region Impuestos
        public static List<SelectListItem> ListaIVA()
        {
            List<SelectListItem> Ivas = new List<SelectListItem>();
            Ivas.Add(new SelectListItem { Value = "0", Text = "0%" });
            Ivas.Add(new SelectListItem { Value = "10", Text = "10%" });
            Ivas.Add(new SelectListItem { Value = "16", Text = "16%" });
            return Ivas;
        }
        public static List<SelectListItem> ListaIEPS()
        {
            List<SelectListItem> Ieps = new List<SelectListItem>();
            Ieps.Add(new SelectListItem { Value = "0", Text = "0%" });
            Ieps.Add(new SelectListItem { Value = "4", Text = "4%" });
            Ieps.Add(new SelectListItem { Value = "11", Text = "11%" });

            return Ieps;
        }
        #endregion
    }
}