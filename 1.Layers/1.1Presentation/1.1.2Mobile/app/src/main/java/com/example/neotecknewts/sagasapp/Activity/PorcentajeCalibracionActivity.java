package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;
import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class PorcentajeCalibracionActivity extends AppCompatActivity implements PorcentajeCalibracionView{
    TextView TVCalibracionPorcentajeActivitySubtitulo;
    NumberPicker NPCalibracionPorcentajeActivityEntero,NPCalibracionPorcentajeActivityDecimal;
    Button BtnCalibracionPipaActivitySiguiente;

    boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal,EsTanquePipaFinalPruebas;
    CalibracionDTO calibracionDTO;
    double porcentaje_original,PorcentajeCalibracion;
    ProgressDialog progressDialog;
    PorcentajeCalibracionPresenter presenter;
    Session session;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_porcentaje_calibracion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsCalibracionPipaInicial = bundle.getBoolean("EsCalibracionPipaInicial",
                    false);
            EsCalibracionPipaFinal = bundle.getBoolean("EsCalibracionPipaFinal",
                    true);
            EsTanquePipaFinalPruebas = bundle.getBoolean("EsTanquePipaFinalPruebas",
                    true);
            calibracionDTO = (CalibracionDTO) bundle.getSerializable("calibracionDTO");
        }
        session = new Session(this);
        presenter = new PorcentajeCalibracionPresenterImpl(this);
        TVCalibracionPorcentajeActivitySubtitulo = findViewById(R.id.
                TVCalibracionPorcentajeActivitySubtitulo);

        NPCalibracionPorcentajeActivityEntero = findViewById(R.id.
                NPCalibracionPorcentajeActivityEntero);
        NPCalibracionPorcentajeActivityDecimal = findViewById(R.id.
                NPCalibracionPorcentajeActivityDecimal);
        BtnCalibracionPipaActivitySiguiente = findViewById(R.id.BtnCalibracionPipaActivitySiguiente);

        BtnCalibracionPipaActivitySiguiente.setOnClickListener(v->{VerificarPorcentaje();});
        porcentaje_original = 04.0;
        NPCalibracionPorcentajeActivityEntero.setMaxValue(99);
        NPCalibracionPorcentajeActivityDecimal.setMaxValue(9);
        presenter.getPorcentaje(session.getToken());
    }

    @Override
    public void VerificarPorcentaje() {
        if(NPCalibracionPorcentajeActivityEntero.getValue()>0) {
            String porcentaje = String.valueOf(NPCalibracionPorcentajeActivityEntero.getValue()) + "." +
                    String.valueOf(NPCalibracionPorcentajeActivityDecimal.getValue());
            PorcentajeCalibracion = Double.parseDouble(porcentaje);
            confirmar();
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle(R.string.error_titulo);
            builder.setMessage("El procentaje de calibración es un valor requerido.");
            builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
                dialog.dismiss();
                NPCalibracionPorcentajeActivityEntero.setFocusable(true);
            }));
            builder.create().show();
        }
    }

    @Override
    public void onSuccessPorcentaje(DatosEmpresaConfiguracionDTO dto) {
        if(dto!=null){
            porcentaje_original = 04.00;
        }
    }

    @Override
    public void onErrorPorcentaje(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            NPCalibracionPorcentajeActivityEntero.setFocusable(true);
        }));
        builder.create().show();
    }

    @Override
    public void confirmar() {
        if(porcentaje_original!= PorcentajeCalibracion){
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle(R.string.title_alert_message);
            builder.setMessage(R.string.confirmacion_configuracion);
            builder.setNegativeButton(R.string.label_no,((dialog, which) -> {
                dialog.dismiss();
                String sp = String.valueOf((porcentaje_original>0)?porcentaje_original:04.0);
                String[] num = sp.split(".");
                int parte_entera = (num[0]!=null)?Integer.valueOf(num[0]):0;
                int parte_decimal = (num[1]!=null)?Integer.valueOf(num[1]):0;
                NPCalibracionPorcentajeActivityEntero.setValue(parte_entera);
                NPCalibracionPorcentajeActivityDecimal.setValue(parte_decimal);
            }));
            builder.setPositiveButton(R.string.label_si,((dialog, which) -> {
                dialog.dismiss();
                continuar();
            }));
            builder.create().show();
        }else{
            calibracionDTO.setPorcentajeCalibracion(PorcentajeCalibracion);
            continuar();
        }
    }

    @Override
    public void continuar() {
        calibracionDTO.setPorcentajeCalibracion(PorcentajeCalibracion);
        Intent intent = new Intent(PorcentajeCalibracionActivity.this,
                LecturaP5000Activity.class);
        intent.putExtra("EsCalibracionPipaInicial",EsCalibracionPipaInicial);
        intent.putExtra("EsCalibracionPipaFinal",EsCalibracionPipaFinal);
        intent.putExtra("EsTanquePipaFinalPruebas",EsTanquePipaFinalPruebas);
        intent.putExtra("calibracionDTO",calibracionDTO);
        startActivity(intent);
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
    public void onHiddeProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }
}