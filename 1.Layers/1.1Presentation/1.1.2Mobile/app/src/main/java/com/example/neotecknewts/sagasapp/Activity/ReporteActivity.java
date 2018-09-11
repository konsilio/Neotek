package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;

public class ReporteActivity extends AppCompatActivity implements ReporteView{
    private TextView TVReporteActivityFecha;
    private ImageButton IBReporteFechaActivityFecha;
    private Spinner SReporteActivityListUnidades;

    private boolean EsReporteDelDia;
    private ArrayList<UnidadesDTO> unidadesDTOS;
    private UnidadesDTO unidadesDTO;
    private ProgressDialog progressDialog;

    public int mYear,mMonth,mDay;
    public Date fecha;

    public DatePickerDialog.OnDateSetListener onDateSetListener =
            (view, year, month, dayOfMonth) -> {
                mYear = year;
                mMonth = month;
                mDay = dayOfMonth;
                setFecha();
            };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_reporte);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsReporteDelDia= (boolean)bundle.get("EsReporteDelDia");
        }
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);

        TextView TVReporteActivityTitulo = findViewById(R.id.TVLecturaAlmacenActivityTitulo);
        TextView TVReporteactivitySeleccionaFecha = findViewById(R.id.TVReporteactivitySeleccionaFecha);
        TVReporteActivityFecha = findViewById(R.id.TVReporteActivityFecha);
        TextView TVReporteActivitySelecioneUnidad = findViewById(R.id.TVReporteActivitySelecioneUnidad);
        IBReporteFechaActivityFecha = findViewById(R.id.IBReporteFechaActivityFecha);
        SReporteActivityListUnidades = findViewById(R.id.SReporteActivityListUnidades);
        Button btnReporteactivityAceptar = findViewById(R.id.BtnReporteactivityAceptar);

        IBReporteFechaActivityFecha.setOnClickListener(v -> {
            showDialog(0);
        });
        String[] list_unidades = new String[]{"Seleccione", "Pipa No. 1", "Camioneta No. 1", "Estación No. 1",
                "Pipa No. 2", "Camioneta No. 2", "Estación No. 2"};
        SReporteActivityListUnidades.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner, list_unidades));
        SReporteActivityListUnidades.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        TVReporteActivityFecha.setOnClickListener(v->{
            IBReporteFechaActivityFecha.callOnClick();
        });
        btnReporteactivityAceptar.setOnClickListener(v -> {
            VermificarCampos();
        });
    }

    @Override
    public void VermificarCampos() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(fecha==null ){
            error = true;
            mensajes.add("La fecha a obtener el reporte es requerido");
        }
        if(SReporteActivityListUnidades.getSelectedItemPosition()<=0){
            error = true;
            mensajes.add("La unidad es un valor requerido");
        }
        if(error)
            MensajeError(mensajes);
        else
            new HiloReporte().execute();
    }

    @Override
    public void MensajeError(ArrayList<String> mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(ReporteActivity.this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder dialogo = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for (String error:mensaje){
            dialogo.append("\n").append(error);
        }
        builder.setMessage(dialogo);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void onSuccessGetUnidades(ArrayList<UnidadesDTO> data) {

    }

    @Override
    public void onErrorMessage(String mensaje) {

    }

    @Override
    public void onShowProgress() {
        progressDialog = new ProgressDialog(ReporteActivity.this);
        progressDialog.setMessage(getString(R.string.generando_reporte));
        progressDialog.setCancelable(false);
        progressDialog.show();
    }

    @Override
    public void hiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();

        }
    }

    public void setFecha(){
            if(fecha==null){
                fecha = new Date();
            }
        TVReporteActivityFecha.setText(
                    new StringBuilder()
                            // Month is 0 based so add 1
                            .append(mDay).append("-")
                            .append(mMonth + 1).append("-")
                            .append(mYear).append(" "));
            fecha.setDate(mDay);
            fecha.setMonth(mMonth);
            fecha.setYear(mYear);
    }

    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case 0:
                return new DatePickerDialog(this,
                        onDateSetListener,
                        mYear, mMonth, mDay);
        }
        return null;
    }

    @SuppressLint("StaticFieldLeak")
    public class HiloReporte extends AsyncTask<Void,Integer,String>{
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            onShowProgress();
        }

        @Override
        protected void onPostExecute(String data) {
            super.onPostExecute(data);
            hiddeProgress();
            if(EsReporteDelDia) {
                Intent intent = new Intent(ReporteActivity.this,
                        VerReporteActivity.class);
                intent.putExtra("EsReporteDelDia",EsReporteDelDia);
                intent.putExtra("FechaReporte",fecha);

                intent.putExtra("StringReporte",data);

                startActivity(intent);
            }
        }

        @Override
        protected void onProgressUpdate(Integer... values) {
            super.onProgressUpdate(values);
            Log.w("Progress", Arrays.toString(values));
        }

        @Override
        protected String doInBackground(Void... voids) {
            return "Reporte ";
        }
    }
}
