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
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

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
    public ImageView IVZCameraLecturaActivityImagen;

    public boolean EsLecturaInicial,EsLecturaFinal,EsFotoP5000,fotoTomada,
            EsLecturaInicialPipa,EsLecturaFinalPipa;
    public String imageurl;
    public LecturaDTO lecturaDTO;
    public LecturaPipaDTO lecturaPipaDTO;
    public RecargaDTO recargaDTO;
    public AutoconsumoDTO autoconsumoDTO;
    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsAutoconsumoEstacionInicial,EsAutoconsumoEstacionFinal;

    public Uri imageUri;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera_lectura);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaFinal = b.getBoolean("EsLecturaFinal",false);
            EsLecturaInicial =  b.getBoolean("EsLecturaInicial",false);
            EsLecturaInicialPipa = b.getBoolean("EsLecturaInicialPipa",false);
            EsLecturaFinalPipa = (boolean) b.getBoolean("EsLecturaFinalPipa",false);
            EsFotoP5000 =  b.getBoolean("EsFotoP5000",false);
            lecturaDTO = (LecturaDTO) b.getSerializable("lecturaDTO");
            lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
            EsRecargaEstacionInicial = b.getBoolean("EsRecargaEstacionInicial",
                    false);
            EsRecargaEstacionFinal = b.getBoolean("EsRecargaEstacionFinal",
                    false);
            EsPrimeraLectura = b.getBoolean("EsPrimeraLectura",false);
            recargaDTO = (RecargaDTO) b.getSerializable("recargaDTO");
            EsAutoconsumoEstacionFinal = b.getBoolean("EsAutoconsumoEstacionFinal",false);
            EsAutoconsumoEstacionInicial = b.getBoolean("EsAutoconsumoEstacionInicial",false);
            autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");

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
        }else if (EsLecturaFinal){
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    " - "+lecturaDTO.getNombreEstacionCarburacion());
        }else if (EsLecturaInicialPipa){
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    " - "+lecturaPipaDTO.getNombrePipa());
        }else if (EsLecturaFinalPipa){
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    " - "+lecturaPipaDTO.getNombrePipa());
        }
        if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            if(EsPrimeraLectura){
                TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)
                +" - " +getString(R.string.Pipa));
            }else{
                TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)
                        +" - " +getString(R.string.Estacion));
            }
        }
        if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                    +" - " +getString(R.string.Estacion));
        }

        BtnCameraLecturaTomarFoto.setOnClickListener(v -> {
            List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

            Log.w("Prueba","prueba"+permissions(permissionList));

            if (permissions(permissionList)) {

                openCameraIntent();
            }
        });

        BtnCameraLecturaFotoNitidaNo.setOnClickListener(v -> openCameraIntent());

        BtnCameraLecturaFotoNitidaSi.setOnClickListener(v -> verificarBoton());
    }

    private void verificarBoton() {
        if(EsFotoP5000 && EsLecturaInicial || EsLecturaFinal){
            try {
                lecturaDTO.getImagenes().add(imageurl);
                lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                //lecturaDTO.setImagenP5000(imageurl);
                //lecturaDTO.setImagenP5000URI(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("lecturaDTO",lecturaDTO);
                intent.putExtra("EsLecturaInicial",EsLecturaInicial);
                intent.putExtra("EsLecturaFinal",EsLecturaFinal);
                startActivity(intent);
            } catch (URISyntaxException e) {
                e.printStackTrace();
            }

        }else if (EsFotoP5000 && (EsLecturaInicialPipa || EsLecturaFinalPipa)){
            try {
                lecturaPipaDTO.getImagenes().add(imageurl);
                lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                //lecturaPipaDTO.setImagenP5000(imageurl);
                //lecturaPipaDTO.setImagenP5000URI(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("lecturaPipaDTO",lecturaPipaDTO);
                intent.putExtra("EsLecturaInicial",EsLecturaInicial);
                intent.putExtra("EsLecturaFinal",EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa",EsLecturaInicialPipa);
                intent.putExtra("EsLecturaFinalPipa",EsLecturaFinalPipa);
                startActivity(intent);
            } catch (URISyntaxException e) {
                e.printStackTrace();
            }
        }else if(EsRecargaEstacionInicial|| EsRecargaEstacionFinal){
            try {
                recargaDTO.getImagenes().add(imageurl);
                recargaDTO.getImagenesUri().add(new URI(imageUri.toString()));
                if (EsPrimeraLectura) {
                    Intent intent = new Intent(CameraLecturaActivity.this,
                        LecturaP5000Activity.class);
                    intent.putExtra("EsRecargaEstacionInicial",EsRecargaEstacionInicial);
                    intent.putExtra("EsRecargaEstacionFinal",EsRecargaEstacionFinal);
                    intent.putExtra("recargaDTO",recargaDTO);
                    intent.putExtra("EsPrimeraLectura",false);
                    startActivity(intent);
                }else{
                    //Ir a la lectura del medidor
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            CapturaPorcentajeActivity.class);
                    intent.putExtra("EsRecargaEstacionInicial",EsRecargaEstacionInicial);
                    intent.putExtra("EsRecargaEstacionFinal",EsRecargaEstacionFinal);
                    intent.putExtra("recargaDTO",recargaDTO);
                    intent.putExtra("EsPrimeraLectura",EsPrimeraLectura);
                    startActivity(intent);

                }
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            try {
                autoconsumoDTO.getImagenes().add(imageurl);
                autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsAutoconsumoEstacionInicial",EsAutoconsumoEstacionInicial);
                intent.putExtra("EsAutoconsumoEstacionFinal",EsAutoconsumoEstacionFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }catch (URISyntaxException e){
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
