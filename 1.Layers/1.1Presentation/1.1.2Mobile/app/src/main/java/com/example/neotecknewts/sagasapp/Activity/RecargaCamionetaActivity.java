package com.example.neotecknewts.sagasapp.Activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.R;

public class RecargaCamionetaActivity extends AppCompatActivity {
    public TextView TVRecargaCamionetaActivityTitulo;
    public Spinner SRecargaCamionetaActivityListaCamionetas;
    public Button BRecargaCamionetaActivityGuardar;
    public boolean EsRecargaCamioneta;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recarga_camioneta);
        Bundle bundle = getIntent().getExtras();
        if (bundle!=null){
            EsRecargaCamioneta = bundle.getBoolean("EsRecargaCamioneta",false);
        }
        TVRecargaCamionetaActivityTitulo = findViewById(R.id.TVRecargaCamionetaActivityTitulo);
        SRecargaCamionetaActivityListaCamionetas = findViewById(R.id.SRecargaCamionetaActivityListaCamionetas);
        BRecargaCamionetaActivityGuardar = findViewById(R.id.BRecargaCamionetaActivityGuardar);

        BRecargaCamionetaActivityGuardar.setOnClickListener(v->{
            ValidarForm();
        });
    }
    protected void ValidarForm(){
        
    }
}
