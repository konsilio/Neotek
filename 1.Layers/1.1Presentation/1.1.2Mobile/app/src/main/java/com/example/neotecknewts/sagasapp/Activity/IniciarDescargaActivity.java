package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
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

import com.example.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.IniciarDescargaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.IniciarDescargaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.List;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class IniciarDescargaActivity extends AppCompatActivity implements IniciarDescargaView{

    public Spinner spinnerOrdenCompra;
    public Switch switchTanquePrestado;
    public Spinner spinnerMedidorAlmacen;
    public Spinner spinnerMedidorTractor;
    ProgressDialog progressDialog;
    public Session session;

    public OrdenCompraDTO ordenCompraDTO;
    List<OrdenCompraDTO> ordenesCompraDTO;

    public IniciarDescargaPresenter presenter;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_iniciar_descarga);

        spinnerOrdenCompra = (Spinner)findViewById(R.id.spinner_orden_compra);
        switchTanquePrestado = (Switch)findViewById(R.id.switch_tanque);
        spinnerMedidorAlmacen = (Spinner) findViewById(R.id.spinner_medidor_almacen);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);

        presenter = new IniciarDescargaPresenterImpl(this);

        String[] ordenes = {"prueba1", "prueba2"};
        String[] medidores = {"Rotogate", "Magnatel"};


        spinnerOrdenCompra.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        spinnerMedidorAlmacen.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));
        spinnerMedidorTractor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));

        final Button buttonRegistrar = (Button) findViewById(R.id.registrar_button);
        buttonRegistrar.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickRegistrar();
            }
        });

        presenter.getOrdenesCompra(1,session.getTokenWithBearer());
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

    private void showDialogAceptar(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        startActivity(intent);
    }

    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    @Override
    public void hideProgress() {
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    @Override
    public void messageError(int mensaje) {
        showDialogAceptar(getResources().getString(mensaje));
    }

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        Log.w("VIEW", respuestaOrdenesCompraDTO.getOrdenesCompra().size()+"");
        this.ordenesCompraDTO = respuestaOrdenesCompraDTO.getOrdenesCompra();
        String[] ordenes = new String[ordenesCompraDTO.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTO.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompra.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
    }
}

