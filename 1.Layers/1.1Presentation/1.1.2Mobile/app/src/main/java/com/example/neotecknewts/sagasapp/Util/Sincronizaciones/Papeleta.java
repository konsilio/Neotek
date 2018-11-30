package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.app.Activity;
import android.content.Context;

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;

import java.util.List;

public class Papeleta {
    Context context;
    Activity activity;
    PrecargaPapeletaDTO dto;
    PapeletaSQL sql;
    public List<String> mensajes;

    /**
     * Permite realizar la sincronización de los datos de la
     * papeleta
     * @param context Contexto de la aplicación
     * @param activity Activity en la que se trabaja
     * @param sql Base de datos en SQLITE
     */
    public Papeleta(Context context,Activity activity,PapeletaSQL sql){
        this.context = context;
        this.activity = activity;
        this.sql = sql;
    }
    public boolean SincronizarPapeletas(){
        return false;
    }
}
