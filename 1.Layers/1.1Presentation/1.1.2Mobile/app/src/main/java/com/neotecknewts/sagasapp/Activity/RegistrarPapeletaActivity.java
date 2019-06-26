package com.neotecknewts.sagasapp.Activity;

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

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenReferenciaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenter;
import com.neotecknewts.sagasapp.Presenter.RegistrarPapeletaPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class RegistrarPapeletaActivity extends AppCompatActivity implements RegistrarPapeletaView {

    //variables de la vista
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

    //variables para la fecha
    private int mYear;
    private int mMonth;
    private int mDay;

    private Date fecha;
    private Date fechaEmbarque;
    public int fechaSeleccionada = 0;
    static final int DATE_DIALOG_ID = 0;

    //variables para la lista de ordenes de compra y la orden de compra seleccionada
    public OrdenCompraDTO ordenCompraDTOExpedidor;
    List<OrdenCompraDTO> ordenesCompraDTOExpedidor;
    public OrdenCompraDTO ordenCompraDTOPorteador;
    List<OrdenCompraDTO> ordenesCompraDTOPorteador;

    //objeto a completar con esta vista
    PrecargaPapeletaDTO papeletaDTO;

    //objetos de dialogo de progresso, sesion y presenter
    ProgressDialog progressDialog;
    Session session;
    RegistrarPapeletaPresenter presenter;

    //lista de medidores para el spinner
    List<MedidorDTO> medidorDTOs;
    boolean inicial_porteador;
    boolean inicial_expedidor;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        inicial_expedidor = false;
        inicial_porteador = false;
        setContentView(R.layout.activity_registrar_papeleta);

        //se inicializan session y presenter
        session = new Session(getApplicationContext());
        presenter = new RegistrarPapeletaPresenterImpl(this);

        fecha = null;
        fechaEmbarque = null;

        papeletaDTO = new PrecargaPapeletaDTO();

        //se obtienen las variables de la vista
        spinnerOrdenCompraExpedidor = (Spinner) findViewById(R.id.spinner_orden_compra_expedidor);
        spinnerOrdenCompraPorteador = (Spinner) findViewById(R.id.spinner_orden_compra_porteador);
        spinnerMedidorTractor = (Spinner) findViewById(R.id.spinner_medidor_tractor);
        textViewFecha = (TextView) findViewById(R.id.textFecha);
        textViewFechaEmbarque = (TextView) findViewById(R.id.textFechaEmbarque);
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

        //se completan los spinners con los adapters
        String[] ordenes = {"prueba", "prueba"};
        final String[] medidores = {"Magnatel", "Rotogate"};
        //spinnerOrdenCompraPorteador.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        //spinnerOrdenCompraExpedidor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        //spinnerMedidorTractor.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, medidores));

        //onclick para la fecha

        final ImageButton buttonFecha = (ImageButton) findViewById(R.id.imageBtnFecha);
        buttonFecha.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                fechaSeleccionada = 0;
                showDialog(DATE_DIALOG_ID);

            }
        });

        // get the current date
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);


        //onclick para la fecha de embarque
        final ImageButton buttonFechaEmbarque = (ImageButton) findViewById(R.id.imageBtnFechaEmbarque);
        buttonFechaEmbarque.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                fechaSeleccionada = 1;
                showDialog(DATE_DIALOG_ID);
            }
        });

        //onclick para registrar
        final Button buttonRegistrar = (Button) findViewById(R.id.registrar_button);
        buttonRegistrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickRegistrar();
            }
        });

        //onclcik para limpiar campos
        final Button buttonLimpiar = (Button) findViewById(R.id.limpiar_button);
        buttonLimpiar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onClickLimpiar();
            }
        });

        //listener que obtiene la orden de compra del expedidor y llena ese campo
        spinnerOrdenCompraExpedidor.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                if (ordenesCompraDTOExpedidor.size() != 0) {
                    Log.w("Selected", "" + position);
                    ordenCompraDTOExpedidor = ordenesCompraDTOExpedidor.get(spinnerOrdenCompraExpedidor.getSelectedItemPosition());
                    spinnerOrdenCompraExpedidor.getSelectedItemPosition();
                    editTextNombreExpedidor.setText(ordenCompraDTOExpedidor.getProveedorNombreComercial());
                    papeletaDTO.setIdOrdenCompraExpedidor(ordenCompraDTOExpedidor.getIdOrdenCompra());
                    papeletaDTO.setIdOrdenCompraExpedidor(ordenCompraDTOExpedidor.getIdProveedor());
                    editTextNombreExpedidor.setEnabled(false);
                    if (position >= 0 && ordenCompraDTOExpedidor!=null) {
                        presenter.getOrderReferencia(session.getToken(),
                                ordenCompraDTOExpedidor.getIdOrdenCompra(),
                                true
                        );
                    }
                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });
        //listener que obtiene la orden de compra del porteador y llena ese campo
        spinnerOrdenCompraPorteador.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                if (ordenesCompraDTOPorteador.size() != 0) {
                    Log.w("Selected", "" + position);
                    ordenCompraDTOPorteador = ordenesCompraDTOPorteador.get(spinnerOrdenCompraPorteador.getSelectedItemPosition());
                    spinnerOrdenCompraPorteador.getSelectedItemPosition();
                    editTextNombrePorteador.setText(ordenCompraDTOPorteador.getProveedorNombreComercial());
                    papeletaDTO.setIdOrdenCompraPorteador(ordenCompraDTOPorteador.getIdOrdenCompra());
                    papeletaDTO.setIdOrdenCompraPorteador(ordenCompraDTOPorteador.getIdProveedor());
                    editTextNombrePorteador.setEnabled(false);
                    if (position >= 0 && ordenCompraDTOPorteador!=null) {
                        presenter.getOrderReferencia(session.getToken(),
                                ordenCompraDTOPorteador.getIdOrdenCompra(),
                                false
                        );
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        //listener que obtiene el tipo de medidor
        spinnerMedidorTractor.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                //if (medidores.size()!=0){
                Log.w("Selected", "" + position);
                tipoMedidor = medidores[spinnerMedidorTractor.getSelectedItemPosition()];
                //}
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        presenter.getOrdenesCompraExpedidor(session.getIdEmpresa(), session.getTokenWithBearer());
        inicial_porteador = true;
        inicial_expedidor = true;
    }

    //metodo que pone el texto de la fecha seleccionada
    private void updateDisplay() {
        if (fechaSeleccionada == 0) {
            if (fecha == null) {
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
        if (fechaSeleccionada == 1) {
            if (fechaEmbarque == null) {
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
                        R.style.datepicker,
                        mDateSetListener,
                        mYear, mMonth, mDay);
        }
        return null;
    }


    //metodo que verifica que los campos esten completos y si no muestra mensjae
    public void onClickRegistrar() {
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

        if (TextUtils.isEmpty(numeroEmbarque) || TextUtils.isEmpty(placasTractor) || TextUtils.isEmpty(nombreOperador)
                || TextUtils.isEmpty(producto) || TextUtils.isEmpty(noTanque) || TextUtils.isEmpty(sello)
                || TextUtils.isEmpty(nombreResponsable)) {
            empty = true;
        }

        if (fecha == null || fechaEmbarque == null) {
            empty = true;
        }

        if (empty) {
            showDialog(getResources().getString(R.string.empty_field));
        } else {
            buildPapeleta();
        }
    }

    //metodo que construye el objeto papeleta
    public void buildPapeleta() {
        SimpleDateFormat sf = new SimpleDateFormat("y-MM-dd HH:mm:ss");
        String formato_fecha = String.valueOf(fecha.getYear()) + "-" + String.valueOf(fecha.getMonth()) + "-" + String.valueOf(fecha.getDate()) + " " + String.valueOf(fecha.getHours())
                + ":" + String.valueOf(fechaEmbarque.getMinutes()) + ":" + String.valueOf(fechaEmbarque.getSeconds());
        String formato_fecha_embarque = String.valueOf(fechaEmbarque.getYear()) + "-" + String.valueOf(fechaEmbarque.getMonth()) + "-" + String.valueOf(fechaEmbarque.getDate()) + " " + String.valueOf(fechaEmbarque.getHours())
                + ":" + String.valueOf(fechaEmbarque.getMinutes()) + ":" + String.valueOf(fechaEmbarque.getSeconds());
        //sf.format(fechaEmbarque);

        papeletaDTO.setCapacidadTanque(Double.parseDouble(editTextCapTanque.getText().toString()));
        //Falta capacidad tanque el kg
        papeletaDTO.setCapacidadTanqueKg(Double.parseDouble("0"));
        papeletaDTO.setFecha(formato_fecha);
        papeletaDTO.setFechaEmbarque(formato_fecha_embarque);
        papeletaDTO.setIdOrdenCompraExpedidor(ordenCompraDTOExpedidor.getIdOrdenCompra());
        papeletaDTO.setIdOrdenCompraPorteador(ordenCompraDTOPorteador.getIdOrdenCompra());
        papeletaDTO.setIdProveedorPorteador(ordenCompraDTOPorteador.getIdProveedor());
        papeletaDTO.setIdProveedorExpedidor(ordenCompraDTOExpedidor.getIdProveedor());
        papeletaDTO.setNumeroEmbarque(editTextNumEmbarque.getText().toString());
        papeletaDTO.setPlacasTractor(editTextPlacasTractor.getText().toString());
        papeletaDTO.setNombreOperador(editTextNombreOperador.getText().toString());
        papeletaDTO.setProducto(editTextProducto.getText().toString());
        papeletaDTO.setNumeroTanque(editTextNumTanque.getText().toString());
        papeletaDTO.setPresionTanque(Double.parseDouble(editTextPresionTanque.getText().toString()));
        papeletaDTO.setPorcentajeTanque(Double.parseDouble(editTextPorcentajeTanque.getText().toString()));
        papeletaDTO.setMasa(Double.parseDouble(editTextMasa.getText().toString()));
        papeletaDTO.setSello(editTextSello.getText().toString());
        papeletaDTO.setValorCarga(Double.parseDouble(editTextValorCarga.getText().toString()));
        papeletaDTO.setNombreResponsable(editTextNombreResponsable.getText().toString());

        papeletaDTO.setCantidadFotosTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getCantidadFotografias());
        papeletaDTO.setIdTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getIdTipoMedidor());
        papeletaDTO.setNombreTipoMedidorTractor(medidorDTOs.get(spinnerMedidorTractor.getSelectedItemPosition()).getNombreTipoMedidor());
        if (papeletaDTO.getIdOrdenCompraExpedidor() == papeletaDTO.getIdOrdenCompraPorteador()){
            AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
            builder.setTitle(R.string.mensjae_error_campos);
            builder.setMessage("La orden de compra de porteador y de expedidor no puede ser la misma");
            builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                    spinnerOrdenCompraExpedidor.setFocusable(true));
            builder.create().show();
        }else{
            startActivity();
        }
    }

    //metodo que muestra un mensaje
    private void showDialog(String mensaje){
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

    //se inicia el siguiente activity con el objeto
    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), CameraPapeletaActivity.class);
        intent.putExtra("Papeleta",papeletaDTO);
        intent.putExtra("EsPapeleta",true);
        intent.putExtra("EsMedidor",false);
        startActivity(intent);
    }

//metodo que vacia los campos
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

    //metodo que muestra un progress al estar obteniendo los datos
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
        progressDialog.setProgressStyle(R.style.AlertDialog);
    }

    //metodo que oculta el progresso
    @Override
    public void hideProgress() {
        if(progressDialog != null && progressDialog.isShowing()){
            progressDialog.dismiss();
        }
    }

    //metodo que muestra mensaje de error
    @Override
    public void messageError(int mensaje) {
        Log.w("Mensaje",getResources().getString(mensaje));
        //showDialog(getResources().getString(mensaje));
        AlertDialog.Builder builder1 = new AlertDialog.Builder(
                RegistrarPapeletaActivity.this,R.style.AlertDialog);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        //AlertDialog alert11 = builder1.create();
        //alert11.show();
        builder1.create();
        builder1.show();
    }

    //metodo que se ejecuta al obtener las ordenes de compra del expedidor y llena el sppiner con los numeros de orden de compra
    @Override
    public void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTOExpedidor = respuesta.getOrdenesCompra();
        String[] ordenes = new String[ordenesCompraDTOExpedidor.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTOExpedidor.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompraExpedidor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
        //por medio del presenter se obtiene la siguiente lista
        presenter.getOrdenesCompraPorteador(session.getIdEmpresa(),session.getTokenWithBearer());
    }

    //metodo que se ejecuta al obtener las ordenes de compra del expedidor y llena el sppiner con los numeros de orden de compra
    @Override
    public void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTOPorteador = respuesta.getOrdenesCompra();
        String[]ordenes = new String[ordenesCompraDTOPorteador.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTOPorteador.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompraPorteador.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
        //por medio del presenter se obtiene la siguiente lista
        presenter.getMedidores(session.getTokenWithBearer());
    }

    //metodo que se ejecuta al obtener los medidores y llena el sppiner con los nombres
    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs) {
        this.medidorDTOs = medidorDTOs;
        String[]medidores = new String[medidorDTOs.size()];
        for (int i =0; i<medidores.length; i++){
            medidores[i]=medidorDTOs.get(i).getNombreTipoMedidor();
        }

        spinnerMedidorTractor.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, medidores));
    }

    /**
     * Muestra el mensaje de exito para la papeleta registrada
     */
    @Override
    public void onSuccessRegistrarPapeleta() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this.getApplicationContext(),
                R.style.AlertDialog);
        builder.setTitle("Listo");
        builder.setMessage("Papeleta registrada");
        builder.show();
    }

    /**
     * Muesta el mensaje de exito en iniciar la descarga
     */
    @Override
    public void onSuccessRegistrarIniciarDescarga() {

    }

    /**
     * Muestra el mensaje de error
     */
    @Override
    public void showMessageError() {

    }

    @Override
    public void messageError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void onSuccessReferencia(RespuestaOrdenReferenciaDTO data, boolean esExpedidor) {
        if(data!=null){
            if(data.isExito() && data.getId()>0){
                if(esExpedidor){
                    for (OrdenCompraDTO or:
                            this.ordenesCompraDTOPorteador) {
                        if(or.getIdOrdenCompra()==data.getId()){
                            if(or.getIdOrdenCompra()== data.getId()){
                                int index = this.ordenesCompraDTOPorteador.indexOf(or);
                                spinnerOrdenCompraPorteador.setSelection(index);
                            }
                        }
                    }
                }else{
                    for (OrdenCompraDTO or :
                            this.ordenesCompraDTOPorteador) {
                        if(or.getIdOrdenCompra()==data.getId()){
                            if(or.getIdOrdenCompra()== data.getId()){
                                int index = this.ordenesCompraDTOPorteador.indexOf(or);
                                spinnerOrdenCompraExpedidor.setSelection(index);
                            }
                        }
                    }
                }
            }
        }
    }
}
