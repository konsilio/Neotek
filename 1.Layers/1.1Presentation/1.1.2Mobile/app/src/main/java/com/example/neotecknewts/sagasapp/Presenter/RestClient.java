package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

/**
 * Created by neotecknewts on 08/08/18.
 */

public interface RestClient {

    String LIST_EMPRESAS = "mobile/empresas/listaempresaslogin";
    @GET(LIST_EMPRESAS)
    Call<List<EmpresaDTO>> getListEmpresas();

    @POST("mobile/login")
    Call<UsuarioDTO> postLogin(@Body UsuarioLoginDTO loginBody);


}
