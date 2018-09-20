package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class CapturaPorcentajeActivity extends AppCompatActivity {

    //variable que guarda el porcentaje capturado
    public Double porcentaje;
    public Double porcentaje_inicial;

    //variables relacionadas con la vista
    public NumberPicker numberPickerProcentaje;
    public NumberPicker numberPickerDecimal;
    public TextView textViewTitulo;
    public TextView textView;

    //objetos a completar con el porcentaje obtenido
    PrecargaPapeletaDTO papeletaDTO;
    IniciarDescargaDTO iniciarDescargaDTO;
    FinalizarDescargaDTO finalizarDescargaDTO;
    LecturaDTO lecturaDTO;
    LecturaPipaDTO lecturaPipaDTO;
    LecturaAlmacenDTO lecturaAlmacenDTO;
    RecargaDTO recargaDTO;

    //banderas para saber que objeto utilizar
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public boolean almacen;
    public boolean es_tanque_prestado;
    public boolean EsLecturaInicial;
    public boolean EsLecturaFinal;
    public boolean EsLecturaInicialPipa,EsLecturaFinalPipa;
    public boolean EsLecturaInicialAlmacen,EsLecturaFinalAlmacen;
    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsRecargaPipaFinal,EsRecargaPipaInicial;


    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_captura_porcentaje);


        //Se inicializan las variables que vienen del layout
        textViewTitulo = (TextView) findViewById(R.id.textViewTituloPorcentaje) ;
        textView = (TextView) findViewById(R.id.textViewCapturaText) ;
        numberPickerDecimal = (NumberPicker) findViewById(R.id.number_picker_decimal);
        numberPickerProcentaje = (NumberPicker) findViewById(R.id.number_picker_porcentaje);

        //se declaran los extras de donde se obtendran los valores que vienen de otro activity
        Bundle extras = getIntent().getExtras();

        if (extras !=null){
            //si es papeleta se cambian los textos y se obtiene el objeto del activity anterior
            if(extras.getBoolean("EsPapeleta")) {
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                Log.w("Image", Uri.parse(papeletaDTO.getImagenesURI().get(0).toString()) + "");
                papeleta=true;
                iniciar=false;
                finalizar=false;
                almacen=false;
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                textViewTitulo.setText(papeletaDTO.getNombreTipoMedidorTractor()+" - Tractor");
                textView.setText(R.string.porcentaje_medidor_tractor_message);
                //si es Iniciar descarga se cambian los textos y se obtiene el objeto del activity anterior
            }else if(extras.getBoolean("EsDescargaIniciar")){
                    iniciarDescargaDTO = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                    papeleta = false;
                    iniciar = true;
                    finalizar = false;
                    almacen = extras.getBoolean("Almacen");
                    es_tanque_prestado = extras.getBoolean("TanquePrestado");
                    EsLecturaInicial = false;
                    EsLecturaFinal = false;
                    EsLecturaInicialPipa = false;
                    EsLecturaFinalPipa = false;
                if (almacen) {
                    textViewTitulo.setText(iniciarDescargaDTO.getNombreTipoMedidorTractor()+" - Almacen");
                    textView.setText(R.string.porcentaje_medidor_almacen_message);
                }else{
                    textViewTitulo.setText(iniciarDescargaDTO.getNombreTipoMedidorTractor()+" - Tractor");
                    textView.setText(R.string.porcentaje_medidor_tractor_message);
                }
            }
            //si es finalizar descarga se cambian los textos y se obtiene el objeto del activity anterior
            else if(extras.getBoolean("EsDescargaFinalizar")){
                finalizarDescargaDTO = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                papeleta=false;
                iniciar=false;
                finalizar=true;
                almacen = extras.getBoolean("Almacen");
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                if (almacen) {
                    textViewTitulo.setText(finalizarDescargaDTO.getNombreTipoMedidorTractor()+" - Almacen");
                    textView.setText(R.string.porcentaje_medidor_almacen_message);
                }else{
                    textViewTitulo.setText(finalizarDescargaDTO.getNombreTipoMedidorTractor()+" - Tractor");
                    textView.setText(R.string.porcentaje_medidor_tractor_message);
                }
            }else if(extras.getBoolean("EsLecturaInicial") || extras.getBoolean("EsLecturaFinal")){
                lecturaDTO = (LecturaDTO) extras.getSerializable("lecturaDTO");
                textViewTitulo.setText(lecturaDTO.getNombreTipoMedidor()+
                        " - Estación ");
                textView.setText(getString(R.string.registra_porcentaje_estacion)+" "+
                        lecturaDTO.getNombreTipoMedidor()+" de la estación");
                EsLecturaInicial = (boolean) extras.get("EsLecturaInicial");
                EsLecturaFinal = (boolean) extras.get("EsLecturaFinal");
                porcentaje_inicial = lecturaDTO.getPorcentajeMedidor();
                if(porcentaje_inicial>0) {
                    String val_per = porcentaje_inicial.toString();
                    numberPickerProcentaje.setValue(Integer.parseInt(val_per.split(".")[0]));
                    numberPickerDecimal.setValue(Integer.parseInt(val_per.split(".")[1]));
                }
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;

            }else if (extras.getBoolean("EsLecturaInicialPipa") ||
                    extras.getBoolean("EsLecturaFinalPipa")){
                lecturaPipaDTO = (LecturaPipaDTO) extras.getSerializable("lecturaPipaDTO");
                textViewTitulo.setText(lecturaPipaDTO.getTipoMedidor()+
                        " - "+getString(R.string.Pipa));
                textView.setText(getString(R.string.registra_porcentaje_estacion)+" "+
                        lecturaPipaDTO.getTipoMedidor()+" de la "+getString(R.string.Pipa));
                EsLecturaInicial = (boolean) extras.get("EsLecturaInicial");
                EsLecturaFinal = (boolean) extras.get("EsLecturaFinal");
                EsLecturaInicialPipa = (boolean) extras.get("EsLecturaInicialPipa");
                EsLecturaFinalPipa = (boolean) extras.get("EsLecturaFinalPipa");
                porcentaje_inicial = lecturaPipaDTO.getPorcentajeMedidor();
                porcentaje_inicial = lecturaDTO.getPorcentajeMedidor();
                if(porcentaje_inicial>0) {
                    String val_per = porcentaje_inicial.toString();
                    numberPickerProcentaje.setValue(Integer.parseInt(val_per.split(".")[0]));
                    numberPickerDecimal.setValue(Integer.parseInt(val_per.split(".")[1]));
                }
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;
            }else if(extras.getBoolean("EsLecturaInicialAlmacen")||
                    extras.getBoolean("EsLecturaFinalAlmacen")){
                lecturaAlmacenDTO = (LecturaAlmacenDTO) extras
                        .getSerializable("lecturaAlmacenDTO");
                EsLecturaInicialAlmacen = extras.getBoolean("EsLecturaInicialAlmacen");
                EsLecturaFinalAlmacen = extras.getBoolean("EsLecturaFinalAlmacen");
                porcentaje_inicial = lecturaAlmacenDTO.getPorcentajeMedidor();
                porcentaje_inicial = lecturaDTO.getPorcentajeMedidor();
                if(porcentaje_inicial>0) {
                    String val_per = porcentaje_inicial.toString();
                    numberPickerProcentaje.setValue(Integer.parseInt(val_per.split(".")[0]));
                    numberPickerDecimal.setValue(Integer.parseInt(val_per.split(".")[1]));
                }
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;
                textViewTitulo.setText(EsLecturaInicial? "Toma de lectura inicial":
                        "Toma de lectura final");
                textView.setText("Registra el porcentaje del "+
                        lecturaAlmacenDTO.getNombreTipoMedidor()+"del almacén pral.");
            }else if(extras.getBoolean("EsRecargaEstacionInicial") ||
                    extras.getBoolean("EsRecargaEstacionFinal")){
                EsRecargaEstacionInicial = extras.getBoolean("EsRecargaEstacionInicial",
                        false);
                EsRecargaEstacionFinal = extras.getBoolean("EsRecargaEstacionFinal",
                        false);
                EsPrimeraLectura = extras.getBoolean("EsPrimeraLectura",false);
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                textViewTitulo.setText("Recarga gas");
                textView.setText("Registra el porcentaje de la estación "+
                    recargaDTO.getNombreMedidorEntrada()
                );
            }else if(extras.getBoolean("EsRecargaPipaFinal") ||
                    extras.getBoolean("EsRecargaPipaInicial")){
                EsRecargaPipaFinal = extras.getBoolean("EsRecargaPipaFinal",false);
                EsRecargaPipaInicial = extras.getBoolean("EsRecargaPipaInicial",false);
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");

            }
        }


        //se pone un valor minimo y maximo para cada number picker
        numberPickerProcentaje.setMaxValue(100);
        numberPickerProcentaje.setMinValue(0);

        numberPickerDecimal.setMinValue(0);
        numberPickerDecimal.setMaxValue(9);


        //se agrega el listener para revisar que se cambio el valor
        numberPickerProcentaje.setOnValueChangedListener(onValueChangeListener);
        numberPickerDecimal.setOnValueChangedListener(onValueChangeListener);


        //onclick del boton
        final Button buttonAceptar = (Button) findViewById(R.id.aceptar_button);
        buttonAceptar.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickAceptar();
            }
        });
    }

    //no se le premite al usuario tener decimales si el valor es 100
    NumberPicker.OnValueChangeListener onValueChangeListener =
            new 	NumberPicker.OnValueChangeListener(){
                @Override
                public void onValueChange(NumberPicker numberPicker, int i, int i1) {
                    if(numberPickerProcentaje.getValue()==100){
                        numberPickerDecimal.setValue(0);
                    }
                }
            };

    //se obtiene el valor y se asinga al objeto correspondiente
    public void onClickAceptar(){
        porcentaje = (numberPickerProcentaje.getValue())+(numberPickerDecimal.getValue()*.10);
        if(papeleta){
            papeletaDTO.setPorcentajeMedidor(porcentaje);
        }else if(iniciar&&almacen){
            iniciarDescargaDTO.setPorcentajeMedidorAlmacen(porcentaje);
        }else if(finalizar&&almacen){
            finalizarDescargaDTO.setPorcentajeMedidorAlmacen(porcentaje);
        }else if(iniciar&&!almacen){
            iniciarDescargaDTO.setPorcentajeMedidorTractor(porcentaje);
        }else if(finalizar&&!almacen){
            finalizarDescargaDTO.setPorcentajeMedidorTractor(porcentaje);
        }else if(EsLecturaInicial || EsLecturaFinal){
            lecturaDTO.setPorcentajeMedidor(porcentaje);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            lecturaPipaDTO.setPorcentajeMedidor(porcentaje);
        }else if (EsLecturaInicialAlmacen ||EsLecturaFinalAlmacen){
            lecturaAlmacenDTO.setPorcentajeMedidor(porcentaje);
        }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            recargaDTO.setProcentajeEntrada(porcentaje);
        }else if(EsRecargaPipaFinal || EsRecargaPipaInicial){
            recargaDTO.setProcentajeEntrada(porcentaje);
        }
        startActivity();
    }

    //se inicia el siguiente activity y se le envian parametros
    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CameraDescargaActivity.class);
        CameraPapeletaActivity.fotoTomada = false;
        if(papeleta){
            intent.putExtra("Papeleta",papeletaDTO);
        }else if(iniciar) {
            intent.putExtra("IniciarDescarga",iniciarDescargaDTO);
            intent.putExtra("TanquePrestado",es_tanque_prestado);
            intent.putExtra("EsLecturaInicial",false);
            intent.putExtra("EsLecturaFinal",false);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsLecturaInicialAlmacen",false);
            intent.putExtra("EsLecturaFinalAlmacen",false);
        }else if(finalizar){
            intent.putExtra("FinalizarDescarga",finalizarDescargaDTO);
            intent.putExtra("EsLecturaInicial",false);
            intent.putExtra("EsLecturaFinal",false);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsLecturaInicialAlmacen",false);
            intent.putExtra("EsLecturaFinalAlmacen",false);
        }else if(EsLecturaInicial || EsLecturaFinal){
            intent.putExtra("lecturaDTO",lecturaDTO);
            intent.putExtra("EsLecturaInicial",EsLecturaInicial);
            intent.putExtra("EsLecturaFinal",EsLecturaFinal);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsLecturaInicialAlmacen",false);
            intent.putExtra("EsLecturaFinalAlmacen",false);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            intent.putExtra("lecturaPipaDTO",lecturaPipaDTO);
            intent.putExtra("EsLecturaInicial",EsLecturaInicial);
            intent.putExtra("EsLecturaFinal",EsLecturaFinal);
            intent.putExtra("EsLecturaInicialPipa",EsLecturaInicialPipa);
            intent.putExtra("EsLecturaFinalPipa",EsLecturaFinalPipa);
            intent.putExtra("EsLecturaInicialAlmacen",false);
            intent.putExtra("EsLecturaFinalAlmacen",false);
        }else if (EsLecturaInicialAlmacen ||EsLecturaFinalAlmacen){
            intent.putExtra("lecturaAlmacenDTO",lecturaAlmacenDTO);
            intent.putExtra("EsLecturaInicial",EsLecturaInicial);
            intent.putExtra("EsLecturaFinal",EsLecturaFinal);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsLecturaInicialAlmacen",EsLecturaInicialAlmacen);
            intent.putExtra("EsLecturaFinalAlmacen",EsLecturaFinalAlmacen);
        }else  if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
            intent.putExtra("lecturaAlmacenDTO",false);
            intent.putExtra("EsLecturaInicial",false);
            intent.putExtra("EsLecturaFinal",false);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsRecargaEstacionInicial",EsRecargaEstacionInicial);
            intent.putExtra("EsRecargaEstacionFinal",EsRecargaEstacionFinal);
            intent.putExtra("EsPrimeraLectura",EsPrimeraLectura);
            intent.putExtra("recargaDTO",recargaDTO);
        }else if(EsRecargaPipaFinal){
            intent.putExtra("EsRecargaPipaFinal", true);
            intent.putExtra("EsLecturaFinal",false);
            intent.putExtra("EsLecturaInicialPipa",false);
            intent.putExtra("EsLecturaFinalPipa",false);
            intent.putExtra("EsRecargaEstacionInicial",false);
            intent.putExtra("EsRecargaEstacionFinal",false);
            intent.putExtra("EsPrimeraLectura",false);
            intent.putExtra("recargaDTO",recargaDTO);
        }
        intent.putExtra("EsPapeleta",papeleta);
        intent.putExtra("EsDescargaIniciar",iniciar);
        intent.putExtra("EsDescargaFinalizar",finalizar);
        intent.putExtra("Almacen",almacen);

        startActivity(intent);
    }

    /**
     * Detecta el keyCode de la pantalla en el activity, esto con relaciòn
     * @param keyCode Código correspondiente a la tecla que se detecta
     * @param event Contiene los agrumentos o caracteristicas del evento cachado
     * @return Boleano para ejecutar la acción
     */
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK) {
            if(iniciar&& almacen) {

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
                        Intent intent = new Intent(CapturaPorcentajeActivity.this, IniciarDescargaActivity.class);
                        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                        startActivity(intent);
                        finish();
                    }
                });
                builder.setCancelable(false);
                builder.show();
                return false;
            }else if (iniciar) {
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
                        finish();
                    }
                });
                builder.setCancelable(false);
                builder.show();
                return false;

            }else{
                return super.onKeyDown(keyCode, event);
            }
        }
        else {
            return super.onKeyDown(keyCode, event);
        }
    }
}
