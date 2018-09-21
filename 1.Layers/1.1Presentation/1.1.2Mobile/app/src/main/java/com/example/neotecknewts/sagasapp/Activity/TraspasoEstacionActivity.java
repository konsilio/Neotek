package com.example.neotecknewts.sagasapp.Activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class TraspasoEstacionActivity extends AppCompatActivity implements TraspasoEstacionView {
    boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal;
    Session session;
    String[] lista_estacion_salida,lista_medidor,lista_pipa_entrada;
    TraspasoDTO traspasoDTO;

    TextView TVTraspasoEstacionActivityTitulo,TVTraspasoEstacionActivityEstacionSalidaTitulo,
            TVTraspasoEstacionActivityMedidor,TVTraspasoEstacionActivityPipaDeEntrada;
    Spinner STraspasoEstacionActivityEstacionSalida,STraspasoEstacionActivityMedidor,
            STraspasoEstacionActivityPipaEntrada;
    Button BtnTraspasoEstacionActivityAceptar;

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
        }
        session = new Session(this);
        traspasoDTO = new TraspasoDTO();

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

             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {

             }
         });

         STraspasoEstacionActivityMedidor.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {

             }
         });

         STraspasoEstacionActivityPipaEntrada.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {

             }
         });

         lista_estacion_salida = new String[]{"Estación No.1","Estación No.2"};
         lista_medidor = new String[]{"Magnatel","Rotogate"};
         lista_pipa_entrada = new String[]{"Pipa No, 1","Pipa No. 2"};

         BtnTraspasoEstacionActivityAceptar.setOnClickListener(v -> {

         });
    }
}
