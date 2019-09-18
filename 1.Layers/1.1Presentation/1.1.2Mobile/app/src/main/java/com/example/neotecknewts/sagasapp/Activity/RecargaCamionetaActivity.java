package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RecargaCamionetaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RecargaCamionetaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class RecargaCamionetaActivity extends AppCompatActivity implements RecargaCamionetaView{
    public TextView TVRecargaCamionetaActivityTitulo;
    public Spinner SRecargaCamionetaActivityListaCamionetas,SRecargaCamionetaActivityEstacion;
    public Button BRecargaCamionetaActivityGuardar;
    public boolean EsRecargaCamioneta;
    public RecargaCamionetaPresenter presenter;
    public ProgressDialog dialog;
    public Session session;
    public DatosRecargaDto DatosTomaLecturaDto;
    public EstacionCarburacionDTO estacionCarburacionDTO;
    public String[] list_camionetas,list_estaciones;
    public RecargaDTO recargaDTO;
    public CilindrosDTO CilindrosDTO;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recarga_camioneta);
        Bundle bundle = getIntent().getExtras();
        if (bundle!=null){
            EsRecargaCamioneta = bundle.getBoolean("EsRecargaCamioneta",false);
        }
        TVRecargaCamionetaActivityTitulo = findViewById(R.id.TVRecargaCamionetaActivityTitulo);
        SRecargaCamionetaActivityListaCamionetas =findViewById(R.id.SRecargaCamionetaActivityListaCamionetas);
        SRecargaCamionetaActivityEstacion = findViewById(R.id.SRecargaCamionetaActivityEstacion);
        BRecargaCamionetaActivityGuardar = findViewById(R.id.BRecargaCamionetaActivityGuardar);

        BRecargaCamionetaActivityGuardar.setOnClickListener(v->{
            ValidarForm();
        });
        recargaDTO = new RecargaDTO();
        session = new Session(this);
        presenter = new RecargaCamionetaPresenterImpl(this);
        presenter.getCamionetas(session.getToken());
        SRecargaCamionetaActivityListaCamionetas.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(DatosTomaLecturaDto!=null){
                    if(DatosTomaLecturaDto.getCamionetasDTOS().size()>0){
                        for (int x=0;x<DatosTomaLecturaDto.getCamionetasDTOS().size();x++){
                            if(parent.getItemAtPosition(position).toString().equals(
                                    DatosTomaLecturaDto.getCamionetasDTOS().get(x).getNombreAlmacen()
                            )){
                                recargaDTO.setIdCAlmacenGasEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getIdAlmacenGas()
                                );
                                recargaDTO.setIdTipoMedidorEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                                .getIdTipoMedidor()
                                );
                                recargaDTO.setP5000Entrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getCantidadP5000()
                                );
                                recargaDTO.setNombreEstacionEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getNombreAlmacen()
                                );
                                recargaDTO.setNombreMedidorEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getMedidor().getNombreTipoMedidor()
                                );
                                recargaDTO.setCantidadFotosEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getMedidor().getCantidadFotografias()
                                );
                                recargaDTO.setProcentajeEntrada(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                                .getPorcentajeMedidor()
                                );
                                recargaDTO.setCilindros(
                                        DatosTomaLecturaDto.getCamionetasDTOS().get(x)
                                        .getCilindros()
                                );
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasEntrada(0);
                recargaDTO.setIdTipoMedidorEntrada(0);
                recargaDTO.setP5000Entrada(0);
                recargaDTO.setNombreEstacionEntrada("");
                recargaDTO.setNombreMedidorEntrada("");
                recargaDTO.setCantidadFotosEntrada(0);
                recargaDTO.setProcentajeEntrada(0);
            }
        });
        SRecargaCamionetaActivityEstacion.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(DatosTomaLecturaDto!=null){
                    for (int x=0;x<DatosTomaLecturaDto.getAlmacenesAlternosDTOS().size();x++){
                        if(parent.getItemAtPosition(position).toString().equals(
                                DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x).getNombreAlmacen()
                        )) {
                            recargaDTO.setIdCAlmacenGasSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getIdAlmacenGas()
                            );
                            recargaDTO.setIdTipoMedidorSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getIdTipoMedidor()
                            );
                            recargaDTO.setP5000Salida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getCantidadP5000()
                            );
                            recargaDTO.setNombreEstacionSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getNombreAlmacen()
                            );
                            recargaDTO.setNombreMedidorSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getMedidor().getNombreTipoMedidor()
                            );
                            recargaDTO.setCantidadFotosSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getMedidor().getCantidadFotografias()
                            );
                            recargaDTO.setProcentajeSalida(
                                    DatosTomaLecturaDto.getAlmacenesAlternosDTOS().get(x)
                                    .getPorcentajeMedidor()
                            );

                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasSalida(0);
                recargaDTO.setIdTipoMedidorSalida(0);
                recargaDTO.setP5000Salida(0);
                recargaDTO.setNombreEstacionSalida("");
                recargaDTO.setNombreMedidorSalida("");
                recargaDTO.setCantidadFotosSalida(0);
                recargaDTO.setProcentajeSalida(0);
            }
        });
    }
    @Override
    public void ValidarForm(){
        if(SRecargaCamionetaActivityListaCamionetas.getSelectedItemPosition()<0){
            AlertDialog.Builder builder = new AlertDialog.Builder(
                    RecargaCamionetaActivity.this,R.style.AlertDialog);
            builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
            builder.setTitle(R.string.error_titulo);
            builder.setMessage("Debes selecciónar una camioenta, ya que es un valor requerido");
            builder.create().show();
        }else{
            GoBackWindow();
        }
    }

    @Override
    public void GoBackWindow() {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaCamionetaActivity.this,R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_goback_diabled);
        builder.setNegativeButton(R.string.message_cancel, (dialog, which) -> dialog.dismiss());
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            Intent intent = new Intent(RecargaCamionetaActivity.this,
                    ConfiguracionCamionetaActivity.class);
            intent.putExtra("EsRecargaCamioneta",EsRecargaCamioneta);
            intent.putExtra("EsLecturaInicialCamioneta",false);
            intent.putExtra("EsLecturaFinalCamioneta",false);
            intent.putExtra("recargaDTO",recargaDTO);
            startActivity(intent);
        }));
        builder.create().show();
    }

    @Override
    public void showProgres() {
        dialog = new ProgressDialog(this,R.style.AlertDialog);
        dialog.setIndeterminate(true);
        dialog.setTitle(R.string.project_id);
        dialog.setMessage(getString(R.string.message_cargando));
        dialog.show();
    }

    @Override
    public void hideProgress() {
        if(dialog!=null && dialog.isShowing()){
            dialog.hide();
        }
    }

    @Override
    public void onSuccessCamionetas(DatosRecargaDto data) {
        DatosTomaLecturaDto = data;
        if(data!=null) {
            if (data.getCamionetasDTOS().size() > 0) {
                list_camionetas = new String[data.getCamionetasDTOS().size()];
                for (int x = 0; x < data.getCamionetasDTOS().size(); x++) {
                    list_camionetas[x] = data.getCamionetasDTOS().get(x).getNombreAlmacen();
                }
                SRecargaCamionetaActivityListaCamionetas.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        list_camionetas
                ));
            }
            if(data.getAlmacenesAlternosDTOS().size()>0){
                list_estaciones = new String[data.getAlmacenesAlternosDTOS().size()];
                for (int x=0;x<data.getAlmacenesAlternosDTOS().size();x++){
                    list_estaciones[x] = data.getAlmacenesAlternosDTOS().get(x)
                    .getNombreAlmacen();
                }
                SRecargaCamionetaActivityEstacion.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                list_estaciones
                        )
                );
            }
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaCamionetaActivity.this,R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(getString(R.string.error_conexion)+"\n"+mensaje);
        builder.setNegativeButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }
}
