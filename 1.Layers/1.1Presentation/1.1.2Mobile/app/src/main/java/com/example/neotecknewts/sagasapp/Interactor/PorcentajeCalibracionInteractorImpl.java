package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;
import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class PorcentajeCalibracionInteractorImpl implements PorcentajeCalibracionInteractor {
    PorcentajeCalibracionPresenter presenter;
    public PorcentajeCalibracionInteractorImpl(PorcentajeCalibracionPresenterImpl presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getPorcentaje(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosEmpresaConfiguracionDTO> call = restClient.getDatosConfiguracionEmpresa(
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosEmpresaConfiguracionDTO>() {
            @Override
            public void onResponse(Call<DatosEmpresaConfiguracionDTO> call,
                                   Response<DatosEmpresaConfiguracionDTO> response) {

                if (response.isSuccessful()) {
                    DatosEmpresaConfiguracionDTO data = response.body();
                    Log.w("Estatus","Success");
                    presenter.onSuccessPorcentaje(data);
                }
                else {
                    String mensaje = "";
                    switch (response.code()) {
                        case 404:
                            Log.w("Error","not found");

                            break;
                        case 500:
                            Log.w("Error", "server broken");

                            break;
                        default:
                            Log.w("Error", "Error desconocido: "+response.code());

                            break;
                    }
                    mensaje = "Se ha generado un error: "+response.message();
                    presenter.onError(mensaje);
                }

            }

            @Override
            public void onFailure(Call<DatosEmpresaConfiguracionDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });
    }
}
