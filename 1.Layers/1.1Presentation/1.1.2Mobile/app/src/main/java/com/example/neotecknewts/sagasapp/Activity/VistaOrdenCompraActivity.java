package com.example.neotecknewts.sagasapp.Activity;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.text.method.ScrollingMovementMethod;
import android.view.Gravity;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;


import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class VistaOrdenCompraActivity extends AppCompatActivity {

    public TextView textViewProveedor;
    public TableLayout tableLayout;
    public Spinner spinnerOrdenCompra;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_vista_orden_compra);

        textViewProveedor = (TextView) findViewById(R.id.textViewProovedor);
        tableLayout = (TableLayout) findViewById(R.id.tableProductos);
        spinnerOrdenCompra = (Spinner) findViewById(R.id.spinner_orden_compra);
        String[] ordenes = {"OC1", "OC2"};


        spinnerOrdenCompra.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        textViewProveedor.setMovementMethod(new ScrollingMovementMethod());
        textViewProveedor.setText("Proveedor S.A. de C.V. \nAv. Universidad #435 Col. Lazaro Cardenas \nChilpancingo Guerrero, Mex. \n" +
                "Cp. 55472 \n \nTels. 546-56-56");

        for(int i=0; i<50;i++) {
            TableRow row = new TableRow(this);
            TextView tv = new TextView(this);
            TextView tv1 = new TextView(this);
            TextView tv2 = new TextView(this);
            TextView tv3 = new TextView(this);
            TextView tv4 = new TextView(this);
            TextView tv5 = new TextView(this);
            tv.setText("Llantas");
            tv1.setText("4");
            tv1.setGravity(Gravity.RIGHT);
            tv1.setPadding(0,0,10,0);
            tv2.setText("pz");
            tv3.setText("$2.00");
            tv3.setGravity(Gravity.RIGHT);
            tv4.setText("0%");
            tv4.setGravity(Gravity.CENTER);
            tv5.setText("$3459");
            tv5.setGravity(Gravity.RIGHT);
            tv.setTextSize(20);
            tv1.setTextSize(20);
            tv2.setTextSize(20);
            tv3.setTextSize(20);
            tv4.setTextSize(20);
            tv5.setTextSize(20);
            tv.setTextColor(getResources().getColor(R.color.colorTextLogin));
            tv1.setTextColor(getResources().getColor(R.color.colorTextLogin));
            tv2.setTextColor(getResources().getColor(R.color.colorTextLogin));
            tv3.setTextColor(getResources().getColor(R.color.colorTextLogin));
            tv4.setTextColor(getResources().getColor(R.color.colorTextLogin));
            tv5.setTextColor(getResources().getColor(R.color.colorTextLogin));
            row.addView(tv);
            row.addView(tv1);
            row.addView(tv2);
            row.addView(tv3);
            row.addView(tv4);
            row.addView(tv5);
            tableLayout.addView(row);
        }

    }
}
