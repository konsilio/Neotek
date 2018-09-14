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

import com.example.neotecknewts.sagasapp.R;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Set;
import java.util.UUID;

public class VerReporteActivity extends AppCompatActivity {
    private boolean EsReporteDelDia;
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
            EsReporteDelDia = (boolean)bundle.get("EsReporteDelDia");
            StringReporte = (String) bundle.get("StringReporte");
            HtmlReporte = (String) bundle.get("HtmlReporte");
        }
        WebView WVVerReporteActivityReporte = findViewById(R.id.WVVerReporteActivityReporte);
        Button btnVerReporteActivityTerminar= findViewById(R.id.BtnVerReporteActivityTerminar);
        Button btnReporteActivityImprimir = findViewById(R.id.BtnReporteActivityImprimir);
        btnVerReporteActivityTerminar.setOnClickListener(v -> {
            Intent intent = new Intent(VerReporteActivity.this, MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
            finish();
        });
        btnReporteActivityImprimir.setOnClickListener(v -> {
            listDevices();
            btnVerReporteActivityTerminar.setVisibility(View.VISIBLE);
            /*new Imprimir(this,this).starPrint(StringReporte)*/
        });
        if (EsReporteDelDia){
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
