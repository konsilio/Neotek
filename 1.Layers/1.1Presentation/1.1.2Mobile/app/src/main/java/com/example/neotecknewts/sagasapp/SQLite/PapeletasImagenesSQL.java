package com.example.neotecknewts.sagasapp.SQLite;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by neotecknewts on 20/08/18.
 */

public class PapeletasImagenesSQL extends SQLiteOpenHelper {
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_NAME = "papeletas_imagenes";
    public PapeletasImagenesSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL("CREATE TABLE "+TABLE_NAME+"(" +
                "IMAGEN TEXT,"+
                "IMAGEN_URL TEXT,"+
                "Uuid_ TEXT"+
                ")");
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
         /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXIST "+TABLE_NAME);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }
}
