package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.ClienteDTO;
import com.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaClienteDTO;
import com.neotecknewts.sagasapp.Presenter.RegistroClientePresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RegistroClienteInteractorImpl implements RegistroClienteInteractor {
    RegistroClientePresenter presenter;
    public RegistroClienteInteractorImpl(RegistroClientePresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getLista(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
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


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
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
