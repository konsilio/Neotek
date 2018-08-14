package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.io.ByteArrayOutputStream;
import java.net.URI;
import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class CameraDescargaActivity extends AppCompatActivity implements CameraDescargaView{
    public static final int REQUEST_ID_MULTIPLE_PERMISSIONS = 1;
    private static final int CAMERA_REQUEST = 2;

    public static boolean fotoTomada;

    public LinearLayout layoutTitle;
    public LinearLayout layoutCameraButton;
    public LinearLayout layoutNitidez;
    public ImageView imageViewFoto;
    public Uri imageUri;
    public String imageurl;
    public PrecargaPapeletaDTO papeletaDTO;
    public IniciarDescargaDTO iniciarDescarga;
    public FinalizarDescargaDTO finalizarDescarga;
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public String tipoMedidor;
    public int cantidadFotos;
    public boolean almacen;
    public TextView textViewTitulo;
    public TextView textViewMensaje;

    ProgressDialog progressDialog;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera);

        textViewTitulo = (TextView) findViewById(R.id.textTitulo);
        textViewMensaje = (TextView) findViewById(R.id.textIndicaciones);

        Bundle extras = getIntent().getExtras();

        fotoTomada=false;
        if(extras != null) {
            if (extras.getBoolean("EsPapeleta")) {
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                tipoMedidor = extras.getString("TipoMedidor");
                if(tipoMedidor.equals("Magnatel")){
                    cantidadFotos =1;
                    textViewTitulo.setText("Fotografia Magnatel - Tractor");
                }
                else if (tipoMedidor.equals("Rotogate")){
                    cantidadFotos=2;
                    textViewTitulo.setText("Fotografia Rotogate - Tractor");
                }
                papeleta=true;
                iniciar=false;
                finalizar=false;
                textViewMensaje.setText(R.string.mensaje_primera_foto);
            }
            else if(extras.getBoolean("EsDescargaIniciar")){
                Log.w("CAMARA","DescargaIniciar");
                iniciarDescarga = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                if(extras.getBoolean("Almacen")){
                    if(iniciarDescarga.getIdTipoMedidorAlmacen()==0){
                        cantidadFotos =1;
                        Log.w("CAMARA","Almacen"+cantidadFotos);
                        textViewTitulo.setText("Fotografia Magnatel - Almacen");
                    } else if (iniciarDescarga.getIdTipoMedidorAlmacen()==1){
                        Log.w("CAMARA","Almacen"+cantidadFotos);
                        textViewTitulo.setText("Fotografia Rotogate - Almacen");
                        cantidadFotos =2;
                    }
                }else if(!extras.getBoolean("Almacen")){
                    if(iniciarDescarga.getIdTipoMedidorTractor()==0){
                        Log.w("CAMARA","Tractor"+cantidadFotos);
                        textViewTitulo.setText("Fotografia Magnatel - Tractor");
                        cantidadFotos =1;
                    } else if (iniciarDescarga.getIdTipoMedidorTractor()==1){
                        Log.w("CAMARA","Tractor"+cantidadFotos);
                        textViewTitulo.setText("Fotografia Rotogate - Tractor");
                        cantidadFotos =2;
                    }
                }
                iniciar=extras.getBoolean("EsDescargaIniciar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
            }
            else if(extras.getBoolean("EsDescargaFinalizar")){
                finalizarDescarga = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                if(extras.getBoolean("Almacen")){
                    if(finalizarDescarga.getIdTipoMedidorAlmacen()==0){
                        cantidadFotos =1;
                        Log.w("CAMARA","Almacen"+cantidadFotos);
                    } else if (finalizarDescarga.getIdTipoMedidorAlmacen()==1){
                        cantidadFotos =2;
                        Log.w("CAMARA","Almacen"+cantidadFotos);
                    }
                }else if(!extras.getBoolean("Almacen")){
                    if(finalizarDescarga.getIdTipoMedidorTractor()==0){
                        cantidadFotos =1;
                        Log.w("CAMARA","Tractor"+cantidadFotos);
                    } else if (finalizarDescarga.getIdTipoMedidorTractor()==1){
                        cantidadFotos =2;
                        Log.w("CAMARA","Tractor"+cantidadFotos);
                    }
                }
                finalizar= extras.getBoolean("EsDescargaFinalizar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
            }

        }
        layoutCameraButton = (LinearLayout) findViewById(R.id.layout_photo_button);
        layoutNitidez = (LinearLayout) findViewById(R.id.layout_photo_nitida);
        layoutTitle = (LinearLayout) findViewById(R.id.layout_title);
        imageViewFoto = (ImageView) findViewById(R.id.image_view_foto);

        if(fotoTomada==true){
            layoutTitle.setVisibility(View.GONE);
            layoutCameraButton.setVisibility(View.GONE);
            layoutNitidez.setVisibility(View.VISIBLE);
            textViewMensaje.setVisibility(View.GONE);
        }else{
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.GONE);
            textViewMensaje.setVisibility(View.VISIBLE);
        }

        final Button buttonFoto = (Button) findViewById(R.id.button_foto);
        buttonFoto.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

                Log.w("Prueba","prueba"+permissions(permissionList));

                if (permissions(permissionList)) {

                    openCameraIntent();
                }
            }
        });

        final Button buttonRetomarFoto =(Button) findViewById(R.id.button_foto_incorrecta);
        buttonRetomarFoto.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

                Log.w("Prueba","prueba"+permissions(permissionList));

                if (permissions(permissionList)) {

                    openCameraIntent();
                }
            }
        });

        final Button buttonFotoCorrecta =(Button) findViewById(R.id.button_foto_correcta);
        buttonFotoCorrecta.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                Log.w("BOTON","finalizar"+cantidadFotos);
                checarboton();
            }
        });


    }
    public void checarboton(){
        Log.w("Boton","finalizar"+cantidadFotos+finalizar+almacen);
        if(cantidadFotos!=1){
            try {
                if(papeleta) {
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(iniciar){

                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(finalizar){
                    Log.w("Boton","finalizar"+cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                }
            }catch(Exception ex){

            }
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.GONE);
            textViewMensaje.setVisibility(View.VISIBLE);
            textViewMensaje.setText(R.string.mensaje_segunda_foto);
            cantidadFotos--;
            Log.w("Boton","finalizar"+cantidadFotos);
        }else {
            try {
                if(papeleta) {
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    //processImages();
                    startActivity();
                }else if(iniciar&&!almacen){
                    Log.w("Boton","TractorIniciar"+cantidadFotos);
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivityPorcentaje();
                }else if(finalizar&&!almacen){
                    Log.w("Boton","TractorFinalizar"+cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivityPorcentaje();
                }else if(iniciar&&almacen){
                    Log.w("Boton","AlmacenIniciar"+cantidadFotos);
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //processImages();
                    startActivity();
                }else if(finalizar&&almacen){
                    Log.w("Boton","AlmacenFinalizar"+cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //processImages();
                    startActivity();
                }
            }catch(Exception ex){

            }

        }
    }

    public void processImages(){
        startActivity();
      /*  if(papeleta && papeletaDTO!=null){
            for(int i =0; i<papeletaDTO.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(papeletaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                    papeletaDTO.getImagenes().add(bs.toByteArray());
                    Log.w("Imagenes"+i,""+uri.toString());
                    hideProgress();
                }catch (Exception e){

                }
            }
        }
        else if(iniciar && iniciarDescarga!=null){
            for(int i =0; i<iniciarDescarga.getImagenesURI().size();i++){
                try{
                    Uri uri = Uri.parse(iniciarDescarga.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                    iniciarDescarga.getImagenes().add(bs.toByteArray());
                    Log.w("Imagenes"+i,""+uri.toString());
                    hideProgress();
                }catch (Exception e){

                }
            }
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
                    hideProgress();
                }catch (Exception e){

                }
            }
        }*/
    }


    private void openCameraIntent() {

        ContentValues values = new ContentValues();
        values.put(MediaStore.Images.Media.TITLE, "New Picture");
        values.put(MediaStore.Images.Media.DESCRIPTION, "From your Camera");
        imageUri = getContentResolver().insert(
                MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        intent.putExtra(MediaStore.EXTRA_OUTPUT, imageUri);
        startActivityForResult(intent, CAMERA_REQUEST);

        /*Intent pictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult(pictureIntent, CAMERA_REQUEST);*/

    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {

        if (requestCode == CAMERA_REQUEST && resultCode == RESULT_OK) {
            try {
                Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), imageUri);
                imageViewFoto.setImageBitmap(bitmap);
                imageurl = getRealPathFromURI(imageUri);
                fotoTomada=true;
                layoutTitle.setVisibility(View.GONE);
                layoutCameraButton.setVisibility(View.GONE);
                layoutNitidez.setVisibility(View.VISIBLE);
                textViewMensaje.setVisibility(View.GONE);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private boolean permissions(List<String> listPermissionsNeeded) {

        if (!listPermissionsNeeded.isEmpty()) {
            ActivityCompat.requestPermissions(this, listPermissionsNeeded.toArray
                    (new String[listPermissionsNeeded.size()]), REQUEST_ID_MULTIPLE_PERMISSIONS);
            return true;
        }
        return false;
    }

    public String getRealPathFromURI(Uri contentUri) {
        String[] proj = { MediaStore.Images.Media.DATA };
        Cursor cursor = managedQuery(contentUri, proj, null, null, null);
        int column_index = cursor
                .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }

    public void startActivityPorcentaje(){
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        if(iniciar){
            intent.putExtra("IniciarDescarga", iniciarDescarga);
        }else if(finalizar){
            intent.putExtra("FinalizarDescarga",finalizarDescarga);
        }
        intent.putExtra("EsPapeleta",false);
        intent.putExtra("EsDescargaFinalizar",finalizar);
        intent.putExtra("EsDescargaIniciar",iniciar);
        intent.putExtra("Almacen",true);
        startActivity(intent);
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), SubirImagenesActivity.class);
        if(iniciar){

            intent.putExtra("IniciarDescarga", iniciarDescarga);
        }else if(finalizar){
            Log.w("START","objeto");
            intent.putExtra("FinalizarDescarga",finalizarDescarga);
        }else if(papeleta){
            intent.putExtra("Papeleta",papeletaDTO);
        }
        Log.w("START","bool");
        intent.putExtra("EsPapeleta",papeleta);
        intent.putExtra("EsDescargaFinalizar",finalizar);
        intent.putExtra("EsDescargaIniciar",iniciar);
        intent.putExtra("Almacen",true);
        Log.w("START","fin");
        startActivity(intent);
    }
}
