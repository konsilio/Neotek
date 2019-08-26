package com.neotecknewts.sagasapp.Activity;

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
    public TextView TVCapturaPorcentajeActivityTotalGas;

    //objetos a completar con el porcentaje obtenido
    PrecargaPapeletaDTO papeletaDTO;
    IniciarDescargaDTO iniciarDescargaDTO;
    FinalizarDescargaDTO finalizarDescargaDTO;
    LecturaDTO lecturaDTO;
    LecturaPipaDTO lecturaPipaDTO;
    LecturaAlmacenDTO lecturaAlmacenDTO;
    RecargaDTO recargaDTO;
    AutoconsumoDTO autoconsumoDTO;
    TraspasoDTO traspasoDTO;
    CalibracionDTO calibracionDTO;

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
    public boolean EsAutoconsumoPipaInicial,EsAutoconsumoPipaFinal;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    public boolean EsCalibracionEstacionInicial,EsCalibracionEstacionFinal;
    public boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal;

    //Variable para guardar total de gas
    double TotalGas;

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
        TVCapturaPorcentajeActivityTotalGas = findViewById(R.id.TVCapturaPorcentajeActivityTotalGas);
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
                    //double val_per =  Double.valueOf(porcentaje_inicial.toString());
                    int index = porcentaje_inicial.toString().indexOf(".");
                    String entero = porcentaje_inicial.toString().substring(
                            0,index
                    );
                    String decimal = porcentaje_inicial.toString().substring(
                            index+1,
                            porcentaje_inicial.toString().length()
                    );

                    numberPickerProcentaje.setValue(Integer.parseInt(entero));
                    numberPickerDecimal.setValue(Integer.parseInt(decimal));
                }
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;
                setTitle(R.string.toma_de_lectura);
            }else if (extras.getBoolean("EsLecturaInicialPipa") ||
                    extras.getBoolean("EsLecturaFinalPipa")){
                lecturaPipaDTO = (LecturaPipaDTO) extras.getSerializable("lecturaPipaDTO");
                String pipa_nombre = lecturaPipaDTO.getNombrePipa().isEmpty()?
                        " ":": "+lecturaPipaDTO.getNombrePipa();
                textViewTitulo.setText(lecturaPipaDTO.getTipoMedidor()+
                        " - "+getString(R.string.Pipa)+ pipa_nombre);
                textView.setText(getString(R.string.registra_porcentaje_estacion)+" "+
                        lecturaPipaDTO.getTipoMedidor()+" de la "+getString(R.string.Pipa));
                EsLecturaInicial = (boolean) extras.get("EsLecturaInicial");
                EsLecturaFinal = (boolean) extras.get("EsLecturaFinal");
                EsLecturaInicialPipa = (boolean) extras.get("EsLecturaInicialPipa");
                EsLecturaFinalPipa = (boolean) extras.get("EsLecturaFinalPipa");
                porcentaje_inicial = lecturaPipaDTO.getPorcentajeMedidor();
                Log.d("CapturaPorcentaje: ", lecturaPipaDTO.getPorcentajeMedidor()+"");
                if(porcentaje_inicial>0) {
                    int parte_entera = porcentaje_inicial.intValue();
                    int parte_decimal = (int) (porcentaje_inicial - parte_entera);

                    numberPickerProcentaje.setValue(parte_entera);
                    numberPickerDecimal.setValue(parte_decimal);
                }
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;
                setTitle(R.string.toma_de_lectura);
            }else if(extras.getBoolean("EsLecturaInicialAlmacen")||
                    extras.getBoolean("EsLecturaFinalAlmacen")){
                lecturaAlmacenDTO = (LecturaAlmacenDTO) extras
                        .getSerializable("lecturaAlmacenDTO");
                EsLecturaInicialAlmacen = extras.getBoolean("EsLecturaInicialAlmacen");
                EsLecturaFinalAlmacen = extras.getBoolean("EsLecturaFinalAlmacen");
                porcentaje_inicial = lecturaAlmacenDTO.getPorcentajeMedidor();
                //porcentaje_inicial = lecturaDTO.getPorcentajeMedidor();
                Log.d("CapturaPorcentaje: ", lecturaPipaDTO.getPorcentajeMedidor()+"");
                Log.d("CapturaPorcentaje: ", lecturaDTO.getPorcentajeMedidor()+"");
                if(porcentaje_inicial>0) {
                    int parte_entera = porcentaje_inicial.intValue();
                    int parte_decimal = (int) (porcentaje_inicial - parte_entera);

                    numberPickerProcentaje.setValue(parte_entera);
                    numberPickerDecimal.setValue(parte_decimal);
                }
                EsLecturaInicial = false;
                EsLecturaFinal = false;
                EsLecturaInicialPipa = false;
                EsLecturaFinalPipa = false;
                papeleta=false;
                iniciar=false;
                finalizar=false;
                almacen=false;
                textViewTitulo.setText(EsLecturaInicialAlmacen? "Toma de lectura inicial":
                        "Toma de lectura final");
                textView.setText("Registra el porcentaje del "+
                        lecturaAlmacenDTO.getNombreTipoMedidor()+" del almacén pral.");
                setTitle(R.string.toma_de_lectura);
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
                setTitle(R.string.recarga);
            }else if(extras.getBoolean("EsRecargaPipaFinal") ||
                    extras.getBoolean("EsRecargaPipaInicial")){
                EsRecargaPipaFinal = extras.getBoolean("EsRecargaPipaFinal",false);
                EsRecargaPipaInicial = extras.getBoolean("EsRecargaPipaInicial",false);
                recargaDTO = (RecargaDTO) extras.getSerializable("recargaDTO");
                setTitle(R.string.recarga);
            }else if(extras.getBoolean("EsAutoconsumoPipaInicial")|| extras.getBoolean("EsAutoconsumoPipaFinal")){
                EsAutoconsumoPipaInicial = extras.getBoolean("EsAutoconsumoPipaInicial",false);
                EsAutoconsumoPipaFinal = extras.getBoolean("EsAutoconsumoPipaFinal",false);
                autoconsumoDTO = (AutoconsumoDTO) extras.getSerializable("autoconsumoDTO");
                textView.setText("Registra el porcentaje del "+
                        autoconsumoDTO.getNombreTipoMedidor()+
                        " de la pipa"
                );
                textViewTitulo.setText(autoconsumoDTO.getNombreTipoMedidor()+" - Pipa");
                setTitle("Autoconsumo");
            }else if(extras.getBoolean("EsTraspasoEstacionInicial",false)|| extras.getBoolean("EsTraspasoEstacionFinal",false)){
                EsTraspasoEstacionInicial = extras.getBoolean("EsTraspasoEstacionInicial",false);
                EsTraspasoEstacionFinal = extras.getBoolean("EsTraspasoEstacionFinal",false);
                EsPrimeraParteTraspaso = extras.getBoolean("EsPrimeraParteTraspaso",true);
                traspasoDTO = (TraspasoDTO) extras.getSerializable("traspasoDTO");
                if(EsPrimeraParteTraspaso)
                    textView.setText("Registra el porcentaje del "+traspasoDTO.getNombreMedidor()+" de" +
                        "la estación");
                else
                    textView.setText("Registra el porcentaje del "+traspasoDTO.getNombreMedidor()+" de" +
                            "la Pipa");
                setTitle(R.string.Traspaso);
            }else if(extras.getBoolean("EsCalibracionEstacionInicial",false) ||
                    extras.getBoolean("EsCalibracionEstacionFinal",false)){
                EsCalibracionEstacionInicial = extras.getBoolean("EsCalibracionEstacionInicial",
                        false);
                EsCalibracionEstacionFinal = extras.getBoolean("EsCalibracionEstacionFinal",
                        false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                textViewTitulo.setText((EsCalibracionEstacionInicial) ?
                        getString( R.string.Calibracion)+" - Inicial":
                        getString(R.string.Calibracion)+" - Final"
                );
                porcentaje_inicial = calibracionDTO.getPorcentajeCalibracion();
                //porcentaje_inicial = lecturaDTO.getPorcentajeMedidor();
                if(porcentaje_inicial>0) {
                    int parte_entera = porcentaje_inicial.intValue();
                    int parte_decimal = (int) (porcentaje_inicial - parte_entera);

                    numberPickerProcentaje.setValue(parte_entera);
                    numberPickerDecimal.setValue(parte_decimal);
                }
                textView.setText("Registra el porcentaje del magnatel de la Estación");
                setTitle(R.string.Calibracion);
            }else if(extras.getBoolean("EsCalibracionPipaInicial",false) ||
                    extras.getBoolean("EsCalibracionPipaFinal",false)){
                EsCalibracionPipaInicial = extras.getBoolean("EsCalibracionPipaInicial",
                        false);
                EsCalibracionPipaFinal = extras.getBoolean("EsCalibracionPipaFinal",
                        false);
                calibracionDTO = (CalibracionDTO) extras.getSerializable("calibracionDTO");
                textViewTitulo.setText((EsCalibracionEstacionInicial) ?
                        getString( R.string.Calibracion)+" - Inicial":
                        getString(R.string.Calibracion)+" - Final"
                );
                textView.setText("Registra el porcentaje del magnatel de la pipa");
                setTitle(R.string.Calibracion);
            }
        }
        //region Permite mostrar calculo aprox. de gas
        if(EsLecturaInicial || EsLecturaFinal){
            TVCapturaPorcentajeActivityTotalGas.setVisibility(View.VISIBLE);
        }else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen){
            TVCapturaPorcentajeActivityTotalGas.setVisibility(View.VISIBLE);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            TVCapturaPorcentajeActivityTotalGas.setVisibility(View.VISIBLE);
        }else{
            TVCapturaPorcentajeActivityTotalGas.setVisibility(View.GONE);
        }
        //endregion

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
                    double porcentajeCalculo = (numberPickerProcentaje.getValue())
                            +(numberPickerDecimal.getValue()*.10);
                    if(EsLecturaInicial || EsLecturaFinal){

                        TotalGas = (lecturaDTO.getCapacidadAlmacen()  *porcentajeCalculo )/100;
                        TVCapturaPorcentajeActivityTotalGas.setText(
                                "Total de gas aproximado: "+String.valueOf(TotalGas)+"Lt.");
                    }else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen){
                        TotalGas = (lecturaAlmacenDTO.getCapacidadAlmacen()  *porcentajeCalculo )/100;
                        TVCapturaPorcentajeActivityTotalGas.setText(
                                "Total de gas aproximado: "+String.valueOf(TotalGas)+"Lt.");
                    }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
                        TotalGas = (lecturaPipaDTO.getCapacidadAlmacen()  *porcentajeCalculo )/100;
                        TVCapturaPorcentajeActivityTotalGas.setText(
                                "Total de gas aproximado: "+String.valueOf(TotalGas)+"Lt.");
                    }
                    //Log.v("Total aproximado de gas",String.valueOf(TotalGas));
                    if(numberPickerProcentaje.getValue()==100){
                        numberPickerDecimal.setValue(0);
                    }
                }
            };

    //se obtiene el valor y se asinga al objeto correspondiente
    public void onClickAceptar(){

            porcentaje = (numberPickerProcentaje.getValue()) + (numberPickerDecimal.getValue()*.10)-1.1;

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
        }else if(EsAutoconsumoPipaInicial||EsAutoconsumoPipaFinal){
            autoconsumoDTO.setPorcentajeMedidor(porcentaje);
        }else if(EsTraspasoEstacionInicial ||EsTraspasoEstacionFinal){
            traspasoDTO.setPorcentajeSalida(porcentaje);
        }else if (EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            calibracionDTO.setPorcentajeMedidor2(porcentaje);
        }else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            calibracionDTO.setPorcentajeMedidor2(porcentaje);
        }
        startActivity();
    }

    //se inicia el siguiente activity y se le envian parametros
    public void startActivity(){
        if(EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
            Intent intent = new Intent(CapturaPorcentajeActivity.this,
                    CameraDescargaActivity.class);
            intent.putExtra("EsAutoconsumoPipaInicial",EsAutoconsumoPipaInicial);
            intent.putExtra("EsAutoconsumoPipaFinal",EsAutoconsumoPipaFinal);
            intent.putExtra("autoconsumoDTO",autoconsumoDTO);
            startActivity(intent);
        }else {
            Intent intent = new Intent(getApplicationContext(), CameraDescargaActivity.class);
            CameraPapeletaActivity.fotoTomada = false;
            if (papeleta) {
                intent.putExtra("Papeleta", papeletaDTO);
            } else if (iniciar) {
                intent.putExtra("IniciarDescarga", iniciarDescargaDTO);
                intent.putExtra("TanquePrestado", es_tanque_prestado);
                intent.putExtra("EsLecturaInicial", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsLecturaInicialAlmacen", false);
                intent.putExtra("EsLecturaFinalAlmacen", false);
            } else if (finalizar) {
                intent.putExtra("FinalizarDescarga", finalizarDescargaDTO);
                intent.putExtra("EsLecturaInicial", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsLecturaInicialAlmacen", false);
                intent.putExtra("EsLecturaFinalAlmacen", false);
            } else if (EsLecturaInicial || EsLecturaFinal) {
                intent.putExtra("lecturaDTO", lecturaDTO);
                intent.putExtra("EsLecturaInicial", EsLecturaInicial);
                intent.putExtra("EsLecturaFinal", EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsLecturaInicialAlmacen", false);
                intent.putExtra("EsLecturaFinalAlmacen", false);
            } else if (EsLecturaInicialPipa || EsLecturaFinalPipa) {
                intent.putExtra("lecturaPipaDTO", lecturaPipaDTO);
                intent.putExtra("EsLecturaInicial", EsLecturaInicial);
                intent.putExtra("EsLecturaFinal", EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa", EsLecturaInicialPipa);
                intent.putExtra("EsLecturaFinalPipa", EsLecturaFinalPipa);
                intent.putExtra("EsLecturaInicialAlmacen", false);
                intent.putExtra("EsLecturaFinalAlmacen", false);
            } else if (EsLecturaInicialAlmacen || EsLecturaFinalAlmacen) {
                intent.putExtra("lecturaAlmacenDTO", lecturaAlmacenDTO);
                intent.putExtra("EsLecturaInicial", EsLecturaInicial);
                intent.putExtra("EsLecturaFinal", EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsLecturaInicialAlmacen", EsLecturaInicialAlmacen);
                intent.putExtra("EsLecturaFinalAlmacen", EsLecturaFinalAlmacen);
            } else if (EsRecargaEstacionInicial || EsRecargaEstacionFinal) {
                intent.putExtra("lecturaAlmacenDTO", false);
                intent.putExtra("EsLecturaInicial", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsRecargaEstacionInicial", EsRecargaEstacionInicial);
                intent.putExtra("EsRecargaEstacionFinal", EsRecargaEstacionFinal);
                intent.putExtra("EsPrimeraLectura", EsPrimeraLectura);
                intent.putExtra("recargaDTO", recargaDTO);
            } else if (EsRecargaPipaFinal) {
                intent.putExtra("EsRecargaPipaFinal", true);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsRecargaEstacionInicial", false);
                intent.putExtra("EsRecargaEstacionFinal", false);
                intent.putExtra("EsPrimeraLectura", false);
                intent.putExtra("recargaDTO", recargaDTO);
            }else if(EsTraspasoEstacionInicial||EsTraspasoEstacionFinal){
                intent.putExtra("EsRecargaPipaFinal", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsRecargaEstacionInicial", false);
                intent.putExtra("EsRecargaEstacionFinal", false);
                intent.putExtra("EsPrimeraLectura", false);
                intent.putExtra("EsTraspasoEstacionInicial",EsTraspasoEstacionInicial);
                intent.putExtra("EsTraspasoEstacionFinal",EsTraspasoEstacionFinal);
                intent.putExtra("EsPrimeraParteTraspaso",EsPrimeraParteTraspaso);
                intent.putExtra("traspasoDTO",traspasoDTO);
            }else if (EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
                intent.putExtra("EsRecargaPipaFinal", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsRecargaEstacionInicial", false);
                intent.putExtra("EsRecargaEstacionFinal", false);
                intent.putExtra("EsPrimeraLectura", false);
                intent.putExtra("EsTraspasoEstacionInicial",false);
                intent.putExtra("EsTraspasoEstacionFinal",false);
                intent.putExtra("EsPrimeraParteTraspaso",false);
                intent.putExtra("EsCalibracionEstacionInicial",EsCalibracionEstacionInicial);
                intent.putExtra("EsCalibracionEstacionFinal",EsCalibracionEstacionFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
            }else if (EsCalibracionPipaInicial || EsCalibracionPipaFinal){
                intent.putExtra("EsRecargaPipaFinal", false);
                intent.putExtra("EsLecturaFinal", false);
                intent.putExtra("EsLecturaInicialPipa", false);
                intent.putExtra("EsLecturaFinalPipa", false);
                intent.putExtra("EsRecargaEstacionInicial", false);
                intent.putExtra("EsRecargaEstacionFinal", false);
                intent.putExtra("EsPrimeraLectura", false);
                intent.putExtra("EsTraspasoEstacionInicial",false);
                intent.putExtra("EsTraspasoEstacionFinal",false);
                intent.putExtra("EsPrimeraParteTraspaso",false);
                intent.putExtra("EsCalibracionEstacionInicial",false);
                intent.putExtra("EsCalibracionEstacionFinal",false);
                intent.putExtra("EsCalibracionPipaInicial",EsCalibracionPipaInicial);
                intent.putExtra("EsCalibracionPipaFinal",EsCalibracionPipaFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
            }
            intent.putExtra("EsPapeleta", papeleta);
            intent.putExtra("EsDescargaIniciar", iniciar);
            intent.putExtra("EsDescargaFinalizar", finalizar);
            intent.putExtra("Almacen", almacen);

            startActivity(intent);
        }
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

                AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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
                AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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
