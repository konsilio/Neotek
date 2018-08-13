package com.example.neotecknewts.sagasapp.Activity;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.Calendar;
import java.util.Date;
import java.util.List;

import com.example.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class RegistrarPapeletaActivity extends AppCompatActivity implements RegistrarPapeletaView{

    public Spinner spinnerOrdenCompra;
    public TextView textViewFecha;
    public TextView textViewFechaEmbarque;
    public EditText editTextNumEmbarque;
    public Spinner spinnerExpedidor;
    public Spinner spinnerPorteador;
    public EditText editTextNombreOperador;
    public EditText editTextPlacasTractor;
    public EditText editTextProducto;
    public EditText editTextNumTanque;
    public EditText editTextCapTanque;
    public EditText editTextPorcentajeTanque;
    public EditText editTextMasa;
    public EditText editTextPresionTanque;
    public EditText editTextSello;
    public EditText editTextValorCarga;
    public EditText editTextNombreResponsable;

    private int mYear;
    private int mMonth;
    private int mDay;

    private Date fecha;
    private Date fechaEmbarque;
    public int fechaSeleccionada =0;
    static final int DATE_DIALOG_ID = 0;

    public OrdenCompraDTO ordenCompraDTO;
    List<OrdenCompraDTO> ordenesCompraDTO;

    ProgressDialog progressDialog;
    Session session;
    RegistrarPapeletaPresenter presenter;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar_papeleta);

        session = new Session(getApplicationContext());
        presenter = new RegistrarPapeletaPresenterImpl(this);

        fecha = new Date();
        fechaEmbarque = new Date();

        spinnerOrdenCompra = (Spinner) findViewById(R.id.spinner_orden_compra);
        textViewFecha = (TextView) findViewById(R.id.textFecha);
        textViewFechaEmbarque = (TextView) findViewById(R.id.textFechaEmbarque) ;
        editTextNumEmbarque = (EditText) findViewById(R.id.input_embarque);
        spinnerExpedidor = (Spinner) findViewById(R.id.spinner_expedidor);
        spinnerPorteador = (Spinner) findViewById(R.id.spinner_porteador);
        editTextNombreOperador = (EditText) findViewById(R.id.input_nombre_operador);
        editTextPlacasTractor = (EditText) findViewById(R.id.input_placas_tractor);
        editTextProducto = (EditText) findViewById(R.id.input_producto);
        editTextNumTanque = (EditText) findViewById(R.id.input_num_tanque);
        editTextCapTanque = (EditText) findViewById(R.id.input_cap_tanque);
        editTextPorcentajeTanque = (EditText) findViewById(R.id.input_medidor);
        editTextMasa = (EditText) findViewById(R.id.input_masa);
        editTextPresionTanque = (EditText) findViewById(R.id.input_presion_tanque);
        editTextSello = (EditText) findViewById(R.id.input_sello);
        editTextValorCarga = (EditText) findViewById(R.id.input_valor_carga);
        editTextNombreResponsable = (EditText) findViewById(R.id.input_nombre_responsable);

        String[] ordenes = {"prueba", "prueba"};
        String[] expedidores = {"Expedidor1", "Expedidor2"};


        spinnerOrdenCompra.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        spinnerExpedidor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, expedidores));
        spinnerPorteador.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, expedidores));

        final ImageButton buttonFecha = (ImageButton) findViewById(R.id.imageBtnFecha);
        buttonFecha.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                fechaSeleccionada=0;
                showDialog(DATE_DIALOG_ID);

            }
        });

        // get the current date
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);


        final ImageButton buttonFechaEmbarque = (ImageButton) findViewById(R.id.imageBtnFechaEmbarque);
        buttonFechaEmbarque.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                fechaSeleccionada=1;
                showDialog(DATE_DIALOG_ID);
            }
        });

        final Button buttonRegistrar = (Button) findViewById(R.id.registrar_button);
        buttonRegistrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickRegistrar();
            }
        });

        final Button buttonLimpiar = (Button) findViewById(R.id.limpiar_button);
        buttonLimpiar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickLimpiar();
            }
        });

        presenter.getOrdenesCompra(session.getIdEmpresa(),session.getTokenWithBearer());
    }

    private void updateDisplay() {
        if(fechaSeleccionada==0) {
            textViewFecha.setText(
                    new StringBuilder()
                            // Month is 0 based so add 1
                            .append(mDay).append("-")
                            .append(mMonth + 1).append("-")
                            .append(mYear).append(" "));
            fecha.setDate(mDay);
            fecha.setMonth(mMonth);
            fecha.setYear(mYear);
        }
        if(fechaSeleccionada==1) {
            textViewFechaEmbarque.setText(
                    new StringBuilder()
                            // Month is 0 based so add 1
                            .append(mDay).append("-")
                            .append(mMonth + 1).append("-")
                            .append(mYear).append(" "));
            fechaEmbarque.setDate(mDay);
            fechaEmbarque.setMonth(mMonth);
            fechaEmbarque.setYear(mYear);
        }
    }

    private DatePickerDialog.OnDateSetListener mDateSetListener =
            new DatePickerDialog.OnDateSetListener() {
                public void onDateSet(DatePicker view, int year,
                                      int monthOfYear, int dayOfMonth) {
                    mYear = year;
                    mMonth = monthOfYear;
                    mDay = dayOfMonth;
                    updateDisplay();
                }
            };
    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DATE_DIALOG_ID:
                return new DatePickerDialog(this,
                        mDateSetListener,
                        mYear, mMonth, mDay);
        }
        return null;
    }


    public void onClickRegistrar(){
        boolean empty = false;

        String numeroEmbarque = editTextNumEmbarque.getText().toString();
        String placasTractor = editTextPlacasTractor.getText().toString();
        String nombreOperador = editTextNombreOperador.getText().toString();
        String producto = editTextProducto.getText().toString();
        String noTanque = editTextNumTanque.getText().toString();

        String presTanque = editTextPresionTanque.getText().toString();
        if (TextUtils.isDigitsOnly(presTanque)) {
            if (presTanque.length() == 0) {
                empty = true;
            }
        }

        String capTanque = editTextCapTanque.getText().toString();
            if (TextUtils.isDigitsOnly(capTanque)) {
                if (capTanque.length() == 0) {
                    empty = true;
                }
            }

        String porcentTanque = editTextPorcentajeTanque.getText().toString();
        if (TextUtils.isDigitsOnly(porcentTanque)) {
            if (porcentTanque.length() == 0) {
                empty = true;
            }
        }

        String masa = editTextMasa.getText().toString();
        if (TextUtils.isDigitsOnly(masa)) {
            if (masa.length() == 0) {
                empty = true;
            }
        }

        String sello = editTextSello.getText().toString();
        String nombreResponsable = editTextNombreResponsable.getText().toString();
        String valorCarga = editTextValorCarga.getText().toString();
        if (TextUtils.isDigitsOnly(valorCarga)) {
            if (valorCarga.length() == 0) {
                empty = true;
            }
        }

        if(TextUtils.isEmpty(numeroEmbarque) || TextUtils.isEmpty(placasTractor) || TextUtils.isEmpty(nombreOperador)
                || TextUtils.isEmpty(producto) || TextUtils.isEmpty(noTanque) || TextUtils.isEmpty(sello)
                || TextUtils.isEmpty(nombreResponsable) || empty ){
            showDialog(getResources().getString(R.string.empty_field));
        }

    }

    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
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

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivity(intent);
    }


    public void onClickLimpiar(){
        spinnerOrdenCompra.setSelection(0);
        textViewFecha.setText(getResources().getString(R.string.seleccionar_fecha));
        textViewFechaEmbarque.setText(getResources().getString(R.string.seleccionar_fecha));
        editTextNumEmbarque.setText("");
        spinnerExpedidor.setSelection(0);
        spinnerPorteador.setSelection(0);
        editTextNombreOperador.setText("");
        editTextPlacasTractor.setText("");
        editTextProducto.setText("");
        editTextNumTanque.setText("");
        editTextCapTanque.setText("");
        editTextPorcentajeTanque.setText("");
        editTextMasa.setText("");
        editTextPresionTanque.setText("");
        editTextSello.setText("");
        editTextValorCarga.setText("");
        editTextNombreResponsable.setText("");
    }

    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    @Override
    public void hideProgress() {
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    @Override
    public void messageError(int mensaje) {
        showDialog(getResources().getString(mensaje));
    }

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTO = respuesta.getOrdenesCompra();
        String[] ordenes = new String[ordenesCompraDTO.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTO.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompra.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
    }
}
