package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.security.SecureRandom;
import java.util.Random;

public class PuntoVentaSolicitarActivity extends AppCompatActivity implements PuntoVentaSolicitarView {
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    boolean esGasLP;
    TextView PuntoVentaSolicitarActivityTitulo;
    Button BtnPuntoVentaSolicitarActivitySeguirSinNumero,
            BtnPuntoVentaSolicitarActivityRegistrarCliente,
            BtnPuntoVentaSolicitarActivityBuscarCliente;
    EditText ETPuntoVentaSolicitarActivityBuscador;
    ProgressDialog progressDialog;
    VentaDTO ventaDTO;
    PuntoVentaSolicitarPresenter presenter;
    @SuppressLint("SimpleDateFormat")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_solicitar);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);
            esGasLP = bundle.getBoolean("esGasLP",false);

        }
        presenter = new PuntoVentaSolicitarPresenterImpl(this);
        Session session = new Session(this);
        presenter.hayCorte(session.getToken());
        PuntoVentaSolicitarActivityTitulo = findViewById(R.id.PuntoVentaSolicitarActivityTitulo);
        /*BtnPuntoVentaSolicitarActivitySeguirSinNumero = findViewById(R.id.
                BtnPuntoVentaSolicitarActivitySeguirSinNumero);*/
        BtnPuntoVentaSolicitarActivityRegistrarCliente = findViewById(R.id.
                BtnPuntoVentaSolicitarActivityRegistrarCliente);
        BtnPuntoVentaSolicitarActivityBuscarCliente= findViewById(R.id.
                BtnPuntoVentaSolicitarActivityBuscarCliente);
        ETPuntoVentaSolicitarActivityBuscador = findViewById(R.id.
                ETPuntoVentaSolicitarActivityBuscador);
        ventaDTO = new VentaDTO();
        Date actual = new Date();
        ventaDTO.setHora(new SimpleDateFormat("hh:mm:ss.SSS").format(actual));
        ventaDTO.setCredito(false);
        /*SimpleDateFormat s =
                new SimpleDateFormat("yyyyMMddhhmmss");*/
        SimpleDateFormat s =
                new SimpleDateFormat("MMddhhmmss");
        if(EsVentaCamioneta){
            //String codigo = s.format(actual)+"VC"+getRandomString(4);
            String codigo = String.valueOf(actual.getYear())+
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            Log.w("codigo",codigo);
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Camioneta));
        }else if(EsVentaCarburacion){
            //String codigo = s.format(new Date())+"VEC"+getRandomString(4);
            String codigo = String.valueOf(actual.getYear())+
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            Log.w("codigo",codigo);
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Estacion));
        }else if(EsVentaPipa){
            //String codigo = s.format(new Date())+"VEC"+getRandomString(4);
            String codigo = String.valueOf(actual.getYear())+
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            Log.w("codigo",codigo);
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.pipa));
        }
        Log.w("FolioVenta",ventaDTO.getFolioVenta());

        //BtnPuntoVentaSolicitarActivitySeguirSinNumero.setOnClickListener(v -> SeguirSinNumero());
        BtnPuntoVentaSolicitarActivityRegistrarCliente.setOnClickListener(v -> RegistrarCliente());
        BtnPuntoVentaSolicitarActivityBuscarCliente.setOnClickListener(v-> Buscar());
    }

    @Override
    public void SeguirSinNumero() {
        ventaDTO.setIdCliente(Constantes.IdClienteGeneral);
        ventaDTO.setCredito(false);
        ventaDTO.setFactura(false);
        ventaDTO.setFecha(Utilidades.getCurrentDate(Constantes.FORMATO_FECHA_API));
        ventaDTO.setRFC("");
        ventaDTO.setRazonSocial("");
        ventaDTO.setNombre("");
        ventaDTO.setSinNumero(true);
        ventaDTO.setEsSinNumero(true);
        ventaDTO.setEsRegistro(false);
        ventaDTO.setEsBusqueda(false);
        if(EsVentaCamioneta) {
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        }else if(EsVentaCarburacion){
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);

        }else  if (EsVentaPipa){
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        }
    }

    @Override
    public void RegistrarCliente() {
        ventaDTO.setFecha(Utilidades.getCurrentDate(Constantes.FORMATO_FECHA_API));
        ventaDTO.setSinNumero(false);
        ventaDTO.setEsSinNumero(false);
        ventaDTO.setEsRegistro(true);
        ventaDTO.setEsBusqueda(false);
        Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                RegistroClienteActivity.class);
        intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
        intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
        intent.putExtra("EsVentaPipa",EsVentaPipa);
        intent.putExtra("ventaDTO",ventaDTO);
        intent.putExtra("esGasLP",esGasLP);
        startActivity(intent);
    }

    @Override
    public void Buscar() {
        if(ETPuntoVentaSolicitarActivityBuscador.getText().length()>0) {
            ventaDTO.setFecha(Utilidades.getCurrentDate(Constantes.FORMATO_FECHA_API));
            ventaDTO.setSinNumero(false);
            ventaDTO.setEsSinNumero(false);
            ventaDTO.setEsRegistro(false);
            ventaDTO.setEsBusqueda(true);
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    BuscarClienteActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("criterio", ETPuntoVentaSolicitarActivityBuscador.getText().toString());
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",esGasLP);
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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

    @Override
    public void onResultVerificaCorte(RespuestaCortesAntesVentaDTO data) {
        if(data!=null){
                if(data.isHayCorte()){
                    AlertDialog.Builder builder = new
                            AlertDialog.Builder(this,R.style.AlertDialog);
                    builder.setTitle(R.string.error_titulo);
                    builder.setMessage("Ya se ha realizado un corte del día de hoy por lo cual "+
                    " no se pueden realizar mas ventas ");
                    builder.setCancelable(false);
                    builder.setPositiveButton(
                            R.string.message_acept,
                            (dialog, which) -> {
                                this.finish();
                            }
                    );
                    builder.create().show();
                }
        }
    }
}
