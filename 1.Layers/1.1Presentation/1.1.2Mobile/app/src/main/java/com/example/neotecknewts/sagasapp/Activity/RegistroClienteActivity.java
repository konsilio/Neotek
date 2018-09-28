package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistroClientePresenter;
import com.example.neotecknewts.sagasapp.Presenter.RegistroClientePresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class RegistroClienteActivity extends AppCompatActivity implements RegistroClienteView{
    Spinner SRegistroClienteActivityTipoPersona,SRegistroClienteActivityRegimenFiscal;
    EditText ETRegistroClienteActivityNombre,ETRegistroClienteActivityApellidoPaterno,
            ETRegistroClienteActivityApellidoMaterno,ETRegistroClienteActivityCelular,
            ETRegistroClienteActivityTelefonoFijo;
    Button BtnRegistroClienteActivityRegistrarCliente,BtnRegistroClienteActivityRegresar;
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    ProgressDialog progressDialog;
    RegistroClientePresenter presenter;
    Session session;
    DatosTipoPersonaDTO datos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro_cliente);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);
        }
        session = new Session(this);
        SRegistroClienteActivityTipoPersona = findViewById(R.id.
                SRegistroClienteActivityTipoPersona);
        SRegistroClienteActivityRegimenFiscal = findViewById(R.id.
                SRegistroClienteActivityRegimenFiscal);
        ETRegistroClienteActivityNombre = findViewById(R.id.ETRegistroClienteActivityNombre);
        ETRegistroClienteActivityApellidoPaterno = findViewById(R.id.
                ETRegistroClienteActivityApellidoPaterno);
        ETRegistroClienteActivityApellidoMaterno = findViewById(R.id.
                ETRegistroClienteActivityApellidoMaterno);
        ETRegistroClienteActivityCelular = findViewById(R.id.ETRegistroClienteActivityCelular);
        ETRegistroClienteActivityTelefonoFijo = findViewById(R.id.
                ETRegistroClienteActivityTelefonoFijo);
        BtnRegistroClienteActivityRegistrarCliente = findViewById(R.id.
                BtnRegistroClienteActivityRegistrarCliente);
        BtnRegistroClienteActivityRegresar = findViewById(R.id.BtnRegistroClienteActivityRegresar);

        SRegistroClienteActivityTipoPersona.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                getResources().getStringArray(R.array.Tipo_persona)
        ));

        SRegistroClienteActivityRegimenFiscal.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                getResources().getStringArray(R.array.Regimen_fiscal)
        ));
        presenter = new RegistroClientePresenterImpl(this);
        presenter.getLista(session.getToken());
        BtnRegistroClienteActivityRegistrarCliente.setOnClickListener(v -> verificarForm());
        BtnRegistroClienteActivityRegresar.setOnClickListener(v -> finish());
    }

    @Override
    public void registrarCliente() {

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
    public void onHideProgress() {
        if (progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onError(DatosTipoPersonaDTO dto) {
        datos = dto;
        String mensaje  = "";
        if(dto!=null) {
            if (datos.getMensajesError().length() > 0) {
                mensaje = datos.getMensaje();
            }
            if (datos.getMensajesError().length() > 0) {
                mensaje = datos.getMensaje();
            }
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle(getString(R.string.error_titulo));
            builder.setMessage(mensaje);
            builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create();
            builder.show();
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle(getString(R.string.error_titulo));
            builder.setMessage("No se ha obtenido respuesta con el servidor, verifique su conexión");
            builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create();
            builder.show();
        }
    }

    @Override
    public void onSuccessDatosFiscales(DatosTipoPersonaDTO dto) {
        datos = dto;
    }

    @Override
    public void verificarForm() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(SRegistroClienteActivityTipoPersona.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El tipo de persona es un valor requerido");
        }
        if(SRegistroClienteActivityRegimenFiscal.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El regiment fiscal es un valor requerido");
        }
        if(ETRegistroClienteActivityNombre.getText().length()<=0){
            error = true;
            mensajes.add("El nombre de la persona es un valor requerido");
        }
        if(ETRegistroClienteActivityApellidoPaterno.getText().length()<=0){
            error = true;
            mensajes.add("El primer apellido es un valor requerido");

        }

        if(error){
            mostrarDialogoErrores(mensajes);
        }else{
            registrarCliente();
        }
    }

    @Override
    public void mostrarDialogoErrores(ArrayList<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje : mensajes) {
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            SRegistroClienteActivityRegimenFiscal.setFocusable(true);
        }));
        builder.create().show();
    }

    @Override
    public void onError(String message) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(getString(R.string.error_titulo));
        builder.setMessage(message);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create();
        builder.show();
    }
}
