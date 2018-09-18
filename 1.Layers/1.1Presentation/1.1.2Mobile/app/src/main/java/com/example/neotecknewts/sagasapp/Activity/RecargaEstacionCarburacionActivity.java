package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RecargaEstacionCarburacionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class RecargaEstacionCarburacionActivity extends AppCompatActivity
        implements RecargaEstacionCarburacionView{
    TextView TVRecargaEstacionCarburacionActivityTitulo;
    TextView TVRecargaEstacionCarburacionActivityPipaSalida;
    Spinner SRecargaEstacionCarburacionActivityListaPipa;
    TextView TVRecargaEstacionCarburacion;
    Spinner SRecargaEstacionCarburacionActivityListaMedidorPipa;
    TextView TVRecargaEstacionCarburacionAcitvityEstacionDestino;
    Spinner SRecargaEstacionCarburacionActivityListaEstacion;
    TextView TVRecargaEstacionCarburacionActivityMedidorEstacion;
    Spinner SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino;
    Button BtnRecargaEstacionCarburacionActivityAceptar;

    ArrayList<String> mensajes_error;
    RecargaDTO recargaDTO;
    boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal;
    Session session;
    ProgressDialog progressDialog;
    RecargaEstacionCarburacionPresenterImpl presenter;
    String[] lista_pipas_salida,lista_meididores,lista_estacion;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recarga_estacion_carburacion);

        Bundle bundle =  getIntent().getExtras();
        if(bundle!=null){
            EsRecargaEstacionFinal = bundle.getBoolean("EsRecargaEstacionFinal",
                    false);
            EsRecargaEstacionInicial = bundle.getBoolean("EsRecargaEstacionInicial",
                    false);
        }
        recargaDTO = new RecargaDTO();
        session = new Session(RecargaEstacionCarburacionActivity.this);
        presenter = new RecargaEstacionCarburacionPresenterImpl(this);

        TVRecargaEstacionCarburacionActivityTitulo = findViewById(R.id.
                TVRecargaEstacionCarburacionActivityTitulo);
        TVRecargaEstacionCarburacionActivityPipaSalida = findViewById(R.id.
                TVRecargaEstacionCarburacionActivityPipaSalida);
        SRecargaEstacionCarburacionActivityListaPipa = findViewById(R.id.
                SRecargaEstacionCarburacionActivityListaPipa);
        TVRecargaEstacionCarburacion = findViewById(R.id.TVRecargaEstacionCarburacion);
        SRecargaEstacionCarburacionActivityListaMedidorPipa = findViewById(R.id.
                SRecargaEstacionCarburacionActivityListaMedidorPipa);
        TVRecargaEstacionCarburacionAcitvityEstacionDestino = findViewById(R.id.
                TVRecargaEstacionCarburacionAcitvityEstacionDestino);
        SRecargaEstacionCarburacionActivityListaEstacion = findViewById(R.id.
                SRecargaEstacionCarburacionActivityListaEstacion);
        TVRecargaEstacionCarburacionActivityMedidorEstacion = findViewById(R.id.
                TVRecargaEstacionCarburacionActivityMedidorEstacion);
        SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino = findViewById(R.id.
                SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino);
        BtnRecargaEstacionCarburacionActivityAceptar = findViewById(R.id.
                BtnRecargaEstacionCarburacionActivityAceptar);

        lista_meididores= new String[]{"Rotogate","Magnatel"};
        lista_estacion = new String[]{"Estacion 1","Estacion 2"};
        lista_pipas_salida = new String[]{"Pipa 1","Pipa 2"};

        SRecargaEstacionCarburacionActivityListaPipa.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_pipas_salida
        ));
        SRecargaEstacionCarburacionActivityListaMedidorPipa.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_meididores
        ));
        SRecargaEstacionCarburacionActivityListaEstacion.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_estacion
        ));
        SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_meididores
        ));


        presenter.getLista(session.getToken());
        SRecargaEstacionCarburacionActivityListaPipa.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                recargaDTO.setIdCAlmacenGasSalida(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasSalida(0);
            }
        });
        SRecargaEstacionCarburacionActivityListaMedidorPipa.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                recargaDTO.setIdTipoMedidorSalida(1);
                recargaDTO.setNombreMedidorSalida("Magnatel");
                recargaDTO.setCantidadFotosSalida(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdTipoMedidorSalida(0);
                recargaDTO.setNombreMedidorSalida("");
                recargaDTO.setCantidadFotosSalida(0);
            }
        });
        SRecargaEstacionCarburacionActivityListaEstacion.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                recargaDTO.setIdCAlmacenGasEntrada(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasEntrada(0);
            }
        });
        SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                recargaDTO.setIdTipoMedidorEntrada(1);
                recargaDTO.setNombreMedidorEntrada("Magnatel");
                recargaDTO.setCantidadFotosEntrada(1);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdTipoMedidorEntrada(0);
                recargaDTO.setNombreMedidorEntrada("");
                recargaDTO.setCantidadFotosEntrada(0);
            }
        });

        BtnRecargaEstacionCarburacionActivityAceptar.setOnClickListener(V->{
            VerificarFormulario();
        });
    }

    @Override
    public void VerificarFormulario() {
        mensajes_error = new ArrayList<>();
        boolean error= false;
        if(SRecargaEstacionCarburacionActivityListaPipa.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("Es necesario selecionar la pipa de salida");
        }
        if (SRecargaEstacionCarburacionActivityListaMedidorPipa.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("Es necesario seleccionar el medior de la pipa");
        }
        if(SRecargaEstacionCarburacionActivityListaEstacion.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("Es necesario seleccionar el medidor de la estacion  ");
        }
        if(SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("Es necesario seleccionar el medidor de porcentual de la estacion");
        }

        if(error){
            MostrarErrores(mensajes_error );
        }else{
            EnviarDatos();
        }
    }

    @Override
    public void EnviarDatos() {
        Intent intent = new Intent(RecargaEstacionCarburacionActivity.this,
                LecturaP5000Activity.class);
        intent.putExtra("EsRecargaEstacionInicial",EsRecargaEstacionInicial);
        intent.putExtra("EsRecargaEstacionFinal",EsRecargaEstacionFinal);
        intent.putExtra("recargaDTO",recargaDTO);
        intent.putExtra("EsPrimeraLectura",true);
        intent.putExtra("EsLecturaInicial",false);
        intent.putExtra("EsLecturaFinal",false);
        intent.putExtra("EsLecturaFinalPipa",false);
        intent.putExtra("EsLecturaInicialPipa",false);

        startActivity(intent);
    }

    @Override
    public void MostrarErrores(ArrayList<String> mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaEstacionCarburacionActivity.this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String men:mensaje){
            stringBuilder.append(men).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_cancel, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                SRecargaEstacionCarburacionActivityListaPipa.setFocusable(true);
            }
        });
        builder.create();
        builder.show();
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(RecargaEstacionCarburacionActivity.this);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.show();
    }

    @Override
    public void onHiddenProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaEstacionCarburacionActivity.this);
        builder.setTitle(R.string.error_titulo);

        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_cancel, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                SRecargaEstacionCarburacionActivityListaPipa.setFocusable(true);
            }
        });
        builder.create();
        builder.show();
    }

    @Override
    public void onSuccessLista(Object datosRecargasDTO) {
        lista_meididores= new String[2];
        lista_estacion = new String[3];
        lista_pipas_salida = new String[3];

    }
}
