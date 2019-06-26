package com.neotecknewts.sagasapp.Activity;

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

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.neotecknewts.sagasapp.Presenter.CalibracionPipaPresenter;
import com.neotecknewts.sagasapp.Presenter.CalibracionPipaPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class CalibracionPipaActivity extends AppCompatActivity implements CalibracionPipaView {

    TextView TVCalibracionPipaActivityTitulo,TVCalibracionPipaActivityTituloPipa,
            TVCalibracionPipaTituloMediro,TVCalibracionPipaActivityDestinoPuruebas;
    Spinner SCalibracionPipaActivityListaPipa,SCalibracionPipaActivityMedidor,
            SCalibracionPipaActivityPruebas;
    Button BtnCalibracionPipaActivityGuardar;

    boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal,EsTanquePipaFinalPruebas;
    CalibracionDTO calibracionDTO;
    DatosCalibracionDTO datosCalibracionDTO;
    Session session;
    ProgressDialog  progressDialog;
    CalibracionPipaPresenter presenter;
    String[] list_pipa_salida,list_tipo_medidor,list_destino_pruebas;

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
        //list_pipa_salida = new String[]{"Pipa No. 1","Pipa No. 2"};
        //list_tipo_medidor = new String[]{"Magnatel","Rotogate"};
        list_destino_pruebas = new String[]{"Tanque de la pipa","Tanque portatil"};

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
                if(position>=0) {
                    if(datosCalibracionDTO.getEstaciones().size()>0 &&
                            !datosCalibracionDTO.getEstaciones().isEmpty()) {
                        for (int x=0;x<datosCalibracionDTO.getEstaciones().size();x++) {
                            if(parent.getItemAtPosition(position).toString().equals(
                                    datosCalibracionDTO.getEstaciones().get(x).getNombrePipa()
                            )) {
                                calibracionDTO.setIdCAlmacenGas(
                                        datosCalibracionDTO.getEstaciones().get(x).getIdAlmacenGas()
                                );
                                calibracionDTO.setNombreCAlmacenGas(
                                        datosCalibracionDTO.getEstaciones().get(x).getNombrePipa()
                                );
                                calibracionDTO.setIdTipoMedidor(
                                        datosCalibracionDTO.getEstaciones().get(x).getIdTipoMedidor()
                                );
                                calibracionDTO.setNombreMedidor(
                                        datosCalibracionDTO.getEstaciones().get(x).getMedidor()
                                                .getNombreTipoMedidor()
                                );
                                calibracionDTO.setCantidadFotografias(
                                        datosCalibracionDTO.getEstaciones().get(x).getMedidor()
                                                .getCantidadFotografias()
                                );
                                calibracionDTO.setP5000(
                                        datosCalibracionDTO.getEstaciones().get(x).getCantidadP5000()
                                );
                                calibracionDTO.setPorcentaje(
                                        datosCalibracionDTO.getEstaciones().get(x)
                                                .getPorcentajeMedidor()
                                );
                                calibracionDTO.setPorcentajeCalibracion(
                                        datosCalibracionDTO.getEstaciones().get(x)
                                                .getPorcentajeMedidor()
                                );
                                calibracionDTO.setPorcentajeMedidor2(
                                        datosCalibracionDTO.getEstaciones().get(x)
                                                .getPorcentajeMedidor()
                                );
                                calibracionDTO.setIdDestinoCalibracion(
                                        datosCalibracionDTO.getEstaciones().get(x)
                                                .getIdAlmacenGas()
                                );
                            }
                            if(datosCalibracionDTO.getMedidores()!=null) {
                                for (int y = 0; y < datosCalibracionDTO.getMedidores().size(); y++) {
                                    if ( datosCalibracionDTO.getEstaciones().get(x).getMedidor()
                                            .getNombreTipoMedidor().equals(
                                                    datosCalibracionDTO.getMedidores().get(y).
                                                            getNombreTipoMedidor()
                                            ))

                                        SCalibracionPipaActivityMedidor.setSelection(y-1);
                                }
                            }
                        }
                    }
                }
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
                for (int x =0;x<datosCalibracionDTO.getMedidores().size();x++) {
                    if (datosCalibracionDTO.getMedidores().get(x).getNombreTipoMedidor()
                            .equals(parent.getItemAtPosition(position).toString())) {
                        calibracionDTO.setIdTipoMedidor(
                                datosCalibracionDTO.getMedidores().get(x).getIdTipoMedidor()
                        );
                        calibracionDTO.setNombreMedidor(
                                datosCalibracionDTO.getMedidores().get(x)
                                        .getNombreTipoMedidor()
                        );
                        calibracionDTO.setCantidadFotografias(
                                datosCalibracionDTO.getMedidores().get(x)
                                        .getCantidadFotografias()
                        );
                    }
                }
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
                if(SCalibracionPipaActivityPruebas.getSelectedItem().equals("Tanque de la pipa")){
                    EsTanquePipaFinalPruebas = true;
                    calibracionDTO.setIdDestinoCalibracion(1);
                }else{
                    EsTanquePipaFinalPruebas = false;
                    calibracionDTO.setIdDestinoCalibracion(2);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                EsTanquePipaFinalPruebas = true;
            }
        });
       /* SCalibracionPipaActivityListaPipa.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_pipa_salida
        ));
        SCalibracionPipaActivityMedidor.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_tipo_medidor
        ));*/
        SCalibracionPipaActivityPruebas.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_destino_pruebas
        ));
        presenter.getList(session.getToken(),EsCalibracionPipaFinal);

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
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            if(EsCalibracionPipaInicial) {
                Intent intent = new Intent(CalibracionPipaActivity.this,
                        LecturaP5000Activity.class);
                intent.putExtra("EsCalibracionPipaInicial", EsCalibracionPipaInicial);
                intent.putExtra("EsCalibracionPipaFinal", EsCalibracionPipaFinal);
                intent.putExtra("calibracionDTO", calibracionDTO);
                startActivity(intent);
            }else{
                Intent intent = new Intent(CalibracionPipaActivity.this,
                        PorcentajeCalibracionActivity.class);
                intent.putExtra("EsCalibracionPipaInicial", EsCalibracionPipaInicial);
                intent.putExtra("EsCalibracionPipaFinal", EsCalibracionPipaFinal);
                intent.putExtra("calibracionDTO", calibracionDTO);
                intent.putExtra("EsTanquePipaFinalPruebas",EsTanquePipaFinalPruebas);
                startActivity(intent);
            }
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
            if(dto.getEstaciones().size()>0
                    && !dto.getEstaciones().isEmpty() && dto.getEstaciones()!=null){
                list_pipa_salida = new String[dto.getEstaciones().size()];
                for (int x=0;x<dto.getEstaciones().size();x++){
                    list_pipa_salida[x] = dto.getEstaciones().get(x).getNombrePipa();
                }
                SCalibracionPipaActivityListaPipa.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        list_pipa_salida
                ));
            }
            if(dto.getMedidores().size()>0 && !dto.getMedidores().isEmpty() &&
                    dto.getMedidores()!=null){
                list_tipo_medidor = new String[dto.getMedidores().size()];
                for (int x=0;x<dto.getMedidores().size();x++){
                    list_tipo_medidor[x] = dto.getMedidores().get(x).getNombreTipoMedidor();
                }
                SCalibracionPipaActivityMedidor.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        list_tipo_medidor
                ));
            }

        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }
}
