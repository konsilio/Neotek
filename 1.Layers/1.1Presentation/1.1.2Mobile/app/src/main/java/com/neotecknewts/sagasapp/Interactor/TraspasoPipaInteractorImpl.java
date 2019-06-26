package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenter;
import com.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenterImpl;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TraspasoPipaInteractorImpl implements TraspasoPipaInteractor {
    TraspasoPipaPresenter presenter;
    public TraspasoPipaInteractorImpl(TraspasoPipaPresenterImpl presenter) {
        this.presenter = presenter;
    }

    @Override
    public void GetList(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosTraspasoDTO> call = restClient.getDatosTraspaso(
                true,
                /*false,*/
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosTraspasoDTO>() {
            @Override
            public void onResponse(Call<DatosTraspasoDTO> call, Response<DatosTraspasoDTO> response) {
                DatosTraspasoDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
                }
                else {
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
                    presenter.onError(response.message());

                }

            }

            @Override
            public void onFailure(Call<DatosTraspasoDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
