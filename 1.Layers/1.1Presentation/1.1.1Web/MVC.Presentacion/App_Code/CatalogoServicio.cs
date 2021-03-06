﻿using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using Security.MainModule.Criptografia;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Json;
using System.Web;
using Utilities.MainModule;
//using System.Web.Script.Serialization;
using System.Web.Mvc;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Pedidos;

namespace MVC.Presentacion.App_Code
{
    public static class CatalogoServicio
    {

        #region Paises
        public static List<PaisModel> GetPaises(string tkn = null)
        {
            var agente = new AgenteServicio();
            agente.BuscarPaises(tkn);
            return agente._listaPaises;
        }
        #endregion

        #region Estados
        public static List<EstadosRepModel> GetEstados(string tkn = null)
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
                    string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]);
                    string destinationFolder = ConfigurationManager.AppSettings["GuardarLogoEmpresa"];
                    string destinationFolderSave = Convertir.GetPhysicalPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]); //1.0
                                                                                                                                      //string destinationFolderSave = Convertir.PhysicalPathToUrlPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]);
                                                                                                                                      //C:\\Users\\Muhammad shahid\\Documents\\Visual Studio 2013\\Projects\\WebApplication_MVC1\\WebApplication\\misc\\flowers.jpg
                                                                                                                                      //orjres.Save(Server.MapPath("~/Content/images/pics" + kapakname));
                                                                                                                                      //orjres.Save(Server.MapPath("~/Content/images/pics/" + kapakname));
                                                                                                                                      //Checking file is available to save.  
                    if (UrlLogotipo180px != null)
                    {
                        string pathBD = Path.Combine(destinationFolder + "/" + Path.GetFileName(UrlLogotipo180px.FileName));
                        string pathSave = Path.Combine(path, Path.GetFileName(UrlLogotipo180px.FileName));
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

        public static EmpresaModel guardarLogosEmpresaDto(EmpresaModel Objemp, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
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
        public static RespuestaDTO ActualizaEdicionEmpresa(EmpresaModel cc, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px, string tkn)
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
            return agente._listaEmpresas.Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
        }
        //consulta empresa mediante id
        public static EmpresaModel FiltrarEmpresa(EmpresaModel model, int id, string tkn)
        {
            EmpresaModel newList = getModel(Empresas(tkn).Where(x => x.IdEmpresa == id).ToList());
            model = newList;
            model.CierreInventarioS = model.CierreInventario.ToShortTimeString();
            model.IdEmpresa = (short)id;
            return model;
        }

        public static EmpresaModel getModel(List<EmpresaDTO> ent)
        {
            EmpresaModel nmodel = new EmpresaModel();
            nmodel.NombreComercial = ent[0].NombreComercial;
            nmodel.RazonSocial = ent[0].RazonSocial;
            nmodel.Rfc = ent[0].Rfc;
            nmodel.Persona1 = ent[0].Persona1;
            nmodel.Persona2 = ent[0].Persona2;
            nmodel.Persona3 = ent[0].Persona3;
            nmodel.Telefono1 = ent[0].Telefono1;
            nmodel.Telefono2 = ent[0].Telefono2;
            nmodel.Telefono3 = ent[0].Telefono3;
            nmodel.Celular1 = ent[0].Celular1;
            nmodel.Celular2 = ent[0].Celular2;
            nmodel.Celular3 = ent[0].Celular3;
            nmodel.Email1 = ent[0].Email1;
            nmodel.Email2 = ent[0].Email2;
            nmodel.Email3 = ent[0].Email3;
            nmodel.SitioWeb1 = ent[0].SitioWeb1;
            nmodel.SitioWeb2 = ent[0].SitioWeb2;
            nmodel.SitioWeb3 = ent[0].SitioWeb3;
            nmodel.IdPais = ent[0].IdPais;
            nmodel.IdEstadoRep = ent[0].IdEstadoRep;
            nmodel.EstadoProvincia = ent[0].EstadoProvincia;
            nmodel.Municipio = ent[0].Municipio;
            nmodel.CodigoPostal = ent[0].CodigoPostal;
            nmodel.Colonia = ent[0].Colonia;
            nmodel.Calle = ent[0].Calle;
            nmodel.NumExt = ent[0].NumExt;
            nmodel.NumInt = ent[0].NumInt;
            nmodel.UrlLogotipo180px = ent[0].UrlLogotipo180px;
            nmodel.UrlLogotipo500px = ent[0].UrlLogotipo500px;
            nmodel.UrlLogotipo1000px = ent[0].UrlLogotipo1000px;
            nmodel.CierreInventario = ent[0].CierreInventario;
            nmodel.FactorCompraLitroAKilos = ent[0].FactorCompraLitroAKilos;
            nmodel.FactorFleteGas = ent[0].FactorFleteGas;
            nmodel.FactorGalonALitros = ent[0].FactorGalonALitros;
            nmodel.FactorLitrosAKilos = ent[0].FactorLitrosAKilos;
            nmodel.InventarioCrítico = ent[0].InventarioCrítico;
            nmodel.InventarioSano = ent[0].InventarioSano;
            nmodel.MaxRemaGaseraMensual = ent[0].MaxRemaGaseraMensual;

            return nmodel;
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
        public static UsuarioRolModel AgregarIdsToentity(UsuarioRolModel cc, int idU, short rol, string token)
        {
            // var Nombre = ObtenerRolesId(cc.IdRol, token);
            //UsuarioRolModel entity = new UsuarioRolModel();
            cc.IdRol = rol;
            cc.IdUsuario = idU;

            return cc;
        }
        public static List<UsuariosModel> ObtenerTodosUsuarios(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(id, token);
            return agente._lstUserEmp;
        }

        public static UsuariosModel ObtenerIdUsuario(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarUsuarioId(id, token);
            return agente._lstUserEmp[0];
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
        public static UsuarioDTO BuscarUsuario(int IdUsuario, string tkn)
        {
            return ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn).FirstOrDefault(x => x.IdUsuario.Equals(IdUsuario));
        }
        public static RespuestaDTO ActualizaCredencialesUser(UsuarioDTO cc, string tkn)
        {
            encryptaPWD(cc);
            var agente = new AgenteServicio();
            agente.GuardarCredenciales(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO AgregarRolAlUsuario(UsuarioRolModel cc, string tkn)
        {
            //  AgregarIdRolToList(cc, tkn);
            var agente = new AgenteServicio();
            agente.GuardarRolesAsig(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static List<UsuariosModel> ObtenerUsuariosRol(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosUsuarios(0, token);
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

        public static List<UsuariosModel> FiltrarBusquedaUsuario(UsuarioDTO us, string token)
        {
            var agente = new AgenteServicio();
            agente.FiltrarUsuarios(us.IdEmpresa, us.IdUsuario, us.Email1, token);
            return agente._lstUserEmp;

            //var Usuarios = ListaUsuarios(TokenServicio.ObtenerIdEmpresa(token), token);
            //List<UsuarioDTO> _lstUserEmp = new List<UsuarioDTO>();
            //if (us.IdEmpresa != 0)
            //{
            //    _lstUserEmp = Usuarios.Where(x => x.IdEmpresa.Equals(us.IdEmpresa)).ToList();
            //}
            //if (us.IdUsuario != 0)
            //{
            //    _lstUserEmp = Usuarios.Where(x => x.IdUsuario.Equals(us.IdUsuario)).ToList();
            //}
            //if (!String.IsNullOrEmpty(us.Email1))
            //{
            //    _lstUserEmp = Usuarios.Where(x => x.Email1.Equals(us.Email1)).ToList();
            //}
            //return _lstUserEmp;
        }

        public static RespuestaDTO EliminarRolAlUsuario(UsuarioRolModel cc, int iduser, short idRol, string tkn)
        {
            AgregarIdsToentity(cc, iduser, idRol, tkn);
            var agente = new AgenteServicio();
            agente.EliminarRolesAsig(cc, tkn);
            return agente._RespuestaDTO;
        }
        #endregion

        #region Roles

        public static List<RolDto> PermisosRequisicion(List<RolRequsicion> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;
                /**/

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;

                Roles.Add(_lstc);
            }

            return Roles;

        }
        public static List<RolDto> PermisosMovilCompra(List<RolMovilCompra> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolDto> PermisosMovilVenta(List<RolMovilVenta> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;
                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolDto> PermisosSistemaVenta(List<RolSistemaVenta> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolDto> PermisosCompra(List<RolCompras> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;
                /*******/
                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolDto> PermisosTransporte(List<RolTransporte> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;
                /**/
                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }

            return Roles;

        }
        public static List<RolDto> TODto(List<RolDto> lst)
        {
            List<RolDto> Roles = new List<RolDto>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolDto _lstc = new RolDto();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;
                _lstc.FechaRegistro = lst[i].FechaRegistro;
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;


                /***********************/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;
                /*******/

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolDto> AddPermisosRoles(RolDto cc, List<RolDto> _roles)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (TODto(cc.ListaRoles));

            _roles.AddRange(Roles);

            return _roles;
        }
        public static List<RolDto> AddPermisosCom(RolDto cc, List<RolDto> _roles)
        {
            //List<RolDto> Roles = new List<RolDto>();
            //Roles = (PermisosCompra(cc.ListaRolesCom));
            //            cc.ListaRoles = Roles;
            //return cc;

            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosCompra(cc.ListaRolesCom));

            _roles.AddRange(Roles);

            return _roles;
        }
        public static List<RolDto> AddPermisosReq(RolDto cc, List<RolDto> _roles)
        {
            //List<RolDto> Roles = new List<RolDto>();
            //Roles = (PermisosRequisicion(cc.ListaRequsicion));
            //cc.ListaRoles = Roles;
            //return cc;
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosRequisicion(cc.ListaRequsicion));

            _roles.AddRange(Roles);

            return _roles;
        }
        public static List<RolDto> AddPermisosMovilCom(RolDto cc, List<RolDto> _roles)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosMovilCompra(cc.ListaMovilCompra));
            _roles.AddRange(Roles);
            return _roles;
        }
        public static List<RolDto> AddPermisosMovilVenta(RolDto cc, List<RolDto> _roles)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosMovilVenta(cc.ListaMovilVenta));
            _roles.AddRange(Roles);
            return _roles;
        }
        public static List<RolDto> AddPermisosSisVenta(RolDto cc, List<RolDto> _roles)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosSistemaVenta(cc.ListaSistemaVenta));
            _roles.AddRange(Roles);
            return _roles;
        }
        public static List<RolDto> AddPermisosTransporte(RolDto cc, List<RolDto> _roles)
        {
            List<RolDto> Roles = new List<RolDto>();
            Roles = (PermisosTransporte(cc.ListaTransporte));

            _roles.AddRange(Roles);

            return _roles;
        }
        public static List<RolDto> ObtenerTodosRoles(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarTodosRoles(token);
            return agente._lstaAllRoles;
        }
        public static List<RolDto> ObtenerRoles(string token, short emp)
        {
            var agente = new AgenteServicio();
            agente.BuscarRoles(token, emp);
            return agente._lstaAllRoles;
        }
        public static List<RolCompras> getListcompras(List<RolDto> lst)
        {
            List<RolCompras> Roles = new List<RolCompras>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolCompras _lstc = new RolCompras();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolRequsicion> getListrequisicion(List<RolDto> lst)
        {
            List<RolRequsicion> Roles = new List<RolRequsicion>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolRequsicion _lstc = new RolRequsicion();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolMovilCompra> getListmc(List<RolDto> lst)
        {
            List<RolMovilCompra> Roles = new List<RolMovilCompra>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolMovilCompra _lstc = new RolMovilCompra();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolMovilVenta> getListmv(List<RolDto> lst)
        {
            List<RolMovilVenta> Roles = new List<RolMovilVenta>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolMovilVenta _lstc = new RolMovilVenta();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolSistemaVenta> getListsv(List<RolDto> lst)
        {
            List<RolSistemaVenta> Roles = new List<RolSistemaVenta>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolSistemaVenta _lstc = new RolSistemaVenta();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
        }
        public static List<RolTransporte> getListTr(List<RolDto> lst)
        {
            List<RolTransporte> Roles = new List<RolTransporte>();
            for (int i = 0; i <= lst.Count() - 1; i++)
            {
                RolTransporte _lstc = new RolTransporte();
                _lstc.Activo = lst[i].Activo;
                _lstc.IdRol = lst[i].IdRol;
                _lstc.Rol1 = lst[i].Rol1;
                _lstc.NombreRol = lst[i].NombreRol;
                _lstc.IdEmpresa = lst[i].IdEmpresa;

                _lstc.AppCompraVerOCompra = lst[i].AppCompraVerOCompra;
                _lstc.AppCompraEntraGas = lst[i].AppCompraEntraGas;
                _lstc.AppCompraGasIniciarDescarga = lst[i].AppCompraGasIniciarDescarga;
                _lstc.AppCompraGasFinalizarDescarga = lst[i].AppCompraGasFinalizarDescarga;
                _lstc.AppAutoconsumoInventarioGral = lst[i].AppAutoconsumoInventarioGral;
                _lstc.AppAutoconsumoEstacionCarb = lst[i].AppAutoconsumoEstacionCarb;
                _lstc.AppAutoconsumoPipa = lst[i].AppAutoconsumoPipa;
                _lstc.AppCalibracionEstacionCarb = lst[i].AppCalibracionEstacionCarb;
                _lstc.AppCalibracionPipa = lst[i].AppCalibracionPipa;
                _lstc.AppCalibracionCamionetaCilindro = lst[i].AppCalibracionCamionetaCilindro;
                _lstc.AppRecargaEstacionCarb = lst[i].AppRecargaEstacionCarb;
                _lstc.AppRecargaPipa = lst[i].AppRecargaPipa;
                _lstc.AppRecargaCamionetaCilindro = lst[i].AppRecargaCamionetaCilindro;
                _lstc.AppTomaLecturaAlmacenPral = lst[i].AppTomaLecturaAlmacenPral;
                _lstc.AppTomaLecturaEstacionCarb = lst[i].AppTomaLecturaEstacionCarb;
                _lstc.AppTomaLecturaPipa = lst[i].AppTomaLecturaPipa;
                _lstc.AppTomaLecturaCamionetaCilindro = lst[i].AppTomaLecturaCamionetaCilindro;
                _lstc.AppTomaLecturaReporteDelDia = lst[i].AppTomaLecturaReporteDelDia;
                _lstc.AppTraspasoEstacionCarb = lst[i].AppTraspasoEstacionCarb;
                _lstc.AppTraspasoPipa = lst[i].AppTraspasoPipa;
                _lstc.AppDisposicionEfectivo = lst[i].AppDisposicionEfectivo;
                _lstc.AppCamionetaPuntoVenta = lst[i].AppCamionetaPuntoVenta;
                _lstc.AppEstacionCarbPuntoVenta = lst[i].AppEstacionCarbPuntoVenta;
                _lstc.AppPipaPuntoVenta = lst[i].AppPipaPuntoVenta;
                /*********/
                _lstc.RequisicionVerRequisiciones = lst[i].RequisicionVerRequisiciones;
                _lstc.RequisicionGenerarNueva = lst[i].RequisicionGenerarNueva;
                _lstc.RequisicionRevisarExistencia = lst[i].RequisicionRevisarExistencia;
                _lstc.RequisicionAutorizar = lst[i].RequisicionAutorizar;
                /*//////////////////*/
                _lstc.CatInsertarUsuario = lst[i].CatInsertarUsuario;
                _lstc.CatModificarUsuario = lst[i].CatModificarUsuario;
                _lstc.CatEliminarUsuario = lst[i].CatEliminarUsuario;
                _lstc.CatConsultarUsuario = lst[i].CatConsultarUsuario;
                _lstc.CatInsertarProveedor = lst[i].CatInsertarProveedor;
                _lstc.CatModificarProveedor = lst[i].CatModificarProveedor;
                _lstc.CatEliminarProveedor = lst[i].CatEliminarProveedor;
                _lstc.CatConsultarProveedor = lst[i].CatConsultarProveedor;
                _lstc.CatInsertarProducto = lst[i].CatInsertarProducto;
                _lstc.CatModificarProducto = lst[i].CatModificarProducto;
                _lstc.CatEliminarProducto = lst[i].CatEliminarProducto;
                _lstc.CatConsultarProducto = lst[i].CatConsultarProducto;
                _lstc.CatInsertarCentroCosto = lst[i].CatInsertarCentroCosto;
                _lstc.CatModificarCentroCosto = lst[i].CatModificarCentroCosto;
                _lstc.CatEliminarCentroCosto = lst[i].CatEliminarCentroCosto;
                _lstc.CatConsultarCentroCosto = lst[i].CatConsultarCentroCosto;
                _lstc.CatInsertarCuentaContable = lst[i].CatInsertarCuentaContable;
                _lstc.CatModificarCuentaContable = lst[i].CatModificarCuentaContable;
                _lstc.CatEliminarCuentaContable = lst[i].CatEliminarCuentaContable;
                _lstc.CatConsultarCuentaContable = lst[i].CatConsultarCuentaContable;
                _lstc.CatInsertarCliente = lst[i].CatInsertarCliente;
                _lstc.CatModificarCliente = lst[i].CatModificarCliente;
                _lstc.CatEliminarCliente = lst[i].CatEliminarCliente;
                _lstc.CatConsultarCliente = lst[i].CatConsultarCliente;
                _lstc.CatAsignarChoferPuntoVenta = lst[i].CatAsignarChoferPuntoVenta;
                _lstc.CatEliminarPuntoVenta = lst[i].CatEliminarPuntoVenta;
                _lstc.CatConsultarPuntoVenta = lst[i].CatConsultarPuntoVenta;
                _lstc.CatAsignarEquipoTransporte = lst[i].CatAsignarEquipoTransporte;
                _lstc.CatModificarEquipoTransporte = lst[i].CatModificarEquipoTransporte;
                _lstc.CatEliminarEquipoTransporte = lst[i].CatEliminarEquipoTransporte;
                _lstc.CatConsultarEquipoTransporte = lst[i].CatConsultarEquipoTransporte;
                _lstc.CatInsertarEmpresa = lst[i].CatInsertarEmpresa;
                _lstc.CatModificarEmpresa = lst[i].CatModificarEmpresa;
                _lstc.CatEliminarEmpresa = lst[i].CatEliminarEmpresa;
                _lstc.CatConsultarEmpresa = lst[i].CatConsultarEmpresa;
                _lstc.CatInsertarRol = lst[i].CatInsertarRol;
                _lstc.CatModificarRol = lst[i].CatModificarRol;
                _lstc.CatEliminarRol = lst[i].CatEliminarRol;
                _lstc.CatConsultarRol = lst[i].CatConsultarRol;
                _lstc.CatInsertarPrecioVentaGas = lst[i].CatInsertarPrecioVentaGas;
                _lstc.CatModificarPrecioVentaGas = lst[i].CatModificarPrecioVentaGas;
                _lstc.CatEliminarPrecioVentaGas = lst[i].CatEliminarPrecioVentaGas;
                _lstc.CatConsultarPrecioVentaGas = lst[i].CatConsultarPrecioVentaGas;
                _lstc.CatInsertarPrecioVenta = lst[i].CatInsertarPrecioVenta;
                _lstc.CatModificarPrecioVenta = lst[i].CatModificarPrecioVenta;
                _lstc.CatEliminarPrecioVenta = lst[i].CatEliminarPrecioVenta;
                _lstc.CatConsultarPrecioVenta = lst[i].CatConsultarPrecioVenta;
                /**/
                _lstc.CompraVerOCompra = lst[i].CompraVerOCompra;
                _lstc.CompraGenerarOCompra = lst[i].CompraGenerarOCompra;
                _lstc.CompraAutorizarOCompra = lst[i].CompraAutorizarOCompra;
                _lstc.CompraEntraProductoOCompra = lst[i].CompraEntraProductoOCompra;
                _lstc.CompraAtiendeServicioOCompra = lst[i].CompraAtiendeServicioOCompra;
                _lstc.CompraCancelaOCompra = lst[i].CompraCancelaOCompra;
                _lstc.AlmacenActualizaExistencias = lst[i].AlmacenActualizaExistencias;
                _lstc.AlmacenVerExistencias = lst[i].AlmacenVerExistencias;
                _lstc.AlmacenVerMovimientos = lst[i].AlmacenVerMovimientos;
                _lstc.AlmacenRegistrarAlmacen = lst[i].AlmacenRegistrarAlmacen;
                _lstc.AlmacenVerProductos = lst[i].AlmacenVerProductos;

                _lstc.CobranzaVerAbonos = lst[i].CobranzaVerAbonos;
                _lstc.CobranzaVerCartera = lst[i].CobranzaVerCartera;
                _lstc.CobranzaVerCreditoRecuperado = lst[i].CobranzaVerCreditoRecuperado;
                _lstc.CobranzaGenerarAbonos = lst[i].CobranzaGenerarAbonos;
                _lstc.PedidoVerPedido = lst[i].PedidoVerPedido;
                _lstc.PedidoGenerarPedido = lst[i].PedidoGenerarPedido;
                _lstc.PedidoModificarPedido = lst[i].PedidoModificarPedido;
                _lstc.PedidoEliminarPedido = lst[i].PedidoEliminarPedido;
                _lstc.ConsultarRemanenteGeneral = lst[i].ConsultarRemanenteGeneral;
                _lstc.ETRegistrarParqueVehicular = lst[i].ETRegistrarParqueVehicular;
                _lstc.ETConsultarParqueVehicular = lst[i].ETConsultarParqueVehicular;
                _lstc.ETAsignarVehiculo = lst[i].ETAsignarVehiculo;
                _lstc.ETConsultarAsignarVehiculo = lst[i].ETConsultarAsignarVehiculo;
                _lstc.ETBorrarAsignacionVehicular = lst[i].ETBorrarAsignacionVehicular;
                _lstc.ETRegistrarMantenimiento = lst[i].ETRegistrarMantenimiento;
                _lstc.ETBorrarMantenimiento = lst[i].ETBorrarMantenimiento;
                _lstc.ETRegistrarRecargaCombustible = lst[i].ETRegistrarRecargaCombustible;
                _lstc.ETBorrarRecargaCombustible = lst[i].ETBorrarRecargaCombustible;
                _lstc.CobranzaConsultarFactura = lst[i].CobranzaConsultarFactura;
                _lstc.CobranzaFacturar = lst[i].CobranzaFacturar;
                _lstc.FacturasVerFacturas = lst[i].FacturasVerFacturas;
                _lstc.FacturasFacturar = lst[i].FacturasFacturar;
                Roles.Add(_lstc);
            }
            return Roles;
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
            List<RolDto> xroles = new List<RolDto>();
            if (cc.ListaRoles != null)
            {
                //AddPermisosRoles(cc, xroles);
                // agente.GuardarPermisos(xroles, tkn);
                agente.GuardarPermisos(cc.ListaRoles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosCompra(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaRolesCom != null)
            {
                AddPermisosCom(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosRequisicion(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaRequsicion != null)
            {
                AddPermisosReq(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosMovilVenta(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaMovilVenta != null)
            {
                AddPermisosMovilVenta(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosSistemaVenta(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaSistemaVenta != null)
            {
                AddPermisosSisVenta(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosMovilCompra(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaMovilCompra != null)
            {
                AddPermisosMovilCom(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
            }

            return agente._RespuestaDTO;

        }
        public static RespuestaDTO ActualizaPermisosTransporte(RolDto cc, string tkn)
        {
            var agente = new AgenteServicio();
            List<RolDto> xroles = new List<RolDto>();

            if (cc.ListaTransporte != null)
            {
                AddPermisosTransporte(cc, xroles);
                agente.GuardarPermisos(xroles, tkn);
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
        public static ClientesModel ObtenerCliente(int id)
        {
            var agente = new AgenteServicio();
            agente.BuscarCliente(id);
            return agente._ClienteModel;
        }
        public static List<TipoPersonaModel> ObtenerTiposPersona(string token = null)
        {
            var agente = new AgenteServicio();
            agente.BuscarTiposPersona(token);
            return agente._lstaTipoPersona;
        }
        public static List<RegimenFiscalModel> ObtenerRegimenFiscal(string token = null)
        {
            var agente = new AgenteServicio();
            agente.BuscarRegimenFiscal(token);
            return agente._lstaRegimenFiscal;
        }
        public static List<ClientesDto> ListaClientes(int id, int? TipoPersona, int? regimen, string rfc, string nombre, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaClientes(id, TipoPersona ?? 0, regimen ?? 0, rfc, nombre, token);
            return agente._lstaClientes;
        }
        public static List<ClientesDto> ListaClientes(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaClientes(idEmpresa, token);
            return agente._lstaClientes;
        }
        public static List<ClientesModel> ListaClientes(int idCliente, string tel1, int pedido, string rfc, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaClientesMod(idCliente, tel1, pedido, rfc, token);
            return agente._lstaClientesMod;
        }
        public static List<ClientesDto> Clientes(string tel1, string rfc, string token)
        {
            var agente = new AgenteServicio();
            ClientesDto mod = new ClientesDto();
            mod.Telefono1 = tel1 != "" ? tel1 : "1";
            mod.Rfc = rfc != "" ? rfc : "1";
            mod.IdEmpresa = TokenServicio.ObtenerIdEmpresa(token);
            mod.IdTipoPersona = 1;
            mod.IdRegimenFiscal = 1;
            mod.limiteCreditoMonto = 1;
            mod.limiteCreditoDias = 1;
            mod.Nombre = "1";
            mod.Apellido1 = "1";
            mod.Apellido2 = "1";
            mod.Celular1 = "1";
            mod.Celular2 = "1";
            mod.Celular3 = "1";
            mod.Email1 = "1";
            mod.Email2 = "1";
            mod.Email3 = "1";
            mod.SitioWeb1 = "1";
            mod.SitioWeb2 = "1";
            mod.SitioWeb3 = "1";
            mod.Usuario = "1";
            mod.Password = "1";
            mod.RazonSocial = "1";
            mod.RepresentanteLegal = "1";
            mod.Telefono = "1";
            mod.Celular = "1";
            mod.CorreoElectronico = "1";
            mod.Domicilio = "1";
            agente.BuscarClientesRfcTel(mod, token);
            return agente._lstaClientes;
        }
        public static List<ClienteLocacionMod> ObtenerLocaciones(int id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaLocaciones(id, token);
            return agente._cteLocacion;
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
        public static RespuestaDTO ModificarClienteLocacion(ClienteLocacionMod cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EditarClienteLocacion(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminarClienteLocacion(ClienteLocacionMod cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarClienteLocacion(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static ClienteLocacionMod ObtenerModel(short idOrden, int IdCliente, string tkn)
        {
            var cat = ObtenerLocaciones(IdCliente, tkn).SingleOrDefault(x => x.Orden.Equals(idOrden));
            return new ClienteLocacionMod()
            {
                IdCliente = cat.IdCliente,
                Orden = cat.Orden,
                IdPais = cat.IdPais,
                IdEstadoRep = cat.IdEstadoRep,
                EstadoProvincia = cat.EstadoProvincia,
                Municipio = cat.Municipio,
                CodigoPostal = cat.CodigoPostal,
                Colonia = cat.Colonia,
                Calle = cat.Calle,
                NumExt = cat.NumExt,
                NumInt = cat.NumInt,
                formatted_address = cat.formatted_address,
                location_lat = cat.location_lat,
                location_lng = cat.location_lng,
                place_id = cat.place_id,
                TipoLocacion = cat.TipoLocacion

            };

        }
        public static List<ClienteLocacionMod> ObtenerModelList(int IdCliente, string tkn)
        {
            return ObtenerLocaciones(IdCliente, tkn);
        }
        public static RespuestaDTO CrearCliente(ClientesModel model)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoCliente(model);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarCliente(ClientesModel dto)
        {
            var agente = new AgenteServicio();
            agente.EditarCliente(dto);
            return agente._RespuestaDTO;
        }       
        #endregion

        #region Puntos de Venta

        public static OperadorChoferModel ObtenerIdPorUsuario(int idUsuario, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarIdChofer(idUsuario, token);
            return agente.Operador;
        }

        public static List<OperadorChoferModel> ObtenerUsuarioOperador(short idEmpresa, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarUsarioOperador(idEmpresa, token);
            return agente._listaOperadoresUsuarios;
        }
        public static PuntoVentaModel guardarModelo(PuntoVentaModel Objemp, int idChofer, string token)
        {
            try
            {
                Objemp.IdOperadorChofer = ObtenerIdPorUsuario(idChofer, token).IdOperadorChofer;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Objemp;
        }
        public static List<PuntoVentaModel> ListaPuntosVenta(int idPV, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaPuntosVenta(idPV, token);
            return agente._listaPuntosV;
        }
        public static List<PuntoVentaModel> ListaPuntosVentaId(short id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaPuntosVentaId(id, token);
            return agente._listaPuntosV;
        }

        public static RespuestaDTO EliminarPuntosVenta(PuntoVentaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarPuntosVenta(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO ModificarOperador(PuntoVentaModel cc, int idChofer, string tkn)
        {
            guardarModelo(cc, idChofer, tkn);
            var agente = new AgenteServicio();
            agente.EditarPuntoVenta(cc, tkn);
            return agente._RespuestaDTO;
        }
        #endregion

        #region Precios de Venta Gas
        public static List<PrecioVentaModel> ListaPrecioVenta(short idPrecioV, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaPrecioVenta(idPrecioV, token);
            return agente._listaPreciosV;
        }
        public static List<PrecioVentaModel> ListaPrecioVentaIdEmpresa(short id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaPreciosVentaIdEmpresa(id, token);
            return agente._listaPreciosV;
        }
        public static List<EstatusTipoFechaModel> ListaTipoFecha(string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEstatus(token);
            return agente._listaEstatus;
        }
        public static RespuestaDTO EliminarPrecioVenta(PrecioVentaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarPrecioVenta(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO RegistrarPrecio(PrecioVentaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarPrecioVenta(cc, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarPrecioVenta(PrecioVentaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarPrecioVenta(cc, tkn);
            return agente._RespuestaDTO;
        }

        #endregion

        #region Centro de Costo
        public static CentroCostoModel InitCentroCosto(string Tkn)
        {
            short Id = TokenServicio.ObtenerIdEmpresa(Tkn);
            return new CentroCostoModel()
            {
                IdEmpresa = Id,
                Empresa = Empresas(Tkn).Where(x => x.IdEmpresa.Equals(Id)).FirstOrDefault().NombreComercial,

                CentrosCostos = BuscarCentrosCosto(Tkn)
            };
        }
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentroCosto;
        }
        public static List<CentroCostoDTO> BuscarCentrosCosto(string Tkn, bool EsExterno)
        {
            var agente = new AgenteServicio();
            agente.BuscarCentrosCostos(Tkn, EsExterno);
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
                IdTipoCentroCosto = model.IdTipoCentroCosto,
                Empresa = model.Empresa,
                IdEmpresa = model.IdEmpresa,
            };
            if (!model.IdEquipoTransporte.Equals(0)) dto.IdEquipoTransporte = model.IdEquipoTransporte;
            if (!model.IdEstacionCarburacion.Equals(0)) dto.IdEstacionCarburacion = model.IdEstacionCarburacion;
            if (!model.IdCAlmacenGas.Equals(0)) dto.IdCAlmacenGas = model.IdCAlmacenGas;

            return ModificarCentroCosto(dto, tkn);
        }
        public static CentroCostoModel ActivarModificar(byte idcc, CentroCostoModel model, string tkn)
        {
            if (model.CentrosCostos == null)
                model = InitCentroCosto(tkn);
            var cc = model.CentrosCostos.SingleOrDefault(x => x.IdCentroCosto.Equals(idcc));
            model.Numero = cc.Numero;
            model.IdCentroCosto = cc.IdCentroCosto;
            model.Descripcion = cc.Descripcion;
            model.IdTipoCentroCosto = cc.IdTipoCentroCosto;
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
            dto.IdCamioneta = model.IdCamioneta;
            dto.IdCilindro = model.IdCilindro;
            dto.IdPipa = model.IdPipa;
            dto.IdVehiculoUtilitario = model.IdVehiculoUtilitario;
            dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
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
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
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
        public static ProductoDTO ActivarEditarProducto(short id, string tkn)
        {
            return ListaProductos(tkn).SingleOrDefault(x => x.IdProducto.Equals(id));
        }

        #endregion

        #region Proveedores
        public static List<ProveedorDTO> ListaProveedores(string Tkn)
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
        public static RespuestaDTO CrearProveedor(ProveedorDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarProveedor(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarProveedor(ProveedorDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarProveedor(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarProveedor(ProveedorDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarProveedor(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static ProveedorDTO ActivarEditarProveedor(short id, string tkn)
        {
            return ListaProveedores(tkn).SingleOrDefault(x => x.IdProveedor.Equals(id));
        }
        //public static List<ProveedorDTO> ListaProveedoresGas(string tkn)
        //{
        //    ListaProveedores(tkn).Where(x => x.EsGas).ToList();
        //}
        //public static List<ProveedorDTO> ListaProveedoresTransporte(string tkn)
        //{
        //    ListaProveedores(tkn).Where(x => x.EsTransporte).ToList();
        //}
        //public static List<ProveedorDTO> ListaProveedoresNoGasTransporte(string tkn)
        //{
        //    ListaProveedores(tkn).Where(x => x.EsGas.Equals(false) && x.EsTransporte.Equalas(false)).ToList();
        //}

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
        public static List<CuentaContableDTO> ListaCtaCtble(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCuentasContables(tkn);
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
            { CuentasContables = ListaCtaCtble(tkn) };
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
        public static List<EquipoTransporteDTO> Obtener(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaVehiculos(id, tkn);
            return agente._ListaVehiculos;
        }
        public static EquipoTransporteDTO Obtener(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObtenerVehiculoId(id, tkn);
            return agente._Vehiculos;
        }
        public static List<EquipoTransporteDTO> Obtener(short id, string Placas, string Nombre, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaVehiculosFiltrar(id, tkn);
            var _list = agente._ListaVehiculos;
            if (Placas != "" && Placas != null)
                _list = (from x in _list where x.Placas == Placas select x).ToList();
            if (Nombre != "" && Nombre != null)
                _list = (from x in _list where x.AliasUnidad.Contains(Nombre) select x).ToList();
            return _list;
        }
        public static RespuestaDTO Crear(EquipoTransporteDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarVehiculo(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO Modificar(EquipoTransporteDTO cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EditarVehiculo(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO Eliminar(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarVehiculo(id, tkn);
            return agente._RespuestaDTO;
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

        #region Tipo proveedor
        public static List<TipoProveedorDTO> ListaTipoProveedor(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaTipoProveedor(tkn);
            return agente._listaTipoProveedor;
        }
        #endregion

        #region Banco
        public static List<BancoDTO> ListaBanco(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaBanco(tkn);
            return agente._listaBanco;
        }
        #endregion

        #region Forma de Pago
        public static List<FormaPagoDTO> ListaFormaPago(string tkn = null)
        {
            var agente = new AgenteServicio();
            agente.ListaFormaPago(tkn);
            return agente._listaFormaPago;
        }

        #endregion

        #region Combustible
        public static List<CombustibleModel> ListaCombustible(string tkn)
        {
            var agente = new AgenteServicio();
            agente.GetListaCombustible(tkn);
            return agente._ListaCombustibles;
        }
        public static List<CombustibleModel> ListaCombustibleIdEmp(short idempresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GetListaCombustibleIdE(idempresa, tkn);
            return agente._ListaCombustibles;
        }
        public static List<TipoUnidadModel> ListaUnidadIdEmp(short idempresa, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GetListaTiposUnidad(idempresa, tkn);
            return agente._ListaTiposUnidad;
        }
        public static List<CombustibleModel> ListaCombustibleFiltrado(string desc, string tkn)
        {
            CombustibleModel dto = new CombustibleModel();
            if (dto.Id_Empresa.Equals(0)) dto.Id_Empresa = TokenServicio.ObtenerIdEmpresa(tkn);
            dto.Descripcion = desc;
            dto.TipoCombustible = "na";
            var agente = new AgenteServicio();
            agente.ListaCombustibleFiltrar(dto, tkn);
            return agente._ListaCombustibles;
        }
        public static CombustibleModel ListaCombustibleID(int idcombustible, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GetListaCombustibleID(idcombustible, tkn);
            return agente._Combustible;
        }
        public static RespuestaDTO CrearCombustible(CombustibleModel dto, string tkn)
        {
            if (dto.Id_Empresa.Equals(0)) dto.Id_Empresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarCombustible(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarCombustible(CombustibleModel dto, string tkn)
        {
            if (dto.Id_Empresa.Equals(0)) dto.Id_Empresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.ModificarCombustible(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarCombustible(int idCombustible, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarCombustible(idCombustible, tkn);
            return agente._RespuestaDTO;
        }
        #endregion

        #region Medidor

        public static List<MedidorDTO> ListaMedidores(string tkn)
        {
            var agente = new AgenteServicio();
            agente.GetListaMedidores(tkn);
            return agente._ListaMedidores;
        }
        #endregion

        #region Uso CFDI
        public static List<UsoCFDIDTO> ObtenerUsoCFDI()
        {
            var agente = new AgenteServicio();
            agente.ListaUsoCFDI();
            return agente._ListaUsoCFDI;
        }
        #endregion

        #region Metodo de Pago
        public static List<MetodoPagoDTO> ObtenerMetodoPago()
        {
            var agente = new AgenteServicio();
            agente.ListaMetodosPago();
            return agente._ListaMetodPago;
        }
        #endregion

        #region Egreso
        public static List<EgresoDTO> ListaEgresos(string Token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaEgreso(Token);
            return agente._ListaEgreso;
        }
        public static RespuestaDTO CrearEgreso(EgresoDTO dto, string tkn)
        {
            if (dto.IdEmpresa.Equals(0)) dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var agente = new AgenteServicio();
            agente.GuardarEgreso(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ModificarEgreso(EgresoDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EditarEgreso(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminiarEgreso(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarEgreso(id, tkn);
            return agente._RespuestaDTO;
        }
        public static EgresoDTO ActivarEditarEgreso(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObteneEgresoId(id, tkn);
            return agente._EgresoDTO;
        }
        #endregion
        public static RespuestaDTO SinPermisos()
        {
            return new RespuestaDTO()
            {
                Exito = false,
                MensajesError = new List<string> { "No tienes permiso para esta accion" },
                Mensaje = "No tienes permiso para esta accion"
            };
        }

    }
}