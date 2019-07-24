package com.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
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

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoEstacionPresenter;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoEstacionPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class AutoconsumoEstacionActivity extends AppCompatActivity implements
        AutoconsumoEstacionView {
    TextView TVAutoconsumoEstacionActivityTitulo,TVAutoconsumoEstacionActivityEstacionSalida,
            TVAutoconsumoEstacionActivityUnidad;
    Spinner SAutoconsumoEstacionActivityListaEstaciones,SAutoconsumoEstacionActivityListaUnidades;
    Button BtnAutoconsumoActtivityGuardar;
    ProgressDialog progressDialog;

    boolean EsAutoconsumoEstacionInicial, EsAutoconsumoEstacionFinal;
    AutoconsumoDTO autoconsumoDTO;
    DatosAutoconsumoDTO dto_resp;
    Session session;
    String[] lista_estaciones,lista_pipa;
    AutoconsumoEstacionPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_autoconsumo_estacion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsAutoconsumoEstacionInicial = bundle.getBoolean("EsAutoconsumoEstacionInicial",
                    false);
            EsAutoconsumoEstacionFinal = bundle.getBoolean("EsAutoconsumoEstacionFinal",
                    false);
        }

        autoconsumoDTO = new AutoconsumoDTO();
        session = new Session(getApplicationContext());
        presenter = new AutoconsumoEstacionPresenterImpl(this);

        TVAutoconsumoEstacionActivityTitulo = findViewById(R.id.TVAutoconsumoEstacionActivityTitulo);
        TVAutoconsumoEstacionActivityTitulo.setText(
                (EsAutoconsumoEstacionInicial)? getString(R.string.carburaci_n_de_gas) + " - Inicial":
                        getString(R.string.carburaci_n_de_gas) + " - Final"
        );
        TVAutoconsumoEstacionActivityEstacionSalida = findViewById(R.
                id.TVAutoconsumoEstacionActivityEstacionSalida);
        TVAutoconsumoEstacionActivityUnidad = findViewById(R.id.TVAutoconsumoEstacionActivityUnidad);
        SAutoconsumoEstacionActivityListaEstaciones = findViewById(R.id.
                SAutoconsumoEstacionActivityListaEstaciones);
        SAutoconsumoEstacionActivityListaUnidades = findViewById(R.id.
                SAutoconsumoEstacionActivityListaUnidades);
        BtnAutoconsumoActtivityGuardar = findViewById(R.id.BtnAutoconsumoActtivityGuardar);

        /*lista_estaciones = new String[]{"Estacion 1","Estacion 2"};
        lista_pipa = new String[]{"Pipa 1","Pipa 2"};



        SAutoconsumoEstacionActivityListaEstaciones.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_estaciones
        ));

        SAutoconsumoEstacionActivityListaUnidades.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_pipa
        ));*/

        presenter.getList(session.getToken(),EsAutoconsumoEstacionFinal);

        SAutoconsumoEstacionActivityListaEstaciones.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0) {
                    if (dto_resp != null && dto_resp.getEstacionSalidaDTOList().size() > 0 &&
                            !dto_resp.getEstacionEntradaDTOList().isEmpty()) {
                        for (int x = 0; x < dto_resp.getEstacionSalidaDTOList().size(); x++) {
                            if(parent.getItemAtPosition(position).toString().equals(
                              dto_resp.getEstacionSalidaDTOList().get(x).getNombreAlmacen()
                            )) {
                                autoconsumoDTO.setIdCAlmacenGasSalida(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getIdAlmacenGas()
                                );
                                autoconsumoDTO.setIdTipoMedidor(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getIdTipoMedidor()
                                );
                                autoconsumoDTO.setP5000Salida(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getCantidadP5000()
                                );
                                autoconsumoDTO.setNombreTipoMedidor(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getMedidor()
                                                .getNombreTipoMedidor()
                                );
                                autoconsumoDTO.setPorcentajeMedidor(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getPorcentajeMedidor()
                                );
                                autoconsumoDTO.setCantidadFotos(dto_resp.getEstacionSalidaDTOList()
                                        .get(x).getMedidor().getCantidadFotografias());
                                autoconsumoDTO.setNombreEstacion(
                                        dto_resp.getEstacionSalidaDTOList().get(x).getNombreAlmacen()
                                );
                            }
                        }
                    }
                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                autoconsumoDTO.setIdCAlmacenGasSalida(0);
                autoconsumoDTO.setIdCAlmacenGasSalida(0);
                autoconsumoDTO.setIdTipoMedidor(0);
                autoconsumoDTO.setP5000Salida(0);
                autoconsumoDTO.setNombreTipoMedidor("");
            }
        });

        SAutoconsumoEstacionActivityListaUnidades.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0 && dto_resp!=null){
                    if(!dto_resp.getEstacionEntradaDTOList().isEmpty() && dto_resp.getEstacionEntradaDTOList().size()>0){
                        for (int x =0;x< dto_resp.getEstacionEntradaDTOList().size();x++){
                            if(parent.getItemAtPosition(position).toString().equals(
                                    dto_resp.getEstacionEntradaDTOList().get(x).getNombreAlmacen()
                            )){
                                autoconsumoDTO.setIdCAlmacenGasEntrada(
                                        dto_resp.getEstacionEntradaDTOList().get(x).getIdAlmacenGas()
                                );
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                autoconsumoDTO.setIdCAlmacenGasEntrada(0);
            }
        });

        BtnAutoconsumoActtivityGuardar.setOnClickListener(v -> verificarErrores());

    }

    @Override
    public void onSuccessLista(DatosAutoconsumoDTO dto) {
        if(dto!= null) {
            dto_resp = dto;
            if(dto.getEstacionEntradaDTOList().size()>0 && dto.getEstacionEntradaDTOList()!=null) {
                lista_estaciones = new String[dto.getEstacionEntradaDTOList().size()];
                for (int x = 0; x < dto.getEstacionEntradaDTOList().size(); x++) {
                    lista_estaciones[x] = dto.getEstacionEntradaDTOList().get(x).getNombreAlmacen();
                }
                SAutoconsumoEstacionActivityListaUnidades.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_estaciones
                ));
            }
            if(dto.getEstacionSalidaDTOList().size()>0 && dto.getEstacionSalidaDTOList()!=null){
                lista_pipa = new String[dto.getEstacionSalidaDTOList().size()];
                for (int x = 0; x < dto.getEstacionSalidaDTOList().size(); x++) {
                    lista_pipa[x] = dto.getEstacionSalidaDTOList().get(x).getNombreAlmacen();
                }

                SAutoconsumoEstacionActivityListaEstaciones
                        .setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_pipa
                ));
            }
            if(dto.getPredeterminadaDTO()!=null){
                for (int x= 0;x<dto.getEstacionEntradaDTOList().size();x++){
                    if(dto.getEstacionEntradaDTOList().get(x).getIdAlmacenGas()==
                            dto.getPredeterminadaDTO().getIdAlmacenGas()){
                        SAutoconsumoEstacionActivityListaUnidades.setSelection(x);
                    }
                }
            }
        }

    }

    @Override
    public void onErrorLista(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void verificarErrores() {
        ArrayList<String> mensajes = new ArrayList<>();
        boolean error = false;

        if(SAutoconsumoEstacionActivityListaEstaciones.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La estacion de salida es un valor requerido");
        }
        if(SAutoconsumoEstacionActivityListaUnidades.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La unidad de entrada en un valor requerido");
        }
        if(error){
            mostrarError(mensajes);
        }else{
            Intent intent = new Intent(AutoconsumoEstacionActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsAutoconsumoEstacionInicial",EsAutoconsumoEstacionInicial);
            intent.putExtra("EsAutoconsumoEstacionFinal",EsAutoconsumoEstacionFinal);
            intent.putExtra("autoconsumoDTO",autoconsumoDTO);
            startActivity(intent);
        }
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setIndeterminate(true);
        progressDialog.setTitle(getString(R.string.app_name));
        progressDialog.setMessage(getString(mensaje));
        //progressDialog.show();
    }

    @Override
    public void onHiddenProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }

    @Override
    public void mostrarError(ArrayList<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String men: mensajes){
            stringBuilder.append(men).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }
}
