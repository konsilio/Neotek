package com.example.neotecknewts.sagasapp.Util;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;

import java.util.HashMap;

/**
 * Created by neotecknewts on 08/08/18.
 */

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
    private static final String IS_LOGIN = "IsLoggedIn";

    // User name (make variable public to access from outside)
    public static final String KEY_PASS= "password";

    // Email address (make variable public to access from outside)
    public static final String KEY_EMAIL = "email";

    public static final String KEY_TOKEN = "token";

    public static final String KEY_ID_EMPRESA = "idEmpresa";

    // Constructor
    public Session(Context context){
        this._context = context;
        pref = _context.getSharedPreferences(PREF_NAME, PRIVATE_MODE);
        editor = pref.edit();
    }


    public void createLoginSession(String password, String email, String token, int idEmpresa){
        // Storing login value as TRUE
        editor.putBoolean(IS_LOGIN, true);

        // Storing name in pref
        editor.putString(KEY_PASS, password);

        // Storing email in pref
        editor.putString(KEY_EMAIL, email);

        editor.putString(KEY_TOKEN, token);

        editor.putInt(KEY_ID_EMPRESA,idEmpresa);

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

    public String getTokenWithBearer(){
        return "Bearer " + pref.getString(KEY_TOKEN,null);
    }

    public String getToken(){
        return pref.getString(KEY_TOKEN,null);
    }

    public int getIdEmpresa(){
        return pref.getInt(KEY_ID_EMPRESA,0);
    }
}
