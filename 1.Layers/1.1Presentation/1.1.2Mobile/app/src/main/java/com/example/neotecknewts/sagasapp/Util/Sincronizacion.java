package com.example.neotecknewts.sagasapp.Util;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;

import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.Util.Sincronizaciones.Papeleta;

import java.util.ArrayList;
import java.util.List;

public class Sincronizacion {
    private Context context;
    private Activity activity;
    public List<String> mensajes;
    private ProgressDialog progressDialog;
    private AlertDialog.Builder dialog;

    public  Sincronizacion(Context context,Activity activity){
        this.context = context;
        this.activity = activity;
        this.mensajes = new ArrayList<>();
        this.dialog = new AlertDialog.Builder(activity, R.style.AlertDialog);
        this.progressDialog = new ProgressDialog(activity,R.style.AlertDialog);
    }
    //region Sincronizar
    public void Sincronizar(){
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(context.getString(R.string.message_cargando));
        progressDialog.setCancelable(false);
        progressDialog.show();
        //region Sincronizar papeletas
        Papeleta papeleta = new Papeleta(context,activity,new PapeletaSQL(context));
        boolean EnvioPapeleta = papeleta.SincronizarPapeletas();
        mensajes.addAll(papeleta.mensajes);
        //endregion
        progressDialog.hide();
    }
    //endregion

    public void mostrarDialogoDetalle(){
        this.dialog.setTitle("Listo");
        StringBuilder detalleDialogos = new StringBuilder();
        for (String mensaje:
             mensajes) {
            detalleDialogos.append(mensaje).append("\n");
        }
        this.dialog.setMessage(detalleDialogos.toString());
        this.dialog.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                dialogInterface.dismiss());
        this.dialog.create().show();
    }
    public void mostrarDialogoExito(){
        this.dialog.setTitle("Listo");
        this.dialog.setMessage("Se na sincronizado los datos");
        this.dialog.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                dialogInterface.dismiss());
        this.dialog.create().show();
    }
}
