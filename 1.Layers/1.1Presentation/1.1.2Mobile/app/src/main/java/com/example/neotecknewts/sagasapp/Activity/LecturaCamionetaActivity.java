package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;
import java.util.List;

public class LecturaCamionetaActivity extends AppCompatActivity implements LecturaCamionetaView{
    public boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta,error;
    public TextView TVLecturaCamionetaActivityTitulo,TVLecturaCamionetaActivotyRecordatorioUno,
            TVLecturaCamionetaActivityRecordatorioDos,TVLecturaCamionetaAcitvityQuien;
    public Spinner SLecturaCamionetaActivityListaCamioneta,SLecturaCamionetaActivityListaQuien;
    public Button BtnLecturaCamionetaActivityAceptar;
    public String[] list_camionetas,list_quien;
    public LecturaCamionetaDTO lecturaCamionetaDTO;
    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_camioneta);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsLecturaInicialCamioneta = (boolean) bundle.get("EsLecturaInicialCamioneta");
            EsLecturaFinalCamioneta = (boolean) bundle.get("EsLecturaFinalCamioneta");
        }
        TVLecturaCamionetaActivityTitulo = findViewById(R.id.TVLecturaCamionetaActivityTitulo);
        TVLecturaCamionetaAcitvityQuien = findViewById(R.id.TVLecturaCamionetaAcitvityQuien);
        TVLecturaCamionetaActivotyRecordatorioUno = findViewById(
                R.id.TVLecturaCamionetaActivotyRecordatorioUno);
        TVLecturaCamionetaActivityRecordatorioDos = findViewById(
                R.id.TVLecturaCamionetaActivityRecordatorioDos);
        SLecturaCamionetaActivityListaCamioneta = findViewById(
                R.id.SLecturaCamionetaActivityListaCamioneta);
        SLecturaCamionetaActivityListaQuien = findViewById(R.id.SLecturaCamionetaActivityListaQuien);
        BtnLecturaCamionetaActivityAceptar = findViewById(R.id.BtnLecturaCamionetaActivityAceptar);
        lecturaCamionetaDTO = new LecturaCamionetaDTO();
        if (EsLecturaInicialCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" inicial");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.GONE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.GONE);
            lecturaCamionetaDTO.setEsEncargadoPuerta(false);
        }else if(EsLecturaFinalCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" final");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.VISIBLE);
        }
        BtnLecturaCamionetaActivityAceptar.setOnClickListener(v -> verificarForm());
        list_camionetas = new String[]{"Seleccione","Camioneta Ford","Camioneta Chevy"};
        list_quien = new String[]{"Seleccione","Encargado de Puerta","Encargado del Andén"};
        SLecturaCamionetaActivityListaCamioneta.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,list_camionetas));
        SLecturaCamionetaActivityListaQuien.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,list_quien));
        SLecturaCamionetaActivityListaCamioneta.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                lecturaCamionetaDTO.setIdCamioneta(1);
                lecturaCamionetaDTO.setNombreCamioneta(SLecturaCamionetaActivityListaCamioneta
                        .getItemAtPosition(position).toString());
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaCamionetaDTO.setIdCamioneta(0);
                lecturaCamionetaDTO.setNombreCamioneta("");
            }
        });
        SLecturaCamionetaActivityListaQuien.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (SLecturaCamionetaActivityListaQuien.
                        getItemAtPosition(position).toString().equals("Encargado de Puerta")){
                    TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.VISIBLE);
                    TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
                    lecturaCamionetaDTO.setEsEncargadoPuerta(true);
                }else{
                    TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
                    TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.VISIBLE);
                    lecturaCamionetaDTO.setEsEncargadoPuerta(false);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaCamionetaDTO.setEsEncargadoPuerta(true);
            }
        });
    }
    @Override
    public void verificarForm(){
        List<String> mensajes_error = new ArrayList<>();
        if(SLecturaCamionetaActivityListaCamioneta.getSelectedItemPosition()<=0) {
                mensajes_error.add("La camioneta es un valor requerido");
        }
        if(EsLecturaFinalCamioneta){
            if(SLecturaCamionetaActivityListaQuien.getSelectedItemPosition()<=0){
                mensajes_error.add("Es necesario especificar quien eres");
            }
        }
        if(error)
            mensajeError(mensajes_error);
        else{
            dialogoRetornar();
        }
    }

    @Override
    public void getCamionetas() {

    }

    @Override
    public void onSuccessCamionetas() {

    }

    @Override
    public void onErrorCamionetas() {

    }
    @Override
    public void mensajeError(List<String> mensaje_error){
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaCamionetaActivity.this);
        builder.setTitle(getString(R.string.error_titulo));
        StringBuilder mensaje = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for(String men : mensaje_error){
            mensaje .append(men).append("\n");
        }
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }

    @Override
    public void dialogoRetornar(){
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaCamionetaActivity.this);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_goback_diabled);
        builder.setPositiveButton(R.string.message_acept,(dialog,which)->{
            Intent intent = new Intent(LecturaCamionetaActivity.this,
                    ConfiguracionCamionetaActivity.class);
            intent.putExtra("EsLecturaInicialCamioneta",EsLecturaInicialCamioneta);
            intent.putExtra("EsLecturaFinalCamioneta",EsLecturaFinalCamioneta);
            intent.putExtra("lecturaCamionetaDTO",lecturaCamionetaDTO);
            dialog.dismiss();
            startActivity(intent);
        });
        builder.setNegativeButton(R.string.message_cancel,((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }
}
