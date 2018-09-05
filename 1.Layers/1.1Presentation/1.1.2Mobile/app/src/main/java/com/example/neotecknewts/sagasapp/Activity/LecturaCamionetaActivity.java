package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.R;

public class LecturaCamionetaActivity extends AppCompatActivity {
    public boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta;
    public TextView TVLecturaCamionetaActivityTitulo,TVLecturaCamionetaActivotyRecordatorioUno,
            TVLecturaCamionetaActivityRecordatorioDos,TVLecturaCamionetaAcitvityQuien;
    public Spinner SLecturaCamionetaActivityListaCamioneta,SLecturaCamionetaActivityListaQuien;
    public Button BtnLecturaCamionetaActivityAceptar;
    public String[] list_camionetas,list_quien;
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
        if (EsLecturaInicialCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" inicial");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.GONE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.GONE);
        }else if(EsLecturaFinalCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" final");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.VISIBLE);
        }
        BtnLecturaCamionetaActivityAceptar.setOnClickListener(v -> VerificarForm());
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

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

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
                }else{
                    TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
                    TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.VISIBLE);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }
    public void VerificarForm(){

    }
}
