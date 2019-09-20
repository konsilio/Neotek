package com.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Adapter.EstacionesAdatper;
import com.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;
import com.neotecknewts.sagasapp.Presenter.AnticipoEstacionCarburacionPresenter;
import com.neotecknewts.sagasapp.Presenter.AnticipoEstacionCarburacionPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class AnticipoEstacionCarburacionActivity extends AppCompatActivity implements
        AnticipoEstacionCarburacionView {
    RecyclerView RVAnticipoEstacionesCarburacionActivityContainer;
    AnticipoEstacionCarburacionPresenter presenter;
    Session session;
    ProgressDialog progressDialog;
    List<DatosEstacionesDTO> list;

    private boolean EsAnticipo, EsCorte;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_estacion_carburacion);
        Bundle bundle = getIntent().getExtras();

        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            Log.d("bundleanticipo", "si entra");
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

        setTitle((EsCorte)? getString(R.string.corte_de_caja): getString(R.string.Anticipo));
        if(EsCorte){
            presenter.checkLecturas(session.getToken());
        }
        //presenter.checkLecturas(session.getToken());

    }

    private List<DatosEstacionesDTO> getLista() {
        //Coloco el Header;
        DatosEstacionesDTO header = new DatosEstacionesDTO();
        header.setNombreCAlmacen(getString(R.string.selecciona_la_estacion_de_carburacion));

        list.add(0,header);

        return  list;
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this,R.style.AlertDialog);
        progressDialog.setTitle(R.string.app_name);
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
            list = new ArrayList<>();
            if(data.getEstaciones()!=null) {
                list.add(
                        data.getEstaciones()
                );
            }

            if(data.getCamionetaDTO()!=null) {
                DatosEstacionesDTO dto =new DatosEstacionesDTO();
                dto.setIdCAlmacenGas(data.getCamionetaDTO().getIdCAlmacen());
                dto.setNombreCAlmacen(data.getCamionetaDTO().getNumero());
                dto.setP5000Inicial(data.getCamionetaDTO().getCantidadP5000());
                dto.setP5000Final(data.getCamionetaDTO().getP5000Final());
                dto.setAnticiposEstacion(data.getCamionetaDTO().getAnticiposEstacionDTO());
                list.add(
                    dto
                );
            }
            if(data.getPipaDTO()!=null){
                DatosEstacionesDTO dto = new DatosEstacionesDTO();
                dto.setIdCAlmacenGas(data.getPipaDTO().getIdAlmacenGas());
                dto.setNombreCAlmacen(data.getPipaDTO().getNombreAlmacen());
                dto.setP5000Inicial(data.getPipaDTO().getCantidadP5000());
                dto.setP5000Final(data.getPipaDTO().getP5000Final());
                dto.setAnticiposEstacion(data.getPipaDTO().getAnticiposEstacionDTO());
                list.add(
                        dto
                );
            }
            DatosEstacionesDTO header = new DatosEstacionesDTO();
            Log.d("anticipoestacionesdatos", "entro");
            header.setNombreCAlmacen(getString(R.string.selecciona_la_estacion_de_carburacion));
            list.add(0,header);
            EstacionesAdatper adatper = new EstacionesAdatper(this,list,
                    EsAnticipo,
                    EsCorte
            );
            adatper.EsCamioneta = data.isEsCamioneta();
            adatper.EsEstacion = data.isEsEstacion();
            adatper.EsPipa = data.isEsPipa();
            RVAnticipoEstacionesCarburacionActivityContainer.setAdapter(adatper);
        }
    }

    @Override
    public void onSuccessRespuestaLecturas(RespuestaVerificarLecturasDTO data) {
        if(!data.isExito()){
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(R.string.info);
            builder.setMessage(data.getMensaje());
            builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
                dialog.dismiss();
                finish();
            });
            builder.setCancelable(false);
            builder.create().show();
        }
    }

    @Override
    public void onErrorVerificarLecturas(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            dialog.dismiss();
            finish();
        });
        builder.setCancelable(false);
        builder.create().show();
    }
}
