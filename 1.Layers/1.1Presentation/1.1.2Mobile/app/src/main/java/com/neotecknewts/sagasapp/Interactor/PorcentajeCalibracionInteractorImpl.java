package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;
import com.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenter;
import com.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

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
        Log.w("Url base", ApiClient.BASE_URL);

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
