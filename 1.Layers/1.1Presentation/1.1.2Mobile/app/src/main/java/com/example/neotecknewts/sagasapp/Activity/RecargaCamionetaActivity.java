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
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RecargaCamionetaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class RecargaCamionetaActivity extends AppCompatActivity implements RecargaCamionetaView{
    public TextView TVRecargaCamionetaActivityTitulo;
    public Spinner SRecargaCamionetaActivityListaCamionetas;
    public Button BRecargaCamionetaActivityGuardar;
    public boolean EsRecargaCamioneta;
    public RecargaCamionetaPresenterImpl presenter;
    public ProgressDialog dialog;
    public Session session;
    public DatosTomaLecturaDto DatosTomaLecturaDto;
    public EstacionCarburacionDTO estacionCarburacionDTO;
    public String[] list_camionetas;
    public RecargaDTO recargaDTO;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recarga_camioneta);
        Bundle bundle = getIntent().getExtras();
        if (bundle!=null){
            EsRecargaCamioneta = bundle.getBoolean("EsRecargaCamioneta",false);
        }
        TVRecargaCamionetaActivityTitulo = findViewById(R.id.TVRecargaCamionetaActivityTitulo);
        SRecargaCamionetaActivityListaCamionetas = findViewById(R.id.
                SRecargaCamionetaActivityListaCamionetas);
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
                for (AlmacenDTO estacion: DatosTomaLecturaDto.getAlmacenes()){
                    if(estacion.getNombreAlmacen().
                            equals(parent.getItemAtPosition(position).toString())){
                        recargaDTO.setIdCAlmacenGasSalida(estacion.getIdAlmacenGas());
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasEntrada(0);
            }
        });
    }
    @Override
    public void ValidarForm(){
        if(SRecargaCamionetaActivityListaCamionetas.getSelectedItemPosition()<=0){
            AlertDialog.Builder builder = new AlertDialog.Builder(
                    RecargaCamionetaActivity.this);
            builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
            builder.setTitle(R.string.error_titulo);
            builder.setMessage("Debes selecciónar una camioenta, ya que es un valor requerido");
        }else{
            GoBackWindow();
        }
    }

    @Override
    public void GoBackWindow() {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaCamionetaActivity.this);
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
        dialog = new ProgressDialog(RecargaCamionetaActivity.this);
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
    public void onSuccessCamionetas(DatosTomaLecturaDto data) {
        DatosTomaLecturaDto = data;
        list_camionetas = new String[data.getAlmacenes().size()+1];
        list_camionetas[0] = "Seleccione";
        for (int x=1;x<data.getAlmacenes().size();x++){
            list_camionetas[x] = data.getAlmacenes().get(x).getNombreAlmacen();
        }
        SRecargaCamionetaActivityListaCamionetas.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,list_camionetas));
    }

    @Override
    public void onError() {
        AlertDialog.Builder builder = new AlertDialog.Builder(
                RecargaCamionetaActivity.this);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.error_conexion);
        builder.setNegativeButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }
}
