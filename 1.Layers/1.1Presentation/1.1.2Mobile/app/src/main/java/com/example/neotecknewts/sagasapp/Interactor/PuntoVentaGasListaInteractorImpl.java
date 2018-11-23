package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class PuntoVentaGasListaInteractorImpl implements PuntoVentaGasListaInteractor {
    PuntoVentaGasListaPresenter presenter;
    public PuntoVentaGasListaInteractorImpl(PuntoVentaGasListaPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getListaCamionetaCilindros(String token, boolean esGasLP,
                                           boolean esCilindroConGas, boolean esCilindro) {
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
        /*Call<DatosPuntoVentaDTO> call = restClient.getListaExistencias(
                esGasLP,
                esCilindroConGas,
                esCilindro,
                token,
                "application/json"
        );*/
        Call<List<ExistenciasDTO>> call = restClient.getListaExistencias(
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<ExistenciasDTO>>() {
            @Override
            public void onResponse(Call<List<ExistenciasDTO>> call, Response<List<ExistenciasDTO>> response) {
                List<ExistenciasDTO> data = response.body();
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
                        presenter.onError(response.message());
                    }
                }

            }

            @Override
            public void onFailure(Call<List<ExistenciasDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });

    }

    @Override
    public void getListEstacionGas(String token, boolean esGasLP, boolean esCilindroGas, boolean esCilindro) {
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
        /*Call<DatosPuntoVentaDTO> call = restClient.getListaExistencias(
                esGasLP,
                esCilindroGas,
                esCilindro,
                token,
                "application/json"
        );*/
        Call<List<ExistenciasDTO>> call = restClient.getListaExistencias(
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<ExistenciasDTO>>() {
            @Override
            public void onResponse(Call<List<ExistenciasDTO>> call, Response<List<ExistenciasDTO>> response) {
                List<ExistenciasDTO> data = response.body();
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
                        presenter.onError(response.message());
                    }
                }

            }

            @Override
            public void onFailure(Call<List<ExistenciasDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getMessage());
            }
        });
    }

    @Override
    public void getPrecioVenta(String token) {
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
        Call<PrecioVentaDTO> call = restClient.getPrecioVenta(
                token,
                "application/json"
        );
        Log.w("Url base",retrofit.baseUrl().toString());

        call.enqueue(new Callback<PrecioVentaDTO>() {
            @Override
            public void onResponse(Call<PrecioVentaDTO> call, Response<PrecioVentaDTO> response) {
                PrecioVentaDTO data = response.body();
                if (response.isSuccessful()) {

                    Log.w("Estatus","Success");
                    presenter.onSuccessPrecioVenta(data);
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
                        presenter.onError(response.message());
                    }
                }

            }

            @Override
            public void onFailure(Call<PrecioVentaDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError("Se ha generado un error: "+t.getCause());
            }
        });
    }
}
