package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaRecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Lisener;
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

public class EnviarDatosInteractoriImpl implements EnviarDatosInteractor {
    private EnviarDatosPresenter enviarDatosPresenter;
    private boolean esta_disponible,registro_local,registra_lectura;

    public EnviarDatosInteractoriImpl(EnviarDatosPresenterImpl enviarDatosPresenter) {
        this.enviarDatosPresenter =  enviarDatosPresenter;
    }

    @Override
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
        //region Verifica si el servcio esta disponible

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
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,"application/json");
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
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,false);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la lectura inicial de camioneta

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
        registra_lectura = true;
        while(intentos_post<3) {
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalCamioneta(
                    lecturaCamionetaDTO,token,"application/json");
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        enviarDatosPresenter.onSuccessServicio();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("LecturaInicialCamioneta", "not found");
                                break;
                            case 500:
                                Log.w("LecturaInicialCamioneta", "server broken");
                                break;
                            default:
                                Log.w("LecturaInicialCamioneta", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                }
            });
            intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }
        }
        if(intentos_post==3){
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,false);
            registro_local = true;
        }
        if(registro_local ){
            enviarDatosPresenter.onSuccessAndroid();
        }
        //endregion
       /* sagasSql.InsertLecturaInicialCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaInicialCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessServicio();*/
    }

    @Override
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
        //region Verifica si el servcio esta disponible

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
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,"application/json");
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
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,true);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la lectura final de la camioneta

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
        registra_lectura = true;
        while(intentos_post<3) {
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalCamioneta(
                    lecturaCamionetaDTO,token,"application/json");
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        enviarDatosPresenter.onSuccessServicio();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("LecturaInicialCamioneta", "not found");
                                break;
                            case 500:
                                Log.w("LecturaInicialCamioneta", "server broken");
                                break;
                            default:
                                Log.w("LecturaInicialCamioneta", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                }
            });
            intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }
        }
        if(intentos_post==3){
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,true);
            registro_local = true;
        }
        if(registro_local ){
            enviarDatosPresenter.onSuccessAndroid();
        }
        //endregion
        /*sagasSql.InsertLecturaFinalCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaFinalCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessAndroid();*/
    }

    @Override
    public void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "RC"+s.format(new Date());
        recargaDTO.setClaveOperacion(clave_unica);
        //region Verifica si el servcio esta disponible

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
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,"application/json");
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
            registrar_local(sagasSql,recargaDTO,"C");
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la lectura final de la camioneta

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
        registra_lectura = true;
        while(intentos_post<3) {
            Call<RespuestaRecargaDTO> call = restClient.postRecarga(
                    recargaDTO,token,"application/json");
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        enviarDatosPresenter.onSuccessServicio();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("LecturaInicialCamioneta", "not found");
                                break;
                            case 500:
                                Log.w("LecturaInicialCamioneta", "server broken");
                                break;
                            default:
                                Log.w("LecturaInicialCamioneta", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                }
            });
            intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }
        }
        if(intentos_post==3){
            registrar_local(sagasSql,recargaDTO,"C");
            registro_local = true;
        }
        if(registro_local ){
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.RecargaCamioneta);
            enviarDatosPresenter.onSuccessAndroid();
        }
        //endregion
    }

    private void registrar_local(SAGASSql sagasSql, LecturaCamionetaDTO lecturaCamionetaDTO,
                                 String clave_unica, boolean es_final) {
        if(!es_final){
            if(sagasSql.GetLecturaInicialCamionetaByClaveOperacion(clave_unica).getCount()==0){
                sagasSql.InsertLecturaInicialCamioneta(lecturaCamionetaDTO);
                if(sagasSql.GetCilindrosLecturaFinalCamioneta(clave_unica).getCount()==0){
                    sagasSql.InsertCilindrosLecturaInicialCamioneta(lecturaCamionetaDTO);
                }
            }
        }else{
            if(sagasSql.GetLecturaFinalCamionetaByClaveOperacion(clave_unica).getCount()==0){
                sagasSql.InsertLecturaFinalCamioneta(lecturaCamionetaDTO);
                if(sagasSql.GetCilindrosLecturaFinalCamioneta(clave_unica).getCount()==0){
                    sagasSql.InsertCilindrosLecturaFinalCamioneta(lecturaCamionetaDTO);
                }
            }
        }
    }
    private void registrar_local(SAGASSql sagasSql,RecargaDTO recargaDTO,String tipo ){
        if(tipo.equals("C")) {
            if (sagasSql.GetRecargaByClaveOperacion(recargaDTO.getClaveOperacion()).getCount() == 0) {
                sagasSql.InsertRecarga(recargaDTO,SAGASSql.TIPO_RECARGA_CAMIONETA);
                if (sagasSql.GetCilindrosRecarga(recargaDTO.getClaveOperacion()).getCount()==0){
                    sagasSql.InsertarCilindrosRecarga(recargaDTO);
                }
            }
        }else{
            if (sagasSql.GetRecargaByClaveOperacion(recargaDTO.getClaveOperacion()).getCount() == 0) {
                sagasSql.InsertRecarga(recargaDTO,(tipo.equals("P"))?
                        SAGASSql.TIPO_RECARGA_PIPA:SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION);
                if (sagasSql.GetImagenesRecarga(recargaDTO.getClaveOperacion()).getCount()==0){
                    sagasSql.InsertarImagesRecarga(recargaDTO);
                }
            }
        }
    }
}
