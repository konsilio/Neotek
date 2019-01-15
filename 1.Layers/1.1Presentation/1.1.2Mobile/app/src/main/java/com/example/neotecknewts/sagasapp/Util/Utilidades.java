package com.example.neotecknewts.sagasapp.Util;

import android.Manifest;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;

import com.example.neotecknewts.sagasapp.R;

import java.math.BigInteger;
import java.nio.charset.Charset;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

/**
 * Created by neotecknewts on 07/08/18.
 */
//clase de utilidades, contiene metodos que se pueden usar en las otras clases
public class Utilidades {

    //metodo que revisa los permisos para el uso de la camara
    public static List<String> checkAndRequestPermissions(Context context) {

        int camera = ContextCompat.checkSelfPermission(context, android.Manifest.permission.CAMERA);
        int readStorage = ContextCompat.checkSelfPermission(context, Manifest.permission.READ_EXTERNAL_STORAGE);
        int writeStorage = ContextCompat.checkSelfPermission(context, android.Manifest.permission.WRITE_EXTERNAL_STORAGE);
        int fineLoc = ContextCompat.checkSelfPermission(context, android.Manifest.permission.ACCESS_FINE_LOCATION);
        int coarseLoc = ContextCompat.checkSelfPermission(context, android.Manifest.permission.ACCESS_COARSE_LOCATION);
        List<String> listPermissionsNeeded = new ArrayList<>();

        if (camera != PackageManager.PERMISSION_GRANTED) {
            listPermissionsNeeded.add(android.Manifest.permission.CAMERA);
        }
        if (readStorage != PackageManager.PERMISSION_GRANTED) {
            listPermissionsNeeded.add(android.Manifest.permission.READ_EXTERNAL_STORAGE);
        }
        if (writeStorage != PackageManager.PERMISSION_GRANTED) {
            listPermissionsNeeded.add(android.Manifest.permission.WRITE_EXTERNAL_STORAGE);
        }
        if (fineLoc != PackageManager.PERMISSION_GRANTED) {
            listPermissionsNeeded.add(android.Manifest.permission.ACCESS_FINE_LOCATION);
        }
        if (coarseLoc != PackageManager.PERMISSION_GRANTED) {
            listPermissionsNeeded.add(android.Manifest.permission.ACCESS_COARSE_LOCATION);
        }

        return listPermissionsNeeded;
    }

    //metodo que encripta la contraseña en SHA256
    public static String getHash(String password) throws NoSuchAlgorithmException {
        MessageDigest md = MessageDigest.getInstance( "SHA-256" );
        String text = password;

        // Change this to UTF-16 if needed
        md.update( text.getBytes( Charset.forName("UTF-8") ) );
        byte[] digest = md.digest();

        return bin2hex(digest);
    }

    //metodo que devuelve el string proveniente del sha256
    static String bin2hex(byte[] data) {
        return String.format("%0" + (data.length * 2) + 'x', new BigInteger(1, data)).toUpperCase();
    }

    public static String getCurrentDate(String format) {
        return new SimpleDateFormat(format, new Locale("es","MX")).format(new Date());
    }
}

