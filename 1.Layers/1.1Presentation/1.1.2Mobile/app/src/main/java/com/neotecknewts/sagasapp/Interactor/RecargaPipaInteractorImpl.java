package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.neotecknewts.sagasapp.Presenter.RecargaPipaPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RecargaPipaInteractorImpl implements RecargaPipaInteractor {
    RecargaPipaPresenter presenter;

    public RecargaPipaInteractorImpl(RecargaPipaPresenter presenter) {
        this.presenter = presenter;
    }

    public void getList(String token,boolean EsRecargaPipaFinal) {
        

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosRecargaDto> call = restClient.getCatalogsRecarga(
                false,
                true,
                false,
                /*EsRecargaPipaFinal,*/
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosRecargaDto>() {
            @Override
            public void onResponse(Call<DatosRecargaDto> call, Response<DatosRecargaDto> response) {
                if (response.isSuccessful()) {
                    DatosRecargaDto data = response.body();
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
                    presenter.onError("Error : "+response.message());
                }

            }

            @Override
            public void onFailure(Call<DatosRecargaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.getMessage());
                presenter.onError(t.getMessage());
            }
        });
    }
}
