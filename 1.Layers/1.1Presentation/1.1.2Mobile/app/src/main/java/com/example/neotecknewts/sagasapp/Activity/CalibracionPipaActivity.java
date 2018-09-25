package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Build;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.example.neotecknewts.sagasapp.Presenter.CalibracionPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.CalibracionPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class CalibracionPipaActivity extends AppCompatActivity implements CalibracionPipaView{

    TextView TVCalibracionPipaActivityTitulo,TVCalibracionPipaActivityTituloPipa,
            TVCalibracionPipaTituloMediro,TVCalibracionPipaActivityDestinoPuruebas;
    Spinner SCalibracionPipaActivityListaPipa,SCalibracionPipaActivityMedidor,
            SCalibracionPipaActivityPruebas;
    Button BtnCalibracionPipaActivityGuardar;

    boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal;
    CalibracionDTO calibracionDTO;
    DatosCalibracionDTO datosCalibracionDTO;
    Session session;
    ProgressDialog  progressDialog;
    CalibracionPipaPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_calibracion_pipa);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            EsCalibracionPipaInicial = extras.getBoolean("EsCalibracionPipaInicial",
                    false);
            EsCalibracionPipaFinal = extras.getBoolean("EsCalibracionPipaFinal",
                    false);
        }
        calibracionDTO = new CalibracionDTO();
        session = new Session(this);
        presenter = new CalibracionPipaPresenterImpl(this);
        presenter.getList(session.getToken(),EsCalibracionPipaFinal);

        TVCalibracionPipaActivityTitulo = findViewById(R.id.TVCalibracionPipaActivityTitulo);
        TVCalibracionPipaActivityTituloPipa = findViewById(R.id.TVCalibracionPipaActivityTituloPipa);
        TVCalibracionPipaTituloMediro = findViewById(R.id.TVCalibracionPipaTituloMediro);
        TVCalibracionPipaActivityDestinoPuruebas = findViewById(R.id.
                TVCalibracionPipaActivityDestinoPuruebas);

        SCalibracionPipaActivityListaPipa = findViewById(R.id.SCalibracionPipaActivityListaPipa);
        SCalibracionPipaActivityMedidor = findViewById(R.id.SCalibracionPipaActivityMedidor);
        SCalibracionPipaActivityPruebas = findViewById(R.id.SCalibracionPipaActivityPruebas);

        BtnCalibracionPipaActivityGuardar = findViewById(R.id.BtnCalibracionPipaActivityGuardar);

        TVCalibracionPipaActivityTitulo.setText((EsCalibracionPipaInicial)?
                getString(R.string.Calibracion)+ " - Inicial":
                getString(R.string.Calibracion)+ " - Final"
        );

        TVCalibracionPipaActivityDestinoPuruebas.setVisibility((EsCalibracionPipaInicial)
                ? View.GONE:View.VISIBLE);
        SCalibracionPipaActivityPruebas.setVisibility((EsCalibracionPipaInicial)
                ? View.GONE:View.VISIBLE);

        SCalibracionPipaActivityListaPipa.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                calibracionDTO.setIdCAlmacenGas(1);
                calibracionDTO.setNombreCAlmacenGas("Pipa No. 1");
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                calibracionDTO.setIdCAlmacenGas(0);
                calibracionDTO.setNombreCAlmacenGas("");
            }
        });

        SCalibracionPipaActivityMedidor.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                calibracionDTO.setIdTipoMedidor(1);
                calibracionDTO.setNombreMedidor("Magnatel");
                calibracionDTO.setCantidadFotografias(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                calibracionDTO.setIdTipoMedidor(0);
                calibracionDTO.setNombreMedidor("");
                calibracionDTO.setCantidadFotografias(0);
            }
        });

        SCalibracionPipaActivityPruebas.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });


        BtnCalibracionPipaActivityGuardar.setOnClickListener(v -> {
            validarForm();
        });
    }

    @Override
    public void validarForm() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(SCalibracionPipaActivityListaPipa.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La pipa a calibrar es un valor requerido");
        }
        if(SCalibracionPipaActivityMedidor.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El tipo de medidor es un valor requerido");
        }
        if(EsCalibracionPipaFinal){
            if(SCalibracionPipaActivityPruebas.getSelectedItemPosition()<0){
                error = true;
                mensajes.add("El destino de las pruebas es requerido");
            }
        }
        if(error){
            errorDialog(mensajes);
        }else{
            dialogoGoBack();
        }
    }

    @Override
    public void errorDialog(ArrayList<String> mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder  = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for(String mens:mensaje){
            stringBuilder.append(mens).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void dialogoGoBack() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(CalibracionPipaActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsCalibracionPipaInicial",EsCalibracionPipaInicial);
            intent.putExtra("EsCalibracionPipaFinal",EsCalibracionPipaFinal);
            intent.putExtra("calibracionDTO",calibracionDTO);
        }));
        builder.setNegativeButton(R.string.message_cancel,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
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
    public void onHiddenProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onSuccessList(DatosCalibracionDTO dto) {
        if(dto!=null){
            datosCalibracionDTO = dto;
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }
}
