package com.example.neotecknewts.sagasapp.Presenter.Rest;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.DatosClientesDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.example.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.ReporteDto;
import com.example.neotecknewts.sagasapp.Model.RespuestaAnticipoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaClienteDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaCorteDto;
import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaFinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenReferenciaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.RespuestaRecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Util.Constantes;

import org.json.JSONObject;

import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

/**
 * Created by neotecknewts on 08/08/18.
 */
//interfaz que describe como seran las llamas a los web service
public interface RestClient {


    //la llamada para obtener empresas es por get no requiere parametros y usa la ruta declarada en constantes
    @GET(Constantes.LISTA_EMPRESAS)
    Call<List<EmpresaDTO>> getListEmpresas();

    //la llamada para hacer login es por post requiere parametros del objeto del usuario y usa la ruta declarada en constantes
    @POST(Constantes.LOGIN_URL)
    Call<UsuarioDTO> postLogin(@Body UsuarioLoginDTO loginBody,
                               @Header("Content-Type") String contentType);

    //la llamada para obtener ordenes decompra es por post requiere parametros de idempresa, si es gas, si es activo de venta y
    // si es transporte de gas y usa la ruta declarada en constantes
    @GET(Constantes.LISTA_ORDENESCOMPRA_GAS)
    Call<RespuestaOrdenesCompraDTO> getOrdenesCompra(@Query("IdEmpresa") int IdEmpresa,
                                                     @Query("EsGas") boolean EsGas,
                                                     @Query("EsActivoVenta") boolean EsActivoVenta,
                                                     @Query("EsTransporteGas") boolean EsTransporteGas,
                                                     @Header("Authorization")String token);

    //la llamada para obtener el menu es por post requiere parametros el token y usa la ruta declarada en constantes
    @GET(Constantes.LISTA_MENU)
    Call<List<MenuDTO>> getMenu(@Header("Authorization")String token);

    //la llamada para obtener la lista de almacenes es por post requiere parametros el token y usa la ruta declarada en constantes
    @GET(Constantes.LISTA_ALMACEN)
    Call<List<AlmacenDTO>> getAlmacenes(@Header("Authorization")String token);

    //la llamada para obtener la lista de medidores es por post requiere parametros el token y usa la ruta declarada en constantes
    @GET(Constantes.LISTA_MEDIDORES)
    Call<List<MedidorDTO>> getMedidores(@Header("Authorization")String token);

    /**
     * postServicio
     * Permite verificar si el servicio de .NET esta disponible
     * @param token {@link String} que reprecenta el token de seguridad  del usuario para el servicio
     * @param contentType El tipo de contenido con el que se trabaja (Ej. 'application/json')
     * @return Objeto {@link RespuestaServicioDisponibleDTO} con la respuesta que dio el web service
     * @author Jorge Omar Tovar Martínez
     */
    @POST(Constantes.VERIFICA_SERVICIO)
    Call<RespuestaServicioDisponibleDTO> postServicio(
            @Header("Authorization")String token, @Header("Content-Type") String contentType);

    /**
     * postPapeleta
     * Permite realizar el el vio de los datros de la papeleta al servicio web ,este retornara un
     * objeto de respuesta tipo {@link RespuestaPapeletaDTO} que da el servicio web
     * @param papeletaDTO Objeto de tipo {@link PrecargaPapeletaDTO} con los datos a enviar de la
     *                    papeleta
     * @param token {@link String} que reprecenta el token de seguridad del usuario
     * @param contentType {@link String} que reprecenta el tipo de contenido (Ej. 'application/json')
     * @return Objeto {@link RespuestaPapeletaDTO} que contiene la repspuesta del servicio web
     * @author Jorge Omar Tovar Martínez
     */
    @POST(Constantes.POST_PAPELETA)
    Call<RespuestaPapeletaDTO> postPapeleta(@Body PrecargaPapeletaDTO papeletaDTO,
                                            @Header("Authorization")String token,
                                            @Header("Content-Type") String contentType);

    /**
     * postDescarga
     * Permite realizar el consumo del servicio para el registro de la descarga, se enviaran como
     * parametros un objeto de tipo {@link IniciarDescargaDTO} que contiene los valores del
     * formulario , una cadena {@link String} que contiene el token de seguridad y un {@link String}
     * con el tipo de contenido enviado, al finalizar retornara una respuesta que se almacena en un
     * objeto de tipo {@link RespuestaIniciarDescargaDTO}.
     * @param iniciarDescargaDTO Objeto {@link IniciarDescargaDTO} con los datos de la descarga
     * @param token {@link String} que contiene el token de seguridad
     * @param contentType {@link String} que reprecenta el tipo de contenido  (Ej. 'application/json')
     * @return Objeto {@link RespuestaIniciarDescargaDTO} Con la respuesta del servicio
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @POST(Constantes.POST_INICIAR_DESCARGA)
    Call<RespuestaIniciarDescargaDTO> postDescarga(@Body IniciarDescargaDTO iniciarDescargaDTO,
                                                   @Header("Authorization")String token,
                                                   @Header("Content-Type") String contentType
                                                  );

    /**
     * postFinalizarDescarga
     * Permite realizar el consumo del servicio para el registro de la finalizacion de la descarga,
     * se requiere como parametros un objeto de tipo {@link FinalizarDescargaDTO} que contine los
     * valores de la finalizaciòn de la descarga, una cadena {@link String} que contiene el token
     * de seguridad y finalmente un {@link String} que contiene el tipo de contenido enviado,
     * tras finalizar se retornara una repuesta que se almacenara en un objeto de tipo
     * {@link RespuestaFinalizarDescargaDTO}.
     * @param finalizarDescargaDTO Objeto {@link FinalizarDescargaDTO} con los datos de la
     *                             finalización de la descarga
     * @param token {@link String} que contiene  el token de seguridad
     * @param contentType {@link String} que reprecenta el tipo de contenido (Ej. 'application/json')
     * @return Un objeto de tipo {@link RespuestaFinalizarDescargaDTO} Con lo que responde el servicio.
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @POST(Constantes.POST_FINALIZAR_DESCARGA)
    Call<RespuestaFinalizarDescargaDTO> postFinalizarDescarga(@Body FinalizarDescargaDTO
                                                                   finalizarDescargaDTO,
                                                              @Header("Authorization")String token,
                                                              @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_INICIAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaInicial(@Body LecturaDTO lecturaDTO,
                                                        @Header("Authorization")String token,
                                                        @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_FINAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaFinal(@Body LecturaDTO lecturaDTO,
                                                          @Header("Authorization")String token,
                                                          @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_INICIAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaInicialPipa(@Body LecturaPipaDTO lecturaDTO,
                                                            @Header("Authorization")String token,
                                                            @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_FINAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaFinalPipa(@Body LecturaPipaDTO lecturaDTO,
                                                                @Header("Authorization")String token,
                                                                @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_INICIAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaInicialAlmacen(@Body LecturaAlmacenDTO lecturaAlmacenDTO,
                                                                   @Header("Authorization")String token,
                                                                   @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_FINAL)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaFinalAlmacen(@Body LecturaAlmacenDTO lecturaAlmacenDTO,
                                                                 @Header("Authorization")String token,
                                                                 @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_INICIAL_CAMIONETA)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaInicialCamioneta(@Body LecturaCamionetaDTO lecturaAlmacenDTO,
                                                                   @Header("Authorization")String token,
                                                                   @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_LECTURA_FINAL_CAMIONETA)
    Call<RespuestaLecturaInicialDTO> postTomaLecturaFinalCamioneta(@Body LecturaCamionetaDTO  lecturaAlmacenDTO,
                                                                 @Header("Authorization")String token,
                                                                 @Header("Content-Type") String contentType
    );
    @GET(Constantes.LISTA_TIPO_ALMACEN)
    Call<DatosTomaLecturaDto> getEstacionesCarburacion(
            @Path(value = "esEstacion", encoded=true) boolean esEstacion,
            @Path(value = "esPipa", encoded=true) boolean esPipa,
            @Path(value = "esCamioneta", encoded=true) boolean esCamioneta,
            @Path(value = "esFinalizar", encoded=true) boolean esFinalizar,
            @Header("Authorization")String token,
            @Header("Content-Type") String contentType
    );
    @POST(Constantes.POST_RECARGA)
    Call<RespuestaRecargaDTO> postRecarga(@Body RecargaDTO recargaDTO,
                                          @Header("Authorization")String token,
                                          @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_RECARGA_INCIAL)
    Call<RespuestaRecargaDTO> postRecargaInicial(@Body RecargaDTO recargaDTO,
                                          @Header("Authorization")String token,
                                          @Header("Content-Type") String contentType
    );

    @POST(Constantes.POST_RECARGA_FINAL)
    Call<RespuestaRecargaDTO> postRecargaFinal(@Body RecargaDTO recargaDTO,
                                                 @Header("Authorization")String token,
                                                 @Header("Content-Type") String contentType
    );

    @GET(Constantes.GET_UNIDADES)
    Call<DatosReporteDTO> getUnidades(@Header("Authorization") String token,
                                      @Header("Content-Type") String contentType);
    @GET(Constantes.GET_CATALOGO_RECARGAS)
    Call<DatosRecargaDto> getCatalogsRecarga(
            @Path(value = "esEstacion", encoded=true) boolean esEstacion,
            @Path(value = "esPipa", encoded=true)boolean esPipa,
            @Path(value = "esCamioneta", encoded=true)boolean esCamioneta,
           /* @Path(value = "esFinalizar", encoded=true)boolean esFinalizar,*/
            @Header("Authorization")String token,
            @Header("Content-Type") String contentType
    );
    @GET(Constantes.GETCATALOGO_AUTOCONSUMO)
    Call<DatosAutoconsumoDTO> getDatosAutoconsumo(
                                                  @Path(value = "esEstacion",encoded = true)
                                                          boolean esEstacion,
                                                  @Path(value = "esInventario",encoded = true)
                                                          boolean esInventario,
                                                  @Path(value =  "esPipas",encoded = true)
                                                          boolean esPipa,
                                                  @Path(value = "esFinal",encoded = true)
                                                          boolean esFinalizar,
                                                  @Header("Authorization") String token,
                                                  @Header("Content-Type") String contentType
                                                  );
    @POST(Constantes.POST_AUTOCONSUMO)
    Call<RespuestaRecargaDTO> postAutorconsumo(@Body AutoconsumoDTO autoconsumoDTO,
                                               /*@Path(value = "esEstacion") boolean esEstacion,
                                               @Path(value = "esIventario") boolean esInventario,
                                               @Path(value = "esPipa") boolean esPipa,*/
                                               @Path(value = "esFinal") boolean esFinal,
                                               @Header("Authorization") String token,
                                               @Header("Content-type") String contentType
    );
    @GET(Constantes.GET_CATALOGO_TRASPASO)
    Call<DatosTraspasoDTO> getDatosTraspaso(/*@Path(value = "esTraspaso") boolean esTraspaso,*/
                                            @Path(value = "esPipa")boolean esPipa,
                                            @Header("Authorization") String token,
                                            @Header("Content-type") String contentType
    );
    @POST(Constantes.POST_TRASPASO)
    Call<RespuestaTraspasoDTO> postTraspaso(@Body TraspasoDTO traspasoDTO,
                                            /*@Path(value = "esEstacion") boolean esEstacion,
                                            @Path(value = "esPipa") boolean esPipa,*/
                                            @Path(value = "esFinal") boolean esFinal,
                                            @Header("Authorization") String token,
                                            @Header("Content-type") String contentType
    );
    @GET(Constantes.GET_CATALOGO_CALIBRACION)
    Call<DatosCalibracionDTO> getDatosCalibracion(@Path(value = "esEstacion") boolean esEstacion,
                                                  @Path(value = "esPipa") boolean esPipa,
                                                  @Header("Authorization") String token,
                                                  @Header("Content-type") String contentType
    );
    @POST(Constantes.POST_CALIBRACION)
    Call<RespuestaTraspasoDTO> postCalibracion(@Body CalibracionDTO calibracionDTO,
                                               /*@Path(value = "esEstacion") boolean esEstacion,
                                               @Path(value = "esPipa") boolean esPipa,*/
                                               @Path(value= "esFinal") boolean esFinal,
                                               @Header("Authorization") String token,
                                               @Header("Content-type") String contentType
    );

    @GET(Constantes.GET_CONFIGURACION_EMPRESA)
    Call<DatosEmpresaConfiguracionDTO> getDatosConfiguracionEmpresa(
            @Header("Authorization") String token,
            @Header("Content-type") String contentType
    );

    @GET(Constantes.GET_CATALOGO_RAZON)
    Call<DatosTipoPersonaDTO> getDatosTipoRason(@Header("Authorization") String token,
                                                @Header("Content-type") String contentType
    );

    @POST(Constantes.POST_CLIENTE)
    Call<RespuestaClienteDTO> registrarCliente(@Body ClienteDTO clienteDTO,
                                               @Header("Authorization") String token,
                                               @Header("Content-type") String contentType
    );

    @GET(Constantes.GET_LIST_CLIENTES)
    Call<DatosClientesDTO> getListaClientes(@Path(value = "criterio") String criterio,
                                            @Header("Authorization")String token,
                                            @Header("Content-Type") String contentType
    );

    @GET(Constantes.GET_LIST_EXISTENCIAS)
    Call<List<ExistenciasDTO>> getListaExistencias(@Path(value = "esGasLP") boolean esGasLP,
                                                 @Path(value = "esCilindroConGas")
                                                         boolean esCilindroConGas,
                                                 @Path(value = "esCilindro") boolean esCilindro,
                                                 @Header("Authorization") String token,
                                                 @Header("Content-type") String contentType
    );
    @GET(Constantes.GET_CATALOGO_PRODUCTO)
    Call<DatosVentaOtrosDTO> getListasProductos( @Header("Authorization") String token,
                                                 @Header("Content-type") String contentType);
    @POST(Constantes.POST_VENTA)
    Call<RespuestaPuntoVenta> pagar(
            @Body VentaDTO ventaDTO,
            /*@Path(value = "esCamioneta") boolean esCamioneta,
            @Path(value = "esEstacion") boolean esEstacion,
            @Path(value = "esPipa") boolean esPipa,*/
            @Header("Authorization") String token,
            @Header("Content-type") String contentType);

    @POST(Constantes.POST_ANTICIPO)
    Call<RespuestaAnticipoDTO> postAnticipo(
            @Body AnticiposDTO anticiposDTO,
            @Header("Authorization") String token,
            @Header("Content-type") String contentType
    );

    @GET(Constantes.GET_CATALOGO_VENTAS_ESTACIONES)
    Call<RespuestaEstacionesVentaDTO> getEstaciones(
            @Header("Authorization") String token,
            @Header("Content-type") String contentType
    );
    @GET(Constantes.GET_REPORTE)
    Call<ReporteDto> getReporte(@Path(value = "idCAlmacenGas") int idCAlmacenGas,
                                @Path(value = "fecha") String fecha,
                                @Header("Authorization") String token,
                                @Header("Content-type") String contentType
    );

    @POST(Constantes.POST_CORTE)
    Call<RespuestaCorteDto> postCorte(@Body CorteDTO corteDTO,
                                      @Header("Authorization") String token,
                                      @Header("Content-type") String contentType);

    /**
     * getReferenciaOrden
     * Permite retornar el id de la orden de compra de referencia ya sea de expedidor o de porteador
     * se envia como parametros el id de la orden que se reuiqere obtener su referencia y el token
     * del usuario , retornara un objeto de tipo {@link RespuestaOrdenReferenciaDTO} con el id
     * de la referencia , en caso de que no tenga retornara un cero
     * @param idOrdenCompra int que reprecenta el id de la orden de compra
     * @param token {@link String} que contiene el token de seguridad
     * @param contenType Reprecenta el tipo de contenido que retornara la respuesta
     * @return Retorna un objeto {@link RespuestaOrdenReferenciaDTO} con el resultado de la solicitud
     * al api
     */
    @GET(Constantes.GET_ORDEN_REFERENCIA)
    Call<RespuestaOrdenReferenciaDTO> getReferenciaOrden(
            @Path(value = "IdOrdenCompra") int idOrdenCompra,
            @Header("Authorization") String token,
            @Header("Content-type") String contenType
    );
    /*@GET(Constantes.GET_CATALOGOS_ANTICIPOS_CORTE_LISTA)
    Call<RespuestaEstacionesVentaDTO> getAnticipo_y_Corte(
            @Path(value = "estacion") int estacion,
            @Path(value = "esAnticipos") boolean esAnticipos,
            @Path(value = "fecha") String fecha,
            @Header("Authorization") String token,
            @Header("Content-type") String contenType
    );*/
    @GET(Constantes.GET_CATALOGOS_ANTICIPOS_CORTE_LISTA)
    Call<DatosBusquedaCortesDTO> getAnticipo_y_Corte(
            @Path(value = "estacion") int estacion,
            @Path(value = "esAnticipos") boolean esAnticipos,
            @Path(value = "fecha") String fecha,
            @Header("Authorization") String token,
            @Header("Content-type") String contenType
    );
    @GET(Constantes.GET_CILINDROS_VENTA)
    Call<List<ExistenciasDTO>> getListaExistencias(
            @Header("Authorization") String token,
            @Header("Content-type") String contenType
    );

    /**
     * getPrecioVenta
     * Permite extraer los valores de venta de gas
     * @param token String que reprecenta el token de usuario
     * @param contenType Reprecenta el tipo de contenid
     * @return Un objeto de tipo {@link PrecioVentaDTO} con los valores de gas
     */
    @GET(Constantes.GET_PRECIO_VENTA)
    Call<PrecioVentaDTO> getPrecioVenta(
            @Header("Authorization") String token,
            @Header("Content-type") String contenType
    );

    @GET(Constantes.GET_PUNTO_VENTA_ASIGNADO)
    Call<PuntoVentaAsignadoDTO> getPuntoVentaAsigando(
            @Header("Authorization") String token,
            @Header("Content-type")  String contenType
    );

    /**
     * getUsuarios
     * Permite realizar la extración del listado de usuarios
     * @param token Token del usuario
     * @param contenType Indica el tipo de formato o contenido que se enviara en la solicitud al
     *                   api
     * @return Objeto de tipo {@link UsuariosCorteDTO} con los usuarios encontrados como resultado
     */
    @GET(Constantes.GET_USUARIOS_ANTICIPOS)
    Call<UsuariosCorteDTO> getUsuarios(
            @Header("Authorization") String token,
            @Header("Content-type")  String contenType
    );

    /**
     * getUsuariosCorte
     * permite retornar el listado de usuarios para el corte de caja, retornara desde el
     * api un objeto de tipo {@link UsuariosCorteDTO} en el cual se obtiene la respuesta del
     * api , en caso de ser correcta retornara el listado de usuarios encontrados
     * @param token String que reprecenta el token de usuario
     * @param contenType String que reprecenta el tipo de contenido enviado
     * @return Reprecenta un objeto de tipo {@link UsuariosCorteDTO} con el resultado del api
     */
    @GET(Constantes.GETUSUARIOS_CORTES)
    Call<UsuariosCorteDTO> getUsuariosCorte(
            @Header("Authorization") String token,
            @Header("Content-type")  String contenType
    );

    /**
     * getTieneVentaExtraforanea
     * Realiza la consulta desde el web api de que si el cliente cuenta con un acceso a
     * venta extraforanea, se retornara un objeto dto con la respuesta de este resultado
     * @param idCliente {@link Integer} que reprecenta el Id del cliente
     * @param token {@link String} que reprecenta el token de session
     * @param contenType Tipo de contenido o formato en que se envian los datos
     * @return Objeto de tipo {@link RespuestaVentaExtraforaneaDTO} con el resultado de la consulta
     */
    @GET(Constantes.GET_VENTA_EXTRAFORANEA)
    Call<RespuestaVentaExtraforaneaDTO> getTieneVentaExtraforanea(
            @Path(value = "idCliente") int idCliente,
            @Header("Authorization") String token,
            @Header("Content-type")  String contenType
    );

    /**
     * getHayVenta
     * Permite verificar si actualmente existe un corte en el día de hoy,
     * retornar un objeto de tipo {@link RespuestaCortesAntesVentaDTO} con
     * la respuesta que genero el servidor api
     * @param fecha Fecha actual o de consulta
     * @param token Token del usuario
     * @param contenType Tipo de contenido que se envía
     * @return Objeto de tipo {@link RespuestaCortesAntesVentaDTO} con la repsuesta
     *          que retorno el objeto
     */
    @GET(Constantes.GET_HAY_VENTA)
    Call<RespuestaCortesAntesVentaDTO> getHayVenta(@Path(value = "fecha") String fecha,
                                                   @Header("Authorization")  String token,
                                                   @Header("Content-type") String contenType
    );

    /**
     * getHasLecturas
     * Permite realizar una verificación de que si existe tanto lectura inicial como
     * lectura final antes de realizar el corte de caja
     * @param token Token del usuario
     * @param contenType Tipo de contenido enviado en la solicitud
     * @return Retornara un objeto de tipo {@link RespuestaVerificarLecturasDTO} con la respuesta
     *          obtenida por el servidor
     */
    @GET(Constantes.GET_HAY_LECTURAS)
    Call<RespuestaVerificarLecturasDTO> getHasLecturas(
            @Header("Authorization")  String token,
            @Header("Content-type") String contenType
    );
}
