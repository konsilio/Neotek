package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.print.PrinterInfo;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.util.Printer;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.Toast;

import com.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.CorteDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Util.Session;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.text.DateFormat;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.Set;
import java.util.UUID;

public class VerReporteActivity extends AppCompatActivity {
    private boolean EsReporteDelDia;
    private boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    public boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal,EsPasoIniciaLPipa;
    boolean  EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    boolean EsAnticipo,EsCorte;
    RecargaDTO recargaDTO;
    TraspasoDTO traspasoDTO;
    VentaDTO ventaDTO;
    AnticiposDTO anticiposDTO;
    CorteDTO corteDTO;
    private String StringReporte,HtmlReporte;

    // android built in classes for bluetooth operations
    BluetoothAdapter mBluetoothAdapter;
    BluetoothSocket mmSocket;
    BluetoothDevice mmDevice;

    OutputStream mmOutputStream;
    InputStream mmInputStream;
    Thread workerThread;

    byte[] readBuffer;
    int readBufferPosition;
    volatile boolean stopWorker;
    String device_select;
    NumberFormat format;
    Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ver_reporte);
        Bundle bundle = getIntent().getExtras();
        session = new Session(this);
        if(bundle!=null){
            EsReporteDelDia = bundle.getBoolean("EsReporteDelDia",false);
            EsRecargaEstacionInicial = bundle.getBoolean("EsRecargaEstacionInicial",
                    false);
            EsRecargaEstacionFinal = bundle.getBoolean("EsRecargaEstacionFinal",
                    false);
            EsPrimeraLectura = bundle.getBoolean("EsPrimeraLectura",false);
            EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);
            /*EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);*/
            EsTraspasoEstacionFinal = bundle.getBoolean("EsTraspasoEstacionFinal",
                    false);
            EsTraspasoPipaInicial = bundle.getBoolean("EsTraspasoPipaInicial",false);
            EsTraspasoPipaFinal = bundle.getBoolean("EsTraspasoPipaFinal",false);
            EsPasoIniciaLPipa = bundle.getBoolean("EsPasoIniciaLPipa",false);
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);

            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);
            //Log.d("CorteDTO", corteDTO.toString());

            if(EsReporteDelDia) {

                Log.d("Ali", "text: "+ (String) bundle.get("StringReporte"));
                Log.d("Ali", "html: "+ (String) bundle.get("HtmlReporte"));
                StringReporte = (String) bundle.get("StringReporte");
                HtmlReporte = (String) bundle.get("HtmlReporte");
            }
            if(EsRecargaEstacionFinal){
                recargaDTO = (RecargaDTO) bundle.getSerializable("recargaDTO");
                GenerarReporteRecargaFinal(recargaDTO);
            }
            if(EsTraspasoEstacionInicial ||EsTraspasoEstacionFinal){
                traspasoDTO = (TraspasoDTO) bundle.getSerializable("traspasoDTO");
                @SuppressLint("SimpleDateFormat") DateFormat s =
                        new SimpleDateFormat("ddMMyyyyhhmmssS");
                String clave_unica = "TE";
                clave_unica += (EsTraspasoEstacionFinal)? "F":"I";
                clave_unica += s.format(new Date());
                traspasoDTO.setClaveOperacion(clave_unica);

                @SuppressLint("SimpleDateFormat") SimpleDateFormat sf =
                        new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss",Locale.getDefault());
                Date fecha = new Date();
                traspasoDTO.setFechaAplicacion(sf.format(fecha));
                traspasoDTO.setFecha(sf.format(fecha));
                GenerarReporteTraspaso(traspasoDTO);
            }
            if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
                traspasoDTO = (TraspasoDTO) bundle.getSerializable("traspasoDTO");
                @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                        new SimpleDateFormat("ddMMyyyyhhmmssS");
                String clave_unica = "TP";
                clave_unica += (EsTraspasoPipaFinal)? "F":"I";
                clave_unica += s.format(new Date());
                traspasoDTO.setClaveOperacion(clave_unica);
                @SuppressLint("SimpleDateFormat") DateFormat sf =
                        new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss",Locale.getDefault());
                Date fecha = new Date();

                traspasoDTO.setFecha(sf.format(fecha));
                traspasoDTO.setFechaAplicacion(sf.format(fecha));
                GenerarReporteTraspasoPipa(traspasoDTO);
            }
            if(EsAnticipo){
                anticiposDTO = (AnticiposDTO) bundle.getSerializable("anticiposDTO");

                setTitle("Nota Anticipo");
                GenerarReporteAnticipo();
            }else if (EsCorte){
                setTitle("Nota de corte de caja");
                corteDTO = (CorteDTO) bundle.getSerializable("corteDTO");
                GenerarReporteCorteCaja();
            }
            if(EsVentaCamioneta || EsVentaCarburacion || EsVentaPipa){
                //if(EsVentaCamioneta) {
                setTitle("Nota de venta");
                ventaDTO = (VentaDTO) bundle.getSerializable("ventaDTO");
                GenerarReporte(ventaDTO);
                //}
            }

        }
        WebView WVVerReporteActivityReporte = findViewById(R.id.WVVerReporteActivityReporte);
        Button btnVerReporteActivityTerminar= findViewById(R.id.BtnVerReporteActivityTerminar);
        Button btnReporteActivityImprimir = findViewById(R.id.BtnReporteActivityImprimir);
        btnVerReporteActivityTerminar.setOnClickListener(v -> {
            if(EsReporteDelDia) {
                Intent intent = new Intent(VerReporteActivity.this, MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }else if(EsRecargaEstacionFinal){
                Intent intent = new Intent(VerReporteActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsRecargaEstacionInicial", EsRecargaEstacionInicial);
                intent.putExtra("EsRecargaEstacionFinal", EsRecargaEstacionFinal);
                intent.putExtra("recargaDTO", recargaDTO);
                startActivity(intent);
            }else if(EsTraspasoEstacionInicial || EsTraspasoEstacionFinal){
                Intent intent = new Intent(VerReporteActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsTraspasoEstacionInicial", EsTraspasoEstacionInicial);
                intent.putExtra("EsTraspasoEstacionFinal", EsTraspasoEstacionFinal);
                intent.putExtra("EsPrimeraParteTraspaso", EsPrimeraParteTraspaso);
                intent.putExtra("traspasoDTO", traspasoDTO);
                startActivity(intent);
            }else if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
                Intent intent = new Intent(VerReporteActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("EsTraspasoPipaInicial", EsTraspasoPipaInicial);
                intent.putExtra("EsTraspasoPipaFinal", EsTraspasoPipaFinal);
                intent.putExtra("traspasoDTO", traspasoDTO);
                startActivity(intent);
            }else if(EsCorte || EsAnticipo){
                Intent intent = new Intent(VerReporteActivity.this,
                        MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }else if(EsVentaCamioneta || EsVentaCarburacion || EsVentaPipa){
                Intent intent = new Intent(VerReporteActivity.this,
                        MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            }
        });
        btnReporteActivityImprimir.setOnClickListener((View v) -> {
            listDevices();
            btnVerReporteActivityTerminar.setVisibility(View.VISIBLE);
            /*new Imprimir(this,this).starPrint(StringReporte);*/
        });
        WVVerReporteActivityReporte.setWebViewClient(new WebViewClient(){
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                return true;
            }

            @Override
            public void onPageFinished(WebView view, String url) {
            }
        });
        WVVerReporteActivityReporte.loadDataWithBaseURL(null, HtmlReporte,
                "text/HTML", "UTF-8", null);
    }

    private void GenerarReporte(VentaDTO ventaDTO) {
        HtmlReporte = "<body>" +
                "<h3><u>Nota de venta</u></h3>" +
                "<table>" +
                "<tr>" +
                "<td>Folio Venta: </td>" +
                "<td>[{Clave-venta}]</td>"+
                "</tr>"+
                "<tr>" +
                "<td>Fecha</td>"+
                "<td>[{Fecha}]</td>"+
                "</tr>"+
                "<tr>" +
                "<td>Hora</td>"+
                "<td>[{Hora}]</td>"+
                "</tr>";
        if(ventaDTO.getEstacion().trim().length()>0 || !ventaDTO.getEstacion().isEmpty()
                && !ventaDTO.getEstacion().equals("")) {
            HtmlReporte+="<tr>" +
                    "<td>Surtido</td>"+
                    "<td>[{Estacion}]</td>"+
                    "</tr>";
        }
        HtmlReporte+="</table>"+
                "<hr>";
        //region Busqueda por cliente
        if(ventaDTO.isEsBusqueda()) {
            if(ventaDTO.getRazonSocial().trim().length()>0){
                HtmlReporte += "<h3>Cliente</h3>" +
                        "<table>" +
                        "<tr>"+
                        "<td>No.Cliente</td>"+
                        "<td>[{No.Cliente}]</td>"+
                        "</tr>"+
                        "<tr>" +
                        "<td>Razon social</td>" +
                        "<td>[{Razon-social}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>RFC</td>" +
                        "<td>[{RFC}]</td>" +
                        "</tr>" +
                        "</table>";
            }else {
                HtmlReporte += "<h3>Cliente</h3>" +
                        "<table>" +
                        "<tr>"+
                        "<td>No.Cliente</td>"+
                        "<td>[{No.Cliente}]</td>"+
                        "</tr>"+
                        "<tr>" +
                        "<td>Cliente</td>" +
                        "<td>[{Cliente}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>RFC</td>" +
                        "<td>[{RFC}]</td>" +
                        "</tr>" +
                        "</table>";
            }
        }
        //endregion
        //region Registro
        if(ventaDTO.isEsRegistro()){
            if(ventaDTO.getRazonSocial().trim().length()<=0){
                HtmlReporte += "<h3>Cliente</h3>" +
                        "<table>" +
                        "<tr>" +
                        "<td>Razon social</td>" +
                        "<td>[{Razon-social}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>RFC</td>" +
                        "<td>[{RFC}]</td>" +
                        "</tr>" +
                        "</table>";
            }else{
                HtmlReporte += "<h3>Cliente</h3>" +
                        "<table>" +
                        "<tr>" +
                        "<td>Cliente</td>" +
                        "<td>[{Cliente}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>RFC</td>" +
                        "<td>[{RFC}]</td>" +
                        "</tr>" +
                        "</table>";
            }
        }
        //endregion
        HtmlReporte+="<table>" +
                "<tr>" +
                "<td>Concepto</td>"+
                "<td>Cant.</td>"+
                "<td>P.Uni.</td>"+
                "<td>Desc</td>"+
                "<td>Subt.</td>"+
                "</tr>"+
                "[{Concepto}]"+
                "</table>"+
                "<table>" +
                "<tr>" +
                "<td>I.V.A. (16%)</td>"+
                "<td>[{iva}]</td>"+
                "</tr>"+
                "<tr>"+
                "<td>Total</td>"+
                "<td>[{Total}]</td>"+
                "</tr>";
        if(!ventaDTO.isCredito()) {
            HtmlReporte += "<tr>" +
                    "<td>Efectivo recibido:</td>" +
                    "<td>[{Efectivo}]</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Cambio</td>" +
                    "<td>[{Cambio}]</td>" +
                    "</tr>";
        }
        HtmlReporte+="</table>"+
                "</body>";

        StringReporte = "\tTiket de venta\n" +
                "Gas Mundial de Guerrero\n\n"+
                "Tiket\t[{Clave-venta}]\n"+
                "Fecha\t[{Fecha}]\n"+
                "Hora\t[{Hora}]\n";
        if(ventaDTO.getEstacion().trim().length()>0 || !ventaDTO.getEstacion().isEmpty()
                && !ventaDTO.getEstacion().equals("")) {
            StringReporte+="Surtido: \t [{Estacion}]\n";
        }
        StringReporte+="_________________________\n";
        //region Busqueda por cliente
        if(ventaDTO.isEsBusqueda()) {
            if(ventaDTO.getRazonSocial().trim().length()>0){
                StringReporte += "\tCliente\n" +
                        "No.Cliente: \t[{No.Cliente}]\n"+
                        "Razon Social \t[{Razon-social}]\n" +
                        "RFC \t[{RFC}]\n";
            }else {
                StringReporte += "\tCliente\n" +
                        "No.Cliente: \t[{No.Cliente}]\n"+
                        "Cliente \t[{Cliente}]\n" +
                        "RFC \t[{RFC}]\n";
            }
        }
        //endregion
        //region Registro
        if(ventaDTO.isEsRegistro()){
            if(ventaDTO.getRazonSocial().trim().length()<=0){
                StringReporte += "\tCliente\n" +
                        "No.Cliente: \t[{No.Cliente}]\n"+
                        "Razon Social \t[{Razon-social}]\n" +
                        "RFC \t[{RFC}]\n";
            }else{
                StringReporte += "\tCliente\n" +
                        "No.Cliente: \t[{No.Cliente}]\n"+
                        "Cliente \t[{Cliente}]\n" +
                        "RFC \t[{RFC}]\n";
            }
        }
        //endregion
        StringReporte +="--------------------------------\n"+
                "|Concepto|Cant.|P.Uni.|Desc|Subt|\n"+
                "--------------------------------\n"+
                "[{Concepto}]\n"+
                "________________________________\n"+
                "\tI.V.A. (16%) [{iva}]\n"+
                "\tTotal [{Total}]\n";
        if(!ventaDTO.isCredito()) {
            StringReporte += "\tEfectivo recibido: [{Efectivo}]\n" +
                    "\tCambio [{Cambio}]\n";

            StringReporte += "\tVenta Contado\n";
        }else{
            StringReporte += "\tEfectivo recibido: [{Efectivo}]\n" +
                    "\tCambio [{Cambio}]\n";

            StringReporte += "\tVenta Credito\n";
        }
        if(ventaDTO.isBonificacion()) {
            StringReporte += "\t Bonificación \n";
        }

        StringReporte += "Le atendio [{Usuario}]"+
                "\n--------------------------------\n"+
                "Gas Mundial de Guerrero S.A de C.V.\n"+
                "Av. Principal No. 5477 C.P. 56789\n"+
                "www.gasmundialdeguerrero.com.mx\n\n"+

                "Facturacion electronica en :\n"+
                "www.gasmundialdeguerrero.com.mx/\n"+
                "facturacion\n\n"+
                "Folio Factura: [{Folio-factura}]\n\n"+
                "Gracias por su confianza,vuelva\n" +
                "pronto!"
        ;
        StringReporte = StringReporte.replace("[{Clave-venta}]",ventaDTO.getFolioVenta());
        HtmlReporte = HtmlReporte.replace("[{Clave-venta}]",ventaDTO.getFolioVenta());
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        @SuppressLint("SimpleDateFormat") SimpleDateFormat tdate =
                new SimpleDateFormat("HH:mm");
        Date registro = new Date();
        StringReporte = StringReporte.replace("[{Fecha}]",fdate.format(registro));
        HtmlReporte = HtmlReporte.replace("[{Fecha}]",fdate.format(registro));
        StringReporte = StringReporte.replace("[{Hora}]",tdate.format(registro));
        HtmlReporte = HtmlReporte.replace("[{Hora}]",tdate.format(registro));
        if(ventaDTO.getEstacion().trim().length()>0 || !ventaDTO.getEstacion().isEmpty() &&
                !ventaDTO.getEstacion().equals("")) {
            StringReporte = StringReporte.replace("[{Estacion}]", ventaDTO.getEstacion());
            HtmlReporte = HtmlReporte.replace("[{Estacion}]", ventaDTO.getEstacion());
        }
        if(ventaDTO.getRazonSocial().isEmpty()||ventaDTO.getRazonSocial()!=null) {
            StringReporte = StringReporte.replace("[{Razon-social}]", ventaDTO.getRazonSocial());
            HtmlReporte = HtmlReporte.replace("[{Razon-social}]", ventaDTO.getRazonSocial());
        }else {
            if (ventaDTO.isSinNumero()){
                StringReporte = StringReporte.replace("Razon social", "Cliente");
                HtmlReporte = HtmlReporte.replace("Razon social", "Cliente");
                StringReporte = StringReporte.replace("[{Razon-social}]", ventaDTO.getNombre());
                HtmlReporte = HtmlReporte.replace("[{Razon-social}]", ventaDTO.getNombre());
            }
        }
        if (!ventaDTO.isSinNumero()) {
            StringReporte = StringReporte.replace("[{RFC}]", ventaDTO.getRFC());
            HtmlReporte = HtmlReporte.replace("[{RFC}]", ventaDTO.getRFC());
        }

        StringBuilder Conpecto = new StringBuilder();
        StringBuilder ConpectoHtml = new StringBuilder();
        NumberFormat dformat = new DecimalFormat("#.00");
        for (ConceptoDTO conceptoDTO: ventaDTO.getConcepto()){
            Conpecto.append("|").append(conceptoDTO.getConcepto())
                    .append("|").append(String.valueOf(conceptoDTO.getCantidad()))
                    .append("|$ ").append(String.valueOf(dformat.format(conceptoDTO.getPUnitario())))
                    .append("|$ ").append(String.valueOf(dformat.format(conceptoDTO.getDescuento())))
                    .append("|$ ").append(String.valueOf(dformat.format(conceptoDTO.getSubtotal()))).append("|\n");
                     Log.d("desc", conceptoDTO.getDescuento()+"");

            ConpectoHtml.append("<tr><td>").append(conceptoDTO.getConcepto())
                    .append("</td><td>").append(String.valueOf(conceptoDTO.getCantidad()))
                    .append("</td><td>$ ").append(String.valueOf(dformat.format(conceptoDTO.getPUnitario())))
                    .append("</td><td>$ ").append(String.valueOf(dformat.format(conceptoDTO.getDescuento())))
                    .append("</td><td>$ ").append(String.valueOf(dformat.format(conceptoDTO.getSubtotal())))
                    .append("</tr>");
            Log.d("desc", conceptoDTO.getDescuento()+"");
        }
        StringReporte = StringReporte.replace("[{Concepto}]",Conpecto);
        HtmlReporte = HtmlReporte.replace("[{Concepto}]",ConpectoHtml);
        //region Busqueda por cliente
        if(ventaDTO.isEsBusqueda()) {
            if(ventaDTO.getRazonSocial().trim().length()>0){
                StringReporte = StringReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                HtmlReporte = HtmlReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                StringReporte = StringReporte.replace("[{Razon-social}]",
                        String.valueOf(ventaDTO.getRazonSocial()));
                HtmlReporte = HtmlReporte.replace("[{Razon-social}]",
                        String.valueOf(ventaDTO.getRazonSocial()));
                StringReporte = StringReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
                HtmlReporte = HtmlReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
            }else {
                StringReporte = StringReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                HtmlReporte = HtmlReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                StringReporte = StringReporte.replace("[{Cliente}]",
                        String.valueOf(ventaDTO.getNombre()));
                HtmlReporte = HtmlReporte.replace("[{Cliente}]",
                        String.valueOf(ventaDTO.getNombre()));
                StringReporte = StringReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
                HtmlReporte = HtmlReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
            }
        }
        //endregion
        //region Registro
        if(ventaDTO.isEsRegistro()){
            if(ventaDTO.getRazonSocial().trim().length()<=0){
                StringReporte = StringReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                HtmlReporte = HtmlReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                StringReporte = StringReporte.replace("[{Razon-social}]",
                        String.valueOf(ventaDTO.getRazonSocial()));
                HtmlReporte = HtmlReporte.replace("[{Razon-social}]",
                        String.valueOf(ventaDTO.getRazonSocial()));
                StringReporte = StringReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
                HtmlReporte = HtmlReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
            }else{
                StringReporte = StringReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                HtmlReporte = HtmlReporte.replace("[{No.Cliente}]",
                        String.valueOf(ventaDTO.getIdCliente()));
                StringReporte = StringReporte.replace("[{Cliente}]",
                        String.valueOf(ventaDTO.getNombre()));
                HtmlReporte = HtmlReporte.replace("[{Cliente}]",
                        String.valueOf(ventaDTO.getNombre()));
                StringReporte = StringReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
                HtmlReporte = HtmlReporte.replace("[{RFC}]",
                        String.valueOf(ventaDTO.getIva()));
            }
        }
        //endregion
        if(!ventaDTO.isEsSinNumero()){

            StringReporte = StringReporte.replace("[{Razon-social}]",
                    String.valueOf(ventaDTO.getIva()));
            HtmlReporte = HtmlReporte.replace("[{Razon-social}]",
                    String.valueOf(ventaDTO.getIva()));
            StringReporte = StringReporte.replace("[{RFC}]",
                    String.valueOf(ventaDTO.getIva()));
            HtmlReporte = HtmlReporte.replace("[{RFC}]",
                    String.valueOf(ventaDTO.getIva()));
        }
        if(!ventaDTO.isCredito()) {
            StringReporte = StringReporte.replace("[{Efectivo}]", "$"+String.valueOf(
                    dformat.format(ventaDTO.getEfectivo())));
            HtmlReporte = HtmlReporte.replace("[{Efectivo}]", "$"+String.valueOf(dformat.format(ventaDTO.getEfectivo())));
        }
        if(ventaDTO.isBonificacion()) {
            StringReporte = StringReporte.replace("[{Efectivo}]", "$"+String.valueOf(
                    dformat.format(ventaDTO.getEfectivo())));
            HtmlReporte = HtmlReporte.replace("[{Efectivo}]", "$"+String.valueOf(dformat.format(ventaDTO.getEfectivo())));
        }

        StringReporte = StringReporte.replace("[{iva}]","$"+String.valueOf(
                dformat.format(ventaDTO.getIva())));
        HtmlReporte = HtmlReporte.replace("[{iva}]","$"+String.valueOf(
                dformat.format(ventaDTO.getIva())));
        StringReporte = StringReporte.replace("[{Total}]","$"+String.valueOf(
                dformat.format(ventaDTO.getTotal())));
        HtmlReporte = HtmlReporte.replace("[{Total}]","$"+String.valueOf(
                dformat.format(ventaDTO.getTotal())));

        StringReporte = StringReporte.replace("[{Cambio}]","$"+String.valueOf(
                dformat.format(ventaDTO.getCambio())
        ));
        HtmlReporte = HtmlReporte.replace("[{Cambio}]","$"+String.valueOf(
                dformat.format(ventaDTO.getCambio())
        ));

        StringReporte = StringReporte.replace("[{Folio-factura}]",ventaDTO.getFolioVenta());
        HtmlReporte = HtmlReporte.replace("[{Folio-factura}]",ventaDTO.getFolioVenta());
        StringReporte = StringReporte.replace("[{Folio-factura}]",ventaDTO.getFolioVenta());
        HtmlReporte = HtmlReporte.replace("[{Folio-factura}]",ventaDTO.getFolioVenta());

        String nombre = session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE);

        StringReporte = StringReporte.replace("[{Usuario}]",
                nombre
                /*session.getAttribute(Session.KEY_NOMBRE)*/);
        HtmlReporte = HtmlReporte.replace("[{Usuario}]",
                nombre
                /*session.getAttribute(Session.KEY_NOMBRE)*/);
        ;
    }

    private void GenerarReporteCorteCaja() {
        HtmlReporte = "<body>" +
                "<h3 style='text-align: center;'><u>Reporte-Corte de caja</u></h3>" +
                "<table style='font-size:14px; width:100%; margin-left:5px;margin-rigth:5px;'>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Corte</td>" +
                "<td style='text-align: right; font-weight:bold;'>[{ClaveTraspaso}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha</td>" +
                "<td style='text-align: right; font-weight:bold;'>[{Fecha}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Hora</td>" +
                "<td style='text-align: right; font-weight:bold;'>[{Hora}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table >" +
                "<hr>" +
                "<table style='font-size:14px; width:100%;margin-left:5px;margin-rigth:5px;'>" +
                "<tbody>" +
                "<tr>" +
                "<td>Estación</td>"+
                "<td style='text-align: right; font-weight:bold;'>[{Estacion}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha de venta</td>"+
                "<td style='text-align: right; font-weight:bold;'>[{Fecha-venta}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Venta Total</td>"+
                "<td style='text-align: right; font-weight:bold;'>[{Venta-total}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Anticipos</td>"+
                "<td style='text-align: right; font-weight:bold;'>[{Anticipos}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Monto de corte</td>"+
                "<td style='text-align: right; font-weight:bold;'>[{Monto-corte}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table >"+
                "<hr>";
        if(!corteDTO.isCamioneta()) {
            HtmlReporte += "<h3 style='text-align: center;'>P5000</h3>" +
                    "<table style='font-size:14px; width:100%;margin-left:5px;margin-rigth:5px;'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td>Inicial: </td>" +
                    "<td style='text-align: right; font-weight:bold;'> [{Inicial}]</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Final: </td>" +
                    "<td style='text-align: right; font-weight:bold;'>[{Final}]</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Litros vendidos: </td>" +
                    "<td style='text-align: right; font-weight:bold;'>[{Litros-vendidos}]</td>" +
                    "</tr></tbody>"+
                    "</table>";
        }
        HtmlReporte +=
                "</body>";

        StringReporte =
                "Reporte-Corte de caja\n" +

                        "Clave Corte\t" +
                        "[{ClaveTraspaso}]\n" +
                        "Fecha \t" +
                        " [{Fecha}]\n" +
                        "Hora \t" +
                        "[{Hora}]\n" +
                        "----------------------\n" +
                        "Estacion \t"+
                        "[{Estacion}]\n" +
                        "Fecha de venta \t"+
                        "[{Fecha-venta}]\n" +
                        "Venta Total \t"+
                        "[{Venta-total}]\n" +
                        "Anticipos \t"+
                        "[{Anticipos}]\n" +
                        "Monto de corte \t"+
                        "[{Monto-corte}]\n" +
                        "-----------------------\n";
        if(!corteDTO.isCamioneta()) {
            StringReporte +="\tP5000" +
                    "Inicial: \t" +
                    "[{Inicial}]\n" +
                    "Final: \t" +
                    "[{Final}]\n" +
                    "Litros vendidos: \t" +
                    "[{Litros-vendidos}]\n" ;
        }
        StringReporte+="Entrega\n"+
                "[{Entrego-nombre}]\n\n" +
                "________________________\n"+
                "Firma\n\n"+
                "Recibe:\n"+
                "[{Recibio}]\n\n"+
                "________________________\n"+
                "Firma\n\n"
        ;
        /*HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",
                corteDTO.getClaveOperacion());
        StringReporte = StringReporte.replace("[{ClaveTraspaso}]",
                corteDTO.getClaveOperacion());*/
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sfdate =
                new SimpleDateFormat("dd/MM/yyyy");
        try{
            HtmlReporte= HtmlReporte.replace("[{Fecha}]",sfdate.format(new Date()));
            StringReporte = StringReporte.replace("[{Fecha}]",sfdate.format(new Date()));
        }catch (Exception e){
            e.printStackTrace();
        }


        HtmlReporte = HtmlReporte.replace("[{Hora}]",
                corteDTO.getHora());
        StringReporte = StringReporte.replace("[{Hora}]",
                corteDTO.getHora());

        HtmlReporte = HtmlReporte.replace("[{Estacion}]",
                corteDTO.getNombreEstacion());
        StringReporte = StringReporte.replace("[{Estacion}]",
                corteDTO.getNombreEstacion());
        try{
            Date fechaVenta = fdate.parse(corteDTO.getFechaVenta());
            HtmlReporte = HtmlReporte.replace("[{Fecha-venta}]",
                    sfdate.format(fechaVenta)
            );
            StringReporte = StringReporte.replace("[{Fecha-venta}]",
                    sfdate.format(fechaVenta)
            );
        }catch (Exception e){
            e.printStackTrace();
        }


        NumberFormat dformat = new DecimalFormat("#.00");
        HtmlReporte = HtmlReporte.replace("[{Venta-total}]",
                "$"+dformat.format(corteDTO.getTotal())
        );
        StringReporte = StringReporte.replace("[{Venta-total}]",
                "$"+dformat.format(corteDTO.getTotal())
        );

        HtmlReporte = HtmlReporte.replace("[{Anticipos}]",
                "$"+dformat.format(corteDTO.getTotalAnticipos())
        );
        StringReporte = StringReporte.replace("[{Anticipos}]",
                "$"+dformat.format(corteDTO.getTotalAnticipos())
        );

        HtmlReporte = HtmlReporte.replace("[{Monto-corte}]",
                "$"+dformat.format(corteDTO.getMontoCorte())
        );
        StringReporte = StringReporte.replace("[{Monto-corte}]",
                "$"+dformat.format(corteDTO.getMontoCorte())
        );
        if(!corteDTO.isCamioneta()) {
            StringReporte = StringReporte.replace("[{Inicial}]",
                    String.valueOf(corteDTO.getP5000Inicial())
            );

            HtmlReporte = HtmlReporte.replace("[{Inicial}]",
                    String.valueOf(corteDTO.getP5000Inicial())
            );

            StringReporte = StringReporte.replace("[{Final}]",
                    String.valueOf(corteDTO.getP5000Final())
            );

            HtmlReporte = HtmlReporte.replace("[{Final}]",
                    String.valueOf(corteDTO.getP5000Final())
            );

            StringReporte = StringReporte.replace("[{Litros-vendidos}]",
                    String.valueOf(corteDTO.getP5000Final())
            );

            HtmlReporte = HtmlReporte.replace("[{Litros-vendidos}]",
                    String.valueOf(corteDTO.getLitrosCorte())
            );
        }
        StringReporte = StringReporte.replace("[{Monto-corte}]",
                String.valueOf(corteDTO.getLitrosCorte())
        );

        HtmlReporte = HtmlReporte.replace("[{Entrego-nombre}]]",
                session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE));
        StringReporte = StringReporte.replace("[{Entrego-nombre}]",
                session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE));

        HtmlReporte = HtmlReporte.replace("[{Recibio}]]",
                corteDTO.getRecibe());
        StringReporte = StringReporte.replace("[{Recibio}]",
                corteDTO.getRecibe());
    }

    @SuppressLint("SimpleDateFormat")
    private void GenerarReporteAnticipo() {
        HtmlReporte = "<body>" +
                "<h2 style='text-align: center; font-weight: bold; font-size:20px;'><u>Reporte-Anticipo</u></h2>" +
                "<table width='100%'  style='margin:5px;'>" +
                "<tbody>" +
                "<tr>" +
                "<td style='width:20%;'>Clave Anticipo</td>" +
                "<td style='text-align: right; font-weight: bold;'>[{ClaveTraspaso}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha</td>" +
                "<td style='text-align: right; font-weight: bold;'>[{Fecha}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Hora</td>" +
                "<td style='text-align: right; font-weight: bold;'>[{Hora}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<table width='100%'  style='margin:5px;'>" +
                "<tbody>" +
                "<tr>" +
                "<td style='width:20%;'>Estación</td>"+
                "<td style='text-align: right; font-weight: bold;'>[{Estacion}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Monto anticipado: </td>" +
                "<td style='text-align: right; font-weight: bold;'>[{Monto-anticipo}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</body>";

        StringReporte =
                "\t Reporte-Anticipo \n" +
                        "Clave Anticipo \t" +
                        "[{ClaveTraspaso}]\n" +
                        "Fecha \t" +
                        " [{Fecha}]\n" +
                        "Hora \t" +
                        "[{Hora}]\n" +
                        "-----------------------------\n" +
                        "Estacion \t"+
                        "[{Estacion}]\n" +
                        "Monto anticipado: \t" +
                        "[{Monto-anticipo}]\n\n"+
                        "Entrega: \n"+
                        "[{Usuario-entrego}]\n____________________________\n"+
                        "Firma\n\n"+
                        "Recibe\n"+
                        "[{Usuario-recibi}]\n____________________________\n"+
                        "Firma\n\n";
        /*HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",anticiposDTO.getClaveOperacion());
        StringReporte = StringReporte.replace("[{ClaveTraspaso}]",anticiposDTO.getClaveOperacion());*/
        @SuppressLint("SimpleDateFormat") SimpleDateFormat formatter6 =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        try {
            Date fecha=formatter6.parse(anticiposDTO.getFecha());
            HtmlReporte = HtmlReporte.replace("[{Fecha}]",
                    new SimpleDateFormat("dd/MM/YYYY").format(fecha));
            StringReporte = StringReporte.replace("[{Fecha}]",
                    new SimpleDateFormat("dd/MM/YYYY").format(fecha));
        } catch (ParseException e) {
            e.printStackTrace();
        }

        HtmlReporte = HtmlReporte.replace("[{Hora}]",anticiposDTO.getHora());
        StringReporte = StringReporte.replace("[{Hora}]",anticiposDTO.getHora());
        HtmlReporte = HtmlReporte.replace("[{Estacion}]",anticiposDTO.getNombreEstacion());
        StringReporte = StringReporte.replace("[{Estacion}]",anticiposDTO.getNombreEstacion());
        format = NumberFormat.getCurrencyInstance();
        HtmlReporte = HtmlReporte.replace("[{Monto-anticipo}]","$"+String.valueOf(
                anticiposDTO.getAnticipar()));
        StringReporte = StringReporte.replace("[{Monto-anticipo}]","$"+String.valueOf(
                anticiposDTO.getAnticipar()));
        HtmlReporte = HtmlReporte.replace("[{Usuario-recibi}]]",
                session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE));
        StringReporte = StringReporte.replace("[{Usuario-recibi}]",
                session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE));
        HtmlReporte = HtmlReporte.replace("[{Usuario-entrego}]]",
                anticiposDTO.getNombreEntrega()==null?"":anticiposDTO.getNombreEntrega());
        StringReporte = StringReporte.replace("[{Usuario-entrego}]",
                anticiposDTO.getNombreEntrega()==null?"":anticiposDTO.getNombreEntrega());
    }

    private void GenerarReporteTraspasoPipa(TraspasoDTO traspasoDTO) {
        HtmlReporte = "<body>" +
                "<h3>Reporte-Traspaso-[{Pipa}]</h3>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Traspaso</td>" +
                "<td>[{ClaveTraspaso}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha</td>" +
                "<td>[{Fecha}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Hora</td>" +
                "<td>[{Hora}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<h4>Lectura P5000</h4>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>&nbsp;</td>"+
                "<td>Inicial: </td>" +
                "<td>Final</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{PipaNombre}]</td>" +
                "<td>[{P5000Inicial}]</td>" +
                "<td>[{P5000Final}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{PipaNombre2}]</td>" +
                "<td>[{P5000Inicial2}]</td>" +
                "<td>[{P5000Final2}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<tr>" +
                "<td>Litros traspasados: </td>" +
                "<td>[{LitrosTraspasados}]</td>" +
                "</tr>" +
                "</table>" +
                "</body>";

        StringReporte = "\n Rep-Traspaso - [{Pipa}] \n" +
                "\n Clave Traspaso" +
                "\n [{ClaveTraspaso}]" +
                "\n Fecha " +
                "\t [{Fecha}]" +
                "\n Hora" +
                "\t [{Hora}]\n" +
                "------------------------------------" +
                "\n Lectura P5000 " +
                "\n"+
                "\t Inicial: " +
                "\t Final " +
                "\n[{PipaNombre}] | "+
                "\t[{P5000Inicial}] | " +
                "\t[{P5000Final}] | " +
                "\n[{PipaNombre2}] | "+
                "\t[{P5000Inicial2}] | " +
                "\t[{P5000Final2}] | " +
                "\n------------------------------------" +
                "\nLitros traspasados: \t" +
                "[{LitrosTraspasados}]\n"+
                "\t\tFirma\n"+
                "[{NombrePipaTraspaso}](Traspase)--------\n"+
                "[{NombreUsuarioTraspaso}]\n"+
                "[{NombrePipaRecibi}](Recibi)\n"+
                "[{NombreUsuarioRecibi}]------------------\n";
        HtmlReporte = HtmlReporte.replace("[{Pipa}]",
                traspasoDTO.getNombreEstacionTraspaso());
        StringReporte = StringReporte.replace("[{Estacion}]",
                traspasoDTO.getNombreEstacionTraspaso());

        /*HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",traspasoDTO.
                getClaveOperacion());
        StringReporte = StringReporte.replace("[{ClaveTraspaso}]",traspasoDTO.
                getClaveOperacion());*/

        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        HtmlReporte = HtmlReporte.replace("[{Fecha}]",fdate.format(new Date()));
        StringReporte = StringReporte.replace("[{Fecha}]",fdate.format(new Date()));
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fhour=
                new SimpleDateFormat("hh:mm:ss a");
        HtmlReporte = HtmlReporte.replace("[{Hora}]",fhour.format(
                new Date()
        ));
        StringReporte = StringReporte.replace("[{Hora}]",fhour.format(
                new Date()
        ));
        HtmlReporte = HtmlReporte.replace("[{Pipa}]",
                traspasoDTO.getNombreEstacionTraspaso());
        StringReporte = StringReporte.replace("[{Pipa}]",
                traspasoDTO.getNombreEstacionTraspaso());

        HtmlReporte = HtmlReporte.replace("[{PipaNombre}]",
                traspasoDTO.getNombreEstacionTraspaso());
        StringReporte = StringReporte.replace("[{PipaNombre}]",
                traspasoDTO.getNombreEstacionTraspaso());

        HtmlReporte = HtmlReporte.replace("[{P5000Inicial}]",
                String.valueOf(traspasoDTO.getP5000SalidaInicial()));
        StringReporte = StringReporte.replace("[{P5000Inicial}]",
                String.valueOf(traspasoDTO.getP5000SalidaInicial()));

        HtmlReporte = HtmlReporte.replace("[{P5000Final}]",
                String.valueOf(traspasoDTO.getP5000Salida()));
        StringReporte = StringReporte.replace("[{P5000Final}]",
                String.valueOf(traspasoDTO.getP5000Salida()));

        HtmlReporte = HtmlReporte.replace("[{PipaNombre2}]",
                traspasoDTO.getNombreEstacionEntrada());
        StringReporte = StringReporte.replace("[{PipaNombre2}]",
                traspasoDTO.getNombreEstacionEntrada());

        HtmlReporte = HtmlReporte.replace("[{P5000Inicial2}]",
                String.valueOf(traspasoDTO
                        .getP5000EntradaInicial()));
        StringReporte = StringReporte.replace("[{P5000Inicial2}]",
                String.valueOf(
                        traspasoDTO.getP5000EntradaInicial()
                ));

        HtmlReporte = HtmlReporte.replace("[{P5000Final2}]",
                String.valueOf(
                        traspasoDTO.getP5000Entrada()
                )
        );
        StringReporte = StringReporte.replace("[{P5000Final2}]",
                String.valueOf(
                        traspasoDTO.getP5000Entrada()
                )
        );
        double traspasados = traspasoDTO.getP5000Salida() - traspasoDTO.getP5000Entrada();
        HtmlReporte = HtmlReporte.replace("[{LitrosTraspasados}]",
                String.valueOf(traspasados));
        StringReporte = StringReporte.replace("[{LitrosTraspasados}]",
                String.valueOf(traspasados));

        HtmlReporte = HtmlReporte.replace("[{NombrePipaTraspaso}]",
                traspasoDTO.getNombreEstacionTraspaso());
        StringReporte = StringReporte.replace("[{NombrePipaTraspaso}]",
                traspasoDTO.getNombreEstacionTraspaso());

        HtmlReporte = HtmlReporte.replace("[{NombreUsuarioTraspaso}]",
                session.getAttribute(Session.KEY_NOMBRE));
        StringReporte = StringReporte.replace("[{NombreUsuarioTraspaso}]",
                session.getAttribute(Session.KEY_NOMBRE));

        HtmlReporte = HtmlReporte.replace("[{NombrePipaRecibi}]",
                traspasoDTO.getNombreEstacionEntrada());
        StringReporte = StringReporte.replace("[{NombrePipaRecibi}]",
                traspasoDTO.getNombreEstacionEntrada());

        HtmlReporte = HtmlReporte.replace("[{NombreUsuarioRecivi}]",
                /*session.getAttribute(Session.KEY_NOMBRE)*/"");
        StringReporte = StringReporte.replace("[{NombreUsuarioRecibi}]",
                /*session.getAttribute(Session.KEY_NOMBRE)*/"");

    }

    /**
     * Permite realizar la asignación de los valores para el reporte de  la recarga final
     * de la estación ,toma de parametro el modelo para luego setear los valores
     * @param recargaDTO Objeto co
     */
    private void GenerarReporteRecargaFinal(RecargaDTO recargaDTO) {
        HtmlReporte = "<body>" +
                "<h3>Reporte-Recarga-[{Pipa}]</h3>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Recarga</td>" +
                //"<td>[{ClaveRecarga}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha</td>" +
                "<td>[{Fecha}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Hora</td>" +
                "<td>[{Hora}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<h4>Porcentaje Estación (%)</h4>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Inicial: </td>" +
                "<td>Final</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{PorcentajeInicial}]</td>" +
                "<td>[{PorcentajeFinal}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<h4>Lectura P5000</h4>" +
                "<table>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "<td>Inicial: </td>" +
                "<td>Final</td>" +
                "</tr>" +
                "<tr>" +
              // "<td>[{NombrePipa}]</td>" +
                "<td>[{LecturaInicialPipa}]</td>" +
                "<td>[{LecturaFinalPipa}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{NombreEstacion}]</td>" +
                "<td>[{LecturaIncialEstacion}]</td>" +
                "<td>[{LecturaFinalEstacion}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Litros recargados: </td>" +
                "<td>[{LitrosRecargados}]</td>" +
                "</tr>" +
                "</table>" +
                "</body>";

        StringReporte = "\n Reporte-Recarga - [{Pipa}] \n" +
                "\n Clave Recarga" +
               // "\t [{ClaveRecarga}]" +
                "\n Fecha " +
                "\t [{Fecha}]" +
                "\n Hora" +
                "\t [{Hora}]\n" +
                "------------------------------------" +
                "\n Porcentaje Estación (%) " +
                "\n Inicial: " +
                "\t Final</td>" +
                "\n[{PorcentajeInicial}]" +
                "\t[{PorcentajeFinal}]" +
                "------------------------------------" +
                "\n Lectura P5000" +
                "\n\t" +
                "Inicial:\t" +
                "Final\n" +
                //"[{NombrePipa}] \t" +
                "[{LecturaInicialPipa}] \t" +
                "[{LecturaFinalPipa}] \n" +
                "[{NombreEstacion}] \t" +
                "[{LecturaIncialEstacion}] \t" +
                "[{LecturaFinalEstacion}] \n" +
                "Litros recargados: \t" +
                "[{LitrosRecargados}]";

        StringReporte = StringReporte.replace("[{Pipa}]","");
        HtmlReporte = HtmlReporte.replace("[{Pipa}]","");

        /*StringReporte = StringReporte.replace("[{ClaveRecarga}]",recargaDTO
                .getClaveOperacion());

        HtmlReporte = HtmlReporte.replace("[{ClaveRecarga}]",recargaDTO
                .getClaveOperacion());*/

        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        StringReporte = StringReporte.replace("[{Fecha}]","");
        HtmlReporte = HtmlReporte.replace("[{Fecha}]","");

        StringReporte = StringReporte.replace("[{Hora}]","");
        HtmlReporte = HtmlReporte.replace("[{Hora}]","");

       /* StringReporte = StringReporte.replace("[{Hora}]",(recargaDTO
                .getClaveOperacion()));*/

       /* HtmlReporte = HtmlReporte.replace("[{Hora}]",(recargaDTO
                .getClaveOperacion()));*/
        StringReporte = StringReporte.replace("[{PorcentajeInicial}]",
                String.valueOf(recargaDTO.getProcentajeSalida()));
        HtmlReporte = HtmlReporte.replace("[{PorcentajeInicial}]",
                String.valueOf(recargaDTO.getProcentajeSalida()));

        StringReporte = StringReporte.replace("[{PorcentajeFinal}]",
                String.valueOf(recargaDTO.getProcentajeEntrada()));
        HtmlReporte = HtmlReporte.replace("[{PorcentajeFinal}]",
                String.valueOf(recargaDTO.getProcentajeEntrada()));

        StringReporte = StringReporte.replace("[{NombrePipa}]","");
        HtmlReporte = HtmlReporte.replace("[{PorcentajeFinal}]", "");

        StringReporte = StringReporte.replace("[{LecturaInicialPipa}]","");
        HtmlReporte = HtmlReporte.replace("[{LecturaFinalPipa}]", "");

        StringReporte = StringReporte.replace("[{LecturaInicialPipa}]","");
        HtmlReporte = HtmlReporte.replace("[{LecturaInicialPipa}]", "");

        StringReporte = StringReporte.replace("[{LecturaFinalPipa}]","");
        HtmlReporte = HtmlReporte.replace("[{LecturaFinalPipa}]", "");

        StringReporte = StringReporte.replace("[{NombreEstacion}]","");
        HtmlReporte = HtmlReporte.replace("[{NombreEstacion}]", "");

        StringReporte = StringReporte.replace("[{LecturaIncialEstacion}]","");
        HtmlReporte = HtmlReporte.replace("[{LecturaIncialEstacion}]", "");

        StringReporte = StringReporte.replace("[{LecturaFinalEstacion}]","");
        HtmlReporte = HtmlReporte.replace("[{LecturaFinalEstacion}]", "");

        StringReporte = StringReporte.replace("[{LitrosRecargados}]","");
        HtmlReporte = HtmlReporte.replace("[{LitrosRecargados}]", "");
    }

    private void GenerarReporteTraspaso(TraspasoDTO traspasoDTO){
        HtmlReporte = "<body>" +
                "<h3>Reporte-Traspaso-[{Estacion}]</h3>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Traspaso</td>" +
                "<td>[{ClaveTraspaso}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha</td>" +
                "<td>[{Fecha}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Hora</td>" +
                "<td>[{Hora}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<h4>Porcentaje Estación (%)</h4>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Inicial: </td>" +
                "<td>Final</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{PorcentajeInicial}]</td>" +
                "<td>[{PorcentajeFinal}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<hr>" +
                "<h4>Lectura P5000</h4>" +
                "<table>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "<td>Inicial: </td>" +
                "<td>Final</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>[{NombreEstacion}]</td>" +
                "<td>[{LecturaIncialEstacion}]</td>" +
                "<td>[{LecturaFinalEstacion}]</td>" +
                "</tr>" +

               // "<td>[{NombrePipa}]</td>" +
                "<td>[{LecturaInicialPipa}]</td>" +
                "<td>[{LecturaFinalPipa}]</td>" +
                "</tr>" +

                "<tr>" +
                "<td>Litros traspasados: </td>" +
                "<td>[{LitrosTraspasados}]</td>" +
                "</tr>" +
                "</table>" +
                "</body>";

        StringReporte = "\n Rep-Traspaso - [{Estacion}] \n" +
                "\n Clave Traspaso" +
                "\n[{ClaveTraspaso}]\n" +
                "\n Fecha " +
                "\t [{Fecha}]" +
                "\n Hora" +
                "\t [{Hora}]\n" +
                "--------------------------------" +
                "\n Porcentaje Estacion (%) " +
                "\n Inicial: " +
                "\t Final" +
                "\n| [{PorcentajeInicial}]" +
                "\t | [{PorcentajeFinal}] |" +
                "\n--------------------------------" +
                "\n Lectura P5000" +
                "\n\t  " +
                "Inicial:\t" +
                "Final\n" +

                "[{NombreEstacion}] |\t" +
                "[{LecturaIncialEstacion}] |\t" +
                "[{LecturaFinalEstacion}] |\n" +

               // "[{NombrePipa}] |\t" +
                "[{LecturaInicialPipa}] |\t" +
                "[{LecturaFinalPipa}] |\n" +
                "Litros traspasados: \t" +
                "[{LitrosTraspasados}]\n"+
                "\t\tFirma\n"+
                "[{NombrePipaTraspaso}](Traspase)\n________________________________\n"+
                "[{NombreUsuarioTraspaso}]\n"+
                "[{NombrePipaRecibi}](Recibí)\n"+
                "[{NombreUsuarioRecibi}]\n________________________________\n";

        StringReporte = StringReporte.replace("[{Estacion}]",
                traspasoDTO.getNombreEstacionTraspaso());
        HtmlReporte = HtmlReporte.replace("[{Estacion}]",
                traspasoDTO.getNombreEstacionTraspaso());

        /*StringReporte = StringReporte.replace("[{ClaveTraspaso}]",traspasoDTO
                .getClaveOperacion());
        HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",traspasoDTO
                .getClaveOperacion());*/

        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        StringReporte = StringReporte.replace("[{Fecha}]",fdate.format(new Date()));
        HtmlReporte = HtmlReporte.replace("[{Fecha}]",fdate.format(new Date()));
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fhour=
                new SimpleDateFormat("hh:mm:ss a");
        StringReporte = StringReporte.replace("[{Hora}]",fhour.format(new Date()));
        HtmlReporte = HtmlReporte.replace("[{Hora}]",fhour.format(new Date()));

        StringReporte = StringReporte.replace("[{PorcentajeInicial}]",
                String.valueOf(traspasoDTO.getPorcentajeInicial())+" %");
        HtmlReporte = HtmlReporte.replace("[{PorcentajeInicial}]",
                String.valueOf(traspasoDTO.getPorcentajeInicial())+" %");

        StringReporte = StringReporte.replace("[{PorcentajeFinal}]",
                String.valueOf(traspasoDTO.getPorcentajeSalida())+" %");
        HtmlReporte = HtmlReporte.replace("[{PorcentajeFinal}]",
                String.valueOf(traspasoDTO.getPorcentajeSalida())+" %");

        StringReporte = StringReporte.replace("[{NombreEstacion}]",
                traspasoDTO.getNombreEstacionTraspaso());
        HtmlReporte = HtmlReporte.replace("[{NombreEstacion}]",
                traspasoDTO.getNombreEstacionTraspaso());

        StringReporte = StringReporte.replace("[{LecturaIncialEstacion}]",
                String.valueOf(traspasoDTO.getP5000SalidaInicial()));
        HtmlReporte = HtmlReporte.replace("[{LecturaIncialEstacion}]",
                String.valueOf(traspasoDTO.getP5000SalidaInicial()));

        StringReporte = StringReporte.replace("[{LecturaFinalEstacion}]",
                String.valueOf(traspasoDTO.getP5000Salida()));
        HtmlReporte = HtmlReporte.replace("[{LecturaFinalEstacion}]",
                String.valueOf(traspasoDTO.getP5000Salida()));

        /*StringReporte = StringReporte.replace("[{NombrePipa}]",
                traspasoDTO.getNombreEstacionTraspaso());

        HtmlReporte = HtmlReporte.replace("[{NombrePipa}]",
                traspasoDTO.getNombreEstacionTraspaso());*/

        StringReporte = StringReporte.replace("[{LecturaInicialPipa}]",
                String.valueOf(traspasoDTO.getP5000EntradaInicial()));
        HtmlReporte = HtmlReporte.replace("[{LecturaInicialPipa}]",
                String.valueOf(traspasoDTO.getP5000EntradaInicial()));

        StringReporte = StringReporte.replace("[{LecturaFinalPipa}]",
                String.valueOf(traspasoDTO.getP5000Entrada()));
        HtmlReporte = HtmlReporte.replace("[{LecturaFinalPipa}]",
                String.valueOf(traspasoDTO.getP5000Entrada()));

        double traspasados = traspasoDTO.getP5000Salida() - traspasoDTO.getP5000Entrada();
        StringReporte = StringReporte.replace("[{LitrosTraspasados}]",
                String.valueOf(traspasados));
        HtmlReporte = HtmlReporte.replace("[{LitrosTraspasados}]",
                String.valueOf(traspasados));

        StringReporte = StringReporte.replace("[{NombrePipaTraspaso}]",
                traspasoDTO.getNombreEstacionTraspaso());
        HtmlReporte = HtmlReporte.replace("[{NombrePipaTraspaso}]",
                traspasoDTO.getNombreEstacionTraspaso());

        StringReporte = StringReporte.replace("[{NombreUsuarioTraspaso}]",
                session.getAttribute(Session.KEY_NOMBRE));
        HtmlReporte = HtmlReporte.replace("[{NombreUsuarioTraspaso}]",
                session.getAttribute(Session.KEY_NOMBRE));

        StringReporte = StringReporte.replace("[{NombrePipaRecibi}]",
                traspasoDTO.getNombreEstacionEntrada());
        HtmlReporte = HtmlReporte.replace("[{NombrePipaRecibi}]",
                traspasoDTO.getNombreEstacionEntrada());

        StringReporte = StringReporte.replace("[{NombreUsuarioRecivi}]",
                "");
        HtmlReporte = HtmlReporte.replace("[{NombreUsuarioRecibi}]",
                "");
    }

    void listDevices(){
        try {
            mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

            if (mBluetoothAdapter == null) {
                Toast.makeText(this, "El dispositivo no cuenta con Bluetooth", Toast.LENGTH_SHORT).show();
                //myLabel.setText("No bluetooth adapter available");
            }

            if (!mBluetoothAdapter.isEnabled()) {
                Intent enableBluetooth = new Intent(
                        BluetoothAdapter.ACTION_REQUEST_ENABLE);
                startActivityForResult(enableBluetooth, 0);
            }

            Set<BluetoothDevice> pairedDevices = mBluetoothAdapter
                    .getBondedDevices();
            if (pairedDevices.size() > 0) {
                final CharSequence[] lista = new CharSequence[pairedDevices.size()];
                ArrayList<String> adevices = new ArrayList<>();
                for (BluetoothDevice device : pairedDevices){
                    adevices.add(device.getName());
                }
                if(adevices.size()<=0){
                    AlertDialog.Builder builder= new AlertDialog.Builder(this,
                            R.style.AlertDialog);
                    builder.setTitle(R.string.error_titulo);
                    builder.setMessage("No se ha podido encontra ninguna impresora, verique:\n"+
                            " - La impreso este encendida\n - Que la impresora este vinculada al dispositivo");
                }
                for (int x = 0;x<adevices.size();x++){
                    lista[x] = adevices.get(x);
                }
                AlertDialog.Builder builder = new AlertDialog.Builder(this,
                        R.style.AlertDialog);
                builder.setTitle("Seleccione un dispositivo de la lista");
                builder.setItems(lista,(dialog, which) -> {
                    device_select = lista[which].toString();
                    Log.w("Dispositivo",device_select);
                    try {
                        findBT(lista[which]);
                        openBT();
                        sendData();
                        closeBT();

                    } catch (IOException ex) {
                        ex.fillInStackTrace();
                    }
                });
                builder.setNegativeButton(R.string.message_cancel,((dialog, which) -> {
                    dialog.dismiss();
                }));
                builder.create();
                builder.show();
            }
            Log.w("Activo","Bluetooth Device Found");
            //myLabel.setText("Bluetooth Device Found");
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // This will find a bluetooth printer device
    void findBT(CharSequence charSequence) {

        try {
            mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

            if (mBluetoothAdapter == null) {
                Toast.makeText(this, "El dispositivo no cuenta con Bluetooth",
                        Toast.LENGTH_SHORT).show();
                //myLabel.setText("No bluetooth adapter available");
            }

            if (!mBluetoothAdapter.isEnabled()) {
                Intent enableBluetooth = new Intent(
                        BluetoothAdapter.ACTION_REQUEST_ENABLE);
                startActivityForResult(enableBluetooth, 0);
            }

            Set<BluetoothDevice> pairedDevices = mBluetoothAdapter
                    .getBondedDevices();
            if (pairedDevices.size() > 0) {
                for (BluetoothDevice device : pairedDevices) {

                    // MP300 is the name of the bluetooth printer device
                    //if (device.getName().equals("EC MP-2")) {
                    if (device.getName().equals(charSequence)) {
                        mmDevice = device;
                        break;
                    }
                }
            }
            //myLabel.setText("Bluetooth Device Found");
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // Tries to open a connection to the bluetooth printer device
    void openBT() throws IOException {
        try {
            // Standard SerialPortService ID
            UUID uuid = UUID.fromString("00001101-0000-1000-8000-00805f9b34fb");
            mmSocket = mmDevice.createRfcommSocketToServiceRecord(uuid);
            mmSocket.connect();
            mmOutputStream = mmSocket.getOutputStream();
            mmInputStream = mmSocket.getInputStream();

            beginListenForData();
            Log.w("Activo","Bluetooth Opened");
            //myLabel.setText("Bluetooth Opened");
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // After opening a connection to bluetooth printer device,
    // we have to listen and check if a data were sent to be printed.
    void beginListenForData() {
        try {
            final Handler handler = new Handler();

            // This is the ASCII code for a newline character
            final byte delimiter = 10;

            stopWorker = false;
            readBufferPosition = 0;
            readBuffer = new byte[1024];

            workerThread = new Thread(new Runnable() {
                public void run() {
                    while (!Thread.currentThread().isInterrupted()
                            && !stopWorker) {

                        try {

                            int bytesAvailable = mmInputStream.available();
                            if (bytesAvailable > 0) {
                                byte[] packetBytes = new byte[bytesAvailable];
                                mmInputStream.read(packetBytes);
                                for (int i = 0; i < bytesAvailable; i++) {
                                    byte b = packetBytes[i];
                                    if (b == delimiter) {
                                        byte[] encodedBytes = new byte[readBufferPosition];
                                        System.arraycopy(readBuffer, 0,
                                                encodedBytes, 0,
                                                encodedBytes.length);
                                        final String data = new String(
                                                encodedBytes,Charset.forName("UTF-8"));
                                        readBufferPosition = 0;

                                        handler.post(new Runnable() {
                                            public void run() {
                                                //myLabel.setText(data);
                                                Log.w("data",data);
                                            }
                                        });
                                    } else {
                                        readBuffer[readBufferPosition++] = b;
                                    }
                                }
                            }

                        } catch (IOException ex) {
                            stopWorker = true;
                        }

                    }
                }
            });

            workerThread.start();
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /*
     * This will send data to be printed by the bluetooth printer
     */
    void sendData() throws IOException {
        try {

            // the text typed by the user
            String msg = Convert( StringReporte);
            msg += "\n\n\n";
            byte[] cc = new byte[]{0x1B,0x21,0x00};
            mmOutputStream.write(cc);
            mmOutputStream.write(msg.getBytes());
            Log.w("Activo","Data Sent");

        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // Close the connection to bluetooth printer.
    void closeBT() throws IOException {
        try {
            stopWorker = true;
            mmOutputStream.close();
            mmInputStream.close();
            mmSocket.close();
            Log.w("Activo","Bluetooth Closed");
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public String Convert(String text)
    {
        String newText="";
        char[]charArray=text.toCharArray();
        for(char c: charArray)
        {
            switch(c)
            {
                case 'í':
                    newText+= "i";
                    break;
                default:
                    newText += c;
                    break;
            }
        }
        return newText;
    }
}
