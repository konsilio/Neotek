package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.neotecknewts.sagasapp.Presenter.RecargaCamionetaPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RecargaCamionetaInteractorImpl implements RecargaCamionetaInteractor {
    private RecargaCamionetaPresenterImpl recargaCamionetaPresenter;
    public RecargaCamionetaInteractorImpl(RecargaCamionetaPresenterImpl recargaCamionetaPresenter) {
        this.recargaCamionetaPresenter = recargaCamionetaPresenter;
    }

    @Override
    public void getCamionetas(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosRecargaDto> call = restClient.getCatalogsRecarga(
                false,
                false,
                true,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL.toString());

        call.enqueue(new Callback<DatosRecargaDto>() {
            @Override
            public void onResponse(Call<DatosRecargaDto> call, Response<DatosRecargaDto> response) {

                if (response.isSuccessful()) {
                    DatosRecargaDto data = response.body();
                    Log.w("Estatus","Success");
                    recargaCamionetaPresenter.onSuccessCamionetas(data);
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
                    recargaCamionetaPresenter.onError(mensaje);
                }

            }

            @Override
            public void onFailure(Call<DatosRecargaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                recargaCamionetaPresenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });
    }
}
