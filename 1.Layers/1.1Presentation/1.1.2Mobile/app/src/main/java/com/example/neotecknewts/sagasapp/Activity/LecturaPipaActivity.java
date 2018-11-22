package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class LecturaPipaActivity extends AppCompatActivity implements View.OnClickListener,
        LecturaPipaView {

    public Button BtnLecturaPipaActivityGuardar;
    public TextView TVLecturaPipaActivityTitulo,TVLecturaPipaAcivitySeleccionPipa,
            TVLecturaPipaActivityTipoLector;
    public Spinner SLecturaPipaActivityListaPipa,SLecturaPipaActivityListaTipoMedidor;

    ProgressDialog progressDialog;
    String []ListaMedidores,ListaPipas;
    List<MedidorDTO> MedidorDTOList;
    DatosTomaLecturaDto DatosTomaLecturaDto;
    LecturaPipaPresenter lecturaPipaPresenter;
    Session session;
    LecturaPipaDTO lecturaPipaDTO;
    Boolean EsLecturaInicialPipa,EsLecturaFinalPipa,EsFinal;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_pipa);
        Bundle b = getIntent().getExtras();
        if(b!=null) {
            EsLecturaInicialPipa = (boolean)b.get("EsLecturaInicialPipa");
            EsLecturaFinalPipa = (boolean) b.get("EsLecturaFinalPipa");
            EsFinal = b.getBoolean("EsLecturaFinalPipa",false);
        }
        TVLecturaPipaActivityTitulo = findViewById(R.id.TVLecturaPipaActivityTitulo);
        if(EsLecturaInicialPipa){
            TVLecturaPipaActivityTitulo.setText(R.string.toma_de_lectura_inicial);
        }else if (EsLecturaFinalPipa){
            TVLecturaPipaActivityTitulo.setText(R.string.toma_de_lectura_final);
        }
        TVLecturaPipaAcivitySeleccionPipa = findViewById(R.id.TVLecturaPipaAcivitySeleccionPipa);
        TVLecturaPipaActivityTipoLector = findViewById(R.id.TVLecturaPipaActivityTipoLector);

        SLecturaPipaActivityListaPipa = findViewById(R.id.SLecturaPipaActivityListaPipa);
        SLecturaPipaActivityListaTipoMedidor =
                findViewById(R.id.SLecturaPipaActivityListaTipoMedidor);

        BtnLecturaPipaActivityGuardar = findViewById(R.id.BtnLecturaPipaActivityGuardar);

        BtnLecturaPipaActivityGuardar.setOnClickListener(this);

        lecturaPipaDTO = new LecturaPipaDTO();
        lecturaPipaPresenter = new LecturaPipaPresenterImpl(this);
        session = new Session(LecturaPipaActivity.this);
        /*ListaPipas = new String[]{"Seleccióne","Pipa 1","Pipa 2"};
        ListaMedidores = new String[]{"Seleccióne","Magnatel","Rotogate"};
        SLecturaPipaActivityListaTipoMedidor.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,ListaMedidores));
        SLecturaPipaActivityListaPipa.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,ListaPipas));*/
        //lecturaPipaPresenter.getMedidores(session.getToken());
        lecturaPipaPresenter.getPipas(session.getToken(),EsFinal);

        SLecturaPipaActivityListaPipa.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0) {
                    for (AlmacenDTO almacenDTO :DatosTomaLecturaDto.getAlmacenes()){
                        if (almacenDTO.getNombreAlmacen().equals(parent.getItemAtPosition(position).toString())) {
                            lecturaPipaDTO.setIdPipa(almacenDTO.getIdAlmacenGas());
                            lecturaPipaDTO.setNombrePipa(almacenDTO.getNombreAlmacen());
                            lecturaPipaDTO.setPorcentajeMedidor(almacenDTO.getPorcentajeMedidor());
                            lecturaPipaDTO.setCantidadP5000(almacenDTO.getCantidadP5000());
                        }
                    }
                    /*for (String m : ListaPipas) {
                        if (m.equals(parent.getItemAtPosition(position).toString())) {
                            lecturaPipaDTO.setNombrePipa(m);
                            lecturaPipaDTO.setIdPipa(1);
                        }
                    }*/
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaPipaDTO.setIdPipa(0);
                lecturaPipaDTO.setNombrePipa("");
                lecturaPipaDTO.setPorcentajeMedidor(0.0);
                lecturaPipaDTO.setCantidadP5000(0);
            }
        });

        SLecturaPipaActivityListaTipoMedidor.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (position>=0){
                    for (MedidorDTO medidor: MedidorDTOList){
                        if(medidor.getNombreTipoMedidor().equals(parent.getItemAtPosition(position)
                                .toString())){
                            lecturaPipaDTO.setTipoMedidor(medidor.getNombreTipoMedidor());
                            lecturaPipaDTO.setIdTipoMedidor(medidor.getIdTipoMedidor());
                            lecturaPipaDTO.setCantidadFotografias(medidor.getCantidadFotografias());
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaPipaDTO.setIdTipoMedidor(0);
                lecturaPipaDTO.setTipoMedidor("");
                lecturaPipaDTO.setCantidadFotografias(0);
            }
        });

    }

    @Override
    public void onClick(View v) {
        if(v.getId()==R.id.BtnLecturaPipaActivityGuardar){
            ValidateForm();
        }
    }

    @Override
    public void ValidateForm() {
        ArrayList<String> mensajes = new ArrayList<>();
        boolean error = false;
        if(SLecturaPipaActivityListaPipa.getSelectedItemPosition()<0){
            mensajes.add("La pipa es un valor requerido");
            error = true;
        }
        if(SLecturaPipaActivityListaTipoMedidor.getSelectedItemPosition()<0){
            mensajes.add("El tipo de medidor es un valor requerido");
            error = true;
        }
        if(!error) {
            DialogoRetornar();
        }else{
            DialogoCamposVacios(mensajes);
        }
    }

    @Override
    public void DialogoCamposVacios(ArrayList<String> mensaje) {
        AlertDialog.Builder dialogo = new AlertDialog.Builder(LecturaPipaActivity.this,
                R.style.AlertDialog);
        dialogo.setTitle(R.string.error_titulo);
        StringBuilder mensaje_dialogo = new StringBuilder(getString(R.string.mensjae_error_campos));
        for (String texto: mensaje
             ) {
            mensaje_dialogo.append("\n").append(texto);
        }
        dialogo.setMessage(mensaje_dialogo);
        dialogo.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            SLecturaPipaActivityListaPipa.setFocusable(true);
            dialog.dismiss();
        });
        dialogo.create().show();
    }


    @Override
    public void DialogoRetornar() {
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaPipaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setNegativeButton(R.string.message_cancel, (dialog, which) -> dialog.dismiss());
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            Intent intent = new Intent(LecturaPipaActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("lecturaPipaDTO",lecturaPipaDTO);
            intent.putExtra("EsLecturaInicial",false);
            intent.putExtra("EsLecturaFinal",false);
            intent.putExtra("EsLecturaInicialPipa",EsLecturaInicialPipa);
            intent.putExtra("EsLecturaFinalPipa",EsLecturaFinalPipa);
            startActivity(intent);
        });
        builder.create().show();
    }

    @Override
    public void showLoadingProgress(int message_cargando) {
        progressDialog = new ProgressDialog(LecturaPipaActivity.this);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void hiddeLoadingProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }

    @Override
    public void onSuccessMedidores(List<MedidorDTO> data) {
        //ListaMedidores = new String[data.size()+1];
        ListaMedidores = new String[data.size()];
        MedidorDTOList = new ArrayList<>();
        MedidorDTOList = data;
        //ListaMedidores[0]= "Seleccione";
        for (int x =0; x< data.size();x++){
            //ListaMedidores[x+1] = data.get(x).getNombreTipoMedidor();
            ListaMedidores[x] = data.get(x).getNombreTipoMedidor();
        }
        SLecturaPipaActivityListaTipoMedidor.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,ListaMedidores));
        hiddeLoadingProgress();
    }

    @Override
    public void ErrorMedidores() {
        progressDialog = new ProgressDialog(LecturaPipaActivity.this);
        progressDialog.setMessage(getString(R.string.error_conexion));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onError() {
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaPipaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.titulo_error_papeleta);
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
               dialog.dismiss();
            }
        });
        builder.create().show();
    }

    @Override
    public void onSuccessPipas(DatosTomaLecturaDto data) {
        //ListaPipas = new String[data.getAlmacenes().size()+1];
        ListaPipas = new String[data.getAlmacenes().size()];
        DatosTomaLecturaDto = data;
        //ListaPipas[0]= "Seleccione";
        for (int x =0; x< data.getAlmacenes().size();x++){
            //ListaPipas[x+1] = data.getAlmacenes().get(x).getNombreAlmacen();
            ListaPipas[x] = data.getAlmacenes().get(x).getNombreAlmacen();
        }
        SLecturaPipaActivityListaPipa.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,ListaPipas));
        hiddeLoadingProgress();
    }
}
