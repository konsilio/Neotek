package com.example.neotecknewts.sagasapp.Util;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.MenuDTO;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Set;

/**
 * Created by neotecknewts on 08/08/18.
 */
//clase para guardar la sesion
public class Session {
    // Shared Preferences
    SharedPreferences pref;

    // Editor for Shared preferences
    Editor editor;

    // Context
    Context _context;

    // Shared pref mode
    int PRIVATE_MODE = 0;

    // Sharedpref file name
    private static final String PREF_NAME = "USER";

    // All Shared Preferences Keys
    public static final String IS_LOGIN = "IsLoggedIn";

    // User name (make variable public to access from outside)
    public static final String KEY_PASS= "password";

    // Email address (make variable public to access from outside)
    public static final String KEY_EMAIL = "email";

    public static final String KEY_TOKEN = "token";

    public static final String KEY_ID_EMPRESA = "idEmpresa";

    //Variable de session para los mensajes de los tikets
    public static final String KEY_MENSAJE = "mensaje";

    public static final String KEY_FB_TOKEN = "fbToken";

    public static final String KEY_NOMBRE = "name";

    public static final String KEY_TIME_SESSION = "time";

    public static final String KEY_ID_USUARIO = "id";
    // Constructor
    public Session(Context context){
        this._context = context;
        pref = _context.getSharedPreferences(PREF_NAME, PRIVATE_MODE);
        editor = pref.edit();
    }


    public void createLoginSession(String password, String email, String token, int idEmpresa,
                                   String fb_token,String mensaje,String nombre,int idUsuario){
        // Storing login value as TRUE
        editor.putBoolean(IS_LOGIN, true);

        // Storing name in pref
        editor.putString(KEY_PASS, password);

        // Storing email in pref
        editor.putString(KEY_EMAIL, email);

        editor.putString(KEY_TOKEN, token);

        editor.putInt(KEY_ID_EMPRESA,idEmpresa);

        editor.putString(KEY_MENSAJE,mensaje);

        editor.putString(KEY_FB_TOKEN,fb_token);

        editor.putString(KEY_NOMBRE,nombre);

        editor.putString(KEY_TIME_SESSION,new Date().toString());

        editor.putInt(KEY_ID_USUARIO,idUsuario);

        // commit changes
        editor.commit();
    }


    /**
     * Get stored session data
     * */
    public HashMap<String, String> getUserDetails(){
        HashMap<String, String> user = new HashMap<String, String>();
        // user name
        user.put(KEY_PASS, pref.getString(KEY_PASS, null));

        // user email id
        user.put(KEY_EMAIL, pref.getString(KEY_EMAIL, null));

        user.put(KEY_TOKEN, pref.getString(KEY_TOKEN,null));

        user.put(KEY_ID_EMPRESA, pref.getString(KEY_ID_EMPRESA,null));

        // return user
        return user;
    }

    //regresa el token junto con el texto "Bearer"
    public String getTokenWithBearer(){
        return "Bearer " + pref.getString(KEY_TOKEN,null);
    }

    //regresa el token simple
    public String getToken(){
        return pref.getString(KEY_TOKEN,null);
    }

    //regresa el id de la empresa
    public int getIdEmpresa(){
        return pref.getInt(KEY_ID_EMPRESA,0);
    }

    public String getAttribute(String attribute){
        return pref.getString(attribute,"");
    }

    public boolean isLogin(){
        return pref.getBoolean(IS_LOGIN,false);
    }

    public void logOut() {
        editor.putBoolean(IS_LOGIN,false);
        editor.putString(KEY_TOKEN,"");
        editor.putString(KEY_PASS,"");
        //editor.putString(KEY_EMAIL,"");
        editor.commit();
    }

    public boolean isExpired(){
        Date fecha_session = new Date(pref.getString(KEY_TIME_SESSION,Calendar.getInstance().getTime().toString()));
        int dias=(int) ((new Date().getTime()-fecha_session.getTime())/86400000);
        Log.w("Han pasado",dias+" Dias");
        return dias<=0;
    }
}
