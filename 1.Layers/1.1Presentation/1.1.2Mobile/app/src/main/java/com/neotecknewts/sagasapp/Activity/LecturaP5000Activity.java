package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.neotecknewts.sagasapp.Model.LecturaDTO;
import com.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.TraspasoDTO;

public class LecturaP5000Activity extends AppCompatActivity implements LecturaP5000View,
        View.OnClickListener {
    public TextView TVLecturaP5000Titulo,TVLecturaP5000Tipo,TVLecturaP5000Pregunta
            ,TVLecturaP5000Registro;
    public NumberPicker NPLecturaP500CantidadLectura;
    public Button BtnLecturaP5000Guardar;
    public EditText ETLecturaP5000CantidadNull;

    public Boolean EsLecturaInicial,EsLecturaFinal,EsLecturaFinalPipa,EsLecturaInicialPipa;
    public LecturaDTO lecturaDTO;
    public LecturaPipaDTO lecturaPipaDTO;
    public RecargaDTO recargaDTO;
    public AutoconsumoDTO autoconsumoDTO;
    public TraspasoDTO traspasoDTO;
    public CalibracionDTO calibracionDTO;

    public int max_p5000,p5000;
    public boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsAutoconsumoEstacionInicial,EsAutoconsumoEstacionFinal;
    public boolean EsAutoconsumoInvetarioInicial, EsAutoconsumoInventarioFinal;
    public boolean EsAutoconsumoPipaInicial,EsAutoconsumoPipaFinal;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    public boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal,EsPasoIniciaLPipa;
    public boolean EsCalibracionEstacionInicial,EsCalibracionEstacionFinal;
    public boolean EsCalibracionPipaInicial,EsCalibracionPipaFinal;

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
            EsAutoconsumoInvetarioInicial = b.getBoolean("EsAutoconsumoInvetarioInicial",false);
            EsAutoconsumoInventarioFinal = b.getBoolean("EsAutoconsumoInventarioFinal",false);
            EsAutoconsumoPipaInicial = b.getBoolean("EsAutoconsumoPipaInicial",false);
            EsAutoconsumoPipaFinal = b.getBoolean("EsAutoconsumoPipaFinal",false);
            EsTraspasoEstacionInicial = b.getBoolean("EsTraspasoEstacionInicial",false);
            EsTraspasoEstacionFinal = b.getBoolean("EsTraspasoEstacionFinal",false);
            EsPrimeraParteTraspaso = b.getBoolean("EsPrimeraParteTraspaso",true);
            EsTraspasoPipaInicial = b.getBoolean("EsTraspasoPipaInicial",false);
            EsTraspasoPipaFinal = b.getBoolean("EsTraspasoPipaFinal",false);
            EsPasoIniciaLPipa = b.getBoolean("EsPasoIniciaLPipa",true);
            EsCalibracionEstacionInicial = b.getBoolean("EsCalibracionEstacionInicial",
                    false);
            EsCalibracionEstacionFinal = b.getBoolean("EsCalibracionEstacionFinal",
                    false);
            EsCalibracionPipaInicial = b.getBoolean("EsCalibracionPipaInicial",
                    false);
            EsCalibracionPipaFinal = b.getBoolean("EsCalibracionPipaFinal",
                    false);
            ETLecturaP5000CantidadNull = findViewById(R.id.ETLecturaP5000CantidadNull);
            NPLecturaP500CantidadLectura = findViewById(R.id.NPLecturaP500CantidadLectura);

            if(EsLecturaInicial){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
                max_p5000 = lecturaDTO.getCantidadP5000();
                p5000 = lecturaDTO.getCantidadP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.toma_de_lectura);
            }else if(EsLecturaFinal){
                lecturaDTO  = (LecturaDTO) b.getSerializable ("lecturaDTO");
                max_p5000 = 99999;/*lecturaDTO.getCantidadP5000();*/

                p5000 = lecturaDTO.getCantidadP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.toma_de_lectura);
            }else if (EsLecturaInicialPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
                max_p5000 = 99999;/*lecturaPipaDTO.getCantidadP5000();*/
                p5000 = lecturaPipaDTO.getCantidadP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.toma_de_lectura);
            }else if(EsLecturaFinalPipa){
                lecturaPipaDTO = (LecturaPipaDTO) b.getSerializable("lecturaPipaDTO");
                max_p5000 = 99999; /*lecturaPipaDTO.getCantidadP5000();*/
                p5000 = lecturaPipaDTO.getCantidadP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.toma_de_lectura);
            }else if(EsRecargaEstacionInicial || EsRecargaEstacionFinal){
                recargaDTO = (RecargaDTO) b.getSerializable("recargaDTO");
                if(EsPrimeraLectura) {
                    max_p5000 = 99999;/*recargaDTO.getP5000Salida();*/
                    p5000 = recargaDTO.getP5000Salida();
                }else{
                    max_p5000 = 99999;recargaDTO.getP5000Entrada();
                    p5000 = recargaDTO.getP5000Entrada();
                }
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.recarga);
            }else if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
                autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");
                max_p5000 = 99999;/*autoconsumoDTO.getP5000Salida();*/
                p5000 = autoconsumoDTO.getP5000Salida();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.Autoconsumo);
            }else if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
                autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");
                max_p5000 = 99999; /*autoconsumoDTO.getP5000Salida();*/
                p5000 = autoconsumoDTO.getP5000Salida();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.Autoconsumo);
            }else if (EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
                autoconsumoDTO = (AutoconsumoDTO) b.getSerializable("autoconsumoDTO");
                max_p5000 = 99999;/*autoconsumoDTO.getP5000Salida();*/
                p5000 = autoconsumoDTO.getP5000Salida();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.Autoconsumo);
            }else if (EsTraspasoEstacionInicial|| EsTraspasoEstacionFinal){
                traspasoDTO = (TraspasoDTO) b.getSerializable("traspasoDTO");
                if(EsPrimeraParteTraspaso) {
                    max_p5000 = traspasoDTO.getP5000Salida();
                    p5000 = traspasoDTO.getP5000Salida();
                }else{
                    max_p5000 = traspasoDTO.getP5000Entrada();
                    p5000 = traspasoDTO.getP5000Entrada();
                }
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.Traspaso);
            }else if(EsTraspasoPipaInicial||EsTraspasoPipaFinal){
                traspasoDTO = (TraspasoDTO) b.getSerializable("traspasoDTO");
                if(EsPasoIniciaLPipa ) {

                    max_p5000 = 99999; /*traspasoDTO.getP5000Salida();*/
                    p5000 = traspasoDTO.getP5000Salida();

                }else{
                    max_p5000 = traspasoDTO.getP5000Entrada();
                    p5000 = traspasoDTO.getP5000Entrada();
                }
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                setTitle(R.string.Traspaso);
            }else if(EsCalibracionEstacionInicial||EsCalibracionEstacionFinal){
                calibracionDTO = (CalibracionDTO) b.getSerializable("calibracionDTO");
                max_p5000 = 99999;/*calibracionDTO.getP5000();*/
                p5000 = calibracionDTO.getP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                //min_p5000= 5000;
                setTitle(R.string.Calibracion);
            }else if(EsCalibracionPipaInicial|| EsCalibracionPipaFinal){
                calibracionDTO = (CalibracionDTO) b.getSerializable("calibracionDTO");
                max_p5000 = 99999;/*calibracionDTO.getP5000();*/
                p5000 = calibracionDTO.getP5000();
                if(p5000<=0){
                    ETLecturaP5000CantidadNull.setVisibility(View.VISIBLE);
                    NPLecturaP500CantidadLectura.setVisibility(View.GONE);
                }else{
                    ETLecturaP5000CantidadNull.setVisibility(View.GONE);
                    NPLecturaP500CantidadLectura.setVisibility(View.VISIBLE);
                }
                //min_p5000= 5000;
                setTitle(R.string.Calibracion);
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
            String pipa_nombre = lecturaPipaDTO.getNombrePipa().isEmpty()?
                    "":lecturaPipaDTO.getNombrePipa();
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+" "
                    +getString(R.string.Pipa)+": "+pipa_nombre);
        }else if (EsLecturaFinalPipa){
            TVLecturaP5000Titulo.setText(R.string.toma_de_lectura_final);
            String pipa_nombre = lecturaPipaDTO.getNombrePipa().isEmpty()?
                    "":lecturaPipaDTO.getNombrePipa();
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+" "
                    +getString(R.string.Pipa)+": "+pipa_nombre);
        }

        if(EsLecturaInicial||EsLecturaFinal ) {
            TVLecturaP5000Registro.setText(R.string.registra_la_lectura_del_p500_de_la_estaci_n);
        }else if(EsLecturaInicialPipa || EsLecturaFinalPipa){
            TVLecturaP5000Registro.setText(R.string.registra_la_lectura_del_p500_de_la_pipa);
        }

        if(EsRecargaEstacionInicial||EsRecargaEstacionFinal){
            TVLecturaP5000Titulo.setText(getString(R.string.registra_la_lectura_del_p500_de_la_pipa));
            if(EsPrimeraLectura) {
                String nombre = recargaDTO.getNombreEstacionSalida().isEmpty()?"":
                        recargaDTO.getNombreEstacionSalida();
                TVLecturaP5000Titulo.setText(getString(R.string.p500) + " " +
                        getString(R.string.Pipa)+": "+nombre);
                TVLecturaP5000Tipo.setText(getString(R.string.p500)+
                        nombre);
            }else{
                String nombre = recargaDTO.getNombreEstacionEntrada().isEmpty()?"":
                        recargaDTO.getNombreEstacionEntrada();
                TVLecturaP5000Titulo.setText(getString(R.string.p500) + " " +
                        getString(R.string.Estacion)+": "+nombre);
                TVLecturaP5000Tipo.setText(getString(R.string.p500)+
                        nombre);
            }
        }

        if(EsAutoconsumoEstacionInicial || EsAutoconsumoEstacionFinal){
            String est = (autoconsumoDTO.getNombreEstacion().isEmpty())? "":
                    " "+autoconsumoDTO.getNombreEstacion();
            TVLecturaP5000Titulo.setText((
                    (EsAutoconsumoEstacionInicial) ?
                            getString(R.string.carburaci_n_de_gas)+" - Incial":
                            getString(R.string.carburaci_n_de_gas)+" - Final")
                    + "\n P5000 Estación"+est
            );
            TVLecturaP5000Tipo.setText(getString(R.string.p500)+" "+est);
            TVLecturaP5000Registro.setText(getString(R.string.registra_la_lectura_del_p500_de_la_estaci_n));
        }

        if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
            TVLecturaP5000Titulo.setText((EsAutoconsumoInvetarioInicial)?
                    getString(R.string.carburaci_n_de_gas)+" - Inicial":
                    getString(R.string.carburaci_n_de_gas)+" - Final");
            TVLecturaP5000Tipo.setText(
                   "P5000"
            );
            TVLecturaP5000Registro.setText("Registra la lectura del P5000");
        }

        if(EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
            TVLecturaP5000Titulo.setText((EsAutoconsumoPipaInicial)?
                    getString(R.string.carburaci_n_de_gas)+" - Inicial":
                    getString(R.string.carburaci_n_de_gas)+" - Final");
            TVLecturaP5000Tipo.setText(
                    "P5000 - Pipa"
            );
            TVLecturaP5000Registro.setText(getString(R.string.registra_la_lectura_del_p500_de_la_pipa));
        }

        if(EsTraspasoEstacionInicial||EsTraspasoEstacionFinal){
            TVLecturaP5000Titulo.setText((EsAutoconsumoPipaInicial) ?
                    getString(R.string.trasferencia_de_gas) + " - Inicial" :
                    getString(R.string.trasferencia_de_gas) + " - Final");
            if(EsPrimeraParteTraspaso) {
                String nombre = (traspasoDTO.getNombreEstacionTraspaso().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionTraspaso();
                TVLecturaP5000Tipo.setText(
                        "P5000 - " + getString(R.string.Estacion)+nombre
                );
                TVLecturaP5000Registro.setText(getString(R.string.registra_la_lectura_del_p500_de_la_estaci_n));
            }else{
                String nombre = (traspasoDTO.getNombreEstacionEntrada().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionEntrada();
                TVLecturaP5000Tipo.setText(
                        "P5000 - " + getString(R.string.Pipa)
                );
                TVLecturaP5000Registro.setText(getString(R.string.registra_la_lectura_del_p500_de_la_pipa));
            }
        }

        if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){

            TVLecturaP5000Titulo.setText((EsTraspasoPipaInicial) ?
                    getString(R.string.trasferencia_de_gas) + " - Inicial" :
                    getString(R.string.trasferencia_de_gas) + " - Final");
            if(EsPasoIniciaLPipa) {
                String title = (traspasoDTO.getNombreEstacionTraspaso().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionTraspaso();
                TVLecturaP5000Tipo.setText(
                        "P5000 - " + getString(R.string.Pipa)+title
                );
            }else{
                String title = (traspasoDTO.getNombreEstacionEntrada().isEmpty())?"":
                        ": "+traspasoDTO.getNombreEstacionEntrada();
                TVLecturaP5000Tipo.setText(
                        "P5000 - " + getString(R.string.Pipa)+title
                );
            }
            TVLecturaP5000Registro.setText("Registra la lectura del P5000 de Pipa");
        }

        if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
            TVLecturaP5000Titulo.setText((EsCalibracionEstacionInicial) ?
                    getString(R.string.Calibracion) + " - Inicial" :
                    getString(R.string.Calibracion) + " - Final");
            TVLecturaP5000Tipo.setText(
                    "P5000 - " + calibracionDTO.getNombreCAlmacenGas()
            );
            TVLecturaP5000Registro.setText("Registra la lectura del P5000 de la "+
                    calibracionDTO.getNombreCAlmacenGas());
        }

        if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
            TVLecturaP5000Titulo.setText((EsCalibracionPipaInicial) ?
                    getString(R.string.Calibracion) + " - Inicial" :
                    getString(R.string.Calibracion) + " - Final");
            TVLecturaP5000Tipo.setText(
                    "P5000 - " + calibracionDTO.getNombreCAlmacenGas()
            );
            TVLecturaP5000Registro.setText("Registra la lectura del P5000 de la "+
                    calibracionDTO.getNombreCAlmacenGas());
        }

        BtnLecturaP5000Guardar = findViewById(R.id.BtnLecturaP5000Guardar);
        BtnLecturaP5000Guardar.setOnClickListener(this);
        if(ETLecturaP5000CantidadNull.getVisibility()==View.VISIBLE &&
                !ETLecturaP5000CantidadNull.getText().toString().isEmpty() &&
        ETLecturaP5000CantidadNull.getText().toString().length()>0){
            p5000 = Integer.parseInt(ETLecturaP5000CantidadNull.getText().toString());
        }
        NPLecturaP500CantidadLectura.setMaxValue(999999);
        NPLecturaP500CantidadLectura.setMinValue(0);
        NPLecturaP500CantidadLectura.setValue(p5000);

        for (int i = 0; i < NPLecturaP500CantidadLectura.getChildCount(); i++) {
            View childView = NPLecturaP500CantidadLectura.getChildAt(i);
            if (childView instanceof EditText) {
                childView.setOnKeyListener((view, i1, keyEvent) -> {
                    if (keyEvent.getAction() == KeyEvent.ACTION_UP) {
                        NPLecturaP500CantidadLectura.setValue(Integer.parseInt(((EditText) view).getText().toString()));
                        return true;
                    }
                    return false;
                });
            }
        }
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
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
        int CantidadP500=0;
        if(!ETLecturaP5000CantidadNull.getText().toString().isEmpty()
                && ETLecturaP5000CantidadNull.getText().toString().length()>0){
             CantidadP500 = Integer.parseInt(ETLecturaP5000CantidadNull.getText().toString());
        }else{
            CantidadP500 = NPLecturaP500CantidadLectura.getValue();
        }

        if (CantidadP500 <0)
        {
            mensaje += "La lectura del P5000 es un valor requerido";
            error = true;
        }else{
            /*if(CantidadP500<0) {
                mensaje += "La lectura del P5000 es un valor entero mayor a cero";
                error = true;
            }*/
            error = false;
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
                if(EsPrimeraLectura) {
                    recargaDTO.setP5000Salida(CantidadP500);
                }else{
                    recargaDTO.setP5000Entrada(CantidadP500);
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
                intent.putExtra("EsAutoconsumoEstacionInicial",EsAutoconsumoEstacionInicial);
                intent.putExtra("EsAutoconsumoEstacionFinal",EsAutoconsumoEstacionFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }else if(EsAutoconsumoInvetarioInicial || EsAutoconsumoInventarioFinal){
                autoconsumoDTO.setP5000Salida(CantidadP500);
                intent.putExtra("EsAutoconsumoInvetarioInicial",EsAutoconsumoInvetarioInicial);
                intent.putExtra("EsAutoconsumoInventarioFinal",EsAutoconsumoInventarioFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }else if(EsAutoconsumoPipaInicial || EsAutoconsumoPipaFinal){
                autoconsumoDTO.setP5000Salida(CantidadP500);
                intent.putExtra("EsAutoconsumoPipaInicial",EsAutoconsumoPipaInicial);
                intent.putExtra("EsAutoconsumoPipaFinal",EsAutoconsumoPipaFinal);
                intent.putExtra("autoconsumoDTO",autoconsumoDTO);
                startActivity(intent);
            }else if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
                if(EsPrimeraParteTraspaso) {
                    traspasoDTO.setP5000Salida(CantidadP500);
                }else {
                    traspasoDTO.setP5000Entrada(CantidadP500);
                }
                intent.putExtra("EsTraspasoEstacionInicial",EsTraspasoEstacionInicial);
                intent.putExtra("EsTraspasoEstacionFinal",EsTraspasoEstacionFinal);
                intent.putExtra("EsPrimeraParteTraspaso",EsPrimeraParteTraspaso);
                intent.putExtra("traspasoDTO",traspasoDTO);
            }else if(EsTraspasoPipaInicial||EsTraspasoPipaFinal){
                if (EsPasoIniciaLPipa) {
                    traspasoDTO.setP5000Salida(CantidadP500);
                }else {
                    traspasoDTO.setP5000Entrada(CantidadP500);
                }
                intent.putExtra("EsTraspasoPipaInicial",EsTraspasoPipaInicial);
                intent.putExtra("EsTraspasoPipaFinal",EsTraspasoPipaFinal);
                intent.putExtra("EsPasoIniciaLPipa", EsPasoIniciaLPipa);
                intent.putExtra("EsPasoInicial",EsPasoIniciaLPipa);
                intent.putExtra("traspasoDTO",traspasoDTO);
            }else if(EsCalibracionEstacionInicial || EsCalibracionEstacionFinal){
                calibracionDTO.setP5000(CantidadP500);
                intent.putExtra("EsCalibracionEstacionInicial",EsCalibracionEstacionInicial);
                intent.putExtra("EsCalibracionEstacionFinal",EsCalibracionEstacionFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
            }else if(EsCalibracionPipaInicial || EsCalibracionPipaFinal){
                calibracionDTO.setP5000(CantidadP500);
                intent.putExtra("EsCalibracionPipaInicial",EsCalibracionPipaInicial);
                intent.putExtra("EsCalibracionPipaFinal",EsCalibracionPipaFinal);
                intent.putExtra("calibracionDTO",calibracionDTO);
            }
            startActivity(intent);
        }
    }
}
