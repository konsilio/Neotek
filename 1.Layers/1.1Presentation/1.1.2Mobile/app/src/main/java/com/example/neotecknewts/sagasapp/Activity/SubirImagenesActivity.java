package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
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
import android.util.Base64;
import android.util.Log;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletasImagenesSQL;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.io.ByteArrayOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by neotecknewts on 14/08/18.
 */

public class SubirImagenesActivity extends AppCompatActivity implements SubirImagenesView {

    //variables de la vista
    public TextView textView;

    //objetos
    public PrecargaPapeletaDTO papeletaDTO;
    public IniciarDescargaDTO iniciarDescarga;
    public FinalizarDescargaDTO finalizarDescarga;
    //banderas para indicar el objeto a utlizar
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;

    public SubirImagenesPresenter presenter;
    public ProgressDialog progressDialog;
    public Session session;
    public PapeletaSQL papeletaSQL;
    public PapeletasImagenesSQL papeletasImagenesSQL;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_subir_imagenes);

        //se obtiene el objeto de la vista
        textView = (TextView) findViewById(R.id.textTitulo);

        presenter = new SubirImagenesPresenterImpl(this);
        session = new Session(getApplicationContext());

        textView.setText(R.string.cargando_imagenes_inicio);
        Bundle extras = getIntent().getExtras();
        //se obtienen los objetos del activity anterior
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
        papeletaSQL = new PapeletaSQL(this.getApplicationContext());
        papeletasImagenesSQL = new PapeletasImagenesSQL(this.getApplicationContext());
        //se ejecuta la tarea asincrona para procesar las imagenes
        new AsyncTaskRunner().execute();
        //processImage();

        //hideProgress();

    }

    //este metodo toma todas las imagenes de la lista dependiendo de cual objeto se esta usando, las transofrma a byte array y posteriormente las pasa a base 64
    private void processImage(){
        if(papeleta && papeletaDTO!=null){
            for(int i =0; i<papeletaDTO.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(papeletaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    papeletaDTO.getImagenes().add(image.trim());

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
                    bitmap.compress(Bitmap.CompressFormat.PNG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    iniciarDescarga.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                    Log.w("Imagenes"+i,"Base64: "+image);
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
                    bitmap.compress(Bitmap.CompressFormat.PNG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    finalizarDescarga.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){

                }
            }
        }

    }
    public void comprimr(){

    }
    //se muestra un cuadro de dialogo con un mensaje
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

    //se inicia el activity del menu para poder hacer alguna otra accion
    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivity(intent);
    }

    /**
     * Permite mostrar el dialogo de progreso en la interfaz
     * @param mensaje Id del mensaje en string a mostrar
     */
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    /**
     * Oculta el mensaje de progressdialog
     */
    @Override
    public void hideProgress() {
        progressDialog.hide();
    }

    //tarea asincrona que ejecuta el procesado de las imagenes
    private class AsyncTaskRunner extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            processImage();
            return null;
        }


        @Override
        protected void onPostExecute(Void result) {
            presenter.registrarPapeleta(papeletaDTO,session.getToken(),papeletaSQL,getApplicationContext());
            textView.setText(R.string.cargando_imagenes_fin);
            //presenter.onSuccessRegistrarPapeleta();
            //showDialogAceptar("Operación Exitosa","Los datos se han guardado exitosamente");

        }


        @Override
        protected void onPreExecute() {
        }

    }

}

