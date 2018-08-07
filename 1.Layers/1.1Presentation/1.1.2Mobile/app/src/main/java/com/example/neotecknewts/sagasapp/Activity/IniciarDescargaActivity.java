package com.example.neotecknewts.sagasapp.Activity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.Switch;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Switch;
import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class IniciarDescargaActivity extends AppCompatActivity {

    public Spinner spinnerOrdenCompra;
    public Switch switchTanquePrestado;
    public Spinner spinnerMedidorAlmacen;
    public Spinner spinnerMedidorTractor;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_iniciar_descarga);

        spinnerOrdenCompra = (Spinner)findViewById(R.id.spinner_orden_compra);
        switchTanquePrestado = (Switch)findViewById(R.id.switch_tanque);
        spinnerMedidorAlmacen = (Spinner) findViewById(R.id.spinner_medidor_almacen);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);


        final Button buttonRegistrar = (Button) findViewById(R.id.registrar_button);
        buttonRegistrar.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickRegistrar();
            }
        });
    }


    public void onClickRegistrar(){
        spinnerOrdenCompra.getSelectedItem();
        spinnerMedidorTractor.getSelectedItem();
        spinnerMedidorAlmacen.getSelectedItem();
        switchTanquePrestado.isChecked();

        showDialog(getResources().getString(R.string.message_continuar));

    }
    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_cancel,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        builder1.setPositiveButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        startActivity();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        startActivity(intent);
    }
}

