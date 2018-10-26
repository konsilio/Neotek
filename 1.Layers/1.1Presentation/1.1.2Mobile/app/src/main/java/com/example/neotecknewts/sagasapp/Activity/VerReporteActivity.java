package com.example.neotecknewts.sagasapp.Activity;

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

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
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
        if(bundle!=null){
            EsReporteDelDia = bundle.getBoolean("EsReporteDelDia",false);
            EsRecargaEstacionInicial = bundle.getBoolean("EsRecargaEstacionInicial",
                    false);
            EsRecargaEstacionFinal = bundle.getBoolean("EsRecargaEstacionFinal",
                    false);
            EsPrimeraLectura = bundle.getBoolean("EsPrimeraLectura",false);
            EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);
            EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);
            EsTraspasoEstacionFinal = bundle.getBoolean("EsTraspasoEstacionFinal",
                    false);
            EsTraspasoPipaInicial = bundle.getBoolean("EsTraspasoPipaInicial",false);
            EsTraspasoEstacionFinal = bundle.getBoolean("EsTraspasoPipaFinal",false);
            EsPasoIniciaLPipa = bundle.getBoolean("EsPasoIniciaLPipa",false);
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);

            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);

            if(EsReporteDelDia) {
                StringReporte = (String) bundle.get("StringReporte");
                HtmlReporte = (String) bundle.get("HtmlReporte");
            }
            if(EsRecargaEstacionFinal){
                recargaDTO = (RecargaDTO) bundle.getSerializable("recargaDTO");
                GenerarReporteRecargaFinal(recargaDTO);
            }
            if(EsTraspasoEstacionInicial ||EsTraspasoEstacionFinal){
                traspasoDTO = (TraspasoDTO) bundle.getSerializable("traspasoDTO");
                GenerarReporteTraspaso(traspasoDTO);
            }
            if(EsTraspasoPipaInicial || EsTraspasoPipaFinal){
                traspasoDTO = (TraspasoDTO) bundle.getSerializable("traspasoDTO");
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
                if(EsVentaCamioneta) {
                    setTitle("Nota de venta");
                    ventaDTO = (VentaDTO) bundle.getSerializable("ventaDTO");
                    GenerarReporte(ventaDTO);
                }
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

        session = new Session(this);
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
                "</tr>"+
                "</table>"+
                "<hr>"+
                "<h3>Cliente</h3>" +
                "<table>" +
                "<tr>" +
                "<td>Razon Social</td>" +
                "<td>[{Razon-social}]</td>" +
                "</tr>"+
                "<tr>" +
                "<td>RFC</td>" +
                "<td>[{RFC}]</td>" +
                "</tr>"+
                "</table>" +
                "<table>" +
                "<tr>" +
                "<td>Concepto</td>"+
                "<td>Cant.</td>"+
                "<td>P.Uni.</td>"+
                "<td>Desc</td>"+
                "<td>Subt.</td>"+
                "</tr>"+
                "<tr>" +
                "<td>[{Concepto}]</td>"+
                "<td>[{Cant}]</td>"+
                "<td>[{P-Uni}]</td>"+
                "<td>[{Desc}]</td>"+
                "<td>[{Subt}]</td>"+
                "</tr>"+
                "</table>"+
                "<table>" +
                "<tr>" +
                "<td>I.V.A. (16%)</td>"+
                "<td>[{iva}]</td>"+
                "</tr>"+
                "<tr>"+
                "<td>Total</td>"+
                "<td>[{Total}]</td>"+
                "</tr>"+
                "<tr>"+
                "<td>Efectivo recibido:</td>"+
                "<td>[{Efectivo}]</td>"+
                "</tr>"+
                "<tr>"+
                "<td>Cambio</td>"+
                "<td>[{Cambio}]</td>"+
                "</tr>"+
                "</table>"+
                "</body>";
        StringReporte = "\tTiket de venta\n" +
                        "Gas Mundial de Guerrero\n\n"+
                        "Tiket\t[{Clave-venta}]\n"+
                        "Fecha\t[{Fecha}]\n"+
                        "Hora\t[{Hora}]\n"+
                        "---------------------------"+
                        "\tCliente\n" +
                        "Razon Social\t[{Razon-social}]\n" +
                        "RFC\t[{RFC}]\n" +
                        "---------------------------------\n"+
                        "|Concepto|Cant.|P.Uni.|Desc|Subt|\n"+
                        "---------------------------------\n"+
                        "|[{Concepto}]|[{Cant}]|[{P-Uni}]|[{Desc}]|[{Subt}]|\n"+
                        "________________________________\n"+
                        "\tI.V.A. (16%) [{iva}]\n"+
                        "\tTotal [{Total}]\n";
        StringReporte = "\tEfectivo recibido: [{Efectivo}]\n"+
                        "\tCambio [{Cambio}]\n";
        StringReporte = "\tVenta Contado\n";
        StringReporte = "Le atendio [{Usuario}]"+
                        "----------------------------------\n"+
                        "Gas Mundial de Guerrero S.A de C.V.\n"+
                        "Av. Principal No. 5477 C.P. 56789\n"+
                        "www.gasmundialdeguerrero.com.mx\n\n"+

                        "Facturación electrónica en :\n"+
                        "www.gasmundialdeguerrero.com.mx/\n"+
                        "facturacion\n\n"+
                        "Folio Factura: [{Folio-factura}]\n\n"+
                        "Gracias por su confianza,¡vuelva\n" +
                        "pronto!"
                    ;


    }

    private void GenerarReporteCorteCaja() {
        HtmlReporte = "<body>" +
                "<h3 style='text-align: center;'><u>Reporte-Corte de caja</u></h3>" +
                "<table style='font-size:25px; width:100%; margin-left:5px;margin-rigth:5px;'>" +
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
                "<table style='font-size:25px; width:100%;margin-left:5px;margin-rigth:5px;'>" +
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
                "<hr>"+
                "<h3 style='text-align: center;'>P5000</h3>"+
                "<table style='font-size:25px; width:100%;margin-left:5px;margin-rigth:5px;'>"+
                "<tbody>"+
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
                "</tr>" +
                "</tbody>"+
                "</table>"+
                "</body>";

        StringReporte =
                        "Reporte-Corte de caja\n" +

                        "Clave Anticipo\t" +
                        "[{ClaveTraspaso}]\n" +
                        "Fecha\t" +
                        "[{Fecha}]\n" +
                        "Hora\t" +
                        "[{Hora}]\n" +
                        "----------------------\n" +
                        "Estación\t"+
                        "[{Estacion}]\n" +
                        "Fecha de venta\t"+
                        "[{Fecha-venta}]\n" +
                        "Venta Total\t"+
                        "[{Venta-total}]\n" +
                        "Anticipos\t"+
                        "[{Anticipos}]\n" +
                        "Monto de corte\t"+
                        "[{Monto-corte}]\n" +
                        "-----------------------\n"+
                        "\tP5000"+
                        "Inicial: \t" +
                        "[{Inicial}]\n" +
                        "Final: \t" +
                        "[{Final}]\n" +
                        "Litros vendidos: \t" +
                        "[{Litros-vendidos}]\n" +
                        "Entregué\n"+
                        "[{Entrego-nombre}]\n\n" +
                        "________________________\n"+
                        "Recibi:\n"+
                        "[{Recibio}]\n\n"+
                        "________________________\n"
                        ;
        HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",
                corteDTO.getClaveOperacion());
        StringReporte = StringReporte.replace("[{ClaveTraspaso}]",
                corteDTO.getClaveOperacion());
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        HtmlReporte.replace("[{Fecha}]",format.format(corteDTO.getFecha()));
        StringReporte = StringReporte.replace("[{Fecha}]",format.format(corteDTO.getFecha()));

        HtmlReporte = HtmlReporte.replace("[{Hora}]]",
                corteDTO.getHora());
        StringReporte = StringReporte.replace("[{Hora}]",
                corteDTO.getHora());

        HtmlReporte = HtmlReporte.replace("[{Estacion}]]",
                corteDTO.getNombreEstacion());
        StringReporte = StringReporte.replace("[{Estacion}]",
                corteDTO.getNombreEstacion());
    }

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
                "\t Reporte-Anticipo\n" +
                "Clave Anticipo\t" +
                "[{ClaveTraspaso}]\n" +
                "Fecha\t" +
                "[{Fecha}]\n" +
                "Hora\t" +
                "[{Hora}]\n" +
                "-----------------------------\n" +
                "Estación\t"+
                "[{Estacion}]\n" +
                "Monto anticipado: \t" +
                "[{Monto-anticipo}]\n\n"+
                "Entregué:\n"+
                "[{Usuario-entrego}]__________\n\n"+
                "Recibí\n"+
                        "[{Usuario-recibi}]__________\n\n";
        HtmlReporte = HtmlReporte.replace("[{ClaveTraspaso}]",anticiposDTO.getClaveOperacion());
        StringReporte = StringReporte.replace("[{ClaveTraspaso}]",anticiposDTO.getClaveOperacion());
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate=
                new SimpleDateFormat("dd/MM/yyyy");
        HtmlReporte = HtmlReporte.replace("[{Fecha}]",fdate.format(anticiposDTO.getFecha()));
        StringReporte = StringReporte.replace("[{Fecha}]",fdate.format(anticiposDTO.getFecha()));
        HtmlReporte = HtmlReporte.replace("[{Hora}]",anticiposDTO.getHora());
        StringReporte = StringReporte.replace("[{Hora}]",anticiposDTO.getHora());
        HtmlReporte = HtmlReporte.replace("[{Estacion}]",anticiposDTO.getNombreEstacion());
        StringReporte = StringReporte.replace("[{Estacion}]",anticiposDTO.getNombreEstacion());
        format = NumberFormat.getCurrencyInstance();
        HtmlReporte = HtmlReporte.replace("[{Monto-anticipo}]","$"+String.valueOf(
                anticiposDTO.getAnticipar()));
        StringReporte = StringReporte.replace("[{Monto-anticipo}]","$"+String.valueOf(
                anticiposDTO.getAnticipar()));
        StringReporte = StringReporte.replace("[{Usuario-entrego}]",
                anticiposDTO.getNombreEstacion());
        StringReporte = StringReporte.replace("[{Usuario-recibi}]",
                anticiposDTO.getNombreEstacion());
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
                "<td>[{P5000Inicio}]</td>" +
                "<td>[{P5000Fin}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>[{PipaNombre2}]</td>" +
                "<td>[{P5000Inicio2}]</td>" +
                "<td>[{P5000Fin2}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "<tr>" +
                "<td>Litros traspasados: </td>" +
                "<td>[{LitrosTraspasados}]</td>" +
                "</tr>" +
                "</table>" +
                "</body>";

        StringReporte = "\n Rep-Traspaso - [{Estacion}] \n" +
                "\n Clave Traspaso" +
                "\t [{ClaveTraspaso}]" +
                "\n Fecha " +
                "\t [{Fecha}]" +
                "\n Hora" +
                "\t [{Hora}]\n" +
                "------------------------------------" +
                "\n Lectura P5000 " +
                "\n  "+
                "\t Inicial: " +
                "\t Final" +
                "\n[{PipaNombre}]"+
                "\t[{P5000Inicial}]" +
                "\t[{P5000Final}]" +
                "\n[{PipaNombre2}]"+
                "\t[{P5000Inicial2}]" +
                "\t[{P5000Final2}]" +
                "\n------------------------------------" +
                "\nLitros traspasados: \t" +
                "[{LitrosTraspasados}]\n"+
                "\t\tFirma\n"+
                "[{NombrePipaTraspaso}](Traspasé)--------\n"+
                "[{NombreUsuarioTraspaso}]\n"+
                "[{NombrePipaRecibi}](Recibí)\n"+
                "[{NombreUsuarioRecivi}]------------------\n";

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
                "<td>[{ClaveRecarga}]</td>" +
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
                "<td>[{NombrePipa}]</td>" +
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
                "\t [{ClaveRecarga}]" +
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
                "[{NombrePipa}] \t" +
                "[{LecturaInicialPipa}] \t" +
                "[{LecturaFinalPipa}] \n" +
                "[{NombreEstacion}] \t" +
                "[{LecturaIncialEstacion}] \t" +
                "[{LecturaFinalEstacion}] \n" +
                "Litros recargados: \t" +
                "[{LitrosRecargados}]";


    }

    private void GenerarReporteTraspaso(TraspasoDTO traspasoDTO){
        HtmlReporte = "<body>" +
                "<h3>Reporte-Traspaso-[{Estracion}]</h3>" +
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

                "<td>[{NombrePipa}]</td>" +
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
                "\t [{ClaveTraspaso}]" +
                "\n Fecha " +
                "\t [{Fecha}]" +
                "\n Hora" +
                "\t [{Hora}]\n" +
                "------------------------------------" +
                "\n Porcentaje Estación (%) " +
                "\n Inicial: " +
                "\t Final" +
                "\n[{PorcentajeInicial}]" +
                "\t[{PorcentajeFinal}]" +
                "\n------------------------------------" +
                "\n Lectura P5000" +
                "\n\t" +
                "Inicial:\t" +
                "Final\n" +

                "[{NombreEstacion}] \t" +
                "[{LecturaIncialEstacion}] \t" +
                "[{LecturaFinalEstacion}] \n" +

                "[{NombrePipa}] \t" +
                "[{LecturaInicialPipa}] \t" +
                "[{LecturaFinalPipa}] \n" +
                "Litros traspasados: \t" +
                "[{LitrosTraspasados}]\n"+
                "\t\tFirma\n"+
                "[{NombrePipaTraspaso}](Traspasé)--------\n"+
                "[{NombreUsuarioTraspaso}]\n"+
                "[{NombrePipaRecibi}](Recibí)\n"+
                "[{NombreUsuarioRecivi}]------------------\n";

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
                    AlertDialog.Builder builder= new AlertDialog.Builder(this);
                    builder.setTitle(R.string.error_titulo);
                    builder.setMessage("No se ha podido encontra ninguna impresora, verique:\n"+
                    " - La impreso este encendida\n - Que la impresora este vinculada al dispositivo");
                }
                for (int x = 0;x<adevices.size();x++){
                    lista[x] = adevices.get(x);
                }
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
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
