package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.neotecknewts.sagasapp.Model.LecturaDTO;

public class SAGASSql extends SQLiteOpenHelper {
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_LECTURA_INICIAL = "lectura_inicial";
    private static final String TABLE_LECTURA_INICIAL_P5000 = "lectura_inicial_p5000";
    private static final String TABLE_LECTURA_INICIAL_IMAGENES = "lectura_inicial_imagenes";

    //region Constructor de clase
    /**
     * Constructor de clase , se tomara como parametro un objeto de tipo {@link Context }
     * que reprecenta la venta o activity actual con la que se invoca la base de datos
     * @param context
     */
    public SAGASSql(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }
    //endregion

    //region Metodos sobreescritos
    /**
     * onCreate
     * Se llama cuando la base de datos se crea por primera vez. Aquí es donde debería ocurrir la
     * creación de tablas y la población inicial de las tablas.
     * @param db Objeto {@link SQLiteDatabase} que permite la inteacción con bases de datos locales
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        //region Tabla lectura_inicial
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "IdTipoMedidor INTEGER," +
                "NombreTipoMedidor TEXT," +
                "CantidadFotografiasMedidor TEXT," +
                "NombreEstacionCarburacion TEXT," +
                "IdEstacionCarburacion INTEGER," +
                "CantidadP5000 INTEGER," +
                "PorcentajeMedidor DOUBLE," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_p5000
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_P5000+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
    }

    /**
     * onUpgrade
     * Se llama cuando la base de datos necesita ser actualizada.La implementación debería usar este
     * método para eliminar tablas, agregar tablas o hacer cualquier otra cosa que necesite para
     * actualizar a la nueva versión de esquema. Si agrega nuevas columnas, puede usar ALTER TABLE
     * para insertarlas en una tabla activa. Si cambia el nombre o elimina las columnas, puede usar
     * ALTER TABLE para cambiar el nombre de la tabla anterior, luego crear la nueva tabla y llenar
     * la nueva tabla con el contenido de la tabla anterior.
     * Este método se ejecuta dentro de una transacción. Si se lanza una excepción, todos
     * los cambios se revertirán automáticamente.
     * @param db Objeto {@link SQLiteDatabase} que permite la inteacción con bases de datos locales
     * @param oldVersion Valor de tipo int que reprecenta la verción mas vieja de la base de datos
     * @param newVersion Valor de tipo int que reprecenta la versión nueva de la base de datos
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_P5000);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_IMAGENES);
        onCreate(db);
    }
    //endregion

    //region Metodos para lectura inicial

    /**
     * <h3>InsertLecturaInicial</h3>
     * Permite realizar el registro de la lectura inicial, se enviaran como parametros ,
     * un objeto de tipo {@link LecturaDTO} con los valores a registrar en local y al final
     * se retorna un valor de tipo {@link Long} que reprecenta el número de registro en la
     * base de datos local, en caso de ser menor o igual a -1 no se registro correctamente.
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Valor de tipo {@link Long} que tiene el id registrado en base de datos
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long InsertLecturaInicial(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("IdTipoMedidor",lecturaDTO.getIdTipoMedidor());
        contentValues.put("NombreTipoMedidor",lecturaDTO.getNombreTipoMedidor());
        contentValues.put("CantidadFotografiasMedidor",lecturaDTO.getCantidadFotografias());
        contentValues.put("NombreEstacionCarburacion",lecturaDTO.getNombreEstacionCarburacion());
        contentValues.put("IdEstacionCarburacion",lecturaDTO.getIdEstacionCarburacion());
        contentValues.put("CantidadP5000",lecturaDTO.getCantidadP500());
        contentValues.put("PorcentajeMedidor",lecturaDTO.getPorcentajeMedidor());

        return db.insert(TABLE_LECTURA_INICIAL,null,contentValues);
    }

    /**
     * <h3>GetLecturaByClaveUnica</h3>
     * Permite consultar un registro de la lectura inicial por medio de la clave ùnica, se envia de
     * parametro un {@link String} que reprecenta dicha clave y retornara un objeto de tipo
     * {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL+" WHERE ClaveProceso = '"
                +ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLectura</h3>
     * Permite realizar la eliminación de un registro de la lectura inicial, se envia de parametro
     * una cadena de tipo {@link String} que reprecenta la clave única de proceso y en caso de
     * que se elimine retornara el numero de registro eliminados en caso contrario, retornara
     * un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLectura(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL,"ClaveProceso = "+
                ClaveProceso,null);
    }
    //endregion

    //region Metodos para la lectura del P5000

    /**
     * <h3>InsertLecturaP5000</h3>
     * Permite realizar el registro de la imagen del P5000, tomara como paramreto un
     * objeto de tipo {@link LecturaDTO} que contiene la información de la imagen,
     * al final retornara un valor de tipo {@link Long} con el resultado del registro.
     * @param lecturaDTO  Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con el id del registro , en caso de ser -1
     *          el regitro no fue correcto
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long InsertLecturaP5000(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("Imagen",lecturaDTO.getImagenP500());
        contentValues.put("Url",lecturaDTO.getImagenP500URI().toString());
        return db.insert(TABLE_LECTURA_INICIAL_P5000,null,contentValues);
    }

    /**
     * <h3>GetLecturaP500ByClaveUnica</h3>
     * Permite consultar un registro de las imagenes DEL P5000 de lectura inicial por medio de la
     * clave ùnica, se envia de parametro un {@link String} que reprecenta dicha clave y retornara
     * un objeto de tipo {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaP500ByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_P5000+" WHERE ClaveProceso = '"
                +ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLecturaP5000</h3>
     * Permite realizar la eliminación de un registro de las imagenes de P5000
     * lectura inicial, se envia de parametro una cadena de tipo {@link String} que reprecenta
     * la clave única de proceso y en caso de que se elimine retornara el numero de registro
     * eliminados en caso contrario,  retornara un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLecturaP5000(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_P5000,"ClaveProceso = "+
                ClaveProceso,null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura inicial
    /**
     * <h3>InsertLecturaImagenes</h3>
     * Permite realizar el registro de las imagenes, tomara como paramreto un
     * objeto de tipo {@link LecturaDTO} que contiene la información de la imagen,
     * al final retornara un valor de tipo {@link Long} con el resultado del registro.
     * @param lecturaDTO  Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con los ids del registros , en caso de ser -1
     *          el regitro no fue correcto
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long[] InsertLecturaImagenes(LecturaDTO lecturaDTO){
        Long[] inserts = new Long[lecturaDTO.getImagenesURI().size()];
        SQLiteDatabase db = this.getWritableDatabase();
        for (int x = 0; x<lecturaDTO.getImagenesURI().size();x++) {
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveProceso", lecturaDTO.getClaveProceso());
            contentValues.put("Imagen", lecturaDTO.getImagenP500());
            contentValues.put("Url", lecturaDTO.getImagenP500URI().toString());
            inserts[x] = db.insert(TABLE_LECTURA_INICIAL_IMAGENES,null,contentValues);
        }
        return inserts;
    }

    /**
     * <h3>GetLecturaImagenesByClaveUnica</h3>
     * Permite consultar  las imagenes de lectura inicial por medio de la
     * clave ùnica, se envia de parametro un {@link String} que reprecenta dicha clave y retornara
     * un objeto de tipo {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaImagenesByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_IMAGENES+
                " WHERE ClaveProceso = '"+ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLecturaImagenes</h3>
     * Permite realizar la eliminación de las imagenes para la lectura inicial,
     * se envia de parametro una cadena de tipo {@link String} que reprecenta
     * la clave única de proceso y en caso de que se elimine retornara el número de registros
     * eliminados en caso contrario,  retornara un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLecturaImagenes(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_IMAGENES,"ClaveProceso = "+
                ClaveProceso,null);
    }
    ///endregion
}
