using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Flujos;
using DS.MainModule.Helpers.MetodosExtension;
using DS.MainModule.Results;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.MainModule;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/catalogos")]
    public class CatalogosController : ApiController
    {
        private Catalogos _catalogos;

        public CatalogosController()
        {
            _catalogos = new Catalogos();
        }

        #region Paises
        [Route("paises")]
        public HttpResponseMessage GetListaPaises()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaPaises());
        }      
        #endregion

        #region Empresas

        [Route("registra/empresa")]
        public async Task<HttpResponseMessage> PostRegistraEmpresas()
        {
            string destinationFolderPDF = Convertir.GetPhysicalPath(ConfigurationManager.AppSettings["GuardarLogoEmpresa"]);
            string root = Convertir.GetPhysicalPath(ConfigurationManager.AppSettings["RutaTemporal"]);

            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                var empresaDto = provider.FormData.AsObject<EmpresaCrearDTO>();

                var resp = ValidadorClases.EnlistaErrores(empresaDto);
                if(!resp.ModeloValido)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, resp);

                string extension = string.Empty;
                string fileName = string.Empty;
                bool existe;

                empresaDto.rutasImagenes = new List<string>();

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    try
                    {
                        extension = new FileInfo(file.Headers.ContentDisposition.FileName.Replace("\"", "")).Extension;
                        fileName = new FileInfo(file.Headers.ContentDisposition.FileName.Replace("\"", "")).Name;
                        extension.ToLower();
                        existe = true;
                    }
                    catch
                    {
                        extension = string.Empty;
                        fileName = string.Empty;
                        existe = false;
                    }

                    if (extension.Contains(ExpresionRegular.ImagenesExtensiones) && existe)
                    {
                        var newFilePath = string.Format(@"{0}\{1}", destinationFolderPDF, fileName);

                        if (!File.Exists(newFilePath))
                            File.Move(file.LocalFileName, newFilePath);
                        else
                        {
                            File.Delete(newFilePath);
                            File.Move(file.LocalFileName, newFilePath);
                        }
                        // Guardar newFilePath en la propiedad de UrlImage de la entidad
                        empresaDto.rutasImagenes.Add(newFilePath.ToString());
                    }
                }
                return RespuestaHttp.crearRespuesta(_catalogos.RegistraEmpresa(empresaDto), Request);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        //public HttpResponseMessage PostRegistraEmpresas(EmpresaCrearDTO empresaDto)
        //{
        //    return RespuestaHttp.crearRespuesta(_catalogos.RegistraEmpresa(empresaDto), Request);
        //}

        [Route("modifica/empresa")]
        public HttpResponseMessage PutModificaEmpresas(EmpresaModificarDto empresaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaEmpresa(empresaDto), Request);
        }

        [Route("elimina/empresa")]
        public HttpResponseMessage PutEliminaEmpresas(EmpresaEliminarDto empresaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaEmpresa(empresaDto), Request);
        }

        [AllowAnonymous]
        [Route("empresas/listaempresaslogin")]
        public HttpResponseMessage GetListaEmpresasLogin()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaEmpresasLogin());
        }

        [Route("empresas/listaempresa")]
        public HttpResponseMessage GetListaEmpresas()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaEmpresas());
        }       
        [Route("empresas/listaempresa/{conAdminCent}")]
        public HttpResponseMessage GetListaEmpresascad(bool conAdminCent)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaEmpresas(conAdminCent));
        }
        #endregion

        #region Usuarios
        [Route("usuarios/listausuarios/{idEmpresa}")]
        public HttpResponseMessage GetListaUsuarios(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaUsuarios(idEmpresa));
        }

        [Route("registra/usuarios")]
        public HttpResponseMessage PostRegistraUsuario(UsuarioCrearDto usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.AltaUsuarios(usuarioDto), Request);
        }

        [Route("modifica/usuario")]
        public HttpResponseMessage PutModificaUsuario(UsuarioModificarDto usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaUsuario(usuarioDto), Request);
        }

        [Route("elimina/usuario")]
        public HttpResponseMessage PutEliminaUsuario(UsuarioEliminarDto usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaUsuario(usuarioDto), Request);
        }

        #endregion

        #region Productos
        [Route("productos/listaproductos/{idEmpresa}")]
        public HttpResponseMessage GetListaProductos(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaProductos(idEmpresa));
        }
        [Route("productos/listaproductosasociados/{idProducto}")]
        public HttpResponseMessage GetListaProductosAsociados(int idProducto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaPorductosAsociados(idProducto));
        }
        #endregion

        #region Administracion Central

        #endregion

        #region Proveedor
        [Route("registra/proveedor")]        
        public HttpResponseMessage PostRegistraProveedor(ProveedorCrearDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraProveedor(provDto), Request);
        }

        [Route("modifica/proveedor")]
        public HttpResponseMessage PutModificaProveedor(ProveedorModificarDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaProveedor(provDto), Request);
        }

        [Route("elimina/proveedor")]
        public HttpResponseMessage PutEliminaProveedor(ProveedorEliminarDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaProveedor(provDto), Request);
        }

        [Route("consulta/proveedores")]
        public HttpResponseMessage GetListaProveedores()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ConsultaProveedores(), Request);
        }

        [Route("consulta/proveedor/{idProveedor}")]
        public HttpResponseMessage GetListaProveedores(int idProveedor)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaProveedor(idProveedor));
        }
        #endregion

        #region Centro de Costo
        [Route("registra/centro/costo")]
        public HttpResponseMessage PostRegistraCentroCosto(CentroCostoCrearDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraCentroCosto(cuentaDto), Request);
        }

        [Route("modifica/centro/costo")]
        public HttpResponseMessage PutModificaCentroCosto(CentroCostoModificarDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaCentroCosto(cuentaDto), Request);
        }

        [Route("elimina/centro/costo")]
        public HttpResponseMessage PutEliminaCentroCosto(CentroCostoEliminarDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaCentroCosto(cuentaDto), Request);
        }

        [Route("consulta/centrosdecosto")]
        public HttpResponseMessage GetCentrosCostos()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaCentrosCostos(), Request);
        }

        [Route("consulta/centro/costo/{idCosto}")]
        public HttpResponseMessage GetCentroCosto(int idCosto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaCentroCosto(idCosto));
        }
        #endregion

        #region Cuentas Contables
        [Route("registra/cuenta/contable")]
        public HttpResponseMessage PostRegistraCuentaContable(CuentaContableCrearDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraCuentaContable(cuentaDto), Request);
        }

        [Route("modifica/cuenta/contable")]
        public HttpResponseMessage PutModificaCuentaContable(CuentaContableModificarDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaCuentaContable(cuentaDto), Request);
        }

        [Route("elimina/cuenta/contable")]
        public HttpResponseMessage PutEliminaCuentaContable(CuentaContableEliminarDto cuentaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaCuentaContable(cuentaDto), Request);
        }

        [Route("consulta/cuentas/contables/{idEmpresa}")]
        public HttpResponseMessage GetListaCuentasContables(short idEmpresa)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ConsultaCuentasContables(idEmpresa), Request);
        }

        [Route("consulta/cuenta/contable/{idCuenta}")]
        public HttpResponseMessage GetCuentaContable(int idCuenta)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaCuentaContable(idCuenta));
        }
        #endregion      
    }
}
