package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.text.SimpleDateFormat;
import java.util.Date;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class SubirImagenesInteractorImpl implements SubirImagenesInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "SubirImagInteractor";
    SubirImagenesPresenter subirImagenesPresenter;

    //constructor de la clase y se inicializa el presenter
    public SubirImagenesInteractorImpl(SubirImagenesPresenter subirImagenesPresenter){
        this.subirImagenesPresenter = subirImagenesPresenter;
    }

    //metodos para llamada web service
    @Override
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO,String token) {
        Log.w("Entra", String.valueOf(precargaPapeletaDTO.getImagenes().size()));
        SimpleDateFormat s = new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "O"+s.format(new Date());
        precargaPapeletaDTO.setClaveOperacion(clave_unica);
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaPapeletaDTO> call = restClient.postPapeleta(precargaPapeletaDTO,token,"application/json");

        Log.w(TAG,retrofit.baseUrl().toString());
        Log.w("Numerp ",precargaPapeletaDTO.toString());

                call.enqueue(new Callback<RespuestaPapeletaDTO>() {
                @Override
                public void onResponse(Call<RespuestaPapeletaDTO> call, Response<RespuestaPapeletaDTO> response) {

                    if (response.isSuccessful()) {
                        RespuestaPapeletaDTO data = response.body();
                        Log.w(TAG, "Success");
                        subirImagenesPresenter.onSuccessRegistrarPapeleta();
                    } else {
                        RespuestaPapeletaDTO data = response.body();
                        //Log.w("Respuesta",data.getMensaje());
                        switch (response.code()) {
                            case 404:
                                Log.w(TAG, "not found");
                                subirImagenesPresenter.onError();
                                break;
                            case 500:
                                Log.w(TAG, "server broken");
                                subirImagenesPresenter.onError();
                                break;
                            default:
                                Log.w(TAG, "" + response.code());
                                Log.w(" Error", response.message() + " " + response.raw().toString());

                                break;
                        }
                    }

                }

                @Override
                public void onFailure(Call<RespuestaPapeletaDTO> call, Throwable t) {
                    Log.e("error", t.toString());

                    subirImagenesPresenter.onError();
                }
            });
    }


    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO) {

    }

    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO) {

    }
}
