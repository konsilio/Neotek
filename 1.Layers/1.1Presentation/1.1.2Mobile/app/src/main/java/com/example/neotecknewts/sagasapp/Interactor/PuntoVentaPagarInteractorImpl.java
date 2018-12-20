package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Lisener;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class PuntoVentaPagarInteractorImpl implements PuntoVentaPagarInteractor {
    PuntoVentaPagarPresenter presenter;
    private boolean registro_local = false;
    public PuntoVentaPagarInteractorImpl(PuntoVentaPagarPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
                      boolean esPipa, SAGASSql sagasSql) {

        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
        ventaDTO.setFecha(f.format(new Date(ventaDTO.getFecha())));
        Call<RespuestaPuntoVenta> call = restClient.pagar(
                ventaDTO,
                /*esCamioneta,
                esEstacion,
                esPipa,*/
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<RespuestaPuntoVenta>() {
            @Override
            public void onResponse(Call<RespuestaPuntoVenta> call, Response<RespuestaPuntoVenta> response) {
                RespuestaPuntoVenta data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
                    registro_local = false;
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
                    registro_local = true;
                    if(data!= null){
                        presenter.onError(data);
                    }else {
                        presenter.onError(mensaje);
                    }

                }
                if(response.code()>=300) {
                    local(sagasSql, ventaDTO,esCamioneta,esEstacion,esPipa);
                    presenter.onSuccessAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.VENTA);
                }
            }

            @Override
            public void onFailure(Call<RespuestaPuntoVenta> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                registro_local = true;
                presenter.onError("Se ha generado un error: "+t.getMessage());
                local(sagasSql, ventaDTO,esCamioneta,esEstacion,esPipa);
                presenter.onSuccessAndroid();
                Lisener lisener = new Lisener(sagasSql,token);
                lisener.CrearRunable(Lisener.VENTA);

            }

        });
        /*if(registro_local) {
            local(sagasSql, ventaDTO,esCamioneta,esEstacion,esPipa);
            presenter.onSuccessAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.VENTA);
        }*/
    }

    @Override
    public void puntoVentaAsignado(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<PuntoVentaAsignadoDTO> call = restClient.getPuntoVentaAsigando(
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<PuntoVentaAsignadoDTO>() {
            @Override
            public void onResponse(Call<PuntoVentaAsignadoDTO> call, Response<PuntoVentaAsignadoDTO> response) {
                PuntoVentaAsignadoDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccessPuntoVentaAsignado(data);
                    registro_local = false;
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
                    presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
                }

            }

            @Override
            public void onFailure(Call<PuntoVentaAsignadoDTO> call, Throwable t) {
                presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
            }

        });
    }

    @Override
    public void verificarVentaExtraforanea(int idCliente, String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaVentaExtraforaneaDTO> call = restClient.getTieneVentaExtraforanea(
                idCliente,
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<RespuestaVentaExtraforaneaDTO>() {
            @Override
            public void onResponse(Call<RespuestaVentaExtraforaneaDTO> call,
                                   Response<RespuestaVentaExtraforaneaDTO> response) {

                RespuestaVentaExtraforaneaDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccessVentaExtraforanea(data);
                    registro_local = false;
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
                        try {
                            Log.w("Error body",respuesta.toString());
                            presenter.onError(respuesta.getString("Mensaje"));

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }else{
                        presenter.onError(response.message());
                    }

                }

            }

            @Override
            public void onFailure(Call<RespuestaVentaExtraforaneaDTO> call, Throwable t) {
                presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
            }

        });
    }

    private void local(SAGASSql sagasSql, VentaDTO ventaDTO, boolean esCamioneta,
                       boolean esEstacion, boolean esPipa) {
        try {
            if (sagasSql.GetVenta(ventaDTO.getFolioVenta()).getCount() == 0) {
                sagasSql.InsertarVenta(ventaDTO, esCamioneta, esEstacion, esPipa);
                if(sagasSql.GetVentaConcepto(ventaDTO.getFolioVenta()).getCount()==0) {
                    sagasSql.InsertarConcepto(ventaDTO);
                }
            }
        }catch (Exception ex){
            ex.printStackTrace();
            Log.e("Mensaje excepcion",ex.getMessage());
        }

    }
}
