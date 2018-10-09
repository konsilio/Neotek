package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenter;
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

public class PuntoVentaOtrosInteractorImpl implements PuntoVentaOtrosInteractor {
    PuntoVentaOtrosPresenter presenter;

    public PuntoVentaOtrosInteractorImpl(PuntoVentaOtrosPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getList(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<DatosVentaOtrosDTO> call = restClient.getListasProductos(
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<DatosVentaOtrosDTO>() {
            @Override
            public void onResponse(Call<DatosVentaOtrosDTO> call, Response<DatosVentaOtrosDTO> response) {
                DatosVentaOtrosDTO data = response.body();
                if (response.isSuccessful()) {

                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
                }
                else {
                    String mensaje = "";
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
                    mensaje = "Se ha generado un error: "+response.message();
                    if(data==null) {
                        presenter.onError(mensaje);
                    }else{
                        presenter.onError(data.getMensaje());
                    }
                }

            }

            @Override
            public void onFailure(Call<DatosVentaOtrosDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });

    }
}
