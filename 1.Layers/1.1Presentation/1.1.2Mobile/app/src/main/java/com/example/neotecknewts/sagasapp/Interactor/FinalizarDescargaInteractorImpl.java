package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.FinalizarDescargaPresenter;
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

public class FinalizarDescargaInteractorImpl implements FinalizarDescargaInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "FinalizarDescInteractor";
    FinalizarDescargaPresenter finalizarDescargaPresenter;

    //constructor de la clase y se inicializa el presenter
    public FinalizarDescargaInteractorImpl(FinalizarDescargaPresenter finalizarDescargaPresenter){
        this.finalizarDescargaPresenter = finalizarDescargaPresenter;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas las ordenes de compra
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaOrdenesCompraDTO> call = restClient.getOrdenesCompra(IdEmpresa,true,true,false,token);
        Log.w(TAG,ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaOrdenesCompraDTO>() {
            @Override
            public void onResponse(Call<RespuestaOrdenesCompraDTO> call, Response<RespuestaOrdenesCompraDTO> response) {
                if (response.isSuccessful()) {
                    RespuestaOrdenesCompraDTO data = response.body();
                    if(data.isExito()) {
                        Log.w(TAG, "Success");
                        finalizarDescargaPresenter.onSuccessGetOrdenesCompra(data);
                    }else{
                        finalizarDescargaPresenter.onError(data.getMensaje());
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            finalizarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            finalizarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            finalizarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                finalizarDescargaPresenter.onError();
            }
        });
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas los medidores
    @Override
    public void getMedidores(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);
        Log.w(TAG,ApiClient.BASE_URL);

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    if (data.size()>0) {
                        Log.w(TAG, "Success");
                        finalizarDescargaPresenter.onSuccessGetMedidores(data);
                    }else{
                        finalizarDescargaPresenter.onError("No se pudieron obtener los medidores");
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            finalizarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            finalizarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            finalizarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                finalizarDescargaPresenter.onError();
            }
        });
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas los almacenes
    @Override
    public void getAlmacenes(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<AlmacenDTO>> call = restClient.getAlmacenes(token);
        Log.w(TAG,ApiClient.BASE_URL);

        call.enqueue(new Callback<List<AlmacenDTO>>() {
            @Override
            public void onResponse(Call<List<AlmacenDTO>> call, Response<List<AlmacenDTO>> response) {
                if (response.isSuccessful()) {
                    List<AlmacenDTO> data = response.body();
                    Log.w(TAG,"Success");
                    if (data.size()>0) {
                        finalizarDescargaPresenter.onSuccessGetAlmacenes(data);
                    }else{
                        finalizarDescargaPresenter.onError("No se pudieron obtener las ordenes de compra");
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            finalizarDescargaPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            finalizarDescargaPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            finalizarDescargaPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<AlmacenDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                finalizarDescargaPresenter.onError();
            }
        });
    }
}
