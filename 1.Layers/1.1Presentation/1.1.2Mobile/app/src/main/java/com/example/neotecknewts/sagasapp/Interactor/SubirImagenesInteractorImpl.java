package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;
import com.example.neotecknewts.sagasapp.R;
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
    private boolean esta_disponible;
    private boolean registra_papeleta;
    private boolean registro_local;

    //constructor de la clase y se inicializa el presenter
    public SubirImagenesInteractorImpl(SubirImagenesPresenter subirImagenesPresenter){
        this.subirImagenesPresenter = subirImagenesPresenter;
    }

    //metodos para llamada web service
    @Override
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL, final Context context) {
        registro_local =false;
        Log.w("Entra", String.valueOf(precargaPapeletaDTO.getImagenes().size()));

        @SuppressLint("SimpleDateFormat") SimpleDateFormat s = new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "O"+s.format(new Date());
        Log.w("Genero_clave",clave_unica);
        Log.w("Consulta","Consulto si el servicio esta disponible");
        //regionVerifica si el servcio esta disponible

        Gson gsons = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofits =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gsons))
                .build();
        RestClient restClientS = retrofits.create(RestClient.class);

        int servicio_intentos = 0;
        esta_disponible= true;

        while (servicio_intentos<3) {
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio("","application/json");
            callS.enqueue(new Callback<RespuestaServicioDisponibleDTO>() {
                @Override
                public void onResponse(Call<RespuestaServicioDisponibleDTO> call, Response<RespuestaServicioDisponibleDTO> response) {
                    RespuestaServicioDisponibleDTO data = response.body();
                    esta_disponible = response.isSuccessful() && data.isExito();
                }

                @Override
                public void onFailure(Call<RespuestaServicioDisponibleDTO> call, Throwable t) {
                    esta_disponible = false;
                }
            });
            if (esta_disponible) {
                break;
            }else {
                servicio_intentos++;
            }
        }

        if (servicio_intentos == 3) {
           registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
            registro_local = true;
        }

        //endregion

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
        int intentos_post = 0;
        registra_papeleta = true;
        while(intentos_post<3) {
            Call<RespuestaPapeletaDTO> call = restClient.postPapeleta(precargaPapeletaDTO, token, "application/json");

            Log.w(TAG, retrofit.baseUrl().toString());
            Log.w("Numero ", precargaPapeletaDTO.toString());

            call.enqueue(new Callback<RespuestaPapeletaDTO>() {
                @Override
                public void onResponse(Call<RespuestaPapeletaDTO> call, Response<RespuestaPapeletaDTO> response) {

                    if (response.isSuccessful()) {
                        RespuestaPapeletaDTO data = response.body();
                        Log.w(TAG, "Success");
                        registra_papeleta = true;
                        //subirImagenesPresenter.onSuccessRegistrarPapeleta();
                        subirImagenesPresenter.onSuccessRegistroPapeleta();
                    } else {
                        RespuestaPapeletaDTO data = response.body();
                        //Log.w("Respuesta",data.getMensaje());
                        switch (response.code()) {
                            case 404:
                                Log.w(TAG, "not found");
                                //subirImagenesPresenter.onError();
                                break;
                            case 500:
                                Log.w(TAG, "server broken");
                                //subirImagenesPresenter.onError();
                                break;
                            default:
                                Log.w(TAG, "" + response.code());
                                Log.w(" Error", response.message() + " " + response.raw().toString());

                                break;
                        }
                        //subirImagenesPresenter.onError();
                        registra_papeleta = false;
                    }

                }

                @Override
                public void onFailure(Call<RespuestaPapeletaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_papeleta = false;
                    //subirImagenesPresenter.onError();
                }
            });
            intentos_post++;
            if(registra_papeleta){
                break;
            }else{
                intentos_post++;
            }
        }
        if(intentos_post==3){
            registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }
    }


    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO) {

    }

    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO) {

    }
    //region Metodos de clase privados
    /**
     * registrar_local
     * Permite realizar el registro de los datos de la papeleta en local , primero
     * verificara si los registros ya existen , en caso de no existir los registrara
     * en local
     * @param papeletaSQL Objeto {@link PapeletaSQL} que permite la conexion a la base de datos local
     * @param precargaPapeletaDTO Objeto {@link PrecargaPapeletaDTO} con los datos a guardar de la
     *                            papeleta
     * @param clave_unica String con la calve unica que se registrara de la papeleta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx >
     */
    private void registrar_local(PapeletaSQL papeletaSQL,PrecargaPapeletaDTO precargaPapeletaDTO,String clave_unica){
        if (papeletaSQL.GetRecordByCalveUnica(clave_unica).getCount() == 0) {
            papeletaSQL.Insert(precargaPapeletaDTO, clave_unica);
            if (papeletaSQL.GetRecordsByCalveUnica(clave_unica).getCount() == 0) {
                papeletaSQL.InsertImagenes(precargaPapeletaDTO.getImagenesURI(), precargaPapeletaDTO.getImagenes(), clave_unica);
            }
        }
    }
    //endregion
}
