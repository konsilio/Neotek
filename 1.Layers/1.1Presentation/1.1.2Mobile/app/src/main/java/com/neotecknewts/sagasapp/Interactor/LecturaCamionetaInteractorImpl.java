package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenter;
import com.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LecturaCamionetaInteractorImpl implements LecturaCamionetaInteractor {
    private LecturaCamionetaPresenter lecturaCamionetaPresenter;
    public LecturaCamionetaInteractorImpl(LecturaCamionetaPresenterImpl lecturaCamionetaPresenter) {
        this.lecturaCamionetaPresenter = lecturaCamionetaPresenter;
    }

    @Override
    public void GetListCamionetas(String token,boolean esFinalizar) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosTomaLecturaDto> call = restClient.getEstacionesCarburacion(
                false,
                false,
                true,
                esFinalizar,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosTomaLecturaDto>() {
            @Override
            public void onResponse(Call<DatosTomaLecturaDto> call,
                                   Response<DatosTomaLecturaDto> response) {
                if (response.isSuccessful()) {
                    DatosTomaLecturaDto data = response.body();
                    Log.w("Estatus","Success");
                    lecturaCamionetaPresenter.onSuccessCamionetas(data);
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
                    lecturaCamionetaPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<DatosTomaLecturaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaCamionetaPresenter.onError();
            }
        });
    }
}
