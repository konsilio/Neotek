package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.neotecknewts.sagasapp.Model.LecturaDTO;
import com.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.neotecknewts.sagasapp.Util.Permisos;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;
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
    public TraspasoDTO traspasoDTO;
    public CalibracionDTO calibracionDTO;

    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsAutoconsumoEstacionInicial,EsAutoconsumoEstacionFinal;
    public boolean EsAutoconsumoInvetarioInicial, EsAutoconsumoInventarioFinal;
    public boolean EsAutoconsumoPipaInicial,EsAutoconsumoPipaFinal;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    public boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal,EsPasoIniciaLPipa;
    public boolean EsCalibracionEstacionInicial,EsCalibracionEstacionFinal;
    public boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal;

    public Uri imageUri;
    String NombreImagen;
    public Permisos permisos;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera_lectura);
        permisos  = new Permisos(CameraLecturaActivity.this);
        permisos.permisos();

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
            EsAutoconsumoInvetarioInicial = b.getBoolean("EsAutoconsumoInvetarioInicial",false);
            EsAutoconsumoInventarioFinal = b.getBoolean("EsAutoconsumoInventarioFinal",false);
            EsAutoconsumoPipaInicial = b.getBoolean("EsAutoconsumoPipaInicial",false);
            EsAutoconsumoPipaFinal = b.getBoolean("EsAutoconsumoPipaFinal",false);
            autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");
            EsTraspasoEstacionInicial = b.getBoolean("EsTraspasoEstacionInicial",false);
            EsTraspasoEstacionFinal = b.getBoolean("EsTraspasoEstacionFinal",false);
            EsPrimeraParteTraspaso = b.getBoolean("EsPrimeraParteTraspaso",true);
            EsTraspasoPipaInicial = b.getBoolean("EsTraspasoPipaInicial",false);
            EsTraspasoPipaFinal = b.getBoolean("EsTraspasoPipaFinal",false);
            EsPasoIniciaLPipa = b.getBoolean("EsPasoIniciaLPipa",true);
            traspasoDTO = (TraspasoDTO) b.getSerializable("traspasoDTO");
            EsCalibracionEstacionInicial = b.getBoolean("EsCalibracionEstacionInicial",false);
            EsCalibracionEstacionFinal = b.getBoolean("EsCalibracionEstacionFinal",false);
            calibracionDTO = (CalibracionDTO) b.getSerializable("calibracionDTO");
            EsCalibracionPipaInicial = b.getBoolean("EsCalibracionPipaInicial",false);
            EsCalibracionPipaFinal = b.getBoolean("EsCalibracionPipaFinal",false);
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
            NombreImagen = String.valueOf(lecturaDTO.getIdEstacionCarburacion())+"|"+
                    String.valueOf(lecturaDTO.getNombreTipoMedidor())+"|"+"Inicial";
            setTitle(R.string.toma_de_lectura);
        }else if (EsLecturaFinal){
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    " - "+lecturaDTO.getNombreEstacionCarburacion());
            NombreImagen = String.valueOf(lecturaDTO.getIdEstacionCarburacion())+"|"+
                    String.valueOf(lecturaDTO.getNombreTipoMedidor())+"|"+"Final";
            setTitle(R.string.toma_de_lectura);
        }else if (EsLecturaInicialPipa){
            String pipa_nombre = lecturaPipaDTO.getNombrePipa().isEmpty()?
                    " ":" - "+lecturaPipaDTO.getNombrePipa();
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    pipa_nombre);
            NombreImagen = String.valueOf(lecturaPipaDTO.getIdPipa())+"|"+
                    String.valueOf(lecturaPipaDTO.getTipoMedidor())+"|"+"Inicial";
            setTitle(R.string.toma_de_lectura);
        }else if (EsLecturaFinalPipa){
            String pipa_nombre = lecturaPipaDTO.getNombrePipa().isEmpty()?
                    " ":" - "+lecturaPipaDTO.getNombrePipa();
            TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)+
                    pipa_nombre);
            NombreImagen = String.valueOf(lecturaPipaDTO.getIdPipa())+"|"+
                    String.valueOf(lecturaPipaDTO.getTipoMedidor())+"|"+"Final";
            setTitle(R.string.toma_de_lectura);

        }
        if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            if(EsPrimeraLectura){
                TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)
                +" - " +getString(R.string.Pipa));
                NombreImagen = String.valueOf(recargaDTO.getIdCAlmacenGasEntrada())+"|"+
                        String.valueOf(recargaDTO.getIdTipoMedidorEntrada())+"|"+"Inicial";
            }else{
                TVCameraLecturaActivityFotoEstacion.setText(getString(R.string.tomar_foto_estacion)
                        +" - " +getString(R.string.Estacion));
                NombreImagen = String.valueOf(recargaDTO.getIdCAlmacenGasSalida())+"|"+
                        String.valueOf(recargaDTO.getIdTipoMedidorSalida())+"|"+"Inicial";
            }
            setTitle(R.string.recarga);

        }
        if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                    +" - " +getString(R.string.Estacion)+" "+autoconsumoDTO.getNombreEstacion());
            NombreImagen =
                    (EsAutoconsumoEstacionInicial) ?
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Inicial":
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Final";
            TVCameraLecturaActivityTitulo.setText(getString(R.string.Autoconsumo)+": "
                    +autoconsumoDTO.getNombreEstacion());
            setTitle(getString(R.string.Autoconsumo));
        }
        if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +getString(R.string.Estacion)+" "+autoconsumoDTO.getNombreEstacion());
            NombreImagen =
                    (EsAutoconsumoEstacionInicial) ?
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Inicial":
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Final";
            TVCameraLecturaActivityTitulo.setText(getString(R.string.Autoconsumo)+": "
                    +autoconsumoDTO.getNombreEstacion());
            setTitle(getString(R.string.Autoconsumo));
        }
        if(EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +getString(R.string.Pipa));
            NombreImagen =
                    (EsAutoconsumoPipaInicial) ?
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Inicial":
                            String.valueOf(autoconsumoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(autoconsumoDTO.getNombreTipoMedidor())+"|"+"Final";
            TVCameraLecturaActivityTitulo.setText(getString(R.string.Autoconsumo)+": "
                    +autoconsumoDTO.getNombreEstacion());
            setTitle(getString(R.string.Autoconsumo));
        }
        if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
            setTitle("Traspaso");
            String nombre = "";
            if(EsPrimeraParteTraspaso) {
                 nombre = (traspasoDTO.getNombreEstacionTraspaso().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionTraspaso();
            }else{
                 nombre = (traspasoDTO.getNombreEstacionEntrada().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionEntrada();
            }
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +getString(R.string.Estacion)+nombre
            );
            NombreImagen =
                    (EsTraspasoEstacionInicial) ?
                            String.valueOf(traspasoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(traspasoDTO.getNombreMedidor())+"|"+"Inicial":
                            String.valueOf(traspasoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(traspasoDTO.getNombreMedidor())+"|"+"Final";
        }
        if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
            setTitle("Traspaso");
            String title = "";
            if(EsPasoIniciaLPipa) {
                title = (traspasoDTO.getNombreEstacionTraspaso().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionTraspaso();
            }else{
                title = (traspasoDTO.getNombreEstacionEntrada().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionEntrada();
            }
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +getString(R.string.Pipa)+title
            );
            NombreImagen =
                    (EsTraspasoPipaInicial) ?
                            String.valueOf(traspasoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(traspasoDTO.getNombreMedidor())+"|"+"Inicial":
                            String.valueOf(traspasoDTO.getIdCAlmacenGasSalida())+"|"+
                                    String.valueOf(traspasoDTO.getNombreMedidor())+"|"+"Final";
        }
        if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +calibracionDTO.getNombreCAlmacenGas()
            );
            TVCameraLecturaActivityTitulo.setText(getString(R.string.Calibracion)+": "
                    +calibracionDTO.getNombreCAlmacenGas());
            NombreImagen =
                    (EsCalibracionEstacionInicial) ?
                            String.valueOf(calibracionDTO.getIdCAlmacenGas())+"|"+
                                    String.valueOf(calibracionDTO.getNombreMedidor())+"|"+"Inicial":
                            String.valueOf(calibracionDTO.getIdCAlmacenGas())+"|"+
                                    String.valueOf(calibracionDTO.getNombreMedidor())+"|"+"Final";
            setTitle(R.string.Calibracion);
        }
        if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            TVCameraLecturaActivityFotoEstacion.setText(
                    getString(R.string.tomar_foto_estacion)
                            +" - " +calibracionDTO.getNombreCAlmacenGas()
            );
            NombreImagen =
                    (EsCalibracionPipaInicial) ?
                            String.valueOf(calibracionDTO.getIdCAlmacenGas())+"|"+
                                    String.valueOf(calibracionDTO.getNombreMedidor())+"|"+"Inicial":
                            String.valueOf(calibracionDTO.getIdCAlmacenGas())+"|"+
                                    String.valueOf(calibracionDTO.getNombreMedidor())+"|"+"Final";
            setTitle(R.string.Calibracion);
        }
        BtnCameraLecturaTomarFoto.setOnClickListener(v -> {
            //List<String> permissionList = Utilidades.checkAndRequestPermissions(getApplicationContext());

            //Log.w("Prueba","prueba"+permissions(permissionList));

           // if (permissions(permissionList)) {

                openCameraIntent();
            //}
        });

        BtnCameraLecturaFotoNitidaNo.setOnClickListener(v -> openCameraIntent());

        BtnCameraLecturaFotoNitidaSi.setOnClickListener(v -> verificarBoton());
    }

    private void verificarBoton() {
        if(EsFotoP5000 && EsLecturaInicial || EsLecturaFinal){
            try {
                //lecturaDTO.getImagenes().add(imageurl);
                lecturaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                lecturaDTO.setCantidadFotografias(lecturaDTO.getCantidadFotografias());
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
                //lecturaPipaDTO.getImagenes().add(imageurl);
                lecturaPipaDTO.getImagenesURI().add(new URI(imageUri.toString()));
                //lecturaDTO.setCantidadFotografias(lecturaDTO.getCantidadFotografias());
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
                //recargaDTO.getImagenes().add(imageurl);
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
                //autoconsumoDTO.getImagenes().add(imageurl);
                autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                autoconsumoDTO.setCantidadFotos(autoconsumoDTO.getCantidadFotos()+1);
                Intent intent = new Intent(CameraLecturaActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsAutoconsumoEstacionInicial",EsAutoconsumoEstacionInicial);
                intent.putExtra("EsAutoconsumoEstacionFinal",EsAutoconsumoEstacionFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
            try {
                //autoconsumoDTO.getImagenes().add(imageurl);
                autoconsumoDTO.setCantidadFotos(autoconsumoDTO.getCantidadFotos()+1);
                autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsAutoconsumoInvetarioInicial",EsAutoconsumoInvetarioInicial);
                intent.putExtra("EsAutoconsumoInventarioFinal",EsAutoconsumoInventarioFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
            try {
                //autoconsumoDTO.getImagenes().add(imageurl);
                //autoconsumoDTO.setCantidadFotos(autoconsumoDTO.getCantidadFotos()+1);
                autoconsumoDTO.getImagenesURI().add(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("EsAutoconsumoPipaInicial",EsAutoconsumoPipaInicial);
                intent.putExtra("EsAutoconsumoPipaFinal",EsAutoconsumoPipaFinal);
                intent.putExtra("EsPrimeraParteTraspaso",EsPrimeraParteTraspaso);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
            try {
                //traspasoDTO.getImagenes().add(imageurl);
                traspasoDTO.getImagenesUri().add(new URI(imageUri.toString()));
                if(EsPrimeraParteTraspaso) {
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            CapturaPorcentajeActivity.class);
                    intent.putExtra("EsTraspasoEstacionInicial", EsTraspasoEstacionInicial);
                    intent.putExtra("EsTraspasoEstacionFinal", EsTraspasoEstacionFinal);
                    intent.putExtra("EsPrimeraParteTraspaso", EsPrimeraParteTraspaso);
                    intent.putExtra("traspasoDTO", traspasoDTO);
                    startActivity(intent);
                }else{
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            VerReporteActivity.class);
                    intent.putExtra("EsTraspasoEstacionInicial", EsTraspasoEstacionInicial);
                    intent.putExtra("EsTraspasoEstacionFinal", EsTraspasoEstacionFinal);
                    intent.putExtra("EsPrimeraParteTraspaso", EsPrimeraParteTraspaso);
                    intent.putExtra("traspasoDTO", traspasoDTO);
                    startActivity(intent);
                }
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
            try {
                //traspasoDTO.getImagenes().add(imageurl);
                traspasoDTO.getImagenesUri().add(new URI(imageUri.toString()));
                if(EsPasoIniciaLPipa) {
                    EsPasoIniciaLPipa = false;
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            LecturaP5000Activity.class);
                    intent.putExtra("EsTraspasoPipaInicial", EsTraspasoPipaInicial);
                    intent.putExtra("EsTraspasoPipaFinal", EsTraspasoPipaFinal);
                    intent.putExtra("EsPasoIniciaLPipa", EsPasoIniciaLPipa);
                    intent.putExtra("traspasoDTO", traspasoDTO);
                    startActivity(intent);
                }else{
                    Intent intent = new Intent(CameraLecturaActivity.this,
                            VerReporteActivity.class);
                    intent.putExtra("EsTraspasoPipaInicial", EsTraspasoPipaInicial);
                    intent.putExtra("EsTraspasoPipaFinal", EsTraspasoPipaFinal);
                    intent.putExtra("EsPasoIniciaLPipa", EsPrimeraParteTraspaso);
                    intent.putExtra("traspasoDTO", traspasoDTO);
                    startActivity(intent);
                }
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            try {
                //calibracionDTO.getImagenes().add(imageurl);
                calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("EsCalibracionEstacionInicial",EsCalibracionEstacionInicial);
                intent.putExtra("EsCalibracionEstacionFinal",EsCalibracionEstacionFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
                startActivity(intent);
            }catch (URISyntaxException e){
                e.printStackTrace();
            }
        }else if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            try {
                //calibracionDTO.getImagenes().add(imageurl);
                calibracionDTO.getImagenesUri().add(new URI(imageUri.toString()));
                Intent intent = new Intent(CameraLecturaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("EsCalibracionPipaInicial",EsCalibracionPipaInicial);
                intent.putExtra("EsCalibracionPipaFinal",EsCalibracionPipaFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        ContentValues values = new ContentValues();
        values.put(MediaStore.Images.Media.TITLE, (NombreImagen!=null)? NombreImagen:new Date().toString());
        values.put(MediaStore.Images.Media.DESCRIPTION, "From your Camera");
        imageUri = getContentResolver().insert(
                MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        intent.putExtra(MediaStore.EXTRA_OUTPUT, imageUri);
        startActivityForResult(intent, CAMERA_REQUEST);


    }
    /*@RequiresApi(api = Build.VERSION_CODES.M)*/
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
        //Cursor cursor = managedQuery(contentUri, proj, null, null, null);
        @SuppressLint("Recycle") Cursor cursor = getContentResolver().query(contentUri, proj, null, null, null);
        int column_index = 0;
        String cadena = "";
        if (cursor != null) {
            column_index = cursor
                    .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);

            cursor.moveToFirst();
            cadena =cursor.getString(column_index);
        }
        if (cursor != null) {
            cursor.close();
        }
        return cadena;

    }
}
