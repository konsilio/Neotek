package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PuntoVentaOtrosInteractorImpl implements PuntoVentaOtrosInteractor {
    PuntoVentaOtrosPresenter presenter;

    public PuntoVentaOtrosInteractorImpl(PuntoVentaOtrosPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getList(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosVentaOtrosDTO> call = restClient.getListasProductos(
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosVentaOtrosDTO>() {
            @Override
            public void onResponse(Call<DatosVentaOtrosDTO> call, Response<DatosVentaOtrosDTO> response) {
                DatosVentaOtrosDTO data = response.body();
                if (response.isSuccessful()) {

                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
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
                    if(data==null) {
                        presenter.onError(mensaje);
                    }else{
                        presenter.onError(data.getMensaje());
                    }
                }

            }

            @Override
            public void onFailure(Call<DatosVentaOtrosDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });

    }
}
