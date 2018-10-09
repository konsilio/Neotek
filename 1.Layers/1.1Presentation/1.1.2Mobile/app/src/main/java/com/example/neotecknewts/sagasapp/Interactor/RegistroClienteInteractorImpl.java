package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaClienteDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistroClientePresenter;
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

public class RegistroClienteInteractorImpl implements RegistroClienteInteractor {
    RegistroClientePresenter presenter;
    public RegistroClienteInteractorImpl(RegistroClientePresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getLista(String token) {
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
        Call<DatosTipoPersonaDTO> call = restClient.getDatosTipoRason(token,"application/json");

        call.enqueue(new Callback<DatosTipoPersonaDTO>() {
            @Override
            public void onResponse(Call<DatosTipoPersonaDTO> call, Response<DatosTipoPersonaDTO> response) {
                DatosTipoPersonaDTO data = response.body();
                if (response.isSuccessful()) {
                    data = response.body();
                    presenter.onSuccess(data);
                }
                else {
                    data = response.body();
                    presenter.onError(data);
                    /*switch (response.code()) {
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
                    }*/
                }

            }

            @Override
            public void onFailure(Call<DatosTipoPersonaDTO> call, Throwable t) {
                Log.e("error", t.toString());
                presenter.onError(t.getMessage());
            }
        });
    }

    @Override
    public void registrarCliente(ClienteDTO clienteDTO, String token) {
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
        Call<RespuestaClienteDTO> call = restClient.registrarCliente(
                clienteDTO,
                token,
                "application/json"
        );

        call.enqueue(new Callback<RespuestaClienteDTO>() {
            @Override
            public void onResponse(Call<RespuestaClienteDTO> call, Response<RespuestaClienteDTO> response) {
                RespuestaClienteDTO data = response.body();
                if (response.isSuccessful()) {
                    data = response.body();
                    presenter.onSuccessRegistro(data);
                }
                else {
                    data = response.body();
                    presenter.onErrorRegistro(data);
                    /*switch (response.code()) {
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
                    }*/
                }

            }

            @Override
            public void onFailure(Call<RespuestaClienteDTO> call, Throwable t) {
                Log.e("error", t.toString());
                presenter.onError(t.getMessage());
            }
        });
    }
}
