package com.example.neotecknewts.sagasapp.Util;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.util.Log;

import java.util.Date;

public class Lisener extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        new LisenerTaks().execute();
    }
    @SuppressLint("StaticFieldLeak")
    class LisenerTaks extends AsyncTask<String,Void,String>{

        @Override
        protected String doInBackground(String... strings) {
            /*Enviar datos */
            Date d = new Date();
            Log.w("Entro","Entro en el lisener a las "+d.getTime());
            return null;
        }
    }
}
