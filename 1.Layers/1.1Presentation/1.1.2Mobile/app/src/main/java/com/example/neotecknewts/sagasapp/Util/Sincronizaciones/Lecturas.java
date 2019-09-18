package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.content.Context;
import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaLecturaInicialDTO;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Sincronizacion;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class Lecturas {
    private SAGASSql db;
    private Sincronizacion sincronizacion;
    private boolean respuesta_servicio;
    private String token;
    public List<String> mensajes;

    public Lecturas(Context context ,SAGASSql db, Sincronizacion sincronizacion,String token){
        this.db = db;
        this.sincronizacion = sincronizacion;
        this.token = token;
    }
    public boolean SincronizarLecturasEstacion(){
        Log.w("Consulta de Lecturas","Consulta de lecturas estación"+new Date());
        Cursor lecturas = db.GetLecturasIniciales();
        lecturas.moveToFirst();
        Log.w("Total lecturas",String.valueOf(lecturas.getCount()));
        if(lecturas.getCount()>0){
            while (!lecturas.isAfterLast()){
                LecturaDTO dto = new LecturaDTO();
                dto.setClaveProceso(
                        lecturas.getString(
                                lecturas.getColumnIndex("ClaveProceso")
                        )
                );
                dto.setIdTipoMedidor(lecturas.getInt(
                        lecturas.getColumnIndex("IdTipoMedidor")));
                dto.setNombreTipoMedidor(lecturas.getString(lecturas.getColumnIndex(
                        "NombreTipoMedidor")));
                dto.setCantidadFotografias(lecturas.getInt(lecturas.getColumnIndex(
                        "CantidadFotografiasMedidor")));
                dto.setNombreEstacionCarburacion(lecturas.getString(lecturas.getColumnIndex(
                        "NombreEstacionCarburacion")));
                dto.setIdEstacionCarburacion(lecturas.getInt(lecturas.getColumnIndex(
                        "IdEstacionCarburacion")));
                dto.setCantidadP5000(lecturas.getInt(lecturas.getColumnIndex(
                        "CantidadP5000")));
                dto.setPorcentajeMedidor(lecturas.getDouble(lecturas.getColumnIndex(
                        "PorcentajeMedidor")));
                Cursor imagenes = db.GetLecturaImagenesByClaveUnica(
                        dto.getClaveProceso()
                );
                imagenes.moveToFirst();
                while (!imagenes.isAfterLast()){
                    String iuri = imagenes.getString(imagenes.getColumnIndex("Url"));
                    try {
                        dto.getImagenesURI().add(new URI(iuri));
                        dto.getImagenes().add(
                                imagenes.getString(imagenes.getColumnIndex("Imagen"))
                        );
                    } catch (URISyntaxException e) {
                        e.printStackTrace();
                    }
                    imagenes.moveToNext();
                }
                if(Registro(dto)){
                    db.EliminarLectura(dto.getClaveProceso());
                    db.EliminarLecturaImagenes(dto.getClaveProceso());
                }
                lecturas.moveToNext();
            }
        }
        return  db.GetLecturasIniciales().getCount()==0;
    }

    public boolean Registro(LecturaDTO dto){
        Log.w("Registro","Registrando en servicio "+dto.getClaveProceso());

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaLecturaInicialDTO> call = restClient.postTomaLecturaInicial(dto, token, "application/json");
        call.enqueue(new Callback<RespuestaLecturaInicialDTO>() {
            @Override
            public void onResponse(Call<RespuestaLecturaInicialDTO> call,
                                   Response<RespuestaLecturaInicialDTO> response) {
                respuesta_servicio = call.isExecuted() && response.isSuccessful();
                /*Log.e("lectura"+dto.getClaveProceso(),
                        String.valueOf(response.isSuccessful()));*/
            }

            @Override
            public void onFailure(Call<RespuestaLecturaInicialDTO> call, Throwable t) {
                respuesta_servicio = false;
            }
        });
        Log.w("Registro","Registro en servicio "+dto.getClaveProceso()+": "+
                respuesta_servicio);
        return respuesta_servicio;
    }
}
