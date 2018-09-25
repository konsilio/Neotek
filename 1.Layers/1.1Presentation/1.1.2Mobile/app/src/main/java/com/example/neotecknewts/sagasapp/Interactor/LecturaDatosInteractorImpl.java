package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaDatosPresenterImpl;
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

public class LecturaDatosInteractorImpl implements LecturaDatosInteractor {
    public LecturaDatosPresenter lecturaDatosPresenter;
    public LecturaDatosInteractorImpl(LecturaDatosPresenterImpl lecturaDatosPresenter){
        this.lecturaDatosPresenter = lecturaDatosPresenter;
    }

    @Override
    public void getMedidores(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    Log.w("Estatus","Success");
                    lecturaDatosPresenter.onSuccessGetMedidores(data);
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
                    lecturaDatosPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaDatosPresenter.onError();
            }
        });
    }

    @Override
    public void getEstacionesCarburacion(String token,boolean esFinalizar) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<DatosTomaLecturaDto> call = restClient.getEstacionesCarburacion(
                true,
                false,
                false,
                esFinalizar,
                token,
                "application/json"
                );
        Log.w("Url base",call.request().url().toString());

        call.enqueue(new Callback<DatosTomaLecturaDto>() {
            @Override
            public void onResponse(Call<DatosTomaLecturaDto> call, Response<DatosTomaLecturaDto> response) {
                if (response.isSuccessful()) {
                    DatosTomaLecturaDto data = response.body();
                    Log.w("Estatus","Success");
                    lecturaDatosPresenter.onSuccessGetEstacionesCarburacion(data);
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
                    Log.w("Error",response.message());
                    lecturaDatosPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<DatosTomaLecturaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaDatosPresenter.onError();
            }
        });
    }

}
