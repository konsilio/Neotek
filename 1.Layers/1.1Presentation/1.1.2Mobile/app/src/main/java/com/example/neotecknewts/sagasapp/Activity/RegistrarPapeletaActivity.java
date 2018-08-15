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
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class RegistrarPapeletaActivity extends AppCompatActivity implements RegistrarPapeletaView{

    public Spinner spinnerOrdenCompraExpedidor;
    public Spinner spinnerOrdenCompraPorteador;
    public Spinner spinnerMedidorTractor;
    public TextView textViewFecha;
    public TextView textViewFechaEmbarque;
    public EditText editTextNumEmbarque;
    public EditText editTextNombrePorteador;
    public EditText editTextNombreExpedidor;
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
    public String tipoMedidor;
    private int mYear;
    private int mMonth;
    private int mDay;

    private Date fecha;
    private Date fechaEmbarque;
    public int fechaSeleccionada =0;
    static final int DATE_DIALOG_ID = 0;

    public OrdenCompraDTO ordenCompraDTOExpedidor;
    List<OrdenCompraDTO> ordenesCompraDTOExpedidor;
    public OrdenCompraDTO ordenCompraDTOPorteador;
    List<OrdenCompraDTO> ordenesCompraDTOPorteador;

    PrecargaPapeletaDTO papeletaDTO;

    ProgressDialog progressDialog;
    Session session;
    RegistrarPapeletaPresenter presenter;

    List<MedidorDTO> medidorDTOs;
    List<AlmacenDTO> almacenDTOs;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar_papeleta);

        session = new Session(getApplicationContext());
        presenter = new RegistrarPapeletaPresenterImpl(this);

        fecha = null;
        fechaEmbarque = null;

        papeletaDTO = new PrecargaPapeletaDTO();

        spinnerOrdenCompraExpedidor = (Spinner) findViewById(R.id.spinner_orden_compra_expedidor);
        spinnerOrdenCompraPorteador = (Spinner) findViewById(R.id.spinner_orden_compra_porteador);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);
        textViewFecha = (TextView) findViewById(R.id.textFecha);
        textViewFechaEmbarque = (TextView) findViewById(R.id.textFechaEmbarque) ;
        editTextNumEmbarque = (EditText) findViewById(R.id.input_embarque);
        editTextNombreExpedidor = (EditText) findViewById(R.id.input_nombre_expedidor);
        editTextNombrePorteador = (EditText) findViewById(R.id.input_nombre_porteador);
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

        ordenesCompraDTOExpedidor = new ArrayList<>();
        ordenesCompraDTOPorteador = new ArrayList<>();

        String[] ordenes = {"prueba", "prueba"};
        final String [] medidores = {"Magnatel", "Rotogate"};
        spinnerOrdenCompraPorteador.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        spinnerOrdenCompraExpedidor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        spinnerMedidorTractor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));


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

        spinnerOrdenCompraExpedidor.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                if (ordenesCompraDTOExpedidor.size()!=0){
                    Log.w("Selected",""+position);
                    ordenCompraDTOExpedidor = ordenesCompraDTOExpedidor.get(spinnerOrdenCompraExpedidor.getSelectedItemPosition());
                    spinnerOrdenCompraExpedidor.getSelectedItemPosition();
                    editTextNombreExpedidor.setText(ordenCompraDTOExpedidor.getProveedorNombreComercial());
                    editTextNombreExpedidor.setEnabled(false);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        spinnerOrdenCompraPorteador.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                if (ordenesCompraDTOPorteador.size()!=0){
                    Log.w("Selected",""+position);
                    ordenCompraDTOPorteador = ordenesCompraDTOPorteador.get(spinnerOrdenCompraPorteador.getSelectedItemPosition());
                    spinnerOrdenCompraPorteador.getSelectedItemPosition();
                    editTextNombrePorteador.setText(ordenCompraDTOPorteador.getProveedorNombreComercial());
                    editTextNombrePorteador.setEnabled(false);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        spinnerMedidorTractor.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                //if (medidores.size()!=0){
                    Log.w("Selected",""+position);
                    tipoMedidor = medidores[spinnerMedidorTractor.getSelectedItemPosition()];
                //}
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        presenter.getOrdenesCompraExpedidor(session.getIdEmpresa(),session.getTokenWithBearer());
    }

    private void updateDisplay() {
        if(fechaSeleccionada==0) {
            if(fecha==null){
                fecha = new Date();
            }
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
            if(fechaEmbarque == null){
                fechaEmbarque = new Date();
            }
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
                || TextUtils.isEmpty(nombreResponsable) ){
            empty = true;
        }

        if(fecha == null || fechaEmbarque == null)
        {
            empty=true;
        }

        if(empty){
            showDialog(getResources().getString(R.string.empty_field));
        }else{
            buildPapeleta();
        }
    }

    public void buildPapeleta(){

        papeletaDTO.setCapacidadTanque(Double.parseDouble(editTextCapTanque.getText().toString()));
        papeletaDTO.setFecha(fecha);
        papeletaDTO.setFechaEmbarque(fechaEmbarque);
        papeletaDTO.setIdOrdenCompraExpedidor(ordenCompraDTOExpedidor.getIdOrdenCompra());
        papeletaDTO.setIdOrdenCompraPorteador(ordenCompraDTOPorteador.getIdOrdenCompra());
        papeletaDTO.setIdProveedorPorteador(ordenCompraDTOPorteador.getIdProveedor());
        papeletaDTO.setIdProveedorExpedidor(ordenCompraDTOExpedidor.getIdProveedor());
        papeletaDTO.setNumeroEmbarque(editTextNumEmbarque.toString());
        papeletaDTO.setPlacasTractor(editTextPlacasTractor.toString());
        papeletaDTO.setNombreOperador(editTextNombreOperador.toString());
        papeletaDTO.setProducto(editTextProducto.toString());
        papeletaDTO.setNumeroTanque(editTextNumTanque.toString());
        papeletaDTO.setPresionTanque(Double.parseDouble(editTextPresionTanque.getText().toString()));
        papeletaDTO.setPorcentajeTanque(Double.parseDouble(editTextPorcentajeTanque.getText().toString()));
        papeletaDTO.setMasa(Double.parseDouble(editTextMasa.getText().toString()));
        papeletaDTO.setSello(editTextSello.toString());
        papeletaDTO.setValorCarga(Double.parseDouble(editTextValorCarga.getText().toString()));
        papeletaDTO.setNombreResponsable(editTextNombreResponsable.toString());

        papeletaDTO.setCantidadFotosTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getCantidadFotografias());
        papeletaDTO.setIdTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getIdTipoMedidor());
        papeletaDTO.setNombreTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getNombreTipoMedidor());
        startActivity();
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
        Intent intent = new Intent(getApplicationContext(), CameraPapeletaActivity.class);
        intent.putExtra("Papeleta",papeletaDTO);
        startActivity(intent);
    }


    public void onClickLimpiar(){
        spinnerOrdenCompraPorteador.setSelection(0);
        spinnerOrdenCompraExpedidor.setSelection(0);
        textViewFecha.setText(getResources().getString(R.string.seleccionar_fecha));
        textViewFechaEmbarque.setText(getResources().getString(R.string.seleccionar_fecha));
        editTextNumEmbarque.setText("");
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
    public void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTOExpedidor = respuesta.getOrdenesCompra();
        String[] ordenes = new String[ordenesCompraDTOExpedidor.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTOExpedidor.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompraExpedidor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
        presenter.getOrdenesCompraPorteador(session.getIdEmpresa(),session.getTokenWithBearer());
    }

    @Override
    public void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTOPorteador = respuesta.getOrdenesCompra();
        String[]ordenes = new String[ordenesCompraDTOPorteador.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTOPorteador.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompraPorteador.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
        presenter.getMedidores(session.getTokenWithBearer());
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs) {
        this.medidorDTOs = medidorDTOs;
        String[]medidores = new String[medidorDTOs.size()];
        for (int i =0; i<medidores.length; i++){
            medidores[i]=medidorDTOs.get(i).getNombreTipoMedidor();
        }

        spinnerMedidorTractor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, medidores));
    }
}
