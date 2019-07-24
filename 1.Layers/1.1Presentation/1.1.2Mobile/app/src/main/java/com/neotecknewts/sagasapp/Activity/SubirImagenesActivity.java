package com.neotecknewts.sagasapp.Activity;

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
import com.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;
import com.neotecknewts.sagasapp.Presenter.SubirImagenesPresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Session;

import java.io.ByteArrayOutputStream;

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
    public LecturaDTO lecturaDTO;
    public LecturaPipaDTO lecturaPipaDTO;
    public LecturaAlmacenDTO lecturaAlmacenDTO;
    public RecargaDTO recargaDTO;
    public AutoconsumoDTO autoconsumoDTO;
    public TraspasoDTO traspasoDTO;
    public CalibracionDTO calibracionDTO;

    //banderas para indicar el objeto a utlizar
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public boolean EsLecturaInicial,EsLecturaFinal,EsLecturaInicialPipa,EsLecturaFinalPipa;
    public boolean EsLecturaInicialAlmacen,EsLecturaFinalAlamacen;
    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal;
    public boolean EsRecargaPipaInicial,EsRecargaPipaFinal;
    public boolean EsAutoconsumoEstacionInicial,EsAutoconsumoEstacionFinal;
    public boolean EsAutoconsumoInvetarioInicial, EsAutoconsumoInventarioFinal;
    public boolean EsAutoconsumoPipaInicial,EsAutoconsumoPipaFinal;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal;
    public boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal;
    public boolean EsCalibracionEstacionInicial,EsCalibracionEstacionFinal;
    public boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal;

    public SubirImagenesPresenter presenter;
    public ProgressDialog progressDialog;
    public Session session;
    public SAGASSql sagasSql;

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
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Registro papeleta");
            }
            else if(extras.getBoolean("EsDescargaIniciar")){
                Log.w("SUBIR","EsDescargaIniciar");
                iniciarDescarga = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                papeleta=false;
                iniciar=true;
                finalizar=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Descarga");
            }
            else if(extras.getBoolean("EsDescargaFinalizar")){
                Log.w("SUBIR","EsDescargaFinalizar");
                finalizarDescarga = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                papeleta=false;
                iniciar=false;
                finalizar=true;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Descarga");
            }else if (extras.getBoolean("EsLecturaInicial")){
                Log.w("Subir","LecturaInicial");
                lecturaDTO= (LecturaDTO) extras.getSerializable("lecturaDTO");
                papeleta=false;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = extras.getBoolean("EsLecturaInicial");
                EsLecturaFinal = extras.getBoolean("EsLecturaFinal");
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Lectura");
            }else if (extras.getBoolean("EsLecturaFinal")){
                Log.w("Subir","LecturaFinal");
                lecturaDTO= (LecturaDTO) extras.getSerializable("lecturaDTO");
                papeleta=false;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = extras.getBoolean("EsLecturaInicial");
                EsLecturaFinal = extras.getBoolean("EsLecturaFinal");
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Lectura");
            }else  if(extras.getBoolean("EsLecturaInicialPipa") ||
                    extras.getBoolean("EsLecturaFinalPipa")){
                lecturaPipaDTO = (LecturaPipaDTO) extras.getSerializable("lecturaPipaDTO");
                papeleta=false;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = (boolean) extras.get("EsLecturaInicialPipa");
                EsLecturaFinalPipa = (boolean) extras.get("EsLecturaFinalPipa");
                EsLecturaInicialAlmacen = false ;
                EsLecturaFinalAlamacen = false;
                setTitle("Lectura");
            }else if(extras.getBoolean("EsLecturaInicialAlmacen")||
                    extras.getBoolean("EsLecturaFinalAlmacen")){
                lecturaAlmacenDTO = (LecturaAlmacenDTO) extras.
                        getSerializable("lecturaAlmacenDTO");
                papeleta=false;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = (boolean) extras.get("EsLecturaInicialAlmacen") ;
                EsLecturaFinalAlamacen = (boolean)extras.get("EsLecturaFinalAlmacen");
                setTitle("Lectura");
            }else if (extras.getBoolean("EsRecargaEstacionInicial")||
                    extras.getBoolean("EsRecargaEstacionFinal")){
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                EsRecargaEstacionInicial = extras.getBoolean("EsRecargaEstacionInicial");
                EsRecargaEstacionFinal = extras.getBoolean("EsRecargaEstacionFinal");
                papeleta=false;
                iniciar=false;
                finalizar=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                EsLecturaInicialAlmacen = false;
                EsLecturaFinalAlamacen = false;
                setTitle("Recarga");
            }else if(extras.getBoolean("EsRecargaPipaInicial") ||
                        extras.getBoolean("EsRecargaPipaFinal")){
                    EsRecargaPipaInicial = extras.getBoolean("EsRecargaPipaInicial");
                    EsRecargaPipaFinal = extras.getBoolean("EsRecargaPipaFinal");
                    recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                    EsRecargaEstacionInicial = false;
                    EsRecargaEstacionFinal = false;
                    papeleta=false;
                    iniciar=false;
                    finalizar=false;
                    EsLecturaInicial = false;
                    EsLecturaFinal = false;
                    EsLecturaInicialPipa = false;
                    EsLecturaFinalPipa = false;
                    EsLecturaInicialAlmacen = false;
                    EsLecturaFinalAlamacen = false;
                setTitle("Recarga");
            }else if (extras.getBoolean("EsAutoconsumoEstacionInicial") || extras.getBoolean("EsAutoconsumoEstacionFinal")){
                EsAutoconsumoEstacionInicial = extras.getBoolean("EsAutoconsumoEstacionInicial",false);
                EsAutoconsumoEstacionFinal = extras.getBoolean("EsAutoconsumoEstacionFinal",false);
                autoconsumoDTO =  (AutoconsumoDTO) extras.getSerializable("autoconsumoDTO");
                setTitle("Autoconsumo");
            }else if (extras.getBoolean("EsAutoconsumoInvetarioInicial") || extras.getBoolean("EsAutoconsumoInventarioFinal")){
                EsAutoconsumoInvetarioInicial = extras.getBoolean("EsAutoconsumoInvetarioInicial",false);
                EsAutoconsumoInventarioFinal = extras.getBoolean("EsAutoconsumoInventarioFinal",false);
                autoconsumoDTO =  (AutoconsumoDTO) extras.getSerializable("autoconsumoDTO");
                setTitle("Autoconsumo");
            }else if(extras.getBoolean("EsAutoconsumoPipaInicial") ||
                    extras.getBoolean("EsAutoconsumoPipaFinal")){
                EsAutoconsumoPipaInicial = extras.getBoolean("EsAutoconsumoPipaInicial",false);
                EsAutoconsumoPipaFinal = extras.getBoolean("EsAutoconsumoPipaFinal",false);
                autoconsumoDTO = (AutoconsumoDTO) extras.getSerializable("autoconsumoDTO");
                setTitle("Autoconsumo");
            }else if(extras.getBoolean("EsTraspasoEstacionInicial")||extras.getBoolean("EsTraspasoEstacionFinal")){
                EsTraspasoEstacionInicial = extras.getBoolean("EsTraspasoEstacionInicial",false);
                EsTraspasoEstacionFinal = extras.getBoolean("EsTraspasoEstacionFinal",false);
                traspasoDTO = (TraspasoDTO) extras.getSerializable("traspasoDTO");
                setTitle("Traspaso");
            }else if(extras.getBoolean("EsTraspasoPipaInicial",false)|| extras.getBoolean("EsTraspasoPipaFinal",false)){
                EsTraspasoPipaInicial = extras.getBoolean("EsTraspasoPipaInicial",false);
                EsTraspasoPipaFinal = extras.getBoolean("EsTraspasoPipaFinal",false);
                traspasoDTO = (TraspasoDTO) extras.getSerializable("traspasoDTO");
                setTitle("Traspaso");
            }else if(extras.getBoolean("EsCalibracionEstacionInicial",false)|| extras.getBoolean("EsCalibracionEstacionFinal",false)){
                EsCalibracionEstacionInicial = extras.getBoolean("EsCalibracionEstacionInicial",false);
                EsCalibracionEstacionFinal = extras.getBoolean("EsCalibracionEstacionFinal",false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                setTitle("Calibración");
            }else if(extras.getBoolean("EsCalibracionPipaInicial",false)|| extras.getBoolean("EsCalibracionPipaFinal",false)){
                EsCalibracionPipaInicial = extras.getBoolean("EsCalibracionPipaInicial",false);
                EsCalibracionPipaFinal = extras.getBoolean("EsCalibracionPipaFinal",false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                setTitle("Calibración");
            }

        }
        if(papeleta) {
            sagasSql = new SAGASSql(getApplicationContext());
        }else if (iniciar) {
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(finalizar){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if (EsLecturaInicial || EsLecturaFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsLecturaInicialAlmacen || EsLecturaFinalAlamacen){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsRecargaPipaInicial || EsRecargaPipaFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if (EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsAutoconsumoInvetarioInicial|| EsAutoconsumoInventarioFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsAutoconsumoPipaInicial ||EsAutoconsumoPipaFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }else if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            sagasSql = new SAGASSql(getApplicationContext());
        }
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
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
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
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
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
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    finalizarDescarga.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){

                }
            }
        }else if(EsLecturaInicial){//LecturaInicial
            for (int i=0; i<lecturaDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(lecturaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    Log.d("image", image.trim());
                    lecturaDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
            /*Uri uri = Uri.parse(lecturaDTO.getImagenP5000URI().toString());
            Bitmap bitmap = null;
            try {
                bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), uri);
                bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                ByteArrayOutputStream bs = new ByteArrayOutputStream();
                bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                byte[] b = bs.toByteArray();
                String image = Base64.encodeToString(b, Base64.DEFAULT);
                lecturaDTO.setImagenP5000(image.trim());
                Log.w("Imagen P5000",""+uri.toString());
            } catch (IOException e) {
                e.printStackTrace();
            }*/

        }else if(EsLecturaFinal){//LecturaFinal
            for (int i=0; i<lecturaDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(lecturaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    lecturaDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
            /*Uri uri = Uri.parse(lecturaDTO.getImagenP5000URI().toString());
            Bitmap bitmap = null;
            try {
                bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), uri);
                bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                ByteArrayOutputStream bs = new ByteArrayOutputStream();
                bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                byte[] b = bs.toByteArray();
                String image = Base64.encodeToString(b, Base64.DEFAULT);
                lecturaDTO.setImagenP5000(image.trim());
                Log.w("Imagen P5000",""+uri.toString());
            } catch (IOException e) {
                e.printStackTrace();
            }*/

        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){//Lectura pipa
            for (int i=0; i<lecturaPipaDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(lecturaPipaDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    lecturaPipaDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
            /*Uri uri = Uri.parse(lecturaPipaDTO.getImagenP5000URI().toString());
            Bitmap bitmap = null;
            try {
                bitmap = MediaStore.Images.Media.getBitmap(
                        getContentResolver(), uri);
                bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                ByteArrayOutputStream bs = new ByteArrayOutputStream();
                bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                byte[] b = bs.toByteArray();
                String image = Base64.encodeToString(b, Base64.DEFAULT);
                lecturaPipaDTO.setImagenP5000(image.trim());
                Log.w("Imagen P5000",""+uri.toString());
            } catch (IOException e) {
                e.printStackTrace();
            }*/
        }else if(EsLecturaInicialAlmacen || EsLecturaFinalAlamacen){
            for (int i=0; i<lecturaAlmacenDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(lecturaAlmacenDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(), uri);
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    lecturaAlmacenDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            for (int i= 0; i<recargaDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(recargaDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    recargaDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsRecargaPipaFinal){
            for (int i= 0; i<recargaDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(recargaDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    recargaDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if (EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            for (int i= 0; i<autoconsumoDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(autoconsumoDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    autoconsumoDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if (EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
            for (int i= 0; i<autoconsumoDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(autoconsumoDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    autoconsumoDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if (EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
            for (int i= 0; i<autoconsumoDTO.getImagenesURI().size();i++){
                try {
                    Uri uri = Uri.parse(autoconsumoDTO.getImagenesURI().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    autoconsumoDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
            for (int i= 0; i<traspasoDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(traspasoDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    traspasoDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
            for (int i= 0; i<traspasoDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(traspasoDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    traspasoDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            for (int i= 0; i<calibracionDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(calibracionDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    calibracionDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }else if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            for (int i= 0; i<calibracionDTO.getImagenesUri().size();i++){
                try {
                    Uri uri = Uri.parse(calibracionDTO.getImagenesUri().get(i).toString());
                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(
                            getContentResolver(),uri
                    );
                    bitmap = Bitmap.createScaledBitmap(bitmap,bitmap.getWidth(),bitmap.getHeight(),true);
                    ByteArrayOutputStream bs = new ByteArrayOutputStream();
                    bitmap.compress(Bitmap.CompressFormat.JPEG, 40, bs);
                    byte[] b = bs.toByteArray();
                    String image = Base64.encodeToString(b, Base64.DEFAULT);
                    calibracionDTO.getImagenes().add(image.trim());
                    Log.w("Imagenes"+i,""+uri.toString());
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }

    }
    //se muestra un cuadro de dialogo con un mensaje
    private void showDialogAceptar(String titulo, String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this,R.style.AlertDialog);
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
        /*progressDialog = ProgressDialog.show(SubirImagenesActivity.this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);*/
        progressDialog = new ProgressDialog(SubirImagenesActivity.this);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setTitle(getString(R.string.app_name));
        progressDialog.setIndeterminate(true);
        progressDialog.setCancelable(false);
       // progressDialog.show();
    }

    /**
     * Oculta el mensaje de progressdialog
     */
    @Override
    public void hideProgress() {
        if(progressDialog != null && progressDialog.isShowing()){
            progressDialog.dismiss();
        }
    }

    /**
     * En caso de que el servicio web haya registrado los datos sin ningun error
     * se mostrara un dialog para mostrar al usuario que los datos fueron guardados correctamente
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onSuccessRegistroPapeleta() {
        AlertDialog.Builder builder = new AlertDialog.Builder(SubirImagenesActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.titulo_exito_registro_papeleta);
        builder.setMessage(R.string.mensaje_exito_papeleta_registro_en_servicio);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                Intent intent = new Intent(SubirImagenesActivity.this, MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }
        });
        builder.setCancelable(false);
        builder.create().show();
    }

    /**
     * En caso de que el registro de los datos sean en el dispositivo , se mostrara el dialogo
     * de que los datos fueron guardados en el dispositvo
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onSuccessRegistroAndroid(){
        AlertDialog.Builder builder = new AlertDialog.Builder(SubirImagenesActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.titulo_exito_registro_papeleta_android);
        builder.setMessage(R.string.mensaje_exito_papeleta_android);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                Intent intent = new Intent(SubirImagenesActivity.this,
                        MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }
        });
        builder.setCancelable(false);
        builder.create().show();
    }

    /**
     * En caso de generarce algun error interno se mostrara este dialogo de error
     * en pantalla con el mensaje del mismo
     * @param mensaje Mensaje de error retornado del servicio
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void showError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(SubirImagenesActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.titulo_error_papeleta);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                finish();
            }
        });
        builder.setCancelable(false);
       // builder.create().show();

    }

    @Override
    public void onRegistrarIniciarDescarga() {
        AlertDialog.Builder builder = new AlertDialog.Builder(SubirImagenesActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.titulo_exito_registro_papeleta);
        builder.setMessage(R.string.mensaje_exito_papeleta_registro_en_servicio);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                Intent intent = new Intent(SubirImagenesActivity.this, MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }
        });
        builder.setCancelable(false);
        builder.create().show();
    }

    @Override
    public void onSuccessRegistroRecarga() {
        AlertDialog.Builder builder = new AlertDialog.Builder(SubirImagenesActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.titulo_exito_registro_papeleta);
        builder.setMessage(R.string.mensaje_exito_papeleta_registro_en_servicio);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                Intent intent = new Intent(SubirImagenesActivity.this, MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }
        });
        builder.setCancelable(false);
        builder.create().show();
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
            if(papeleta) {
                presenter.registrarPapeleta(papeletaDTO, session.getToken(), sagasSql,getApplicationContext());
            }else if (iniciar){
                presenter.registrarIniciarDescarga(iniciarDescarga,session.getToken(),sagasSql);
            }else if (finalizar){
                Log.d("registrarfinalizarDesc", finalizarDescarga.toString());
                presenter.registrarFinalizarDescarga(finalizarDescarga,session.getToken(),sagasSql);
            }else if (EsLecturaInicial){
                presenter.registrarLecturaInicial(sagasSql,session.getToken(),lecturaDTO);
            }else if (EsLecturaFinal){
                presenter.registrarLecturaFinal(sagasSql,session.getToken(),lecturaDTO);
            }else if (EsLecturaInicialPipa){
                presenter.registrarLecturaInicialPipa(sagasSql,session.getToken(),lecturaPipaDTO);
            }else if (EsLecturaFinalPipa){
                presenter.registrarLecturaFinalalPipa(sagasSql,session.getToken(),lecturaPipaDTO);
            }else if(EsLecturaInicialAlmacen){
                presenter.registrarLecturaInicialAlmacen(sagasSql,session.getToken(),lecturaAlmacenDTO);
            }else if (EsLecturaFinalAlamacen){
                presenter.registrarLecturaFinalAlmacen(sagasSql,session.getToken(),lecturaAlmacenDTO);
            }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
                presenter.registrarRecargaEstacion(sagasSql,session.getToken(),recargaDTO,EsRecargaEstacionInicial);
            }else if (EsRecargaPipaInicial || EsRecargaPipaFinal){
                presenter.registrarRecargaPipa(sagasSql,session.getToken(),recargaDTO,EsRecargaPipaFinal);
            }else if (EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
                presenter.registrarAutoconsumoEstacion(sagasSql,session.getToken(),autoconsumoDTO,EsAutoconsumoEstacionFinal);
            }else if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
                presenter.registrarAutoconsumoInventario(sagasSql,session.getToken(),autoconsumoDTO,EsAutoconsumoInventarioFinal);
            }else if(EsAutoconsumoPipaInicial ||EsAutoconsumoPipaFinal){
                presenter.registrarAutoconsumoPipa(sagasSql,session.getToken(),autoconsumoDTO,EsAutoconsumoPipaFinal);
            }else if (EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
                presenter.registrarTraspasoEstracion(sagasSql,session.getToken(),traspasoDTO,EsTraspasoEstacionFinal);
            }else if (EsTraspasoPipaInicial || EsTraspasoPipaFinal){
                presenter.registrarTraspasoEstracionPipa(sagasSql,session.getToken(),traspasoDTO,EsTraspasoPipaFinal);
            }else if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
                presenter.registrarCalibracionEstacion(sagasSql,session.getToken(),calibracionDTO,EsCalibracionEstacionFinal);
            }else if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
                presenter.registrarCalibracionPipa(sagasSql,session.getToken(),calibracionDTO,EsCalibracionPipaFinal);
            }
            textView.setText(R.string.cargando_imagenes_fin);
            //progressDialog.hide();
            //presenter.onSuccessRegistrarPapeleta();
            //showDialogAceptar("Operación Exitosa","Los datos se han guardado exitosamente");

        }


        @Override
        protected void onPreExecute() {
        }

    }

}

