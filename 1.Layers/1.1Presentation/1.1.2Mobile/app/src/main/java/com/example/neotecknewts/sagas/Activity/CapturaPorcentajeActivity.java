package com.example.neotecknewts.sagas.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import com.example.neotecknewts.sagas.R;
/**
 * Created by neotecknewts on 03/08/18.
 */

public class CapturaPorcentajeActivity extends AppCompatActivity {

    public Double porcentaje;
    public NumberPicker numberPickerProcentaje;
    public NumberPicker numberPickerDecimal;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_captura_procentaje);

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
        startActivity();
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CameraActivity.class);
        CameraActivity.fotoTomada = false;
        startActivity(intent);
    }




}
