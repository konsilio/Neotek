package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenter;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class PuntoVentaSolicitarInteractorImpl implements PuntoVentaSolicitarInteractor {
    PuntoVentaSolicitarPresenter presenter;

    public PuntoVentaSolicitarInteractorImpl(PuntoVentaSolicitarPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void hayCorte(String token) {
        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
        Calendar fecha  = Calendar.getInstance();
        String sfecha = fecha.get(Calendar.YEAR)+"-"+(fecha.get(Calendar.MONTH)+1)+"-"+fecha.get(Calendar.DATE);
        Call<RespuestaCortesAntesVentaDTO> call = restClient.getHayVenta(
                sfecha,
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaCortesAntesVentaDTO>() {
            @Override
            public void onResponse(Call<RespuestaCortesAntesVentaDTO> call,
                                   Response<RespuestaCortesAntesVentaDTO> response) {
                RespuestaCortesAntesVentaDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
                }
                else {
                    JSONObject respuesta = null;
                    try {
                        respuesta = new JSONObject(response.errorBody().string());

                    } catch (JSONException e) {
                        e.printStackTrace();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
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
                    presenter.onError(mensaje);
                }

            }

            @Override
            public void onFailure(Call<RespuestaCortesAntesVentaDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.toString());
            }

        });
    }
}
