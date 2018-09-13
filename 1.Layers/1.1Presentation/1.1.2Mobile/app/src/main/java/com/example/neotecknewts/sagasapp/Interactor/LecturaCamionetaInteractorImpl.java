package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

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
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<EstacionCarburacionDTO>> call = restClient.getEstacionesCarburacion(
                false,
                true,
                true,
                esFinalizar,
                token
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<EstacionCarburacionDTO>>() {
            @Override
            public void onResponse(Call<List<EstacionCarburacionDTO>> call, Response<List<EstacionCarburacionDTO>> response) {
                if (response.isSuccessful()) {
                    List<EstacionCarburacionDTO> data = response.body();
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
            public void onFailure(Call<List<EstacionCarburacionDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaCamionetaPresenter.onError();
            }
        });
    }
}
