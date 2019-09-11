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

        #region Servicio disponible
        /// <summary>
        /// PostServicioDisponible
        /// Permite revisar si el servicio actualmente esta disponible, respodenra un
        /// objeto RespuestaDTO con el resultado
        /// </summary>
        /// <returns>Respuesta del servicio</returns>
        [Route("servicio/disponible")]
        public HttpResponseMessage PostServicioDisponible()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RespuestaDto() { Exito = true });
        }
        #endregion

        #region Login
        /// <summary>
        /// Login
        /// Metodo de inicio de session, recibe un objeto 
        /// json con los datos para el modelo de LoginDTO para inciar secion
        /// </summary>
        /// <param name="autenticacionDto">Formulario de login(empresa,email,contraseña FireBase token)</param>
        /// <returns>RespuestaDTO con el menu , token e información de la session</returns>
        [AllowAnonymous]
        [Route("login")]
        public HttpResponseMessage PostLogin(LoginFbDTO autenticacionDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AutenticacionMobile(autenticacionDto), Request);
        }
        #endregion

        #region Ordenes de compra
        /// <summary>
        /// Permite obtener un lsiatdo con las ordenes de compra actuales
        /// </summary>
        /// <param name="IdEmpresa">Id de la empresa</param>
        /// <param name="EsGas"> Boleano es una orden de gas</param>
        /// <param name="EsActivoVenta">Boleano es un activo de venta</param>
        /// <param name="EsTransporteGas">Boleano es transporte de gas</param>
        /// <returns>Lista con las ordenes de compra que cumplan con la condición</returns>
        [Route("lista/ordenes/compra")]
        public HttpResponseMessage GetListaOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOrdenesCompra(IdEmpresa, EsGas, EsActivoVenta, EsTransporteGas), Request);
        }
        /// <summary>
        /// Permite retornar la orden de compra secundaria con la que se liga la del expedidor o porteador
        /// </summary>
        /// <param name="IdOrdenCompra">Id de la orden de compra ya sea expedidor o porteador</param>
        /// <returns></returns>
        [Route("lista/ordenes/compra/{IdOrdenCompra}")]
        public HttpResponseMessage GetListaPorteadoresxOrdenCompra(int IdOrdenCompra)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOCAlternativa(IdOrdenCompra), Request);
        }
        #endregion

        #region Menu
        /// <summary>
        /// Retorna el menu de usuario segun sus permisos
        /// </summary>
        /// <returns>Retorna el menu deusaurio por permisos</returns>
        [Route("obtener/menu")]
        public HttpResponseMessage GetObtenerMenu()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMenu(), Request);
        }
        #endregion

        #region Papeleta
        /// <summary>
        /// Permite obtener la lista de  los medidores 
        /// </summary>
        /// <returns>Objeto de tipo MedidorDTO con los datos de los medidores en formato de lista </returns>
        [Route("obtener/medidores")]
        public HttpResponseMessage GetObtenerMedidores()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMedidores(), Request);
        }
        /// <summary>
        /// Permite obtener el listado de almacenes 
        /// </summary>
        /// <returns>Retorna una lista de tipo AlmacenGasDTO con los datos de estaciónes</returns>
        [Route("obtener/almacenes")]
        public HttpResponseMessage GetObtenerAlmacenesGas()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerAlmacenesGas(), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la papeleta, toma como parametro un 
        /// objeto de tipo PapeletaDTO con los datos a registrar
        /// </summary>
        /// <param name="papeletaDTO">Objeto con los datos a registrar</param>
        /// <returns>ResouestaDTO con la respuesta del registro</returns>
        [Route("registrar/papeleta")]
        public HttpResponseMessage PostRegistrarPapeleta(PapeletaDTO papeletaDTO)
        {
            return RespuestaHttp.crearRespuesta(_mobile.RegistrarPapeleta(papeletaDTO), Request);
        }
        #endregion

        #region Descarga
        /// <summary>
        /// Permite realizar el registro de la descarga inicial , toma como parametro
        /// un objeto de tipo Descargadto con los datos a registrar 
        /// </summary>
        /// <param name="desDto">Objeto de tipo Descargadto con los datos de la descarga </param>
        /// <returns>RespuestaDTO con la respuesta del registro de la descarga</returns>
        [Route("iniciar/descarga")]
        public HttpResponseMessage PostInicializarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.InicializarDescarga(desDto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la descarga final , toma como parametro
        /// un objeto de tipo Descargadto con los datos a registrar 
        /// </summary>
        /// <param name="desDto">Objeto de tipo Descargadto con los datos de la descarga final</param>
        /// <returns>RespuestaDTO con la respuesta del registro de la descarga final</returns>
        [Route("finalizar/descarga")]
        public HttpResponseMessage PostFinalizarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarDescarga(desDto), Request);
        }
        #endregion

        #region Toma de lectura
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
        /// <summary>
        /// Permite registrar la lectura inicial de la camioneta
        /// </summary>
        /// <param name="licdto">DTO con los datos de la lectura inicial de la camioneta</param>
        /// <returns>Retorna la respuesta del registro</returns>
        [Route("iniciar/toma-lectura-camioneta")]
        public HttpResponseMessage PostIniciarTomaDeLecturaCamioneta(LecturaCamionetaDTO licdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarTomaDeLecturaCamioneta(licdto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la toma de lectura final, tomara como parametro 
        /// un objeto de tipo LecturaCamionetaDTO con los datos de la lectura a registrar
        /// </summary>
        /// <param name="lfcdto">DTO con los datos de la lectura</param>
        /// <returns>Objeto de tipo RespuestaDTO  con la respuesta del registro </returns>
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
        #endregion

        #region Recarga
        /// <summary>
        /// Permite obtener los catalogos para la recrga de gas, dependiendo el tipo de 
        /// estacion o punto de venta(Pipa, camioneta o estación de carburación) se obtendra
        /// un objeto de tipo DatosRecragaDTO con los datos de los catalogos
        /// </summary>
        /// <param name="esEstacion">La recarga es desde una estación de carburación </param>
        /// <param name="esPipa">La recarga es desde una pipa</param>
        /// <param name="esCamioneta">La recraga es desde una Camioneta de cilindros</param>
        /// <returns>Listado de tipo DatosRecargaDTO con la lista de estaciónes a utilizar </returns>
        [Route("catalogos/recarga/{esEstacion}/{esPipa}/{esCamioneta}")]
        public HttpResponseMessage GetListaRecargas(bool esEstacion, bool esPipa, bool esCamioneta)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoRecargas(esEstacion, esPipa, esCamioneta), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga para la 
        /// camioneta 
        /// </summary>
        /// <param name="rdto">Objeto con los datos de la camioneta</param>
        /// <returns>RepsuestaDTO con la repsuesta del registro</returns>
        [Route("recarga/camioneta")]
        public HttpResponseMessage PostRecargaCamioneta(RecargaDTO rdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarRecargaCamioneta(rdto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga inicial de pipa y estación
        /// </summary>
        /// <param name="ridto">Objeto con los datos de la recarga inicial de pipa/estación</param>
        /// <returns>RepsuestaDTO con la repsuesta del registro</returns>
        [Route("recarga/inicial")]
        public HttpResponseMessage PostRecargaInicial(RecargaDTO ridto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.IniciarRecarga(ridto), Request);
        }
        /// <summary>
        /// Permite realizar el registro de la recarga final de pipa y estación
        /// </summary>
        /// <param name="rfdto">Objeto con los datos de la recarga final de pipa/estación</param>
        /// <returns>RepsuestaDTO con la repsuesta del registro</returns>
        [Route("recarga/final")]
        public HttpResponseMessage PostRecargaFinal(RecargaDTO rfdto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarRecarga(rfdto), Request);
        }
        #endregion

        #region Reporte del día 
        /// <summary>
        /// Permite retornar las unidades del reporte del día
        /// </summary>
        /// <returns>Objeto de típo DatosTomadeLecturaDTO con la lista de unidades del dropdown de estaciónes en el mobil</returns>
        [Route("catalogos/unidades")]
        public HttpResponseMessage GetCatalogoUnidades()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoUnidades(), Request);
        }
        /// <summary>
        /// Genera el reporte del dìa, toma como parametros la fecha y el almacen al cual se
        /// generara el reporte del día, al finalizar de crear el reporte se retornara un objeto
        /// con los datos para ser mostrados en la app
        /// </summary>
        /// <param name="fecha">Fecha que se desea generar el reporte del día </param>
        /// <param name="idCAlmacenGas">Id de CAlmacenGas (ya sea camioneta , estacíon o pipa)</param>
        /// <returns>Objeto ReporteDiaDTO con los valores del reporte generado </returns>
        [Route("reportes/reporte-dia/{fecha}/{idCAlmacenGas}")]
        public HttpResponseMessage GetReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ReporteDia(fecha, idCAlmacenGas), Request);
        }
        #endregion

        #region Autoconsumos
        /// <summary>
        /// Permite realizar el registro de un autoconsumo , se enviaran como parametros un 
        /// objeto de tipo AutoconsumoDTO con los datos a registrar y un boolean que determina si
        /// es inicial o final 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="esFinal"></param>
        /// <returns></returns>
        [Route("autoconsumo/{esFinal}")]
        public HttpResponseMessage PostAutoconsumo(AutoconsumoDTO dto, bool esFinal = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Autoconsumo(dto, esFinal), Request);
        }
        /// <summary>
        /// Permite retornar el listado de estaciónes o almacenes de gas para el autoconsumo. 
        /// dependiendo de que tipo se este trabajando desde la app(Si inicio session con 
        /// Estación de carburación , Es de inventario general o es una pipa) ademas de especificar
        /// si es un autoconsumo inicial o final 
        /// </summary>
        /// <param name="esEstacion">Es una estación de carburación</param>
        /// <param name="esInventario">Es inventario general de gas </param>
        /// <param name="esPipas">Es una pipa </param>
        /// <param name="esFinal">Es inicial o final </param>
        /// <returns>Objeto DatosAutoconsumoDTO con las listas a mostrar </returns>
        [Route("catalogos/autoconsumo/{esEstacion}/{esInventario}/{esPipas}/{esFinal}")]
        public HttpResponseMessage GetCatalogosAutoconsumo(bool esEstacion, bool esInventario, bool esPipas, bool esFinal = false)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoAutoconsumo(esEstacion, esInventario, esPipas, esFinal), Request);
        }
        #endregion

        #region Calibracion de gas
        /// <summary>
        /// Permite retornar los catalogos de calibración de gas, se enviara como parametro 
        /// si es una estación de carburación o si se trata de una pipa , retornara una
        /// objeto de tipo DatosCalibracionDTO con los datos de las listas a mostrar 
        /// </summary>
        /// <param name="esEstacion">Es una estación de carburación</param>
        /// <param name="esPipa">Es una pipa</param>
        /// <returns></returns>
        [Route("catalogos/calibracion/{esEstacion}/{esPipa}")]
        public HttpResponseMessage GetCatalogosCalibracion(bool esEstacion, bool esPipa)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoCalibracion(esEstacion, esPipa), Request);
        }
        /// <summary>
        /// Permite registrar la calibración del almacen de gas , retornara un 
        /// objeto de tipo RespuestaDTO con la respuesta del registro , se envian como
        /// parametros los datos de la calibración en el objeto CalibraciónDTO y un boolean
        /// que determina si es final
        /// </summary>
        /// <param name="dto">DTO con los datos de la calibración </param>
        /// <param name="esFinal">Bandera que determian si es inicial o final </param>
        /// <returns></returns>
        [Route("calibracion/{esFinal}")]
        public HttpResponseMessage PostCalibracion(CalibracionDto dto, bool esFinal)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Calibracion(dto, esFinal), Request);
        }
        #endregion

        #region Traspasos
        /// <summary>
        /// Permite retornar los catalogos de estaciones para los traspasos de gas,
        /// tomara de parametro un valor boleano que determina si la estación de donde 
        /// se realiza el traspaso es una pipa 
        /// </summary>
        /// <param name="esPipa">Determina si es una pipa o estación</param>
        /// <returns>Objeto de tipo DatosTraspasosDTO con las listas para el traspaso </returns>
        [Route("catalogos/traspaso/{esPipa}")]
        public HttpResponseMessage GetCatalogoTraspaso(bool esPipa)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogoTraspaso(esPipa), Request);
        }
        /// <summary>
        /// Permite realizar el registro del traspaso toma como parametro un 
        /// valor boleano que determina si es traspaso inicial o final , retornara un objeto de tipo 
        /// RepsuestaDTO con la respuesta del registro del traspaso 
        /// </summary>
        /// <param name="dto">Objeto de tipo TraspasoDTO con los valores del traspaso </param>
        /// <param name="esFinal">Determina si es un traspaso inicial o final</param>
        /// <returns></returns>
        [Route("traspaso/{esFinal}")]
        public HttpResponseMessage PostTraspaso(TraspasoDto dto, bool esFinal)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Traspaso(dto, esFinal), Request);
        }
        #endregion

        #region Anticipos y cortes
        /// <summary>
        /// Permitre retornar el catalogo de estaciones para los 
        /// anticipos y los cortes de caja 
        /// </summary>
        /// <returns>Listado de estaciónes asignadas para realizar corte o anticipo </returns>
        [Route("catalogos/anticipo-y-corte/estaciones")]
        public HttpResponseMessage GetEstaciones()
        {
            return RespuestaHttp.crearRespuesta(_mobile.Estaciones(), Request);
        }
        /// <summary>
        /// Permite realizar el registro de un anticipo 
        /// </summary>
        /// <param name="dto">Objeto de tipo AnticipoDTO en el cual se tienen los datos del anticipo</param>
        /// <returns>Objeto de tipo RespuestaDTO con la respuesta del registro de los datos </returns>
        [Route("anticipos")]
        public HttpResponseMessage PostAnticipo(AnticipoDto dto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.anticipo(dto), Request);
        }
        /// <summary>
        /// Permite realizar el registro del cote de caja , se enviara de parametro un objeto de tipo 
        /// CorteDto con los datos del corte a registrar
        /// </summary>
        /// <param name="dto">Objeto de tipo CorteDto con los datos del corte </param>
        /// <returns>Objeto de tipo RespuestaDTO con la respuesta del registro </returns>
        [Route("corte-de-caja")]
        public HttpResponseMessage PostCorte(CorteDto dto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.corte(dto), Request);
        }
        //[AllowAnonymous]
        /// <summary>
        /// Permite realizar la consuta de los datos para obtener las ventas para
        /// realizar el corte o el anticipo dependiendo de los parametros enviados 
        /// en la ulrl
        /// </summary>
        /// <param name="estacion">Id de la CAlmacenGas a consultar </param>
        /// <param name="esAnticipos">Determina si es un anticipo o un corte de caja </param>
        /// <param name="fecha">Fecha en la que se realiza el corte o el anticipo </param>
        /// <returns></returns>
        [Route("catalogos/anticipo-y-corte/ventas/{estacion}/{esAnticipos}/{fecha}")]
        public HttpResponseMessage GetVentasCortesAnticipos(int estacion, bool esAnticipos, DateTime fecha)
        {
            //return RespuestaHttp.crearRespuesta(_mobile.CatalogoVentasAnticiposCorte(estacion, esAnticipos, fecha), Request);
            return RespuestaHttp.crearRespuesta(_mobile.BusquedaAnticipoCorteFecha(estacion, esAnticipos, fecha), Request);
        }
        /// <summary>
        /// Permite retornar el listado de usuarios para ser implementados
        /// en los cortes y anticipos 
        /// </summary>
        /// <returns>Respuesta DTO con la lista de usuarios encontrados en la empresa de session</returns>
        [Route("catalogos/anticipos-y-cortes/usuarios")]
        public HttpResponseMessage GetUsuariosAnticiposCorte()
        {
            return RespuestaHttp.crearRespuesta(_mobile.UsuariosAnticiposCorte(), Request);
        }

        /// <summary>
        /// Permite retornar el listado de usuarios para ser implementados en 
        /// los cortes de caja, para asignar el responsable del corte 
        /// </summary>
        /// <returns>Respuesta DTO con los datos </returns>
        [Route("catalogos/anticipos-y-cortes/usuarios-liquitar")]
        public HttpResponseMessage GetUsuariosCorte()
        {
            return RespuestaHttp.crearRespuesta(_mobile.UsuariosAnticiposCorteLiquidar(), Request);
        }
        /// <summary>
        /// Permite realizar la verificación de que si se registro la lectura incial y final 
        /// del día de la estación. Retornara un objeto de tipo RespuestaDTO con el resultado
        /// de la consulta 
        /// </summary>
        /// <returns>Objeto de tipo RespuestaDTO con el resultado de la consulta</returns>
        [Route("cortes/verificar-lecturas")]
        public HttpResponseMessage GetHayLectura()
        {
            return RespuestaHttp.crearRespuesta(_mobile.HayLectura(), Request);
        }
        /// <summary>
        /// Permite verificar si existen cortes 
        /// </summary>
        /// <returns>Respuesta con el resultado de la comprobacion</returns>
        [Route("cortes/hay-lectura-final/{idCAlmacenGas}")]
        public HttpResponseMessage GetHayLecturaFinal(short idCAlmacenGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.GetHayLecturaFinal(idCAlmacenGas), Request);
        }
        #endregion

        #region Venta
        #region Clientes
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
        [Route("buscar/cliente/{rfc}")]
        public HttpResponseMessage GetClientePorRFC(string rfc)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _mobile.BuscarClientePorRFC(rfc));
        }
        [Route("cliente/lista-clientes/{criterio}")]
        public HttpResponseMessage GetListaClientes(string criterio)
        {
            return RespuestaHttp.crearRespuesta(_mobile.BuscadorClientes(criterio), Request);
        }
        #endregion
        /// <summary>
        /// Permite realizar el registro de la venta, toma de parametro los
        /// datos de la misma que fueron enviados desde el telefono
        /// </summary>
        /// <param name="venta">Objeto de tipo VentaDTO con los datos de la venta</param>
        /// <returns>RespuestaDTO con el reusltado de la operación</returns>
        [Route("venta")]
        public HttpResponseMessage PostVenta(VentaDTO venta)
        {
            return RespuestaHttp.crearRespuesta(_mobile.Venta(venta), Request);
        }
        /// <summary>
        /// Retorna el precio vigente de la venta de gas por empresa
        /// </summary>
        /// <returns>Objeto PrecioVentaDTO con los valores de venta de gas</returns>
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
        [Route("catalogos/venta-gas/{esLP}/{esCilindroConGas}/{esCilindro}/{idCliente}")]
        public HttpResponseMessage GetCatalogosGas(bool esLP, bool esCilindroConGas, bool esCilindro, int idCliente)
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogosGas(esLP, esCilindroConGas, esCilindro, idCliente), Request);
        }
        /// <summary>
        /// Permite obtener el catalogo de venta de otros productos que 
        /// no sea gas para las camionetas , se retornara un objeto de tipo 
        /// DatosOtrosDTO con las listas de otras ventas 
        /// </summary>
        /// <returns>Listado de productos, lineas y categorias de otras ventas (Ej. Valbulas de gas )</returns>
        [Route("catalogos/venta-gas/otros")]
        public HttpResponseMessage GetOtros()
        {
            return RespuestaHttp.crearRespuesta(_mobile.catalogoOtros(), Request);
        }
        /// <summary>
        /// Permite retornar el catalogo de cilindros de gas para el punto de venta 
        /// </summary>
        /// <returns>lista de tipo DatosGasVentaDTO con los cilindros de la camioneta </returns>
        [Route("catalogos/venta-gas/catalgocilindros")]
        public HttpResponseMessage GetCatalogosClilindro()
        {
            return RespuestaHttp.crearRespuesta(_mobile.CatalogosGas(), Request);
        }
        /// <summary>
        /// Retorna el listado de estaciónes para el aprtado de punto de venta
        /// </summary>
        /// <returns>RespuestaDTO con la lista de estaciónes</returns>
        [Route("estacion/punto-venta")]
        public HttpResponseMessage GetEstacion()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerEstacion(), Request);
        }

        /// <summary>
        /// Permite determinar si el cliente que se envia de parametro en idCliente 
        /// ya tiene atorizado por parte del area de cobranza realizarle una venta extraforanea
        /// retornara un objeto DTO con el resultado de esta respuesta. 
        /// </summary>
        /// <param name="idCliente">Short que reprcenta el id del cliente </param>
        /// <returns>Objeto DTO con el rersultado de la conslta </returns>
        [Route("venta-extraforanea/{idCliente}")]
        public HttpResponseMessage GetVentaExtraforanea(short idCliente)
        {
            return RespuestaHttp.crearRespuesta(_mobile.tieneVentaExtraforanea(idCliente), Request);
        }
        /// <summary>
        /// Permite verificar antes de realizar una venta el dia de hoy no exista ya un corte de caja 
        /// retornarta un objeto de respuesta.
        /// </summary>
        /// <param name="dia">Día del corte que se requiere consultar</param>
        /// <param name="mes">Mes del quen se requiere consultar</param>
        /// <param name="year">Año que se reuiqere consultar</param>
        /// <returns>Retorna una respuesta determinando si ya previamente se ha hecho un corte</returns>
        [Route("hay-corte-estacion/{fecha}")]
        public HttpResponseMessage GetHayCorte(DateTime fecha)
        {
            return RespuestaHttp.crearRespuesta(_mobile.GetHayCorte(fecha), Request);
        }
        #endregion

        #region Revision de lectura inicial
        /// <summary>
        /// Verifica que la estación realizo su lectura inicial 
        /// retornara un objeto de tipo RespuestaDTO 
        /// </summary>
        /// <returns>Retorna un Objeto de tipo RespuestaDTO con el resultado de esta consulta</returns>
        /*[Route("estacion/verificar-lectura")]
        public HttpResponseMessage GetVerificarLecturaIncial()
        {
            return RespuestaHttp.crearRespuesta(_mobile.VerificarLecturaInicial(), Request);
        }*/
        #endregion
        [AllowAnonymous]
        [Route("test/numeroreporte")]
        public HttpResponseMessage GetNumeroReporte()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _mobile.testFuncionNumeroReporte());
        }
    }
}
