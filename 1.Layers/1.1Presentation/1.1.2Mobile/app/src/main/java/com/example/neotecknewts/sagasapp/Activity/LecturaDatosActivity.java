package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LecturaDatosPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class LecturaDatosActivity extends AppCompatActivity implements View.OnClickListener ,
        LecturaDatosView{
    public TextView TVLecturaDatosActivityTitulo;
    public TextView TVLecturaDatosEstacion;
    public TextView TVLecturaDatosActivityTipoMedidor;

    public Spinner SLecturaDatosActivityListaCarburacion;
    public Spinner SLecturaDatosActivityTipoMedidor;

    public Button BLecturaDatosActivityAceptar;

    public boolean EsLecturaInicial, EsLecturaFinal,esFinalizar;
    public ProgressDialog dialog_progres;

    public String[]ListaCarburacion,ListaTipoMedidor;

    public LecturaDatosPresenter lecturaDatosPresenter;
    public Session session;
    public List<MedidorDTO> listaDTO;

    public LecturaDTO lecturaDTO;
    public DatosTomaLecturaDto DatosTomaLecturaDto;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lectura_datos);

        TVLecturaDatosActivityTitulo = findViewById(R.id.TVLecturaDatosactivityTitulo);
        TVLecturaDatosEstacion = findViewById(R.id.TVLecturaDatosEstacion);
        TVLecturaDatosActivityTipoMedidor = findViewById(R.id.TVLecturaDatosActivityTipoMedidor);

        SLecturaDatosActivityListaCarburacion = findViewById(
                R.id.SLecturaDatosActivityListaCarburacion);
        SLecturaDatosActivityTipoMedidor = findViewById(R.id.SLecturaDatosActivityTipoMedidor);

        BLecturaDatosActivityAceptar = findViewById(R.id.BLecturaDatosActivityAceptar);

        BLecturaDatosActivityAceptar.setOnClickListener(this);

        lecturaDatosPresenter = new LecturaDatosPresenterImpl(this);

        session = new Session(getApplicationContext());

        Bundle b = getIntent().getExtras();

        if (b != null) {
            EsLecturaInicial = (boolean) b.get("EsLecturaInicial");
            EsLecturaFinal = (boolean) b.get("EsLecturaFinal");
        }else{
            EsLecturaInicial = true;
            EsLecturaFinal = false;
        }
        esFinalizar = b.getBoolean("EsLecturaInicial",false);
        lecturaDTO = new LecturaDTO();

        TVLecturaDatosActivityTitulo.setText(
                EsLecturaInicial ? getString(R.string.toma_de_lectura_inicial):
                        getString(R.string.toma_de_lectura_final));

        ListaCarburacion = new String[]{"Seleccione","Tipo 1","Tipo 2"};
        ListaTipoMedidor = getResources().getStringArray(R.array.tipos_medidor);
        /*SLecturaDatosActivityListaCarburacion.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner, ListaCarburacion ));*/
        SLecturaDatosActivityTipoMedidor.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner, ListaTipoMedidor));

        /*lecturaDatosPresenter.getMedidores(session.getToken());*/

        SLecturaDatosActivityTipoMedidor.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>0){
                    for(MedidorDTO medidor:listaDTO){
                        String item = SLecturaDatosActivityTipoMedidor
                                .getItemAtPosition(position).toString();
                        if(medidor.getNombreTipoMedidor().equals(item)) {
                            Log.w("id", String.valueOf(medidor.getIdTipoMedidor()));
                            Log.w("meidor", medidor.getNombreTipoMedidor());
                            Log.w("cantidad", String.valueOf(medidor.getCantidadFotografias()));
                            lecturaDTO.setIdTipoMedidor(medidor.getIdTipoMedidor());
                            lecturaDTO.setCantidadFotografias(medidor.getCantidadFotografias());
                            lecturaDTO.setNombreTipoMedidor(medidor.getNombreTipoMedidor());
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                Log.v("Posicion",String.valueOf(parent.getSelectedItemPosition()));
                lecturaDTO.setIdTipoMedidor(0);
                lecturaDTO.setCantidadFotografias(0);
                lecturaDTO.setNombreTipoMedidor("");
            }
        });
        lecturaDatosPresenter.getEstacionesCarburacion(session.getToken(),esFinalizar);
        SLecturaDatosActivityListaCarburacion.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                for (AlmacenDTO estacion:DatosTomaLecturaDto.getAlmacenes()){
                    if(estacion.getNombreAlmacen().equals(parent.getItemAtPosition(position).toString())){
                        lecturaDTO.setNombreEstacionCarburacion(estacion.getNombreAlmacen());
                        lecturaDTO.setIdEstacionCarburacion(estacion.getIdAlmacenGas());
                    }
                }
                /*if(parent.getItemAtPosition(position).toString().equals("Tipo 1")){
                    lecturaDTO.setNombreEstacionCarburacion(parent.getItemAtPosition(position)
                            .toString());
                    lecturaDTO.setIdEstacionCarburacion(1);
                }else{
                    lecturaDTO.setNombreEstacionCarburacion(parent.getItemAtPosition(position)
                            .toString());
                    lecturaDTO.setIdEstacionCarburacion(1);
                }*/
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                lecturaDTO.setNombreEstacionCarburacion("");
                lecturaDTO.setIdEstacionCarburacion(0);
            }
        });

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()){
            case R.id.BLecturaDatosActivityAceptar:
                ValidateForm();
                break;
        }
    }

    @Override
    public void ValidateForm() {
        boolean error = false;
        ArrayList<String> mensaje = new ArrayList<String>();
        if(SLecturaDatosActivityListaCarburacion.getSelectedItemPosition()==0) {
            error = true;
            mensaje.add("La estación de carburación es un campo requerido");
        }
        if(SLecturaDatosActivityTipoMedidor.getSelectedItemPosition()==0) {
            error = true;
            mensaje.add("El tipo de medidos es un campo requerido");
        }
        if (error)
            DialogoCamposVacios(mensaje);
        else {
            DialogoNoRetornar();
        }
    }

    @Override
    public void DialogoCamposVacios(ArrayList<String> mensaje) {

        StringBuilder dialogo = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for (String mens : mensaje){
            dialogo.append("\n").append(mens);
        }
        AlertDialog.Builder alert = CrearAlerta(R.string.error_titulo,dialogo.toString());

        alert.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                SLecturaDatosActivityListaCarburacion.setFocusable(true);
            }
        });
        alert.create();
        alert.show();
    }

    @Override
    public void DialogoNoRetornar() {
        AlertDialog.Builder alert = CrearAlerta(
                R.string.title_alert_message,getString(R.string.message_continuar));
        alert.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                    Intent intent = new Intent(LecturaDatosActivity.this,
                            LecturaP5000Activity.class);
                    intent.putExtra("EsLecturaInicial", EsLecturaInicial);
                    intent.putExtra("EsLecturaFinal", EsLecturaFinal);
                    intent.putExtra("EsLecturaInicialPipa",false);
                    intent.putExtra("EsLecturaFinalPipa",false);
                    intent.putExtra("lecturaDTO",lecturaDTO);
                    startActivity(intent);

            }
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
    public void showLoadingProgress(int message_cargando) {
        dialog_progres= new ProgressDialog(this);
        dialog_progres.setMessage(getString(message_cargando));
        dialog_progres.setIndeterminate(true);
        dialog_progres.show();
    }

    @Override
    public void hiddeLoadingProgress() {
        if(dialog_progres!= null && dialog_progres.isShowing()) {
            dialog_progres.hide();
            dialog_progres.dismiss();
        }
    }

    @Override
    public void onSuccessMedidores(List<MedidorDTO> data) {
        listaDTO = data;
        ListaTipoMedidor = new String[data.size()+1];
        ListaTipoMedidor[0] = "Seleccione";
        for (int x = 0;x<data.size();x++){
            ListaTipoMedidor[x+1]=data.get(x).getNombreTipoMedidor();
        }
        SLecturaDatosActivityTipoMedidor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ListaTipoMedidor ));

    }

    @Override
    public void ErrorMedidores() {
        AlertDialog.Builder builder = CrearAlerta(R.string.error_titulo,"Ha ocurrido un error se ha perdido la conexion, intente nuevamente.");
        builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        //builder.create();
        builder.show();
    }

    @Override
    public void onSuccessEstacionesCarburacion(DatosTomaLecturaDto data) {
        DatosTomaLecturaDto = data;
        ListaCarburacion = new String[DatosTomaLecturaDto.getAlmacenes().size()+1];
        ListaCarburacion[0] = "Seleccione";
        for (int x = 0;x<data.getAlmacenes().size();x++){
            ListaCarburacion[x+1]=data.getAlmacenes().get(x).getNombreAlmacen();
        }
        SLecturaDatosActivityListaCarburacion.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner,ListaCarburacion));
    }

    private AlertDialog.Builder CrearAlerta(int titulo,String mensaje){
        AlertDialog.Builder alert = new AlertDialog.Builder(this);
        alert.setTitle(titulo);
        alert.setMessage(mensaje);
        return alert;
    }
}
