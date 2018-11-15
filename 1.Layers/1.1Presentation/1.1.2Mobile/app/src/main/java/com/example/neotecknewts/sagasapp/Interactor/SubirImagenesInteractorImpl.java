package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaFinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaRecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;
import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Lisener;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.text.SimpleDateFormat;
import java.util.Calendar;
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
    private static final String TAG = "SubirImagInteractor";
    private SubirImagenesPresenter subirImagenesPresenter;
    private boolean esta_disponible;
    private boolean registra_papeleta;
    private boolean registra_descarga;
    private boolean registro_local;
    private boolean registra_reacrga;

    //constructor de la clase y se inicializa el presenter
    public SubirImagenesInteractorImpl(SubirImagenesPresenter subirImagenesPresenter){
        this.subirImagenesPresenter = subirImagenesPresenter;
    }

    /***
     * registrarPapeleta
     * Permite realizar el envio de  los datos de la papeleta al api
     * tomara los datos del dto de tipo {@link PrecargaPapeletaDTO}
     * para su registro , retornara una respuesta en caso de ser correcra
     * en caso cobtrario se guarda en local y se activa el objeto {@link Lisener}
     * para su envio hasta que se tenga conexion a  este servicio.
     * @param precargaPapeletaDTO Objeto de tipo {@link PrecargaPapeletaDTO} con los datos de la
     *                            papeleta
     * @param token {@link String} que reprecenta el token de usuario
     * @param papeletaSQL Objeto {@link PapeletaSQL} que permite la conexion a la base de datos
     *                    local
     */
    @Override
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO,
                                  String token, PapeletaSQL papeletaSQL) {
        registro_local =false;
        Log.w("Entra", String.valueOf(precargaPapeletaDTO.getImagenes().size()));

        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "O"+s.format(new Date());
        Log.w("Genero_clave",clave_unica);
        Log.w("Consulta","Consulto si el servicio esta disponible");
        precargaPapeletaDTO.setClaveOperacion(clave_unica);
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
           registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
            registro_local = true;
        }

        //endregion


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
        /*while(intentos_post<3) {*/
            Call<RespuestaPapeletaDTO> call = restClient.postPapeleta(precargaPapeletaDTO,
                    token, "application/json");

            Log.w(TAG, retrofit.baseUrl().toString());
            Log.w("Numero ", precargaPapeletaDTO.toString());

            call.enqueue(new Callback<RespuestaPapeletaDTO>() {
                @Override
                public void onResponse(Call<RespuestaPapeletaDTO> call,
                                       Response<RespuestaPapeletaDTO> response) {

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
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());

                                break;
                        }
                        //subirImagenesPresenter.onError();
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                        registra_papeleta = false;

                    }
                    if(response.code()>=300){
                        registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(papeletaSQL,token);
                        lisener.CrearRunable(Lisener.Papeleta);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaPapeletaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_papeleta = false;
                    registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(papeletaSQL,token);
                    lisener.CrearRunable(Lisener.Papeleta);
                    //subirImagenesPresenter.onError();
                }
            });
            /*intentos_post++;
            if(registra_papeleta){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(papeletaSQL,precargaPapeletaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(papeletaSQL,token);
            lisener.CrearRunable(Lisener.Papeleta);
        }*/
    }

    /**
     * registrarIniciarDescarga
     * Hace el llamado al servicio web para realizar el registro de la descarga , recive como
     * parametros  un objeto de tipo {@link IniciarDescargaDTO} que contiene todos los datos de
     * la descrga, un {@link String} que reprecenta el token de seguridad del usuario y un
     * objeto {@link IniciarDescargaSQL} para el almacienamiento en local en caso de error
     * @param iniciarDescargaDTO Objeto {@link IniciarDescargaDTO} con los datos de la descarga
     * @param token {@link String} que reprecenta el token de seguridad
     * @param iniciarDescargaSQL Objeto {@link IniciarDescargaSQL} que permite el uso de base de
     *                           datos en local de la descarga
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO,
                                         String token, IniciarDescargaSQL iniciarDescargaSQL) {

        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "D"+s.format(new Date());
        Log.w("Genero_clave",clave_unica);
        Log.w("Consulta","Consulto si el servicio esta disponible");
        iniciarDescargaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
        String formato_fecha_operacion = f.format(new Date());
        iniciarDescargaDTO.setFechaDescarga(formato_fecha_operacion);
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
            registrar_descarga_local(iniciarDescargaSQL,iniciarDescargaDTO,clave_unica);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaIniciarDescargaDTO> call = restClient.postDescarga(iniciarDescargaDTO,
                    token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            Log.w("Numero ", iniciarDescargaDTO.toString());
            call.enqueue(new Callback<RespuestaIniciarDescargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaIniciarDescargaDTO> call,
                                       Response<RespuestaIniciarDescargaDTO> response) {
                    RespuestaIniciarDescargaDTO data = response.body();

                    if (response.isSuccessful()&& data.isExito()) {
                        Log.w("IniciarDescarga", "Success");
                        registra_descarga = true;
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        registra_descarga = false;
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }
                    if(response.code()>=300){
                        registrar_descarga_local(iniciarDescargaSQL,iniciarDescargaDTO,clave_unica);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(iniciarDescargaSQL,token);
                        lisener.CrearRunable(Lisener.IniciarDescarga);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaIniciarDescargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_descarga = false;
                    registrar_descarga_local(iniciarDescargaSQL,iniciarDescargaDTO,clave_unica);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(iniciarDescargaSQL,token);
                    lisener.CrearRunable(Lisener.IniciarDescarga);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_descarga_local(iniciarDescargaSQL,iniciarDescargaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(iniciarDescargaSQL,token);
            lisener.CrearRunable(Lisener.IniciarDescarga);
        }*/
        //endregion
    }

    /**
     * Permite realizar el registro de la finalización de la descarga, se enviara como parametros
     * un objeto de tipo {@link FinalizarDescargaDTO} con los datos para finalizar la descarga,
     * un {@link String } que reprecenta el token de seguridad y un objeto de tipo
     * {@link FinalizarDescargaSQL} para poder registrar en local.
     * @param finalizarDescargaDTO Objeto de tipo {@link FinalizarDescargaDTO} con los valores que
     *                             se registraron de finalizar descarga
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param finalizarDescargaSQL Objeto de tipo {@link FinalizarDescargaSQL} para registrar
     *                             en base de datos local.
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO,
                                           String token,
                                           FinalizarDescargaSQL finalizarDescargaSQL) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "FD"+s.format(new Date());
        Log.w("Genero_clave",clave_unica);
        Log.w("Consulto","Consulto el servicio");
        finalizarDescargaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
        String formato_fecha_operacion = f.format(new Date());
        finalizarDescargaDTO.setFechaDescarga(formato_fecha_operacion);
        finalizarDescargaDTO.setTanquePrestado(false);
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_finalizar_local(finalizarDescargaSQL,finalizarDescargaDTO,clave_unica);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaFinalizarDescargaDTO> call =
                    restClient.postFinalizarDescarga(finalizarDescargaDTO,
                    token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaFinalizarDescargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaFinalizarDescargaDTO> call,
                                       Response<RespuestaFinalizarDescargaDTO> response) {
                    RespuestaFinalizarDescargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null){
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else{
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        //subirImagenesPresenter.errorSolicitud(data.getMensaje());
                    }

                    if(response.code()>=300){
                        registrar_finalizar_local(finalizarDescargaSQL,finalizarDescargaDTO,
                                clave_unica);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(finalizarDescargaSQL,token);
                        lisener.CrearRunable(Lisener.FinalizarDescarga);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaFinalizarDescargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_finalizar_local(finalizarDescargaSQL,finalizarDescargaDTO,
                            clave_unica);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(finalizarDescargaSQL,token);
                    lisener.CrearRunable(Lisener.FinalizarDescarga);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
       /* }*/
        /*if(intentos_post==3){
            registrar_finalizar_local(finalizarDescargaSQL,finalizarDescargaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(finalizarDescargaSQL,token);
            lisener.CrearRunable(Lisener.FinalizarDescarga);
        }*/
        //endregion

    }

    /**
     * Permite realizar el registro de la Lectura inicial , se tomara como paramretos un objeto
     * de tipo {@link SAGASSql} con la conexion a base de datos local, una cadena de tipo
     * {@link String} con el token de seguirdad del usuario y un objeto de tipo {@link LecturaDTO}
     * con los datos a enviar al servicio web o registro en movil.
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los datos a registrar en el servicio
     *                   web o en local.
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    @Override
    public void registrarLecturaInicial(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIEC"+s.format(new Date());
        lecturaDTO.setClaveProceso(clave_unica);
        /*sagasSql.InsertLecturaInicial(lecturaDTO);
        sagasSql.InsertLecturaImagenes(lecturaDTO);
        sagasSql.InsertLecturaP5000(lecturaDTO);*/
        //region Verifica si el servcio esta disponible
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf = new
                SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaDTO.setFechaAplicacion((Date) Calendar.getInstance().getTime());
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local(sagasSql,lecturaDTO,clave_unica,false);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
       /* while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicial(lecturaDTO,
                    token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null){
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else{
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                    }
                    if(response.code()>=300) {
                        registrar_local(sagasSql, lecturaDTO, clave_unica,false);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaInicial);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql, lecturaDTO, clave_unica,false);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaInicial);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaInicial);
        }*/
        //endregion
    }

    /**
     * Permite realizar el registro de los datos de la lectura final, se enviara como paramertros
     * un objeto de tipo {@link SAGASSql} que tiene el acceso a base de datos, una cadena de tipo
     * {@link String} que tiene el token del usuario y un objeto {@link LecturaDTO} con los datos
     * a registrar de la descarga
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token  Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los datos a registrar en el servicio
     *                   web o en local.
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 31/08/2018
     */
    @Override
    public void registrarLecturaFinal(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFEC"+s.format(new Date());
        lecturaDTO.setClaveProceso(clave_unica);
        //region Verifica si el servcio esta disponible
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf = new
                SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaDTO.setFechaAplicacion((Date) Calendar.getInstance().getTime());
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local(sagasSql,lecturaDTO,clave_unica,true);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinal(lecturaDTO,
                    token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>= 300){
                        registrar_local(sagasSql,lecturaDTO,clave_unica,true);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaFinal);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,lecturaDTO,clave_unica,true);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaFinal);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaFinal);
        }*/
        //endregion
        /*sagasSql.IncertarLecturaFinal(lecturaDTO);
        sagasSql.InsertImagenLecturaFinalP5000(lecturaDTO);
        sagasSql.IncertImagenesLecturaFinal(lecturaDTO);
        subirImagenesPresenter.onSuccessRegistroAndroid();*/

    }

    /**
     * <h3>registrarLecturaInicialPipa</h3>
     * Permite realizar el envio de los datos al api del web service, se tomaran como parametros
     * un objeto de tipo {@link SAGASSql} con la conexion a la base de datos local, una cadena
     * {@link String} que reprecenta el token de usuario y un objeto de tipo {@link LecturaPipaDTO}
     * con los datos de la lectura inicial de la pipa tras finalizar retornara a la pantalla de
     * menu y en caso de que no se guarde en el api se guardara en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los datos de lectura de la
     *                       pipa
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 03/09/2018
     */
    @Override
    public void registrarLecturaInicialPipa(SAGASSql sagasSql, String token,
                                            LecturaPipaDTO lecturaPipaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIP"+s.format(new Date());
        lecturaPipaDTO.setClaveProceso(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaPipaDTO.setFechaAplicacion(sf.format(new Date()));
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local(sagasSql,lecturaPipaDTO,clave_unica,false);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialPipa(
                    lecturaPipaDTO,  token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300) {
                        registrar_local(sagasSql,lecturaPipaDTO,clave_unica,false);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaInicialPipas);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,lecturaPipaDTO,clave_unica,false);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaInicialPipas);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaPipaDTO,clave_unica,false);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaInicialPipas);
        }*/
        //endregion
        /*
        sagasSql.InsertLecturaInicialPipas(lecturaPipaDTO);
        sagasSql.InsertLecturaInicialPipaP5000(lecturaPipaDTO);
        sagasSql.InsertImagenesLecturaInicialPipa(lecturaPipaDTO);
        subirImagenesPresenter.onSuccessRegistroAndroid();*/

    }

    /**
     * <h3>registrarLecturaFinalizalPipa</h3>
     * Permite realizar el envio de los datos al api del web service, se tomaran como parametros
     * un objeto de tipo {@link SAGASSql} con la conexion a la base de datos local, una cadena
     * {@link String} que reprecenta el token de usuario y un objeto de tipo {@link LecturaPipaDTO}
     * con los datos de la lectura final de la pipa tras finalizar retornara a la pantalla de
     * menu y en caso de que no se guarde en el api se guardara en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los datos de lectura de la
     *                       pipa
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 03/09/2018
     */
    @Override
    public void registrarLecturaFinalizalPipa(SAGASSql sagasSql, String token,
                                              LecturaPipaDTO lecturaPipaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFP"+s.format(new Date());
        lecturaPipaDTO.setClaveProceso(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaPipaDTO.setFechaAplicacion(sf.format(new Date()));
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local(sagasSql,lecturaPipaDTO,clave_unica,true);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialPipa(
                    lecturaPipaDTO,  token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300){
                        registrar_local(sagasSql,lecturaPipaDTO,clave_unica,true);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaFinalPipas);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local(sagasSql,lecturaPipaDTO,clave_unica,true);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaFinalPipas);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local(sagasSql,lecturaPipaDTO,clave_unica,true);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaFinalPipas);
        }*/
        //endregion
        /*sagasSql.InsertLecturaFinalPipas(lecturaPipaDTO);
        sagasSql.InsertLecturaFinalPipaP5000(lecturaPipaDTO);
        sagasSql.InsertImagenesLecturaFinalPipa(lecturaPipaDTO);
        subirImagenesPresenter.onSuccessRegistroAndroid();*/
    }

    /**
     * <h3>registrarLecturaInicialAlmacen</h3>
     * Permite realizar el envio de los datos del registro de lectura inicial al almacen,
     * tomara como parametros un objeto de tipo {@link SAGASSql} con la conexion a base de datos
     * local, una cadena {@link String} que reprecneta el token de usuario y por ultimo un objeto
     * de tipo {@link LecturaAlmacenDTO} con los datos a enviar en caso de guardarce , mostrara una
     * alerta en caso de no estar el servicio disponible se enviara a la base de datos local
     * @param sagasSql  Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los datos a enviar o
     *                          registrar de la lectura inicial de almacen
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 04/09/2018
     */
    @Override
    public void registrarLecturaInicialAlmacen(SAGASSql sagasSql, String token,
                                               LecturaAlmacenDTO lecturaAlmacenDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIA"+s.format(new Date());
        lecturaAlmacenDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaAlmacenDTO.setFechaAplicacion(sf.format(new Date()));
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,false);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

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
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialAlmacen(
                    lecturaAlmacenDTO,  token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300){
                        registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,
                                false);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaInicialAlmacen);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,
                            false);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaInicialAlmacen);
                }
            });
           /* intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,false);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaInicialAlmacen);
        }*/
        //endregion
        /*sagasSql.InsertLecturaInicialAlmacen(lecturaAlmacenDTO);
        sagasSql.InsertImagenesLecturaInicialAlamacen(lecturaAlmacenDTO);
        subirImagenesPresenter.onSuccessRegistroAndroid();*/
    }


    /**
     * <h3>registrarLecturaFinalAlmacen</h3>
     * Permite realizar el envio de los datos de lectura final de almacen, se enviaran como
     * parametros un objeto de tipo {@link SAGASSql} con la conexion a base de datos local, un
     * objeto de tipo {@link String} que reprecenta el token del usuario y un objeto de tipo
     * {@link LecturaAlmacenDTO} que contiene la información a registrar.- Los datos seran enviados
     * al servicio web , en caso de que no responda se registraran en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los datos a enviar o
     *                          registrar de la lectura inicial de almacen
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 04/09/2018
     */
    @Override
    public void registrarLecturaFinalAlmacen(SAGASSql sagasSql, String token,
                                             LecturaAlmacenDTO lecturaAlmacenDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFA"+s.format(new Date());
        lecturaAlmacenDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        lecturaAlmacenDTO.setFechaAplicacion(sf.format(new Date()));
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
        //servicio_intentos = 3;
        if (servicio_intentos == 3) {
            registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,true);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la descarga

        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofits.create(RestClient.class);

        int intentos_post = 0;
        registra_descarga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalAlmacen(
                    lecturaAlmacenDTO,  token, "application/json");
            Log.w(TAG, retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
                @Override
                public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                       Response<RespuestaLecturaInicialDTO> response) {
                    RespuestaLecturaInicialDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onRegistrarIniciarDescarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("IniciarDescarga", "not found");
                                break;
                            case 500:
                                Log.w("IniciarDescarga", "server broken");
                                break;
                            default:
                                Log.w("IniciarDescarga", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300){
                        registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,
                                true);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.LecturaFinalAlmacen);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,true);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.LecturaFinalAlmacen);
                }
            });
            /*intentos_post++;
            if(registra_descarga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_almacen(sagasSql,lecturaAlmacenDTO,clave_unica,true);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.LecturaFinalAlmacen);
        }*/
        //endregion
        /*sagasSql.InsertLecturaFinalAlmacen(lecturaAlmacenDTO);
        sagasSql.InsertImagenesLecturaFinalAlamacen(lecturaAlmacenDTO);
        subirImagenesPresenter.onSuccessRegistroAndroid();*/
    }

    /**
     * <h3>registrarRecargaEstacion</h3>
     * Permite realizar el registro de los datos de la recarga de estaciónes en el api,
     * en caso de no registrarce se enviara a la base de datos local en el
     * disposivio
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga
     * @param EsRecargaEstacionInicial boolean que determina si es recarga inicial
     */
    @Override
    public void registrarRecargaEstacion(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,
                                         boolean EsRecargaEstacionInicial) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "RE";
        clave_unica += (EsRecargaEstacionInicial)? "I":"F";
        clave_unica += s.format(new Date());
        recargaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        recargaDTO.setFechaApliacacion(sf.format(new Date()));
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
            registrar_local_recarga(sagasSql,recargaDTO,clave_unica,EsRecargaEstacionInicial);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la lectura final de la camioneta

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        int intentos_post = 0;
        registra_reacrga = true;
        /*while(intentos_post<3) {*/
            /*Call<RespuestaRecargaDTO> call = null;
            if(EsRecargaEstacionInicial) {
                restClient.postRecargaInicial(
                        recargaDTO, token, "application/json");
            }else{
                restClient.postRecargaFinal(
                  recargaDTO,token,"application/json"
                );
            }*/
            Call<RespuestaRecargaDTO> call = EsRecargaEstacionInicial ?
                    restClient.postRecargaInicial(
                    recargaDTO, token, "application/json"):
                    restClient.postRecargaFinal(
                            recargaDTO,token,"application/json"
                    );

            Log.w("Url recarga estación ", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("Recarga estación", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("Recarga estación", "not found");
                                break;
                            case 500:
                                Log.w("Recarga estación", "server broken");
                                break;
                            default:
                                Log.w("Recarga estación", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        registra_reacrga = false;
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300){
                        registrar_local_recarga(sagasSql,recargaDTO,recargaDTO.getClaveOperacion(),
                                EsRecargaEstacionInicial);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.RecargaEstacion);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local_recarga(sagasSql,recargaDTO,recargaDTO.getClaveOperacion(),
                            EsRecargaEstacionInicial);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.RecargaEstacion);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
            /*intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_recarga(sagasSql,recargaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.RecargaCamioneta);
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarRecargaPipa
     * Permite realizar la recarga de la pipa en el api, se envia como parametro un objeto
     * con los datos de la recarga de la pipa, en caso de fallar se registra en la base de datos
     * local de l telefono y se espera a que el servicio este disponible
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga
     * @param esRecargaPipaFinal Boolean que especifica si la recarga es final
     */
    @Override
    public void registrarRecargaPipa(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,
                                     boolean esRecargaPipaFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "RP";
        clave_unica += (esRecargaPipaFinal)? "F":"I";
        clave_unica += s.format(new Date());
        recargaDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        recargaDTO.setFechaApliacacion(sf.format(new Date()));
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
            registrar_local_recarga(sagasSql,recargaDTO,clave_unica,esRecargaPipaFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro de la lectura final de la camioneta

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        int intentos_post = 0;
        registra_reacrga = true;
        /*while(intentos_post<3) {*/
            Call<RespuestaRecargaDTO> call = null;
            if(esRecargaPipaFinal) {
                call= restClient.postRecargaFinal(
                        recargaDTO, token, "application/json");
            }else{
                call = restClient.postRecargaInicial(
                        recargaDTO,token,"application/json"
                );
            }
            Log.w("Url recarga pipa", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("RecargaPipa", "not found");
                                break;
                            case 500:
                                Log.w("RecargaPipa", "server broken");
                                break;
                            default:
                                Log.w("RecargaPipa", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>= 300){
                        registrar_local_recarga(sagasSql,recargaDTO,recargaDTO.getClaveOperacion(),
                                esRecargaPipaFinal);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.RecargaPipa);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_reacrga = false;
                    registrar_local_recarga(sagasSql,recargaDTO,recargaDTO.getClaveOperacion(),
                            esRecargaPipaFinal);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.RecargaPipa);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
           /* intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
       /* }*/
        /*if(intentos_post==3){
            registrar_local_recarga(sagasSql,recargaDTO,clave_unica);
            registro_local = true;
        }
        if(registro_local ){
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.RecargaCamioneta);
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarAutoconsumoEstacion
     * Permite realizar el registro del auto consumo de la estación de carburación,
     * se envia como parametro un objeto {@link AutoconsumoDTO} con los datos a registrar en la api
     * en caso de fallar se registra local en el dispositivo
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param autoconsumoDTO Objeto de tipo {@link AutoconsumoDTO} con los datos del autoconsumo
     * @param esAutoconsumoEstacionFinal Boolean que determina si el autoconsumo es final
     */
    @Override
    public void registrarAutoconsumoEstacion(SAGASSql sagasSql, String token, AutoconsumoDTO
            autoconsumoDTO, boolean esAutoconsumoEstacionFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "AE";
        clave_unica += (esAutoconsumoEstacionFinal)? "F":"I";
        clave_unica += s.format(new Date());
        autoconsumoDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        autoconsumoDTO.setFechaAplicacion(sf.format(new Date()));
        //region Verifica si el servcio esta disponible
        autoconsumoDTO.setFechaRegistro(sf.format(new Date()));
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
            registrar_local_autoconsumo(sagasSql,autoconsumoDTO,
                    SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION,esAutoconsumoEstacionFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
         /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/

            Call<RespuestaRecargaDTO> call = restClient.postAutorconsumo(
                        autoconsumoDTO,
                       /* true,
                        false,
                        false,*/
                        esAutoconsumoEstacionFinal,
                        token,
                        "application/json"
            );
            Log.w("Url camioneta", retrofit.baseUrl().toString());

            call.enqueue(new Callback<RespuestaRecargaDTO>() {

                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();

                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                        registro_local = false;

                    } else {
                        switch (response.code()) {
                            case 404:
                                Log.w("Autoconsumo estacion", "not found");
                                break;
                            case 500:
                                Log.w("Autoconsumo estacion", "server broken");
                                break;
                            default:
                                Log.w("Autoconsumo estacion", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        registro_local = true;
                        if(data!=null)
                            subirImagenesPresenter.errorSolicitud(data.getMensajesError());
                        else
                            subirImagenesPresenter.errorSolicitud(response.message());
                    }
                    if(response.code()>=300) {
                        registrar_local_autoconsumo(sagasSql, autoconsumoDTO,
                                SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION,
                                esAutoconsumoEstacionFinal);
                        Lisener lisener = new Lisener(sagasSql, token);
                        lisener.CrearRunable(Lisener.Autoconsumo);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registro_local = true;
                    registrar_local_autoconsumo(sagasSql, autoconsumoDTO,
                            SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION,
                            esAutoconsumoEstacionFinal);
                    Lisener lisener = new Lisener(sagasSql, token);
                    lisener.CrearRunable(Lisener.Autoconsumo);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });

            /*if (registro_local){
                registrar_local_autoconsumo(sagasSql, autoconsumoDTO,
                        SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION, esAutoconsumoEstacionFinal);
                subirImagenesPresenter.onSuccessRegistroAndroid();
                Lisener lisener = new Lisener(sagasSql,token);
                lisener.CrearRunable(Lisener.Autoconsumo);
                subirImagenesPresenter.onSuccessRegistroAndroid();
            }
            intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
       /* if(intentos_post==3){
            registrar_local_autoconsumo(sagasSql,autoconsumoDTO,
                    SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION,esAutoconsumoEstacionFinal);
            registro_local = true;
        }
        if(registro_local ){
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.RecargaCamioneta);
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion

    }

    /**
     * registrarAutoconsumoInventario
     * Realiza el registro de un autoconsumo de inventario, se enviara al api el objeto
     * dto de tipo {@link AutoconsumoDTO} con los datos del autoconsumo de inventario, en caso
     *  de que no se registre en el api se almacenara en la base de datos local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param autoconsumoDTO Objeto de tipo {@link AutoconsumoDTO} con los datos del autoconsumo
     * @param esAutoconsumoInventarioFinal Boolean que determina si es un autoconsumo final
     */
    @Override
    public void registrarAutoconsumoInventario(SAGASSql sagasSql, String token, AutoconsumoDTO
            autoconsumoDTO, boolean esAutoconsumoInventarioFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "AIN";
        clave_unica += (esAutoconsumoInventarioFinal)? "F":"I";
        clave_unica += s.format(new Date());
        autoconsumoDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        autoconsumoDTO.setFechaAplicacion(sf.format(new Date()));
        autoconsumoDTO.setFechaRegistro(sf.format(new Date()));
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
            registrar_local_autoconsumo(sagasSql,autoconsumoDTO,
                    SAGASSql.TIPO_AUTOCONSUMO_INVENTARIO_GENERAL,esAutoconsumoInventarioFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaRecargaDTO> call = restClient.postAutorconsumo(
                    autoconsumoDTO,
                    /*false,
                    true,
                    false,*/
                    esAutoconsumoInventarioFinal,
                    token,
                    "application/json"
            );
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        //registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Autoconsumo inventario", "not found");
                                break;
                            case 500:
                                Log.w("Autoconsumo inventario", "server broken");
                                break;
                            default:
                                Log.w("Autoconsumo inventario", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        //registra_reacrga= false;
                    }
                    if(response.code()>=300){
                        registrar_local_autoconsumo(sagasSql, autoconsumoDTO,
                                SAGASSql.TIPO_AUTOCONSUMO_INVENTARIO_GENERAL,
                                esAutoconsumoInventarioFinal);
                        Lisener lisener = new Lisener(sagasSql, token);
                        lisener.CrearRunable(Lisener.Autoconsumo);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_reacrga = false;
                    registrar_local_autoconsumo(sagasSql, autoconsumoDTO,
                            SAGASSql.TIPO_AUTOCONSUMO_INVENTARIO_GENERAL,
                            esAutoconsumoInventarioFinal);
                    Lisener lisener = new Lisener(sagasSql, token);
                    lisener.CrearRunable(Lisener.Autoconsumo);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
           /* intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }
        }
        if(intentos_post==3){
            registrar_local_autoconsumo(sagasSql,autoconsumoDTO,
                    SAGASSql.TIPO_AUTOCONSUMO_INVENTARIO_GENERAL,esAutoconsumoInventarioFinal);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion

    }

    /**
     * registrarAutoconsumoPipa
     * Permite realizar el registro del autoconsumo de la pipa, se enviara un objeto de
     * tipo {@link AutoconsumoDTO} al servicio api, en caso de que no se registre se
     * guardara en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param autoconsumoDTO Objeto de tipo {@link AutoconsumoDTO} con los datos del autoconsumo
     * @param esAutoconsumoPipaFinal Boolean que determina si el autoconsumo es final
     */
    @Override
    public void registrarAutoconsumoPipa(SAGASSql sagasSql, String token, AutoconsumoDTO
            autoconsumoDTO, boolean esAutoconsumoPipaFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "AP";
        clave_unica += (esAutoconsumoPipaFinal)? "F":"I";
        clave_unica += s.format(new Date());
        autoconsumoDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        autoconsumoDTO.setFechaAplicacion(sf.format(new Date()));
        autoconsumoDTO.setFechaRegistro(sf.format(new Date()));
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
            registrar_local_autoconsumo(sagasSql,autoconsumoDTO,
                    SAGASSql.TIPO_AUTOCONSUMO_PIPAS,esAutoconsumoPipaFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaRecargaDTO> call = restClient.postAutorconsumo(
                    autoconsumoDTO,
                    /*false,
                    false,
                    true,*/
                    esAutoconsumoPipaFinal,
                    token,
                    "application/json"
            );
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaRecargaDTO>() {
                @Override
                public void onResponse(Call<RespuestaRecargaDTO> call,
                                       Response<RespuestaRecargaDTO> response) {
                    RespuestaRecargaDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Autoconsumo inventario", "not found");
                                break;
                            case 500:
                                Log.w("Autoconsumo inventario", "server broken");
                                break;
                            default:
                                Log.w("Autoconsumo inventario", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                       // registra_reacrga= false;
                    }
                    if(response.code()>=300){
                        registrar_local_autoconsumo(sagasSql,
                                autoconsumoDTO,SAGASSql.TIPO_AUTOCONSUMO_PIPAS,
                                esAutoconsumoPipaFinal);
                        Lisener lisener = new Lisener(sagasSql, token);
                        lisener.CrearRunable(Lisener.Autoconsumo);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_reacrga = false;
                    registrar_local_autoconsumo(sagasSql,
                            autoconsumoDTO,SAGASSql.TIPO_AUTOCONSUMO_PIPAS,
                            esAutoconsumoPipaFinal);
                    Lisener lisener = new Lisener(sagasSql, token);
                    lisener.CrearRunable(Lisener.Autoconsumo);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
            /*intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_autoconsumo(sagasSql,
                    autoconsumoDTO,SAGASSql.TIPO_AUTOCONSUMO_PIPAS,esAutoconsumoPipaFinal);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarTraspasoEstacion
     * Premite registrar los datos del traspaso de la estacion , tomara un objeto de tipo
     * {@link TraspasoDTO} con los datos de este para ser registrado en el api , en caso de que
     * los datos no se registracen se guardaran en la base de datos local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param traspasoDTO Objeto de tipo {@link TraspasoDTO} con los datos del traspaso
     * @param esTraspasoEstacionFinal Boolean que determina si es un traspaso final
     */
    @Override
    public void registrarTraspasoEstacion(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO,
                                          boolean esTraspasoEstacionFinal) {
        /*@SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "TE";
        clave_unica += (esTraspasoEstacionFinal)? "F":"I";
        clave_unica += s.format(new Date());
        traspasoDTO.setClaveOperacion(clave_unica);
        traspasoDTO.setFecha((Date) Calendar.getInstance().getTime());
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        traspasoDTO.setFechaAplicacion(sf.format(new Date()));*/
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
            registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoEstacionFinal,
                    SAGASSql.TIPO_TRASPASO_ESTACION);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaTraspasoDTO> call = restClient.postTraspaso(
                    traspasoDTO,
                    /*true,
                    false,*/
                    esTraspasoEstacionFinal,
                    token,
                    "application/json"
            );
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaTraspasoDTO>() {
                @Override
                public void onResponse(Call<RespuestaTraspasoDTO> call,
                                       Response<RespuestaTraspasoDTO> response) {
                    RespuestaTraspasoDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                        registra_reacrga = true;
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Autoconsumo inventario", "not found");
                                break;
                            case 500:
                                Log.w("Autoconsumo inventario", "server broken");
                                break;
                            default:
                                Log.w("Autoconsumo inventario", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        registra_reacrga= false;
                    }
                    if(response.code()>=300){
                        registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoEstacionFinal,
                                SAGASSql.TIPO_TRASPASO_ESTACION);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Traspaso);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_reacrga = false;
                    registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoEstacionFinal,
                            SAGASSql.TIPO_TRASPASO_ESTACION);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Traspaso);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
          /*  intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_traspaso(sagasSql,traspasoDTO);
            registro_local = true;
        }
        if(registro_local ){
            //Lisener lisener = new Lisener(sagasSql,token);
            //lisener.CrearRunable(Lisener.RecargaCamioneta);
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarTraspasoPipa
     * Permite realizar el registro de un traspaso de pipa , tomara los datos que estan el
     * el objeto dto {@link TraspasoDTO} para enviarlos al registro, en caso de que no
     * reponda el servicio o no este disponible , los datos seran registrados en local y enviados
     * para procesarce al api nuevamente.
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param traspasoDTO Objeto de tipo {@link TraspasoDTO} con los datos del traspaso
     * @param esTraspasoPipaFinal Boolean que indica si el traspaso es final
     */
    @Override
    public void registrarTraspasoPipa(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO,
                                      boolean esTraspasoPipaFinal) {
        /*@SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "TP";
        clave_unica += (esTraspasoPipaFinal)? "F":"I";
        clave_unica += s.format(new Date());
        traspasoDTO.setClaveOperacion(clave_unica);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        traspasoDTO.setFecha((Date) Calendar.getInstance().getTime());
        traspasoDTO.setFechaAplicacion(Calendar.getInstance().getTime().toString());*/
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
            registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoPipaFinal,
                    SAGASSql.TIPO_TRASPASO_PIPA);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaTraspasoDTO> call = restClient.postTraspaso(
                    traspasoDTO,
                    /*false,
                    true,*/
                    esTraspasoPipaFinal,
                    token,
                    "application/json"
            );
            Log.w("Url camioneta", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaTraspasoDTO>() {
                @Override
                public void onResponse(Call<RespuestaTraspasoDTO> call,
                                       Response<RespuestaTraspasoDTO> response) {
                    RespuestaTraspasoDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarDescarga", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Traspaso pipa", "not found");
                                break;
                            case 500:
                                Log.w("Traspaso pipa", "server broken");
                                break;
                            default:
                                Log.w("Traspaso pipa", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        registra_reacrga= false;
                    }
                    if(response.code()>=300){
                        registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoPipaFinal,
                                SAGASSql.TIPO_TRASPASO_PIPA);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Traspaso);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registrar_local_traspaso(sagasSql,traspasoDTO,esTraspasoPipaFinal,
                            SAGASSql.TIPO_TRASPASO_PIPA);
                    registra_reacrga = false;
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Traspaso);
                    subirImagenesPresenter.errorSolicitud(t.getMessage());
                }
            });

            /*intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_traspaso(sagasSql,traspasoDTO);
            registro_local = true;
        }*/
        /*if(registro_local ){
            //Lisener lisener = new Lisener(sagasSql,token);
            //lisener.CrearRunable(Lisener.RecargaCamioneta);
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarCalibracionEstacion
     * Permite realizar el registro de la calibración de la estación de carburación,
     * se envia como parametro el objeto de tipo {@link CalibracionDTO} para su registro en la
     * api , en caso de no registrarse o no estar disponible los dato se guardan en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param calibracionDTO Objeto de tipo {@link CalibracionDTO} con los datos a enviar
     * @param esCalibracionEstacionFinal Boolean que determina si la calibración es final
     */
    @Override
    public void registrarCalibracionEstacion(SAGASSql sagasSql, String token
            , CalibracionDTO calibracionDTO, boolean esCalibracionEstacionFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "CE";
        clave_unica += (esCalibracionEstacionFinal)? "F":"I";
        clave_unica += s.format(new Date());
        Log.w("Clave",clave_unica);
        calibracionDTO.setClaveOperacion(clave_unica);
        calibracionDTO.setFechaAplicacion((Date) Calendar.getInstance().getTime());
        calibracionDTO.setFechaRegistro((Date) Calendar.getInstance().getTime());
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
            registrar_local_calibracion(sagasSql,calibracionDTO,SAGASSql.TIPO_CALIBRACION_ESTACION,
                    esCalibracionEstacionFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaTraspasoDTO> call = restClient.postCalibracion(
                    calibracionDTO,
                    /*true,
                    false,*/
                    esCalibracionEstacionFinal,
                    token,
                    "application/json"
            );
            Log.w("Url calibración", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaTraspasoDTO>() {
                @Override
                public void onResponse(Call<RespuestaTraspasoDTO> call,
                                       Response<RespuestaTraspasoDTO> response) {
                    RespuestaTraspasoDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("Iniciar Calibracion ", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Calibración estación", "not found");
                                break;
                            case 500:
                                Log.w("Calibración estación", "server broken");
                                break;
                            default:
                                Log.w("Calibración estación", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        //registra_reacrga= false;
                    }

                    if(response.code()>=300){
                        registrar_local_calibracion(sagasSql,calibracionDTO,
                                SAGASSql.TIPO_CALIBRACION_ESTACION,
                                esCalibracionEstacionFinal);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Calibracion);
                    }
                }

                @Override
                public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    subirImagenesPresenter.errorSolicitud(t.getMessage());
                    registrar_local_calibracion(sagasSql,calibracionDTO,
                            SAGASSql.TIPO_CALIBRACION_ESTACION,
                            esCalibracionEstacionFinal);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Calibracion);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
            /*intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_calibracion(sagasSql,calibracionDTO);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrarCalibracionPipa
     * Permite el registro de la calibracion de una pipa, toma como parametro los datos
     * de un objeto {@link CalibracionDTO} con los datos de esta para ser enviado al api, en
     * caso de que el api no responda se guardara en local
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param token Cadena de {@link String} con el token de seguirdad de la cuenta
     * @param calibracionDTO Objeto de tipo {@link CalibracionDTO} con los datos a enviar
     * @param esCalibracionPipaFinal boolean que determina si la calibracion es final
     */
    @Override
    public void registrarCalibracionPipa(SAGASSql sagasSql, String token,
                                         CalibracionDTO calibracionDTO,
                                         boolean esCalibracionPipaFinal) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "CP";
        clave_unica += (esCalibracionPipaFinal)? "F":"I";
        clave_unica += s.format(new Date());
        calibracionDTO.setClaveOperacion(clave_unica);
        calibracionDTO.setFechaAplicacion((Date) Calendar.getInstance().getTime());
        calibracionDTO.setFechaRegistro((Date) Calendar.getInstance().getTime());
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
            registrar_local_calibracion(sagasSql,calibracionDTO,SAGASSql.TIPO_CALIBRACION_PIPA,
                    esCalibracionPipaFinal);
            registro_local = true;
        }

        //endregion
        //region Realiza el registro del autoconsumo

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        /*int intentos_post = 0;
        registra_reacrga = true;
        while(intentos_post<3) {*/
            Call<RespuestaTraspasoDTO> call = restClient.postCalibracion(
                    calibracionDTO,
                    /*false,
                    true,*/
                    esCalibracionPipaFinal,
                    token,
                    "application/json"
            );
            Log.w("Url calibración", retrofit.baseUrl().toString());
            call.enqueue(new Callback<RespuestaTraspasoDTO>() {
                @Override
                public void onResponse(Call<RespuestaTraspasoDTO> call,
                                       Response<RespuestaTraspasoDTO> response) {
                    RespuestaTraspasoDTO data = response.body();
                    if (response.isSuccessful()) {
                        Log.w("IniciarCalibracion", "Success");
                        subirImagenesPresenter.onSuccessRegistroRecarga();
                    } else {
                        registra_reacrga = false;
                        switch (response.code()) {
                            case 404:
                                Log.w("Traspaso pipa", "not found");
                                break;
                            case 500:
                                Log.w("Traspaso pipa", "server broken");
                                break;
                            default:
                                Log.w("Traspaso pipa", "" + response.code());
                                Log.w(" Error", response.message() + " " +
                                        response.raw().toString());
                                break;
                        }
                        if(data!=null) {
                            subirImagenesPresenter.errorSolicitud(data.getMensaje());
                        }else {
                            subirImagenesPresenter.errorSolicitud(response.message());
                        }
                        registra_reacrga= false;
                    }
                    if(response.code()>=300){
                        registrar_local_calibracion(sagasSql,calibracionDTO,
                                SAGASSql.TIPO_CALIBRACION_PIPA,esCalibracionPipaFinal);
                        Lisener lisener = new Lisener(sagasSql,token);
                        lisener.CrearRunable(Lisener.Calibracion);
                        subirImagenesPresenter.onSuccessRegistroAndroid();
                    }
                }

                @Override
                public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    registra_reacrga = false;
                    subirImagenesPresenter.errorSolicitud(t.getMessage());
                    registrar_local_calibracion(sagasSql,calibracionDTO,
                            SAGASSql.TIPO_CALIBRACION_PIPA,esCalibracionPipaFinal);
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Calibracion);
                    subirImagenesPresenter.onSuccessRegistroAndroid();
                }
            });
            /*intentos_post++;
            if(registra_reacrga){
                break;
            }else{
                intentos_post++;
            }*/
        /*}*/
        /*if(intentos_post==3){
            registrar_local_calibracion(sagasSql,calibracionDTO,SAGASSql.TIPO_CALIBRACION_PIPA
                    ,esCalibracionPipaFinal);
            subirImagenesPresenter.onSuccessRegistroAndroid();
            Lisener lisener = new Lisener(sagasSql,token);
            lisener.CrearRunable(Lisener.Calibracion);
            registro_local = true;
        }
        if(registro_local ){
            subirImagenesPresenter.onSuccessRegistroAndroid();
        }*/
        //endregion
    }

    /**
     * registrar_local_calibracion
     * Permite realizar el registro de la calibración en el dispositivo en caso de que no se realice
     * por medio del api, se gurada los datos que tiene el objeto {@link CalibracionDTO}
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param calibracionDTO Objeto de tipo {@link CalibracionDTO} con los datos a enviar
     */
    private void registrar_local_calibracion(SAGASSql sagasSql, CalibracionDTO calibracionDTO,
                                             String tipo,boolean esFinal){
        if(sagasSql.GetCalibracionByClaveOperacion(calibracionDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InsertarCalibracion(calibracionDTO,tipo,esFinal);
            if(sagasSql.GetFotografiasCalibracion(calibracionDTO.getClaveOperacion()).getCount()==0){
                sagasSql.InsertarImagenesCalibracion(calibracionDTO);
            }
        }
    }

    /**
     * registrar_local_traspaso
     * Permite realizar el registro en local del traspaso, tomara como parametro
     * el objeto dto {@link TraspasoDTO} con los datos de este
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param traspasoDTO Objeto de tipo {@link TraspasoDTO} con los datos del traspaso
     * @param esFinal boolean que determina si es final
     * @param tipo Determina el tipo de traspaso
     */
    private void registrar_local_traspaso(SAGASSql sagasSql, TraspasoDTO traspasoDTO,
                                          boolean esFinal,String tipo) {
        if(sagasSql.GetTraspasoByClaveOperacion(traspasoDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InsertTraspaso(traspasoDTO,esFinal,tipo);
            if(sagasSql.GetImagenesTraspaso(traspasoDTO.getClaveOperacion()).getCount()==0){
                sagasSql.InsertImagenesTraspaso(traspasoDTO);
            }
        }
    }

    /**
     * Permite realizar el registro en local de un autoconsumo
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param autoconsumoDTO Objeto de tipo {@link AutoconsumoDTO} con los datos del autoconsumo
     * @param Tipo Reprecenta una cadena de string que determina e tipo de autoconsumo
     * @param esFinal boolean que determina si es final
     */
    private void registrar_local_autoconsumo(SAGASSql sagasSql, AutoconsumoDTO autoconsumoDTO,
                                             String Tipo,boolean esFinal) {
        if(sagasSql.GetAutoconsumo(autoconsumoDTO.getClaveOperacion()).getCount()==0){
            sagasSql.InsertarAutoconsumo(autoconsumoDTO,Tipo,esFinal);
            if(sagasSql.GetImagenesAutoconsumo(autoconsumoDTO.getClaveOperacion()).getCount()==0){
                sagasSql.InsertarImagenesAutoconsumo(autoconsumoDTO);
            }
        }
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
    private void registrar_local(PapeletaSQL papeletaSQL,
                                 PrecargaPapeletaDTO precargaPapeletaDTO,
                                 String clave_unica){
        if (papeletaSQL.GetRecordByCalveUnica(clave_unica).getCount() == 0) {
            papeletaSQL.Insert(precargaPapeletaDTO, clave_unica);
            if (papeletaSQL.GetRecordsByCalveUnica(clave_unica).getCount() == 0) {
                papeletaSQL.InsertImagenes(precargaPapeletaDTO.getImagenesURI(), precargaPapeletaDTO.getImagenes(), clave_unica);
            }
        }
    }

    /**
     * registrar_descarga_local
     * Permite realizar el envio para el registro en local de los datos de la descarga, primero
     * se verifica si los registros ya existen, en caso contrario se realiza el registro en local.
     * @param iniciarDescargaSQL Objeto {@link IniciarDescargaSQL} que permite el uso de base
     *                           de datos en local de la descarga
     * @param iniciarDescargaDTO Objero {@link IniciarDescargaDTO} que reprecenta el modelo con
     *                           los datos de la descarga
     * @param clave_unica        Cadena {@link String} que reprecenta la clave unica de la operación
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx >
     */
    private void registrar_descarga_local(IniciarDescargaSQL iniciarDescargaSQL,
                                          IniciarDescargaDTO iniciarDescargaDTO,
                                          String clave_unica){
        if(iniciarDescargaSQL.GetDescargaByClaveOperacion(clave_unica).getCount()== 0){
            iniciarDescargaSQL.InsertDescarga(iniciarDescargaDTO,clave_unica);
            if(iniciarDescargaSQL.GetImagenesDescargaByClaveUnica(clave_unica).getCount()==0){
                iniciarDescargaSQL.IncertarImagenesDescarga(iniciarDescargaDTO,clave_unica);
            }
        }
    }

    /**
     * <h3>registrar_finalizar_local</h3>
     * Permite realizar el registro en local de los datos de la finalización de la descarga,
     * tomara como parametros un objeto de tipo {@link FinalizarDescargaSQL} ,un objeto de
     * tipo {@link FinalizarDescargaDTO} con los datos proporcionados previamente
     * y un {@link String} con la clave de operación.
     * @param finalizarDescargaSQL Objeto de tipo {@link FinalizarDescargaSQL} para registrar
     *                             en base de datos local.
     * @param finalizarDescargaDTO Objeto de tipo {@link FinalizarDescargaDTO} con los valores que
     *                             se registraron de finalizar descarga
     * @param clave_unica  Cadena de tipo {@link String} con la clave única de operación
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx
     * @date 28/08/2018
     */
    private void registrar_finalizar_local(FinalizarDescargaSQL finalizarDescargaSQL,
                                           FinalizarDescargaDTO finalizarDescargaDTO,
                                           String clave_unica){
        if(finalizarDescargaSQL.GetFinalizarDescargaByClaveOperacion(clave_unica).getCount()==0){
            finalizarDescargaSQL.InsertFinalizarDescarga(finalizarDescargaDTO,clave_unica);
            if(finalizarDescargaSQL.GetImagenesFinalizarDescargaByClaveOperacion(clave_unica)
                    .getCount()==0){
                finalizarDescargaSQL.InsertarImagenes(finalizarDescargaDTO,clave_unica);
            }
        }
    }

    private void registrar_local(SAGASSql sagasSql, LecturaDTO lecturaDTO, String clave_unica,
                                 boolean esFinal ) {
        if(esFinal) {
            if(sagasSql.GetLecturaFinalByClaveProceso(clave_unica).getCount()==0){
                sagasSql.IncertarLecturaFinal(lecturaDTO);
                if(sagasSql.GetImagenesLecturaFinalByClaveOperacion(clave_unica).getCount()==0){
                    sagasSql.IncertImagenesLecturaFinal(lecturaDTO);
                }
            }
        }else {
            if (sagasSql.GetLecturaByClaveUnica(clave_unica).getCount() == 0) {
                sagasSql.InsertLecturaInicial(lecturaDTO);
                if (sagasSql.GetLecturaImagenesByClaveUnica(clave_unica).getCount() == 0) {
                    sagasSql.InsertLecturaImagenes(lecturaDTO);
                }
            /*if(sagasSql.GetLecturaP5000ByClaveUnica(clave_unica).getCount()==0){
                sagasSql.InsertLecturaP5000(lecturaDTO);
            }*/
            }
        }
    }

    /**
     * registrar_local
     * Permite el registro en local de los datos de la lectura de la pipa que estan en
     * el dto en caso de que no esten registrados
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los datos de lectura de la
     *                       pipa
     * @param clave_unica String con la clave unica del registro
     * @param es_final Verifica si la lectura es final
     */
    private void registrar_local(SAGASSql sagasSql, LecturaPipaDTO lecturaPipaDTO,
                                 String clave_unica,boolean es_final) {
        if(!es_final) {
            if (sagasSql.GetLecturaIncialPipasByClaveOperacion(clave_unica).getCount() == 0) {
                sagasSql.InsertLecturaInicialPipas(lecturaPipaDTO);
                if (sagasSql.GetImagenesLecturaInicialPipaByClaveOperacion(clave_unica).getCount() == 0) {
                    sagasSql.InsertImagenesLecturaInicialPipa(lecturaPipaDTO);
                }
            }
        }else{
            if (sagasSql.GetLecturaFinalPipasByClaveOperacion(clave_unica).getCount() == 0) {
                sagasSql.InsertLecturaFinalPipas(lecturaPipaDTO);
                if (sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(clave_unica).getCount() == 0) {
                    sagasSql.InsertImagenesLecturaFinalPipa(lecturaPipaDTO);
                }
            }
        }
    }

    /**
     * registrar_local_almacen
     * Permite realizar el registro en local de la lectura del almacen ya sea
     * incial o final
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los datos a enviar o
     *                          registrar de la lectura del almacen
     * @param clave_unica String que reprecenta la clave unica del registro
     * @param es_final Verifica si es lectura inicial o final
     */
    private void registrar_local_almacen(SAGASSql sagasSql, LecturaAlmacenDTO lecturaAlmacenDTO,
                                         String clave_unica, boolean es_final) {
        if(!es_final){
            if(sagasSql.GetLecturaInicialAlmacenByClaveOperacion(clave_unica).getCount()==0){
                sagasSql.InsertLecturaInicialAlmacen(lecturaAlmacenDTO);
                if(sagasSql.GetImagenesLecturaInicialAlmacenByClaveOperacion(clave_unica).
                        getCount()==0){
                    sagasSql.InsertImagenesLecturaInicialAlamacen(lecturaAlmacenDTO);
                }
            }
        }else {
            if(sagasSql.GetLecturaFinalAlmacenByClaveOperacion(clave_unica).getCount()==0){
                sagasSql.InsertLecturaFinalAlmacen(lecturaAlmacenDTO);
                if(sagasSql.GetImagenesLecturaFinalAlmacenByClaveOperacion(clave_unica).
                        getCount()==0){
                    sagasSql.InsertImagenesLecturaFinalAlamacen(lecturaAlmacenDTO);
                }
            }
        }
    }

    /**
     * registrar_local_recarga
     * Permite realizar el registro de la recarga en local, para su envio
     * al api cuando se restablesca la conexión
     * @param sagasSql Objeto de tipo {@link SAGASSql} para registro en base de datos local
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga
     * @param clave_unica Cadena de tipo {@link String} con la clave única de operación
     * @param EsRecargaEstacionInicial boolean Determina si es una recarga inicial
     */
    private void registrar_local_recarga(SAGASSql sagasSql,
                                         RecargaDTO recargaDTO,
                                         String clave_unica,
                                         boolean EsRecargaEstacionInicial
                                         ) {
            if(sagasSql.GetRecargaByClaveOperacion(recargaDTO.getClaveOperacion()).getCount()==0){
                sagasSql.InsertRecarga(recargaDTO,SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION
                ,EsRecargaEstacionInicial);
                if(sagasSql.GetImagenesRecarga(recargaDTO.getClaveOperacion()).getCount()==0){
                    sagasSql.InsertarImagesRecarga(recargaDTO);
                }
            }

    }
    //endregion
}
