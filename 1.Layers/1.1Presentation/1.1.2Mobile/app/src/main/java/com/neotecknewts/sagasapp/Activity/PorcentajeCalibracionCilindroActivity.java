package com.neotecknewts.sagasapp.Activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.Button;
import android.widget.NumberPicker;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionCilindroPresenter;
import com.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionCilindroPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

public class PorcentajeCalibracionCilindroActivity extends AppCompatActivity implements
        PorcentajeCalibracionCilindroView {
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
