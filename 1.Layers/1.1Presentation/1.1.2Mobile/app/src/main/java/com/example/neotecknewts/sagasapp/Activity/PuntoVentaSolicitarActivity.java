package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;

public class PuntoVentaSolicitarActivity extends AppCompatActivity implements PuntoVentaSolicitarView {
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    TextView PuntoVentaSolicitarActivityTitulo;
    Button BtnPuntoVentaSolicitarActivitySeguirSinNumero,
            BtnPuntoVentaSolicitarActivityRegistrarCliente,
            BtnPuntoVentaSolicitarActivityBuscarCliente;
    EditText ETPuntoVentaSolicitarActivityBuscador;
    ProgressDialog progressDialog;
    VentaDTO ventaDTO;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_solicitar);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);
        }
        PuntoVentaSolicitarActivityTitulo = findViewById(R.id.PuntoVentaSolicitarActivityTitulo);
        BtnPuntoVentaSolicitarActivitySeguirSinNumero = findViewById(R.id.
                BtnPuntoVentaSolicitarActivitySeguirSinNumero);
        BtnPuntoVentaSolicitarActivityRegistrarCliente = findViewById(R.id.
                BtnPuntoVentaSolicitarActivityRegistrarCliente);
        BtnPuntoVentaSolicitarActivityBuscarCliente= findViewById(R.id.
                BtnPuntoVentaSolicitarActivityBuscarCliente);
        ETPuntoVentaSolicitarActivityBuscador = findViewById(R.id.
                ETPuntoVentaSolicitarActivityBuscador);
        if(EsVentaCamioneta){
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Camioneta));
        }else if(EsVentaCarburacion){
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Estacion));
        }else if(EsVentaPipa){
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.pipa));
        }
        ventaDTO = new VentaDTO();
        BtnPuntoVentaSolicitarActivitySeguirSinNumero.setOnClickListener(v -> SeguirSinNumero());
        BtnPuntoVentaSolicitarActivityRegistrarCliente.setOnClickListener(v -> RegistrarCliente());
        BtnPuntoVentaSolicitarActivityBuscarCliente.setOnClickListener(v-> Buscar());
    }

    @Override
    public void SeguirSinNumero() {
        if(EsVentaCamioneta) {
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    VentaGasActivity.class);
            ventaDTO.setIdCliente(1);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        }else if(EsVentaCarburacion){
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    PuntoVentaGasListaActivity.class);
            ventaDTO.setIdCliente(1);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);

        }else  if (EsVentaPipa){
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    PuntoVentaGasListaActivity.class);
            ventaDTO.setIdCliente(1);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        }
    }

    @Override
    public void RegistrarCliente() {
        Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                RegistroClienteActivity.class);
        intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
        intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
        intent.putExtra("EsVentaPipa",EsVentaPipa);
        intent.putExtra("ventaDTO",ventaDTO);
        startActivity(intent);
    }

    @Override
    public void Buscar() {
        if(ETPuntoVentaSolicitarActivityBuscador.getText().length()>0) {
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    BuscarClienteActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("criterio", ETPuntoVentaSolicitarActivityBuscador.getText().toString());
            intent.putExtra("ventaDTO",ventaDTO);
            startActivity(intent);
        }
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void onHiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }
}
