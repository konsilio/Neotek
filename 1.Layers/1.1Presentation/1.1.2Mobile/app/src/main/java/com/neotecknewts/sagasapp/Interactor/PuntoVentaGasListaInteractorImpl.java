package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PuntoVentaGasListaInteractorImpl implements PuntoVentaGasListaInteractor {
    PuntoVentaGasListaPresenter presenter;
    public PuntoVentaGasListaInteractorImpl(PuntoVentaGasListaPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getListaCamionetaCilindros(String token, boolean esGasLP,
                                           boolean esCilindroConGas, boolean esCilindro) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<ExistenciasDTO>> call = restClient.getListaExistencias(
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

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
    public void getListEstacionGas(String token, boolean esGasLP, boolean esCilindroGas, boolean
            esCilindro) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<ExistenciasDTO>> call = restClient.getListaExistencias(
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

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


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<PrecioVentaDTO> call = restClient.getPrecioVenta(
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

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
                    mensaje = "Se ha generado un error,al solicitar los precio";
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

    @Override
    public void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,
                                      String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<ExistenciasDTO>> call = restClient.getListaExistencias(
                esGasLP,
                esCilindroGas,
                esCilindro,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<List<ExistenciasDTO>>() {
            @Override
            public void onResponse(Call<List<ExistenciasDTO>> call, Response<List<ExistenciasDTO>> response) {
                List<ExistenciasDTO> data = response.body();
                if (response.isSuccessful()) {

                    Log.w("Estatus","Success");
                    presenter.onSuccessDatosCamioneta(data);
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
}
