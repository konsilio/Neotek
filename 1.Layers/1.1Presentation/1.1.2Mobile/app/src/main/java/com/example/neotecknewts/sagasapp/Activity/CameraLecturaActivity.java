package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.annotation.RequiresApi;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;
import com.jsibbold.zoomage.ZoomageView;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.List;

public class CameraLecturaActivity extends AppCompatActivity {
    public static final int REQUEST_ID_MULTIPLE_PERMISSIONS = 1;
    private static final int CAMERA_REQUEST = 2;

    public LinearLayout LLCameraLecturaActivityTitulo,LLCameraLecturaActivityImagen;
    public TableLayout TLCameraLecturaActivityTomarFoto,TLCameraLecturaActivityFotoNitida;
    public Button BtnCameraLecturaTomarFoto,BtnCameraLecturaFotoNitidaSi,BtnCameraLecturaFotoNitidaNo;
    public TextView TVCameraLecturaActivityTitulo,TVCameraLecturaActivityFotoEstacion;
    public ZoomageView IVZCameraLecturaActivityImagen;

    public boolean EsLecturaInicial,EsLecturaFinal,EsFotoP5000,fotoTomada;
    public String imageurl;
    public LecturaDTO lecturaDTO;

    public Uri imageUri;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera_lectura);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaFinal = (boolean) b.get("EsLecturaFinal");
            EsLecturaInicial = (boolean) b.get("EsLecturaInicial");
            EsFotoP5000 = (boolean) b.get("EsFotoP5000");
            lecturaDTO = (LecturaDTO) b.getSerializable("lecturaDTO");
        }

        LLCameraLecturaActivityTitulo = findViewById(R.id.LLCameraLecturaActivityTitulo);
        LLCameraLecturaActivityImagen = findViewById(R.id.LLCameraLecturaActivityImagen);
        TLCameraLecturaActivityTomarFoto = findViewById(R.id.TLCameraLecturaActivityTomarFoto);
        TLCameraLecturaActivityFotoNitida = findViewById(R.id.TLCameraLecturaActivityFotoNitida);
        BtnCameraLecturaTomarFoto = findViewById(R.id.BtnCameraLecturaTomarFoto);
        BtnCameraLecturaFotoNitidaNo = findViewById(R.id.BtnCameraLecturaFotoNitidaNo);
        BtnCameraLecturaFotoNitidaSi = findViewById(R.id.BtnCameraLecturaFotoNitidaSi);
        TVCameraLecturaActivityTitulo = findViewById(R.id.TVCameraLecturaActivityTitulo);
        TVCameraLecturaActivityFotoEstacion = findViewById(R.id.TVCameraLecturaActivityFotoEstacion);
        IVZCameraLecturaActivityImagen = findViewById(R.id.IVZCameraLecturaActivityImagen);
        if(EsLecturaInicial){
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    " - "+lecturaDTO.getNombreEstacionCarburacion());
        }

        BtnCameraLecturaTomarFoto.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

                Log.w("Prueba","prueba"+permissions(permissionList));

                if (permissions(permissionList)) {

                    openCameraIntent();
                }
            }
        });

        BtnCameraLecturaFotoNitidaNo.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.M)
            @Override
            public void onClick(View v) {
                openCameraIntent();
            }
        });

        BtnCameraLecturaFotoNitidaSi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                verificarBoton();
            }
        });
    }

    private void verificarBoton() {
        if(EsFotoP5000 && EsLecturaInicial){
            try {
                lecturaDTO.setImagenP500(imageurl);
                lecturaDTO.setImagenP500URI(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("lecturaDTO",lecturaDTO);
                intent.putExtra("EsLecturaInicial",EsLecturaInicial);
                intent.putExtra("EsLecturaFinal",EsLecturaFinal);
                startActivity(intent);
            } catch (URISyntaxException e) {
                e.printStackTrace();
            }

        }
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {

        if(keyCode == KeyEvent.KEYCODE_BACK){
            AlertDialog.Builder builder = CrearDialogo(R.string.title_alert_message,getString(R.string.message_goback_diabled));
            builder.setPositiveButton(R.string.label_si, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            MenuActivity.class);
                    intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    startActivity(intent);
                    finish();
                }
            });
            builder.setNegativeButton(R.string.label_no, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            builder.show();
        }
        return false;

    }

    private AlertDialog.Builder CrearDialogo(int Titulo, String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(Titulo);
        builder.setMessage(mensaje);

        return builder;
    }

    private boolean permissions(List<String> listPermissionsNeeded) {

        if (!listPermissionsNeeded.isEmpty()) {
            ActivityCompat.requestPermissions(this, listPermissionsNeeded.toArray
                    (new String[listPermissionsNeeded.size()]), REQUEST_ID_MULTIPLE_PERMISSIONS);
            return true;
        }
        return false;
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


    }
    @RequiresApi(api = Build.VERSION_CODES.M)
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {

        if (requestCode == CAMERA_REQUEST && resultCode == RESULT_OK) {
            try {
                //se crea un bitmap a partir de la uri de la imagen
                Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), imageUri);
                //se muestra la foto
                IVZCameraLecturaActivityImagen.setImageBitmap(bitmap);
                imageurl = getRealPathFromURI(imageUri);
                //como la foto ya fue tomada se muestra el layout de nitidez
                fotoTomada=true;
                TLCameraLecturaActivityTomarFoto.setVisibility(View.GONE);
                LLCameraLecturaActivityImagen.setVisibility(View.VISIBLE);
                TLCameraLecturaActivityFotoNitida.setVisibility(View.VISIBLE);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

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
