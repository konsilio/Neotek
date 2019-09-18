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
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
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

    /**
     * RegistrarLecturaInicialCamioneta
     * Permite realizar en envio y registro de la lectura inicial de la camioneta en el api rest
     * en caso de no registrarce se registrar en la base de datos local
     * @param sagasSql Objeto {@link SAGASSql} que contiene la base de datos local
     * @param token string que reprecenta el token del usuario
     * @param lecturaCamionetaDTO Objeto de tipo {@link LecturaCamionetaDTO} con los datos
     *                            a enviar al api de la lectura de camioneta
     */
    @Override
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaCamionetaDTO.setFechaAplicacion(sf.format(new Date()));
        //region Verifica si el servcio esta disponible


        RestClient restClientS = ApiClient.getClient().create(RestClient.class);

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



        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        int intentos_post = 0;
        registra_lectura = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialCamioneta(
                    lecturaCamionetaDTO,token,"application/json");
            Log.w("Url camioneta", ApiClient.BASE_URL);
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
                    if(response.code()>=300){
                        registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,false);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Proceso.LecturaInicialCamioneta);
                        enviarDatosPresenter.onSuccessAndroid();

                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,false);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Proceso.LecturaInicialCamioneta);
                    enviarDatosPresenter.onSuccessAndroid();
                }
            });
            /*intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,false);
            registro_local = true;
        }
        if(registro_local ){
            enviarDatosPresenter.onSuccessAndroid();
        }*/
        //endregion
       /* sagasSql.InsertLecturaInicialCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaInicialCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessServicio();*/
    }

    /**
     * RegistrarLecturaFinalCamioneta
     * Permite realizar el registro de la lectura final de la recarga de camioneta a la api,
     *  en caso de no registrarce se enviara a local
     * @param sagasSql Objeto {@link SAGASSql} que contiene la base de datos local
     * @param token string que reprecenta el token del usuario
     * @param lecturaCamionetaDTO Objeto de tipo {@link LecturaCamionetaDTO} con los datos
     *                            a enviar al api de la lectura de camioneta
     */
    @Override
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaCamionetaDTO.setFechaAplicacion(sf.format(new Date()));
        //region Verifica si el servcio esta disponible


        RestClient restClientS = ApiClient.getClient().create(RestClient.class);

        int servicio_intentos = 0;
        esta_disponible= true;

        while (servicio_intentos<3) {
            Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,
                    "application/json");
            callS.enqueue(new Callback<RespuestaServicioDisponibleDTO>() {
                @Override
                public void onResponse(Call<RespuestaServicioDisponibleDTO> call,
                                       Response<RespuestaServicioDisponibleDTO> response) {
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



        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        int intentos_post = 0;
        registra_lectura = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalCamioneta(
                    lecturaCamionetaDTO,token,"application/json");
            Log.w("Url camioneta", ApiClient.BASE_URL);
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
                    if(response.code()>=300){
                        registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,true);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Proceso.LecturaFinalCamioneta);
                        enviarDatosPresenter.onSuccessAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,true);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Proceso.LecturaFinalCamioneta);
                    enviarDatosPresenter.onSuccessAndroid();
                }
            });
            /*intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaCamionetaDTO,clave_unica,true);
            registro_local = true;
        }
        if(registro_local ){
            enviarDatosPresenter.onSuccessAndroid();
        }*/
        //endregion
        /*sagasSql.InsertLecturaFinalCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaFinalCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessAndroid();*/
    }

    /**
     * RegistrarRecargaCamioneta
     * Permite realizar el registro de la recarga de camioneta en el api, en caso de que no se registre
     * se registrara en la base de datos local
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga de la
     *                   camioneta
     * @param token string que reprecenta el token del usuario
     * @param sagasSql Objeto {@link SAGASSql} que contiene la base de datos local
     */
    @Override
    public void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "RC"+s.format(new Date());
        recargaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        recargaDTO.setFechaApliacacion(sf.format(new Date()));
        //region Verifica si el servcio esta disponible


        RestClient restClientS = ApiClient.getClient().create(RestClient.class);

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



        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        int intentos_post = 0;
        registra_lectura = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaRecargaDTO> call = restClient.postRecarga(
                    recargaDTO,token,"application/json");
            Log.w("Url camioneta", ApiClient.BASE_URL);
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("Recarga camioneta", "Success");
                        enviarDatosPresenter.onSuccessServicio();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("Recarga camioneta", "not found");
                                break;
                            case 500:
                                Log.w("Recarga camioneta", "server broken");
                                break;
                            default:
                                Log.w("Recarga camioneta", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }
                    if(response.code()>=300){
                        registrar_local(sagasSql,recargaDTO,"C");
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Proceso.RecargaCamioneta);
                        enviarDatosPresenter.onSuccessAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,recargaDTO,"C");
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Proceso.RecargaCamioneta);
                    enviarDatosPresenter.onSuccessAndroid();
                }
            });
            /*intentos_post++;
            if(registra_lectura){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,recargaDTO,"C");
            registro_local = true;
        }
        if(registro_local ){
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.Proceso.RecargaCamioneta);
            enviarDatosPresenter.onSuccessAndroid();
        }*/
        //endregion
    }

    /**
     * registrar_local
     * Permite realizar el registro de la lectura de camioneta
     * @param sagasSql Objeto {@link SAGASSql} que contiene la base de datos local
     * @param lecturaCamionetaDTO Objeto de tipo {@link LecturaCamionetaDTO} con los datos
     *                            a enviar al api de la lectura de camioneta
     * @param clave_unica string que reprecenta la clave unica del registro
     * @param es_final boolean reprecenta si el registro es una lectura final
     */
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

    /**
     * registrar_local
     * Permite realizar el registro de la recarga de la camioneta
     * @param sagasSql Objeto {@link SAGASSql} que contiene la base de datos local
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga de la
     *                   camioneta
     * @param tipo Reprecenta el tipo de registro que se hara, en este caso la cmaioneta
     */
    private void registrar_local(SAGASSql sagasSql,RecargaDTO recargaDTO,String tipo ){
        if(tipo.equals("C")) {
            if (sagasSql.GetRecargaByClaveOperacion(recargaDTO.getClaveOperacion()).getCount() == 0) {
                sagasSql.InsertRecarga(recargaDTO,SAGASSql.TIPO_RECARGA_CAMIONETA,true);
                if (sagasSql.GetCilindrosRecarga(recargaDTO.getClaveOperacion()).getCount()==0){
                    sagasSql.InsertarCilindrosRecarga(recargaDTO);
                }
            }
        }/*else{
            if (sagasSql.GetRecargaByClaveOperacion(recargaDTO.getClaveOperacion()).getCount() == 0) {
                sagasSql.InsertRecarga(recargaDTO,(tipo.equals("P"))?
                        SAGASSql.TIPO_RECARGA_PIPA:SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION);
                if (sagasSql.GetImagenesRecarga(recargaDTO.getClaveOperacion()).getCount()==0){
                    sagasSql.InsertarImagesRecarga(recargaDTO);
                }
            }
        }*/
    }
}
