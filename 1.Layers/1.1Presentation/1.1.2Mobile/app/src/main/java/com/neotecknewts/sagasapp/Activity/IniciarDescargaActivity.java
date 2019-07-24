package com.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.neotecknewts.sagasapp.Presenter.IniciarDescargaPresenter;
import com.neotecknewts.sagasapp.Presenter.IniciarDescargaPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.List;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class IniciarDescargaActivity extends AppCompatActivity implements IniciarDescargaView {

    //Variables relacionadas con la vista
    public Spinner spinnerOrdenCompra;
    public Switch switchTanquePrestado;
    public Spinner spinnerMedidorAlmacen;
    public Spinner spinnerMedidorTractor;
    public Spinner spinnerAlmacenes;
    public TextView TvIniciarDescargaActivityNo,TvIniciarDescargaActivitySi;
    //cuadro de dialogo con el progreso de la obtencion de datos
    ProgressDialog progressDialog;

    //clase de la session
    public Session session;

    //lista de objetos a mostrar en los spinners
    List<OrdenCompraDTO> ordenesCompraDTO;
    List<MedidorDTO> medidorDTOs;
    List<AlmacenDTO> almacenDTOs;

    //presentador(interactua entre la vista y el interactor (clase que hace los llamados a web Api / web service)
    public IniciarDescargaPresenter presenter;

    //objeto que se llenara con los datos del activity
    public IniciarDescargaDTO iniciarDescargaDTO;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_iniciar_descarga);

        iniciarDescargaDTO = new IniciarDescargaDTO();

        //se incializan las variables de la vista
        spinnerOrdenCompra = (Spinner)findViewById(R.id.spinner_orden_compra);
        switchTanquePrestado = (Switch)findViewById(R.id.switch_tanque);
        spinnerMedidorAlmacen = (Spinner) findViewById(R.id.spinner_medidor_almacen);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);
        spinnerAlmacenes = (Spinner)findViewById(R.id.spinner_almacen);
        /* Obtengo los text view del switch (si, no)*/
        TvIniciarDescargaActivitySi = findViewById(R.id.TvIniciarDescargaActivitySi);
        TvIniciarDescargaActivityNo = findViewById(R.id.TvIniciarDescargaActivityNo);

        //se incializa la clase de la session
        session = new Session(getApplicationContext());

        //se inicializa el presenter
        presenter = new IniciarDescargaPresenterImpl(this);

        //se asinga el adapter para los spinners
        String[] ordenes = {"prueba1", "prueba2"};
        String[] medidores = {"Rotogate", "Magnatel"};


        spinnerOrdenCompra.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        spinnerMedidorAlmacen.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));
        spinnerMedidorTractor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));
        spinnerAlmacenes.setAdapter(new ArrayAdapter<String>(this,R.layout.custom_spinner,ordenes));

        //onclick del boton
        final Button buttonRegistrar = (Button) findViewById(R.id.registrar_button);
        buttonRegistrar.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickRegistrar();
            }
        });

        //se obtienen las ordenes de compra, con el token guardado en la session
        presenter.getOrdenesCompra(session.getIdEmpresa(),session.getTokenWithBearer());
        /* Detecto el evento clic en el label de 'No' para cambiar el switch a off*/
        TvIniciarDescargaActivityNo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                switchTanquePrestado.setChecked(false);
            }
        });
        /*Detecto el evento clic en el label de 'Si' opara cambiar el switch a on*/
        TvIniciarDescargaActivitySi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                switchTanquePrestado.setChecked(true);
            }
        });
    }

    //este metodo recopila los datos de la vista y los asigna al objeto
    public void onClickRegistrar(){
        Log.d("iniciardescargadto",iniciarDescargaDTO+"");
        System.out.println(ordenesCompraDTO);
        Log.d("Almacendto", almacenDTOs+"");
        iniciarDescargaDTO.setIdOrdenCompra(ordenesCompraDTO.get(spinnerOrdenCompra.getSelectedItemPosition()).getIdOrdenCompra());

        iniciarDescargaDTO.setIdTipoMedidorAlmacen(medidorDTOs.get(spinnerMedidorAlmacen.getSelectedItemPosition()).getIdTipoMedidor());
        iniciarDescargaDTO.setCantidadFotosAlmacen(medidorDTOs.get(spinnerMedidorAlmacen.getSelectedItemPosition()).getCantidadFotografias());
        iniciarDescargaDTO.setNombreTipoMedidorAlmacen(medidorDTOs.get(spinnerMedidorAlmacen.getSelectedItemPosition()).getNombreTipoMedidor());

        iniciarDescargaDTO.setIdTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getIdTipoMedidor());
        iniciarDescargaDTO.setCantidadFotosTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getCantidadFotografias());
        iniciarDescargaDTO.setNombreTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getNombreTipoMedidor());

        iniciarDescargaDTO.setIdAlmacen(almacenDTOs.get(spinnerAlmacenes.getSelectedItemPosition()).getIdAlmacenGas());

        iniciarDescargaDTO.setTanquePrestado(switchTanquePrestado.isChecked());
        showDialog(getResources().getString(R.string.message_continuar));

    }

    //funcion que muestra un aviso o un dialogo que se puede cancelar o aceptar
    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_cancel,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        builder1.setPositiveButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        startActivity();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    //funcion que muestra un aviso o un dialogo que solo se puede aceptar
    private void showDialogAceptar(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    //se inicia el nuevo activity
    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CapturaPorcentajeActivity.class);
        intent.putExtra("IniciarDescarga",iniciarDescargaDTO);
        intent.putExtra("EsPapeleta",false);
        intent.putExtra("EsDescargaIniciar",true);
        intent.putExtra("EsDescargaFinalizar",false);
        intent.putExtra("Almacen",false);
        intent.putExtra("TanquePrestado",switchTanquePrestado.isChecked());
        startActivity(intent);
    }

    //se muestra el cuadro de progreso
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    // se oculta el cuadro de progreso
    @Override
    public void hideProgress() {
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    //muestra un mensaje de error
    @Override
    public void messageError(int mensaje) {
        showDialogAceptar(getResources().getString(mensaje));
    }

    //metodo que se ejectuta cuando se terminan de obtener las ordenes de compra

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        Log.w("VIEW", respuestaOrdenesCompraDTO.getOrdenesCompra().size()+"");
        if(respuestaOrdenesCompraDTO.isExito()) {
            this.ordenesCompraDTO = respuestaOrdenesCompraDTO.getOrdenesCompra();
            String[] ordenes = new String[ordenesCompraDTO.size()];
            for (int i = 0; i < ordenes.length; i++) {
                //se asignan al arreglo que se pone en el spinner los nombres de las ordenes de compra
                ordenes[i] = ordenesCompraDTO.get(i).getNumOrdenCompra();
            }

            spinnerOrdenCompra.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(R.string.error_titulo);
            builder.setMessage(respuestaOrdenesCompraDTO.getMensaje());
            builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create().show();
        }
        //se hace el lamado al web service para obtener medidores
        presenter.getMedidores(session.getTokenWithBearer());
    }

    //metodo que se ejectuta cuando se terminan de obtener las ordenes de compra
    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs) {
        this.medidorDTOs = medidorDTOs;
        String[] medidores = new String[medidorDTOs.size()];
        for (int i =0; i<medidores.length; i++){
            medidores[i]=medidorDTOs.get(i).getNombreTipoMedidor();
        }

        spinnerMedidorAlmacen.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, medidores));
        spinnerMedidorTractor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, medidores));
        //se hace el lamado al web service para obtener almacenes
        presenter.getAlmacenes(session.getTokenWithBearer());
    }

    //metodo que se ejectuta cuando se terminan de obtener las ordenes de compra
    @Override
    public void onSuccessGetAlmacen(List<AlmacenDTO> almacenDTOs) {
        this.almacenDTOs = almacenDTOs;
        String[] almacenes = new String[almacenDTOs.size()];
        for (int i =0; i<almacenes.length; i++){
            almacenes[i]=almacenDTOs.get(i).getNombreAlmacen();
        }

        spinnerAlmacenes.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, almacenes));
    }
}

