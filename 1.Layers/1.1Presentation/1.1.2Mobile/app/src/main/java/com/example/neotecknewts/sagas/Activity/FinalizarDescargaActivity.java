package com.example.neotecknewts.sagas.Activity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagas.R;

/**
 * Created by neotecknewts on 06/08/18.
 */

public class FinalizarDescargaActivity extends AppCompatActivity {
    public Spinner spinnerOrdenCompra;
    public LinearLayout linearLayoutTanque;
    public Spinner spinnerMedidorAlmacen;
    public Spinner spinnerMedidorTractor;
    public TextView textViewTitulo;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_iniciar_descarga);

        spinnerOrdenCompra = (Spinner)findViewById(R.id.spinner_orden_compra);
        linearLayoutTanque = (LinearLayout) findViewById(R.id.layout_tanque);
        spinnerMedidorAlmacen = (Spinner) findViewById(R.id.spinner_medidor_almacen);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);
        textViewTitulo = (TextView) findViewById(R.id.textTitulo);

        linearLayoutTanque.setVisibility(View.GONE);
        textViewTitulo.setText(R.string.title_finalizar_descarga);


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
