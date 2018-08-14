package com.example.neotecknewts.sagasapp.Activity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class CapturaPorcentajeActivity extends AppCompatActivity {

    public Double porcentaje;
    public NumberPicker numberPickerProcentaje;
    public NumberPicker numberPickerDecimal;
    PrecargaPapeletaDTO papeletaDTO;
    IniciarDescargaDTO iniciarDescargaDTO;
    FinalizarDescargaDTO finalizarDescargaDTO;
    public String tipoMedidor;
    public boolean papeleta;
    public boolean iniciar;
    public boolean finalizar;
    public boolean almacen;
    public TextView textViewTitulo;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_captura_porcentaje);


        textViewTitulo = (TextView) findViewById(R.id.textViewTituloPorcentaje) ;
        Bundle extras = getIntent().getExtras();

        if (extras !=null){
            if(extras.getBoolean("EsPapeleta")) {
                papeletaDTO = (PrecargaPapeletaDTO) extras.getSerializable("Papeleta");
                Log.w("Image", Uri.parse(papeletaDTO.getImagenesURI().get(0).toString()) + "");
                tipoMedidor = extras.getString("TipoMedidor");
                Log.w("Medidor", tipoMedidor + "");
                papeleta=true;
                iniciar=false;
                finalizar=false;
                textViewTitulo.setText(tipoMedidor+" - Tractor");
            }else if(extras.getBoolean("EsDescargaIniciar")){
                    iniciarDescargaDTO = (IniciarDescargaDTO) extras.getSerializable("IniciarDescarga");
                    papeleta = false;
                    iniciar = true;
                    finalizar = false;
                    almacen = extras.getBoolean("Almacen");
                if (almacen) {
                    textViewTitulo.setText("Medidor - Almacen");
                }else{
                    textViewTitulo.setText("Medidor - Tractor");
                }
            }
            else if(extras.getBoolean("EsDescargaFinalizar")){
                finalizarDescargaDTO = (FinalizarDescargaDTO) extras.getSerializable("FinalizarDescarga");
                papeleta=false;
                iniciar=false;
                finalizar=true;
                almacen = extras.getBoolean("Almacen");
                if (almacen) {
                    textViewTitulo.setText("Medidor - Almacen");
                }else{
                    textViewTitulo.setText("Medidor - Tractor");
                }
            }
        }

        numberPickerDecimal = (NumberPicker) findViewById(R.id.number_picker_decimal);
        numberPickerProcentaje = (NumberPicker) findViewById(R.id.number_picker_porcentaje);

        numberPickerProcentaje.setMaxValue(100);
        numberPickerProcentaje.setMinValue(0);

        numberPickerDecimal.setMinValue(0);
        numberPickerDecimal.setMaxValue(9);

        numberPickerProcentaje.setOnValueChangedListener(onValueChangeListener);
        numberPickerDecimal.setOnValueChangedListener(onValueChangeListener);


        final Button buttonAceptar = (Button) findViewById(R.id.aceptar_button);
        buttonAceptar.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickAceptar();
            }
        });
    }

    NumberPicker.OnValueChangeListener onValueChangeListener =
            new 	NumberPicker.OnValueChangeListener(){
                @Override
                public void onValueChange(NumberPicker numberPicker, int i, int i1) {
                    if(numberPickerProcentaje.getValue()==100){
                        numberPickerDecimal.setValue(0);
                    }
                }
            };
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
        }
        startActivity();
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CameraDescargaActivity.class);
        CameraPapeletaActivity.fotoTomada = false;
        if(papeleta){
            intent.putExtra("TipoMedidor",tipoMedidor);
            intent.putExtra("Papeleta",papeletaDTO);
        }else if(iniciar) {
            intent.putExtra("IniciarDescarga",iniciarDescargaDTO);
        }else if(finalizar){
            intent.putExtra("FinalizarDescarga",finalizarDescargaDTO);
        }
        intent.putExtra("EsPapeleta",papeleta);
        intent.putExtra("EsDescargaIniciar",iniciar);
        intent.putExtra("EsDescargaFinalizar",finalizar);
        intent.putExtra("Almacen",almacen);
        startActivity(intent);
    }




}
