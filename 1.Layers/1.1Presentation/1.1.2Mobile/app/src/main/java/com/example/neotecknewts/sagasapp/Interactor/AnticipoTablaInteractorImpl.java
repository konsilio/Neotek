package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaAnticipoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaCorteDto;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Lisener;
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

public class AnticipoTablaInteractorImpl implements AnticipoTablaInteractor {
    AnticipoTablaPresenter presenter;
    public AnticipoTablaInteractorImpl(AnticipoTablaPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token) {
        if(VerificarServicio(token)){

            RestClient restClient = ApiClient.getClient().create(RestClient.class);

            Call<RespuestaAnticipoDTO> call = restClient.
                    postAnticipo(anticiposDTO,token,"application/json");
            call.enqueue(new Callback<RespuestaAnticipoDTO>() {
                @Override
                public void onResponse(Call<RespuestaAnticipoDTO> call,
                                       Response<RespuestaAnticipoDTO> response) {
                    RespuestaAnticipoDTO data = response.body();
                    if(response.isSuccessful() && data.isExito())
                        presenter.onSuccess();
                    else
                        if(data!=null)
                            Log.e("Error anticipo",data.getMensaje());
                        else
                            Log.e("Error anticipo",response.code()+": "+
                                    response.message());
                        if(response.code()>=300) {
                            registra_local(anticiposDTO,sagasSql,token);
                            presenter.onSuccessAndroid();
                        }
                }

                @Override
                public void onFailure(Call<RespuestaAnticipoDTO> call, Throwable t) {
                    presenter.onError(t.getMessage());
                    presenter.onSuccessAndroid();
                }
            });
        }else{
            registra_local(anticiposDTO,sagasSql,token);
            presenter.onSuccessAndroid();
        }
    }

    @Override
    public boolean VerificarServicio(String token) {
        //region Verifica si el servcio esta disponible
        final boolean[] esta_disponible = {true};

        RestClient restClientS = ApiClient.getClient().create(RestClient.class);

        int servicio_intentos = 0;


        while (servicio_intentos<3) {
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,"application/json");
            callS.enqueue(new Callback<RespuestaServicioDisponibleDTO>() {
                @Override
                public void onResponse(Call<RespuestaServicioDisponibleDTO> call, Response<RespuestaServicioDisponibleDTO> response) {
                    RespuestaServicioDisponibleDTO data = response.body();
                    esta_disponible[0] = response.isSuccessful() && data.isExito();
                    if(response.code()>=300)
                        esta_disponible[0] = false;
                }

                @Override
                public void onFailure(Call<RespuestaServicioDisponibleDTO> call, Throwable t) {
                    esta_disponible[0] = false;
                }
            });

            if (esta_disponible[0]) {
                break;
            }else {
                servicio_intentos++;
            }
        }

        //endregion
        return esta_disponible[0];
    }

    @Override
    public void Corte(CorteDTO corteDTO, SAGASSql sagasSql, String token) {
        if(VerificarServicio(token)){

            RestClient restClient = ApiClient.getClient().create(RestClient.class);
            //corteDTO.setFecha(new Date().toString());

            Call<RespuestaCorteDto> call = restClient.
                    postCorte(corteDTO,token,"application/json");
            call.enqueue(new Callback<RespuestaCorteDto>() {
                @Override
                public void onResponse(Call<RespuestaCorteDto> call,
                                       Response<RespuestaCorteDto> response) {
                    RespuestaCorteDto data = response.body();
                    if(response.isSuccessful() && data.isExito())
                        presenter.onSuccess();
                    else
                    if(data!=null)
                        Log.e("Error anticipo",data.getMensaje());
                    else
                        Log.e("Error anticipo",response.code()+": "+
                                response.message());
                    if(response.code()>=300) {
                        registra_local(corteDTO,sagasSql,token);
                        presenter.onSuccessAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaCorteDto> call, Throwable t) {
                    presenter.onError(t.getMessage());
                    presenter.onSuccessAndroid();
                }
            });
        }else{
            registra_local(corteDTO,sagasSql,token);
            presenter.onSuccessAndroid();
        }
    }

    @Override
    public void getAnticipos(String token,int estacion,boolean esAnticipos,String fecha) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosBusquedaCortesDTO> call = restClient.getAnticipo_y_Corte(
                estacion,
                esAnticipos,
                fecha,
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosBusquedaCortesDTO>() {
            @Override
            public void onResponse(Call<DatosBusquedaCortesDTO> call, Response<DatosBusquedaCortesDTO> response) {
                DatosBusquedaCortesDTO data = response.body();
                if (response.isSuccessful()) {
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
                    JSONObject respuesta = null;
                    try {
                        respuesta = new JSONObject(response.errorBody().string());

                    } catch (JSONException e) {
                        e.printStackTrace();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    if(respuesta!=null){

                        try {
                            presenter.onError(respuesta.getString("Mensaje"));
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }

                    }else{
                        if(data!=null){
                            presenter.onError(data);
                        }else{
                            presenter.onError(response.message());
                        }
                    }

                }

            }

            @Override
            public void onFailure(Call<DatosBusquedaCortesDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getMessage());
            }
        });
    }

    /**
     * usuarios
     * Permite extraer el listado de usuario para determinar de quien se esta recibiendo el
     * anticipo,retornara un objeto de tipo {@link UsuariosCorteDTO} con el que se determina
     * si existe una lista de usuario o no
     * @param token String que reprecenta el tonque de usuario
     */
    @Override
    public void usuarios(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<UsuariosCorteDTO> call = restClient.getUsuarios(
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<UsuariosCorteDTO>() {
            @Override
            public void onResponse(Call<UsuariosCorteDTO> call,
                                   Response<UsuariosCorteDTO> response) {
                UsuariosCorteDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    if(data.isExito())
                        presenter.onSuccessList(data);
                    else
                        presenter.onError(data.getMensaje());
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
            public void onFailure(Call<UsuariosCorteDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getMessage());
            }
        });
    }

    @Override
    public void usuariosCortes(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<UsuariosCorteDTO> call = restClient.getUsuariosCorte(
                token,
                "application/json"
        );
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<UsuariosCorteDTO>() {
            @Override
            public void onResponse(Call<UsuariosCorteDTO> call,
                                   Response<UsuariosCorteDTO> response) {
                UsuariosCorteDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    if(data.isExito())
                        presenter.onSuccessList(data);
                    else
                        presenter.onError(data.getMensaje());
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
            public void onFailure(Call<UsuariosCorteDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getMessage());
            }
        });
    }

    private void registra_local(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token){
        if(sagasSql.GetAnticipoByClaveOperacion(anticiposDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InsertAnticipo(anticiposDTO);
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.Proceso.Anticipo);
        }
    }

    private void registra_local(CorteDTO corteDTO,SAGASSql sagasSql,String token){
        if(sagasSql.GetCorte(corteDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InserCorte(corteDTO);
            if(sagasSql.GetVentasCorte(corteDTO.getClaveOperacion()).getCount()==0) {
                sagasSql.InsertVentasCorte(corteDTO);
                Lisener lisener = new Lisener(sagasSql, token);
                lisener.CrearRunable(Lisener.Proceso.CorteDeCaja);
            }
        }
    }
}
