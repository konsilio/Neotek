package com.example.neotecknewts.sagasapp.Util;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.support.v4.app.ActivityCompat;

public class Permisos {
    private Context context;
    private Activity activity;
    private static final int MULTIPLE_PERMISSIONS_REQUEST_CODE = 3;
    private String[] permissions = new String[]{
            Manifest.permission.CAMERA,
            Manifest.permission.BLUETOOTH,
            Manifest.permission.BLUETOOTH_ADMIN,
            Manifest.permission.WRITE_EXTERNAL_STORAGE,
            Manifest.permission.READ_EXTERNAL_STORAGE,
            Manifest.permission.ACCESS_FINE_LOCATION,
            Manifest.permission.ACCESS_COARSE_LOCATION,
            Manifest.permission.INTERNET,
            Manifest.permission.ACCESS_NETWORK_STATE,
            Manifest.permission.STATUS_BAR,
            Manifest.permission.WRITE_GSERVICES,
            Manifest.permission.MEDIA_CONTENT_CONTROL

    };
    public Permisos(Activity activity){
        this.context = activity.getApplicationContext();
        this.activity = activity;
    }
    public void permisos(){
        //Verifica si los permisos establecidos se encuentran concedidos
        if (ActivityCompat.checkSelfPermission(context, permissions[0]) != PackageManager.PERMISSION_GRANTED ||
                ActivityCompat.checkSelfPermission(context, permissions[1]) != PackageManager.PERMISSION_GRANTED) {
            //Si alguno de los permisos no esta concedido lo solicita
            ActivityCompat.requestPermissions(activity, permissions, MULTIPLE_PERMISSIONS_REQUEST_CODE);
        }
    }

}
