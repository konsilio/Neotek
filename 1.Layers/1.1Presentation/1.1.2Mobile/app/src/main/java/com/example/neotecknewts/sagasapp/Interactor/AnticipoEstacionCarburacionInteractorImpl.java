package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoEstacionCarburacionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class AnticipoEstacionCarburacionInteractorImpl implements AnticipoEstacionCarburacionInteractor {
    AnticipoEstacionCarburacionPresenter presenter;
    public AnticipoEstacionCarburacionInteractorImpl(AnticipoEstacionCarburacionPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getEstaciones(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaEstacionesVentaDTO> call = restClient.getEstaciones(
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaEstacionesVentaDTO>() {
            @Override
            public void onResponse(Call<RespuestaEstacionesVentaDTO> call, Response<RespuestaEstacionesVentaDTO> response) {
                RespuestaEstacionesVentaDTO data = response.body();
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
            public void onFailure(Call<RespuestaEstacionesVentaDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }

    /**
     * checkLecturas
     * Permite verificar si actualmente la estación con la que se realiza el corte
     * tiene la lectura inicial y final , retornara un objeto de tipo RespuestaDTO
     * con el resultado de la consulta
     * @param token String que reprecenta el token de seguridad de la sesion del
     *              usuario
     */
    @Override
    public void checkLecturas(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaVerificarLecturasDTO> call = restClient.getHasLecturas(
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaVerificarLecturasDTO>() {
            @Override
            public void onResponse(Call<RespuestaVerificarLecturasDTO> call, Response<RespuestaVerificarLecturasDTO> response) {
                RespuestaVerificarLecturasDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccessVerificarLecturas(data);
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
                    if(respuesta!=null){
                        data = new RespuestaVerificarLecturasDTO();

                        try {
                            data.setExito(respuesta.getBoolean("Exito"));
                            data.setMensaje(respuesta.getString("Mensaje"));
                            presenter.onSuccessVerificarLecturas(data);
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }else {
                        presenter.onErrorVerificarLecturas(response.message());
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaVerificarLecturasDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onErrorVerificarLecturas(t.getLocalizedMessage());
            }
        });
    }
}
