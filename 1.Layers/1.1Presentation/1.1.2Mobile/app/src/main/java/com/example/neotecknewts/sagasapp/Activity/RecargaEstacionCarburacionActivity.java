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

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RecargaEstacionCarburacionPresenter;
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
    RecargaEstacionCarburacionPresenter presenter;
    String[] lista_pipas_salida,lista_meididores,lista_estacion;
    DatosRecargaDto dto;

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
                if(dto!=null) {
                    for (int x=0;x<dto.getPipasDTOS().size();x++) {
                        if(dto.getPipasDTOS().get(x).getNombreAlmacen().equals(
                                parent.getItemAtPosition(position).toString()
                        )) {
                            recargaDTO.setIdCAlmacenGasSalida(
                                    dto.getPipasDTOS().get(x).getIdAlmacenGas()
                            );

                            recargaDTO.setProcentajeSalida(
                                    dto.getPipasDTOS().get(x).getPorcentajeMedidor()
                            );

                            recargaDTO.setP5000Salida(
                                    dto.getPipasDTOS().get(x).getCantidadP5000()
                            );

                            recargaDTO.setNombreEstacionSalida(
                                    dto.getPipasDTOS().get(x).getNombreAlmacen()
                            );

                            for (int y=0;y<dto.getMedidorDTOS().size();y++){
                                if(dto.getMedidorDTOS().get(y).getIdTipoMedidor()==
                                        dto.getPipasDTOS().get(x).getIdTipoMedidor()
                                        ){
                                    SRecargaEstacionCarburacionActivityListaMedidorPipa
                                            .setSelection(y);
                                }
                            }
                        }
                    }
                }
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
                if(dto!=null) {
                    for (int x=0;x<dto.getMedidorDTOS().size();x++) {
                        if(dto.getMedidorDTOS().get(x).getNombreTipoMedidor().equals(
                                parent.getItemAtPosition(position).toString()
                        )) {
                            recargaDTO.setIdTipoMedidorSalida(
                                    dto.getMedidorDTOS().get(x).getIdTipoMedidor()
                            );
                            recargaDTO.setNombreMedidorSalida(
                                    dto.getMedidorDTOS().get(x).getNombreTipoMedidor()
                            );
                            recargaDTO.setCantidadFotosSalida(
                                    dto.getMedidorDTOS().get(x).getCantidadFotografias()
                            );
                        }
                    }
                }

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
                if(dto!=null) {
                    for (int x=0;x<dto.getEstacionesDTOS().size();x++) {
                        if(dto.getEstacionesDTOS().get(x).getNombreAlmacen().equals(
                                parent.getItemAtPosition(position).toString()
                        )) {
                            recargaDTO.setIdCAlmacenGasEntrada(
                                    dto.getEstacionesDTOS().get(x).getIdAlmacenGas()
                            );
                            recargaDTO.setP5000Entrada(
                                    dto.getEstacionesDTOS().get(x).getCantidadP5000()
                            );

                            recargaDTO.setProcentajeEntrada(
                                    dto.getEstacionesDTOS().get(x).getPorcentajeMedidor()
                            );
                            recargaDTO.setNombreEstacionEntrada(
                                    dto.getEstacionesDTOS().get(x).getNombreAlmacen()
                            );
                            for (int y=0;y<dto.getMedidorDTOS().size();y++){
                                if(dto.getMedidorDTOS().get(y).getNombreTipoMedidor().equals(
                                        dto.getEstacionesDTOS().get(x).getMedidor()
                                                .getNombreTipoMedidor()
                                )
                                        ){
                                    SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino
                                            .setSelection(y);
                                }
                            }

                        }
                    }
                }
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
                if(dto!=null) {
                    for (int x=0;x<dto.getMedidorDTOS().size();x++) {
                        if(dto.getMedidorDTOS().get(x).getNombreTipoMedidor().equals(
                                parent.getItemAtPosition(position).toString()
                        )) {
                            recargaDTO.setIdTipoMedidorEntrada(
                                    dto.getMedidorDTOS().get(x).getIdTipoMedidor()
                            );
                            recargaDTO.setNombreMedidorEntrada(
                                    dto.getMedidorDTOS().get(x).getNombreTipoMedidor()
                            );
                            recargaDTO.setCantidadFotosEntrada(
                                    dto.getMedidorDTOS().get(x).getCantidadFotografias()
                            );
                        }
                    }
                }
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
                RecargaEstacionCarburacionActivity.this,R.style.AlertDialog);
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
                RecargaEstacionCarburacionActivity.this,R.style.AlertDialog);
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
    public void onSuccessLista(DatosRecargaDto datosRecargasDTO) {
        if(datosRecargasDTO!=null) {
            dto = datosRecargasDTO;
            if(datosRecargasDTO.getMedidorDTOS().size()>0){
                lista_meididores = new String[datosRecargasDTO.getMedidorDTOS().size()];
                for (int x =0; x<datosRecargasDTO.getMedidorDTOS().size();x++) {
                    lista_meididores[x] =datosRecargasDTO.getMedidorDTOS().get(x)
                            .getNombreTipoMedidor();
                }
                SRecargaEstacionCarburacionActivityListaMedidorPipa.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_meididores
                ));
                SRecargaEstacionCarburacionActivityListaMedidorPorcentualDestino.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_meididores
                        )
                );
            }
            if(datosRecargasDTO.getEstacionesDTOS().size()>0){
                lista_estacion = new String[datosRecargasDTO.getEstacionesDTOS().size()];
                for (int x =0; x<datosRecargasDTO.getEstacionesDTOS().size();x++){
                    lista_estacion[x]=datosRecargasDTO.getEstacionesDTOS().get(x).getNombreAlmacen();
                }
                SRecargaEstacionCarburacionActivityListaEstacion.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_estacion
                        )
                );
            }

            if(datosRecargasDTO.getPipasDTOS().size()>0){
                lista_pipas_salida = new String[datosRecargasDTO.getPipasDTOS().size()];
                for (int x=0;x<datosRecargasDTO.getPipasDTOS().size();x++){
                    lista_pipas_salida[x] = datosRecargasDTO.getPipasDTOS().get(x).getNombreAlmacen();
                }
                SRecargaEstacionCarburacionActivityListaPipa.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_pipas_salida
                        )
                );
            }
        }

    }
}
