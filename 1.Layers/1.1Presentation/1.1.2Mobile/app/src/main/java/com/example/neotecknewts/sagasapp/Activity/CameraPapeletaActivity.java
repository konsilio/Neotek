package com.example.neotecknewts.sagasapp.Activity;

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

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Permisos;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.net.URI;
import java.util.Date;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class CameraPapeletaActivity extends AppCompatActivity {
    public static final int REQUEST_ID_MULTIPLE_PERMISSIONS = 1;
    private static final int CAMERA_REQUEST = 2;

    //Como se sabe que de la papeleta solo se captura una imagen este activity es igual a cameraDescarga solo que solo toma una imagen
    public static boolean fotoTomada;

    public LinearLayout layoutTitle;
    public LinearLayout layoutCameraButton;
    public LinearLayout layoutNitidez;
    public ImageView imageViewFoto;
    public Uri imageUri;
    public String imageurl;
    public TextView textViewTitulo;
    public TextView textViewMensaje;
    public Permisos permisos;

    PrecargaPapeletaDTO papeletaDTO;
    public String NombreFoto;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera);
        permisos = new Permisos(CameraPapeletaActivity.this);
        permisos.permisos();
        Bundle extras = getIntent().getExtras();

        if (extras !=null){
            papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
        }
        layoutCameraButton = (LinearLayout) findViewById(R.id.layout_photo_button);
        layoutNitidez = (LinearLayout) findViewById(R.id.layout_photo_nitida);
        layoutTitle = (LinearLayout) findViewById(R.id.layout_title);
        imageViewFoto = (ImageView) findViewById(R.id.image_view_foto);
        textViewTitulo = (TextView) findViewById(R.id.textTitulo);
        textViewMensaje = (TextView) findViewById(R.id.textIndicaciones);

        textViewTitulo.setText(R.string.title_foto_papeleta);
        NombreFoto = "FotoPapeleta|"+new Date();
        fotoTomada = false;
        if(fotoTomada==true){
            layoutTitle.setVisibility(View.GONE);
            layoutCameraButton.setVisibility(View.GONE);
            layoutNitidez.setVisibility(View.VISIBLE);
            textViewMensaje.setVisibility(View.GONE);

        }else{
            textViewMensaje.setVisibility(View.VISIBLE);
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.GONE);
        }

        final Button buttonFoto = (Button) findViewById(R.id.button_foto);
        buttonFoto.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

                Log.w("Prueba","prueba"+permissions(permissionList));

                /*if (permissions(permissionList)== false) {*/

                    openCameraIntent();
                /*}*/
            }
        });

        final Button buttonRetomarFoto =(Button) findViewById(R.id.button_foto_incorrecta);
        buttonRetomarFoto.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

                Log.w("Prueba","prueba"+permissions(permissionList));

                //if (!permissions(permissionList)) {

                    openCameraIntent();
                //}
            }
        });

        final Button buttonFotoCorrecta =(Button) findViewById(R.id.button_foto_correcta);
        buttonFotoCorrecta.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }catch(Exception e){

                }
                startActivity();
            }
        });


    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        intent.putExtra("Papeleta",papeletaDTO);
        intent.putExtra("EsPapeleta",true);
        intent.putExtra("EsDescargaIniciar",false);
        intent.putExtra("EsDescargaFinalizar",false);
        startActivity(intent);
    }


    private void openCameraIntent() {

        ContentValues values = new ContentValues();
        values.put(MediaStore.Images.Media.TITLE, "New Picture"+new Date().toString());
        values.put(MediaStore.Images.Media.DESCRIPTION, "From your Camera");
       // values.put(MediaStore.Images.Media.CONTENT_TYPE,"image/jpeg");
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
                //papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                fotoTomada=true;
                layoutTitle.setVisibility(View.GONE);
                layoutCameraButton.setVisibility(View.GONE);
                layoutNitidez.setVisibility(View.VISIBLE);
                textViewMensaje.setVisibility(View.GONE);
                Log.w("MASA", papeletaDTO.getMasa()+"");
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
}

