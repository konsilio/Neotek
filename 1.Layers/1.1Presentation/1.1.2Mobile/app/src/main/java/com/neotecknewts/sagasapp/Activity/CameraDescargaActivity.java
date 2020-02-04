package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.ContentValues;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.hardware.Camera;
import android.hardware.camera2.CaptureRequest;
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
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.neotecknewts.sagasapp.Model.LecturaDTO;
import com.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.neotecknewts.sagasapp.Util.Permisos;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.URI;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class CameraDescargaActivity extends AppCompatActivity implements CameraDescargaView {
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
    public LecturaAlmacenDTO lecturaAlmacenDTO;
    public RecargaDTO recargaDTO;
    public AutoconsumoDTO autoconsumoDTO;
    public TraspasoDTO traspasoDTO;
    public CalibracionDTO calibracionDTO;

    //Banderas para indicar que objeto trabajar
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public int cantidadFotos;
    public boolean almacen;
    public boolean TanquePrestado;
    public boolean EsLecturaInicial, EsLecturaFinal;
    public boolean EsLecturaInicialPipa, EsLecturaFinalPipa;
    public boolean EsLecturaInicialAlmacen, EsLecturaFinalAlmacen;
    public boolean EsRecargaEstacionInicial, EsRecargaEstacionFinal, EsPrimeraLectura;
    public boolean EsRecargaPipaFinal;
    public boolean EsAutoconsumoPipaInicial, EsAutoconsumoPipaFinal;
    public boolean EsTraspasoEstacionInicial, EsTraspasoEstacionFinal, EsPrimeraParteTraspaso;
    public boolean EsCalibracionEstacionInicial, EsCalibracionEstacionFinal;
    public boolean EsCalibracionPipaInicial, EsCalibracionPipaFinal;
    public Permisos permisos;

    private Camera mCamera;
    private CameraPreview mPreview;
    private Camera.PictureCallback mPicture;
    private Button capture, switchCamera;
    private Context myContext;
    private LinearLayout cameraPreview;
    private boolean cameraFront = false;
    public static Bitmap bitmap;
    ProgressDialog dialog;


    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //Se indica que layout se carga
        setContentView(R.layout.activity_camera);

        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);


        myContext = this;
        mCamera = Camera.open();
        mCamera.setDisplayOrientation(90);

        Camera.Parameters parameters = mCamera.getParameters();
        parameters.setFocusMode(Camera.Parameters.FOCUS_MODE_CONTINUOUS_PICTURE);
        mCamera.setParameters(parameters);

        cameraPreview = (LinearLayout) findViewById(R.id.cPreview);
        mPreview = new CameraPreview(myContext, mCamera);
        cameraPreview.addView(mPreview);
        mPicture = getPictureCallback();
        capture = (Button) findViewById(R.id.button_foto);

        dialog = new ProgressDialog(CameraDescargaActivity.this);
        dialog.setMessage("Cargando");
        capture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.w("Camera log", "Click");

                runOnUiThread(new Runnable() {

                    @Override
                    public void run() {
                        dialog.show();
                    }
                });


                mCamera.takePicture(null, null, mPicture);
            }
        });

        mCamera.startPreview();

        permisos = new Permisos(CameraDescargaActivity.this);
        permisos.permisos();

        //Se inicia obtienen las variables desde la vista
        textViewTitulo = (TextView) findViewById(R.id.textTitulo);
        textViewMensaje = (TextView) findViewById(R.id.textIndicaciones);
        layoutCameraButton = (LinearLayout) findViewById(R.id.layout_photo_button);
        layoutNitidez = (LinearLayout) findViewById(R.id.layout_photo_nitida);
        layoutTitle = (LinearLayout) findViewById(R.id.layout_title);
        imageViewFoto = (ImageView) findViewById(R.id.image_view_foto);

        //se obtienen los datos que provienen del activity anterior
        Bundle extras = getIntent().getExtras();

        fotoTomada = false;
        if (extras != null) {
            //se acomoda la vista en modo Papeleta y se les da valor a las variables realcionadas con papeleta
            if (extras.getBoolean("EsPapeleta")) {
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                cantidadFotos = papeletaDTO.getCantidadFotosTractor();
                textViewTitulo.setText("Fotografia " + papeletaDTO.getNombreTipoMedidorTractor() + " - Tractor");
                papeleta = true;
                iniciar = false;
                finalizar = false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsCalibracionPipaInicial = false;
                EsCalibracionPipaFinal = false;
                textViewMensaje.setText(R.string.mensaje_primera_foto);
            }
            //se acomoda la vista en modo Iniciar descarga y se les da valor a las variables realcionadas con Iniciar descarga
            else if (extras.getBoolean("EsDescargaIniciar")) {
                Log.w("CAMARA", "DescargaIniciar");
                iniciarDescarga = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                if (extras.getBoolean("Almacen")) {
                    cantidadFotos = iniciarDescarga.getCantidadFotosAlmacen();
                    textViewTitulo.setText("Fotografia " + iniciarDescarga.getNombreTipoMedidorAlmacen() + " - Almacen");

                } else if (!extras.getBoolean("Almacen")) {
                    cantidadFotos = iniciarDescarga.getCantidadFotosTractor();
                    textViewTitulo.setText("Fotografia " + iniciarDescarga.getNombreTipoMedidorTractor() + " - Tractor");

                }
                iniciar = extras.getBoolean("EsDescargaIniciar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
                TanquePrestado = extras.getBoolean("TanquePrestado");
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                setTitle(R.string.title_iniciar_descarga);
            }
            //se acomoda la vista en modo Finalizar descarga y se les da valor a las variables realcionadas con finalizar descarga
            else if (extras.getBoolean("EsDescargaFinalizar")) {
                finalizarDescarga = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                if (extras.getBoolean("Almacen")) {
                    cantidadFotos = finalizarDescarga.getCantidadFotosAlmacen();
                    textViewTitulo.setText("Fotografia " + finalizarDescarga.getNombreTipoMedidorAlmacen() + " - Almacen");

                } else if (!extras.getBoolean("Almacen")) {
                    cantidadFotos = finalizarDescarga.getCantidadFotosTractor();
                    textViewTitulo.setText("Fotografia " + finalizarDescarga.getNombreTipoMedidorTractor() + " - Tractor");

                }
                finalizar = extras.getBoolean("EsDescargaFinalizar");
                almacen = extras.getBoolean("Almacen");
                textViewMensaje.setText(R.string.mensaje_primera_foto);
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                setTitle(R.string.title_finalizar_descarga);
            } else if (extras.getBoolean("EsLecturaInicial") ||
                    extras.getBoolean("EsLecturaFinal")) {
                lecturaDTO = (LecturaDTO) extras.getSerializable("lecturaDTO");
                cantidadFotos = lecturaDTO.getCantidadFotografias();
                textViewTitulo.setText("Fotografia " + lecturaDTO.getNombreTipoMedidor()
                        + " - " + lecturaDTO.getNombreEstacionCarburacion());
                EsLecturaInicial = extras.getBoolean("EsLecturaInicial");
                EsLecturaFinal = extras.getBoolean("EsLecturaFinal");
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                setTitle(R.string.toma_de_lectura);
                textViewMensaje.setText("Fotografia medidor " + lecturaDTO.getNombreTipoMedidor());
            } else if (extras.getBoolean("EsLecturaInicialPipa") ||
                    extras.getBoolean("EsLecturaFinalPipa")) {
                lecturaPipaDTO = (LecturaPipaDTO) extras.getSerializable("lecturaPipaDTO");
                cantidadFotos = lecturaPipaDTO.getCantidadFotografias();
                textViewTitulo.setText("Fotografia " + lecturaPipaDTO.getTipoMedidor()
                        + " - " + lecturaPipaDTO.getNombrePipa());
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = extras.getBoolean("EsLecturaInicialPipa");
                EsLecturaFinalPipa = extras.getBoolean("EsLecturaFinalPipa");
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                setTitle(R.string.toma_de_lectura);
                textViewMensaje.setText("");
            } else if (extras.getBoolean("EsLecturaInicialAlmacen") ||
                    extras.getBoolean("EsLecturaFinalAlmacen")) {
                lecturaAlmacenDTO = (LecturaAlmacenDTO) extras.
                        getSerializable("lecturaAlmacenDTO");
                cantidadFotos = lecturaAlmacenDTO.getCantidadFotografias();
                textViewTitulo.setText("Fotografia " + lecturaAlmacenDTO.getNombreTipoMedidor() +
                        " - " + lecturaAlmacenDTO.getNombreAlmacen()
                );
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = extras.getBoolean("EsLecturaInicialAlmacen");
                EsLecturaFinalAlmacen = extras.getBoolean("EsLecturaFinalAlmacen");
                setTitle(R.string.toma_de_lectura);
            } else if (extras.getBoolean("EsRecargaEstacionInicial") ||
                    extras.getBoolean("EsRecargaEstacionFinal")) {
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                cantidadFotos = recargaDTO.getCantidadFotosEntrada();
                textViewTitulo.setText("Fotografia " + recargaDTO.getNombreMedidorEntrada() +
                        " - Estación"
                );
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = extras.getBoolean("EsRecargaEstacionInicial",
                        false);
                EsRecargaEstacionFinal = extras.getBoolean("EsRecargaEstacionFinal",
                        false);
                setTitle(R.string.recarga);
            } else if (extras.getBoolean("EsRecargaPipaFinal")) {
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                cantidadFotos = recargaDTO.getCantidadFotosEntrada();
                EsRecargaPipaFinal = extras.getBoolean("EsRecargaPipaFinal");
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = false;
                EsRecargaEstacionFinal = false;
                textViewTitulo.setText("Fotografia " + recargaDTO.getNombreMedidorEntrada());
                setTitle(R.string.recarga);
            } else if (extras.getBoolean("EsAutoconsumoPipaInicial") ||
                    extras.getBoolean("EsAutoconsumoPipaFinal")) {
                EsRecargaPipaFinal = false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = false;
                EsRecargaEstacionFinal = false;
                EsAutoconsumoPipaInicial = extras.getBoolean("EsAutoconsumoPipaInicial");
                EsAutoconsumoPipaFinal = extras.getBoolean("EsAutoconsumoPipaFinal");
                autoconsumoDTO = (AutoconsumoDTO) extras.getSerializable("autoconsumoDTO");
                textViewTitulo.setText("Fotografia del " + autoconsumoDTO.getNombreTipoMedidor());
                cantidadFotos = autoconsumoDTO.getCantidadFotos();
                setTitle(R.string.Autoconsumo);
            } else if (extras.getBoolean("EsTraspasoEstacionInicial", false) ||
                    extras.getBoolean("EsTraspasoEstacionFinal", false)) {
                EsRecargaPipaFinal = false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = false;
                EsRecargaEstacionFinal = false;
                EsAutoconsumoPipaInicial = false;
                EsAutoconsumoPipaFinal = false;
                EsTraspasoEstacionInicial = extras.getBoolean("EsTraspasoEstacionInicial", false);
                EsTraspasoEstacionFinal = extras.getBoolean("EsTraspasoEstacionFinal", false);
                EsPrimeraParteTraspaso = extras.getBoolean("EsPrimeraParteTraspaso", true);
                traspasoDTO = (TraspasoDTO) extras.getSerializable("traspasoDTO");
                textViewTitulo.setText("Fotografia del " + traspasoDTO.getNombreMedidor());
                cantidadFotos = traspasoDTO.getCantidadDeFotos();
                setTitle(R.string.Traspaso);
            } else if (extras.getBoolean("EsCalibracionEstacionInicial", false) ||
                    extras.getBoolean("EsCalibracionEstacionFinal", false)) {
                EsRecargaPipaFinal = false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = false;
                EsRecargaEstacionFinal = false;
                EsAutoconsumoPipaInicial = false;
                EsAutoconsumoPipaFinal = false;
                EsTraspasoEstacionInicial = false;
                EsTraspasoEstacionFinal = false;
                EsPrimeraParteTraspaso = false;
                EsCalibracionEstacionInicial = extras.getBoolean("EsCalibracionEstacionInicial",
                        false);
                EsCalibracionEstacionFinal = extras.getBoolean("EsCalibracionEstacionFinal",
                        false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                setTitle(R.string.Calibracion);
                textViewTitulo.setText("Fotografia del " + calibracionDTO.getNombreMedidor());
                textViewMensaje.setText("Fotografia del " + calibracionDTO.getNombreMedidor());
                cantidadFotos = calibracionDTO.getCantidadFotografias();
            } else if (extras.getBoolean("EsCalibracionPipaInicial", false) ||
                    extras.getBoolean("EsCalibracionPipaFinal", false)) {
                Log.d("escalibracionpipa", "");
                EsRecargaPipaFinal = false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlmacen = false;
                EsRecargaEstacionInicial = false;
                EsRecargaEstacionFinal = false;
                EsAutoconsumoPipaInicial = false;
                EsAutoconsumoPipaFinal = false;
                EsTraspasoEstacionInicial = false;
                EsTraspasoEstacionFinal = false;
                EsPrimeraParteTraspaso = false;
                EsCalibracionEstacionInicial = false;
                EsCalibracionEstacionFinal = false;
                EsCalibracionPipaInicial = extras.getBoolean("EsCalibracionPipaInicial",
                        false);
                EsCalibracionPipaFinal = extras.getBoolean("EsCalibracionPipaFinal",
                        false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                textViewTitulo.setText("Fotografia del " + calibracionDTO.getNombreMedidor());
                setTitle(R.string.Calibracion);
                cantidadFotos = calibracionDTO.getCantidadFotografias();
                textViewMensaje.setText("Fotografia del " + calibracionDTO.getNombreMedidor());
            }

        }
//            layoutTitle.setVisibility(View.VISIBLE);
        layoutCameraButton.setVisibility(View.VISIBLE);
        layoutNitidez.setVisibility(View.GONE);
        textViewMensaje.setVisibility(View.VISIBLE);
        final Button buttonFotoCorrecta = (Button) findViewById(R.id.button_foto_correcta);
        buttonFotoCorrecta.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                Log.w("BOTON", "finalizar" + cantidadFotos);
                checarboton();
            }
        });


    }

    public void onResume() {

        super.onResume();

        if (mCamera == null) {
            mCamera = Camera.open();
            mCamera.setDisplayOrientation(90);
            mPicture = getPictureCallback();
            mPreview.refreshCamera(mCamera);
            Log.d("nu", "null");
        } else {
            Log.d("nu", "no null");
        }

    }

    @Override
    protected void onPause() {
        super.onPause();
        //when on Pause, release camera in order to be used from other applications
        releaseCamera();
    }

    private void releaseCamera() {
        // stop and release camera
        if (mCamera != null) {
            mCamera.stopPreview();
            mCamera.setPreviewCallback(null);
            mCamera.release();
            mCamera = null;
        }
    }

    private String saveToInternalStorage(byte[] data) {
        ContextWrapper cw = new ContextWrapper(getApplicationContext());
        // path to /data/data/yourapp/app_data/imageDir
        File directory = cw.getDir("imageDir", Context.MODE_PRIVATE);
        // Create imageDir
        File mypath = new File(directory, Calendar.getInstance().getTimeInMillis() + ".jpg");
        FileOutputStream fos = null;

        try {
            fos = new FileOutputStream(mypath);
            // Use the compress method on the BitMap object to write image to the OutputStream
            fos.write(data);
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                fos.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return directory.getAbsolutePath();
    }

    private Camera.PictureCallback getPictureCallback() {
        Camera.PictureCallback picture = new Camera.PictureCallback() {
            @Override
            public void onPictureTaken(byte[] data, Camera camera) {
                Log.w("Camera log", "Tomada");
                Matrix matrix = new Matrix();
                matrix.postRotate(90);

                bitmap = BitmapFactory.decodeByteArray(data, 0, data.length);
                Bitmap rotatedBitmap = Bitmap.createBitmap(bitmap, 0, 0, bitmap.getWidth(), bitmap.getHeight(), matrix, true);
                fotoTomada = true;
                Log.w("Camera log", "Rotada");
                imageurl = saveToInternalStorage(data);

                Log.d("Imagen data", data + "");
                Log.w("Camera log", "Guardada");

                imageViewFoto.setImageBitmap(rotatedBitmap);
                Log.w("Camera log", "SetBitmap");
                dialog.dismiss();
                mPreview.refreshCamera(mCamera);
                //como la foto ya fue tomada se muestra el layout de nitidez
                layoutNitidez.setVisibility(View.VISIBLE);
                textViewMensaje.setVisibility(View.VISIBLE);
                ContentValues values = new ContentValues();
                values.put(MediaStore.Images.Media.TITLE, new Date().toString() + "M");
                values.put(MediaStore.Images.Media.DESCRIPTION, "From your Camera");
                imageUri = getContentResolver().insert(
                        MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
                imageurl = saveToInternalStorage(data);
            }
        };
        return picture;
    }


    //En caso de que la foto sea correcta
    public void checarboton() {
        Log.w("Boton", "finalizar" + cantidadFotos + finalizar + almacen);
        //si aun faltan fotos por tomar
        if (cantidadFotos != 1) {
            try {
                //se cehca que objeto se esta usando y se agrega la uri de la imagen a su lista
                if (papeleta) {
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                } else if (iniciar) {
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (finalizar) {
                    Log.w("Boton", "finalizar" + cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsLecturaInicial) {
                    Log.w("Boton lec.Inic.", "finalizar" + cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsLecturaFinal) {
                    Log.w("Boton lec.Final.", "finalizar" + cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsLecturaInicialPipa || EsLecturaFinalPipa) {
                    Log.w("Lectura Pipa", "finalizar" + cantidadFotos);
                    lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen) {
                    Log.w("Lectura Almacen", "finalizar" + cantidadFotos);
                    lecturaAlmacenDTO.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsRecargaEstacionInicial || EsRecargaEstacionFinal) {
                    Log.v("Recarga estación", "finalizar " + cantidadFotos);
                    recargaDTO.getImagenesUri().add(new URI(imageUri.toString()));
                } else if (EsRecargaPipaFinal) {
                    Log.v("Recarga estación", "finalizar " + cantidadFotos);
                    recargaDTO.getImagenesUri().add(new URI(imageUri.toString()));
                } else if (EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal) {
                    Log.v("Autoconsumo pipa", "finalizar " + cantidadFotos);
                    autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                } else if (EsTraspasoEstacionInicial || EsTraspasoEstacionFinal) {
                    Log.v("Traspaso estacion", "finalizar " + cantidadFotos);
                    traspasoDTO.getImagenesUri().add(new URI(imageUri.toString()));
                } else if (EsCalibracionEstacionInicial || EsCalibracionEstacionFinal) {
                    Log.v("calibracion estacion", "finalizar " + cantidadFotos);
                    calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                } else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal) {
                   /* Log.v("calibracion pipa", "finalizar " + cantidadFotos);
                    calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));*/
                    calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityCalibracion();
                } else {
                    Log.d("Checar boton", "Else");
                }
            } catch (Exception ex) {
                ex.printStackTrace();
                Log.d("exception camara", ex + "");
            }
            //  Log.d("Imagen", papeletaDTO.getImagenes()+"");
            //se pone visible el layout para tomar la siguiente fotografia
            layoutTitle.setVisibility(View.VISIBLE);
            layoutCameraButton.setVisibility(View.VISIBLE);
            layoutNitidez.setVisibility(View.VISIBLE);
            textViewMensaje.setVisibility(View.VISIBLE);
            textViewMensaje.setText(R.string.mensaje_segunda_foto);
            cantidadFotos--;
            Log.w("Boton", "finalizar" + cantidadFotos);
            //si ya se termino de tomar las fotos
        } else {
            try {
                if (papeleta) {
                    //se agrega la ultima foto tomada
                    papeletaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    //se inicia el activity que se encargara de procesaar las imagenes
                    startActivity();
                } else if (iniciar && !almacen) {
                    Log.w("Boton", "TractorIniciar" + cantidadFotos);
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //si es el medidor del tractor (la variable almacen es falsa por lo que estamos en la foto del medidor del tractor
                    //se inicia el captivity para capturar el porcentaje del siguiente medidor
                    if (!TanquePrestado)
                        startActivityPorcentaje();
                    else
                        startActivity();
                } else if (finalizar && !almacen) {
                    Log.w("Boton", "TractorFinalizar" + cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivityPorcentaje();
                } else if (iniciar && almacen) {
                    Log.w("Boton", "AlmacenIniciar" + cantidadFotos);
                    iniciarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //processImages();
                    startActivity();
                } else if (finalizar && almacen) {
                    Log.w("Boton", "AlmacenFinalizar" + cantidadFotos);
                    finalizarDescarga.getImagenesURI().add(new URI(imageUri.toString()));
                    //processImages();
                    startActivity();
                } else if (EsLecturaInicial) {
                    Log.w("Boton lec.Inic.", "finalizar" + cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                } else if (EsLecturaFinal) {
                    Log.w("Boton lec.Final.", "finalizar" + cantidadFotos);
                    lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                } else if (EsLecturaInicialPipa || EsLecturaFinalPipa) {
                    Log.w("Lectura Pipa", "finalizar" + cantidadFotos);
                    lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                } else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen) {
                    Log.w("Lectura Almacen", "finalizar" + cantidadFotos);
                    lecturaAlmacenDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivity();
                } else if (EsRecargaEstacionInicial || EsRecargaEstacionFinal) {
                    Log.w("Recarga estación", "finalizar" + cantidadFotos);
                    recargaDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityRecarga();
                } else if (EsRecargaPipaFinal) {
                    Log.w("Recarga estación", "finalizar" + cantidadFotos);
                    recargaDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityRecarga();
                } else if (EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal) {
                    Log.v("Autoconsumo pipa", "finalizar " + cantidadFotos);
                    autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                    startActivityAutoconsumo();
                } else if (EsTraspasoEstacionInicial || EsTraspasoEstacionFinal) {
                    Log.v("Traspaso estación", "finalizar " + cantidadFotos);
                    traspasoDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityTraspaso();
                } else if (EsCalibracionEstacionInicial || EsCalibracionEstacionFinal) {
                    Log.v("calibracion estacion", "finalizar " + cantidadFotos);
                    calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityCalibracion();
                } else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal) {
                    Log.v("calibracion pipa2", "finalizar " + cantidadFotos);
                    calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                    startActivityCalibracion();
                } else {
                    Log.d("Checar boton", "Else");
                }
            } catch (Exception ex) {
                ex.printStackTrace();
                Log.d("exception", ex + "");
            }

        }
    }


    //metodo que abre la camara
    private void openCameraIntent() {
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        ContentValues values = new ContentValues();
        values.put(MediaStore.Images.Media.TITLE, new Date().toString() + "M");
        values.put(MediaStore.Images.Media.DESCRIPTION, "From your Camera");
        imageUri = getContentResolver().insert(
                MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        intent.putExtra(MediaStore.EXTRA_OUTPUT, imageUri);
        startActivityForResult(intent, CAMERA_REQUEST);

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
        String[] proj = {MediaStore.Images.Media.DATA};
        @SuppressLint("Recycle") Cursor cursor = getContentResolver().query(contentUri, proj, null, null, null);
        int column_index = 0;
        String cadena = "";
        if (cursor != null) {
            column_index = cursor
                    .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);

            cursor.moveToFirst();
            cadena = cursor.getString(column_index);
        }
        if (cursor != null) {
            cursor.close();
        }
        return cadena;
    }

    /**
     * Permite detectar cuando se da click en la tecla de back para lanzar el
     * dialogo de advertencia en caso de ser necesario
     *
     * @param keyCode Codigo de la tecla que seleccióno el usuario
     * @param event   Objeto que contiene una referencia {@link KeyEvent} de la tecla seleccíonada
     * @return Valor voleano de que si se ejecuta la acción o no
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            if (iniciar) {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
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
            } else {
                return super.onKeyDown(keyCode, event);
            }
        } else {
            return super.onKeyDown(keyCode, event);
        }
    }

    //metodo que inicia y pasa los parametros a capturar porcentaje
    public void startActivityPorcentaje() {
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        if (iniciar) {
            intent.putExtra("IniciarDescarga", iniciarDescarga);
        } else if (finalizar) {
            intent.putExtra("FinalizarDescarga", finalizarDescarga);
        }
        intent.putExtra("EsPapeleta", false);
        intent.putExtra("EsDescargaFinalizar", finalizar);
        intent.putExtra("EsDescargaIniciar", iniciar);
        intent.putExtra("Almacen", true);
        startActivity(intent);
    }

    //metodo que inicia el activity que proocesa las imagenes y pasa los parametros
    public void startActivity() {
        Intent intent = new Intent(getApplicationContext(), SubirImagenesActivity.class);
        if (iniciar) {
            intent.putExtra("IniciarDescarga", iniciarDescarga);
        } else if (finalizar) {
            if (TanquePrestado) {
                finalizarDescarga.setCantidadFotosTractor(0);
                finalizarDescarga.setPorcentajeMedidorTractor(0.0);
            }
            intent.putExtra("FinalizarDescarga", finalizarDescarga);
        } else if (papeleta) {
            intent.putExtra("Papeleta", papeletaDTO);
        } else if (EsLecturaInicial) {
            intent.putExtra("lecturaDTO", lecturaDTO);
        } else if (EsLecturaFinal) {
            intent.putExtra("lecturaDTO", lecturaDTO);
        } else if (EsLecturaInicialPipa || EsLecturaFinalPipa) {
            intent.putExtra("lecturaPipaDTO", lecturaPipaDTO);
            intent.putExtra("EsLecturaInicialPipa", EsLecturaInicialPipa);
            intent.putExtra("EsLecturaFinalPipa", EsLecturaFinalPipa);
        } else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen) {
            intent.putExtra("lecturaAlmacenDTO", lecturaAlmacenDTO);
            intent.putExtra("EsLecturaInicialAlmacen", EsLecturaInicialAlmacen);
            intent.putExtra("EsLecturaFinalAlmacen", EsLecturaFinalAlmacen);
        }
       /* else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal) {
            intent.putExtra("Calibraciondto", calibracionDTO);
            intent.putExtra("EsCalibracionPipaInicial", EsCalibracionPipaInicial);
            intent.putExtra("EsCalibracionPipaFinal", EsCalibracionPipaFinal);
        }*/
        intent.putExtra("EsPapeleta", papeleta);
        intent.putExtra("EsDescargaFinalizar", finalizar);
        intent.putExtra("EsDescargaIniciar", iniciar);
        intent.putExtra("Almacen", true);
        intent.putExtra("EsLecturaInicial", EsLecturaInicial);
        intent.putExtra("EsLecturaFinal", EsLecturaFinal);
        startActivity(intent);
    }

    /**
     * <h3>startActivityRecarga</h3>
     * Permite inciar la actividad para genera el reporte
     * de la recarga en caso de ser finalizar ,  en caso contrario
     * se enviaran los datos de la recarga para su registro.
     *
     * @author Jorge Omar Tovar Martínez
     */
    public void startActivityRecarga() {
        if (EsRecargaEstacionFinal) {
            Intent intent = new Intent(CameraDescargaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsRecargaEstacionInicial", EsRecargaEstacionInicial);
            intent.putExtra("EsRecargaEstacionFinal", EsRecargaEstacionFinal);
            intent.putExtra("recargaDTO", recargaDTO);
            startActivity(intent);
        } else if (EsRecargaEstacionInicial) {
            Intent intent = new Intent(CameraDescargaActivity.this,
                    SubirImagenesActivity.class);
            intent.putExtra("EsRecargaEstacionInicial", EsRecargaEstacionInicial);
            intent.putExtra("EsRecargaEstacionFinal", EsRecargaEstacionFinal);
            intent.putExtra("recargaDTO", recargaDTO);
            startActivity(intent);
        } else if (EsRecargaPipaFinal) {
            Intent intent = new Intent(CameraDescargaActivity.this,
                    SubirImagenesActivity.class);
            intent.putExtra("recargaDTO", recargaDTO);
            intent.putExtra("EsRecargaPipaFinal", true);
            intent.putExtra("EsRecargaPipaInicial", false);
            startActivity(intent);
        }
    }

    private void startActivityAutoconsumo() {
        if (EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal) {
            Intent intent = new Intent(CameraDescargaActivity.this,
                    SubirImagenesActivity.class);
            intent.putExtra("EsAutoconsumoPipaInicial", EsAutoconsumoPipaInicial);
            intent.putExtra("EsAutoconsumoPipaFinal", EsAutoconsumoPipaFinal);
            intent.putExtra("autoconsumoDTO", autoconsumoDTO);
            startActivity(intent);
        }
    }

    private void startActivityTraspaso() {
        if (EsTraspasoEstacionInicial || EsTraspasoEstacionFinal) {
            EsPrimeraParteTraspaso = false;
            Intent intent = new Intent(CameraDescargaActivity.this, LecturaP5000Activity.class);
            intent.putExtra("EsTraspasoEstacionInicial", EsTraspasoEstacionInicial);
            intent.putExtra("EsTraspasoEstacionFinal", EsTraspasoEstacionFinal);
            intent.putExtra("EsPrimeraParteTraspaso", EsPrimeraParteTraspaso);
            intent.putExtra("traspasoDTO", traspasoDTO);
            startActivity(intent);
        }
    }

    private void startActivityCalibracion() {
        if (EsCalibracionEstacionInicial || EsCalibracionEstacionFinal) {
            Log.d("escalibracionestacion", EsCalibracionEstacionInicial + "" + EsCalibracionEstacionFinal + "");
            Intent intent = new Intent(CameraDescargaActivity.this,
                    SubirImagenesActivity.class);
            intent.putExtra("EsCalibracionEstacionInicial", EsCalibracionEstacionInicial);
            intent.putExtra("EsCalibracionEstacionFinal", EsCalibracionEstacionFinal);
            intent.putExtra("calibracionDTO", calibracionDTO);
            startActivity(intent);
        } else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal) {
            Log.d("escalibracionpipa", EsCalibracionPipaInicial + "" + EsCalibracionPipaFinal + "");
            Log.d("startEscalibracionPipa", "calibracionpipa");
            Intent intent = new Intent(CameraDescargaActivity.this,
                    SubirImagenesActivity.class);
            intent.putExtra("EsCalibracionPipaInicial", EsCalibracionPipaInicial);
            intent.putExtra("EsCalibracionPipaFinal", EsCalibracionPipaFinal);
            intent.putExtra("calibracionDTO", calibracionDTO);
            startActivity(intent);
        }
    }
}
