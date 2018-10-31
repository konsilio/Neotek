package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class TraspasoPipaActivity extends AppCompatActivity implements TraspasoPipaView{
    boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal,EsPasoIniciaLPipa;
    TraspasoDTO traspasoDTO;
    DatosTraspasoDTO datosTraspasoDTO;
    String[] lista_pipa_salida,lista_pipa_entrada;
    ProgressDialog progressDialog;

    TextView TVTransfereciaPipaActivityTitulo,TVTraspasoPipaActivityPipaSalida,
            TVTrasferenciaPipaActivityPipaEntrada;
    Spinner STraspasoPipaActivityPipaSalida,STraspasoPipaActivityPipaEntrada;
    Button BtnTraspasoPipaActivityGuardar;
    TraspasoPipaPresenter presenter;
    Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_traspaso_pipa);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsTraspasoPipaInicial = bundle.getBoolean("EsTraspasoPipaInicial",false);
            EsTraspasoPipaFinal = bundle.getBoolean("EsTraspasoPipaFinal",false);
            EsPasoIniciaLPipa = bundle.getBoolean("EsPasoIniciaLPipa",true);
        }
        traspasoDTO = new TraspasoDTO();
        presenter = new TraspasoPipaPresenterImpl(this);
        session = new Session(this);
        TVTransfereciaPipaActivityTitulo = findViewById(R.id.TVTransfereciaPipaActivityTitulo);
        TVTraspasoPipaActivityPipaSalida = findViewById(R.id.TVTraspasoPipaActivityPipaSalida);
        TVTrasferenciaPipaActivityPipaEntrada = findViewById(R.id.TVTrasferenciaPipaActivityPipaEntrada);

        STraspasoPipaActivityPipaSalida = findViewById(R.id.STraspasoPipaActivityPipaSalida);
        STraspasoPipaActivityPipaEntrada = findViewById(R.id.STraspasoPipaActivityPipaEntrada);
        BtnTraspasoPipaActivityGuardar = findViewById(R.id.BtnTraspasoPipaActivityGuardar);

        TVTransfereciaPipaActivityTitulo.setText((EsTraspasoPipaInicial)?
            getString(R.string.trasferencia_de_gas)+" - Incial":
                getString(R.string.trasferencia_de_gas)+" - Final"
        );

        STraspasoPipaActivityPipaSalida.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                traspasoDTO.setIdCAlmacenGasSalida(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasSalida(0);

            }
        });

        STraspasoPipaActivityPipaEntrada.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                traspasoDTO.setIdCAlmacenGasEntrada(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasEntrada(0);
            }
        });
        lista_pipa_salida = new String[]{"Pipa 1","Pipa 2"};
        lista_pipa_entrada = new String[]{"Pipa 1","Pipa 2"};

        STraspasoPipaActivityPipaSalida.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_pipa_salida
        ));
        STraspasoPipaActivityPipaEntrada.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_pipa_entrada
        ));

        presenter.GetList(session.getToken());
        BtnTraspasoPipaActivityGuardar.setOnClickListener(V->{
            ValidarForm();
        });
    }

    @Override
    public void ValidarForm() {
        boolean error=false;
        List<String> mensajes = new ArrayList<>();
        if(STraspasoPipaActivityPipaSalida.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La pipa de salida es un valor requerido");
        }
        if(STraspasoPipaActivityPipaEntrada.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La pipa de entra es un valor requerido");
        }
        if(error){
            ErrorForm(mensajes);
        }else{
            goback();
        }
    }

    @Override
    public void ErrorForm(List<String> mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String men:mensaje){
            stringBuilder.append(men).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create().show();
    }

    @Override
    public void onSuccessLista(DatosTraspasoDTO dto) {
        datosTraspasoDTO = dto;
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {dialog.dismiss();}));
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
    public void onHiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.dismiss();
        }
    }

    @Override
    public void goback() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(getString(R.string.message_continuar));
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(TraspasoPipaActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsTraspasoPipaInicial",EsTraspasoPipaInicial);
            intent.putExtra("EsTraspasoPipaFinal",EsTraspasoPipaFinal);
            intent.putExtra("EsPasoIniciaLPipa",EsPasoIniciaLPipa);
            intent.putExtra("traspasoDTO",traspasoDTO);
            startActivity(intent);
        }));
        builder.setNegativeButton(R.string.message_cancel,((dialog, which) -> {dialog.dismiss();}));
        builder.create().show();

    }
}
