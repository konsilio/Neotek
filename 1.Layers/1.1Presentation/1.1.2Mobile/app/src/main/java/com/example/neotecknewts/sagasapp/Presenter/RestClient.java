package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Util.Constantes;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
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
    @POST(Constantes.POST_iINICIAR_DESCARGA)
    Call<RespuestaIniciarDescargaDTO> postDescarga(@Body IniciarDescargaDTO iniciarDescargaDTO,
                                                   @Header("Authorization")String token,
                                                   @Header("Content-Type") String contentType
                                                  );

}
