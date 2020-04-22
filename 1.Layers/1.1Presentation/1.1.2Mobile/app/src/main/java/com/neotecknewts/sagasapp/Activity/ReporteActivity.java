package com.neotecknewts.sagasapp.Activity;

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

import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.neotecknewts.sagasapp.Model.ReporteDto;
import com.neotecknewts.sagasapp.Presenter.ReportePresenter;
import com.neotecknewts.sagasapp.Presenter.ReportePresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;

public class ReporteActivity extends AppCompatActivity implements ReporteView {
    private TextView TVReporteActivityFecha;
    private ImageButton IBReporteFechaActivityFecha;
    private Spinner SReporteActivityListUnidades;

    private boolean EsReporteDelDia;
    private DatosReporteDTO unidadesDTOS;
    private DatosReporteDTO.AlmacenesDTO unidadesDTO;
    private ProgressDialog progressDialog;

    public int mYear, mMonth, mDay;
    public Date fecha;
    public Session session;
    public ReportePresenter presenter;
    public Object reporteDTO;
    public String[] reporte_con_formato;
    public String[] datos;
    public String txt_date;
    VentaDTO ventaDTO;

    public String tanquestxt;
    public String tanqueshtml;

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
        Log.d("bundle",bundle.toString());
        if (bundle != null) {
            EsReporteDelDia = (boolean) bundle.get("EsReporteDelDia");
            Log.d("bundlereportedia",EsReporteDelDia+"");
        }
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);
        session = new Session(ReporteActivity.this);
        presenter = new ReportePresenterImpl(this);

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
        presenter.GetUnidades(session.getToken());
       /* String[] list_unidades = new String[]{"Seleccione", "Pipa No. 1", "Camioneta No. 1", "Estación No. 1",
                "Pipa No. 2", "Camioneta No. 2", "Estación No. 2"};
        SReporteActivityListUnidades.setAdapter(new ArrayAdapter<>(this,
                R.layout.custom_spinner, list_unidades));*/
        SReporteActivityListUnidades.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                        if (unidadesDTOS != null && unidadesDTOS.getAlmacenes().size() > 0) {
                            for (DatosReporteDTO.AlmacenesDTO unidad : unidadesDTOS.getAlmacenes()) {
                                if (unidad.getNombreAlmacen().equals(parent.getItemAtPosition(position).toString())) {
                                    unidadesDTO = unidad;
                                }
                            }
                        }
                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> parent) {
                        unidadesDTO = null;
                    }
                });
        TVReporteActivityFecha.setOnClickListener(v -> {
            IBReporteFechaActivityFecha.callOnClick();
        });
        btnReporteactivityAceptar.setOnClickListener(v -> {
            VermificarCampos();
            Intent intent = new Intent(ReporteActivity.this,
                    VerReporteActivity.class);
        });
    }

    @Override
    public void VermificarCampos() {
        Log.d("FerChido", "VermificarCampos");
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if (fecha == null) {
            error = true;
            mensajes.add("La fecha a obtener el reporte es requerido");
        }
        if (SReporteActivityListUnidades.getSelectedItemPosition() < 0) {
            error = true;
            mensajes.add("La unidad es un valor requerido");
        }
        if (error)
            MensajeError(mensajes);
        else
            presenter.Reporte(unidadesDTO.getIdAlmacenGas(), txt_date, session.getToken());
        new HiloReporte().execute();
    }

        @Override
    public void MensajeError(ArrayList<String> mensaje) {
            android.support.v7.app.AlertDialog.Builder builder = new android.support.v7.app.AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(R.string.error_titulo);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
            for (String men:mensaje){
                stringBuilder.append(men).append("\n");
            }
            builder.setMessage(stringBuilder);
            builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create().show();
    }





    @Override
    public void onSuccessGetUnidades(DatosReporteDTO data) {
        unidadesDTOS = data;
        String[] list_unidades = new String[unidadesDTOS.getAlmacenes().size()];
        for (int x = 0; x < unidadesDTOS.getAlmacenes().size(); x++) {
            list_unidades[x] = data.getAlmacenes().get(x).getNombreAlmacen();
        }
        SReporteActivityListUnidades.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_unidades
        ));
    }

    @Override
    public void onErrorMessage(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(ReporteActivity.this, R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(ReporteActivity.this, R.style.AlertDialog);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setCancelable(false);
        progressDialog.show();
    }

    @Override
    public void hiddeProgress() {
        if (progressDialog != null && progressDialog.isShowing()) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    public void setFecha() {
        if (fecha == null) {
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
        txt_date = mYear + "-" + (mMonth + 1) + "-" + mDay;
    }

    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case 0:
                DatePickerDialog dialog = new DatePickerDialog(this,
                        R.style.datepicker,
                        onDateSetListener,
                        mYear, mMonth, mDay);
                dialog.getDatePicker().setMaxDate(System.currentTimeMillis());
                return dialog;
        }
        return null;
    }


    @Override
    public void onSuccessReport(ReporteDto reporteDTO) {
        this.reporteDTO = reporteDTO;
        if (reporteDTO.isExito()) {
            if (!reporteDTO.isEsCamioneta()) {
                Log.d("ReporteDTO", reporteDTO.toString());
                String formato_reporte_pipa_text = "Reporte \t\n[{Elemento}] \n" +
                        "\n" +
                        "Clave Reporte: \n[{ClaveReporte}] \n" +
                        "Fecha:[{Fecha}]" +
                        "\n" +
                        "-------------------------------\n" +
                        "Porcentajes(%)\n" +
                        "\n" +
                        "Salida: " +
                        "\tRegreso:\n" +

                        "[{Porcentaje-salida}] " +
                        "\t [{Porcentaje-regreso}]\n" +
                        "-------------------------------" +
                        "\nLectura medidor\n" +
                        "Inicial: " +
                        "\tFinal:\n" +
                        "[{Lectura-inicial}]" +
                        "\t [{Lectura-final}] \n" +
                        "-------------------------------\n" +
                        "Litros de venta: " +
                        "\t  [{litros-venta}] \n" +
                        "Precio día :" +
                        "\t $ [{Precio}]\n" +
                        "Importe contado:" +
                        "\t $ [{Importe-contado}] \n" +
                        "Importe credito:" +
                        "\t $ [{Importe-credito}]";

                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Elemento}]",
                        reporteDTO.getEstacion());
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{ClaveReporte}]",
                        reporteDTO.getClaveOperacion());

                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Fecha}]",
                        mDay + "/" + (mMonth + 1) + "/" + mYear);
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Porcentaje-salida}]",
                        String.valueOf(reporteDTO.getLecturaInicial().getPorcentajeMedidor()) + "%"
                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Porcentaje-regreso}]",
                        String.valueOf(reporteDTO.getLecturaFinal().getPorcentajeMedidor()) + "%"
                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Lectura-inicial}]",
                        String.valueOf(reporteDTO.getLecturaInicial()!=null?reporteDTO.getLecturaInicial().getCantidadP5000():"")
                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Lectura-final}]",
                        String.valueOf(reporteDTO.getLecturaFinal()!=null?reporteDTO.getLecturaFinal().getCantidadP5000():"")
                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{litros-venta}]",
                        String.valueOf(reporteDTO.getLitrosVenta())
                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Precio}]",
                        String.valueOf(reporteDTO.getPrecio())
                );

                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Importe-contado}] ",
                        String.valueOf(reporteDTO.getImporteContado())

                );
                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Importe-credito}]",
                        String.valueOf(reporteDTO.getImporteCredito())
                );

                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Bonificacion}]",
                        String.valueOf(reporteDTO.isBonificacion())
                );
                Log.d("bonificacionreporte",reporteDTO.isBonificacion()+"");

                formato_reporte_pipa_text = formato_reporte_pipa_text.replace(
                        "[{Importe-credito}]",
                        String.valueOf(reporteDTO.getImporteCredito())
                );
                Log.d("Reporte", "pre: "+ formato_reporte_pipa_text.replace("[{Importe-credito}]",String.valueOf(reporteDTO.getImporteCredito())));
               String formato_reporte_pipa_html = "<body>" +
                        "<h4>Reporte-"+ reporteDTO.getEstacion()+"</h4>" +
                        "<div>" +
                        "<p>Clave Reporte:"+reporteDTO.getClaveOperacion()+"</p>" +
                        "<p>Fecha:[{Fecha}]</p>" +
                        "</div>" +
                        "<hr>" +
                        "<h4>Porcentajes(%)</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Salida: </th>" +
                        "<th>Regreso: </th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Porcentaje-salida}]</td>" +
                        "<td>[{Porcentaje-regreso}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "<hr>" +
                        "<h4>Lectura medidor</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Inicial: </th>" +
                        "<th>Final:</th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Lectura-inicial}]</td>" +
                        "<td>[{Lectura-final}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "<hr>" +
                        "<div>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Litros de venta: </td>" +
                        "<td> [{litros-venta}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Precio día : </td>" +
                        "<td>$ [{Precio}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe contado: </td>" +
                        "<td>$ [{Importe-Contado}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe credito: </td>" +
                        "<td>$ [{Importe-credito}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "</body>";


                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Elemento}]",
                        reporteDTO.getEstacion());
                Log.d("Estacion", reporteDTO.getEstacion());
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{ClaveReporte}]",
                        reporteDTO.getClaveOperacion()
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Fecha}]",
                        mDay + "/" + (mMonth + 1) + "/" + mYear
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Porcentaje-salida}]",
                        String.valueOf(reporteDTO.getLecturaInicial().getPorcentajeMedidor()) + "%"
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Porcentaje-regreso}]",
                        String.valueOf(reporteDTO.getLecturaFinal().getPorcentajeMedidor()) + "%"
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Lectura-inicial}]",
                        String.valueOf(reporteDTO.getLecturaInicial()!=null?reporteDTO.getLecturaInicial().getCantidadP5000():"")
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Lectura-final}]",
                        String.valueOf(reporteDTO.getLecturaFinal()!=null?reporteDTO.getLecturaFinal().getCantidadP5000():"")
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{litros-venta}]",
                        String.valueOf(reporteDTO.getLitrosVenta())
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Precio}]",
                        String.valueOf(reporteDTO.getPrecio())
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Importe-Contado}]",
                        String.valueOf(reporteDTO.getImporteContado())
                );
                //Log.d("Reporte", "pre: "+ formato_reporte_pipa_html.replace("[{Importe-credito}]",String.valueOf(reporteDTO.getImporteCredito())));
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Importe-credito}]",
                        String.valueOf(reporteDTO.getImporteCredito())
                );
                formato_reporte_pipa_html = formato_reporte_pipa_html.replace(
                        "[{Bonificacion}]",
                        String.valueOf(reporteDTO.isBonificacion())
                );
                Log.d("bonificacionreporte",reporteDTO.isBonificacion()+"");
                Log.d("Reporte", "pre html: "+formato_reporte_pipa_html.replace("[{Elemento}]",
                        reporteDTO.getEstacion()));
                reporte_con_formato = new String[2];
                reporte_con_formato[0] = formato_reporte_pipa_text;
                reporte_con_formato[1] = formato_reporte_pipa_html;

                Log.d("Ali", "text: "+formato_reporte_pipa_text);
                Log.d("Ali", "html: "+formato_reporte_pipa_html);

                if (EsReporteDelDia) {
                    Log.d("Ali", "text: "+datos[0]);
                    Log.d("Ali", "html: "+datos[1]);
                    Intent intent = new Intent(ReporteActivity.this,
                            VerReporteActivity.class);
                    intent.putExtra("EsReporteDelDia", EsReporteDelDia);
                    intent.putExtra("FechaReporte", fecha);
                    intent.putExtra("unidadDTO", unidadesDTO);
                    intent.putExtra("StringReporte",formato_reporte_pipa_text );
                    intent.putExtra("HtmlReporte", formato_reporte_pipa_html);

                    startActivity(intent);
                }
            }
            else {
                int total_otras_ventas = 0;
                String formato_reporte_camioneta_text =
                        "Reporte: \t \n [{Elemento}]\n" +
                                "\n" +
                                "Clave Reporte: \n[{ClaveReporte}]\n" +
                                "Fecha:[{Fecha}]\n" +
                                "--------------------------------\n" +
                                "Tanques de: " +
                                "\tNormal:  " +
                                "\tVenta: \n" +

                                "[{Tanques}]\n" +

                                "--------------------------------\n" +
                                "Otras ventas\n" +

                                "[{Otras-ventas}]\n" +
                                "--------------------------------\n" +

                                "Carburacion: " +
                                "\t  [{Carburacion}]\n" +
                                "Kilos de venta: " +
                                "\t  [{Kilos-de-venta}]\n" +
                                "Precio día : " +
                                "\t $ [{Precio}]\n" +

                                "Otras ventas: " +
                                "\t$ [{Otras-ventas-total}]\n" +

                                "Importe contado: " +
                                "\t $ [{importe-contado}]\n" +

                                "Importe credito: " +
                                "\t $ [{importe-credito}]\n"

                        ;
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Elemento}]",
                        reporteDTO.getEstacion()
                );
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{ClaveReporte}]",
                        reporteDTO.getClaveOperacion()
                );
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Fecha}]",
                        mDay + "/" + (mMonth + 1) + "/" + mYear);

                Log.d("tanques", reporteDTO.getTanques()+"");
                Log.d("Reportedto",reporteDTO.getTanques().size()+"");
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Fecha}]",
                        mDay + "/" + (mMonth + 1) + "/" + mYear);

                tanquestxt = "";
                for (ReporteDto.TanquesDto tanqueDto :   reporteDTO.getTanques()) {
                    tanquestxt +=
                                tanqueDto.getTanques() + "          " +
                                    tanqueDto.getNormal() + "           "  +
                                    tanqueDto.getVenta() + "\n" ;

                }
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Tanques}]", tanquestxt);



                for (ReporteDto.TanquesDto tanqueDto :
                        reporteDTO.getTanques()) {
                    formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                            "[{Otras-ventas}]",
                            tanqueDto.getNormal() + ""
                    );
                }

                for (ReporteDto.OtrasVentasDTO otraVentaDTO :
                        reporteDTO.getOtrasVentas()) {
                    formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                            "[{Otras-ventas}]",
                            otraVentaDTO.getProducto() +
                                    "\t" + otraVentaDTO.getCantidad() + "\n"

                    );
                    total_otras_ventas += otraVentaDTO.getCantidad();
                }
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Carburacion}]",
                        String.valueOf(reporteDTO.getCarburacion())
                );
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Kilos-de-venta}]",
                        String.valueOf(reporteDTO.getKilosVenta())
                );
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Precio}]",
                        String.valueOf(reporteDTO.getPrecio())
                );

                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Otras-ventas-total}]",
                        String.valueOf(total_otras_ventas)
                );

                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{importe-contado}]",
                        String.valueOf(reporteDTO.getImporteContado())
                );

                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{importe-credito}]",
                        String.valueOf(reporteDTO.getImporteCredito())
                );
                formato_reporte_camioneta_text = formato_reporte_camioneta_text.replace(
                        "[{Bonificacion}]",
                        String.valueOf(reporteDTO.isBonificacion())
                );

                String formato_reporte_camioneta_html = "<body>" +
                        "<h4>Reporte: [{Elemento}]</h4>" +
                        "<div>" +
                        "<p>Clave Reporte:[{ClaveReporte}]</p>" +
                        "<p>Fecha:[{Fecha}]</p>" +
                        "</div>" +
                        "<hr>" +
                        //"<h4>Porcentajes(%)</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Tanques de: </th>" +
                        "<th>Normal:</th>" +
                        "<th>Venta:</th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +

                        "[{Tanques}]" +

                        "</tbody>" +
                        "</table>" +

                        "<hr>" +
                        "<h4>Otras ventas</h4>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "[{Otras-ventas}]" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +

                        "</div>" +
                        "<hr>" +
                        "<div>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Carburación: </td>" +
                        "<td> [{Carburacion}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Kilos de venta: </td>" +
                        "<td> [{Kilos-de-venta}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Precio día : </td>" +
                        "<td>$ [{Precio}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Otras ventas: </td>" +
                        "<td>$ [{Otras-ventas}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe contado: </td>" +
                        "<td>$ [{importe-contado}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe credito: </td>" +
                        "<td>$ [{importe-credito}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "</body>";
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Bonificacion}]",
                        reporteDTO.isBonificacion() +""
                );
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{ClaveReporte}]",
                        reporteDTO.getClaveOperacion()
                );
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Fecha}]",
                        mDay + "/" + (mMonth + 1) + "/" + mYear);

                Log.d("tanquestabla", reporteDTO.getTanques()+"");

                tanqueshtml = "";
                for (ReporteDto.TanquesDto tanqueDto :   reporteDTO.getTanques()) {
                    tanqueshtml +=
                            "<tr><td>" + tanqueDto.getTanques() + "</td>" +
                                    "<td>" + tanqueDto.getNormal() + "</td>" +
                                    "<td>" + tanqueDto.getVenta() + "</td></tr>" ;

                }
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Tanques}]", tanqueshtml);

                for (ReporteDto.TanquesDto tanqueDto :
                        reporteDTO.getTanques()) {
                    formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                            "[{Otras-ventas}]",
                            tanqueDto.getNormal() + ""
                    );
                }
                /*if(total_otras_ventas==0){
                    formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                            "[{Otras-ventas}]",
                            String.valueOf(reporteDTO.getOtrasVentas())

                    );
                }else{
                    for (ReporteDto.OtrasVentasDTO otraVentaDTO :
                            reporteDTO.getOtrasVentas()) {
                        formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                                "[{Otras-ventas}]",
                                "<td>" + otraVentaDTO.getProducto() + "</td>" +
                                        "<td>" + otraVentaDTO.getCantidad() + "</td>"
                        );
                    }
                }*/


                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Carburacion}]",
                        String.valueOf(reporteDTO.getCarburacion())
                );
               /* formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                                "[{Otras-ventas}]",
                                String.valueOf(reporteDTO.getImporteCredito())

                );*/
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Kilos-de-venta}]",
                        String.valueOf(reporteDTO.getKilosVenta())
                );
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Precio}]",
                        String.valueOf(reporteDTO.getPrecio())
                );

                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Otras-ventas-total}]",
                        String.valueOf(total_otras_ventas)
                );

                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{importe-contado}]",
                        String.valueOf(reporteDTO.getImporteContado())
                );

                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{importe-credito}]",
                        String.valueOf(reporteDTO.getImporteCredito())
                );
                formato_reporte_camioneta_html = formato_reporte_camioneta_html.replace(
                        "[{Bonificacion}]",
                        String.valueOf(reporteDTO.isBonificacion())
                );
                Log.d("bonificacionreporte",reporteDTO.isBonificacion()+"");
                reporte_con_formato = new String[2];
                reporte_con_formato[0] = formato_reporte_camioneta_text;
                reporte_con_formato[1] = formato_reporte_camioneta_html;

                if (EsReporteDelDia) {
                    Log.d("Ali", "text: " + datos[0]);
                    Log.d("Ali", "html: " + datos[1]);
                    Intent intent = new Intent(ReporteActivity.this,
                            VerReporteActivity.class);
                    intent.putExtra("EsReporteDelDia", EsReporteDelDia);
                    intent.putExtra("FechaReporte", fecha);
                    intent.putExtra("unidadDTO", unidadesDTO);
                    intent.putExtra("StringReporte", reporte_con_formato[0]);
                    intent.putExtra("HtmlReporte", reporte_con_formato[1]);

                    startActivity(intent);
                }
            }

            Log.d("esreportedia", EsReporteDelDia+"");

        }
    }

    @SuppressLint("StaticFieldLeak")
    public class HiloReporte extends AsyncTask<String, Integer, String[]> {
        @Override
        protected String[] doInBackground(String... strings) {
            String[] text = new String[2];
            Log.d("reportedeldia", EsReporteDelDia+"");
            if (EsReporteDelDia) {
                //presenter.Reporte(1, fecha.toString() ,session.getToken());
                String formato_reporte_pipa_text = "Reporte: \t \n[{Elemento}] \n" +
                        "\n" +
                        "Clave Reporte: \n[{ClaveReporte}] \n" +
                        "Fecha:[{Fecha}] " +
                        "\n" +
                        "-------------------------------\n" +
                        "Porcentajes(%)\n" +
                        "\n" +
                        "Salida: " +
                        "\tRegreso:\n" +

                        "[{Porcentaje-salida}] " +
                        "\t [{Porcentaje-regreso}]\n" +
                        "-------------------------------" +
                        "\nLectura medidor\n" +
                        "Inicial: " +
                        "\tFinal:\n" +
                        "[{Lectura-inicial}]" +
                        "\t [{Lectura-final}] \n" +
                        "-------------------------------\n" +
                        "Litros de venta: " +
                        "\t [{litros-venta}] \n" +
                        "Precio día :" +
                        "\t $ [{Precio}]\n" +
                        "Importe contado:" +
                        "\t $ [{Importe-contado}] \n" +
                        "Importe credito:" +
                        "\t $ [{Importe-credito}]";
                String formato_reporte_pipa_html = "<body>" +
                        "<h4>Reporte: [{Elemento}]</h4>" +
                        "<div>" +
                        "<p>Clave Reporte:[{ClaveReporte}]</p>" +
                        "<p>Fecha:[{Fecha}]</p>" +
                        "</div>" +
                        "<hr>" +
                        "<h4>Porcentajes(%)</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Salida: </th>" +
                        "<th>Regreso:</th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Porcentaje-salida}]</td>" +
                        "<td>[{Porcentaje-regreso}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "<hr>" +
                        "<h4>Lectura medidor</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Inicial: </th>" +
                        "<th>Final:</th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Lectura-inicial}]</td>" +
                        "<td>[{Lectura-final}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "<hr>" +
                        "<div>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Litros de venta: </td>" +
                        "<td> [{litros-venta}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Precio día : </td>" +
                        "<td>$ [{Precio}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe contado: </td>" +
                        "<td>$ [{Importe-contado}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe credito: </td>" +
                        "<td>$ [{Importe-credito}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "</body>";

                String formato_reporte_camioneta_text =
                        "Reporte: \t \n[{Elemento}]\n" +
                                "\n" +
                                "Clave Reporte: \n[{ClaveReporte}]\n" +
                                "Fecha:[{Fecha}]\n" +
                                "---------------------------------\n" +
                                "Porcentajes(%)\n" +
                                "<div>" +
                                "<table>" +
                                "<theader>" +
                                "<tr>" +
                                "Tanques de: " +
                                "\tNormal:" +
                                "\tVenta:\n" +

                                "[{Tanque-de}]" +
                                "\t[{Normal}]" +
                                "\t[{Venta}]\n" +

                                "---------------------------------\n" +
                                "Otras ventas\n" +

                                "[{Tipo}]\t" +
                                "[{Cantidad}]\n" +
                                "---------------------------------\n" +

                                "Carburación: " +
                                "\t  [{Carburacion}]\n" +
                                "Kilos de venta: " +
                                "\t  [{Kilos-de-venta}]" +
                                "Precio día : " +
                                "\t $ [{Precio}]\n" +

                                "Otras ventas: " +
                                "\t$ [{Otras-ventas}]\n" +

                                "Importe contado: " +
                                "\t $ [{importe-contado}]\n" +

                                "Importe credito: " +
                                "\t $ [{importe-credito}]\n";

                String formato_reporte_camioneta_html = "<body>" +
                        "<h4>Reporte-[{Elemento}]</h4>" +
                        "<div>" +
                        "<p>Clave Reporte: [{ClaveReporte}]</p>" +
                        "<p>Fecha:[{Fecha}]</p>" +
                        "</div>" +
                        "<hr>" +
                        "<h4>Porcentajes(%)</h4>" +
                        "<div>" +
                        "<table>" +
                        "<theader>" +
                        "<tr>" +
                        "<th>Tanques de: </th>" +
                        "<th>Normal:</th>" +
                        "<th>Venta:</th>" +
                        "</tr>" +
                        "</theader>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Tanque-de}]</td>" +
                        "<td>[{Normal}]</td>" +
                        "<td>[{Venta}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +

                        "<hr>" +
                        "<h4>Otras ventas</h4>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>[{Tipo}]</td>" +
                        "<td>[{Cantidad}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +

                        "</div>" +
                        "<hr>" +
                        "<div>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Carburación: </td>" +
                        "<td> [{Carburacion}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Kilos de venta: </td>" +
                        "<td> [{Kilos-de-venta}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Precio día : </td>" +
                        "<td>$ [{Precio}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Otras ventas: </td>" +
                        "<td>$ [{Otras-ventas}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe contado: </td>" +
                        "<td>$ [{importe-contado}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Importe credito: </td>" +
                        "<td>$ [{importe-credito}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +
                        "</body>";
                text[0] = formato_reporte_pipa_text;
                text[1] = formato_reporte_pipa_html;

            }
            return text;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            //onShowProgress();
        }

        @Override
        protected void onPostExecute(String data[]) {
            super.onPostExecute(data);
            //hiddeProgress();
            datos = data;
        }

        @Override
        protected void onProgressUpdate(Integer... values) {
            super.onProgressUpdate(values);
            Log.w("Progress", Arrays.toString(values));
        }

    }


    @Override
    public void onErrorMessage(ReporteDto reporteDTO) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(reporteDTO.getMensajesError());
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
            dialogInterface.dismiss();
        });
    }
}
