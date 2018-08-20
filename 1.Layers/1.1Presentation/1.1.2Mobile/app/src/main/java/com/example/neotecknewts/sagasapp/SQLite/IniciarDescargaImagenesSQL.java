package com.example.neotecknewts.sagasapp.SQLite;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * InciarDescargaSQL
 * Clase que exitiende de android.database.SQLiteOpenHelper la cual,
 * permite la creación de la base de datos en el dispositivo
 * y la ejecucion de consultas SQLite en el dispositivo, este almacena
 * los datos de las imagenes de sección de iniciar descarga
 * @author  Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
 * @date 20/08/2018
 * @lastupdate 20/08/2018
 * @company NEOTECK
 */

public class IniciarDescargaImagenesSQL extends SQLiteOpenHelper {
    //region Constantes
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_NAME = "iniciar_descarga_imagenes";
    //endregion
    //region Constructores
    /**
     * Crea un objeto helper que crea, abre y/o administra la base de datos.
     * Este metodo retornara una respuesta rapida. La base de datos no esta actualmente creada
     * o abierta aun un {@link #getWritableDatabase} or {@link #getReadableDatabase} es invocado.
     *
     * @param context Contexto que se utiliza para abrir o crear la base de datos
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 20/08/2018
     * @lastupdate 20/08/2018
     */
    public IniciarDescargaImagenesSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }
    //endregion
    //region Metodos sobrescritor
    /**
     * Se invoca cuando la base de datos es creada por primera vez. En este es donde las tablas
     * son creadas y la pobracion de estas sucede.
     *
     * @param db La base de datos
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 20/08/2018
     * @lastupdate 20/08/2018
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        /* Ejecuto la creación de la tabla para descargas */
        db.execSQL(
                "CREATE TABLE "+TABLE_NAME+
                        "(IdOrdenCompra INTEGER PRIMARY KEY,"+
                        "Imagenes TEXT,"+
                        "ImagenesURL TEXT,"+
                        "UuidOrden TEXT)"
        );
    }

    /**
     * Es invocado cuando la base de datos nesecita se actualizada. La implementacion
     * usara este metodo para eliminar las tablas, agregar tablas o para cualquier cosa
     * si es nesesario actualizar la version.
     * <p>
     * <p>
     * La documentación de SQLite ALTER TABLE puede ser encontrada en
     * <a href="http://sqlite.org/lang_altertable.html">here</a>. Si usted agrega nuevas columnas
     * puede utilizar ALTER TABLE  para agregar despues de dar vida a la tabla. Si usted renombra
     * o elimina columnas puede utilizar  ALTER TABLE  para renombrar la tabla antigua, despues de
     * crear la nueva tabla y poblar con el contenido de la anterior.
     * </p><p>
     * Este metodo se ejecuta en una transacción. Si existe una excepcion, todos los cambios
     * seran descartados automaticamente.
     * </p>
     *
     * @param db         La base de datos.
     * @param oldVersion La version anterior de la base de datos.
     * @param newVersion La nueva version de la base de datos.
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXIST "+TABLE_NAME);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }
    //endregion
}
