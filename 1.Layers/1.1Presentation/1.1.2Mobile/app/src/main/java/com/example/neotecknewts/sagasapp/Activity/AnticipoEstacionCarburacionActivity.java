package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;

import com.example.neotecknewts.sagasapp.Adapter.EstacionesAdatper;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoEstacionCarburacionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoEstacionCarburacionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class AnticipoEstacionCarburacionActivity extends AppCompatActivity implements
        AnticipoEstacionCarburacionView{
    RecyclerView RVAnticipoEstacionesCarburacionActivityContainer;
    AnticipoEstacionCarburacionPresenter presenter;
    Session session;
    ProgressDialog progressDialog;
    List<DatosEstacionesDTO> list;

    private boolean EsAnticipo,EsCorte;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_estacion_carburacion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);
        }
        RVAnticipoEstacionesCarburacionActivityContainer = findViewById(R.id.
                RVAnticipoEstacionesCarburacionActivityContainer);
        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(
                AnticipoEstacionCarburacionActivity.this);
        RVAnticipoEstacionesCarburacionActivityContainer.setLayoutManager(linearLayoutManager);
        RVAnticipoEstacionesCarburacionActivityContainer.setHasFixedSize(true);
        session = new Session(this);
        presenter = new AnticipoEstacionCarburacionPresenterImpl(this);
        presenter.getEstaciones(session.getToken());
        setTitle((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));

    }

    private List<DatosEstacionesDTO> getLista() {
        //Coloco el Header;
        DatosEstacionesDTO header = new DatosEstacionesDTO();
        header.setNombreCAlmacen(getString(R.string.selecciona_la_estacion_de_carburacion));

        /*if(list ==null){

            //Items
            for (int x = 0; x < 20; x++){
                DatosEstacionesDTO item = new DatosEstacionesDTO();
                item.setNombreCAlmacen("Estación Bulevar "+String.valueOf(x));
                item.setIdCAlmacenGas(x);
                list.add(item);
            }
        }*/
        list.add(0,header);

        return  list;
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.project_id);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setCancelable(false);
        progressDialog.show();
    }

    @Override
    public void onHiddeProgress() {
        if(progressDialog!=null && progressDialog.isShowing()) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onError(String error) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(error);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) ->
                dialogInterface.dismiss());
        builder.create().show();
    }

    @Override
    public void onError(RespuestaEstacionesVentaDTO data) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(data.getMensajesError());
        builder.setPositiveButton(R.string.message_acept,((dialogInterface, i) ->
            dialogInterface.dismiss()
        ));
        builder.create().show();
    }

    @Override
    public void onSuccess(RespuestaEstacionesVentaDTO data) {

        if(data!=null){
            list = data.getEstaciones();
            DatosEstacionesDTO header = new DatosEstacionesDTO();
            header.setNombreCAlmacen(getString(R.string.selecciona_la_estacion_de_carburacion));
            list.add(0,header);
            EstacionesAdatper adatper = new EstacionesAdatper(this,list,
                    EsAnticipo,
                    EsCorte
            );

            RVAnticipoEstacionesCarburacionActivityContainer.setAdapter(adatper);
        }
    }
}
