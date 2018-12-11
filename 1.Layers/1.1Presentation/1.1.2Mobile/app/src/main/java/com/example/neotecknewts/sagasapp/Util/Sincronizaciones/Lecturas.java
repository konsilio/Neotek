package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.content.Context;
import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Sincronizacion;

import java.util.Date;
import java.util.List;

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
    public boolean SincronizarLecturas(){
        Log.w("Consulta de Lecturas","Consulta de lecturas"+new Date());
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

                lecturas.moveToNext();
            }
        }
        return  true;
    }

    public boolean Registro(LecturaDTO lecturaDTO){
        return false;
    }
}
