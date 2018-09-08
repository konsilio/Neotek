using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
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
        [Route("registra/empresa")]
        
        public HttpResponseMessage PostRegistraEmpresas(EmpresaCrearDTO empresaDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraEmpresa(empresaDto), Request);
        }

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

        #region Clientes
        [Route("registra/cliente")]
        public HttpResponseMessage PostRegistraCliente(ClienteCrearDto clienteDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraCliente(clienteDto), Request);
        }

        [Route("modifica/cliente")]
        public HttpResponseMessage PutModificaCliente(ClienteModificarDto clienteDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaCliente(clienteDto), Request);
        }

        [Route("elimina/cliente")]
        public HttpResponseMessage PutEliminaCliente(ClienteEliminarDto clienteDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaCliente(clienteDto), Request);
        }

        [Route("consulta/clientes")]
        public HttpResponseMessage GetCategoriasProducto()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaCategoriasProducto(), Request);
        }

        [Route("consulta/cliente/{idCliente}")]
        public HttpResponseMessage GetCliente(short idCliente)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaCliente(idCliente));
        }
        #endregion       

        #region Productos

        #region Categoria Productos
        [Route("registra/categoria/producto")]
        public HttpResponseMessage PostRegistraCategoriaProducto(CategoriaProductoCrearDto cProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraCategoriaProducto(cProdDto), Request);
        }

        [Route("modifica/categoria/producto")]
        public HttpResponseMessage PutModificaCategoriaProducto(CategoriaProductoModificarDto cProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaCategoriaProducto(cProdDto), Request);
        }

        [Route("elimina/categoria/producto")]
        public HttpResponseMessage PutEliminaCategoriaProducto(CategoriaProductoEliminarDto cProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaCategoriaProducto(cProdDto), Request);
        }

        [Route("consulta/categorias/producto")]
        public HttpResponseMessage GetCategoriasProducto()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaCategoriasProducto(), Request);
        }

        [Route("consulta/categoria/producto/{idCosto}")]
        public HttpResponseMessage GetCategoriaProducto(short idCategoria)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaCategoriaProducto(idCategoria));
        }
        #endregion

        #region Linea Productos
        [Route("registra/linea/producto")]
        public HttpResponseMessage PostRegistraLineaProducto(LineaProductoCrearDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraLineaProducto(lProdDto), Request);
        }

        [Route("modifica/linea/producto")]
        public HttpResponseMessage PutModificalLineaProducto(LineaProductoModificarDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaLineaProducto(lProdDto), Request);
        }

        [Route("elimina/linea/producto")]
        public HttpResponseMessage PutEliminaLineaProducto(LineaProductoEliminarDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaLineaProducto(lProdDto), Request);
        }

        [Route("consulta/lineas/producto")]
        public HttpResponseMessage GetlineasProducto()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaLineasProducto(), Request);
        }

        [Route("consulta/linea/producto/{idlinea}")]
        public HttpResponseMessage GetlineaProducto(short idlinea)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaLineaProducto(idlinea));
        }
        #endregion

        #region Unidad de Medida
        [Route("registra/unidad/medida")]
        public HttpResponseMessage PostRegistraUnidadMedida(UnidadMedidaCrearDto uMedDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraUnidadMedida(uMedDto), Request);
        }

        [Route("modifica/unidad/medida")]
        public HttpResponseMessage PutModificalUnidadMedida(UnidadMedidaModificarDto uMedDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaUnidadMedida(uMedDto), Request);
        }

        [Route("elimina/unidad/medida")]
        public HttpResponseMessage PutEliminaUnidadMedida(UnidadMedidaEliminarDto uMedDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaUnidadMedida(uMedDto), Request);
        }

        [Route("consulta/unidades/medida")]
        public HttpResponseMessage GetUnidadesMedida()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListaUnidadesMedida(), Request);
        }

        [Route("consulta/unidad/medida/{idUnidad}")]
        public HttpResponseMessage GetUnidadMedida(short idUnidad)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaUnidadMedida(idUnidad));
        }
        #endregion

        #region Producto
        [Route("registra/producto")]
        public HttpResponseMessage PostRegistraProducto(ProductoCrearDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.RegistraProducto(lProdDto), Request);
        }

        [Route("modifica/producto")]
        public HttpResponseMessage PutModificalProducto(ProductoModificarDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ModificaProducto(lProdDto), Request);
        }

        [Route("elimina/producto")]
        public HttpResponseMessage PutEliminaProducto(ProductoEliminarDto lProdDto)
        {
            return RespuestaHttp.crearRespuesta(_catalogos.EliminaProducto(lProdDto), Request);
        }

        [Route("consulta/producto")]
        public HttpResponseMessage GetProductos()
        {
            return RespuestaHttp.crearRespuesta(_catalogos.ListasProducto(), Request);
        }

        [Route("consulta/producto/{id}")]
        public HttpResponseMessage GetProducto(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ConsultaProducto(id));
        }

        [Route("productos/listaproductos/")]
        public HttpResponseMessage GetListaProductos()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaProductos());
        }
        [Route("productos/listaproductosasociados/{idProducto}")]
        public HttpResponseMessage GetListaProductosAsociados(int idProducto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ListaProductosAsociados(idProducto));
        }
        #endregion

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
