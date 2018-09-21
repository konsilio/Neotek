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

            if (!String.IsNullOrEmpty(Objemp.EstadoProvincia))
            {
                Objemp.IdEstadoRep = null;
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
            var agente = new AgenteServicio();
            agente.GuardarEmpresaNueva(cc, tkn);
            return agente._RespuestaDTO;
        }   

        public static RespuestaDTO ActualizaConfigEmpresa(EmpresaConfiguracion cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEmpresaConfiguracion(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ActualizaEdicionEmpresa(EmpresaDTO cc, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px, string tkn)
        {
            guardarLogosEmpresaDto(cc, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px);
            var agente = new AgenteServicio();
            agente.GuardarEmpresaEdicion(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO EliminaEmpresaSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarEmpresa(id, tkn);
            return agente._RespuestaDTO;
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
        public static List<UsuariosModel> ObtenerTodosUsuarios(int id ,string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(id,token);
            return agente._lstUserEmp;
        }

        public static List<UsuariosModel> ObtenerIdUsuario(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarUsuarioId(id, token);
            return agente._lstUserEmp;
        }

        public static RespuestaDTO CrearUsuario(UsuarioDTO cc, string tkn)
        {
            encryptaPWD(cc);
            var agente = new AgenteServicio();
            agente.GuardarNuevoUsuario(cc, tkn);
            return agente._RespuestaDTO;
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
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO AgregarRolAlUsuario(UsuariosModel cc, string tkn)
        {
            AgregarIdRolToList(cc, tkn);
            var agente = new AgenteServicio();
            agente.GuardarRolesAsig(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static List<UsuariosModel> ObtenerUsuariosRol(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(0,token);
            return agente._lstUserEmp;
        }
        public static RespuestaDTO ActualizaEdicionUsuario(UsuarioDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarUsuarioEdicion(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO EliminaUsuarioSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarUsuario(id, tkn);
            return agente._RespuestaDTO;
        }

        public static List<UsuariosModel> FiltrarBusquedaUsuario(UsuariosModel us, string token)
        {
            var agente = new AgenteServicio();
            agente.FiltrarUsuarios(us.IdEmpresa, us.IdUsuario, us.Email1, token);
            return agente._lstUserEmp;

        }

        public static RespuestaDTO EliminarRolAlUsuario(UsuariosModel cc, string tkn)
        {
           // AgregarIdRolToList(cc, tkn);
            var agente = new AgenteServicio();
            agente.EliminarRolesAsig(cc, tkn);
            return agente._RespuestaDTO;
        }
        #endregion

        #region Roles

        public static List<RolDto> PermisosRequisicion(List<RolRequsicion> lst)
        {
            List<RolDto> Roles = new List<RolDto>();

            RolDto _lstc = new RolDto();
            _lstc.Activo = lst[0].Activo;
            _lstc.IdRol = lst[0].IdRol;
            _lstc.Rol1 = lst[0].Rol1;
            _lstc.NombreRol = lst[0].NombreRol;
            _lstc.IdEmpresa = lst[0].IdEmpresa;

            _lstc.RequisicionVerRequisiciones = lst[0].RequisicionVerRequisiciones;
            _lstc.RequisicionGenerarNueva = lst[0].RequisicionGenerarNueva;
            _lstc.RequisicionRevisarExistencia = lst[0].RequisicionRevisarExistencia;
            _lstc.RequisicionAutorizar = lst[0].RequisicionAutorizar;
            /*//////////////////*/
            _lstc.CatInsertarUsuario = lst[0].CatInsertarUsuario;
            _lstc.CatModificarUsuario = lst[0].CatModificarUsuario;
            _lstc.CatEliminarUsuario = lst[0].CatEliminarUsuario;
            _lstc.CatConsultarUsuario = lst[0].CatConsultarUsuario;
            _lstc.CatInsertarProveedor = lst[0].CatInsertarProveedor;
            _lstc.CatModificarProveedor = lst[0].CatModificarProveedor;
            _lstc.CatEliminarProveedor = lst[0].CatEliminarProveedor;
            _lstc.CatConsultarProveedor = lst[0].CatConsultarProveedor;
            _lstc.CatInsertarProducto = lst[0].CatInsertarProducto;
            _lstc.CatModificarProducto = lst[0].CatModificarProducto;
            _lstc.CatEliminarProducto = lst[0].CatEliminarProducto;
            _lstc.CatConsultarProducto = lst[0].CatConsultarProducto;
            _lstc.CatInsertarCentroCosto = lst[0].CatInsertarCentroCosto;
            _lstc.CatModificarCentroCosto = lst[0].CatModificarCentroCosto;
            _lstc.CatEliminarCentroCosto = lst[0].CatEliminarCentroCosto;
            _lstc.CatConsultarCentroCosto = lst[0].CatConsultarCentroCosto;
            _lstc.CatInsertarCuentaContable = lst[0].CatInsertarCuentaContable;
            _lstc.CatModificarCuentaContable = lst[0].CatModificarCuentaContable;
            _lstc.CatEliminarCuentaContable = lst[0].CatEliminarCuentaContable;
            _lstc.CatConsultarCuentaContable = lst[0].CatConsultarCuentaContable;
            _lstc.CatInsertarCliente = lst[0].CatInsertarCliente;
            _lstc.CatModificarCliente = lst[0].CatModificarCliente;
            _lstc.CatEliminarCliente = lst[0].CatEliminarCliente;
            _lstc.CatConsultarCliente = lst[0].CatConsultarCliente;
            _lstc.CatAsignarChoferPuntoVenta = lst[0].CatAsignarChoferPuntoVenta;
            _lstc.CatEliminarPuntoVenta = lst[0].CatEliminarPuntoVenta;
            _lstc.CatConsultarPuntoVenta = lst[0].CatConsultarPuntoVenta;
            _lstc.CatAsignarEquipoTransporte = lst[0].CatAsignarEquipoTransporte;
            _lstc.CatModificarEquipoTransporte = lst[0].CatModificarEquipoTransporte;
            _lstc.CatEliminarEquipoTransporte = lst[0].CatEliminarEquipoTransporte;
            _lstc.CatConsultarEquipoTransporte = lst[0].CatConsultarEquipoTransporte;
            /**/
            _lstc.CompraVerOCompra = lst[0].CompraVerOCompra;
            _lstc.CompraGenerarOCompra = lst[0].CompraGenerarOCompra;
            _lstc.CompraAutorizarOCompra = lst[0].CompraAutorizarOCompra;
            _lstc.CompraEntraProductoOCompra = lst[0].CompraEntraProductoOCompra;
            _lstc.CompraAtiendeServicioOCompra = lst[0].CompraAtiendeServicioOCompra;
            /**/

            _lstc.AppCompraVerOCompra = lst[0].AppCompraVerOCompra;
            _lstc.AppCompraEntraGas = lst[0].AppCompraEntraGas;
            _lstc.AppCompraGasIniciarDescarga = lst[0].AppCompraGasIniciarDescarga;
            _lstc.AppCompraGasFinalizarDescarga = lst[0].AppCompraGasFinalizarDescarga;
            _lstc.AppAutoconsumoInventarioGral = lst[0].AppAutoconsumoInventarioGral;
            _lstc.AppAutoconsumoEstacionCarb = lst[0].AppAutoconsumoEstacionCarb;
            _lstc.AppAutoconsumoPipa = lst[0].AppAutoconsumoPipa;
            _lstc.AppCalibracionEstacionCarb = lst[0].AppCalibracionEstacionCarb;
            _lstc.AppCalibracionPipa = lst[0].AppCalibracionPipa;
            _lstc.AppCalibracionCamionetaCilindro = lst[0].AppCalibracionCamionetaCilindro;
            _lstc.AppRecargaEstacionCarb = lst[0].AppRecargaEstacionCarb;
            _lstc.AppRecargaPipa = lst[0].AppRecargaPipa;
            _lstc.AppRecargaCamionetaCilindro = lst[0].AppRecargaCamionetaCilindro;
            _lstc.AppTomaLecturaAlmacenPral = lst[0].AppTomaLecturaAlmacenPral;
            _lstc.AppTomaLecturaEstacionCarb = lst[0].AppTomaLecturaEstacionCarb;
            _lstc.AppTomaLecturaPipa = lst[0].AppTomaLecturaPipa;
            _lstc.AppTomaLecturaCamionetaCilindro = lst[0].AppTomaLecturaCamionetaCilindro;
            _lstc.AppTomaLecturaReporteDelDia = lst[0].AppTomaLecturaReporteDelDia;
            _lstc.AppTraspasoEstacionCarb = lst[0].AppTraspasoEstacionCarb;
            _lstc.AppTraspasoPipa = lst[0].AppTraspasoPipa;
            Roles.Add(_lstc);
                       

            return Roles;

        }

        public static List<RolDto> PermisosMovilCompra(List<RolMovilCompra> lst)
        {
            List<RolDto> Roles = new List<RolDto>();

            RolDto _lstc = new RolDto();
            _lstc.Activo = lst[0].Activo;
            _lstc.IdRol = lst[0].IdRol;
            _lstc.Rol1 = lst[0].Rol1;
            _lstc.NombreRol = lst[0].NombreRol;
            _lstc.IdEmpresa = lst[0].IdEmpresa;

            _lstc.AppCompraVerOCompra = lst[0].AppCompraVerOCompra;
            _lstc.AppCompraEntraGas = lst[0].AppCompraEntraGas;
            _lstc.AppCompraGasIniciarDescarga = lst[0].AppCompraGasIniciarDescarga;
            _lstc.AppCompraGasFinalizarDescarga = lst[0].AppCompraGasFinalizarDescarga;
            _lstc.AppAutoconsumoInventarioGral = lst[0].AppAutoconsumoInventarioGral;
            _lstc.AppAutoconsumoEstacionCarb = lst[0].AppAutoconsumoEstacionCarb;
            _lstc.AppAutoconsumoPipa = lst[0].AppAutoconsumoPipa;
            _lstc.AppCalibracionEstacionCarb = lst[0].AppCalibracionEstacionCarb;
            _lstc.AppCalibracionPipa = lst[0].AppCalibracionPipa;
            _lstc.AppCalibracionCamionetaCilindro = lst[0].AppCalibracionCamionetaCilindro;
            _lstc.AppRecargaEstacionCarb = lst[0].AppRecargaEstacionCarb;
            _lstc.AppRecargaPipa = lst[0].AppRecargaPipa;
            _lstc.AppRecargaCamionetaCilindro = lst[0].AppRecargaCamionetaCilindro;
            _lstc.AppTomaLecturaAlmacenPral = lst[0].AppTomaLecturaAlmacenPral;
            _lstc.AppTomaLecturaEstacionCarb = lst[0].AppTomaLecturaEstacionCarb;
            _lstc.AppTomaLecturaPipa = lst[0].AppTomaLecturaPipa;
            _lstc.AppTomaLecturaCamionetaCilindro = lst[0].AppTomaLecturaCamionetaCilindro;
            _lstc.AppTomaLecturaReporteDelDia = lst[0].AppTomaLecturaReporteDelDia;
            _lstc.AppTraspasoEstacionCarb = lst[0].AppTraspasoEstacionCarb;
            _lstc.AppTraspasoPipa = lst[0].AppTraspasoPipa;
            /*********/
            _lstc.RequisicionVerRequisiciones = lst[0].RequisicionVerRequisiciones;
            _lstc.RequisicionGenerarNueva = lst[0].RequisicionGenerarNueva;
            _lstc.RequisicionRevisarExistencia = lst[0].RequisicionRevisarExistencia;
            _lstc.RequisicionAutorizar = lst[0].RequisicionAutorizar;
            /*//////////////////*/
            _lstc.CatInsertarUsuario = lst[0].CatInsertarUsuario;
            _lstc.CatModificarUsuario = lst[0].CatModificarUsuario;
            _lstc.CatEliminarUsuario = lst[0].CatEliminarUsuario;
            _lstc.CatConsultarUsuario = lst[0].CatConsultarUsuario;
            _lstc.CatInsertarProveedor = lst[0].CatInsertarProveedor;
            _lstc.CatModificarProveedor = lst[0].CatModificarProveedor;
            _lstc.CatEliminarProveedor = lst[0].CatEliminarProveedor;
            _lstc.CatConsultarProveedor = lst[0].CatConsultarProveedor;
            _lstc.CatInsertarProducto = lst[0].CatInsertarProducto;
            _lstc.CatModificarProducto = lst[0].CatModificarProducto;
            _lstc.CatEliminarProducto = lst[0].CatEliminarProducto;
            _lstc.CatConsultarProducto = lst[0].CatConsultarProducto;
            _lstc.CatInsertarCentroCosto = lst[0].CatInsertarCentroCosto;
            _lstc.CatModificarCentroCosto = lst[0].CatModificarCentroCosto;
            _lstc.CatEliminarCentroCosto = lst[0].CatEliminarCentroCosto;
            _lstc.CatConsultarCentroCosto = lst[0].CatConsultarCentroCosto;
            _lstc.CatInsertarCuentaContable = lst[0].CatInsertarCuentaContable;
            _lstc.CatModificarCuentaContable = lst[0].CatModificarCuentaContable;
            _lstc.CatEliminarCuentaContable = lst[0].CatEliminarCuentaContable;
            _lstc.CatConsultarCuentaContable = lst[0].CatConsultarCuentaContable;
            _lstc.CatInsertarCliente = lst[0].CatInsertarCliente;
            _lstc.CatModificarCliente = lst[0].CatModificarCliente;
            _lstc.CatEliminarCliente = lst[0].CatEliminarCliente;
            _lstc.CatConsultarCliente = lst[0].CatConsultarCliente;
            _lstc.CatAsignarChoferPuntoVenta = lst[0].CatAsignarChoferPuntoVenta;
            _lstc.CatEliminarPuntoVenta = lst[0].CatEliminarPuntoVenta;
            _lstc.CatConsultarPuntoVenta = lst[0].CatConsultarPuntoVenta;
            _lstc.CatAsignarEquipoTransporte = lst[0].CatAsignarEquipoTransporte;
            _lstc.CatModificarEquipoTransporte = lst[0].CatModificarEquipoTransporte;
            _lstc.CatEliminarEquipoTransporte = lst[0].CatEliminarEquipoTransporte;
            _lstc.CatConsultarEquipoTransporte = lst[0].CatConsultarEquipoTransporte;
            /**/
            _lstc.CompraVerOCompra = lst[0].CompraVerOCompra;
            _lstc.CompraGenerarOCompra = lst[0].CompraGenerarOCompra;
            _lstc.CompraAutorizarOCompra = lst[0].CompraAutorizarOCompra;
            _lstc.CompraEntraProductoOCompra = lst[0].CompraEntraProductoOCompra;
            _lstc.CompraAtiendeServicioOCompra = lst[0].CompraAtiendeServicioOCompra;

            Roles.Add(_lstc);

            return Roles;

        }

        public static List<RolDto> PermisosCompra(List<RolCompras> lst)
        {
            List<RolDto> Roles = new List<RolDto>();

            RolDto _lstc = new RolDto();
            _lstc.Activo = lst[0].Activo;
            _lstc.IdRol = lst[0].IdRol;
            _lstc.Rol1 = lst[0].Rol1;
            _lstc.NombreRol = lst[0].NombreRol;
            _lstc.IdEmpresa = lst[0].IdEmpresa;

            _lstc.CompraVerOCompra = lst[0].CompraVerOCompra;
            _lstc.CompraGenerarOCompra = lst[0].CompraGenerarOCompra;
            _lstc.CompraAutorizarOCompra = lst[0].CompraAutorizarOCompra;
            _lstc.CompraEntraProductoOCompra = lst[0].CompraEntraProductoOCompra;
            _lstc.CompraAtiendeServicioOCompra = lst[0].CompraAtiendeServicioOCompra;
            /*******/

            _lstc.AppCompraVerOCompra = lst[0].AppCompraVerOCompra;
            _lstc.AppCompraEntraGas = lst[0].AppCompraEntraGas;
            _lstc.AppCompraGasIniciarDescarga = lst[0].AppCompraGasIniciarDescarga;
            _lstc.AppCompraGasFinalizarDescarga = lst[0].AppCompraGasFinalizarDescarga;
            _lstc.AppAutoconsumoInventarioGral = lst[0].AppAutoconsumoInventarioGral;
            _lstc.AppAutoconsumoEstacionCarb = lst[0].AppAutoconsumoEstacionCarb;
            _lstc.AppAutoconsumoPipa = lst[0].AppAutoconsumoPipa;
            _lstc.AppCalibracionEstacionCarb = lst[0].AppCalibracionEstacionCarb;
            _lstc.AppCalibracionPipa = lst[0].AppCalibracionPipa;
            _lstc.AppCalibracionCamionetaCilindro = lst[0].AppCalibracionCamionetaCilindro;
            _lstc.AppRecargaEstacionCarb = lst[0].AppRecargaEstacionCarb;
            _lstc.AppRecargaPipa = lst[0].AppRecargaPipa;
            _lstc.AppRecargaCamionetaCilindro = lst[0].AppRecargaCamionetaCilindro;
            _lstc.AppTomaLecturaAlmacenPral = lst[0].AppTomaLecturaAlmacenPral;
            _lstc.AppTomaLecturaEstacionCarb = lst[0].AppTomaLecturaEstacionCarb;
            _lstc.AppTomaLecturaPipa = lst[0].AppTomaLecturaPipa;
            _lstc.AppTomaLecturaCamionetaCilindro = lst[0].AppTomaLecturaCamionetaCilindro;
            _lstc.AppTomaLecturaReporteDelDia = lst[0].AppTomaLecturaReporteDelDia;
            _lstc.AppTraspasoEstacionCarb = lst[0].AppTraspasoEstacionCarb;
            _lstc.AppTraspasoPipa = lst[0].AppTraspasoPipa;
            /*********/
            _lstc.RequisicionVerRequisiciones = lst[0].RequisicionVerRequisiciones;
            _lstc.RequisicionGenerarNueva = lst[0].RequisicionGenerarNueva;
            _lstc.RequisicionRevisarExistencia = lst[0].RequisicionRevisarExistencia;
            _lstc.RequisicionAutorizar = lst[0].RequisicionAutorizar;
            /*//////////////////*/
            _lstc.CatInsertarUsuario = lst[0].CatInsertarUsuario;
            _lstc.CatModificarUsuario = lst[0].CatModificarUsuario;
            _lstc.CatEliminarUsuario = lst[0].CatEliminarUsuario;
            _lstc.CatConsultarUsuario = lst[0].CatConsultarUsuario;
            _lstc.CatInsertarProveedor = lst[0].CatInsertarProveedor;
            _lstc.CatModificarProveedor = lst[0].CatModificarProveedor;
            _lstc.CatEliminarProveedor = lst[0].CatEliminarProveedor;
            _lstc.CatConsultarProveedor = lst[0].CatConsultarProveedor;
            _lstc.CatInsertarProducto = lst[0].CatInsertarProducto;
            _lstc.CatModificarProducto = lst[0].CatModificarProducto;
            _lstc.CatEliminarProducto = lst[0].CatEliminarProducto;
            _lstc.CatConsultarProducto = lst[0].CatConsultarProducto;
            _lstc.CatInsertarCentroCosto = lst[0].CatInsertarCentroCosto;
            _lstc.CatModificarCentroCosto = lst[0].CatModificarCentroCosto;
            _lstc.CatEliminarCentroCosto = lst[0].CatEliminarCentroCosto;
            _lstc.CatConsultarCentroCosto = lst[0].CatConsultarCentroCosto;
            _lstc.CatInsertarCuentaContable = lst[0].CatInsertarCuentaContable;
            _lstc.CatModificarCuentaContable = lst[0].CatModificarCuentaContable;
            _lstc.CatEliminarCuentaContable = lst[0].CatEliminarCuentaContable;
            _lstc.CatConsultarCuentaContable = lst[0].CatConsultarCuentaContable;
            _lstc.CatInsertarCliente = lst[0].CatInsertarCliente;
            _lstc.CatModificarCliente = lst[0].CatModificarCliente;
            _lstc.CatEliminarCliente = lst[0].CatEliminarCliente;
            _lstc.CatConsultarCliente = lst[0].CatConsultarCliente;
            _lstc.CatAsignarChoferPuntoVenta = lst[0].CatAsignarChoferPuntoVenta;
            _lstc.CatEliminarPuntoVenta = lst[0].CatEliminarPuntoVenta;
            _lstc.CatConsultarPuntoVenta = lst[0].CatConsultarPuntoVenta;
            _lstc.CatAsignarEquipoTransporte = lst[0].CatAsignarEquipoTransporte;
            _lstc.CatModificarEquipoTransporte = lst[0].CatModificarEquipoTransporte;
            _lstc.CatEliminarEquipoTransporte = lst[0].CatEliminarEquipoTransporte;
            _lstc.CatConsultarEquipoTransporte = lst[0].CatConsultarEquipoTransporte;
            Roles.Add(_lstc);

            return Roles;
        }
        public static List<RolDto> TODto(List<RolCat> lst)
        {
            List<RolDto> Roles = new List<RolDto>();

            RolDto _lstc = new RolDto();
            _lstc.Activo = lst[0].Activo;
            _lstc.IdRol = lst[0].IdRol;
            _lstc.Rol1 = lst[0].Rol1;
            _lstc.NombreRol = lst[0].NombreRol;
            _lstc.IdEmpresa = lst[0].IdEmpresa;
            _lstc.CatInsertarUsuario = lst[0].CatInsertarUsuario;
            _lstc.CatModificarUsuario = lst[0].CatModificarUsuario;
            _lstc.CatEliminarUsuario = lst[0].CatEliminarUsuario;
            _lstc.CatConsultarUsuario = lst[0].CatConsultarUsuario;
            _lstc.CatInsertarProveedor = lst[0].CatInsertarProveedor;
            _lstc.CatModificarProveedor = lst[0].CatModificarProveedor;
            _lstc.CatEliminarProveedor = lst[0].CatEliminarProveedor;
            _lstc.CatConsultarProveedor = lst[0].CatConsultarProveedor;
            _lstc.CatInsertarProducto = lst[0].CatInsertarProducto;
            _lstc.CatModificarProducto = lst[0].CatModificarProducto;
            _lstc.CatEliminarProducto = lst[0].CatEliminarProducto;
            _lstc.CatConsultarProducto = lst[0].CatConsultarProducto;
            _lstc.CatInsertarCentroCosto = lst[0].CatInsertarCentroCosto;
            _lstc.CatModificarCentroCosto = lst[0].CatModificarCentroCosto;
            _lstc.CatEliminarCentroCosto = lst[0].CatEliminarCentroCosto;
            _lstc.CatConsultarCentroCosto = lst[0].CatConsultarCentroCosto;
            _lstc.CatInsertarCuentaContable = lst[0].CatInsertarCuentaContable;
            _lstc.CatModificarCuentaContable = lst[0].CatModificarCuentaContable;
            _lstc.CatEliminarCuentaContable = lst[0].CatEliminarCuentaContable;
            _lstc.CatConsultarCuentaContable = lst[0].CatConsultarCuentaContable;
            _lstc.CatInsertarCliente = lst[0].CatInsertarCliente;
            _lstc.CatModificarCliente = lst[0].CatModificarCliente;
            _lstc.CatEliminarCliente = lst[0].CatEliminarCliente;
            _lstc.CatConsultarCliente = lst[0].CatConsultarCliente;
            _lstc.CatAsignarChoferPuntoVenta = lst[0].CatAsignarChoferPuntoVenta;
            _lstc.CatEliminarPuntoVenta = lst[0].CatEliminarPuntoVenta;
            _lstc.CatConsultarPuntoVenta = lst[0].CatConsultarPuntoVenta;
            _lstc.CatAsignarEquipoTransporte = lst[0].CatAsignarEquipoTransporte;
            _lstc.CatModificarEquipoTransporte = lst[0].CatModificarEquipoTransporte;
            _lstc.CatEliminarEquipoTransporte = lst[0].CatEliminarEquipoTransporte;
            _lstc.CatConsultarEquipoTransporte = lst[0].CatConsultarEquipoTransporte;

            /***********************/
            _lstc.CompraVerOCompra = lst[0].CompraVerOCompra;
            _lstc.CompraGenerarOCompra = lst[0].CompraGenerarOCompra;
            _lstc.CompraAutorizarOCompra = lst[0].CompraAutorizarOCompra;
            _lstc.CompraEntraProductoOCompra = lst[0].CompraEntraProductoOCompra;
            _lstc.CompraAtiendeServicioOCompra = lst[0].CompraAtiendeServicioOCompra;
            /*******/

            _lstc.AppCompraVerOCompra = lst[0].AppCompraVerOCompra;
            _lstc.AppCompraEntraGas = lst[0].AppCompraEntraGas;
            _lstc.AppCompraGasIniciarDescarga = lst[0].AppCompraGasIniciarDescarga;
            _lstc.AppCompraGasFinalizarDescarga = lst[0].AppCompraGasFinalizarDescarga;
            _lstc.AppAutoconsumoInventarioGral = lst[0].AppAutoconsumoInventarioGral;
            _lstc.AppAutoconsumoEstacionCarb = lst[0].AppAutoconsumoEstacionCarb;
            _lstc.AppAutoconsumoPipa = lst[0].AppAutoconsumoPipa;
            _lstc.AppCalibracionEstacionCarb = lst[0].AppCalibracionEstacionCarb;
            _lstc.AppCalibracionPipa = lst[0].AppCalibracionPipa;
            _lstc.AppCalibracionCamionetaCilindro = lst[0].AppCalibracionCamionetaCilindro;
            _lstc.AppRecargaEstacionCarb = lst[0].AppRecargaEstacionCarb;
            _lstc.AppRecargaPipa = lst[0].AppRecargaPipa;
            _lstc.AppRecargaCamionetaCilindro = lst[0].AppRecargaCamionetaCilindro;
            _lstc.AppTomaLecturaAlmacenPral = lst[0].AppTomaLecturaAlmacenPral;
            _lstc.AppTomaLecturaEstacionCarb = lst[0].AppTomaLecturaEstacionCarb;
            _lstc.AppTomaLecturaPipa = lst[0].AppTomaLecturaPipa;
            _lstc.AppTomaLecturaCamionetaCilindro = lst[0].AppTomaLecturaCamionetaCilindro;
            _lstc.AppTomaLecturaReporteDelDia = lst[0].AppTomaLecturaReporteDelDia;
            _lstc.AppTraspasoEstacionCarb = lst[0].AppTraspasoEstacionCarb;
            _lstc.AppTraspasoPipa = lst[0].AppTraspasoPipa;
            /*********/
            _lstc.RequisicionVerRequisiciones = lst[0].RequisicionVerRequisiciones;
            _lstc.RequisicionGenerarNueva = lst[0].RequisicionGenerarNueva;
            _lstc.RequisicionRevisarExistencia = lst[0].RequisicionRevisarExistencia;
            _lstc.RequisicionAutorizar = lst[0].RequisicionAutorizar;
            Roles.Add(_lstc);

            return Roles;
        }
        public static RolDto AddPermisosCat(RolDto cc)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (TODto(cc.ListaRolesCat));
                      
            cc.ListaRoles = Roles;
            return cc;
        }
        public static RolDto AddPermisosCom(RolDto cc)
        {            
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosCompra(cc.ListaRolesCom));

            cc.ListaRoles = Roles;
            return cc;
        }

        public static RolDto AddPermisosReq(RolDto cc)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosRequisicion(cc.ListaRequsicion));

            cc.ListaRoles = Roles;
            return cc;
        }

        public static RolDto AddPermisosMovilCom(RolDto cc)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosMovilCompra(cc.ListaMovilCompra));

            cc.ListaRoles = Roles;
            return cc;
        }
        public static List<RolDto> ObtenerTodosRoles(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosRoles(token);
            return agente._lstaAllRoles;
        }

        public static List<RolCat> ObtenerRolesCat(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolesCat(token);
            return agente._lstaRolesCat;
        }

        public static List<RolCompras> ObtenerRolesCom(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolesCompras(token);
            return agente._lstaRolesCom;
        }
        public static List<RolMovilCompra> ObtenerRolesMovilCompra(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolesMovilCompras(token);
            return agente._lstaRolesMovilCom;
        }

        public static List<RolRequsicion> ObtenerRolesReq(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolesRequisicion(token);
            return agente._lstaRolesReq;
        }
        
        public static List<RolDto> ObtenerRolesId(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarRolId(id, token);
            return agente._lstaAllRoles;
        }

        public static RespuestaDTO AgregarRoles(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoRol(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO ActualizaNombreRol(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarModificacionRol(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO ActualizaPermisos(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            if (cc.ListaRolesCat != null)
            {
                AddPermisosCat(cc);
                agente.GuardarPermisos(cc, tkn);
            }

            if (cc.ListaRolesCom != null)
            {
                AddPermisosCom(cc);
                agente.GuardarPermisos(cc, tkn);
            }

            if (cc.ListaRequsicion != null)
            {
                AddPermisosReq(cc);
                agente.GuardarPermisos(cc, tkn);
            }

            if (cc.ListaMovilCompra != null)
            {
                AddPermisosMovilCom(cc);
                agente.GuardarPermisos(cc, tkn);
            }

            return agente._RespuestaDTO;

        }

        public static RespuestaDTO EliminaRolSel(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarRol(id, tkn);
            return agente._RespuestaDTO;
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

        public static List<ClientesDto> ListaClientes(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaClientes(id,token);
            return agente._lstaClientes;
        }

        public static RespuestaDTO CrearCliente(ClientesModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoCliente(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO ModificarCliente(ClientesDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EditarCliente(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO EliminarCliente(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCliente(id, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO RegistraLocaciones(ClienteLocacionMod cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarClienteLocacion(cc, tkn);
            return agente._RespuestaDTO;
        }
        #endregion

        #region Centro de Costo
        public static CentroCostoModel InitCentroCosto(string Tkn)
        {
            return new CentroCostoModel()
            {
                CentrosCostos = BuscarCentrosCosto(Tkn)
            };
        }
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentroCosto;
        }
        public static List<TipoCentroCostoDTO> BuscarTipoCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaTipoCentroCosto(Tkn);
            return agente._listaTipoCentroCosto;
        }
        private static RespuestaDTO ModificarCentroCosto(CentroCostoModificarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EditarCentroCosto(CentroCostoModel model, string tkn)
        {
            CentroCostoModificarDTO dto = new CentroCostoModificarDTO()
            {
                IdCentroCosto = model.IdCentroCosto,
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                IdTipoCentroCosto = model.IdTipoCentroCosto
            };
            if (!model.IdEquipoTransporte.Equals(0)) dto.IdEquipoTransporte = model.IdEquipoTransporte;
            if (!model.IdEstacionCarburacion.Equals(0)) dto.IdEstacionCarburacion = model.IdEstacionCarburacion;
            if (!model.IdCAlmacenGas.Equals(0)) dto.IdCAlmacenGas = model.IdCAlmacenGas;
            return ModificarCentroCosto(dto, tkn);
        }
        public static CentroCostoModel ActivarModificar(int idcc, CentroCostoModel model, string tkn)
        {
            if (model.CentrosCostos == null)
                model = InitCentroCosto(tkn);
            var cc = model.CentrosCostos.SingleOrDefault(x => x.IdCentroCosto.Equals(idcc));
            model.Numero = cc.Numero;
            model.IdCentroCosto = cc.IdCentroCosto;
            model.Descripcion = cc.Descripcion;
            if (!cc.IdEquipoTransporte.Equals(0)) model.IdEquipoTransporte = cc.IdEquipoTransporte;
            if (!cc.IdEstacionCarburacion.Equals(0)) model.IdEstacionCarburacion = cc.IdEstacionCarburacion;
            if (!cc.IdCAlmacenGas.Equals(0)) model.IdCAlmacenGas = cc.IdCAlmacenGas;
            return model;
        }
        private static RespuestaDTO EliminarCentroCosto(CentroCostoEliminarDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCtroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO BorrarCentroCosto(int id, string tkn)
        {
            return EliminarCentroCosto(new CentroCostoEliminarDTO { IdCentroCosto = id }, tkn);
        }
        private static RespuestaDTO NuevoCentroCosto(CentroCostoCrearDTO dto, string Tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarCentroCosto(dto, Tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO CrearCentroCosto(CentroCostoModel model, string tkn)
        {
            CentroCostoCrearDTO dto = new CentroCostoCrearDTO()
            {
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                IdTipoCentroCosto = model.IdTipoCentroCosto
            };
            dto.IdEquipoTransporte = model.IdEquipoTransporte;
            dto.IdEstacionCarburacion = model.IdEstacionCarburacion;
            dto.IdCAlmacenGas = model.IdCAlmacenGas;
            dto.IdCamioneta = 0;
            dto.IdCilindro = 0;
            dto.IdPipa = 0;
            dto.IdVehiculoUtilitario = 0;
            dto.IdTipoCentroCosto = 1;
            return NuevoCentroCosto(dto, tkn);
        }
        #endregion

        #region Porductos
        public static List<ProductoDTO> ListaProductos(string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductos(Token);
            return agente._listaProductos;
        }
        public static RespuestaDTO CrearProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarProducto(ProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarProducto(dto, tkn);
            return agente._RespuestaDTO;
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
        public static RespuestaDTO GuardarCuentaContable(CuentaContableModel model, string tkn)
        {
            CuentaContableCrearDTO ccC = new CuentaContableCrearDTO()
            {
                Numero = model.Numero,
                Descripcion = model.Descripcion,
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn),
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString())
            };
            return GuardarCtaCtble(ccC, tkn);
        }
        private static RespuestaDTO GuardarCtaCtble(CuentaContableCrearDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarCuentaContable(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static List<CuentaContableDTO> ListaCtaCtble(short idEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCuentasContables(idEmpresa, tkn);
            return agente._listaCuentasContables;
        }
        private static RespuestaDTO EliminarCtaContable(CuentaContableEliminarDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCtaCtble(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO BorrarCuentaContable(int idCC, string tkn)
        {
            return EliminarCtaContable(new CuentaContableEliminarDTO { IdCuenta = idCC }, tkn);
        }
        private static RespuestaDTO ModificarCtaContable(CuentaContableModificarDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCtaCtble(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EditarCuentaContable(CuentaContableModel model, string tkn)
        {
            CuentaContableModificarDTO ccm = new CuentaContableModificarDTO()
            {
                IdCuentaContable = model.IdCuentaContable,
                IdEmpresa = model.IdEmpresa,
                Descripcion = model.Descripcion,
                Numero = model.Numero,
                Activo = true
            };
            return ModificarCtaContable(ccm, tkn);
        }
        public static CuentaContableModel InitCtaContable(string tkn)
        {
            return new CuentaContableModel()
            { CuentasContables = ListaCtaCtble(TokenServicio.ObtenerIdEmpresa(tkn), tkn) };
        }
        public static CuentaContableModel ActivarModifiarCuentaContable(int idcc, CuentaContableModel model, string tkn)
        {
            if (model.CuentasContables == null)
                model = InitCtaContable(tkn);
            var cc = model.CuentasContables.SingleOrDefault(x => x.IdCuentaContable.Equals(idcc));
            model.IdCuentaContable = cc.IdCuentaContable;
            model.Numero = cc.Numero;
            model.Descripcion = cc.Descripcion;
            return model;
        }
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
        #region Estación de Carburación

        public static List<EstacionCarburacionDTO> GetListaEstacionCarburacion(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEstacionCarburacion(tkn);
            return agente._listaEstacionCarburacion;
        }
        #endregion
        #region Unidad Almacen Gas

        public static List<UnidadAlmacenGasDTO> GetListaUnidadAlmcenGas(short IdEmpresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaUnidadAlmacenGas(IdEmpresa, tkn);
            return agente._listaUnidadAlmacenGas;
        }
        #endregion
        #region Equipo de transporte

        public static List<EquipoTransporteDTO> GetListaEquiposTransporte(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEquipoTransporte(tkn);
            return agente._listaEquipoTransporte;
        }
        #endregion
        #region Categoria Producto
        public static RespuestaDTO CrearCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarCategoriaProducto(CategoriaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCategoria(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<CategoriaProductoDTO> ListaCategorias(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCategoriasProducto(tkn);
            return agente._listaCategoriasProducto;
        }
        public static CategoriaProductoDTO ActivarEditarCategoria(short id, string tkn)
        {
            var cat = ListaCategorias(tkn).SingleOrDefault(x => x.IdCategoria.Equals(id));
            return new CategoriaProductoDTO()
            {
                IdCategoria = cat.IdCategoria,
                Descripcion = cat.Descripcion,
                Nombre = cat.Nombre,
                IdEmpresa = cat.IdEmpresa
            };
        }
        
        #endregion
        #region Linea Producto
        public static RespuestaDTO CrearLineaProducto(LineaProductoDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarLineaProducto(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<LineaProductoDTO> ListaLineasProducto(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaLienasProducto(tkn);
            return agente._listaLineasProducto;
        }
        public static LineaProductoDTO ActivarEditarLineaProducto(short id, string tkn)
        {
            var cat = ListaLineasProducto(tkn).SingleOrDefault(x => x.IdProductoLinea.Equals(id));
            return new LineaProductoDTO()
            {
                IdProductoLinea = cat.IdProductoLinea,
                Descripcion = cat.Descripcion,
                Linea = cat.Linea,
                IdEmpresa = cat.IdEmpresa
            };
        }
        #endregion
        #region Unidad de medida
        public static RespuestaDTO CrearUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarUnidadMedida(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<UnidadMedidaDTO> ListaUnidadesMedida(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaUnidadesMedida(tkn);
            return agente._listaUnidadesMedida;
        }
        public static UnidadMedidaDTO ActivarEditarUnidadMedida(short id, string tkn)
        {
            var cat = ListaUnidadesMedida(tkn).SingleOrDefault(x => x.IdUnidadMedida.Equals(id));
            return new UnidadMedidaDTO()
            {
                IdUnidadMedida = cat.IdUnidadMedida,
                Descripcion = cat.Descripcion,
                Acronimo = cat.Acronimo,
                Nombre = cat.Nombre,
                IdEmpresa = cat.IdEmpresa
            };
        }
        #endregion
    }
}