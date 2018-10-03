package com.example.neotecknewts.sagasapp.Activity;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TableLayout;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaPagarActivity extends AppCompatActivity {
    VentaDTO ventaDTO;
    TableLayout TLPuntoVentaPagarActivityConcepto,TLPuntoVentaPagarActivityConceptoHeader;
    Tabla tabla;
    ArrayList<String[]> lista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_pagar);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
        }
        TLPuntoVentaPagarActivityConcepto = findViewById(R.id.TLPuntoVentaPagarActivityConcepto);
        TLPuntoVentaPagarActivityConceptoHeader = findViewById(R.id.
                TLPuntoVentaPagarActivityConceptoHeader);

        NumberFormat format = NumberFormat.getCurrencyInstance();
        lista = new ArrayList<>();
        for (ConceptoDTO conceptoDTO:
                ventaDTO.getConcepto()) {
            lista.add(new String[]{
                    conceptoDTO.getConcepto(),
                    String.valueOf(conceptoDTO.getCantidad()),
                    format.format(conceptoDTO.getPUnitario()),
                    format.format(conceptoDTO.getDescuento()),
                    format.format(conceptoDTO.getSubtotal())
            });
        }
        tabla = new Tabla(PuntoVentaPagarActivity.this);
        tabla.CabeceraPersonalizada(R.array.condepto_venta,TLPuntoVentaPagarActivityConceptoHeader);
        Tabla tabla2 = new Tabla(PuntoVentaPagarActivity.this);
        tabla2.agregarFilaPersonalizada(lista,TLPuntoVentaPagarActivityConcepto);
    }
}
