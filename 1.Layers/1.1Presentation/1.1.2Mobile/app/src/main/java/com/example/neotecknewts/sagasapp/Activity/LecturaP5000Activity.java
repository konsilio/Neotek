package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.R;

public class LecturaP5000Activity extends AppCompatActivity implements LecturaP5000View,
        View.OnClickListener {
    public Boolean EsLecturaInicial,EsLecturaFinal;
    public int IdEstacionCarburacion,IdTipoMedidor,CantidadDeFotos;
    public String EstacionCarburacionNombre,MedidorNombre;
    public TextView TVLecturaP5000Titulo,TVLecturaP5000Tipo,TVLecturaP5000Pregunta
            ,TVLecturaP5000Registro;
    public EditText ETLecturaP500CantidadLectura;
    public Button BtnLecturaP5000Guardar;


    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_p5000);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaInicial = (boolean) b.get("EsLecturaInicial");
            EsLecturaFinal = (boolean) b.get("EsLecturaFinal");
            IdEstacionCarburacion = (int) b.get("IdEstacionCarburacion");
            EstacionCarburacionNombre = (String) b.get("EstacionCarburacionNombre");
            IdTipoMedidor = (int) b.get("IdTipoMedidor");
            MedidorNombre = (String) b.get("MedidorNombre");
            CantidadDeFotos = (int)b.get("CantidadDeFotos");
        }

        TVLecturaP5000Titulo = findViewById(R.id.TVLecturaP5000Titulo);
        TVLecturaP5000Tipo = findViewById(R.id.TVLecturaP5000Tipo);
        TVLecturaP5000Pregunta = findViewById(R.id.TVLecturaP5000Pregunta);
        TVLecturaP5000Registro = findViewById(R.id.TVLecturaP5000Registro);

        TVLecturaP5000Titulo.setText(EsLecturaInicial ? R.string.toma_de_lectura_inicial:R.string.toma_de_lectura_final);
        TVLecturaP5000Tipo.setText(getString(R.string.p500)+EstacionCarburacionNombre);
        TVLecturaP5000Registro.setText(R.string.registra_la_lectura_del_p500_de_la_estaci_n);

        ETLecturaP500CantidadLectura = findViewById(R.id.ETLecturaP500CantidadLectura);
        ETLecturaP500CantidadLectura.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if(actionId == EditorInfo.IME_ACTION_DONE) {
                    BtnLecturaP5000Guardar.performClick();
                }
                return false;
            }
        });

        BtnLecturaP5000Guardar = findViewById(R.id.BtnLecturaP5000Guardar);
        BtnLecturaP5000Guardar.setOnClickListener(this);

    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {

        if(keyCode == KeyEvent.KEYCODE_BACK){
            AlertDialog.Builder builder = CrearDialogo(R.string.title_alert_message,getString(R.string.message_goback_diabled));
            builder.setPositiveButton(R.string.label_si, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    Intent intent = new Intent(LecturaP5000Activity.this,
                            MenuActivity.class);
                    intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    startActivity(intent);
                    finish();
                }
            });
            builder.setNegativeButton(R.string.label_no, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            builder.show();
        }
        return false;

    }

    private AlertDialog.Builder CrearDialogo(int Titulo, String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(Titulo);
        builder.setMessage(mensaje);

        return builder;
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()){
            case R.id.BtnLecturaP5000Guardar:
                    //Intent intent = new Intent()
                    VerificaValor();
                break;
        }
    }

    @Override
    public void VerificaValor() {
        String mensaje = "";
        boolean error = false;
        if (ETLecturaP500CantidadLectura.getText().toString().equals(""))
        {
            mensaje += "La lectura del P5000 es un valor requerido";
            error = true;
        }else{
            if(Integer.parseInt(ETLecturaP500CantidadLectura.getText().toString())<=0) {
                mensaje += "La lectura del P5000 es un valor entero mayor a cero";
                error = true;
            }
        }

        if(error){
            mensaje = getString(R.string.mensjae_error_campos)+"\n"+mensaje;
        }

        AlertDialog.Builder dialogo= CrearDialogo(R.string.error_titulo,mensaje);
        dialogo.setNegativeButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        dialogo.create();
        dialogo.show();

    }
}
