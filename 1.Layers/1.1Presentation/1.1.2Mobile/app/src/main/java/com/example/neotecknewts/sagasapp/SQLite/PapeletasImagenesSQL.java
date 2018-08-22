package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;

import java.net.URI;
import java.util.List;

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
                "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "Imagen TEXT,"+
                "Url TEXT,"+
                "CalveUnica TEXT"+
                ")");
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
         /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF  EXISTS "+TABLE_NAME);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }

    /**
     * Insert
     * Realiza el registro en base de datos de los datos de la imagen,
     * retornara en caso de ser correcto el id del registro en caso contrario retornara un -1
     * @param imagen String de 64 bits de la imagen
     * @param url Url en el dispositvo de la imagen
     * @param CalveUnica Clave unica de la papeleta
     * @return En caso de ser correcto el Id del registro, en caso erroneo un -1
     */
    public Long[] Insert(List<URI> imagen, List<String> url, String CalveUnica){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[imagen.size()];
        for (int x = 0;x>imagen.size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("IMAGEN",imagen.get(x).toString());
            contentValues.put("Url",url.get(x));
            contentValues.put("CalveUnica",CalveUnica);
            inserts[x] =  db.insert(TABLE_NAME,null,contentValues);
            Log.w("Imagenes",String.valueOf(inserts[x]));

        }
        return inserts;
    }

    /**
     * Retorna un arreglo con las imagenes de la papeleta, se tomara como parametro la
     * ClaveOperacion
     * @param ClaveOperacion Clave unica del la orden
     * @return Registro/os que retorno la consulta
     */
    public Cursor GetRecordsByCalveUnica(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_NAME+" WHERE CalveUnica ='"+ClaveOperacion+"'",null);
    }
}
