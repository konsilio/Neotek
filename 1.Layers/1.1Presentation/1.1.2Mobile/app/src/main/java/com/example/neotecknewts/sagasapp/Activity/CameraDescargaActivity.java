package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.net.URI;
import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class CameraDescargaActivity extends AppCompatActivity implements CameraDescargaView{
    public static final int REQUEST_ID_MULTIPLE_PERMISSIONS = 1;
    private static final int CAMERA_REQUEST = 2;

    //Bandera que indica si la foto ya fue tomada y ense caso hay que mostrar el layout de nitidez
    public static boolean fotoTomada;

    //variables relacionadas con la vista
    public LinearLayout layoutTitle;
    public LinearLayout layoutCameraButton;
    public LinearLayout layoutNitidez;
    public ImageView imageViewFoto;
    public TextView textViewTitulo;
    public TextView textViewMensaje;

    //variables para la imagen
    public Uri imageUri;
    public String imageurl;

    //DTOS
    public PrecargaPapeletaDTO papeletaDTO;
    public IniciarDescargaDTO iniciarDescarga;
    public FinalizarDescargaDTO finalizarDescarga;
    public LecturaDTO lecturaDTO;
    public LecturaPipaDTO lecturaPipaDTO;

    //Banderas para indicar que objeto trabajar
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public int cantidadFotos;
    public boolean almacen;
    public boolean TanquePrestado;
    public boolean EsLecturaInicial,EsLecturaFinal;
    public boolean EsLecturaInicialPipa,EsLecturaFinalPipa;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //Se indica que layout se carga
        setContentView(R.layout.activity_camera);

        //Se inicia obtienen las variables desde la vista
        textViewTitulo = (TextView) findViewById(R.id.textTitulo);
        textViewMensaje = (TextView) findViewById(R.id.textIndicaciones);
        layoutCameraButton = (LinearLayout) findViewById(R.id.layout_photo_button);
        layoutNitidez = (LinearLayout) findViewById(R.id.layout_photo_nitida);
        layoutTitle = (LinearLayout) findViewById(R.id.layout_title);
        imageViewFoto = (ImageView) findViewById(R.id.image_view_foto);

        //se obtienen los datos que provienen del activity anterior
        Bundle extras = getIntent().getExtras();

        fotoTomada=false;
        if(extras != null) {
            //se acomoda la vista en modo Papeleta y se les da valor a las variables realcionadas con papeleta
            if (extras.getBoolean("EsPapeleta")) {
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                cantidadFotos=papeletaDTO.getCantidadFotosTractor();
                textViewTitulo.setText("Fotografia "+papeletaDTO.getNombreTipoMedidorTractor()+" - Tractor");
                papeleta=true;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa= false;
                textViewMensaje.setText(R.string.mensaje_primera_foto);
            }
            //se acomoda la vista en modo Iniciar descarga y se les da valor a las variables realcionadas con Iniciar descarga
            else if(extras.getBoolean("EsDescargaIniciar")){
                Log.w("CAMARA","DescargaIniciar");
                iniciarDescarga = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                if(extras.getBoolean("Almacen")){
                    cantidadFotos = iniciarDescarga.getCantidadFotosAlmacen();
                    textViewTitulo.setText("Fotografia "+iniciarDescarga.getNombreTipoMedidorAlmacen()+" - Almacen");

                }else if(!extras.getBoolean("Almacen")){
                    cantidadFotos = iniciarDescarga.getCantidadFotosTractor();
                    textViewTitulo.setText("Fotografia "+iniciarDescarga.getNombreTipoMedidorTractor()+" - Tractor");

                }
                iniciar=extras.getBoolean("EsDescargaIniciar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
                TanquePrestado = extras.getBoolean("TanquePrestado");
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa= false;
            }
            //se acomoda la vista en modo Finalizar descarga y se les da valor a las variables realcionadas con finalizar descarga
            else if(extras.getBoolean("EsDescargaFinalizar")){
                finalizarDescarga = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                if(extras.getBoolean("Almacen")){
                    cantidadFotos = finalizarDescarga.getCantidadFotosAlmacen();
                    textViewTitulo.setText("Fotografia "+finalizarDescarga.getNombreTipoMedidorAlmacen()+" - Almacen");

                }else if(!extras.getBoolean("Almacen")){
                    cantidadFotos = finalizarDescarga.getCantidadFotosTractor();
                    textViewTitulo.setText("Fotografia "+finalizarDescarga.getNombreTipoMedidorTractor()+" - Tractor");

                }
                finalizar= extras.getBoolean("EsDescargaFinalizar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa= false;
            }else if (extras.getBoolean("EsLecturaInicial") ||
                    extras.getBoolean("EsLecturaFinal")){
                lecturaDTO = (LecturaDTO) extras.getSerializable("lecturaDTO");
                             cantidadFotos = lecturaDTO.getCantidadFotografias();
                textViewTitulo.setText("Fotografia "+lecturaDTO.getNombreTipoMedidor()
                        +" - "+lecturaDTO.getNombreEstacionCarburacion() );
                EsLecturaInicial = extras.getBoolean("EsLecturaInicial");
                EsLecturaFinal = extras.getBoolean("EsLecturaFinal");
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa= false;
            }else if (extras.getBoolean("EsLecturaInicialPipa")||
                    extras.getBoolean("EsLecturaFinalPipa")){
                lecturaPipaDTO = (LecturaPipaDTO) extras.getSerializable("lecturaPipaDTO");
                cantidadFotos = lecturaPipaDTO.getCantidadFotografias();
                textViewTitulo.setText("Fotografia "+lecturaPipaDTO.getTipoMedidor()
                        +" - "+lecturaPipaDTO.getNombrePipa() );
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = extras.getBoolean("EsLecturaInicialPipa");
                EsLecturaFinalPipa= extras.getBoolean("EsLecturaFinalPipa");
            }

        }

        //deacuerdo a si la foto ya fue tomada se muestra o no el layout de la nitidez
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

        //se decalran los onClick de cada boton
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
    //En caso de que la foto sea correcta
    public void checarboton(){
        Log.w("Boton","finalizar"+cantidadFotos+finalizar+almacen);
        //si aun faltan fotos por tomar
        if(cantidadFotos!=1){
            try {
                //se cehca que objeto se esta usando y se agrega la uri de la imagen a su lista
                if(papeleta) {
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(iniciar){

                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(finalizar){
                    Log.w("Boton","finalizar"+cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(EsLecturaInicial){
                    Log.w("Boton lec.Inic.","finalizar"+cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(EsLecturaFinal){
                    Log.w("Boton lec.Final.","finalizar"+cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
                    Log.w("Lectura Pipa","finalizar"+cantidadFotos);
                    lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                }
            }catch(Exception ex){

            }
            //se pone visible el layout para tomar la siguiente fotografia
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.GONE);
            textViewMensaje.setVisibility(View.VISIBLE);
            textViewMensaje.setText(R.string.mensaje_segunda_foto);
            cantidadFotos--;
            Log.w("Boton","finalizar"+cantidadFotos);
            //si ya se termino de tomar las fotos
        }else {
            try {
                if(papeleta) {
                    //se agrega la ultima foto tomada
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    //se inicia el activity que se encargara de procesaar las imagenes
                    startActivity();
                }else if(iniciar&&!almacen){
                    Log.w("Boton","TractorIniciar"+cantidadFotos);
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //si es el medidor del tractor (la variable almacen es falsa por lo que estamos en la fotos del medidor del tractor
                    //se inicia el captivity para capturar el porcentaje del siguiente medidor
                    if(!TanquePrestado)
                        startActivityPorcentaje();
                    else
                        startActivity();
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
                }else if(EsLecturaInicial){
                    Log.w("Boton lec.Inic.","finalizar"+cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                }else if(EsLecturaFinal){
                    Log.w("Boton lec.Final.","finalizar"+cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
                    Log.w("Lectura Pipa","finalizar"+cantidadFotos);
                    lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                }
            }catch(Exception ex){

            }

        }
    }

//metodo que abre la camara
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

    //metodo que se ejecuta al momento de obtener un resultado que proviene de la camara
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {

        if (requestCode == CAMERA_REQUEST && resultCode == RESULT_OK) {
            try {
                //se crea un bitmap a partir de la uri de la imagen
                Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), imageUri);
                //se muestra la foto
                imageViewFoto.setImageBitmap(bitmap);
                imageurl = getRealPathFromURI(imageUri);
                //como la foto ya fue tomada se muestra el layout de nitidez
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

    //se toman los permisos de la camara
    private boolean permissions(List<String> listPermissionsNeeded) {

        if (!listPermissionsNeeded.isEmpty()) {
            ActivityCompat.requestPermissions(this, listPermissionsNeeded.toArray
                    (new String[listPermissionsNeeded.size()]), REQUEST_ID_MULTIPLE_PERMISSIONS);
            return true;
        }
        return false;
    }

    //metodo que pasa de de uri a url
    public String getRealPathFromURI(Uri contentUri) {
        String[] proj = { MediaStore.Images.Media.DATA };
        Cursor cursor = managedQuery(contentUri, proj, null, null, null);
        int column_index = cursor
                .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }

    /**
     * Permite detectar cuan se da click en la tecla de back para lanzar el
     * dialogo de advertencia en caso de ser necesario
     * @param keyCode Codigo de la tecla que seleccióno el usuario
     * @param event Objeto que contiene una referencia {@link KeyEvent} de la tecla seleccíonada
     * @return Valor voleano de que si se ejecuta la acción o no
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK){
            if(iniciar) {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, android.R.style.Theme_DeviceDefault_Light_Dialog);
                builder.setTitle(R.string.title_alert_message);
                builder.setMessage(R.string.message_goback_diabled);
                builder.setNegativeButton(getString(R.string.label_no), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.dismiss();
                    }
                });
                builder.setPositiveButton(getString(R.string.label_si), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.dismiss();
                        Intent intent = new Intent(CameraDescargaActivity.this, IniciarDescargaActivity.class);
                        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                        startActivity(intent);
                        finish();
                    }
                });
                builder.setCancelable(false);
                builder.show();
                return false;
            }else{
                return super.onKeyDown(keyCode, event);
            }
        }else{
            return super.onKeyDown(keyCode, event);
        }
    }

    //metodo que inicia y pasa los parametros a capturar porcentaje
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

    //metodo que inicia el activity que proocesa las imagenes y pasa los parametros
    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), SubirImagenesActivity.class);
        if(iniciar){
            intent.putExtra("IniciarDescarga", iniciarDescarga);
        }else if(finalizar){
            if(TanquePrestado){
                finalizarDescarga.setCantidadFotosTractor(0);
                finalizarDescarga.setPorcentajeMedidorTractor(0.0);
            }
            intent.putExtra("FinalizarDescarga",finalizarDescarga);
        }else if(papeleta){
            intent.putExtra("Papeleta",papeletaDTO);
        }else if(EsLecturaInicial){
            intent.putExtra("lecturaDTO",lecturaDTO);
        }else if(EsLecturaFinal){
            intent.putExtra("lecturaDTO",lecturaDTO);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            intent.putExtra("lecturaPipaDTO",lecturaPipaDTO);
            intent.putExtra("EsLecturaInicialPipa",EsLecturaInicialPipa);
            intent.putExtra("EsLecturaFinalPipa",EsLecturaFinalPipa);
        }
        intent.putExtra("EsPapeleta",papeleta);
        intent.putExtra("EsDescargaFinalizar",finalizar);
        intent.putExtra("EsDescargaIniciar",iniciar);
        intent.putExtra("Almacen",true);
        intent.putExtra("EsLecturaInicial",EsLecturaInicial);
        intent.putExtra("EsLecturaFinal",EsLecturaFinal);
        startActivity(intent);
    }
}
