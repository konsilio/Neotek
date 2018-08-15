package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.IniciarDescargaPresenter;
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

public class IniciarDescargaInteractorImpl implements IniciarDescargaInteractor {
    public static final String TAG = "IniciarDescInteractor";
    IniciarDescargaPresenter iniciarDescargaPresenter;

    public IniciarDescargaInteractorImpl(IniciarDescargaPresenter iniciarDescargaPresenter){
        this.iniciarDescargaPresenter = iniciarDescargaPresenter;
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
        Call<RespuestaOrdenesCompraDTO> call = restClient.getOrdenesCompra(IdEmpresa,true,true,false,token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<RespuestaOrdenesCompraDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenesCompraDTO> call, Response<RespuestaOrdenesCompraDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenesCompraDTO data = response.body();
                    Log.w(TAG,"Success");
                    iniciarDescargaPresenter.onSuccessGetOrdenesCompra(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            iniciarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            iniciarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            iniciarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                iniciarDescargaPresenter.onError();
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
                    iniciarDescargaPresenter.onSuccessGetMedidores(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            iniciarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            iniciarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            iniciarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                iniciarDescargaPresenter.onError();
            }
        });
    }

    @Override
    public void getAlmacenes(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<AlmacenDTO>> call = restClient.getAlmacenes(token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<AlmacenDTO>>() {
            @Override
            public void onResponse(Call<List<AlmacenDTO>> call, Response<List<AlmacenDTO>> response) {
                if (response.isSuccessful()) {
                    List<AlmacenDTO> data = response.body();
                    Log.w(TAG,"Success");
                    iniciarDescargaPresenter.onSuccessGetAlmacenes(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            iniciarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            iniciarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            iniciarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<AlmacenDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                iniciarDescargaPresenter.onError();
            }
        });
    }
}
