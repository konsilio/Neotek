package com.example.neotecknewts.sagasapp.Activity;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;

import com.example.neotecknewts.sagasapp.Adapter.EstacionesAdatper;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;
import java.util.List;

public class AnticipoEstacionCarburacionActivity extends AppCompatActivity {
    RecyclerView RVAnticipoEstacionesCarburacionActivityContainer;

    private boolean EsAnticipo,EsCorte;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_estacion_carburacion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);
        }
        RVAnticipoEstacionesCarburacionActivityContainer = findViewById(R.id.
                RVAnticipoEstacionesCarburacionActivityContainer);
        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(
                AnticipoEstacionCarburacionActivity.this);
        RVAnticipoEstacionesCarburacionActivityContainer.setLayoutManager(linearLayoutManager);
        RVAnticipoEstacionesCarburacionActivityContainer.setHasFixedSize(true);
        EstacionesAdatper adatper = new EstacionesAdatper(this,getLista(),
                EsAnticipo,
                EsCorte
        );
        setTitle((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        RVAnticipoEstacionesCarburacionActivityContainer.setAdapter(adatper);
    }

    private List<DatosEstacionesDTO> getLista() {
        List<DatosEstacionesDTO> list = new ArrayList<>();
        //Coloco el Header;
        DatosEstacionesDTO header = new DatosEstacionesDTO();
        header.setNombreCAlmacen(getString(R.string.selecciona_la_estacion_de_carburacion));
        list.add(header);
        //Items
        for (int x = 0; x < 20; x++){
            DatosEstacionesDTO item = new DatosEstacionesDTO();
            item.setNombreCAlmacen("Estación Bulevar "+String.valueOf(x));
            item.setIdCAlmacenGas(x);
            list.add(item);
        }
        return  list;
    }
}
