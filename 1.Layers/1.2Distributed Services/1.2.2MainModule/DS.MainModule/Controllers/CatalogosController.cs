using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        #region Empresas
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
        [Route("consulta/centrosdecosto")]
        public HttpResponseMessage GetCentrosCostos()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaCentrosCostos(), Request);
        }
        #endregion

        #region Cuentas Contables
        [Route("registra/cuenta/contable")]
        public HttpResponseMessage PostRegistraCuentaContable(ProveedorCrearDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraCuentaContable(provDto), Request);
        }

        [Route("modifica/cuenta/contable")]
        public HttpResponseMessage PutModificaCuentaContable(ProveedorModificarDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaCuentaContable(provDto), Request);
        }

        [Route("elimina/cuenta/contable")]
        public HttpResponseMessage PutEliminaCuentaContable(ProveedorEliminarDto provDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaCuentaContable(provDto), Request);
        }

        [Route("consulta/cuentas/contables")]
        public HttpResponseMessage GetListaCuentasContables()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ConsultaCuentasContables(), Request);
        }

        [Route("consulta/cuenta/contable/{idCuenta}")]
        public HttpResponseMessage GetCuentaContable(int idCuenta)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaCuentaContable(idCuenta));
        }
        #endregion
    }
}
