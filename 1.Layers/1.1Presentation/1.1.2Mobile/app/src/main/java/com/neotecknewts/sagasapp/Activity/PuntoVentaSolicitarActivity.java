package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenter;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaSolicitarPresenterImpl;
import com.neotecknewts.sagasapp.Util.Constantes;
import com.neotecknewts.sagasapp.Util.Session;
import com.neotecknewts.sagasapp.Util.Utilidades;

import java.io.IOException;
import java.security.SecureRandom;
import java.text.SimpleDateFormat;
import java.util.Date;

public class PuntoVentaSolicitarActivity extends AppCompatActivity implements PuntoVentaSolicitarView {
    boolean EsVentaCarburacion, EsVentaCamioneta, EsVentaPipa;
    boolean esGasLP;
    TextView PuntoVentaSolicitarActivityTitulo;
    Button BtnPuntoVentaSolicitarActivitySeguirSinNumero,
            BtnPuntoVentaSolicitarActivityRegistrarCliente,
            BtnPuntoVentaSolicitarActivityBuscarCliente;
    EditText ETPuntoVentaSolicitarActivityBuscador;
    ProgressDialog progressDialog;
    VentaDTO ventaDTO;
    PuntoVentaSolicitarPresenter presenter;
    static final String AB = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    static SecureRandom rnd = new SecureRandom();


    @SuppressLint("SimpleDateFormat")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_solicitar);
        Bundle bundle = getIntent().getExtras();
        if (bundle != null) {
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion", false);
            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta", false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa", false);
            esGasLP = bundle.getBoolean("esGasLP", false);

        }
        presenter = new PuntoVentaSolicitarPresenterImpl(this, PuntoVentaSolicitarActivity.this);
        Session session = new Session(this);
        String mail = session.getEmail();
        mail =  mail.substring(0, mail.indexOf('@')).replace('.', ' ');
        setTitle("Punto de venta - " + mail);
        if (isOnline())
            presenter.hayCorte(session.getToken());
        PuntoVentaSolicitarActivityTitulo = findViewById(R.id.PuntoVentaSolicitarActivityTitulo);
        /*BtnPuntoVentaSolicitarActivitySeguirSinNumero = findViewById(R.id.
                BtnPuntoVentaSolicitarActivitySeguirSinNumero);*/
        BtnPuntoVentaSolicitarActivityRegistrarCliente = findViewById(R.id.
                BtnPuntoVentaSolicitarActivityRegistrarCliente);
        BtnPuntoVentaSolicitarActivityBuscarCliente = findViewById(R.id.
                BtnPuntoVentaSolicitarActivityBuscarCliente);
        ETPuntoVentaSolicitarActivityBuscador = findViewById(R.id.
                ETPuntoVentaSolicitarActivityBuscador);
        ventaDTO = new VentaDTO();
        Date actual = new Date();
        ventaDTO.setHora(new SimpleDateFormat("hh:mm:ss.SSS").format(actual));
        ventaDTO.setCredito(false);
        ventaDTO.setBonificacion(false);
        /*SimpleDateFormat s =
                new SimpleDateFormat("yyyyMMddhhmmss");*/
        // SimpleDateFormat s = new SimpleDateFormat("MMddhhmmssSSS");
        SimpleDateFormat s = new SimpleDateFormat("MMddhhmmss");
        if (EsVentaCamioneta) {
            String codigo = String.valueOf(actual.getYear()) + randomString(2) +
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Camioneta));
        } else if (EsVentaCarburacion) {
            String codigo = String.valueOf(actual.getYear()) + randomString(2) +
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.Estacion));
        } else if (EsVentaPipa) {
            String codigo = String.valueOf(actual.getYear()) + randomString(2) +
                    Integer.toHexString(Integer.parseInt(s.format(actual)));
            ventaDTO.setFolioVenta(codigo.toUpperCase());
            PuntoVentaSolicitarActivityTitulo.setText(getString(R.string.pipa));
        }

        //BtnPuntoVentaSolicitarActivitySeguirSinNumero.setOnClickListener(v -> SeguirSinNumero());
        BtnPuntoVentaSolicitarActivityRegistrarCliente.setOnClickListener(v -> RegistrarCliente());
        BtnPuntoVentaSolicitarActivityBuscarCliente.setOnClickListener(v -> Buscar());
    }

    private String randomString( int len ){
        StringBuilder sb = new StringBuilder( len );
        for( int i = 0; i < len; i++ )
            sb.append( AB.charAt( rnd.nextInt(AB.length()) ) );
        return sb.toString();
    }

    @Override
    public void SeguirSinNumero() {

        ventaDTO.setIdCliente(Constantes.IdClienteGeneral);
        ventaDTO.setCredito(false);
        ventaDTO.setBonificacion(true);
        ventaDTO.setFactura(false);
        ventaDTO.setFecha(Utilidades.getCurrentDate(Constantes.FORMATO_FECHA_API));
        ventaDTO.setRFC("");
        ventaDTO.setRazonSocial("");
        ventaDTO.setNombre("");
        ventaDTO.setSinNumero(true);
        ventaDTO.setEsSinNumero(true);
        ventaDTO.setEsRegistro(false);
        ventaDTO.setEsBusqueda(false);
        if (EsVentaCamioneta) {
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        } else if (EsVentaCarburacion) {
            Intent intent = new Intent(PuntoVentaSolicitarActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);

        } else if (EsVentaPipa) {
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
        intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
        intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
        intent.putExtra("EsVentaPipa", EsVentaPipa);
        intent.putExtra("ventaDTO", ventaDTO);
        intent.putExtra("esGasLP", esGasLP);
        startActivity(intent);
    }

    @Override
    public void Buscar() {
        if (ETPuntoVentaSolicitarActivityBuscador.getText().length() > 0) {
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
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("esGasLP", esGasLP);
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void onHiddeProgress() {
        if (progressDialog != null && progressDialog.isShowing()) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onResultVerificaCorte(RespuestaCortesAntesVentaDTO data) {
        if (data != null) {
            if (data.isHayCorte()) {
                AlertDialog.Builder builder = new
                        AlertDialog.Builder(this, R.style.AlertDialog);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage("Ya se ha realizado un corte del día de hoy por lo cual " +
                        " no se pueden realizar mas ventas ");
                builder.setCancelable(false);
                builder.setPositiveButton(+
                                R.string.message_acept,
                        (dialog, which) -> {
                            this.finish();
                        }
                );
                builder.create().show();
            }
        }
    }

    public boolean isOnline() {
        Runtime runtime = Runtime.getRuntime();
        try {
            Process ipProcess = runtime.exec("/system/bin/ping -c 1 8.8.8.8");
            int exitValue = ipProcess.waitFor();
            return (exitValue == 0);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }
}
