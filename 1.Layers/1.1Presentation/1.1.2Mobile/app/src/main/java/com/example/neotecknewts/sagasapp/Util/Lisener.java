package com.example.neotecknewts.sagasapp.Util;

import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaFinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
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
    public static final String RecargaCamioneta = "RecargaCamioneta";

    private  String token;

    private boolean completo ;
    private SAGASSql sagasSql;
    private PapeletaSQL papeletaSQL;
    private IniciarDescargaSQL iniciarDescargaSQL;
    private FinalizarDescargaSQL finalizarDescargaSQL;
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
    public Lisener(IniciarDescargaSQL iniciarDescargaSQL ,String token){
        this.iniciarDescargaSQL = iniciarDescargaSQL;
        this.token = token;
    }

    public Lisener(FinalizarDescargaSQL finalizarDescargaSQL ,String token){
        this.finalizarDescargaSQL = finalizarDescargaSQL;
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
                case RecargaCamioneta:
                    completo = RecargaCamioneta();
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
    private boolean RecargaCamioneta(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando lectura iniciar camioneta: " + new Date());
            Cursor cursor = sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_CAMIONETA);
            RecargaDTO recargaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    recargaDTO = new RecargaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    recargaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    recargaDTO.setIdCAlmacenGasSalida(cursor.getInt(
                            cursor.getColumnIndex("IdCAlmacenGasSalida")));
                    recargaDTO.setIdCAlmacenGasEntrada(cursor.getInt(
                            cursor.getColumnIndex("IdCAlmacenGasEntrada")));
                    recargaDTO.setIdTipoMedidorSalida(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorSalida")));
                    recargaDTO.setIdTipoMedidorEntrada(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorEntrada")));
                    recargaDTO.setIdTipoEvento(cursor.getInt(
                            cursor.getColumnIndex("IdTipoEvento")));
                    recargaDTO.setP5000Salida(cursor.getInt(
                            cursor.getColumnIndex("P5000Salida")));
                    recargaDTO.setP5000Entrada(cursor.getInt(
                            cursor.getColumnIndex("P5000Entrada")));

                    String tipo = cursor.getString(
                            cursor.getColumnIndex("Tipo"));
                    if(tipo.equals(SAGASSql.TIPO_RECARGA_CAMIONETA)){

                    }else{

                    }
                    Cursor cantidad = sagasSql.GetCilindrosRecarga(recargaDTO.getClaveOperacion());
                    /*cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindros().add(row);
                        lecturaDTO.getIdCilindro().add(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindroCantidad().add(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaInicialCamioneta(lecturaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaInicialCamioneta(lecturaDTO.getClaveOperacion());
                        sagasSql.EliminarCilindrosLecturaInicialCamioneta(
                                lecturaDTO.getClaveOperacion());
                    }*/
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_CAMIONETA).getCount()==0);
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
                    while (!cantidad.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindros().add(row);
                        lecturaDTO.getIdCilindro().add(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindroCantidad().add(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
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
                    while (!cantidad.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindros().add(row);
                        lecturaDTO.getIdCilindro().add(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        lecturaDTO.getCilindroCantidad().add(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
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
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando finalizar descarga: " + new Date());
            Cursor cursor = finalizarDescargaSQL.GetFinalizarDescargas();
            FinalizarDescargaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new FinalizarDescargaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setIdOrdenCompra(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompra")));
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setFechaDescarga(cursor.getString(
                            cursor.getColumnIndex("FechaDescarga")));
                    lecturaDTO.setNombreTipoMedidorTractor(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorTractor")));
                    lecturaDTO.setNombreTipoMedidorAlmacen(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorAlmacen")));
                    lecturaDTO.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    lecturaDTO.setIdTipoMedidorAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorAlmacen")));
                    lecturaDTO.setCantidadFotosAlmacen(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosAlmacen")));
                    lecturaDTO.setCantidadFotosTractor(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosTractor")));
                    boolean es_prestado = cursor.getInt(
                            cursor.getColumnIndex("TanquePrestado")) > 0;
                    lecturaDTO.setTanquePrestado(es_prestado);
                    lecturaDTO.setPorcentajeMedidorAlmacen(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorAlmacen")));
                    lecturaDTO.setPorcentajeMedidorTractor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorTractor")));
                    lecturaDTO.setIdAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdAlmacen")));

                    Cursor cantidad = finalizarDescargaSQL.GetImagenesFinalizarDescargaByClaveOperacion(lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        String iuri = cantidad.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaFinalizarDescarga(lecturaDTO);
                    if (registrado){
                        finalizarDescargaSQL.EliminarFinalizarDescarga(lecturaDTO.getClaveOperacion());
                        finalizarDescargaSQL.EliminarImagenes(lecturaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (papeletaSQL.GetPapeletas().getCount()==0);
    }

    private boolean RegistrarLecturaFinalizarDescarga(FinalizarDescargaDTO lecturaDTO) {
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
        Call<RespuestaFinalizarDescargaDTO> call = restClient.postFinalizarDescarga(lecturaDTO,token,
                "application/json");
        call.enqueue(new Callback<RespuestaFinalizarDescargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaFinalizarDescargaDTO> call,
                                   Response<RespuestaFinalizarDescargaDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaFinalizarDescargaDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean IniciarDescargas() {
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando inicio descarga: " + new Date());
            Cursor cursor = iniciarDescargaSQL.GetIniciarDescargas();
            IniciarDescargaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new IniciarDescargaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setIdOrdenCompra(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompra")));
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setFechaDescarga(cursor.getString(
                            cursor.getColumnIndex("FechaDescarga")));
                    lecturaDTO.setNombreTipoMedidorTractor(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorTractor")));
                    lecturaDTO.setNombreTipoMedidorAlmacen(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorAlmacen")));
                    lecturaDTO.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    lecturaDTO.setIdTipoMedidorAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorAlmacen")));
                    lecturaDTO.setCantidadFotosAlmacen(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosAlmacen")));
                    lecturaDTO.setCantidadFotosTractor(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosTractor")));
                    boolean es_prestado = cursor.getInt(
                            cursor.getColumnIndex("TanquePrestado")) > 0;
                    lecturaDTO.setTanquePrestado(es_prestado);
                    lecturaDTO.setPorcentajeMedidorAlmacen(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorAlmacen")));
                    lecturaDTO.setPorcentajeMedidorTractor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorTractor")));
                    lecturaDTO.setIdAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdAlmacen")));

                    Cursor cantidad = iniciarDescargaSQL.GetImagenesDescargaByClaveUnica(lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        String iuri = cantidad.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarLecturaDescarga(lecturaDTO);
                    if (registrado){
                        iniciarDescargaSQL.EliminarDescarga(lecturaDTO.getClaveOperacion());
                        iniciarDescargaSQL.EliminarImagenesDescarga(lecturaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (papeletaSQL.GetPapeletas().getCount()==0);
    }

    private boolean RegistrarLecturaDescarga(IniciarDescargaDTO lecturaDTO) {
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
        Call<RespuestaIniciarDescargaDTO> call = restClient.postDescarga(lecturaDTO,token,
                "application/json");
        call.enqueue(new Callback<RespuestaIniciarDescargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaIniciarDescargaDTO> call,
                                   Response<RespuestaIniciarDescargaDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaIniciarDescargaDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    private boolean Papeletas(){
        boolean registrado = false;
        if(ServicioDisponible()) {
            Log.w("Iniciando", "Revisando papeleta: " + new Date());
            Cursor cursor = papeletaSQL.GetPapeletas();
            PrecargaPapeletaDTO lecturaDTO = null;
            if (cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    lecturaDTO = new PrecargaPapeletaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    lecturaDTO.setIdOrdenCompraExpedidor(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompraExpedidor")));
                    lecturaDTO.setIdOrdenCompraPorteador(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompraPorteador")));
                    lecturaDTO.setIdProveedorPorteador(cursor.getInt(
                            cursor.getColumnIndex("IdProveedorPorteador")));
                    lecturaDTO.setIdProveedorExpedidor(cursor.getInt(
                            cursor.getColumnIndex("IdProveedorExpedidor")));
                    lecturaDTO.setFecha(cursor.getString(
                            cursor.getColumnIndex("Fecha")));
                    lecturaDTO.setFechaEmbarque(cursor.getString(
                            cursor.getColumnIndex("FechaEmbarque")));
                    lecturaDTO.setNumeroEmbarque(cursor.getString(
                            cursor.getColumnIndex("NumeroEmbarque")));
                    lecturaDTO.setPlacasTractor(cursor.getString(
                            cursor.getColumnIndex("PlacasTractor")));
                    lecturaDTO.setNombreOperador(cursor.getString(
                            cursor.getColumnIndex("NombreOperador")));
                    lecturaDTO.setProducto(cursor.getString(
                            cursor.getColumnIndex("Producto")));
                    lecturaDTO.setNumeroTanque(cursor.getString(
                            cursor.getColumnIndex("NumeroTanque")));
                    lecturaDTO.setPresionTanque(cursor.getDouble(
                            cursor.getColumnIndex("PresionTanque")));
                    lecturaDTO.setCapacidadTanque(cursor.getDouble(
                            cursor.getColumnIndex("CapacidadTanque")));
                    lecturaDTO.setPorcentajeTanque(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeTanque")));
                    lecturaDTO.setMasa(cursor.getDouble(
                            cursor.getColumnIndex("Masa")));
                    lecturaDTO.setSello(cursor.getString(
                            cursor.getColumnIndex("Sello")));
                    lecturaDTO.setValorCarga(cursor.getDouble(
                            cursor.getColumnIndex("ValorCarga")));
                    lecturaDTO.setNombreResponsable(cursor.getString(
                            cursor.getColumnIndex("NombreResponsable")));
                    lecturaDTO.setPorcentajeMedidor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidor")));
                    lecturaDTO.setNombreTipoMedidorTractor(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorTractor")));
                    lecturaDTO.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    lecturaDTO.setCantidadFotosTractor(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosTractor")));

                    Cursor cantidad = papeletaSQL.GetRecordByCalveUnica(lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        String iuri = cantidad.getString(cursor.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cursor.getString(cursor.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", lecturaDTO.getClaveOperacion());
                    registrado = RegistrarPapeleta(lecturaDTO);
                    if (registrado){
                        papeletaSQL.Eliminar(lecturaDTO.getClaveOperacion());
                        papeletaSQL.EliminarImagenes(lecturaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (papeletaSQL.GetPapeletas().getCount()==0);
    }

    private boolean RegistrarPapeleta(PrecargaPapeletaDTO lecturaDTO){
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
        Call<RespuestaPapeletaDTO> call = restClient.postPapeleta(lecturaDTO,token,
                "application/json");
        call.enqueue(new Callback<RespuestaPapeletaDTO>() {
            @Override
            public void onResponse(Call<RespuestaPapeletaDTO> call,
                                   Response<RespuestaPapeletaDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
            }

            @Override
            public void onFailure(Call<RespuestaPapeletaDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+lecturaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
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
