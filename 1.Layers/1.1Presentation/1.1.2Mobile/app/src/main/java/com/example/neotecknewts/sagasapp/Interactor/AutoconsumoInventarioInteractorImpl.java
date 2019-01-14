package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Presenter.AutoconsumoInventarioPresenter;
import com.example.neotecknewts.sagasapp.Presenter.AutoconsumoInventarioPresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class AutoconsumoInventarioInteractorImpl implements AutoconsumoInventarioInteractor {
    AutoconsumoInventarioPresenter presenter;
    public AutoconsumoInventarioInteractorImpl(AutoconsumoInventarioPresenterImpl presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getList(String token, boolean esAutoconsumoInventarioFinal) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosAutoconsumoDTO> call = restClient.getDatosAutoconsumo(
                false,
                true,
                false,
                esAutoconsumoInventarioFinal,
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosAutoconsumoDTO>() {
            @Override
            public void onResponse(Call<DatosAutoconsumoDTO> call, Response<DatosAutoconsumoDTO> response) {
                DatosAutoconsumoDTO data = response.body();
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
                    if(data!= null){
                        presenter.onError(data.getMensajesError());
                    }else{
                        presenter.onError(response.message());
                    }
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
