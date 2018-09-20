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

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
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
    public RecargaDTO recargaDTO;
    public AutoconsumoDTO autoconsumoDTO;

    public int max_p5000,p5000;
    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsAutoconsumoEstacionInicial,EsAutoconsumoEstacionFinal;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_p5000);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaInicial =  b.getBoolean("EsLecturaInicial",false);
            EsLecturaFinal = b.getBoolean("EsLecturaFinal",false);
            EsLecturaFinalPipa =  b.getBoolean("EsLecturaFinalPipa",false);
            EsLecturaInicialPipa = b.getBoolean("EsLecturaInicialPipa",false);
            EsRecargaEstacionInicial = b.getBoolean("EsRecargaEstacionInicial",false);
            EsRecargaEstacionFinal = b.getBoolean("EsRecargaEstacionFinal",false);
            EsPrimeraLectura = b.getBoolean("EsPrimeraLectura",false);
            EsAutoconsumoEstacionInicial = b.getBoolean("EsAutoconsumoEstacionInicial",false);
            EsAutoconsumoEstacionFinal = b.getBoolean("EsAutoconsumoEstacionFinal",false);

            if(EsLecturaInicial){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
                max_p5000 = lecturaDTO.getCantidadP5000();
                p5000 = lecturaDTO.getCantidadP5000();
            }else if(EsLecturaFinal){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
                max_p5000 = lecturaDTO.getCantidadP5000();
                p5000 = lecturaDTO.getCantidadP5000();
            }else if (EsLecturaInicialPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
                max_p5000 = lecturaPipaDTO.getCantidadP5000();
                p5000 = lecturaPipaDTO.getCantidadP5000();

            }else if(EsLecturaFinalPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
                max_p5000 = lecturaPipaDTO.getCantidadP5000();
                p5000 = lecturaPipaDTO.getCantidadP5000();
            }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
                recargaDTO = (RecargaDTO) b.getSerializable("recargaDTO");

                max_p5000 = 5000;
                p5000 = 5000;
            }else if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
                autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");
                max_p5000 = 5000;
                p5000 = 5000;
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

        if(EsRecargaEstacionInicial||EsRecargaEstacionFinal){
            TVLecturaP5000Titulo.setText(getString(R.string.registra_la_lectura_del_p500_de_la_pipa));
            if(EsLecturaInicial) {
                TVLecturaP5000Titulo.setText(getString(R.string.p500) + " " + getString(R.string.Pipa));
            }else{
                TVLecturaP5000Titulo.setText(getString(R.string.p500) + " " + getString(R.string.Estacion));
            }
        }

        if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            TVLecturaP5000Titulo.setText((
                    (EsAutoconsumoEstacionInicial) ?
                            getString(R.string.carburaci_n_de_gas)+" - Incial":
                            getString(R.string.carburaci_n_de_gas)+" - Final")
                    + "\n P5000 Estación"
            );
            TVLecturaP5000Registro.setText(getString(R.string.registra_la_lectura_del_p500_de_la_estaci_n));
        }

        NPLecturaP500CantidadLectura = findViewById(R.id.NPLecturaP500CantidadLectura);


        BtnLecturaP5000Guardar = findViewById(R.id.BtnLecturaP5000Guardar);
        BtnLecturaP5000Guardar.setOnClickListener(this);

        NPLecturaP500CantidadLectura.setValue(p5000);
        NPLecturaP500CantidadLectura.setMaxValue(max_p5000);
        NPLecturaP500CantidadLectura.setMinValue(0);
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
            }else if(EsRecargaEstacionInicial||EsRecargaEstacionFinal){
                if(EsLecturaInicial) {
                    recargaDTO.setP5000Salida(CantidadP500);
                } else {
                    recargaDTO.setP5000Salida(CantidadP500);
                }
                intent.putExtra("EsRecargaEstacionInicial",EsRecargaEstacionInicial);
                intent.putExtra("EsRecargaEstacionFinal",EsRecargaEstacionFinal);
                intent.putExtra("EsPrimeraLectura",EsPrimeraLectura);
                intent.putExtra("EsLecturaFinal",false);
                intent.putExtra("EsLecturaInicial",false);
                intent.putExtra("EsLecturaInicialPipa",false);
                intent.putExtra("EsLecturaFinalPipa",false);
                intent.putExtra("EsFotoP5000",false);
                intent.putExtra("recargaDTO",recargaDTO);
                startActivity(intent);
            }else if (EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
                autoconsumoDTO.setP5000Salida(CantidadP500);
                /*intent.putExtra("EsRecargaEstacionInicial",false);
                intent.putExtra("EsRecargaEstacionFinal",false);
                intent.putExtra("EsPrimeraLectura",false);
                intent.putExtra("EsLecturaFinal",false);
                intent.putExtra("EsLecturaInicial",false);
                intent.putExtra("EsLecturaInicialPipa",false);
                intent.putExtra("EsLecturaFinalPipa",false);
                intent.putExtra("EsFotoP5000",false);*/
                intent.putExtra("EsAutoconsumoEstacionInicial",EsAutoconsumoEstacionInicial);
                intent.putExtra("EsAutoconsumoEstacionFinal",EsAutoconsumoEstacionFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }
            startActivity(intent);
        }
    }
}
