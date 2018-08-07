package com.example.neotecknewts.sagas.Activity;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.Calendar;
import java.util.Date;
import com.example.neotecknewts.sagas.R;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class RegistrarPapeletaActivity extends AppCompatActivity {

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



    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar_papeleta);

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

}
