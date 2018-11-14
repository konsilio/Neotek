package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Presenter.RecargaPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RecargaPipaPresenterImpl;
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

public class RecargaPipaInteractorImpl implements RecargaPipaInteractor {
    RecargaPipaPresenter presenter;

    public RecargaPipaInteractorImpl(RecargaPipaPresenter presenter) {
        this.presenter = presenter;
    }

    public void getList(String token,boolean EsRecargaPipaFinal) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<DatosRecargaDto> call = restClient.getCatalogsRecarga(
                false,
                true,
                false,
                /*EsRecargaPipaFinal,*/
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

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
