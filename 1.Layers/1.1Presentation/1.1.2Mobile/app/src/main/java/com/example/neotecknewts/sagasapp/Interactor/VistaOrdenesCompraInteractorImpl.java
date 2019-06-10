package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Presenter.VistaOrdenesCompraPresenter;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by neotecknewts on 10/08/18.
 */

public class VistaOrdenesCompraInteractorImpl implements VistaOrdenesCompraInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "VistaOrdenesInteractor";
    VistaOrdenesCompraPresenter vistaOrdenesCompraPresenter;

    //constructor de la clase y se inicializa el presenter
    public VistaOrdenesCompraInteractorImpl(VistaOrdenesCompraPresenter vistaOrdenesCompraPresenter){
        this.vistaOrdenesCompraPresenter = vistaOrdenesCompraPresenter;
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
                    Log.w(TAG,"Success");
                    if(data.isExito() && data.getOrdenesCompra().size()>0) {
                        vistaOrdenesCompraPresenter.onSuccessGetOrdenesCompra(data);
                    }else{
                        vistaOrdenesCompraPresenter.onError(data.getMensaje());
                    }
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            vistaOrdenesCompraPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            vistaOrdenesCompraPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            vistaOrdenesCompraPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<RespuestaOrdenesCompraDTO> call, Throwable t) {
                Log.e("error", t.toString());
                vistaOrdenesCompraPresenter.onError();
            }
        });
    }

}
