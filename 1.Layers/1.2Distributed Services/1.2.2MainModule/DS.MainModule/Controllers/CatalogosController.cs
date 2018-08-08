using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Flujos;
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
        [Route("prodcutos/listaprodcutos/{idEmpresa}")]
        public HttpResponseMessage GetListaProductos(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaProductos(idEmpresa));
        }
        #endregion
        #region Administracion Central

        #endregion
    }
}
