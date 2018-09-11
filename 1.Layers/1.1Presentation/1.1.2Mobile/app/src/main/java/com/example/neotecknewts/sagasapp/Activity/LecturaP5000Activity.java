package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.R;

public class LecturaP5000Activity extends AppCompatActivity implements LecturaP5000View,
        View.OnClickListener {
    public TextView TVLecturaP5000Titulo,TVLecturaP5000Tipo,TVLecturaP5000Pregunta
            ,TVLecturaP5000Registro;
    public NumberPicker NPLecturaP500CantidadLectura;
    public Button BtnLecturaP5000Guardar;

    public Boolean EsLecturaInicial,EsLecturaFinal,EsLecturaFinalPipa,EsLecturaInicialPipa;
    public LecturaDTO lecturaDTO;
    public LecturaPipaDTO lecturaPipaDTO;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_p5000);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaInicial = (boolean) b.get("EsLecturaInicial");
            EsLecturaFinal = (boolean) b.get("EsLecturaFinal");
            EsLecturaFinalPipa = (boolean) b.get("EsLecturaFinalPipa");
            EsLecturaInicialPipa = (boolean) b.get("EsLecturaInicialPipa");
            if(EsLecturaInicial){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
            }else if(EsLecturaFinal){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
            }else if (EsLecturaInicialPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
            }else if(EsLecturaFinalPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
            }
        }

        TVLecturaP5000Titulo = findViewById(R.id.TVLecturaP5000Titulo);
        TVLecturaP5000Tipo = findViewById(R.id.TVLecturaP5000Tipo);
        TVLecturaP5000Pregunta = findViewById(R.id.TVLecturaP5000Pregunta);
        TVLecturaP5000Registro = findViewById(R.id.TVLecturaP5000Registro);
        if(EsLecturaInicial) {
            TVLecturaP5000Titulo.setText(  R.string.toma_de_lectura_inicial );
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+
                    lecturaDTO.getNombreEstacionCarburacion());
        }else if(EsLecturaFinal){
            TVLecturaP5000Titulo.setText( R.string.toma_de_lectura_final);
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+
                    lecturaDTO.getNombreEstacionCarburacion());
        }else if (EsLecturaInicialPipa){
            TVLecturaP5000Titulo.setText( R.string.toma_de_lectura_inicial);
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+" "+getString(R.string.Pipa));
        }else if (EsLecturaFinalPipa){
            TVLecturaP5000Titulo.setText(R.string.toma_de_lectura_final);
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+" "+getString(R.string.Pipa));
        }

        if(EsLecturaInicial||EsLecturaFinal ) {
            TVLecturaP5000Registro.setText(R.string.registra_la_lectura_del_p500_de_la_estaci_n);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            TVLecturaP5000Registro.setText(R.string.registra_la_lectura_del_p500_de_la_pipa);
        }

        NPLecturaP500CantidadLectura = findViewById(R.id.NPLecturaP500CantidadLectura);


        BtnLecturaP5000Guardar = findViewById(R.id.BtnLecturaP5000Guardar);
        BtnLecturaP5000Guardar.setOnClickListener(this);

        NPLecturaP500CantidadLectura.setValue(5000);
        NPLecturaP500CantidadLectura.setMaxValue(5000);
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
                    VerificaValor();
                break;
        }
    }

    @Override
    public void VerificaValor() {
        String mensaje = "";
        boolean error = false;
        int CantidadP500 = NPLecturaP500CantidadLectura.getValue();
        if (CantidadP500 ==0)
        {
            mensaje += "La lectura del P5000 es un valor requerido";
            error = true;
        }else{
            if(CantidadP500<0) {
                mensaje += "La lectura del P5000 es un valor entero mayor a cero";
                error = true;
            }
        }

        if(error){
            mensaje = getString(R.string.mensjae_error_campos)+"\n"+mensaje;
            AlertDialog.Builder dialogo= CrearDialogo(R.string.error_titulo,mensaje);
            dialogo.setNegativeButton(R.string.message_acept, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            dialogo.create();
            dialogo.show();
        }else{
            Intent intent = new Intent(LecturaP5000Activity.this,
                    CameraLecturaActivity.class);
            if(EsLecturaInicial || EsLecturaFinal) {
                intent.putExtra("EsLecturaInicial",EsLecturaInicial);
                intent.putExtra("EsLecturaFinal",EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa",false);
                intent.putExtra("EsLecturaFinalPipa",false);
                lecturaDTO.setCantidadP5000(CantidadP500);
                intent.putExtra("EsFotoP5000",true);
                intent.putExtra("lecturaDTO",lecturaDTO);
            }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
                lecturaPipaDTO.setCantidadP5000(CantidadP500);
                intent.putExtra("lecturaPipaDTO",lecturaPipaDTO);
                intent.putExtra("EsLecturaInicial",EsLecturaInicial);
                intent.putExtra("EsLecturaFinal",EsLecturaFinal);
                intent.putExtra("EsLecturaInicialPipa",EsLecturaInicialPipa);
                intent.putExtra("EsLecturaFinalPipa",EsLecturaFinalPipa);
                intent.putExtra("EsFotoP5000",true);
            }
            startActivity(intent);
        }
    }
}