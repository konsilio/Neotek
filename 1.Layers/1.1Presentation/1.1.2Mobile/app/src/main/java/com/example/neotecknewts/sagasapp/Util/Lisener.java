package com.example.neotecknewts.sagasapp.Util;

import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class Lisener{
    public static final String LecturaInicial = "LecturaInicial";
    public static final String LecturaFinal = "LecturaFinal";
    public static final String Papeleta = "Papeleta";
    public static final String IniciarDescarga = "IniciarDescarga";
    public static final String FinalizarDescarga = "FinalizarDescarga";
    public static final String LecturaInicialPipas = "LecturaInicialPipas";
    public static final String LecturaFinalPipas = "LecturaFinalPipas";
    public static final String LecturaInicialAlmacen = "LecturaInicialAlmacen";
    public static final String LecturaFinalAlmacen = "LecturaFinalAlmacen";
    public static final String LecturaInicialCamioneta = "LecturaInicialCamioneta";
    public static final String LecturaFinalCamioneta = "LecturaFinalCamioneta";

    private  String token;

    private boolean completo ;
    private SAGASSql sagasSql;
    private PapeletaSQL papeletaSQL;
    private boolean EstaDisponible;
    private boolean _registrado;
    public Lisener(SAGASSql sagasSql,String token){
        this.sagasSql = sagasSql;
        this.token = token;
    }
    public Lisener(PapeletaSQL papeletaSQL,String token){
        this.papeletaSQL = papeletaSQL;
        this.token = token;
    }

    public void CrearRunable(final String proceso){
        final Runnable myTask = () -> {
            switch (proceso){
                case Papeleta:
                    completo = Papeletas();
                    break;
                case IniciarDescarga:
                    completo = IniciarDescargas();
                    break;
                case FinalizarDescarga:
                    completo = FinalizarDescarga();
                    break;
                case LecturaInicial:
                    completo = LecturaIniciarEstacion();
                    break;
                case LecturaFinal:
                    completo = LecturaFinalizarEstacion();
                    break;
                case LecturaInicialPipas:
                    completo = LecturaInicialPipa();
                    break;
                case LecturaFinalPipas:
                    completo = LecturaFinalPipa();
                    break;
                case LecturaInicialAlmacen:
                    completo = LecturaInicialAlmacen();
                    break;
                case LecturaFinalAlmacen:
                    completo = LecturaFinalAlmacen();
                    break;
                case LecturaInicialCamioneta:
                    completo = LecturaInicialCamioneta();
                    break;
                case LecturaFinalCamioneta:
                    completo = LecturaFinalCamioneta();
                    break;

            }
        };

        ScheduledExecutorService timer = Executors.newSingleThreadScheduledExecutor();
        ScheduledFuture scheduledFuture = timer.
                scheduleAtFixedRate(myTask, 10, 10, TimeUnit.SECONDS);
        if(completo) {
            scheduledFuture.cancel(false);
        }
    }

    private boolean LecturaInicialCamioneta(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar camioneta: " + new Date());
            Cursor cursor = sagasSql.GetLecturasIncialesCamioneta();
            LecturaCamionetaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaCamionetaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    boolean ban = cursor.getInt(
                            cursor.getColumnIndex("EsEncargadoPuerta")) == 1;
                    lecturaDTO.setEsEncargadoPuerta(ban);
                    /*lecturaDTO.setIdTipoMedior(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedior")));*/
                    /*lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));*/
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdCamioneta(cursor.getInt(cursor.getColumnIndex(
                            "IdCamioneta")));
                    /*lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));*/
                    /*Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveOperacion());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }*/
                    Cursor cantidad = sagasSql.GetCilindrosLecturaInicialCamioneta(
                            lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cursor.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindros().add(row);
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaInicialCamioneta(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaInicialCamioneta(lecturaDTO.getClaveOperacion());
                        sagasSql.EliminarCilindrosLecturaInicialCamioneta(
                                lecturaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasIncialesCamioneta().getCount()==0);
    }

    private boolean RegistrarLecturaInicialCamioneta(LecturaCamionetaDTO lecturaDTO){
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialCamioneta(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaFinalCamioneta(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar camioneta: " + new Date());
            Cursor cursor = sagasSql.GetLecturaFinalCamionetas();
            LecturaCamionetaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaCamionetaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    boolean ban = cursor.getInt(
                            cursor.getColumnIndex("EsEncargadoPuerta")) == 1;
                    lecturaDTO.setEsEncargadoPuerta(ban);
                    /*lecturaDTO.setIdTipoMedior(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedior")));*/
                    /*lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));*/
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdCamioneta(cursor.getInt(cursor.getColumnIndex(
                            "IdCamioneta")));
                    /*lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));*/
                    /*Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveOperacion());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }*/
                    Cursor cantidad = sagasSql.GetCilindrosLecturaFinalCamioneta(
                            lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cursor.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindros().add(row);
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaFinalCamioneta(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaFinalCamioneta(lecturaDTO.getClaveOperacion());
                        sagasSql.EliminarCilindrosLecturaFinalCamioneta(
                                lecturaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturaFinalCamionetas().getCount()==0);
    }

    private boolean RegistrarLecturaFinalCamioneta(LecturaCamionetaDTO lecturaDTO){
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalCamioneta(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaFinalAlmacen(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar pipa: " + new Date());
            Cursor cursor = sagasSql.GetLecturasFinalesAlmacen();
            LecturaAlmacenDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaAlmacenDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setIdTipoMedior(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedior")));
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdAlmacen(cursor.getInt(cursor.getColumnIndex(
                            "IdAlmacen")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));
                    Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveOperacion());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaFinalAlmacen(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaFinalAlmacen(lecturaDTO.getClaveOperacion());
                        sagasSql.EliminarImagenesLecturaFinalAlmacen(lecturaDTO.getClaveOperacion());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasFinalesAlmacen().getCount()==0);
    }

    private boolean RegistrarLecturaFinalAlmacen(LecturaAlmacenDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalAlmacen(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaInicialAlmacen(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar pipa: " + new Date());
            Cursor cursor = sagasSql.GetLecturasIncialesAlmacen();
            LecturaAlmacenDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaAlmacenDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setIdTipoMedior(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedior")));
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdAlmacen(cursor.getInt(cursor.getColumnIndex(
                            "IdAlmacen")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));
                    Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveOperacion());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaInicialAlmacen(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaIncialAlmacen(lecturaDTO.getClaveOperacion());
                        sagasSql.EliminarImagenesLecturaInicialAlmacen(lecturaDTO.getClaveOperacion());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasIncialesAlmacen().getCount()==0);
    }

    private boolean RegistrarLecturaInicialAlmacen(LecturaAlmacenDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialAlmacen(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaInicialPipa() {
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar pipa: " + new Date());
            Cursor cursor = sagasSql.GetLecturasIncialesPipas();
            LecturaPipaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaPipaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveProceso(cursor.getString(
                            cursor.getColumnIndex("ClaveProceso")));
                    lecturaDTO.setIdTipoMedidor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidor")));
                    /*lecturaDTO.setNombreTipoMedidor(cursor.getString(cursor.getColumnIndex(
                            "NombreTipoMedidor")));*/
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdPipa(cursor.getInt(cursor.getColumnIndex(
                            "IdPipa")));
                    lecturaDTO.setCantidadP5000(cursor.getInt(cursor.getColumnIndex(
                            "CantidadP5000")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));

                   /* Cursor ImagenP5000 = sagasSql.GetLecturaP5000ByClaveUnica(
                            lecturaDTO.getClaveProceso());*/
                    /* Coloco los valores de la imagen del P5000 en el DTO */
                    /*lecturaDTO.setImagenP5000(cursor.getString(cursor.getColumnIndex(
                            "Imagen")));
                    String uri = cursor.getString(cursor.getColumnIndex(
                            "Url"));
                    try {
                        lecturaDTO.setImagenP5000URI(new URI(uri));
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }*/
                    Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveProceso());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveProceso());
                    registrado = RegistrarLecturaInicialPipa(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaInicialPipa(lecturaDTO.getClaveProceso());
                        sagasSql.EliminarImagenesLecturaInicialPipas(lecturaDTO.getClaveProceso());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasIncialesPipas().getCount()==0);
    }

    private boolean RegistrarLecturaInicialPipa(LecturaPipaDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveProceso());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicialPipa(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveProceso()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaFinalPipa() {
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura final pipa: " + new Date());
            Cursor cursor = sagasSql.GetLecturasFinaesPipas();
            LecturaPipaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaPipaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveProceso(cursor.getString(
                            cursor.getColumnIndex("ClaveProceso")));
                    lecturaDTO.setIdTipoMedidor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidor")));
                    /*lecturaDTO.setNombreTipoMedidor(cursor.getString(cursor.getColumnIndex(
                            "NombreTipoMedidor")));*/
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    /*lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));*/
                    lecturaDTO.setIdPipa(cursor.getInt(cursor.getColumnIndex(
                            "IdPipa")));
                    lecturaDTO.setCantidadP5000(cursor.getInt(cursor.getColumnIndex(
                            "CantidadP5000")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));

                   /* Cursor ImagenP5000 = sagasSql.GetLecturaP5000ByClaveUnica(
                            lecturaDTO.getClaveProceso());*/
                    /* Coloco los valores de la imagen del P5000 en el DTO */
                    /*lecturaDTO.setImagenP5000(cursor.getString(cursor.getColumnIndex(
                            "Imagen")));
                    String uri = cursor.getString(cursor.getColumnIndex(
                            "Url"));
                    try {
                        lecturaDTO.setImagenP5000URI(new URI(uri));
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }*/
                    Cursor Imagen = sagasSql.GetImagenesLecturaFinalPipaByClaveOperacion(
                            lecturaDTO.getClaveProceso());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveProceso());
                    registrado = RegistrarLecturaFinalPipa(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaFinalPipa(lecturaDTO.getClaveProceso());
                        sagasSql.EliminarImagenesLecturaFinalPipas(lecturaDTO.getClaveProceso());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }

        return sagasSql.GetLecturasFinaesPipas().getCount() == 0;
    }

    private boolean RegistrarLecturaFinalPipa(LecturaPipaDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveProceso());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinalPipa(lecturaDTO,
                token,"application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveProceso()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaFinalizarEstacion() {
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar estación: " + new Date());
            Cursor cursor = sagasSql.GetLecturasFinales();
            LecturaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveProceso(cursor.getString(
                            cursor.getColumnIndex("ClaveProceso")));
                    lecturaDTO.setIdTipoMedidor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidor")));
                    lecturaDTO.setNombreTipoMedidor(cursor.getString(cursor.getColumnIndex(
                            "NombreTipoMedidor")));
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));
                    lecturaDTO.setIdEstacionCarburacion(cursor.getInt(cursor.getColumnIndex(
                            "IdEstacionCarburacion")));
                    lecturaDTO.setCantidadP5000(cursor.getInt(cursor.getColumnIndex(
                            "CantidadP5000")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));

                   /* Cursor ImagenP5000 = sagasSql.GetLecturaP5000ByClaveUnica(
                            lecturaDTO.getClaveProceso());*/
                    /* Coloco los valores de la imagen del P5000 en el DTO */
                    /*lecturaDTO.setImagenP5000(cursor.getString(cursor.getColumnIndex(
                            "Imagen")));
                    String uri = cursor.getString(cursor.getColumnIndex(
                            "Url"));
                    try {
                        lecturaDTO.setImagenP5000URI(new URI(uri));
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }*/
                    Cursor Imagen = sagasSql.GetImagenesLecturaFinalByClaveOperacion(
                            lecturaDTO.getClaveProceso());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveProceso());
                    registrado = RegistrarLecturaFinal(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaFinal(lecturaDTO.getClaveProceso());
                        sagasSql.EliminarImagenesLecturaFinal(lecturaDTO.getClaveProceso());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasFinales().getCount()==0);
    }

    private boolean RegistrarLecturaFinal(LecturaDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveProceso());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaFinal(lecturaDTO,token,
                "application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveProceso()+": "+
                _registrado);
        return _registrado;
    }

    private boolean LecturaIniciarEstacion() {
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar estación: " + new Date());
            Cursor cursor = sagasSql.GetLecturasIniciales();
            LecturaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new LecturaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveProceso(cursor.getString(
                            cursor.getColumnIndex("ClaveProceso")));
                    lecturaDTO.setIdTipoMedidor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidor")));
                    lecturaDTO.setNombreTipoMedidor(cursor.getString(cursor.getColumnIndex(
                            "NombreTipoMedidor")));
                    lecturaDTO.setCantidadFotografias(cursor.getInt(cursor.getColumnIndex(
                            "CantidadFotografiasMedidor")));
                    lecturaDTO.setNombreEstacionCarburacion(cursor.getString(cursor.getColumnIndex(
                            "NombreEstacionCarburacion")));
                    lecturaDTO.setIdEstacionCarburacion(cursor.getInt(cursor.getColumnIndex(
                            "IdEstacionCarburacion")));
                    lecturaDTO.setCantidadP5000(cursor.getInt(cursor.getColumnIndex(
                            "CantidadP5000")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(cursor.getColumnIndex(
                            "PorcentajeMedidor")));

                   /* Cursor ImagenP5000 = sagasSql.GetLecturaP5000ByClaveUnica(
                            lecturaDTO.getClaveProceso());*/
                    /* Coloco los valores de la imagen del P5000 en el DTO */
                    /*lecturaDTO.setImagenP5000(cursor.getString(cursor.getColumnIndex(
                            "Imagen")));
                    String uri = cursor.getString(cursor.getColumnIndex(
                            "Url"));
                    try {
                        lecturaDTO.setImagenP5000URI(new URI(uri));
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }*/
                    Cursor Imagen = sagasSql.GetLecturaImagenesByClaveUnica(
                            lecturaDTO.getClaveProceso());
                    Imagen.moveToFirst();
                    while (Imagen.isAfterLast()){
                        String iuri = Imagen.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                    }
                    Log.w("ClaveProceso", lecturaDTO.getClaveProceso());
                    registrado = RegistrarLecturaInicial(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLectura(lecturaDTO.getClaveProceso());
                        sagasSql.EliminarLecturaImagenes(lecturaDTO.getClaveProceso());
                        //sagasSql.EliminarLecturaP5000(lecturaDTO.getClaveProceso());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetLecturasIniciales().getCount()==0);
    }

    private boolean RegistrarLecturaInicial(LecturaDTO lecturaDTO) {
        Log.w("Registro","Registrando en servicio "+lecturaDTO.getClaveProceso());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicial(lecturaDTO,token,
                "application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveProceso()+": "+
            _registrado);
        return _registrado;
    }

    private boolean FinalizarDescarga() {
        Log.w("Iniciando","Revisando finalizar descarga: "+new Date());
        return false;
    }

    private boolean IniciarDescargas() {
        Log.w("Iniciando","Revisando iniciar descarga: "+new Date());
        return false;
    }

    private boolean Papeletas(){
        Log.w("Mensaje","Revisando papeletas: "+new Date());
        return false;
    }

    private boolean ServicioDisponible(){
        Log.v("Servicio","Verifica el estatus del servicio");
        Gson gsons = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofits =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gsons))
                .build();
        RestClient restClientS = retrofits.create(RestClient.class);
        Call<RespuestaServicioDisponibleDTO> callS = restClientS.postServicio(token,
                "application/json");
        callS.enqueue(new Callback<RespuestaServicioDisponibleDTO>() {
            @Override
            public void onResponse(Call<RespuestaServicioDisponibleDTO> call, Response<RespuestaServicioDisponibleDTO> response) {
                RespuestaServicioDisponibleDTO data = response.body();
                EstaDisponible = response.isSuccessful() && data.isExito();
                Log.w("Servicio","El servicio esta disponible");
            }

            @Override
            public void onFailure(Call<RespuestaServicioDisponibleDTO> call, Throwable t) {
                EstaDisponible = false;
                Log.w("Servicio","El servicio no esta disponible");
            }
        });
        return EstaDisponible;
    }
}
