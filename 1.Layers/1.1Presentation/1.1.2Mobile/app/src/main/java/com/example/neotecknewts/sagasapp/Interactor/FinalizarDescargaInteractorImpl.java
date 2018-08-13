package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.FinalizarDescargaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class FinalizarDescargaInteractorImpl implements FinalizarDescargaInteractor {
    public static final String TAG = "FinalizarDescInteractor";
    FinalizarDescargaPresenter finalizarDescargaPresenter;

    public FinalizarDescargaInteractorImpl(FinalizarDescargaPresenter finalizarDescargaPresenter){
        this.finalizarDescargaPresenter = finalizarDescargaPresenter;
    }
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaOrdenesCompraDTO> call = restClient.getOrdenesCompra(IdEmpresa,token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<RespuestaOrdenesCompraDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenesCompraDTO> call, Response<RespuestaOrdenesCompraDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenesCompraDTO data = response.body();
                    Log.w(TAG,"Success");
                    finalizarDescargaPresenter.onSuccessGetOrdenesCompra(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            finalizarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            finalizarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            finalizarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                finalizarDescargaPresenter.onError();
            }
        });
    }
}
