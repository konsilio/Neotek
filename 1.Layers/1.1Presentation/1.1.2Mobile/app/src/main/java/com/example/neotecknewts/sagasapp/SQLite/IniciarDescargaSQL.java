package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;

/**
 * InciarDescargaSQL
 * Clase que exitiende de android.database.SQLiteOpenHelper la cual,
 * permite la creación de la base de datos en el dispositivo
 * y la ejecucion de consultas SQLite en el dispositivo, este almacena
 * los datos de la sección de iniciar descarga
 * @author  Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
 * @date 20/08/2018
 * @lastupdate 24/08/2018
 * @company NEOTECK
 */

public class IniciarDescargaSQL extends SQLiteOpenHelper {
    //region Constantes
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_DESCARGAS = "iniciar_descarga";
    private static final String TABLE_DESCARGAS_IMAGENES = "iniciar_descarga_imagenes";
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
    public IniciarDescargaSQL(Context context) {
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
        //region Tabla de iniciar descargas
        db.execSQL(
                "CREATE TABLE "+TABLE_DESCARGAS+
                        "(Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                        "IdOrdenCompra INTEGER,"+
                        "ClaveOperacion TEXT,"+
                        "FechaDescarga DATE,"+
                        "NombreTipoMedidorTractor TEXT,"+
                        "NombreTipoMedidorAlmacen TEXT,"+
                        "IdTipoMedidorTractor INTEGER,"+
                        "IdTipoMedidorAlmacen INTEGER,"+
                        "CantidadFotosAlmacen INTEGER,"+
                        "CantidadFotosTractor INTEGER,"+
                        "TanquePrestado BOOLEAN DEFAULT 1,"+
                        "PorcentajeMedidorAlmacen DOUBLE,"+
                        "PorcentajeMedidorTractor DOUBLE,"+
                        "IdAlmacen INTEGER," +
                        "Falta BOOLEAN DEFAULT 1)"
        );
        //endregion
        //region Tabla de imagenes descargas
        db.execSQL(
                "CREATE TABLE "+TABLE_DESCARGAS_IMAGENES+
                        "(Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                        "ClaveOperacion TEXT,"+
                        "Imagen TEXT,"+
                        "Uri TEXT,"+
                        "Falta BOOLEAN DEFAULT 1)"
        );
        //endregion
    }

    /**
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
     *
     * @param db         La base de datos.
     * @param oldVersion La version anterior de la base de datos.
     * @param newVersion La nueva version de la base de datos.
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_DESCARGAS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_DESCARGAS_IMAGENES);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }
    //endregion
    //region Acciónes Iniciar descarga

    /**
     * InsertDescarga
     * Permite el registro de una nueva descarga en local, tras ser generada la tabla  se tomaran
     * los valores del modelo de la clase {@link IniciarDescargaDTO} y se colocara para la sentencia
     * insert para su registro en la tabla en local de descargas, al finalizar retornara el id del
     * registro , en caso de no registrar se retornara un -1
     * @param iniciarDescargaDTO Objeto {@link IniciarDescargaDTO} que contiene los valores a ser
     *                           registrados en la base de datos
     * @param ClaveOperacion {@link String} de clave unica de la operaciòn que le corresponde
     * @return Long que reprecenta el resultado del registro , en caso de ser -1 no se ha registrado
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     *
     */
    public Long InsertDescarga(IniciarDescargaDTO iniciarDescargaDTO,String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("IdOrdenCompra",iniciarDescargaDTO.getIdOrdenCompra());
        contentValues.put("ClaveOperacion",ClaveOperacion);
        contentValues.put("NombreTipoMedidorTractor",iniciarDescargaDTO.getNombreTipoMedidorTractor());
        contentValues.put("NombreTipoMedidorAlmacen",iniciarDescargaDTO.getIdTipoMedidorAlmacen());
        contentValues.put("IdTipoMedidorTractor",iniciarDescargaDTO.getIdTipoMedidorTractor());
        contentValues.put("IdTipoMedidorAlmacen",iniciarDescargaDTO.getIdTipoMedidorAlmacen());
        contentValues.put("CantidadFotosAlmacen",iniciarDescargaDTO.getCantidadFotosAlmacen());
        contentValues.put("CantidadFotosTractor",iniciarDescargaDTO.getCantidadFotosTractor());
        contentValues.put("TanquePrestado",iniciarDescargaDTO.isTanquePrestado());
        contentValues.put("PorcentajeMedidorAlmacen",iniciarDescargaDTO.getPorcentajeMedidorAlmacen());
        contentValues.put("PorcentajeMedidorTractor",iniciarDescargaDTO.getPorcentajeMedidorTractor());
        contentValues.put("IdAlmacen",iniciarDescargaDTO.getIdAlmacen());
        contentValues.put("FechaDescarga",iniciarDescargaDTO.getFechaDescarga());//Falta verificar si la fecha es digitada o es timestamp
        contentValues.put("Falta",true);
        return db.insert(TABLE_DESCARGAS,null,contentValues);
    }

    /**
     * GetDescargaByClaveOperacion
     * Permite consultar un registro de la clave de operaciónes , se tomara como parametro la
     * clave de operación, en caso de obtener algun registro se retornara en un objeto
     * {@link Cursor}.
     *
     * @param ClaveOperacion {@link String} de clave unica de la operaciòn que le corresponde
     * @return Objeto {@link Cursor} con los datos que se obtienen o un null en caso de no encontrar
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     *
     */
    public Cursor GetDescargaByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_DESCARGAS+" WHERE ClaveOperacion = '"
                +ClaveOperacion+"'",null);
    }

    /**
     * GetDescargaById
     * Permite obtener por medio de la id un registro de la descarga
     * @param Id {@link String} que reprecenta el Id del registro en base de datos
     * @return Un objeto de tipo {@link Cursor} con los valores de la consulta en caso de existir
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetDescargaById(String Id){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_DESCARGAS+" WHERE Id = '"
                +Id+"'",null);
    }

    /**
     * EliminarDescarga
     * Permite realizar la eliminación de un registro en la base de datos ,
     * se requerira como parametro un {@link String} que reprecenta la clave de operación y
     * este retornara un valor entero con el total de registro eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} de clave unica de la operaciòn
     * @return Valor de tipo {@link Integer} que reprecenta el número de registros eliminados.
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarDescarga(String ClaveOperacion) {
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_DESCARGAS,
                "ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }
    //endregion
    //region Acciónes para Imagenes de Iniciar descarga

    /**
     * IncertarImagenesDescarga
     * Permite realizar el registro de la imagenes en la base de datos local, se envian como
     * parametros un objeto de tipo {@link IniciarDescargaDTO} que contiene la imagenes y un
     * {@link String} que sera la clave de operación que le corresponde, tras finalizar de
     * registrar todas las imagenes se retornara un arreglo de tipo {@link Long} con los id
     * de las imagenes.
     * @param iniciarDescargaDTO Objeto Objeto {@link IniciarDescargaDTO} que contiene las listas
     *                           de tipo {@link java.util.List} con las imagenes
     * @param ClaveOperacion     {@link String} de la clave de operación
     * @return Un arreglo de tipo {@link Long} con los id de los datos registrados en base de datos
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
    public Long[] IncertarImagenesDescarga(IniciarDescargaDTO iniciarDescargaDTO,
                                           String ClaveOperacion){
        Long[] inserts = new Long[iniciarDescargaDTO.getImagenes().size()];
        SQLiteDatabase db = this.getWritableDatabase();
        for (int x = 0; x<iniciarDescargaDTO.getImagenes().size(); x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion",ClaveOperacion);
            contentValues.put("Imagen", iniciarDescargaDTO.getImagenes().get(x));
            contentValues.put("Uri",iniciarDescargaDTO.getImagenesURI().get(x).toString());
            inserts[x] =  db.insert(TABLE_DESCARGAS_IMAGENES,null, contentValues);
        }
        return inserts;
    }

    /**
     * GetImagenesDescargaByClaveUnica
     * Permite obtener por medio de la clave unica todas las imagenes que tiene esa descarga ,
     * se neviara como parametro un {@link String} la clave unica de la operación y se retornara
     * como resultado las imagenes referenciadas a esta en un objeto {@link Cursor}.
     * @param ClaveUnica {@link String} que reprecenta la clave unica de operación
     * @return Un objeto {@link Cursor} con los resultados de la consulta
     */
    public Cursor GetImagenesDescargaByClaveUnica(String ClaveUnica){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_DESCARGAS_IMAGENES+" WHERE ClaveOperacion = '"+ClaveUnica+"'",null);
    }

    /**
     * EliminarImagenesDescarga
     * Permite eliminar los registros de las imagenes de la base de datos, se enviara como parametro
     * un {@link String} que reprecenta la clave unica de la descarga, en caso de que se eliminen
     * los registro se retornara un valor de tipo {@link Integer} con la cantidad de registros
     * eliminados.
     * @param ClaveUnica {@link String} que reprecenta la clave única de la descarga
     * @return Valor de tipo {@link Integer} que reprecenta los registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarImagenesDescarga(String ClaveUnica){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_DESCARGAS_IMAGENES,"WHERE ClaveUnica = '"+ClaveUnica+"'",null);
    }
    //endregion
}
