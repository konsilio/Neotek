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

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.io.ByteArrayOutputStream;
import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class CameraDescargaActivity extends AppCompatActivity {
    public static final int REQUEST_ID_MULTIPLE_PERMISSIONS = 1;
    private static final int CAMERA_REQUEST = 2;

    public static boolean fotoTomada;

    public LinearLayout layoutTitle;
    public LinearLayout layoutCameraButton;
    public LinearLayout layoutNitidez;
    public ImageView imageViewFoto;
    public Uri imageUri;
    public String imageurl;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera);

        Bundle extras = getIntent().getExtras();

        layoutCameraButton = (LinearLayout) findViewById(R.id.layout_photo_button);
        layoutNitidez = (LinearLayout) findViewById(R.id.layout_photo_nitida);
        layoutTitle = (LinearLayout) findViewById(R.id.layout_title);
        imageViewFoto = (ImageView) findViewById(R.id.image_view_foto);

        if(fotoTomada==true){
            layoutTitle.setVisibility(View.GONE);
            layoutCameraButton.setVisibility(View.GONE);
            layoutNitidez.setVisibility(View.VISIBLE);
        }else{
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.GONE);
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
                //startActivity();
            }
        });


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
                ByteArrayOutputStream bs = new ByteArrayOutputStream();
                bitmap.compress(Bitmap.CompressFormat.PNG, 50, bs);
                Log.w("SIZE",bs.size()+"");
                fotoTomada=true;
                layoutTitle.setVisibility(View.GONE);
                layoutCameraButton.setVisibility(View.GONE);
                layoutNitidez.setVisibility(View.VISIBLE);
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
