package com.example.neotecknewts.sagasapp.Activity;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.NumberPicker;

import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionCilindroPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionCilindroPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class PorcentajeCalibracionCilindroActivity extends AppCompatActivity implements
        PorcentajeCalibracionCilindroView{
    Button BTNPorcentajeCalibracionCilindroActivityAceptar;
    NumberPicker NPPorcentajeCalibracionCilindroActivityEntero,
            NPPorcentajeCalibracionCilindroActivityDecimal;

    PorcentajeCalibracionCilindroPresenter presenter;
    Session session;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_porcentaje_calibracion_cilindro);
        presenter = new PorcentajeCalibracionCilindroPresenterImpl(this);
        session = new Session(this);
        NPPorcentajeCalibracionCilindroActivityEntero = findViewById(
                R.id.NPPorcentajeCalibracionCilindroActivityEntero);
        NPPorcentajeCalibracionCilindroActivityDecimal = findViewById(
                R.id.NPPorcentajeCalibracionCilindroActivityDecimal);
        BTNPorcentajeCalibracionCilindroActivityAceptar = findViewById(
                R.id.BTNPorcentajeCalibracionCilindroActivityAceptar);

        BTNPorcentajeCalibracionCilindroActivityAceptar.setOnClickListener(v->{
            Verificar();
        });
    }

    @Override
    public void Verificar() {
        double value = Double.valueOf( NPPorcentajeCalibracionCilindroActivityDecimal.getValue()+"."+
                NPPorcentajeCalibracionCilindroActivityDecimal.getValue());
        if(value>0){

        }else{

        }
    }
}
