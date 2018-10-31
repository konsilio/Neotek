package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaAnticipoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaCorteDto;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Lisener;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

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
            Gson gsons = new GsonBuilder()
                    .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                    .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                    .create();
            Retrofit retrofits =  new Retrofit.Builder()
                    .baseUrl(Constantes.BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create(gsons))
                    .build();
            RestClient restClient = retrofits.create(RestClient.class);


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
        Gson gsons = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofits =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL+"/db/")
                .addConverterFactory(GsonConverterFactory.create(gsons))
                .build();
        RestClient restClientS = retrofits.create(RestClient.class);

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
            Gson gsons = new GsonBuilder()
                    .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                    .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                    .create();
            Retrofit retrofits =  new Retrofit.Builder()
                    .baseUrl(Constantes.BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create(gsons))
                    .build();
            RestClient restClient = retrofits.create(RestClient.class);


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

    private void registra_local(AnticiposDTO anticiposDTO,SAGASSql sagasSql,String token){
        if(sagasSql.GetAnticipoByClaveOperacion(anticiposDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InsertAnticipo(anticiposDTO);
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.Anticipo);
        }
    }

    private void registra_local(CorteDTO corteDTO,SAGASSql sagasSql,String token){
        if(sagasSql.GetCorte(corteDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InserCorte(corteDTO);
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.CorteDeCaja);
        }
    }
}
