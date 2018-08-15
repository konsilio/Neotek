package com.example.neotecknewts.sagasapp.Activity;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.io.ByteArrayOutputStream;
import java.util.ArrayList;

/**
 * Created by neotecknewts on 14/08/18.
 */

public class SubirImagenesActivity extends AppCompatActivity {

    public TextView textView;
    public PrecargaPapeletaDTO papeletaDTO;
    public IniciarDescargaDTO iniciarDescarga;
    public FinalizarDescargaDTO finalizarDescarga;
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_subir_imagenes);

        textView = (TextView) findViewById(R.id.textTitulo);

        textView.setText(R.string.cargando_imagenes_inicio);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            if(extras.getBoolean("EsPapeleta")){
                Log.w("SUBIR","EsPapeleta");
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                papeleta=true;
                iniciar=false;
                finalizar=false;
            }
            else if(extras.getBoolean("EsDescargaIniciar")){
                Log.w("SUBIR","EsDescargaIniciar");
                iniciarDescarga = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                papeleta=false;
                iniciar=true;
                finalizar=false;
            }
            else if(extras.getBoolean("EsDescargaFinalizar")){
                Log.w("SUBIR","EsDescargaFinalizar");
                finalizarDescarga = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                papeleta=false;
                iniciar=false;
                finalizar=true;
            }

        }

        new AsyncTaskRunner().execute();

    }

    private void processImage(){
        if(papeleta && papeletaDTO!=null){
            for(int i =0; i<papeletaDTO.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(papeletaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                    papeletaDTO.getImagenes().add(bs.toByteArray());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){

                }
            }
        }
        else if(iniciar && iniciarDescarga!=null){
            Log.w("SUBIR","EsDescargaIniciar2");
            for(int i =0; i<iniciarDescarga.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(iniciarDescarga.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                    iniciarDescarga.getImagenes().add(bs.toByteArray());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){

                }
            }
            Log.w("SUBIR","EsDescargaIniciar2fin");
        }
        else if(finalizar && finalizarDescarga!=null){
            for(int i =0; i<finalizarDescarga.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(finalizarDescarga.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                    finalizarDescarga.getImagenes().add(bs.toByteArray());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){

                }
            }
        }

    }

    private void showDialogAceptar(String titulo, String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setTitle(titulo);
        builder1.setMessage(mensaje);
        builder1.setCancelable(false);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        startActivity();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivity(intent);
    }

    private class AsyncTaskRunner extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            processImage();
            return null;
        }


        @Override
        protected void onPostExecute(Void result) {
            textView.setText(R.string.cargando_imagenes_fin);
            showDialogAceptar("Operación Exitosa","Los datos se han guardado exitosamente");
        }


        @Override
        protected void onPreExecute() {
        }

    }

}

