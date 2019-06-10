package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
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
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaCamionetaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class LecturaCamionetaActivity extends AppCompatActivity implements LecturaCamionetaView{
    public boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta,error,esFinal;
    public TextView TVLecturaCamionetaActivityTitulo,TVLecturaCamionetaActivotyRecordatorioUno,
            TVLecturaCamionetaActivityRecordatorioDos,TVLecturaCamionetaAcitvityQuien;
    public Spinner SLecturaCamionetaActivityListaCamioneta,SLecturaCamionetaActivityListaQuien;
    public Button BtnLecturaCamionetaActivityAceptar;
    public String[] list_camionetas,list_quien;
    public LecturaCamionetaDTO lecturaCamionetaDTO;
    public LecturaCamionetaPresenter lecturaCamionetaPresenter;
    public ProgressDialog progressDialog;
    public Session session;
    DatosTomaLecturaDto DatosTomaLecturaDto;
    public List<CilindrosDTO> cilindrosDTOS;
    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_camioneta);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsLecturaInicialCamioneta = (boolean) bundle.get("EsLecturaInicialCamioneta");
            EsLecturaFinalCamioneta = (boolean) bundle.get("EsLecturaFinalCamioneta");
            esFinal = bundle.getBoolean("EsLecturaInicialCamioneta",false);
        }
        session = new Session(LecturaCamionetaActivity.this);
        TVLecturaCamionetaActivityTitulo = findViewById(R.id.TVLecturaCamionetaActivityTitulo);
        TVLecturaCamionetaAcitvityQuien = findViewById(R.id.TVLecturaCamionetaAcitvityQuien);
        TVLecturaCamionetaActivotyRecordatorioUno = findViewById(
                R.id.TVLecturaCamionetaActivotyRecordatorioUno);
        TVLecturaCamionetaActivityRecordatorioDos = findViewById(
                R.id.TVLecturaCamionetaActivityRecordatorioDos);
        SLecturaCamionetaActivityListaCamioneta = findViewById(
                R.id.SLecturaCamionetaActivityListaCamioneta);
        SLecturaCamionetaActivityListaQuien = findViewById(R.id.SLecturaCamionetaActivityListaQuien);
        BtnLecturaCamionetaActivityAceptar = findViewById(R.id.BtnLecturaCamionetaActivityAceptar);
        lecturaCamionetaDTO = new LecturaCamionetaDTO();
        if (EsLecturaInicialCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" inicial");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.GONE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.GONE);
            lecturaCamionetaDTO.setEsEncargadoPuerta(false);
        }else if(EsLecturaFinalCamioneta){
            TVLecturaCamionetaActivityTitulo.setText(TVLecturaCamionetaActivityTitulo.getText()
                    +" final");
            TVLecturaCamionetaAcitvityQuien.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.VISIBLE);
            TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
            SLecturaCamionetaActivityListaQuien.setVisibility(View.VISIBLE);
        }
        lecturaCamionetaPresenter = new LecturaCamionetaPresenterImpl(this);
        BtnLecturaCamionetaActivityAceptar.setOnClickListener(v -> verificarForm());
        /*list_camionetas = new String[]{"Seleccione","Camioneta Ford","Camioneta Chevy"};*/
        list_quien = new String[]{"Seleccione","Encargado de Puerta","Encargado del Andén"};
        /*SLecturaCamionetaActivityListaCamioneta.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,list_camionetas));*/
        SLecturaCamionetaActivityListaQuien.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,list_quien));

        SLecturaCamionetaActivityListaCamioneta.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0 && DatosTomaLecturaDto.getAlmacenes()!=null) {
                 for (AlmacenDTO almacenDTO:DatosTomaLecturaDto.getAlmacenes()) {
                     if(almacenDTO.getNombreAlmacen().equals(parent.getItemAtPosition(position).toString())) {
                         lecturaCamionetaDTO.setIdCamioneta(almacenDTO.getIdAlmacenGas());
                         lecturaCamionetaDTO.setNombreCamioneta(SLecturaCamionetaActivityListaCamioneta
                                 .getItemAtPosition(position).toString());
                         cilindrosDTOS = almacenDTO.getCilindros();
                     }
                 }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaCamionetaDTO.setIdCamioneta(0);
                lecturaCamionetaDTO.setNombreCamioneta("");
            }
        });

        SLecturaCamionetaActivityListaQuien.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (SLecturaCamionetaActivityListaQuien.
                        getItemAtPosition(position).toString().equals("Encargado de Puerta")){
                    TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.VISIBLE);
                    TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.GONE);
                    lecturaCamionetaDTO.setEsEncargadoPuerta(true);
                }else{
                    TVLecturaCamionetaActivotyRecordatorioUno.setVisibility(View.GONE);
                    TVLecturaCamionetaActivityRecordatorioDos.setVisibility(View.VISIBLE);
                    lecturaCamionetaDTO.setEsEncargadoPuerta(false);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaCamionetaDTO.setEsEncargadoPuerta(true);
            }
        });
        lecturaCamionetaPresenter.GetListCamionetas(session.getToken(),EsLecturaFinalCamioneta);
    }
    @Override
    public void verificarForm(){
        List<String> mensajes_error = new ArrayList<>();
        if(SLecturaCamionetaActivityListaCamioneta.getSelectedItemPosition()<=0) {
                mensajes_error.add("La camioneta es un valor requerido");
        }
        if(EsLecturaFinalCamioneta){
            if(SLecturaCamionetaActivityListaQuien.getSelectedItemPosition()<=0){
                mensajes_error.add("Es necesario especificar quien eres");
            }
        }
        if(error)
            mensajeError(mensajes_error);
        else{
            dialogoRetornar();
        }
    }

    @Override
    public void onSuccessCamionetas(DatosTomaLecturaDto data) {
        this.DatosTomaLecturaDto = new DatosTomaLecturaDto();
        this.DatosTomaLecturaDto = data;
        if(this.DatosTomaLecturaDto!=null && !this.DatosTomaLecturaDto.getAlmacenes().isEmpty()) {
            //list_camionetas = new String[DatosTomaLecturaDto.getAlmacenes().size()+1];
            list_camionetas = new String[this.DatosTomaLecturaDto.getAlmacenes().size()];
            //list_camionetas[0] = "Seleccióne";
            for (int x = 0; x < this.DatosTomaLecturaDto.getAlmacenes().size(); x++) {
                //list_camionetas[x+1] = DatosTomaLecturaDto.getAlmacenes().get(x).getNombreAlmacen();
                list_camionetas[x] = this.DatosTomaLecturaDto.getAlmacenes().get(x).getNombreAlmacen();
            }
            SLecturaCamionetaActivityListaCamioneta.setAdapter(new ArrayAdapter<>(this,
                    R.layout.custom_spinner, list_camionetas));
        }else{
            AlertDialog.Builder builder= new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(R.string.title_alert_message);
            builder.setMessage("Ha ocurrido un error al consultar las camionetas");
            builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
            builder.create().show();
        }
    }

    @Override
    public void onErrorCamionetas() {
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaCamionetaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(getString(R.string.error_titulo));
        builder.setMessage(R.string.error_conexion);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }
    @Override
    public void mensajeError(List<String> mensaje_error){
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaCamionetaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(getString(R.string.error_titulo));
        StringBuilder mensaje = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for(String men : mensaje_error){
            mensaje .append(men).append("\n");
        }
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }

    @Override
    public void dialogoRetornar(){
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaCamionetaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_goback_diabled);
        builder.setPositiveButton(R.string.message_acept,(dialog,which)->{
            Intent intent = new Intent(LecturaCamionetaActivity.this,
                    ConfiguracionCamionetaActivity.class);
            intent.putExtra("EsLecturaInicialCamioneta",EsLecturaInicialCamioneta);
            intent.putExtra("EsLecturaFinalCamioneta",EsLecturaFinalCamioneta);
            intent.putExtra("lecturaCamionetaDTO",lecturaCamionetaDTO);
            intent.putExtra("cilindrosDTOS",(Serializable) cilindrosDTOS);
            dialog.dismiss();
            startActivity(intent);
        });
        builder.setNegativeButton(R.string.message_cancel,((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }

    @Override
    public void onShowProgressDialog(int message_cargando) {
        progressDialog = new ProgressDialog(LecturaCamionetaActivity.this);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setCancelable(false);
        progressDialog.setTitle(R.string.project_id);
        progressDialog.show();
    }

    @Override
    public void hideProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }
}
