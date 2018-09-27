package com.example.neotecknewts.sagasapp.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;

public class AnticipoTablaActivity extends AppCompatActivity {
    Button BtnAnticipoTablaActivityRegresar,BtnAnticipoTablaActivityHacerAnticipo;
    TableLayout TLAnticipoTablaActivityTabla,TLAnticipoTablaActivityResultadosCorteDeCaja;
    TextView TVAnticipoTablaActivityTotal,TVAnticipoTablaActivityTitulo,TVAnticipoTablaActivityP5000;
    Spinner SPAnticipoTablaActivityFechaCorte;
    TableRow TRAnticipoTablaActivityTituloAnticipo,TRAnticipoTablaActivityFormAnticipar;

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
        SPAnticipoTablaActivityFechaCorte = findViewById(R.id.SPAnticipoTablaActivityFechaCorte);
        SPAnticipoTablaActivityFechaCorte.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo = findViewById(R.id.TVAnticipoTablaActivityTitulo);
        TVAnticipoTablaActivityP5000 = findViewById(R.id.TVAnticipoTablaActivityP5000);
        TLAnticipoTablaActivityResultadosCorteDeCaja = findViewById(R.id.
                TLAnticipoTablaActivityResultadosCorteDeCaja);
        TLAnticipoTablaActivityResultadosCorteDeCaja.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityTituloAnticipo = findViewById(R.id.
                TRAnticipoTablaActivityTituloAnticipo);
        TRAnticipoTablaActivityTituloAnticipo.setVisibility((EsAnticipo)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityFormAnticipar = findViewById(R.id.
                TRAnticipoTablaActivityFormAnticipar);
        TRAnticipoTablaActivityFormAnticipar.setVisibility((EsAnticipo)? View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo.setText((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        setTitle((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        BtnAnticipoTablaActivityRegresar.setOnClickListener(V->finish());
        BtnAnticipoTablaActivityHacerAnticipo.setOnClickListener(V->{
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            startActivity(intent);
        });
        BtnAnticipoTablaActivityHacerAnticipo.setText((EsCorte)?getString(R.string.hacer_corte):
        getString(R.string.hacer_anticipo));
        TVAnticipoTablaActivityP5000.setVisibility((EsCorte)? View.VISIBLE:View.GONE);
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
