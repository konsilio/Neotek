package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenter;
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
 * Created by neotecknewts on 13/08/18.
 */

public class RegistrarPapeletaInteractorImpl implements RegistrarPapeletaInteractor {
    public static final String TAG = "RegistrarPapInteractor";
    RegistrarPapeletaPresenter registrarPapeletaPresenter;

    public RegistrarPapeletaInteractorImpl(RegistrarPapeletaPresenter registrarPapeletaPresenter){
        this.registrarPapeletaPresenter = registrarPapeletaPresenter;
    }

    @Override
    public void getOrdenesCompra(int IdEmpresa, final boolean EsGas, boolean EsActivoVenta, boolean EsTransporteGas , String token) {
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
        Call<RespuestaOrdenesCompraDTO> call = restClient.getOrdenesCompra(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas,token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<RespuestaOrdenesCompraDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenesCompraDTO> call, Response<RespuestaOrdenesCompraDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenesCompraDTO data = response.body();
                    Log.w(TAG,"Success");
                    if(EsGas) {
                        registrarPapeletaPresenter.onSuccessGetOrdenesCompraExpedidor(data);
                    }else{
                        registrarPapeletaPresenter.onSuccessGetOrdenesCompraPorteador(data);
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            registrarPapeletaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            registrarPapeletaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            registrarPapeletaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                registrarPapeletaPresenter.onError();
            }
        });
    }

    @Override
    public void getMedidores(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    Log.w(TAG,"Success");
                    registrarPapeletaPresenter.onSuccessGetMedidores(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            registrarPapeletaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            registrarPapeletaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            registrarPapeletaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                registrarPapeletaPresenter.onError();
            }
        });
    }
}
