package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Presenter.MainPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class GeneralInteractorImpl implements GeneralInteractor {
    public static final String TAG = "Interactor";
    MainPresenter mainPresenter;

    public GeneralInteractorImpl(MainPresenter mainPresenter){
        this.mainPresenter = mainPresenter;
    }
    @Override
    public void webServiceCall() {

    }

    @Override
    public void loadJSON(){
        String url = Constantes.BASE_URL+"mobile/empresas/listaempresaslogin/";
        Log.w(TAG,url);
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<EmpresaDTO>> call = restClient.getData();

        call.enqueue(new Callback<List<EmpresaDTO>>() {
            @Override
            public void onResponse(Call<List<EmpresaDTO>> call, Response<List<EmpresaDTO>> response) {
                if (response.isSuccessful()) {
                    List<EmpresaDTO> data = response.body();
                    Log.w(TAG,data.get(0).getNombreComercial());
                    mainPresenter.onSuccessGetEmpresas(data);
                }
                else {
                    Log.e("error", "error");
                }

            }

            @Override
            public void onFailure(Call<List<EmpresaDTO>> call, Throwable t) {
                Log.e("error", t.toString());
            }
        });
    }

}
