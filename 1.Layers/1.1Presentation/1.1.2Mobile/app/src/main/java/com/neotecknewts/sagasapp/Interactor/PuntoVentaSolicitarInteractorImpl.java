package com.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PuntoVentaSolicitarInteractorImpl implements PuntoVentaSolicitarInteractor {
    PuntoVentaSolicitarPresenter presenter;
    Context context;
    public PuntoVentaSolicitarInteractorImpl(PuntoVentaSolicitarPresenter presenter, Context context) {
        this.presenter = presenter;
        this.context = context;
    }

    public void saveSiHayCorte(boolean siHay){
        SharedPreferences prefs = this.context.getSharedPreferences("CORTE", Context.MODE_PRIVATE);

        SharedPreferences.Editor editor = prefs.edit();
        editor.putBoolean("siHay", siHay);
        editor.commit();
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
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaCortesAntesVentaDTO>() {
            @Override
            public void onResponse(Call<RespuestaCortesAntesVentaDTO> call,
                                   Response<RespuestaCortesAntesVentaDTO> response) {
                RespuestaCortesAntesVentaDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    saveSiHayCorte(data.isHayCorte());
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
