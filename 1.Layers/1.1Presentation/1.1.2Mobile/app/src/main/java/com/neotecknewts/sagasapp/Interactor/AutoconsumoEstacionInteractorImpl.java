package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoEstacionPresenter;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoEstacionPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AutoconsumoEstacionInteractorImpl implements AutoconsumoEstacionInteractor {
    AutoconsumoEstacionPresenter presenter;

    public AutoconsumoEstacionInteractorImpl(AutoconsumoEstacionPresenterImpl presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getList(String token,boolean esFinal) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosAutoconsumoDTO> call = restClient.getDatosAutoconsumo(
                true,
                false,
                false,
                esFinal,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosAutoconsumoDTO>() {
            @Override
            public void onResponse(Call<DatosAutoconsumoDTO> call, Response<DatosAutoconsumoDTO> response) {
                DatosAutoconsumoDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccessList(data);
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
            public void onFailure(Call<DatosAutoconsumoDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
