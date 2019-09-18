package com.example.neotecknewts.sagasapp.Util;

import android.app.Activity;
import android.app.AlertDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.util.Log;
import android.widget.Toast;

import com.example.neotecknewts.sagasapp.R;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Set;
import java.util.UUID;

public class Imprimir {
    private BluetoothAdapter mBluetoothAdapter;
    private BluetoothDevice mmDevice;
    private BluetoothSocket mmSocket;
    private Context context;
    private String device_select;
    private OutputStream mmOutputStream;
    private InputStream mmInputStream;
    private Thread workerThread;

    private byte[] readBuffer;
    private int readBufferPosition;
    private volatile boolean stopWorker;
    private String cadena_impresion;
    private Activity activity;

    public Imprimir(Activity activity,Context context){
        this.context = context;
        this.activity = activity;
    }

    /**
     * <h3>starPrint</h3>
     * Inicializa la conexion Bluetooth con las impresoras
     * mostrara en un dialogo el listado de impresoras disponibles,
     * despues de seleccionarce una se iniciara el proceso de impresion tomando
     * como texto de impresion el {@link String} enviado de parametro
     * @param cadena_impresion Cadena {@link String} con el texto a imprimir
     */
    public void starPrint(String cadena_impresion){
        try {
            this.cadena_impresion = cadena_impresion;
            mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

            if (mBluetoothAdapter == null) {
                Toast.makeText(context, "El dispositivo no cuenta con Bluetooth",
                        Toast.LENGTH_SHORT).show();
                //myLabel.setText("No bluetooth adapter available");
            }

            if (!mBluetoothAdapter.isEnabled()) {
                Intent enableBluetooth = new Intent(
                        BluetoothAdapter.ACTION_REQUEST_ENABLE);
                activity.startActivityForResult(enableBluetooth, 0);
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
                AlertDialog.Builder builder = new AlertDialog.Builder(context);
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
    private void findBT(CharSequence charSequence) {

        try {
            mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

            if (mBluetoothAdapter == null) {
                Toast.makeText(context, "El dispositivo no cuenta con Bluetooth",
                        Toast.LENGTH_SHORT).show();
                //myLabel.setText("No bluetooth adapter available");
            }

            if (!mBluetoothAdapter.isEnabled()) {
                Intent enableBluetooth = new Intent(
                        BluetoothAdapter.ACTION_REQUEST_ENABLE);
                activity.startActivityForResult(enableBluetooth, 0);
            }

            Set<BluetoothDevice> pairedDevices = mBluetoothAdapter
                    .getBondedDevices();
            if (pairedDevices.size() > 0) {
                for (BluetoothDevice device : pairedDevices) {
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
    private void openBT() throws IOException {
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

            workerThread = new Thread(() -> {
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

                                    handler.post(() -> {
                                        //myLabel.setText(data);
                                        Log.w("data",data);
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
    private void sendData() throws IOException {
        try {

            // the text typed by the user
            String msg = cadena_impresion;
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
    private void closeBT() throws IOException {
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
