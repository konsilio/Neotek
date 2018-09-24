package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoEstacionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoEstacionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class TraspasoEstacionActivity extends AppCompatActivity implements TraspasoEstacionView {
    boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    Session session;
    String[] lista_estacion_salida,lista_medidor,lista_pipa_entrada;
    TraspasoDTO traspasoDTO;
    ProgressDialog progressDialog;
    DatosTraspasoDTO datosTraspasoDTO;

    TextView TVTraspasoEstacionActivityTitulo,TVTraspasoEstacionActivityEstacionSalidaTitulo,
            TVTraspasoEstacionActivityMedidor,TVTraspasoEstacionActivityPipaDeEntrada;
    Spinner STraspasoEstacionActivityEstacionSalida,STraspasoEstacionActivityMedidor,
            STraspasoEstacionActivityPipaEntrada;
    Button BtnTraspasoEstacionActivityAceptar;
    TraspasoEstacionPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_traspaso_estacion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);
            EsTraspasoEstacionFinal = bundle.getBoolean("EsTraspasoEstacionFinal",
                    false);
            EsPrimeraParteTraspaso = bundle.getBoolean("EsPrimeraParteTraspaso",
                    true);
        }
        session = new Session(this);
        traspasoDTO = new TraspasoDTO();
        presenter = new TraspasoEstacionPresenterImpl(this);

        TVTraspasoEstacionActivityTitulo = findViewById(R.id.TVTraspasoEstacionActivityTitulo);
        TVTraspasoEstacionActivityEstacionSalidaTitulo = findViewById(R.id.
                 TVTraspasoEstacionActivityEstacionSalidaTitulo);
        TVTraspasoEstacionActivityMedidor = findViewById(R.id.TVTraspasoEstacionActivityMedidor);
        TVTraspasoEstacionActivityPipaDeEntrada = findViewById(R.id.
                 TVTraspasoEstacionActivityPipaDeEntrada);
        STraspasoEstacionActivityEstacionSalida = findViewById(R.id.
                 STraspasoEstacionActivityEstacionSalida);
        STraspasoEstacionActivityMedidor = findViewById(R.id.STraspasoEstacionActivityMedidor);
        STraspasoEstacionActivityPipaEntrada = findViewById(R.id.
                 STraspasoEstacionActivityPipaEntrada);
        BtnTraspasoEstacionActivityAceptar = findViewById(R.id.BtnTraspasoEstacionActivityAceptar);

        STraspasoEstacionActivityEstacionSalida.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                traspasoDTO.setIdCAlmacenGasSalida(1);
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasSalida(0);
             }
         });

         STraspasoEstacionActivityMedidor.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                traspasoDTO.setIdTipoMedidorSalida(1);
                traspasoDTO.setNombreMedidor("Magnatel");
                traspasoDTO.setCantidadDeFotos(1);
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdTipoMedidorSalida(0);
                traspasoDTO.setNombreMedidor("");
                traspasoDTO.setCantidadDeFotos(0);
             }
         });

         STraspasoEstacionActivityPipaEntrada.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                traspasoDTO.setIdCAlmacenGasEntrada(1);
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasEntrada(0);
             }
         });

         lista_estacion_salida = new String[]{"Estación No.1","Estación No.2"};
         lista_medidor = new String[]{"Magnatel","Rotogate"};
         lista_pipa_entrada = new String[]{"Pipa No, 1","Pipa No. 2"};
         STraspasoEstacionActivityEstacionSalida.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_estacion_salida
         ));
         STraspasoEstacionActivityMedidor.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_medidor
         ));
         STraspasoEstacionActivityPipaEntrada.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_estacion_salida
         ));
         presenter.GetList(session.getToken());

         BtnTraspasoEstacionActivityAceptar.setOnClickListener(v -> {
             ValidarForm();
         });
    }

    @Override
    public void onSuccessList(DatosTraspasoDTO dto) {
        datosTraspasoDTO = dto;
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,(dialog,which)->{dialog.dismiss();});
        builder.create().show();
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddeProgress() {
        if(progressDialog!=null && progressDialog.isShowing())
            progressDialog.hide();
    }

    @Override
    public void ValidarForm() {
        boolean error = false;
        List<String> mensajes = new ArrayList<>();
        if(STraspasoEstacionActivityEstacionSalida.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La estación de salida es un valor requerido");
        }
        if(STraspasoEstacionActivityMedidor.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El medidor de la estación de salida es un valor requerido");
        }
        if(STraspasoEstacionActivityPipaEntrada.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La pipa de entrada es un valor requerido");
        }
        if(error){
            MostrarErrores(mensajes);
        }else{
            DialogoBack();
        }
    }

    @Override
    public void MostrarErrores(List<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje : mensajes){
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void DialogoBack() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setNegativeButton(R.string.message_cancel, (dialog, which) -> dialog.dismiss());
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(TraspasoEstacionActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsTraspasoEstacionInicial",EsTraspasoEstacionInicial);
            intent.putExtra("EsTraspasoEstacionFinal",EsTraspasoEstacionFinal);
            intent.putExtra("EsPrimeraParteTraspaso",EsPrimeraParteTraspaso);
            intent.putExtra("traspasoDTO",traspasoDTO);
            startActivity(intent);
        }));
        builder.create().show();
    }
}
