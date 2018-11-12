package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenterImpl;
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

public class LecturaCamionetaInteractorImpl implements LecturaCamionetaInteractor {
    private LecturaCamionetaPresenter lecturaCamionetaPresenter;
    public LecturaCamionetaInteractorImpl(LecturaCamionetaPresenterImpl lecturaCamionetaPresenter) {
        this.lecturaCamionetaPresenter = lecturaCamionetaPresenter;
    }

    @Override
    public void GetListCamionetas(String token,boolean esFinalizar) {
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
        Call<DatosTomaLecturaDto> call = restClient.getEstacionesCarburacion(
                false,
                false,
                true,
                esFinalizar,
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

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
