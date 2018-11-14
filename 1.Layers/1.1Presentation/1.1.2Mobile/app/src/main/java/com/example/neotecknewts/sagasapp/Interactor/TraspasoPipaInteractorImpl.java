package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class TraspasoPipaInteractorImpl implements TraspasoPipaInteractor {
    TraspasoPipaPresenter presenter;
    public TraspasoPipaInteractorImpl(TraspasoPipaPresenterImpl presenter) {
        this.presenter = presenter;
    }

    @Override
    public void GetList(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<DatosTraspasoDTO> call = restClient.getDatosTraspaso(
                true,
                /*false,*/
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

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
