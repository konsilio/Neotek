package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Util.Constantes;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.Query;

/**
 * Created by neotecknewts on 08/08/18.
 */

public interface RestClient {


    @GET(Constantes.LISTA_EMPRESAS)
    Call<List<EmpresaDTO>> getListEmpresas();

    @POST(Constantes.LOGIN_URL)
    Call<UsuarioDTO> postLogin(@Body UsuarioLoginDTO loginBody,
                               @Header("Content-Type") String contentType);

    @GET(Constantes.LISTA_ORDENESCOMPRA_GAS)
    Call<RespuestaOrdenesCompraDTO> getOrdenesCompra(@Query("IdEmpresa") int IdEmpresa,
                                                     @Query("EsGas") boolean EsGas,
                                                     @Query("EsActivoVenta") boolean EsActivoVenta,
                                                     @Query("EsTransporteGas") boolean EsTransporteGas,
                                                     @Header("Authorization")String token);


}
