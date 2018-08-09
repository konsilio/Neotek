package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

/**
 * Created by neotecknewts on 08/08/18.
 */

public interface RestClient {
    @GET("listaEmpresasLogin")
    Call<List<EmpresaDTO>> getData();

}
