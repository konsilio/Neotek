package com.example.neotecknewts.sagasapp.Util;

import android.annotation.SuppressLint;
import android.database.Cursor;
import android.database.CursorIndexOutOfBoundsException;
import android.net.Uri;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaAnticipoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaCorteDto;
import com.example.neotecknewts.sagasapp.Model.RespuestaFinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.RespuestaRecargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaServicioDisponibleDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
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
import java.text.ParseException;
import java.text.SimpleDateFormat;
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
    //region Variables estaticas
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
    public static final String RecargaEstacion ="RecargaEstacion";
    public static final String VENTA = "Venta";
    public static final String Autoconsumo = "Autoconsumo";
    public static final String Calibracion = "Calibracion";
    public static final String Traspaso = "Traspaso";
    public static final String Anticipo = "Anticipo";
    public static final String CorteDeCaja = "CorteDeCaja";
    public static final String RecargaPipa = "RecargaPipa";
    //endregion
    //region Variables privadas
    private  String token;
    private boolean completo ;
    private SAGASSql sagasSql;
    private PapeletaSQL papeletaSQL;
    private IniciarDescargaSQL iniciarDescargaSQL;
    private FinalizarDescargaSQL finalizarDescargaSQL;
    private boolean EstaDisponible;
    boolean _registrado;
    //endregion
    //region Constructores
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
    //endregion
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
                case RecargaEstacion:
                    completo = RecargaEstacion();
                    break;
                case RecargaPipa:
                    completo = RecargaPipa();
                    break;
                case VENTA:
                    completo = PuntoVenta();
                    break;
                case Autoconsumo:
                    this.completo = Autoconsumo();
                    break;
                case Calibracion:
                    completo = Calibracion();
                    break;
                case Traspaso:
                    completo = Traspaso();
                    break;
                case Anticipo:
                    completo = Anticipo();
                    break;
                case CorteDeCaja:
                    completo = Corte();
            }
        };

        ScheduledExecutorService timer = Executors.newSingleThreadScheduledExecutor();
        ScheduledFuture scheduledFuture = timer.
                scheduleAtFixedRate(myTask, 10, 10, TimeUnit.SECONDS);
        if(this.completo) {
            scheduledFuture.cancel(false);
        }
    }

    //region Recarga pipa
    private boolean RecargaPipa(){
        if(ServicioDisponible()){
            boolean registrado;
            Log.w("Iniciando", "Revisando recarga estación: " + new Date());
            Cursor cursor = sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION);
            RecargaDTO recargaDTO = null;
            if(cursor.moveToFirst()){
                while (!cursor.isAfterLast()){
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
                    recargaDTO.setFechaApliacacion(
                            cursor.getString(
                                    cursor.getColumnIndex("FechaAplicacion")
                            )
                    );
                    boolean esInicial = cursor.getInt(
                            cursor.getColumnIndex("EsInicial")
                    )
                            > 0;
                    String tipo = cursor.getString(
                            cursor.getColumnIndex("EsTipo"));
                    Cursor imagenes = sagasSql.GetImagenesRecarga(recargaDTO.getClaveOperacion());
                    imagenes.moveToFirst();
                    while (!imagenes.isAfterLast()){
                        try {
                            recargaDTO.getImagenes().add(
                                    imagenes.getString(
                                            imagenes.getColumnIndex("Imagen")
                                    )
                            );

                            recargaDTO.getImagenesUri().add(
                                    new URI(imagenes.getString(
                                            imagenes.getColumnIndex("url")
                                    ))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        imagenes.moveToNext();
                    }

                    Log.w("ClaveProceso", recargaDTO.getClaveOperacion());
                    registrado = RegistrarRecarga(recargaDTO,tipo,esInicial);
                    if (registrado){
                        sagasSql.EliminarRecarga(recargaDTO.getClaveOperacion());
                        sagasSql.EliminarImagenesRecarga(
                                recargaDTO.getClaveOperacion());
                    }

                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_PIPA).getCount()==0);
    }
    //endregion

    //region Corte de caja
    private boolean Corte() {
        if(ServicioDisponible()){
            Log.w("Iniciando",new Date()+"Revisando cortes");
            Cursor cursor = sagasSql.GetCortes();
            @SuppressLint("SimpleDateFormat") SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
            if(cursor.moveToFirst()){
                CorteDTO corteDTO;
                while (!cursor.isAfterLast()){
                    try {
                        corteDTO = new CorteDTO();
                        corteDTO.setClaveOperacion(
                                cursor.getString(
                                    cursor.getColumnIndex("ClaveOperacion")
                                )
                        );
                        corteDTO.setFecha(
                                cursor.getString(
                                                cursor.getColumnIndex("Fecha")
                                        )
                        );

                        corteDTO.setIdEstacion(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdEstacion")
                                )
                        );
                        corteDTO.setHora(
                                cursor.getString(
                                        cursor.getColumnIndex("Hora")
                                )
                        );
                        //values.put("FechaCorte",corteDTO.getFecha().toString());
                        if(Registrar(corteDTO,token)){
                            sagasSql.EliminarCorte(corteDTO.getClaveOperacion());
                        }

                    }catch (Exception ex){
                        ex.printStackTrace();
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetCortes().getCount()==0);
    }

    private  boolean Registrar(CorteDTO corteDTO,String token){
        Gson gsons = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofits =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gsons))
                .build();
        RestClient restClient = retrofits.create(RestClient.class);


        Call<RespuestaCorteDto> call = restClient.
                postCorte(corteDTO,token,"application/json");
        call.enqueue(new Callback<RespuestaCorteDto>() {
            @Override
            public void onResponse(Call<RespuestaCorteDto> call,
                                   Response<RespuestaCorteDto> response) {
                RespuestaCorteDto data = response.body();
                if(response.isSuccessful() && data.isExito()) {
                    _registrado = call.isExecuted() && response.isSuccessful();
                }else {
                    _registrado = false;
                }
                Log.e("Corte"+corteDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaCorteDto> call, Throwable t) {
                _registrado = false;
            }
        });
        return _registrado;
    }
    //endregion

    //region Anticipos
    private boolean Anticipo() {
        if(ServicioDisponible()){
            Log.w("Inciando",new Date()+" Revisando los anticipos");
            Cursor cursor = sagasSql.GetAnticipos();
            @SuppressLint("SimpleDateFormat") SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
            if(cursor.moveToFirst()){
                AnticiposDTO anticiposDTO;
                while (!cursor.isAfterLast()){
                    anticiposDTO = new AnticiposDTO();
                    try{
                        anticiposDTO.setTotal(cursor.getDouble(
                                cursor.getColumnIndex("Total")
                        ));
                        anticiposDTO.setNombreEstacion(
                                cursor.getString(
                                        cursor.getColumnIndex("NombreEstacion")
                                )
                        );
                        anticiposDTO.setClaveOperacion(
                                cursor.getString(
                                        cursor.getColumnIndex("ClaveOperacion")
                                )
                        );
                        anticiposDTO.setHora(
                                cursor.getString(
                                        cursor.getColumnIndex("Hora")
                                )
                        );
                        anticiposDTO.setFecha(

                                cursor.getString(
                                        cursor.getColumnIndex("Fecha")
                                )
                        );
                        anticiposDTO.setAnticipar(
                                cursor.getDouble(
                                        cursor.getColumnIndex("Anticipo")
                                )
                        );
                        anticiposDTO.setIdEstacion(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdEstacion")
                                )
                        );
                        anticiposDTO.setIdCAlmacen(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdCAlmacen")
                                )
                        );
                        if(Registrar(anticiposDTO,token)){
                            sagasSql.EliminarAnticipo(anticiposDTO.getClaveOperacion());
                        }
                    }catch (Exception ex){
                        ex.printStackTrace();
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetAnticipos().getCount()==0);
    }

    private boolean Registrar(AnticiposDTO anticiposDTO, String token) {
        Gson gsons = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofits =  new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gsons))
                .build();
        RestClient restClient = retrofits.create(RestClient.class);


        Call<RespuestaAnticipoDTO> call = restClient.
                postAnticipo(anticiposDTO,token,"application/json");
        call.enqueue(new Callback<RespuestaAnticipoDTO>() {
            @Override
            public void onResponse(Call<RespuestaAnticipoDTO> call,
                                   Response<RespuestaAnticipoDTO> response) {
                RespuestaAnticipoDTO data = response.body();
                if(response.isSuccessful() && data.isExito()) {
                    _registrado = call.isExecuted() && response.isSuccessful();
                }else {
                    _registrado = false;
                }
                Log.e("Anticipo"+anticiposDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaAnticipoDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        return _registrado;
    }
    //endregion

    //region Traspasos
    private boolean Traspaso() {
        if(ServicioDisponible()){
            Log.w("Inciando",new Date()+" Revisando los traspasos");
            Cursor cursor = sagasSql.GetTraspasos();
            @SuppressLint("SimpleDateFormat") SimpleDateFormat format = new
                    SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
            if(cursor.moveToFirst()){
                TraspasoDTO traspasoDTO;
                while (!cursor.isAfterLast()){
                    traspasoDTO = new TraspasoDTO();
                    try {
                        traspasoDTO.setFecha(
                                format.parse(
                                    cursor.getString(
                                            cursor.getColumnIndex("Fecha")
                                    )
                                )
                        );
                        traspasoDTO.setCantidadDeFotos(
                                cursor.getInt(
                                        cursor.getColumnIndex("CantidadDeFotos")
                                )
                        );
                        traspasoDTO.setClaveOperacion(
                                cursor.getString(
                                        cursor.getColumnIndex("ClaveOperacion")
                                )
                        );
                        traspasoDTO.setIdCAlmacenGasEntrada(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdCAlmacenGasEntrada")
                                )
                        );
                        traspasoDTO.setIdCAlmacenGasSalida(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdCAlmacenGasSalida")
                                )
                        );
                        traspasoDTO.setIdTipoMedidorSalida(
                                cursor.getInt(
                                        cursor.getColumnIndex("IdTipoMedidorSalida")
                                )
                        );
                        traspasoDTO.setNombreMedidor(
                                cursor.getString(
                                        cursor.getColumnIndex("NombreMedidor")
                                )
                        );
                        traspasoDTO.setP5000Entrada(
                                cursor.getInt(
                                        cursor.getColumnIndex("P5000Entrada")
                                )
                        );
                        traspasoDTO.setP5000Salida(
                                cursor.getInt(
                                        cursor.getColumnIndex("P5000Salida")
                                )
                        );
                        traspasoDTO.setPorcentajeSalida(
                                cursor.getDouble(
                                        cursor.getColumnIndex("PorcentajeSalida")
                                )
                        );
                        traspasoDTO.setFechaAplicacion(
                                cursor.getString(
                                        cursor.getColumnIndex("FechaAplicacion")
                                )
                        );
                        Cursor imagenes = sagasSql.GetImagenesTraspaso(traspasoDTO.getClaveOperacion());
                        imagenes.moveToFirst();
                        while (!imagenes.isAfterLast()){
                            traspasoDTO.getImagenes().add(
                                    cursor.getString(
                                            cursor.getColumnIndex("Imagen")
                                    )
                            );
                            traspasoDTO.getImagenesUri().add(
                                    new URI(
                                            cursor.getString(
                                                    cursor.getColumnIndex("Url")
                                            )
                                    )
                            );
                            imagenes.moveToNext();
                        }
                    } catch (ParseException e) {
                        e.printStackTrace();
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }
                    boolean esFinal = cursor.getInt(cursor.getColumnIndex("EsFinal")) > 0;
                    String tipo = cursor.getString(cursor.getColumnIndex("Tipo"));
                    if(Registrar(traspasoDTO,tipo,esFinal)) {
                        sagasSql.EliminarTraspasos(traspasoDTO.getClaveOperacion());
                        sagasSql.EliminarImagenesTraspasos(traspasoDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (this.sagasSql.GetTraspasos().getCount()==0);
    }

    private  boolean Registrar(TraspasoDTO dto,String tipo,boolean esFinal){
        if(ServicioDisponible()){
            Log.w("Iniciando",new Date()+"Envio del traspaso: "+dto.getClaveOperacion());
            Gson gson = new GsonBuilder()
                    .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                    .create();

            Retrofit retrofit = new Retrofit.Builder()
                    .baseUrl(Constantes.BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create(gson))
                    .build();

            RestClient restClient = retrofit.create(RestClient.class);
            Call<RespuestaTraspasoDTO> call = restClient.postTraspaso(
                    dto,
                    /*tipo.equals(SAGASSql.TIPO_TRASPASO_ESTACION),
                    tipo.equals(SAGASSql.TIPO_TRASPASO_PIPA),*/
                    esFinal,
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
                        _registrado = call.isExecuted() && response.isSuccessful();
                    } else {
                        _registrado = false;
                    }
                    Log.e("Traspaso "+dto.getClaveOperacion(),
                            String.valueOf(response.isSuccessful()));
                }

                @Override
                public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                    Log.e("error", t.toString());
                    _registrado = false;
                }
            });
        }
        return _registrado;
    }
    //endregion

    //region Calibracion

    /**
     * Permite realizar en el envio de las calibraciónes pendientes en la base de datos,
     * retornara un valor de tipo boolean si el envio de los datos fue exitoso
     * @return boolean que determina si todos lo datos fueron enviados correctamente
     */
    private boolean Calibracion() {
        if(ServicioDisponible()){
            Log.w("Iniciando",new Date()+" Revisado de las calibraciónes");
            Cursor cursor = sagasSql.GetAutoconsumos();
            if(cursor.moveToFirst()){
                CalibracionDTO dto;
                while (!cursor.isAfterLast()){
                    dto = new CalibracionDTO();
                    dto.setCantidadFotografias(
                            cursor.getInt(
                                    cursor.getColumnIndex("CantidadFotografias")
                            )
                    );
                    dto.setNombreMedidor(
                            cursor.getString(
                                    cursor.getColumnIndex("NombreMedidor")
                            )
                    );
                    dto.setIdTipoMedidor(
                            cursor.getInt(
                                    cursor.getColumnIndex("IdTipoMedidor")
                            )
                    );
                    dto.setNombreCAlmacenGas(
                            cursor.getString(
                                    cursor.getColumnIndex("NombreCAlmacenGas")
                            )
                    );
                    dto.setFechaRegistro(new Date(
                            cursor.getString(
                                    cursor.getColumnIndex("FechaRegistro")
                            ))
                    );
                    dto.setFechaAplicacion(new Date(
                            cursor.getString(
                                    cursor.getColumnIndex("FechaAplicacion")
                            )
                            )
                    );

                    dto.setP5000(
                            cursor.getInt(
                            cursor.getColumnIndex("P5000")
                            )
                    );

                    dto.setPorcentaje(
                            cursor.getDouble(
                                    cursor.getColumnIndex("Porcentaje")
                            )
                    );

                    dto.setPorcentajeCalibracion(
                            cursor.getDouble(
                                    cursor.getColumnIndex("PorcentajeCalibracion")
                            )
                    );

                    dto.setIdDestinoCalibracion(
                            cursor.getInt(
                                    cursor.getColumnIndex("IdDestinoCalibracion")
                            )
                    );

                    Cursor imagenes =sagasSql.GetFotografiasCalibracion(dto.getClaveOperacion());
                    imagenes.moveToFirst();
                    while (!imagenes.isAfterLast()){
                        try {
                            dto.getImagenes().add(
                                imagenes.getString(
                                        imagenes.getColumnIndex("Imagen")
                                )
                            );

                            dto.getImagenesUri().add(new URI(
                               imagenes.getString(
                                       imagenes.getColumnIndex("Url")
                               )
                            ));
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        imagenes.moveToNext();
                    }
                    boolean esFinal = (
                            cursor.getInt(
                              cursor.getColumnIndex("EsFinal")
                            )>0
                            );
                    String tipo = cursor.getString(
                            cursor.getColumnIndex("Tipo")
                    );
                    Log.w("Enviando calibración",dto.getClaveOperacion());
                    if(Registrar(dto,token,esFinal,tipo)){
                        sagasSql.EliminarCalibracion(dto.getClaveOperacion());
                        sagasSql.EliminarImagenesCalibracion(dto.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }

        }
        return (this.sagasSql.GetCalibraciones().getCount()==0);

    }

    /**
     * Permite realizar el envio de los datos al api , tomara como parametros un objeto
     * {@link CalibracionDTO} con los datos a enviar y un string con el token del usuario,
     * tras finalizar retornara un boolean que reprecenta si el registro fue exitoso
     * @param dto Objeto {@link CalibracionDTO} que contiene los datos de la calibración
     * @param token String que reprecenta el token del usuario
     * @param esFinal Determina si la calibración es final
     * @param tipo Stirng que determina el tipo de calibración
     * @return boolean que determina si el registro fue exitoso
     */
    private boolean Registrar(CalibracionDTO dto,String token,boolean esFinal, String tipo){
        Log.w("Registro","Registrando en servicio "+dto.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaTraspasoDTO> call = null;

        restClient.postCalibracion(dto,
                /*tipo.equals(SAGASSql.TIPO_CALIBRACION_ESTACION),
                tipo.equals(SAGASSql.TIPO_CALIBRACION_PIPA),*/
                esFinal,
                token,
                "application/json"
                );
        call.enqueue(new Callback<RespuestaTraspasoDTO>() {
            @Override
            public void onResponse(Call<RespuestaTraspasoDTO> call,
                                   Response<RespuestaTraspasoDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
                Log.w("Listo","Registro "+dto.getClaveOperacion());
            }

            @Override
            public void onFailure(Call<RespuestaTraspasoDTO> call, Throwable t) {
                _registrado = false;
                Log.e("Error","Error registro "+dto.getClaveOperacion());
            }
        });
        Log.w("Registro","Registro en servicio "+dto.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }
    //endregion

    //region Autoconsumo

    /**
     * Autoconsumo
     * Permite realizar el registro de los autoconsumos que estan en local al servicio api
     * retornara un boolean con respecto al registro de estos
     * @return boolean con el resultados de los registros
     */
    private boolean Autoconsumo() {
        if(ServicioDisponible()){
            Log.w("Iniciando",new Date()+" Revisado de los autoconsumos");
            Cursor cursor = sagasSql.GetAutoconsumos();
            AutoconsumoDTO dto;
            if(cursor.moveToFirst()) {
                while (!cursor.isAfterLast()) {
                    dto = new AutoconsumoDTO();
                    /*Coloco los valors del autoconsumo*/
                    dto.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion"))
                    );
                    dto.setCantidadFotos(cursor.getColumnIndex(
                            cursor.getColumnName(
                                    cursor.getColumnIndex("CantidadFotos")
                            )
                    ));
                    dto.setIdCAlmacenGasEntrada(
                            cursor.getInt(
                                    cursor.getColumnIndex("IdCAlmacenGasEntrada")
                            )
                    );
                    dto.setIdCAlmacenGasSalida(
                            cursor.getInt(
                                    cursor.getColumnIndex("IdCAlmacenGasSalida")
                            )
                    );
                    dto.setNombreTipoMedidor(
                            cursor.getString(
                                    cursor.getColumnIndex("NombreTipoMedidor")
                            )
                    );
                    dto.setP5000Salida(
                            cursor.getInt(
                                    cursor.getColumnIndex("P5000Salida")
                            )
                    );
                    dto.setPorcentajeMedidor(
                            cursor.getDouble(
                                    cursor.getColumnIndex("PorcentajeMedidor")
                            )
                    );
                    dto.setFechaAplicacion(
                            cursor.getString(
                                    cursor.getColumnIndex("FechaAplicacion")
                            )
                    );
                    dto.setIdTipoMedidor(
                            cursor.getInt(
                                    cursor.getColumnIndex("IdTipoMedidor")
                            )
                    );
                    /*Coloco las imagenes */
                    Cursor imagenes = sagasSql.GetImagenesAutoconsumo(dto.getClaveOperacion());
                    imagenes.moveToFirst();
                    while (!imagenes.isAfterLast()){
                        dto.getImagenes().add(
                                imagenes.getString(
                                        imagenes.getColumnIndex("Imagen")
                                )
                        );
                        try {
                            URI uri = new URI(imagenes.getString(
                                    imagenes.getColumnIndex("Url")
                            ));
                            dto.getImagenesURI().add(uri);
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        imagenes.moveToNext();
                    }

                    if(Registrar(dto,
                            cursor.getString( cursor.getColumnIndex("Tipo")),
                            cursor.getInt(cursor.getColumnIndex("EsFinal"))>0
                    )) {
                        sagasSql.EliminarAutoconsumo(dto.getClaveOperacion());
                        sagasSql.EliminarImagenesAutoconsumo(dto.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (this.sagasSql.GetAutoconsumos().getCount()==0);
    }

    /**
     * Realiza el registro del objeto de {@link AutoconsumoDTO} al servicio api, retornar
     * un valor boolean del resultado del registro
     * @param dto Objeto {@link AutoconsumoDTO} con los datos del autoconsumo
     * @param Tipo Epecifica el tipo de autoconsumo
     * @param esFinal Especifica si es final
     * @return boolean con el resultado del registro en la api
     */
    private boolean Registrar(AutoconsumoDTO dto,String Tipo,boolean esFinal) {
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaRecargaDTO> call = restClient.postAutorconsumo(
                dto,
                /*Tipo.equals(SAGASSql.TIPO_AUTOCONSUMO_ESTACION_CARBURACION),
                Tipo.equals(SAGASSql.TIPO_AUTOCONSUMO_INVENTARIO_GENERAL),
                Tipo.equals(SAGASSql.TIPO_AUTOCONSUMO_PIPAS),*/
                esFinal,
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
                    _registrado = call.isExecuted() && response.isSuccessful();
                } else {
                    _registrado = false;
                }
                Log.e("Autoconsumo"+dto.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                Log.e("error", t.toString());
               _registrado = false;
            }
        });
        return _registrado;
    }
    //endregion

    //region Punto de venta
    private boolean PuntoVenta() {
        if(ServicioDisponible()){
            Log.w("Iniciando","Revisando las ventas "+ new Date());
            Cursor cursor = sagasSql.GetVentas();
            VentaDTO ventaDTO;
            boolean esCamioneta,
                    esEstacion,
                    esPipa;
            if(cursor.moveToFirst()){
                while (!cursor.isAfterLast()){
                    ventaDTO = new VentaDTO();
                    /*coloco los valores de la venta*/
                    ventaDTO.setFolioVenta(cursor.getString(cursor.getColumnIndex("FolioVenta")));
                    ventaDTO.setIdCliente(cursor.getInt(cursor.getColumnIndex("IdCliente")));
                    ventaDTO.setSubtotal(cursor.getDouble(cursor.getColumnIndex("Subtotal")));
                    ventaDTO.setIva(cursor.getDouble(cursor.getColumnIndex("Iva")));
                    ventaDTO.setTotal(cursor.getDouble(cursor.getColumnIndex("Total")));
                    ventaDTO.setFactura(cursor.getInt(
                            cursor.getColumnIndex("Factura"))>0
                    );
                    ventaDTO.setCredito(cursor.getInt(
                            cursor.getColumnIndex("Credito"))>0
                    );
                    ventaDTO.setEfectivo(cursor.getDouble(cursor.getColumnIndex("Efectivo")));
                    ventaDTO.setFecha(cursor.getString(cursor.getColumnIndex("Fecha")));
                    ventaDTO.setHora(cursor.getString(cursor.getColumnIndex("Hora")));
                    ventaDTO.setCambio(cursor.getDouble(cursor.getColumnIndex("Cambio")));
                    ventaDTO.setSinNumero(
                            cursor.getInt(cursor.getColumnIndex("SinNumero")) > 0
                    );
                    esCamioneta = cursor.getInt(cursor.getColumnIndex("EsCamioneta"))>0;
                    esEstacion = cursor.getInt(cursor.getColumnIndex("EsEstacion"))>0;
                    esPipa = cursor.getInt(cursor.getColumnIndex("EsPipa"))>0;
                    //Obtengo y coloco el concepto de venta
                    Cursor concepto = sagasSql.GetVentaConcepto(ventaDTO.getFolioVenta());
                    if(concepto.moveToFirst()) {
                        while (!concepto.isAfterLast()) {
                            ConceptoDTO conceptoDTO= new ConceptoDTO();

                            conceptoDTO.setIdTipoGas(concepto.getInt(
                                    concepto.getColumnIndex("IdTipoGas"))
                            );
                            conceptoDTO.setCantidad(concepto.getInt(
                                    concepto.getColumnIndex("Cantidad"))
                            );
                            conceptoDTO.setPUnitario(concepto.getDouble(
                                    concepto.getColumnIndex("PUnitario"))
                            );
                            conceptoDTO.setDescuento(concepto.getDouble(
                                    concepto.getColumnIndex("Descuento")
                            ));
                            conceptoDTO.setSubtotal(concepto.getDouble(
                                    concepto.getColumnIndex("Subtotal")
                            ));
                            conceptoDTO.setIdCategoria(concepto.getInt(
                                    concepto.getColumnIndex("IdCategoria")));
                            conceptoDTO.setIdLinea(concepto.getInt(
                                    concepto.getColumnIndex("IdLinea"))
                            );
                            conceptoDTO.setIdProducto(concepto.getInt(
                                    concepto.getColumnIndex("IdProducto"))
                            );
                            conceptoDTO.setConcepto(concepto.getString(
                                    concepto.getColumnIndex("Concepto"))
                            );
                            concepto.moveToNext();
                        }
                    }
                    registrarVenta(ventaDTO,esCamioneta,esEstacion,esPipa);
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetVentas().getCount()==0);
    }

    private boolean registrarVenta(VentaDTO ventaDTO,boolean esCamioneta,boolean esEstacion,boolean
                                esPipa) {

        Log.w("Registro","Registrando en servicio de ventas: "+ventaDTO.getFolioVenta());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaPuntoVenta> call = restClient.pagar(
                ventaDTO,
                /*esCamioneta,
                esEstacion,
                esPipa,*/
                token,
                "application/json"
        );
        call.enqueue(new Callback<RespuestaPuntoVenta>() {
            @Override
            public void onResponse(Call<RespuestaPuntoVenta> call,
                                   Response<RespuestaPuntoVenta> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
                Log.e("Corte"+ventaDTO.getFolioVenta(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaPuntoVenta> call, Throwable t) {
                _registrado = false;
            }
        });
        if(_registrado){
            sagasSql.EliminarVenta(ventaDTO.getFolioVenta());
            sagasSql.EliminarVentaConcepto(ventaDTO.getFolioVenta());
        }
        Log.w("Registro","Registro en servicio venta"+ventaDTO.getFolioVenta()+": "+
                _registrado);
        return _registrado;
    }
    //endregion

    //region Recarga Estacion

    /**
     * Permite realizar el registro de la recarga de la estación,
     * retornara un booleano que reprecenta si se completaron todos los registros
     * @return boolean que reprecenta si se completaron todos los envios
     */
    private boolean RecargaEstacion(){
        if(ServicioDisponible()){
            boolean registrado;
            Log.w("Iniciando", "Revisando recarga estación: " + new Date());
            Cursor cursor = sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION);
            RecargaDTO recargaDTO = null;
            if(cursor.moveToFirst()){
                while (!cursor.isAfterLast()){
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
                    recargaDTO.setFechaApliacacion(
                            cursor.getString(
                                    cursor.getColumnIndex("FechaAplicacion")
                            )
                    );
                    boolean esInicial = cursor.getInt(
                            cursor.getColumnIndex("EsInicial")
                    )
                            > 0;
                    String tipo = cursor.getString(
                            cursor.getColumnIndex("Tipo"));
                    Cursor imagenes = sagasSql.GetImagenesRecarga(recargaDTO.getClaveOperacion());
                    imagenes.moveToFirst();
                    while (!imagenes.isAfterLast()){
                        try {
                            recargaDTO.getImagenes().add(
                                    imagenes.getString(
                                      imagenes.getColumnIndex("Imagen")
                                    )
                            );

                            recargaDTO.getImagenesUri().add(
                                    new URI(imagenes.getString(
                                            imagenes.getColumnIndex("url")
                                    ))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        imagenes.moveToNext();
                    }

                    Log.w("ClaveProceso", recargaDTO.getClaveOperacion());
                    registrado = RegistrarRecarga(recargaDTO,tipo,esInicial);
                    if (registrado){
                        sagasSql.EliminarRecarga(recargaDTO.getClaveOperacion());
                        sagasSql.EliminarImagenesRecarga(
                                recargaDTO.getClaveOperacion());
                    }

                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_ESTACION_CARBURACION).getCount()==0);
    }

    /**
     * RegistrarRecarga
     * Permite realizar el registro de la recarga
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga
     * @return boolean con la respuesta del registro
     */
    private boolean RegistrarRecarga(RecargaDTO recargaDTO,String tipo,boolean esInicial) {
        Log.w("Registro","Registrando en servicio "+recargaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaRecargaDTO> call = null;
        if(esInicial) {
            restClient.postRecargaInicial(
                    recargaDTO, token, "application/json");
        }else{
            restClient.postRecargaFinal(
                    recargaDTO,token,"application/json"
            );
        }
        call.enqueue(new Callback<RespuestaRecargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaRecargaDTO> call,
                                   Response<RespuestaRecargaDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
                Log.e("Corte"+recargaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+recargaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }

    //endregion

    //region Recarga camioneta

    /**
     * RecargaCamioneta
     * Permite realizart el registro de la recarga de la camioneta desde la base de datos
     * local , convirtiendo a {@link RecargaDTO} los datos
     * @return boolean Determina si la recarga fu enviada al api
     */
    private boolean RecargaCamioneta(){
        boolean registrado;
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
                    Cursor cantidad = sagasSql.GetCilindrosRecarga(recargaDTO.getClaveOperacion());
                    cantidad.moveToFirst();

                    while (!cantidad.isAfterLast()) {
                        CilindrosDTO row = new CilindrosDTO();
                        row.setCantidad(cursor.getInt(cursor.getColumnIndex(
                                "Cantidad")));
                        row.setCilindroKg(cursor.getString(cursor.getColumnIndex(
                                "CilindroKg")));
                        row.setIdCilindro(cursor.getInt(cursor.getColumnIndex(
                                "IdCilindro")));
                        recargaDTO.getCilindros().add(row);
                        cantidad.moveToNext();
                    }

                    Log.w("ClaveProceso", recargaDTO.getClaveOperacion());
                    registrado = RegistrarRecargaCamioneta(recargaDTO);
                    if (registrado){
                        sagasSql.EliminarLecturaInicialCamioneta(recargaDTO.getClaveOperacion());
                        sagasSql.EliminarCilindrosLecturaInicialCamioneta(
                                recargaDTO.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
            }
        }
        return (sagasSql.GetRecargas(SAGASSql.TIPO_RECARGA_CAMIONETA).getCount()==0);
    }

    /**
     * RegistrarRecargaCamioneta
     * permite realizar el registro de la recarga al api , toma como parametro
     * el objeto de tipo {@link RecargaDTO}
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los datos de la recarga
     * @return boolean Con la respuesta del registro en la api
     */
    private boolean RegistrarRecargaCamioneta(RecargaDTO recargaDTO){
        Log.w("Registro","Registrando en servicio "+recargaDTO.getClaveOperacion());
        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(Constantes.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
        RestClient restClient = retrofit.create(RestClient.class);
        Call<RespuestaRecargaDTO> call = restClient.postRecarga(
                recargaDTO,token,"application/json");
        call.enqueue(new Callback<RespuestaRecargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaRecargaDTO> call,
                                   Response<RespuestaRecargaDTO> response) {
                _registrado = call.isExecuted() && response.isSuccessful();
                Log.e("Corte"+recargaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<RespuestaRecargaDTO> call, Throwable t) {
                _registrado = false;
            }
        });
        Log.w("Registro","Registro en servicio "+recargaDTO.getClaveOperacion()+": "+
                _registrado);
        return _registrado;
    }
    //endregion

    //region Lectura inicial camioneta
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
                    lecturaDTO.setIdCamioneta(cursor.getInt(cursor.getColumnIndex(
                            "IdCamioneta")));
                    lecturaDTO.setFechaAplicacion(cursor.getString(
                            cursor.getColumnIndex("FechaAplicacion")
                    ));

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
                Log.e("lectura"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura final camioneta
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
                    lecturaDTO.setIdCamioneta(cursor.getInt(cursor.getColumnIndex(
                            "IdCamioneta")));
                    lecturaDTO.setFechaAplicacion(cursor.getString(
                            cursor.getColumnIndex("FechaAplicacion")
                    ));
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
                Log.e("lectura final"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura final almacen

    /**
     * LecturaFinalAlmacen
     * Permite hacer el envio de los datos de la lectura final del almacen en local
     * al api
     * @return boolean con la respuesta del registro
     */
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

    /**
     * Realiza el envio y registro de los datos del dto al api
     * @param lecturaDTO Objeto Objeto {@link LecturaAlmacenDTO} con los datos de la lectura de almacen
     * @return boolean con la respuesta del registro
     */
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
                Log.e("lectura"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura inicial almacen

    /**
     * Premite realizar el registro de las lecturas iniciales de almacen en el telefono
     * al api, retornara un boleano con el resultado del registro
     * @return boolean con el resultado del registro
     */
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

    /**
     * RegistrarLecturaInicialAlmacen
     * Realiza el envio y registro en el api del dto de la lectura del almacen
     * @param lecturaDTO Objeto {@link LecturaAlmacenDTO} con los datos de la lectura de almacen
     * @return boolean con la respuesta del registro
     */
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
                Log.e("lectura"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura inicial pipa

    /**
     * LecturaInicialPipa
     * Registra le lecturas iniciales de la pipa en el api rest, retorna un boolean con la
     * respuesta del registro
     * @return boolean con la respuesta del registro de la lectura
     */
    private boolean LecturaInicialPipa() {
        boolean registrado;
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

    /**
     * RegistrarLecturaInicialPipa
     * Permite realizer el registro en el api de la lectura incial de la pipa
     * retornara un boolean con los datos del dto
     * @param lecturaDTO Objeto de tipo {@link LecturaPipaDTO} con los datos de la
     *                   lectura incial de la pipa
     * @return boolean con la respuesta del servidor
     */
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
                Log.e("lectura"+lecturaDTO.getClaveProceso(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura final pipa
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

    /**
     * RegistrarLecturaFinalPipa
     * Realiza el envio y registro en el api de la lectura final de la pipa
     * retornara un boolean con la respuesta del servicio
     * @param lecturaDTO Objeto de tipo {@link LecturaPipaDTO} con los datos de la
     *                   lectura final de la pipa
     * @return boolean Respuesta del registro en la api
     */
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
                Log.e("Lectura final"+lecturaDTO.getClaveProceso(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura final estación

    /**
     * LecturaFinalizarEstacion
     * Permirte el registro de la lectura final de la estación, convierte los datos
     * almacenados en la base de datos a un Dto de tipo {@link LecturaDTO} , retornara un
     * boleano si los datos ha sido registrados en la api
     * @return boolean con la respuesta del servicio
     */
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

    /**
     * Permite realizar el registro de la lectura final de la
     * estación en el servicio, retornara como parametro un boolean
     * que indica si fue registrado en el api
     * @param lecturaDTO Modelo con los datos de la lectura final
     * @return boolean con la respuesta del registro
     */
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
                Log.e("lectura "+lecturaDTO.getClaveProceso(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Lectura inicial estación

    /**
     * LecturaIniciarEstacion
     * Permite ejecutar el envio de los datos de las lecturas iniciales de estación en
     * el telefono al servicio , consulta los datos y los pasa de {@link Cursor} a
     * {@link LecturaDTO} para su envio al api
     * @return boolean Si todos los datos fueron enviados correctamente
     */
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

    /**
     * RegistrarLecturaInicial
     *
     * Realiza el envio de los datos del Dto de lectura inicial estación  al servicio nuevamente,
     * retornara un valor boleano en caso de que se guardaron correctamente
     * @param lecturaDTO Objeto {@link LecturaDTO} con los datos a enviar
     * @return boolean Con la respuesta del envio
     */
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
                Log.e("lectura"+lecturaDTO.getClaveProceso(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Finalizar descarga
    private boolean FinalizarDescarga() {
        boolean servicio = ServicioDisponible();
        boolean registrado;
        if(servicio) {
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
                    /*lecturaDTO.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));*/
                    lecturaDTO.setFechaDescarga(cursor.getString(
                            cursor.getColumnIndex("FechaDescarga")));
                    /*lecturaDTO.setNombreTipoMedidorTractor(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorTractor")));
                    lecturaDTO.setNombreTipoMedidorAlmacen(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorAlmacen")));*/
                    lecturaDTO.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    lecturaDTO.setIdTipoMedidorAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorAlmacen")));
                    /*lecturaDTO.setCantidadFotosAlmacen(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosAlmacen")));*/
                    /*lecturaDTO.setCantidadFotosTractor(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosTractor")));*/
                    boolean es_prestado = cursor.getInt(
                            cursor.getColumnIndex("TanquePrestado")) > 0;
                    lecturaDTO.setTanquePrestado(es_prestado);
                    lecturaDTO.setPorcentajeMedidorAlmacen(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedirorAlmacen")));
                    lecturaDTO.setPorcentajeMedidorTractor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorTractor")));
                    lecturaDTO.setIdAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdAlmacen")));

                    Cursor cantidad = finalizarDescargaSQL.GetImagenesFinalizarDescargaByClaveOperacion(lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        String iuri = cantidad.getString(cantidad.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cantidad.getString(cantidad.getColumnIndex("Imagen"))
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
                Log.e("FINALIZAR DESCARGA"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Iniciar descarga
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
                        String iuri = cantidad.getString(cantidad.getColumnIndex("Url"));
                        try {
                            lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cantidad.getString(cantidad.getColumnIndex("Imagen"))
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
                Log.e("iniciar descarga"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Papeleta
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

                    Cursor cantidad = papeletaSQL.GetRecordsByCalveUnica(lecturaDTO.getClaveOperacion());
                    cantidad.moveToFirst();
                    while (!cantidad.isAfterLast()) {
                        //String iuri = cantidad.getString(cantidad.getColumnIndex("Imagen"));
                        //try {
                            //lecturaDTO.getImagenesURI().add(new URI(iuri));
                            lecturaDTO.getImagenes().add(
                                    cantidad.getString(cantidad.getColumnIndex("Url"))
                            );
                        //} catch (URISyntaxException e) {
                        //    e.printStackTrace();
                        //}
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
                Log.e("Papeletas"+lecturaDTO.getClaveOperacion(),
                        String.valueOf(response.isSuccessful()));
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
    //endregion

    //region Estatus servicio
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
    //endregion
}
