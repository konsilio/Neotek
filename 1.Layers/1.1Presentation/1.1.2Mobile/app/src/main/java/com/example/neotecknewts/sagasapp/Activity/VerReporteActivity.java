package com.example.neotecknewts.sagasapp.Activity;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.Toast;

import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.R;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Set;
import java.util.UUID;

public class VerReporteActivity extends AppCompatActivity {
    private boolean EsReporteDelDia;
    private boolean EsRecargaEstacionInicial,EsRecargaEstacionFinal,EsPrimeraLectura;
    public boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    public boolean EsTraspasoPipaInicial,EsTraspasoPipaFinal,EsPasoIniciaLPipa;
    boolean EsAnticipo,EsCorte;
    RecargaDTO recargaDTO;
    TraspasoDTO traspasoDTO;
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
                GenerarReporteAnticipo();
            }else if (EsCorte){
                GenerarReporteCorteCaja();
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

                /*intent.putExtra("EsTraspasoPipaInicial", EsTraspasoPipaInicial);
                intent.putExtra("EsTraspasoPipaFinal", EsTraspasoPipaFinal);
                intent.putExtra("traspasoDTO", traspasoDTO);
                startActivity(intent);*/
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

    private void GenerarReporteCorteCaja() {
        HtmlReporte = "<body>" +
                "<h3>Reporte-Corte de caja</h3>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Anticipo</td>" +
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
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Estación</td>"+
                "<td>[{Estacion}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Fecha de venta</td>"+
                "<td>[{Fecha-venta}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Venta Total</td>"+
                "<td>[{Venta-total}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Anticipos</td>"+
                "<td>[{Anticipos}]</td>" +
                "</tr>" +
                "<tr>" +
                "<tr>" +
                "<td>Monto de corte</td>"+
                "<td>[{Monto-corte}]</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>"+
                "<hr>"+
                "<h3>P5000</h3>"+
                "<table>"+
                "<tbody>"+
                "<tr>" +
                "<td>Inicial: </td>" +
                "<td>[{Inicial}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Final: </td>" +
                "<td>[{Final}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Litros vendidos: </td>" +
                "<td>[{Litros-vendidos}]</td>" +
                "</tr>" +
                "</tbody>"+
                "</table>"+
                "</body>";

        StringReporte =
                "<body>" +
                        "<h3>Reporte-Corte de caja</h3>" +
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Clave Anticipo</td>" +
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
                        "<table>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Estación</td>"+
                        "<td>[{Estacion}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Fecha de venta</td>"+
                        "<td>[{Fecha-venta}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<tr>" +
                        "<td>Venta Total</td>"+
                        "<td>[{Venta-total}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<tr>" +
                        "<td>Anticipos</td>"+
                        "<td>[{Anticipos}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<tr>" +
                        "<td>Monto de corte</td>"+
                        "<td>[{Monto-corte}]</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>"+
                        "<hr>"+
                        "<h3>P5000</h3>"+
                        "<table>"+
                        "<tbody>"+
                        "<tr>" +
                        "<td>Inicial: </td>" +
                        "<td>[{Inicial}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Final: </td>" +
                        "<td>[{Final}]</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Litros vendidos: </td>" +
                        "<td>[{Litros-vendidos}]</td>" +
                        "</tr>" +
                        "</tbody>"+
                        "</table>"+
                        "</body>";
    }

    private void GenerarReporteAnticipo() {
        HtmlReporte = "<body>" +
                "<h3>Reporte-Anticipo</h3>" +
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Clave Anticipo</td>" +
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
                "<table>" +
                "<tbody>" +
                "<tr>" +
                "<td>Estación</td>"+
                "<td>[{Estacion}]</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Monto anticipado: </td>" +
                "<td>[{Monto-anticipo}]</td>" +
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
                                                encodedBytes, "US-ASCII");
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
            String msg = StringReporte;
            msg += "\n";
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


}
