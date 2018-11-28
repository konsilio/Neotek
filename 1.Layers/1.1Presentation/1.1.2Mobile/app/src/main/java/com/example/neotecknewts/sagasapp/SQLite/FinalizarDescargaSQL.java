package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;

/**
 * <h3>FinalizarDescargaSQL</h3>
 * Clase de la cual se extiende a {@link SQLiteOpenHelper} para realizar el registro de los datos
 * de finalizar descarga de manera local
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 * @company Neoteck
 * @date 20/08/2018
 * @updated 28/08/2018
 */
public class FinalizarDescargaSQL extends SQLiteOpenHelper {
    //region Constantes de clase
    private static final String DB_NAME="sagas_db_finalizar_descarga";
    private static final int DB_VERSION = 1;
    private static final String TABLE_FINALIZAR_DESCARGA="finalizar_descarga";
    private static final String TABLE_IMAGENES_FINALIZAR_DESCARGA = "imagenes_finalizar_descarga";
    //endregion
    //region Constructores de clase

    /**
     * FinalizarDescargaSQL
     * Constructor de clase, se enviara como parametro un objeto de tipo {@link Context}
     * que reprecenta la actividad o vista actual con la que se esta trabajando
     * @param context
     */
    public FinalizarDescargaSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }
    //endregion
    //region Metodos obreescritos

    /**
     * onCreate
     * Se invoca cuando la base de datos es creada por primera vez. En este es donde las tablas
     * son creadas y la pobracion de estas sucede.
     * @param db Objeto {@link SQLiteDatabase} que reprecenta la base de datos.
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        //region Tabla de Finalizar descarga
        db.execSQL("CREATE TABLE "+TABLE_FINALIZAR_DESCARGA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "ClaveOperacion TEXT,"+
                "IdOrdenCompra INTEGER,"+
                "IdTipoMedidorTractor INTEGER,"+
                "IdTipoMedidorAlmacen INTEGER,"+
                "TanquePrestado BOOLEAN DEFAULT 1,"+
                "PorcentajeMedirorAlmacen DECIMAL,"+
                "PorcentajeMedidorTractor DECIMAL,"+
                "IdAlmacen INTEGER,"+
                "FechaDescarga TEXT,"+
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion
        //region Tabla de imagenes finalizar descarga
        db.execSQL("CREATE TABLE "+TABLE_IMAGENES_FINALIZAR_DESCARGA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Url TEXT," +
                "Imagen TEXT," +
                "ClaveOperacion TEXT,"+
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
    }

    /**
     * onUpgrade
     * Es invocado cuando la base de datos nesecita se actualizada. La implementacion
     * usara este metodo para eliminar las tablas, agregar tablas o para cualquier cosa
     * si es nesesario actualizar la version.
     * <p>
     * <p>
     * La documentación de SQLite ALTER TABLE puede ser encontrada en
     * <a href="http://sqlite.org/lang_altertable.html">este vinculo</a>. Si usted agrega nuevas columnas
     * puede utilizar ALTER TABLE  para agregar despues de dar vida a la tabla. Si usted renombra
     * o elimina columnas puede utilizar  ALTER TABLE  para renombrar la tabla antigua, despues de
     * crear la nueva tabla y poblar con el contenido de la anterior.
     * </p><p>
     * Este metodo se ejecuta en una transacción. Si existe una excepcion, todos los cambios
     * seran descartados automaticamente.
     * </p>
     * @param db Objeto {@link SQLiteDatabase} que reprecenta la base de datos.
     * @param oldVersion Número de tipo {@link int} que reprecenta la version vieja de la base
     *                   de datos
     * @param newVersion Número de tipo {@link int} que reprecenta la nueva version de la base
     *                   de datos
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_FINALIZAR_DESCARGA);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_IMAGENES_FINALIZAR_DESCARGA);
        onCreate(db);
    }
    //endregion
    //region Metodos de finalizar descarga
    //region Metodos de la tabla de finalizar descarga
    /**
     * InsertFinalizarDescarga
     * Permite realizar el registro en local de la finalización de descarga, se tomaran como
     * parametros un objeto de tipo {@link FinalizarDescargaDTO} que contendra los datos y un
     * {@link String} con la clave del proceso , tras finalizar retornara un valor de tipo
     * {@link Long} que retornara el id del regitro en caso de ser correcto, en caso contrario
     * retornara un -1
     * @param finalizarDescargaDTO Objeto de tipo {@link FinalizarDescargaDTO} con los datos
     *                             de la finalización de descarga
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave de proceso
     * @return Valor de tipo {@link Long} que en caso de ser mayor a -1 quiere decir que se
     *          registro en local
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    public Long InsertFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO,String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",ClaveOperacion);
        contentValues.put("IdOrdenCompra",finalizarDescargaDTO.getIdOrdenCompra());
        contentValues.put("IdTipoMedidorTractor",finalizarDescargaDTO.getIdTipoMedidorTractor());
        contentValues.put("IdTipoMedidorAlmacen",finalizarDescargaDTO.getIdTipoMedidorAlmacen());
        contentValues.put("TanquePrestado",finalizarDescargaDTO.getTanquePrestado());
        contentValues.put("PorcentajeMedirorAlmacen",finalizarDescargaDTO.getPorcentajeMedidorAlmacen());
        contentValues.put("PorcentajeMedidorTractor",finalizarDescargaDTO.getPorcentajeMedidorTractor());
        contentValues.put("IdAlmacen",finalizarDescargaDTO.getIdAlmacen());
        contentValues.put("FechaDescarga",finalizarDescargaDTO.getFechaDescarga());
        return db.insert(TABLE_FINALIZAR_DESCARGA,null,contentValues);
    }

    /**
     * GetFinalizarDescargaByClaveOperacion
     * Permite realizar la consulta de un registro de finalizar descarga, se enviara como parametro
     * un {@link String} que reprecenta la clave de operación y el metodo en caso de encontrar
     * algun registro lo retornara en una variable de tipo {@link Cursor}.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave de operación
     * @return Objeto de tipo {@link Cursor} con los datos de la consulta en caso de existir
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    public Cursor GetFinalizarDescargaByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_FINALIZAR_DESCARGA+" WHERE ClaveOperacion ='"
                +ClaveOperacion+"'",null);
    }

    /**
     * DeleteFinalizarDescarga
     * Permite realizar la eliminaciòn de un registro por medio de su clave unica , tras finalizar
     * retornara un valor de tipo {@link Integer} que reprecenta el numero de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave de opreación a
     *                     eliminar
     * @return Valor de tipo {@link Integer} que reprecenta el numero de registros eliminados.
     */
    public Integer EliminarFinalizarDescarga(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_FINALIZAR_DESCARGA,
                "ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }
    //endregion
    //region Metodos para la tabla de imagenes de finalizar descarga

    /**
     * <h3>InsertarImagenes</h3>
     * Permite realizar el registro de las imagenes de finalizar descarga, se requieren como parametros
     * un objeto de tipo {@link FinalizarDescargaDTO} el cual cotiene los datos requeridos y una cadena
     * de tipo {@link String} que reprecenta la clave unica de operación , se retornara un array de
     * tipo {@link Long} que contiene los id de los registros realizados en caso de ser correctos.
     * @param finalizarDescargaDTO Objeto de tipo {@link FinalizarDescargaDTO} con los datos
     *                             de la finalización de descarga
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave de operación
     * @return Array de tipo {@link Long} con los id registrados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    public Long[] InsertarImagenes(FinalizarDescargaDTO finalizarDescargaDTO,String ClaveOperacion){
        Long[] inserts = new Long[finalizarDescargaDTO.getImagenes().size()];
        SQLiteDatabase db =  this.getWritableDatabase();
        for (int x = 0; x< finalizarDescargaDTO.getImagenes().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("Url",finalizarDescargaDTO.getImagenesURI().get(x).toString());
            contentValues.put("Imagen",finalizarDescargaDTO.getImagenes().get(x));
            contentValues.put("ClaveOperacion",ClaveOperacion);
            inserts[x] = db.insert(TABLE_IMAGENES_FINALIZAR_DESCARGA,null,contentValues);
        }
        return inserts;
    }

    /**
     * <h3>EliminarImagenes</h3>
     * Permite realizar la eliminación de las imagenes que se encuentren registradas en local,
     * tomara como parametro un {@link String} que reprecenta la clave de operación de
     * finalizar descarga , al final retornara un valor de tipo {@link Integer} con el total de
     * registros eliminados.
     * @param ClaveOperacion Cadena {@link String} que reprecenta la clave de operación
     * @return Valor de tipo {@link Integer} con el total de registros afectados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    public Integer EliminarImagenes(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_IMAGENES_FINALIZAR_DESCARGA,
                "ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }

    /**
     * <h3>GetImagenesFinalizarDescargaByClaveOperacion</h3>
     * Permite realizar la consulta de la imagenes registradas en local de la finalización de
     * la descarga, obtiene como parametro un {@link String} con la clave de operación para
     * retornar un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave de operación
     * @return Objeto {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 28/08/2018
     */
    public Cursor GetImagenesFinalizarDescargaByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_IMAGENES_FINALIZAR_DESCARGA+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * Permite realizar una consulta de todos los registros de finalizar descarga, retornara
     * un objeto de tipo {@link Cursor} con los datos resultantes
     * @return Objeto {@link Cursor} con el resultado de los datos
     */
    public Cursor GetFinalizarDescargas() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_FINALIZAR_DESCARGA,
                null);
    }
    //endregion
    //endregion

}
