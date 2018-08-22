package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
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

    //@POST(Constantes.VERIFICA_SERVICIO)
    //Call<RespuestaServicioDisponibleDTO> postServicio(@Header("Authorization")String token, @Header("Content-Type") String contentType);

    @POST(Constantes.POST_PAPELETA)
    Call<RespuestaPapeletaDTO> postPapeleta(@Body PrecargaPapeletaDTO papeletaDTO, @Header("Authorization")String token,@Header("Content-Type") String contentType);
}
