/**
 * Clase para generar las tablas para
 * grid de datos
 * @author Jorge Omar Tovar Martínez
 * @company Neoteck
 * @date 27/09/2018
 * @update 27/09/2018
 */
package com.example.neotecknewts.sagasapp.Util;

import android.app.Activity;
import android.content.res.Resources;
import android.graphics.Paint;
import android.graphics.Rect;
import android.view.Gravity;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import java.util.ArrayList;

public class Tabla {
    private TableLayout tableLayout;
    private Resources resources;
    private Activity activity;

    public Tabla(Activity activity, TableLayout tableLayout){
        this.activity = activity;
        this.tableLayout = tableLayout;
        resources = this.activity.getResources();
    }

    public void Cabecera(int recursos){
        TableRow.LayoutParams layoutCelda;
        TableRow fila = new TableRow(activity);
        TableRow.LayoutParams layoutFila = new TableRow.LayoutParams(
                TableRow.LayoutParams.WRAP_CONTENT,
                TableRow.LayoutParams.WRAP_CONTENT
        );
        fila.setLayoutParams(layoutFila);
        String[] cabecera = resources.getStringArray(recursos);
        for (String aCabecera : cabecera) {
            TextView text = new TextView(activity);
            layoutCelda = new TableRow.LayoutParams(obtenerAnchoPixelesText(aCabecera),
                    TableRow.LayoutParams.WRAP_CONTENT);
            text.setText(aCabecera);
            text.setGravity(Gravity.LEFT);
            text.setTextSize(30);
            //text.setTextAppearance(activity, R.style.estilo_celda);
            //text.setBackgroundResource(R.drawable.tabla_celda_cabecera);
            text.setLayoutParams(layoutCelda);
            fila.addView(text);
        }
        tableLayout.addView(fila);
    }

    public void agregarFila(ArrayList<String[]> elementos){
        TableRow.LayoutParams layoutCelda;
        TableRow.LayoutParams layoutFila = new TableRow.LayoutParams(
                TableRow.LayoutParams.WRAP_CONTENT, TableRow.LayoutParams.WRAP_CONTENT);


        for (String[] elemento:elementos){
            TableRow fila = new TableRow(activity);
            fila.setLayoutParams(layoutFila);
            for (int x = 0; x<elemento.length;x++) {
                TextView texto = new TextView(activity);
                texto.setText(String.valueOf(elemento[x]));
                texto.setGravity(Gravity.LEFT);
                texto.setTextSize(30);
                //texto.setTextAppearance(activity, R.style.estilo_celda);
                //texto.setBackgroundResource(R.drawable.tabla_celda);
                layoutCelda = new TableRow.LayoutParams(obtenerAnchoPixelesText(
                        texto.getText().toString()), TableRow.LayoutParams.WRAP_CONTENT);
                texto.setLayoutParams(layoutCelda);

                fila.addView(texto);
            }
            tableLayout.addView(fila);
        }

    }

    private int obtenerAnchoPixelesText(String cabecera) {
        Paint p = new Paint();
        Rect bounds = new Rect();
        p.setTextSize(50);
        p.getTextBounds(cabecera, 0, cabecera.length(), bounds);
        return bounds.width();
    }
}