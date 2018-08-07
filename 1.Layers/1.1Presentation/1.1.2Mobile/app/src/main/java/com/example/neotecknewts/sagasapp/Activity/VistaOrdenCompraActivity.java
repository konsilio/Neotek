package com.example.neotecknewts.sagasapp.Activity;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.text.method.ScrollingMovementMethod;
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

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_vista_orden_compra);

        textViewProveedor = (TextView) findViewById(R.id.textViewProovedor);
        tableLayout = (TableLayout) findViewById(R.id.tableProductos);
        textViewProveedor.setMovementMethod(new ScrollingMovementMethod());
        textViewProveedor.setText("Proveedor S.A. de C.V. \n Av. Universidad #435 Col. Lazaro Cardenas \n Chilpancingo Guerrero, Mex. \n" +
                "Cp. 55472 \n \n Tels. 546-56-56");


        TableRow row = new TableRow(this);
        TextView tv = new TextView(this);
        TextView tv1 = new TextView(this);
        tv.setText("This is text");
        tv1.setText("This is text");
        row.addView(tv);
        row.addView(tv1);
        tableLayout.addView(row);

    }
}
