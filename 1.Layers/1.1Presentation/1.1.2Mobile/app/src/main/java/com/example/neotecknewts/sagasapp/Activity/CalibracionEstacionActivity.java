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

import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Presenter.CalibracionEstacionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.CalibracionEstacionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.function.Predicate;
import java.util.stream.Collector;
import java.util.stream.Collectors;

public class CalibracionEstacionActivity extends AppCompatActivity
        implements CalibracionEstacionView{
    boolean EsCalibracionEstacionInicial,EsCalibracionEstacionFinal;

    TextView TVCalibracionEstacionActivityTitulo,TVCalibracionEstacionEstacionACalibrar,
            TVCalibracionEstacionActivityMedidor;
    Spinner SCalibracionEstacionActivityListaEstaciones,SCalibracionEstacionActivityListaMedidor;
    Button BtnCalibracionEstacionAceptar;
    ProgressDialog progressDialog;
    DatosCalibracionDTO dto;
    CalibracionDTO calibracionDTO;
    Session session;
    CalibracionEstacionPresenter presenter;
    String[] lista_estacion,lista_medidor;
    ArrayAdapter medidores;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_calibracion_estacion);
        Bundle extras = getIntent().getExtras();
        if(extras!= null){
            EsCalibracionEstacionInicial = extras.getBoolean("EsCalibracionEstacionInicial",
                    false);
            EsCalibracionEstacionFinal = extras.getBoolean("EsCalibracionEstacionFinal",
                    false);
        }
        calibracionDTO = new CalibracionDTO();
        session = new Session(this);
        presenter = new CalibracionEstacionPresenterImpl(this);
        TVCalibracionEstacionActivityTitulo = findViewById(R.id.
                TVCalibracionEstacionActivityTitulo);
        TVCalibracionEstacionEstacionACalibrar = findViewById(R.id.
                TVCalibracionEstacionEstacionACalibrar);
        TVCalibracionEstacionActivityMedidor = findViewById(R.id.
                TVCalibracionEstacionActivityMedidor);
        SCalibracionEstacionActivityListaEstaciones = findViewById(R.id.
                SCalibracionEstacionActivityListaEstaciones);
        SCalibracionEstacionActivityListaMedidor = findViewById(R.id.
                SCalibracionEstacionActivityListaMedidor);
        lista_estacion = new String[]{"Estación No. 1","Estacion No. 2"};
        lista_medidor = new String[]{"Magnatel","Rotogate"};
        SCalibracionEstacionActivityListaEstaciones.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_estacion
        ));
        SCalibracionEstacionActivityListaMedidor.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_medidor
        ));
        SCalibracionEstacionActivityListaEstaciones.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(dto!=null) {
                    for (DatosCalibracionDTO.EstacionDTO estacionDTO:
                            dto.getEstaciones()) {
                        if(estacionDTO.getNombreAlmacen().
                                equals(parent.getItemAtPosition(position).toString())) {
                            calibracionDTO.setIdTipoMedidor(estacionDTO.getIdTipoMedidor());
                            calibracionDTO.setNombreMedidor(estacionDTO.getMedidor()
                                    .getNombreTipoMedidor());
                            calibracionDTO.setCantidadFotografias(estacionDTO.getMedidor()
                                    .getCantidadFotografias());
                            calibracionDTO.setIdCAlmacenGas(estacionDTO.getIdAlmacenGas());
                            calibracionDTO.setNombreCAlmacenGas(estacionDTO.getNombreAlmacen());
                             int pos = medidores.getPosition(estacionDTO.getMedidor().getNombreTipoMedidor());
                             SCalibracionEstacionActivityListaMedidor.setSelection(pos);
                        }
                    }
                }else{
                    calibracionDTO.setIdCAlmacenGas(1);
                    calibracionDTO.setNombreCAlmacenGas("Estación No. 1");
                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                calibracionDTO.setIdCAlmacenGas(0);
                calibracionDTO.setNombreCAlmacenGas("");
            }
        });
        SCalibracionEstacionActivityListaMedidor.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(dto!=null){
                    for (MedidorDTO medidorDTO:
                         dto.getMedidores()) {
                        if(medidorDTO.getNombreTipoMedidor().equals(
                                parent.getItemAtPosition(position).toString())) {
                            calibracionDTO.setIdTipoMedidor(medidorDTO.getIdTipoMedidor());
                            calibracionDTO.setNombreMedidor(medidorDTO.getNombreTipoMedidor());
                            calibracionDTO.setCantidadFotografias(medidorDTO.
                                    getCantidadFotografias());
                        }
                    }
                }else {
                    calibracionDTO.setIdTipoMedidor(1);
                    calibracionDTO.setNombreMedidor("Magnatel");
                    calibracionDTO.setCantidadFotografias(1);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                calibracionDTO.setIdTipoMedidor(0);
                calibracionDTO.setNombreMedidor("");
                calibracionDTO.setCantidadFotografias(0);
            }
        });

        BtnCalibracionEstacionAceptar = findViewById(R.id.BtnCalibracionEstacionAceptar);
        BtnCalibracionEstacionAceptar.setOnClickListener(V->{verificarForm();});
        presenter.getList(session.getToken(),EsCalibracionEstacionFinal);
    }

    @Override
    public void verificarForm() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(SCalibracionEstacionActivityListaEstaciones.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La estación de carburación a calibrar es un valor requerido");
        }
        if(SCalibracionEstacionActivityListaMedidor.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El medidor porcentual es un valor requerido");
        }
        if(error){
            dialogoErrorForm(mensajes);
        }else{
            dialogoGoBack();
        }
    }

    @Override
    public void onSuccessList(DatosCalibracionDTO dto) {
        if(dto!=null) {
            this.dto = dto;
            lista_medidor = new String[dto.getMedidores().size()];
            for (int x=0;x<dto.getMedidores().size();x++) lista_medidor[x] =
                    dto.getMedidores().get(x).getNombreTipoMedidor();
            SCalibracionEstacionActivityListaMedidor.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_medidor
            ));
            for (int x = 0; x<dto.getEstaciones().size();x++) lista_estacion[x]= dto.getEstaciones()
                    .get(x).getNombreAlmacen();
            medidores = new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_estacion
            );
            SCalibracionEstacionActivityListaMedidor.setAdapter(medidores);
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void dialogoErrorForm(ArrayList<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje:mensajes){
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
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
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }

    @Override
    public void dialogoGoBack() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(CalibracionEstacionActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsCalibracionEstacionInicial",EsCalibracionEstacionInicial);
            intent.putExtra("EsCalibracionEstacionFinal",EsCalibracionEstacionFinal);
            intent.putExtra("calibracionDTO",calibracionDTO);
            startActivity(intent);
        }));
        builder.create().show();
    }
}
