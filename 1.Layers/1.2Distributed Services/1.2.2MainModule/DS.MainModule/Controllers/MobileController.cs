using Application.MainModule.DTOs.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.MainModule.Flujos;
using Application.MainModule.DTOs.Respuesta;

using DS.MainModule.Results;
using Application.MainModule.DTOs.Mobile;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/mobile")]
    public class MobileController : ApiController
    {
        private Seguridad _seguridad;
        private Catalogos _catalogos;
        private Mobile _mobile;

        public MobileController()
        {
            _seguridad = new Seguridad();
            _catalogos = new Catalogos();
            _mobile = new Mobile();
        }

        [Route("servicio/disponible")]
        public HttpResponseMessage PostServicioDisponible()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RespuestaDto() { Exito = true });
        }

        [AllowAnonymous]
        [Route("login")]
        public HttpResponseMessage PostLogin(LoginFbDTO autenticacionDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AutenticacionMobile(autenticacionDto), Request);
        }

        [Route("lista/ordenes/compra")]
        public HttpResponseMessage GetListaOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOrdenesCompra(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas), Request);
        }

        [Route("obtener/menu")]
        public HttpResponseMessage GetObtenerMenu()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMenu(), Request);
        }

        [Route("obtener/medidores")]
        public HttpResponseMessage GetObtenerMedidores()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMedidores(), Request);
        }

        [Route("obtener/almacenes")]
        public HttpResponseMessage GetObtenerAlmacenesGas()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerAlmacenesGas(), Request);
        }

        [Route("registrar/papeleta")]
        public HttpResponseMessage PostRegistrarPapeleta(PapeletaDTO papeletaDTO)
        {
            return RespuestaHttp.crearRespuesta(_mobile.RegistrarPapeleta(papeletaDTO), Request);
        }

        [Route("iniciar/descarga")]
        public HttpResponseMessage PostInicializarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.InicializarDescarga(desDto), Request);
        }

        [Route("finalizar/descarga")]
        public HttpResponseMessage PostFinalizarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarDescarga(desDto), Request);
        }

        /// <summary>
        /// Permite realizar el registro de la toma de lectura,
        /// tomara como parametro un objeto LecturaAlmacenDto con los datos a guardar, 
        /// tras finalizar el api retornara una respuesta de tipo RespuestaHttp
        /// con el resultado de la operación.
        /// Author:Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
        /// </summary>
        /// <param name="desDto">Objeto DescargaDto con los datos a procesar</param>
        /// <returns>Respuesta del registro de la lectura inicial de la estacion de calibración</returns>
        [Route("iniciar/toma-de-lectura")]
        public HttpResponseMessage PostIniciarTomaDeLectura(LecturaDTO liadto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.InicializarTomaDeLectura(liadto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la finalizacion de la toma de lectura, 
        /// se tomara como paramtro un objeto de tipo LecturaAlmacenDto con los datos a guardar
        /// tras finalizar el api retorana una respuesta de tipo RespuestaHttp con el resultado 
        /// Author:Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
        /// </summary>
        /// <param name="lfadto">Objeto DescargaDto con los datos a procesar</param>
        /// <returns>Respuesta del registro de la lectura inicial de la estacion de calibración </returns>
        [Route("finalizar/toma-de-lectura")]
        public HttpResponseMessage PostFinalizarTomaDeLectura(LecturaDTO lfadto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarTomaDeLectura(lfadto), Request);
        }
        [Route("iniciar/toma-lectura-camioneta")]
        public HttpResponseMessage PostIniciarTomaDeLecturaCamioneta(LecturaCamionetaDTO licdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarTomaDeLecturaCamioneta(licdto), Request);
        }
        [Route("finalizar/toma-lectura-camioneta")]
        public HttpResponseMessage PostFinalizarTomaDeLecturaCamioneta(LecturaCamionetaDTO lfcdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarTomaDeLecturaCamioneta(lfcdto), Request);
        }
        /// <summary>
        /// Permite obtener el catalogo de estaciones filtrando por el tipo 
        /// </summary>
        /// <param name="EsEstacion">Es una estacion</param>
        /// <param name="EsAlmacen">Es un almacen</param>
        /// <param name="EsPipa">Es una pipa</param>
        /// <param name="EsCamioneta">Es una camioneta</param>
        /// <returns></returns>
        [Route("catalogos/almacenes/{esEstacion}/{esPipa}/{esCamioneta}/{esFinalizar}")]
        public HttpResponseMessage GetDatosTomaLectura(bool esEstacion, bool esPipa,bool esCamioneta, bool esFinalizar = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultaDatosTomaLectura(esEstacion, esPipa, esCamioneta, esFinalizar),Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga para la 
        /// camioneta 
        /// </summary>
        /// <param name="rdto">Objeto con los datos de la camioneta</param>
        /// <returns></returns>
        [Route("recarga/camioneta")]
        public HttpResponseMessage PostRecargaCamioneta(RecargaDTO rdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarRecargaCamioneta(rdto),Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga inicial de pipa y estación
        /// </summary>
        /// <param name="ridto">Objeto con los datos de la recarga inicial de pipa/estación</param>
        /// <returns></returns>
        [Route("recarga/inicial")]
        public HttpResponseMessage PostRecargaInicial(RecargaDTO ridto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarRecarga(ridto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga final de pipa y estación
        /// </summary>
        /// <param name="rfdto">Objeto con los datos de la recarga final de pipa/estación</param>
        /// <returns></returns>
        [Route("recarga/final")]
        public HttpResponseMessage PostRecargaFinal(RecargaDTO rfdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarRecarga(rfdto), Request);
        }
        [Route("catalogos/unidades")]
        public HttpResponseMessage GetCatalogoUnidades()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoUnidades(), Request);
        }

        [Route("reportes/reporte-dia/{fecha}/{idCAlmacenGas}")]
        public HttpResponseMessage GetReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ReporteDia(fecha, idCAlmacenGas),Request);
        }
        [Route("catalogos/anticipos/{esAnticipo}/{esCorteCaja}")]
        public HttpResponseMessage GetEstacionesDeposito(bool esAnticipo,bool esCorteCaja)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoEstaciones(esAnticipo, esCorteCaja),Request);
        }

        [Route("catalogos/tipo-persona")]
        public HttpResponseMessage GetTipoPersona()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoTipoPersona(), Request);
        }
        /// <summary>
        /// Permite realizar el registro del cliente, realizara una busqueda para verificar 
        /// si este ya este registrado, en caso de que si tomara el id y lo steara en el 
        /// DTO para luego realizar un update, en caso de que no encuentre el id realizara 
        /// un registro normal , retornando un obtjeto RespuestaDTO
        /// </summary>
        /// <param name="cliente">Objeto DTO con los datos del cliente</param>
        /// <returns>Respuesta del registro</returns>
        [Route("cliente/registrar")]
        public HttpResponseMessage PostRegistrarCliente(ClienteDTO cliente)
        {
            return RespuestaHttp.crearRespuesta(_mobile.registrarCliente(cliente), Request);
        }

        [Route("cliente/lista-clientes/{criterio}")]
        public HttpResponseMessage GetListaClientes(String criterio)
        {
            return RespuestaHttp.crearRespuesta(_mobile.BuscadorClientes(criterio), Request);
        }
    }
}