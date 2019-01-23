package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenReferenciaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
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

/**
 * Created by neotecknewts on 13/08/18.
 */

public class RegistrarPapeletaInteractorImpl implements RegistrarPapeletaInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "RegistrarPapInteractor";
    RegistrarPapeletaPresenter registrarPapeletaPresenter;

    //constructor de la clase y se inicializa el presenter
    public RegistrarPapeletaInteractorImpl(RegistrarPapeletaPresenter registrarPapeletaPresenter){
        this.registrarPapeletaPresenter = registrarPapeletaPresenter;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas las ordenes de compra
    @Override
    public void getOrdenesCompra(int IdEmpresa, final boolean EsGas, boolean EsActivoVenta, boolean EsTransporteGas , String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaOrdenesCompraDTO> call = restClient.getOrdenesCompra(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas,token);

        call.enqueue(new Callback<RespuestaOrdenesCompraDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenesCompraDTO> call, Response<RespuestaOrdenesCompraDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenesCompraDTO data = response.body();
                    if(data.isExito()) {
                        Log.w(TAG, "Success");
                        if (EsGas) {
                            registrarPapeletaPresenter.onSuccessGetOrdenesCompraExpedidor(data);
                        } else {
                            registrarPapeletaPresenter.onSuccessGetOrdenesCompraPorteador(data);
                        }
                    }else{
                        registrarPapeletaPresenter.onError(data.getMensaje());
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            registrarPapeletaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            registrarPapeletaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            registrarPapeletaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                registrarPapeletaPresenter.onError();
            }
        });
    }

    @Override
    public void getMedidores(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    Log.w(TAG,"Success");
                    registrarPapeletaPresenter.onSuccessGetMedidores(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            registrarPapeletaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            registrarPapeletaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            registrarPapeletaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                registrarPapeletaPresenter.onError();
            }
        });
    }

    /**
     * getOrderReferencia
     * Permite retornar la orden de compra que tien de referencia (sea expedidor o porteador),
     * toma como parametros el tiken de usuario , el id de la orden de compra y si es del
     * expedidor, retornara un objeto  {@link RespuestaOrdenReferenciaDTO} desde el api
     * @param token {@link String} que reprecenta el token de usuario
     * @param idOrdenCompra {@link Integer} que reprecenta el id de la oprden de compra
     * @param esExpedidor {@link Boolean} que reprecenta si es de expedidor o porteador la orden
     *                                   que se envia de parametro
     */
    @Override
    public void getOrderReferencia(String token, int idOrdenCompra,boolean esExpedidor) {
        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaOrdenReferenciaDTO> call = restClient.getReferenciaOrden(
                idOrdenCompra,
                token,
                "application/json"
        );

        call.enqueue(new Callback<RespuestaOrdenReferenciaDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenReferenciaDTO> call, Response<RespuestaOrdenReferenciaDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenReferenciaDTO data = response.body();
                    if(data.isExito()) {
                        Log.w("Orden de referencia ", "Success");
                        registrarPapeletaPresenter.onSuccessReferencia(data,esExpedidor);
                    }else{
                        registrarPapeletaPresenter.onError(data.getMensaje());
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            //registrarPapeletaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            //registrarPapeletaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            //registrarPapeletaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenReferenciaDTO> call, Throwable t) {
                Log.e("error", t.toString());
                //registrarPapeletaPresenter.onError();
            }
        });
    }
}
