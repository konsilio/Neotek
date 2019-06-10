package com.example.neotecknewts.sagasapp.Activity;

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
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaAlmacenPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaAlmacenPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class LecturaAlmacenActivity extends AppCompatActivity implements LecturaAlmacenView {
    public TextView TVLecturaAlmacenActivityTitulo;
    public Spinner SLecturaAlmacenActivityListaAlmacen,SLecturaAlmacenActivityListaMedidor;
    public Button BtnLecturaAlamacenActivityGuardar;
    public String[] lista_medidores,lista_almacenes;

    public boolean EsLecturaInicialAlmacen,EsLecturaFinalAlmacen,error;
    public List<MedidorDTO> medidorDTOList;
    public ArrayList<String> mensajes_error;
    public LecturaAlmacenDTO lecturaAlmacenDTO;
    public Session session;
    public LecturaAlmacenPresenter lecturaAlmacenPresenter;
    public ProgressDialog progressDialog;
    public DatosTomaLecturaDto DatosTomaLecturaDtoList;
    public boolean banlist;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_almacen);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsLecturaInicialAlmacen = (boolean)bundle.get("EsLecturaInicialAlmacen");
            EsLecturaFinalAlmacen = (boolean) bundle.get("EsLecturaFinalAlmacen");
            banlist = bundle.getBoolean("EsLecturaFinalAlmacen", false);
        }
        session = new Session(LecturaAlmacenActivity.this);
        lecturaAlmacenDTO = new LecturaAlmacenDTO();
        lecturaAlmacenPresenter = new LecturaAlmacenPresenterImpl(this);
        TVLecturaAlmacenActivityTitulo = findViewById(R.id.TVLecturaAlmacenActivityTitulo);
        if(EsLecturaInicialAlmacen){
            TVLecturaAlmacenActivityTitulo.setText(R.string.toma_de_lectura_inicial);
        }else if (EsLecturaFinalAlmacen){
            TVLecturaAlmacenActivityTitulo.setText(R.string.toma_de_lectura_final);
        }
        SLecturaAlmacenActivityListaMedidor = findViewById(R.id.SLecturaAlmacenActivityListaMedidor);
        SLecturaAlmacenActivityListaAlmacen = findViewById(R.id.SLecturaAlmacenActivityListaAlmacen);

        lista_medidores = new String[]{"Seleccióne"};
        /*SLecturaAlmacenActivityListaMedidor.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,lista_medidores));*/
        lista_almacenes = new String[]{"Seleccióne","Almacen 1","Almacen 2"};
        /*lecturaAlmacenPresenter.getMedidores(session.getToken());*/

       /* SLecturaAlmacenActivityListaAlmacen.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,lista_almacenes));*/
        SLecturaAlmacenActivityListaMedidor.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0){
                    lecturaAlmacenDTO.setNombreTipoMedidor(
                            SLecturaAlmacenActivityListaMedidor.getItemAtPosition(position).
                                    toString()
                    );
                    if(medidorDTOList.size()>0 && !medidorDTOList.isEmpty() && medidorDTOList!=null) {
                        for (MedidorDTO medidor : medidorDTOList) {
                            if (medidor.getNombreTipoMedidor().equals(
                                    SLecturaAlmacenActivityListaMedidor.getItemAtPosition(position).
                                            toString())) {
                                lecturaAlmacenDTO.setIdTipoMedior(medidor.getIdTipoMedidor());
                                lecturaAlmacenDTO.setCantidadFotografias(medidor.getCantidadFotografias());
                                lecturaAlmacenDTO.setNombreTipoMedidor(medidor.getNombreTipoMedidor());
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaAlmacenDTO.setIdTipoMedior(0);
                lecturaAlmacenDTO.setNombreTipoMedidor("");
                lecturaAlmacenDTO.setCantidadFotografias(0);

            }
        });
        SLecturaAlmacenActivityListaAlmacen.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (position>=0){
                    if(DatosTomaLecturaDtoList!=null && DatosTomaLecturaDtoList.getAlmacenes().size()>0) {
                        for (AlmacenDTO almacenDTO : DatosTomaLecturaDtoList.getAlmacenes()) {
                            if (almacenDTO.getNombreAlmacen().equals(parent.getItemAtPosition(position).toString())) {
                                lecturaAlmacenDTO.setIdAlmacen(almacenDTO.getIdAlmacenGas());
                                lecturaAlmacenDTO.setNombreAlmacen(almacenDTO.getNombreAlmacen());
                                lecturaAlmacenDTO.setPorcentajeMedidor(almacenDTO.getPorcentajeMedidor());
                                lecturaAlmacenDTO.setCapacidadAlmacen(almacenDTO.getCapacidad());
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaAlmacenDTO.setIdAlmacen(0);
                lecturaAlmacenDTO.setNombreAlmacen("");
            }
        });
        lecturaAlmacenPresenter.getAlmacenes(session.getToken(),banlist);
        BtnLecturaAlamacenActivityGuardar = findViewById(R.id.BtnLecturaAlamacenActivityGuardar);
        BtnLecturaAlamacenActivityGuardar.setOnClickListener(v -> {
            VerificarErrores();
        });

    }

    @Override
    public void VerificarErrores() {
        error = false;
        mensajes_error = new ArrayList<>();
        if(SLecturaAlmacenActivityListaAlmacen.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("El almacén principal es un valor requerido ");
        }
        if(SLecturaAlmacenActivityListaMedidor.getSelectedItemPosition()<0){
            error = true;
            mensajes_error.add("El medidor prorcentual es un valor requerido");
        }
        if(error)
            DialogoError(mensajes_error);
        else
            DialogoRetroceder();
    }

    @Override
    public void onSuccessMedidores(List<MedidorDTO> data) {
        medidorDTOList = data;
        //lista_medidores = new String[data.size()+1];
        lista_medidores = new String[data.size()];
        //lista_medidores[0] = "Seleccióne";
        for (int x=0;x<data.size();x++){
            //lista_medidores[x+1]=data.get(x).getNombreTipoMedidor();
            lista_medidores[x]=data.get(x).getNombreTipoMedidor();
        }
        SLecturaAlmacenActivityListaMedidor.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,lista_medidores));
    }

    @Override
    public void onSuccessAlmacenes(DatosTomaLecturaDto data) {
        DatosTomaLecturaDtoList = data;
        //lista_almacenes = new String[data.getAlmacenes().size()+1];
        lista_almacenes = new String[data.getAlmacenes().size()];
        //lista_almacenes[0]= "Seleccióne";
        for (int x = 0; x<data.getAlmacenes().size();x++){
            //lista_almacenes[x+1] = data.getAlmacenes().get(x).getNombreAlmacen();
            lista_almacenes[x] = data.getAlmacenes().get(x).getNombreAlmacen();
        }
        SLecturaAlmacenActivityListaAlmacen.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,lista_almacenes));
    }

    @Override
    public void onError() {
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaAlmacenActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(R.string.error_conexion);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create();
        builder.show();
    }

    @Override
    public void DialogoRetroceder() {
        AlertDialog.Builder alert = new AlertDialog.Builder(LecturaAlmacenActivity.this,
                R.style.AlertDialog);
        alert.setTitle(R.string.title_alert_message);
        alert.setMessage(R.string.message_continuar);
        alert.setPositiveButton(R.string.message_acept, (dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(LecturaAlmacenActivity.this,
                    CapturaPorcentajeActivity.class);
            intent.putExtra("EsLecturaInicialAlmacen", EsLecturaInicialAlmacen);
            intent.putExtra("EsLecturaFinalAlmacen", EsLecturaFinalAlmacen);
            intent.putExtra("lecturaAlmacenDTO",lecturaAlmacenDTO);
            startActivity(intent);

        });
        alert.setNegativeButton(R.string.message_cancel, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        alert.create();
        alert.show();
    }

    @Override
    public void DialogoError(ArrayList<String> mensajes_error) {
        AlertDialog.Builder builder = new AlertDialog.Builder(LecturaAlmacenActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.app_name);
        builder.setMessage(R.string.error_titulo);
        StringBuilder dialogo = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for (String mens : mensajes_error){
            dialogo.append("\n").append(mens);
        }
        builder.setMessage(dialogo);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }

    @Override
    public void showProgress(int message_cargando) {
        progressDialog = new ProgressDialog(LecturaAlmacenActivity.this);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setTitle(R.string.project_id);
        progressDialog.show();
    }

    @Override
    public void hiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }
}
