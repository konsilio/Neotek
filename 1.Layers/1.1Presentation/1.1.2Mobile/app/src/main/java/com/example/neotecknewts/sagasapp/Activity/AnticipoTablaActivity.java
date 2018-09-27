package com.example.neotecknewts.sagasapp.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;

public class AnticipoTablaActivity extends AppCompatActivity {
    Button BtnAnticipoTablaActivityRegresar,BtnAnticipoTablaActivityHacerAnticipo;
    TableLayout TLAnticipoTablaActivityTabla;
    TextView TVAnticipoTablaActivityTotal;
    float total;
    ArrayList<String[]> elementos;
    boolean EsAnticipo,EsCorte;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_tabla);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);
        }
        BtnAnticipoTablaActivityRegresar = findViewById(R.id.BtnAnticipoTablaActivityRegresar);
        BtnAnticipoTablaActivityHacerAnticipo = findViewById(R.id.
                BtnAnticipoTablaActivityHacerAnticipo);
        TVAnticipoTablaActivityTotal = findViewById(R.id.TVAnticipoTablaActivityTotal);
        BtnAnticipoTablaActivityRegresar.setOnClickListener(V->finish());
        BtnAnticipoTablaActivityHacerAnticipo.setOnClickListener(V->{
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            startActivity(intent);
        });
        TLAnticipoTablaActivityTabla = findViewById(R.id.TLAnticipoTablaActivityTabla);
        Tabla tabla = new Tabla(this, TLAnticipoTablaActivityTabla);
        tabla.Cabecera(R.array.header_tabla_anticipo);
        elementos = new ArrayList<>();

        NumberFormat format = NumberFormat.getCurrencyInstance();
        for(int i = 0; i < 15; i++)
        {
            elementos.add(new String[]{"201809180785236","18/09/2018",
                    format.format(i*100.00)});
            total += i*100;
        }
        tabla.agregarFila(elementos);

        TVAnticipoTablaActivityTotal.setText(format.format(total));
    }
}
