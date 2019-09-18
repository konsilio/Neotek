package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Session;

public class EnviarDartosActivity extends AppCompatActivity  implements EnviarDatosView{
    public boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta,EsRecargaCamioneta;
    public LecturaCamionetaDTO lecturaCamionetaDTO;
    public ProgressDialog progressDialog;
    public EnviarDatosPresenter enviarDatosPresenter;
    public Session session;
    public SAGASSql sagasSql;
    public RecargaDTO recargaDTO;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_enviar_datos);
        Bundle bundle =  getIntent().getExtras();
        if(bundle!= null){
            EsLecturaInicialCamioneta = (boolean) bundle.get("EsLecturaInicialCamioneta");
            EsLecturaFinalCamioneta = (boolean) bundle.get("EsLecturaFinalCamioneta");
            EsRecargaCamioneta = bundle.getBoolean("EsRecargaCamioneta",false);
            lecturaCamionetaDTO = (LecturaCamionetaDTO) bundle.
                    getSerializable("lecturaCamionetaDTO");
            recargaDTO = (RecargaDTO) bundle.getSerializable("recargaDTO");
        }
        enviarDatosPresenter = new EnviarDatosPresenterImpl(this);
        sagasSql = new SAGASSql(EnviarDartosActivity.this);
        session = new Session(EnviarDartosActivity.this);
        new HiloEnviar().execute();
    }

    @Override
    public void onSuccessEnvio() {
        AlertDialog.Builder builder = new AlertDialog.Builder(EnviarDartosActivity.this,R.style.AlertDialog);
        builder.setTitle(getString(R.string.titulo_exito_registro_papeleta));
        builder.setMessage(getString(R.string.mensaje_exito_papeleta_registro_en_servicio));
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            Intent intent = new Intent(EnviarDartosActivity.this, MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
            finish();
            dialog.dismiss();
        });
        builder.create();
        builder.show();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(EnviarDartosActivity.this,R.style.AlertDialog);
        builder.setTitle(getString(R.string.error_titulo));
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            Intent intent = new Intent(EnviarDartosActivity.this, MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
            finish();
            dialog.dismiss();
        });
        builder.create();
        builder.show();
    }

    @Override
    public void onSuccessAndroid() {
        AlertDialog.Builder builder = new AlertDialog.Builder(EnviarDartosActivity.this,R.style.AlertDialog);
        builder.setTitle(getString(R.string.titulo_exito_registro_papeleta_android));
        builder.setMessage(getString(R.string.mensaje_exito_papeleta_android));
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            Intent intent = new Intent(EnviarDartosActivity.this, MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
            finish();
            dialog.dismiss();
        });
        builder.create();
        builder.show();
    }

    @Override
    public void showProgressDialog() {
        progressDialog = new ProgressDialog(EnviarDartosActivity.this);
        progressDialog.setTitle(R.string.message_cargando);
        progressDialog.setMessage(getString(R.string.message_cargando));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void hiddenProgressDialog() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            //progressDialog.dismiss();
        }
    }

    public class HiloEnviar extends AsyncTask<Void,Integer,Void>{

        @Override
        protected Void doInBackground(Void... voids) {
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);
            if(EsLecturaInicialCamioneta) {
                enviarDatosPresenter.RegistrarLecturaInicialCamioneta(sagasSql,session.getToken(),
                        lecturaCamionetaDTO);
            }else  if (EsLecturaFinalCamioneta){
                enviarDatosPresenter.RegistrarLecturaFinalCamioneta(sagasSql,session.getToken(),
                        lecturaCamionetaDTO);
            }if (EsRecargaCamioneta){
                enviarDatosPresenter.RegistrarRecargaCamioneta(recargaDTO,session.getToken(),
                        sagasSql);
            }
        }
    }
}
