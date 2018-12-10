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
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOrdenesCompra(IdEmpresa, EsGas, EsActivoVenta, EsTransporteGas), Request);
        }

        [Route("lista/ordenes/compra/{IdOrdenCompra}")]
        public HttpResponseMessage GetListaPorteadoresxOrdenCompra(int IdOrdenCompra)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOCAlternativa(IdOrdenCompra), Request);
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
        public HttpResponseMessage GetDatosTomaLectura(bool esEstacion, bool esPipa, bool esCamioneta, bool esFinalizar = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultaDatosTomaLectura(esEstacion, esPipa, esCamioneta, esFinalizar), Request);
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
            return RespuestaHttp.crearRespuesta(_mobile.IniciarRecargaCamioneta(rdto), Request);
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
        /// <summary>
        /// Permite retornar las unidades del reporte del día
        /// Falta
        /// </summary>
        /// <returns></returns>
        [Route("catalogos/unidades")]
        public HttpResponseMessage GetCatalogoUnidades()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoUnidades(), Request);
        }
        /// <summary>
        /// Genera el reporte del dìa, toma como parametros la fecha y el almacen
        /// Falta
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="idCAlmacenGas"></param>
        /// <returns></returns>
        [Route("reportes/reporte-dia/{fecha}/{idCAlmacenGas}")]
        public HttpResponseMessage GetReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ReporteDia(fecha, idCAlmacenGas), Request);
        }

        #region Clientes punto venta mobile
        /// <summary>
        /// GetTipoPersona
        /// Permite mostrar un listado de los tipos de persona para el registro de 
        /// clientes, esta tendra ligada la razon social respectiva para el tipo de 
        /// persona
        /// </summary>
        /// <returns>Dto con la lista de tipos de persona y razones sociales</returns>
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
        #endregion

        #region Punto de venta
        [Route("cliente/lista-clientes/{criterio}")]
        public HttpResponseMessage GetListaClientes(String criterio)
        {
            return RespuestaHttp.crearRespuesta(_mobile.BuscadorClientes(criterio), Request);
        }
        [Route("venta")]
        public HttpResponseMessage PostVenta(VentaDTO venta)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Venta(venta), Request);
        }
        #endregion

        [Route("catalogos/recarga/{esEstacion}/{esPipa}/{esCamioneta}")]
        public HttpResponseMessage GetListaRecargas(bool esEstacion, bool esPipa, bool esCamioneta)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoRecargas(esEstacion, esPipa, esCamioneta), Request);
        }

        #region Autoconsumos
        [Route("autoconsumo/{esFinal}")]
        public HttpResponseMessage PostAutoconsumo(AutoconsumoDTO dto, bool esFinal = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Autoconsumo(dto, esFinal), Request);
        }

        [Route("catalogos/autoconsumo/{esEstacion}/{esInventario}/{esPipas}/{esFinal}")]
        public HttpResponseMessage GetCatalogosAutoconsumo(bool esEstacion, bool esInventario, bool esPipas, bool esFinal = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoAutoconsumo(esEstacion, esInventario, esPipas, esFinal), Request);
        }
        #endregion

        #region Calibracion de gas
        [Route("catalogos/calibracion/{esEstacion}/{esPipa}")]
        public HttpResponseMessage GetCatalogosCalibracion(bool esEstacion, bool esPipa)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoCalibracion(esEstacion, esPipa), Request);
        }

        [Route("calibracion/{esFinal}")]
        public HttpResponseMessage PostCalibracion(CalibracionDto dto, bool esFinal)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Calibracion(dto, esFinal), Request);
        }
        #endregion

        [Route("catalogos/traspaso/{esPipa}")]
        public HttpResponseMessage GetCatalogoTraspaso(bool esPipa)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoTraspaso(esPipa), Request);
        }

        [Route("traspaso/{esFinal}")]
        public HttpResponseMessage PostTraspaso(TraspasoDto dto, bool esFinal)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Traspaso(dto, esFinal), Request);
        }
        //Anticipos y corte
        [Route("catalogos/anticipo-y-corte/estaciones")]
        public HttpResponseMessage GetEstaciones()
        {
            return RespuestaHttp.crearRespuesta(_mobile.Estaciones(), Request);
        }
        [Route("anticipos")]
        public HttpResponseMessage PostAnticipo(AnticipoDto dto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.anticipo(dto), Request);
        }
        [Route("corte-de-caja")]
        public HttpResponseMessage PostCorte(CorteDto dto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.corte(dto), Request);
        }
        //[AllowAnonymous]
        [Route("catalogos/anticipo-y-corte/ventas/{estacion}/{esAnticipos}/{fecha}")]
        public HttpResponseMessage GetVentasCortesAnticipos(int estacion, bool esAnticipos, DateTime fecha)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoVentasAnticiposCorte(estacion, esAnticipos, fecha), Request);
        }
        [Route("consulta/precioventa/vigente")]
        public HttpResponseMessage GetPreciosVentaVigente()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _catalogos.ObtenerPrecioVentaVigente());
        }
        /// <summary>
        /// Permite retornar los catalogos de venta de gas 
        /// Flata ver donde se extrae
        /// </summary>
        /// <param name="esLP">Si es Gas LP</param>
        /// <param name="esCilindroConGas">Si es Cilindro con gas</param>
        /// <param name="esCilindro">Si es cilindro</param>
        /// <returns></returns>
        [Route("catalogos/venta-gas/{esLP}/{esCilindroConGas}/{esCilindro}")]
        public HttpResponseMessage GetCatalogosGas(bool esLP, bool esCilindroConGas, bool esCilindro)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogosGas(esLP, esCilindroConGas, esCilindro), Request);
        }
        [Route("catalogos/venta-gas/otros")]
        public HttpResponseMessage GetOtros()
        {
            return RespuestaHttp.crearRespuesta(_mobile.catalogoOtros(), Request);
        }
        [Route("catalogos/venta-gas/catalgocilindros")]
        public HttpResponseMessage GetCatalogosClilindro()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogosGas(), Request);
        }
        [Route("buscar/cliente/{rfc}")]
        public HttpResponseMessage GetClientePorRFC(string rfc)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _mobile.BuscarClientePorRFC(rfc));
        }
        [Route("estacion/punto-venta")]
        public HttpResponseMessage GetEstacion()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerEstacion(),Request);
        }
    }
}