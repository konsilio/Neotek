package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Presenter.AutoconsumoPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.AutoconsumoPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class AutoconsumoPipaActivity extends AppCompatActivity implements  AutoconsumoPipaView{
    TextView TVAutoconsumoPipaActivityTitulo,TVAutoconsumoPipaActivityTituloPipa,
            TVAutoconsumoPipaActivityTituloUnidad;
    Spinner SAutoconsumoPipaActivityListaPipasSalida,SAutoconsumoPipaActivityListaUnidadEntrada;
    Button BtnAutoconsumoPipaActivityGuardar;

    AutoconsumoDTO autoconsumoDTO;
    boolean EsAutoconsumoPipaInicial,EsAutoconsumoPipaFinal;
    DatosAutoconsumoDTO dto;
    String[] lista_pipa_salida,lista_unidad_entrada;
    Session session;
    ProgressDialog progressDialog;
    AutoconsumoPipaPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_autoconsumo_pipa);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsAutoconsumoPipaInicial = bundle.getBoolean("EsAutoconsumoPipaInicial",
                    false);
            EsAutoconsumoPipaFinal = bundle.getBoolean("EsAutoconsumoPipaFinal",
                    false);
        }
        autoconsumoDTO = new AutoconsumoDTO();
        session = new Session(this);
        presenter = new AutoconsumoPipaPresenterImpl(this);

        TVAutoconsumoPipaActivityTitulo = findViewById(R.id.TVAutoconsumoPipaActivityTitulo);
        TVAutoconsumoPipaActivityTituloPipa = findViewById(R.id.TVAutoconsumoPipaActivityTituloPipa);
        TVAutoconsumoPipaActivityTituloUnidad = findViewById(R.id.TVAutoconsumoPipaActivityTituloUnidad);
        SAutoconsumoPipaActivityListaUnidadEntrada = findViewById(R.id.SAutoconsumoPipaActivityListaUnidadEntrada);
        SAutoconsumoPipaActivityListaPipasSalida = findViewById(R.id.SAutoconsumoPipaActivityListaPipasSalida);
        BtnAutoconsumoPipaActivityGuardar = findViewById(R.id.BtnAutoconsumoPipaActivityGuardar);

        SAutoconsumoPipaActivityListaUnidadEntrada.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                autoconsumoDTO.setIdCAlmacenGasEntrada(1);
                autoconsumoDTO.setNombreTipoMedidor("Magnatel");
                autoconsumoDTO.setCantidadFotos(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                autoconsumoDTO.setIdCAlmacenGasEntrada(0);
                autoconsumoDTO.setNombreTipoMedidor("");
                autoconsumoDTO.setCantidadFotos(0);
            }
        });

        SAutoconsumoPipaActivityListaPipasSalida.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                autoconsumoDTO.setIdCAlmacenGasSalida(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                autoconsumoDTO.setIdCAlmacenGasSalida(0);
            }
        });

        BtnAutoconsumoPipaActivityGuardar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                VerificarCampos();
            }
        });
        presenter.getList(session,EsAutoconsumoPipaFinal);
    }

    @Override
    public void VerificarCampos() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(SAutoconsumoPipaActivityListaPipasSalida.getSelectedItemPosition()<0){
            mensajes.add("La pipa de salida es un valor requerido");
            error=true;
        }
        if(SAutoconsumoPipaActivityListaUnidadEntrada.getSelectedItemPosition()<0){
            mensajes.add("La unidad de entrada es un valor requerido");
            error=true;
        }
        if(error) {
            mostrarErrores(mensajes);
        }else{
            Intent intent = new Intent(AutoconsumoPipaActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsAutoconsumoPipaInicial",EsAutoconsumoPipaInicial);
            intent.putExtra("EsAutoconsumoPipaFinal",EsAutoconsumoPipaFinal);
            intent.putExtra("autoconsumoDTO",autoconsumoDTO);
            startActivity(intent);
        }
    }

    @Override
    public void onShowprogress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddeprogress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.dismiss();
        }
    }

    @Override
    public void mostrarErrores(ArrayList<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje: mensajes) {
            stringBuilder.append(mensaje);
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create().show();
    }

    @Override
    public void onSuccessList(DatosAutoconsumoDTO dto) {
        if(dto!=null){
            this.dto = dto;
            lista_pipa_salida = new String[]{"Pipa 1","Pipa 2"};
            lista_unidad_entrada = new String[]{"Unidad No. 1","Unidad no. 2"};
            SAutoconsumoPipaActivityListaPipasSalida.setAdapter( new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_pipa_salida
            ));
            SAutoconsumoPipaActivityListaUnidadEntrada.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_unidad_entrada
            ));
        }
    }
}
