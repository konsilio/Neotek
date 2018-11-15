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
                if (datosTraspasoDTO != null) {
                    if(datosTraspasoDTO.getPipas().size()>0) {
                        for (int x =0;x<datosTraspasoDTO.getPipas().size();x++) {
                            if(datosTraspasoDTO.getPipas().get(x).getNombreAlmacen().equals(
                                    parent.getItemAtPosition(position).toString()
                            )) {
                                traspasoDTO.setIdCAlmacenGasSalida(
                                        datosTraspasoDTO.getPipas().get(x).getIdAlmacenGas()
                                );
                                traspasoDTO.setPorcentajeSalida(
                                        datosTraspasoDTO.getPipas().get(x).getPorcentajeMedidor()
                                );
                                traspasoDTO.setP5000Salida(
                                        datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                                );
                                traspasoDTO.setP5000SalidaInicial(
                                        datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                                );
                                traspasoDTO.setIdTipoMedidorSalida(
                                        datosTraspasoDTO.getPipas().get(x).getIdTipoMedidor()
                                );
                                traspasoDTO.setPorcentajeInicial(
                                        datosTraspasoDTO.getPipas().get(x).getIdTipoMedidor()
                                );
                                traspasoDTO.setNombreEstacionTraspaso(
                                        datosTraspasoDTO.getPipas().get(x).getNombreAlmacen()
                                );
                            }
                        }
                    }
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasSalida(0);
                traspasoDTO.setPorcentajeSalida(0);
                traspasoDTO.setP5000Salida(0);
                traspasoDTO.setP5000SalidaInicial(0);
                traspasoDTO.setIdTipoMedidorSalida(0);
                traspasoDTO.setPorcentajeInicial(0);
                traspasoDTO.setNombreEstacionTraspaso("");

            }
        });

        STraspasoPipaActivityPipaEntrada.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (datosTraspasoDTO!=null) {
                    if(datosTraspasoDTO.getPipas().size()>0) {
                        for (int x=0;x<datosTraspasoDTO.getPipas().size();x++) {
                            if (datosTraspasoDTO.getPipas().get(x).getNombreAlmacen().equals(
                                    parent.getItemAtPosition(position).toString()
                            )) {
                                traspasoDTO.setIdCAlmacenGasEntrada(
                                        datosTraspasoDTO.getPipas().get(x).getIdAlmacenGas()
                                );
                                traspasoDTO.setP5000Entrada(
                                        datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                                );
                                traspasoDTO.setP5000EntradaInicial(
                                        datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                                );
                                traspasoDTO.setNombreEstacionEntrada(
                                        datosTraspasoDTO.getPipas().get(x).getNombreAlmacen()
                                );
                                traspasoDTO.setIdTipoMedidorSalida(
                                        datosTraspasoDTO.getPipas().get(x).getIdTipoMedidor()
                                );
                                traspasoDTO.setNombreMedidor(
                                        datosTraspasoDTO.getPipas().get(x).getMedidor().getNombreTipoMedidor()
                                );
                                traspasoDTO.setCantidadDeFotos(
                                        datosTraspasoDTO.getPipas().get(x).getMedidor().getCantidadFotografias()
                                );
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdCAlmacenGasEntrada(0);
                traspasoDTO.setIdTipoMedidorSalida(0);
                traspasoDTO.setNombreMedidor("");
                traspasoDTO.setCantidadDeFotos(0);
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
        int predeterminada = 0;
        if(datosTraspasoDTO!=null){
            if(!datosTraspasoDTO.getPipas().isEmpty()){
                lista_pipa_entrada= new String[datosTraspasoDTO.getPipas().size()];
                for (int x =0; x<datosTraspasoDTO.getPipas().size();x++){
                    lista_pipa_entrada[x]=datosTraspasoDTO.getPipas().get(x).getNombreAlmacen();
                    if(datosTraspasoDTO.getPredeterminada()==datosTraspasoDTO.getPipas().get(x)
                            .getIdAlmacenGas()){
                        predeterminada = x;
                    }
                }
                STraspasoPipaActivityPipaEntrada.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_pipa_entrada
                ));

                lista_pipa_salida = new String[datosTraspasoDTO.getPipas().size()];
                for (int x =0; x<datosTraspasoDTO.getPipas().size();x++){
                    lista_pipa_salida[x]=datosTraspasoDTO.getPipas().get(x).getNombreAlmacen();
                }
                STraspasoPipaActivityPipaSalida.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_pipa_salida
                ));
                //if(predeterminada>0)
                STraspasoPipaActivityPipaEntrada.setSelection(predeterminada);
            }
        }
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
